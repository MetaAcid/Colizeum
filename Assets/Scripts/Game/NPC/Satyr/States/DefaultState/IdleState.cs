namespace NPC.States.DefaultState
{
    public class IdleState : SatyrState
    {
        public override void Enter()
        {
            StateMachine.NavMeshAgent.SetDestination(StateMachine.transform.position);
        }

        public override void Process()
        {
            
        }
    }
}