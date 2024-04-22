using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeTurnPlayer : MonoBehaviour
{
    [SerializeField] private ChangeTurnManager changeTurn;
    public Transform doorInZone;
    private bool playerInZone;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Door")) return;
        playerInZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Door")) return;
        playerInZone = false;
    }

    public void ChangePlayerPosition(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.performed || !playerInZone) return;
        changeTurn.ChangePlayerPosition();
    }
    
    // public void ChangePlayerPosition(InputAction.CallbackContext callbackContext)
    // {
    //     if (!callbackContext.performed || !playerInZone) return;
    //     changeTurn.ChangePlayerPosition();
    //     GetComponent<CharacterController>().enabled = false;
    //     GetComponent<PlayerInput>().enabled = false;
    //     playerOther.GetComponent<CharacterController>().enabled = false;
    //     playerOther.GetComponent<PlayerInput>().enabled = false;
    //
    //     var newPosPlayer1 = new Vector3(playerOther.position.x, 1.08f, playerOther.position.z);
    //     var newPosPlayer2 = new Vector3(transform.position.x, 1.08f, transform.position.z);
    //     
    //     transform.position = newPosPlayer1;
    //     playerOther.position = newPosPlayer2;
    //
    //     playerInZone = false;
    //     canTeleport = false;
    //     
    //     UIGameManager.instance.PanelChangeTime(true);
    //     
    //     Invoke("EnableTeleport", 1.5f);
    // }
}
