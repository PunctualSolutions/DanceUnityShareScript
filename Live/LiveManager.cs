using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using OpenBLive.Runtime.Data;
using PunctualSolutionsTool.CommonLive;
using UnityEngine;

namespace PunctualSolutions.Dance.Live
{
    public class LiveManager : MonoBehaviour
    {
        [SerializeField] LiveConfig _config;
        ILiveServer                 _liveServer;

        public Task<Guard>        WaitGuardBuy(CancellationTokenSource     source = default) => _liveServer.WaitGuardBuy(source);
        public Task<Gift>         WaitGift(CancellationTokenSource         source = default) => _liveServer.WaitGift(source);
        public Task<Commentaries> WaitCommentaries(CancellationTokenSource source = default) => _liveServer.WaitCommentaries(source);
        public Task<LikeInfo>     WaitLike(CancellationTokenSource         source = default) => _liveServer.WaitLike(source);

        /// <summary>
        /// 外部要先调用此方法再去等待其他消息
        /// </summary>
        public async UniTask LinkLive() => _liveServer = await new BiliBiliLiveServerFactory(_config.AccessKeySecret, _config.AccessKeyId, _config.Code, _config.AppId).Get();
    }
}