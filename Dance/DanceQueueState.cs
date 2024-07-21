using PunctualSolutions.Tool.StateMachine;

namespace PunctualSolutions.Dance.Dance.Dance
{
    public abstract class DanceQueueState : IState
    {
        static          DanceRequest CurrentRequest => DanceQueueManager.Instance.CurrentRequest;
        protected       Dancer       CurrentDancer  => CurrentRequest.Dancer;
        public abstract void         Enter();
        public abstract void         FixedUpdate();
        public abstract void         LogicUpdate();
        public abstract void         Exit();
    }
}