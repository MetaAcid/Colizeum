namespace Game.NPC.Cyclope
{
    public abstract class CyclopeState
    {
        protected CyclopeStateMachine StateMachine { get; private set; }
        protected Cyclope Cyclope { get; private set; }

        public virtual void Enter() {}
        public virtual void Exit() {}
        
        public virtual void Initialize(CyclopeStateMachine cyclopeStateMachine, Cyclope cyclope)
        {
            StateMachine = cyclopeStateMachine;
            Cyclope = cyclope;
        }

        public abstract void Process();
        public virtual void DrawGizmos(){}
    }
}