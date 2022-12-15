using Game.NPC;

namespace NPC
{
    public abstract class EnemyState
    {
        protected EnemyDefaultStateMachine StateMachine { get; private set; }
        protected Enemy Enemy { get; private set; }

        public virtual void Enter() {}
        public virtual void Exit() {}
        
        public virtual void Initialize(EnemyDefaultStateMachine enemyDefaultStateMachine, Enemy enemy)
        {
            StateMachine = enemyDefaultStateMachine;
            Enemy = enemy;
        }

        public abstract void Process();
        public virtual void DrawGizmos(){}
    }
}