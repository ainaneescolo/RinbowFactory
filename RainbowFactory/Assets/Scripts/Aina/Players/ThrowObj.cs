using UnityEngine;

public class ThrowObj : MonoBehaviour
{
    [Header("----- Object Variables -----")] 
    private GameObject _objToThrow;
    private bool playerInZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GarbageCan"))
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GarbageCan"))
        {
            playerInZone = false;
        }
    }

    public void ThrowObjToCan()
    {
        if (!playerInZone || _objToThrow == null) return;
        if (_objToThrow.GetComponent<Package>())
        {
            _objToThrow.GetComponent<Package>().ThrowPackage();
        }
        else
        {
            _objToThrow.GetComponent<PaintBucket>().ThrowBucket();
        }
    }
}
