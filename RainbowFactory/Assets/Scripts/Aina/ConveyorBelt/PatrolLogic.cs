using UnityEngine;

public class PatrolLogic : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject nextWayPoint;

    public Transform NextWayPoint => nextWayPoint.transform;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickableObject"))
        {
            if (other.TryGetComponent(out PackageMoveLogic packageMoveLogic))
            {
                packageMoveLogic.targeto = nextWayPoint.transform;
                if (packageMoveLogic.isPicked) return;
                packageMoveLogic.ImInConveyorBelt = true;
                var positionPackage = new Vector3(packageMoveLogic.transform.position.x, transform.position.y, packageMoveLogic.transform.position.z);
                packageMoveLogic.InitializeMovementToWayPoint(nextWayPoint.transform, speed, positionPackage);
            }
        }
    }
}
