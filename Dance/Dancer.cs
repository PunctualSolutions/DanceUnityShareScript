using Aya.TweenPro;
using PunctualSolutions.Tool.Audio;
using UnityEngine;

namespace PunctualSolutions.Dance.Dance
{
    public class Dancer : MonoBehaviour
    {
        [SerializeField]               Animator    animator;
        [SerializeField]               DanceConfig config;
        [field: SerializeField] public float       MoveTime      { get; private set; }
        public                         float       DanceTime     => config.DanceTime;
        public                         ActionType  CurrentState  { get; private set; }
        public                         Vector3     StartPosition { get; private set; }

        void Awake()
        {
            if (!config) return;
            animator.runtimeAnimatorController = config.AnimatorController;
            StartPosition                      = transform.position;
            CurrentState                       = ActionType.Idle;
        }

        public void Idle(Vector3 worldPosition)
        {
            CurrentState = ActionType.Idle;
            transform.LookAt(worldPosition);
            animator.Play("Idle");
        }

        public void Dance(float eulerAnglesY)
        {
            CurrentState          = ActionType.Dance;
            transform.eulerAngles = new(transform.eulerAngles.x, eulerAnglesY, transform.eulerAngles.z);
            animator.Play("Dance");
            AudioManager.Instance.PlayBgm(config.Bgm);
        }

        public void Move(Vector3 target)
        {
            CurrentState = ActionType.Move;
            animator.Play("Move");
            transform.LookAt(target);
            UTween.Position(transform, target, MoveTime);
        }
    }
}