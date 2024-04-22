using UnityEngine;

public class GarbageCan : MonoBehaviour
{
    [SerializeField] private AudioClip garbageClip;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out RobotMovement robot))
        {
            if (robot.objPlayer == null) return;
            LevelManager.instance.PlaySound(garbageClip);
        }

        // if (other.GetComponent<PackageMoveLogic>())
        // {
        //     if (other.GetComponent<PackageMoveLogic>().ImInConveyorBelt && !other.GetComponent<PackageMoveLogic>().isPicked)
        //     {
        //         if (other.GetComponent<Package>())
        //         {
        //             other.GetComponent<Package>().ThrowPackage();
        //         }
        //         else
        //         {
        //             other.GetComponent<PaintBucket>().ThrowBucket();
        //         }
        //         LevelManager.instance.PlaySound(garbageClip);
        //     }
        // }
        
        if (!other.GetComponent<PackageMoveLogic>()) return;
        if (!other.GetComponent<PackageMoveLogic>().ImInConveyorBelt) return;
        if (other.GetComponent<PackageMoveLogic>().isPicked) return;
        
        if (other.GetComponent<Package>())
        {
            other.GetComponent<Package>().ThrowPackage();
        }
        else
        {
            other.GetComponent<PaintBucket>().ThrowBucket();
        }
        LevelManager.instance.PlaySound(garbageClip);
    }
}
