using UnityEngine;

namespace PunctualSolutions.Dance.Live
{
    [CreateAssetMenu(fileName = "Live", menuName = "PunctualSolutions/Dance/Live")]
    public class LiveConfig : ScriptableObject
    {
        [field: SerializeField] public string AccessKeySecret { get; private set; }
        [field: SerializeField] public string AccessKeyId     { get; private set; }
        [field: SerializeField] public string Code            { get; private set; }
        [field: SerializeField] public long   AppId           { get; private set; }
    }
}