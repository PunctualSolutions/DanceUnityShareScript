using PunctualSolutions.Tool.StateMachine;

namespace PunctualSolutions.Dance.Dance.Dance
{
    public abstract class DanceQueueState : IState
    {
        readonly DanceQueueManager _manager;
        DanceRequest               CurrentRequest => _manager.CurrentRequest;
        protected       Dancer     CurrentDancer  => CurrentRequest.Dancer;
        public abstract void       Enter();
        public abstract void       FixedUpdate();
        public abstract void       LogicUpdate();
        public abstract void       Exit();
        protected DanceQueueState(DanceQueueManager manager) => _manager = manager;
    }
}