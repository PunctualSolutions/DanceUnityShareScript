using UnityEngine;

namespace PunctualSolutions.Dance.Dance
{
    [CreateAssetMenu(fileName = "Dance", menuName = "PunctualSolutions/Dance/Dance")]
    public class DanceConfig : ScriptableObject
    {
        [field: SerializeField] public RuntimeAnimatorController AnimatorController { get; set; }
        [field: SerializeField] public AudioClip                 Bgm                { get; set; }
        [field: SerializeField] public float                     DanceTime          { get; set; }
    }
}