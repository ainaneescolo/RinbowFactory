using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectPickup : MonoBehaviour
{
    [Header("----- Obj Variables -----")] 
    public bool alreadyWithObj;
    private bool objectInZone;
    private bool platformInZone;
    // private bool cintaInZone;
    // private bool paintZone;

    private int dropObjIndex;
    
    private PaintZone _paintZone;
    private GameObject objectToPick;
    private Transform platformSnap;

    [Header("----- Game Variables -----")]
    [SerializeField] private RobotMovement robot;

    private GameObject playerHand;
    
    public GameObject ObjectInHand;
    [SerializeField] private AudioClip objPickUp;

    private void Start()
    {
        var prefab = transform.GetChild(2).transform;
        playerHand = prefab.GetChild(prefab.childCount-1).gameObject;
    }

    public void InteractObj(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.performed) return;
        
        if (objectInZone && !alreadyWithObj)
        {
            objectInZone = false;
            alreadyWithObj = true;
            ObjectInHand = objectToPick;
            ObjectInHand.transform.SetParent(playerHand.transform);
            ObjectInHand.transform.position = playerHand.transform.position;
            ObjectInHand.transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
            ObjectInHand.GetComponent<PackageMoveLogic>().ImInConveyorBelt = false;
            ObjectInHand.GetComponent<PackageMoveLogic>().isPicked = true;

            if (platformInZone && dropObjIndex == 3 )
            {
                if (objectToPick.GetComponent<Package>())
                {
                    _paintZone.package = null;
                }
                else
                {
                    _paintZone.paintBucket = null;
                }
            }
            
            if (ObjectInHand.GetComponent<PaintBucket>())
            {
                GetComponent<PlayerPaint>().paintBucket = ObjectInHand.GetComponent<PaintBucket>();
            }
            
            GetComponent<PlayerAnimations>().BoolAnim("Grab", true);
            LevelManager.instance.PlaySound(objPickUp);
            objectToPick = null;
        }
        else if(platformInZone && dropObjIndex != 0 && alreadyWithObj)
        {
            //&& alreadyWithObj && (platformSnap != null || (platformSnap == null && cintaInZone) || paintZone)
            LevelManager.instance.PlaySound(objPickUp);
            //objectToPick = ObjectInHand;
            //objectInZone = objectToPick;
            alreadyWithObj = false;
            playerHand.transform.DetachChildren();
            ObjectInHand.transform.localScale = new Vector3(1, 1, 1);
            GetComponent<PlayerAnimations>().BoolAnim("Grab", false);
            
            switch (dropObjIndex)
            {
                case 1:
                    ObjectInHand.transform.SetParent(platformSnap);
                    ObjectInHand.transform.localPosition = Vector3.zero;
                    break;
                case 2:
                    ObjectInHand.TryGetComponent(out PackageMoveLogic packageMoveLogic);
                    packageMoveLogic.isPicked = false;
                    packageMoveLogic.ImInConveyorBelt = true;
                    var positionPackage = new Vector3(packageMoveLogic.transform.position.x, 
                        packageMoveLogic.targeto.transform.position.y, packageMoveLogic.transform.position.z);
                    packageMoveLogic.InitializeMovementToWayPoint(packageMoveLogic.targeto, 1f, positionPackage);
                    break;
                case 3:
                    Transform parent = null;
                    _paintZone.package = ObjectInHand.GetComponent<Package>();
                    parent = _paintZone.snapPoint1;

                    ObjectInHand.transform.SetParent(parent);
                    ObjectInHand.transform.localPosition = Vector3.zero;
                    break;
            }
            
            if (ObjectInHand.GetComponent<PaintBucket>())
            {
                GetComponent<PlayerPaint>().paintBucket = null;
            }
            
            if (robot != null && robot.playerInZone)
            {
                robot.playerInZone = false;
            }
            
            ObjectInHand = null;
            dropObjIndex = 0;

            // if (paintZone)
            // {
            //     Transform parent = null;
            //     _paintZone.package = ObjectInHand.GetComponent<Package>();
            //     parent = _paintZone.snapPoint1;
            //
            //     objectToPick.transform.SetParent(parent);
            //     objectToPick.transform.localPosition = Vector3.zero;
            // }
            //
            // LevelManager.instance.PlaySound(objPickUp);
            // objectInZone = transform;
            // objectToPick = ObjectInHand;
            // alreadyWithObj = false;
            // ObjectInHand = null;
            // playerHand.transform.DetachChildren();
            // objectToPick.transform.localScale = new Vector3(1, 1, 1);
            //
            // if (platformSnap != null)
            // {
            //     objectToPick.transform.SetParent(platformSnap);
            //     objectToPick.transform.localPosition = Vector3.zero;
            // }
            //
            // GetComponent<PlayerAnimations>().BoolAnim("Grab", false);
            //
            // if (robot != null && robot.playerInZone)
            // {
            //     robot.playerInZone = false;
            // }
            //
            // objectToPick.TryGetComponent(out PackageMoveLogic packageMoveLogic);
            // packageMoveLogic.isPicked = false;
            //
            // if (!cintaInZone)return;
            // packageMoveLogic.ImInConveyorBelt = true;
            // var positionPackage = new Vector3(packageMoveLogic.transform.position.x, packageMoveLogic.targeto.transform.position.y,
            //     packageMoveLogic.transform.position.z);
            // packageMoveLogic.InitializeMovementToWayPoint(packageMoveLogic.targeto, 1f, positionPackage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickableObject"))
        {
            objectToPick = other.gameObject;
            objectInZone = true;
        }
        
        if (other.CompareTag("Surface"))
        {
            if (other.transform.childCount != 1 || other.transform.GetChild(0).childCount != 0) return;
            dropObjIndex = 1;
            platformInZone = true;
            platformSnap = other.transform.GetChild(0).transform;
        }
        else if (other.GetComponent<PatrolLogic>())
        {
            dropObjIndex = 2;
            //cintaInZone = true;
            platformInZone = true;
        }
        else if (other.GetComponent<PaintZone>() && ObjectInHand)
        {
            if (!ObjectInHand.GetComponent<Package>())return;
            _paintZone = other.GetComponent<PaintZone>();
            if (_paintZone.snapPoint1.childCount != 0) return;
            dropObjIndex = 3;
            //paintZone = true;
            platformInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickableObject"))
        {
            objectInZone = false;
        }
        
        if (other.CompareTag("Surface"))
        {
            platformInZone = false;
            platformSnap = null;
            dropObjIndex = 0;
        }
        else if (other.GetComponent<PatrolLogic>())
        {
            dropObjIndex = 0;
            //cintaInZone = true;
            platformInZone = false;
        }
        else if (other.GetComponent<PaintZone>())
        {
            platformInZone = false;
            //paintZone = false;
            _paintZone = null;
            dropObjIndex = 0;
        }
    }

    public bool ObjRobotAttack()
    {
        return ObjectInHand.GetComponent<PaintBucket>().colorFinal.material != null || ObjectInHand.GetComponent<Package>();
    }
}
