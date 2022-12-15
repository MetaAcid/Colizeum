using Game.Player;
using Game.PlayerMechanics;
using UnityEngine;

namespace NPC.States
{
    public struct PointDestination
    {
        public float Distance { get; set; }
        public Transform Point { get; set; }

        public PointDestination(float distance, Transform point)
        {
            Distance = distance;
            Point = point;
        }
    }
    
    public class FearState : EnemyState
    {
        private Transform[] _areaPoints;

        public FearState(Transform[] areaPoints)
        {
            _areaPoints = areaPoints;
        }
        
        public override void Process()
        {
            if (Enemy.IsSaveDistance())
            {
                StateMachine.SetState(StateMachine.SearchingBoostState);
                return;
            }
            Debug.Log(GetBestPoint().name);
            StateMachine.SetDestination(GetBestPoint().position, 0, () =>
            {
                Debug.Log("Finish!");
            });
        }

        private Transform GetBestPoint()
        {
            PointDestination pointDestination = new PointDestination(
                Vector3.Distance(Player.Instance.transform.position, _areaPoints[0].position), 
                _areaPoints[0]);

            foreach (var point in _areaPoints)
            {
                float pointDistance = Vector3.Distance(Player.Instance.transform.position, point.position);

                if (pointDestination.Distance > pointDistance) continue;
                
                pointDestination.Distance = pointDistance;
                pointDestination.Point = point;
            }
            return pointDestination.Point;
        }
    }
}