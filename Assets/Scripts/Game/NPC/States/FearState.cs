using Game.Player;
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

        private Transform _targetPoint;

        public FearState(Transform[] areaPoints)
        {
            _areaPoints = areaPoints;
        }
        
        public override void Process()
        {
            _targetPoint = GetBestPoint();
            
            StateMachine.SetDestination(_targetPoint.position, 0, () =>
            {
                Debug.Log("Finish!");
            });
        }

        private Transform GetBestPoint()
        {
            PointDestination pointDestination = new PointDestination(float.MaxValue, _areaPoints[0]);
            
            foreach (var point in _areaPoints)
            {
                float pointDistance = Vector3.Distance(Player.Instance.transform.position, point.position);

                if (!(pointDestination.Distance < pointDistance)) continue;
                
                pointDestination.Distance = pointDistance;
                pointDestination.Point = point;
            }
            return pointDestination.Point;
        }
    }
}