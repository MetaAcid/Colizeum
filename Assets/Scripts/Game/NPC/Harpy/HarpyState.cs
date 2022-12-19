using UnityEngine;

namespace Game.NPC.Harpy
{
    public abstract class HarpyState
    {
        protected HarpyStateMachine StateMachine { get; private set; }
        protected Harpy Harpy { get; private set; }

        public virtual void Enter() {}
        public virtual void Exit() {}
        
        public virtual void Initialize(HarpyStateMachine harpyStateMachine, Harpy harpy)
        {
            StateMachine = harpyStateMachine;
            Harpy = harpy;
        }

        public abstract void Process();
        public virtual void DrawGizmos(){}
    }
}