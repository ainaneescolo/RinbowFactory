using System.Collections;
using UnityEngine;

public class PackageMoveLogic : MonoBehaviour
{
    [SerializeField] public Transform targeto;
    [SerializeField] private bool imInConveyorBelt;
    private Coroutine _currentCoroutine;
    
    public bool isPicked;
    
    public bool ImInConveyorBelt
    {
        get => imInConveyorBelt;
        set => imInConveyorBelt = value;
    }
    
    public void InitializeMovementToWayPoint(Transform target, float speed, Vector3 position)
    {
        transform.position = position;
        //targeto = target;
        if (_currentCoroutine != null) return;
        //_currentCoroutine = null;
        _currentCoroutine = StartCoroutine(MovePackageToNextWayPoint(target, speed));
    }

    IEnumerator MovePackageToNextWayPoint(Transform target, float speed)
    {
        while (imInConveyorBelt && !isPicked)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                target.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                if (target.TryGetComponent(out PatrolLogic patrolLogic))
                {
                    var nextWayPoint = patrolLogic.NextWayPoint;
                    if (nextWayPoint == target) // Evitamos consumir recursos
                    {
                        _currentCoroutine = null;
                        yield break;
                    }
                    
                    target = nextWayPoint;
                }
                else
                {
                    yield break;
                }
            }
            yield return null;
        }

        _currentCoroutine = null;
    }

    public void ResetMovement()
    { 
        imInConveyorBelt = false;
        isPicked = false;
        _currentCoroutine = null;
    }
}
