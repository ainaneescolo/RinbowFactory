using UnityEngine;

public class RobotBlocker : MonoBehaviour
{
    [SerializeField] private Transform nextWayPoint;
    [SerializeField] private RobotMovement robotMovement;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!robotMovement.playerInZone) return;

            robotMovement.playerInZone = false;
            robotMovement.MovementPatrol(nextWayPoint);
        }
    }
}
