using Cysharp.Threading.Tasks;
using PunctualSolutions.Dance.Dance;
using PunctualSolutions.Dance.Live;
using PunctualSolutionsTool.CommonLive;
using UnityEngine;

namespace PunctualSolutions.Dance.Main
{
    public class MainManager : MonoBehaviour
    {
        [SerializeField] DanceQueueManager _danceQueueManager;
        [SerializeField] LiveManager       _liveManager;
        [SerializeField] Dancer            dancer0;
        [SerializeField] Dancer            dancer1;
        [SerializeField] Dancer            dancer2;

        void Start()
        {
            Init().Forget();
            return;

            async UniTask Init()
            {
                await _liveManager.LinkLive();
                await CheckGift();
                return;

                async UniTask CheckGift()
                {
                    while (!destroyCancellationToken.IsCancellationRequested)
                        switch ((await _liveManager.WaitGift()).Id)
                        {
                            case GiftId.小花花:
                                _danceQueueManager.PushDanceRequest(new(dancer1));
                                break;
                            case GiftId.牛蛙牛蛙:
                                _danceQueueManager.PushDanceRequest(new(dancer0));
                                break;
                            case GiftId.人气票:
                                _danceQueueManager.PushDanceRequest(new(dancer2));
                                break;
                        }
                }
            }
        }
    }
}