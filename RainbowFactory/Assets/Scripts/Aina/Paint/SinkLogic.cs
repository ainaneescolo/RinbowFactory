using UnityEngine;

public class SinkLogic : MonoBehaviour
{
    public bool playerInZone;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInZone = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInZone = false;
    }
}
