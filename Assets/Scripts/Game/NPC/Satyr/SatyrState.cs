using Game.NPC;

namespace NPC
{
    public abstract class SatyrState
    {
        protected SatyrStateMachine StateMachine { get; private set; }
        protected Satyr Satyr { get; private set; }

        public virtual void Enter() {}
        public virtual void Exit() {}
        
        public virtual void Initialize(SatyrStateMachine satyrStateMachine, Satyr satyr)
        {
            StateMachine = satyrStateMachine;
            Satyr = satyr;
        }

        public abstract void Process();
        public virtual void DrawGizmos(){}
    }
}