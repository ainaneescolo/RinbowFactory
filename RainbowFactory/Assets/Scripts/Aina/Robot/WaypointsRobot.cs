using UnityEngine;

public class WaypointsRobot : MonoBehaviour
{
    [SerializeField] private Transform nextWayPoint;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out RobotMovement robotMovement))
        {
            if (robotMovement.playerInZone) return;
            robotMovement.targetPatrol = nextWayPoint;
        }
    }
}
