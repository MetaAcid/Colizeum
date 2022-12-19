using Common;

namespace Game.NPC.Harpy.States
{
    public class IdleState : HarpyState
    {
        public override void Enter()
        {
            StateMachine.StartCoroutine(Utils.MakeActionDelay(() =>
            {
                StateMachine.SetState(StateMachine.AttackState);
            }, 3));            
        }

        public override void Process()
        {
            
        }
    }
}