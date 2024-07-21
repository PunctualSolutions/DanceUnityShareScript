using PunctualSolutions.Dance.Dance.Dance;

namespace PunctualSolutions.Dance.Dance
{
    public class DanceQueueStateMachine : Tool.StateMachine.StateMachine
    {
        public bool IsIdle => CurrentState.GetType() == typeof(DanceQueueStateIdle);
    }
}