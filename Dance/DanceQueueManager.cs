using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using PunctualSolutions.Dance.Dance.Dance;
using PunctualSolutions.Tool.Audio;
using PunctualSolutionsTool.Tool;
using UnityEngine;

namespace PunctualSolutions.Dance.Dance
{
    public class DanceQueueManager : MonoSingleton<DanceQueueManager>
    {
        [SerializeField]               float                  checkInterval;
        [SerializeField]               Transform              centerPosition;
        [field: SerializeField] public float                  CenterEulerAnglesY { get; private set; }
        [field: SerializeField] public AudioClip              DefaultBgm         { get; private set; }
        [field: SerializeField] public AudioClip              NewYearBgm         { get; private set; }
        [field: SerializeField] public AudioClip              ClockCountDownSe   { get; private set; }
        [SerializeField]               GameObject             clock;
        readonly                       Queue<DanceRequest>    _danceRequests = new();
        readonly                       DanceQueueStateMachine _stateMachine  = new();
        float                                                 _timer;
        public DanceRequest                                   CurrentRequest { get; private set; }
        Vector3                                               CenterPos      => centerPosition.position;
        Dancer                                                CurrentDancer  => CurrentRequest.Dancer;
        Dancer                                                LastDancer     { get; set; }
        bool                                                  IsCanEnqueue   { get; set; }

        public void Start()
        {
            _stateMachine.Begin<DanceQueueStateIdle>();
            AudioManager.Instance.PlayBgm(DefaultBgm);
            IsCanEnqueue = true;
            clock.SetActive(false);
            CheckQueue().Forget();
        }

        public void Update()
        {
            _stateMachine.Update();
        }

        void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }

        async UniTask CheckQueue()
        {
            while (!destroyCancellationToken.IsCancellationRequested)
            {
                if (_danceRequests.Count == 0 ||
                    !_stateMachine.IsIdle     ||
                    _danceRequests.Peek().Dancer.CurrentState is ActionType.Move or ActionType.Dance) break;
                await OnDanceRequestStart(_danceRequests.Dequeue());
                await checkInterval.Delay();
            }
        }

        public void PushDanceRequest(DanceRequest request)
        {
            if (request == null) return;
            _danceRequests.Enqueue(request);
        }

        async UniTask OnDanceRequestStart(DanceRequest request)
        {
            if (request == null) return;
            CurrentRequest = request;
            _stateMachine.SwitchState<DanceQueueStateDance>();
            CurrentDancer.Move(CenterPos);
            await CurrentDancer.MoveTime.Delay();
            CurrentDancer.Dance(CenterEulerAnglesY);
            await CurrentDancer.DanceTime.Delay();
            CurrentDancer.Move(CurrentDancer.StartPosition);
            OnDanceRequestFinished();
            await CurrentDancer.MoveTime.Delay();
            LastDancer.Idle(CenterPos);
        }

        void OnDanceRequestFinished()
        {
            _stateMachine.SwitchState<DanceQueueStateIdle>();
            LastDancer     = CurrentDancer;
            CurrentRequest = null;
            AudioManager.Instance.PlayBgm(DefaultBgm);
        }
    }
}