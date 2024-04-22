using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDelivery : MonoBehaviour
{
    private DeliveryPackage deliveryPackage;
    private GameObject playerHand;
    private bool playerInZone;

    private void Start()
    {
        var prefab = transform.GetChild(2).transform;
        playerHand = prefab.GetChild(prefab.childCount-1).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerHand.transform.childCount == 0) return;
        if (!other.gameObject.GetComponent<DeliveryPackage>() 
            || !playerHand.transform.GetChild(0).GetComponent<Package>()) return;

        deliveryPackage = other.gameObject.GetComponent<DeliveryPackage>();
        playerInZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.GetComponent<DeliveryPackage>()) return;
        deliveryPackage = null;
        playerInZone = false;
    }

    public void SendPackage(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.performed || !playerInZone || playerHand.transform.childCount < 1) return;
        deliveryPackage.CheckPackageInfo(playerHand.transform.GetChild(0).GetComponent<Package>());
        GetComponent<ObjectPickup>().alreadyWithObj = false;
        playerHand.transform.DetachChildren();
        GetComponent<PlayerAnimations>().BoolAnim("Grab", false);
    }
}
