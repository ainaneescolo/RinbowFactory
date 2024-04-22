using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeTurnManager : MonoBehaviour
{
    [SerializeField] private ChangeTurnPlayer player1Reference;
    [SerializeField] private ChangeTurnPlayer player2Reference;
    [SerializeField] private Transform door1Reference;
    [SerializeField] private Transform door2Reference;
    private bool canTeleport = true;
    private bool whileTeleport;

    public void ChangePlayerPosition()
    {
        if (!canTeleport) return;
        player1Reference.GetComponent<CharacterController>().enabled = false;
        player1Reference.GetComponent<PlayerInput>().enabled = false;
        player2Reference.GetComponent<CharacterController>().enabled = false;
        player2Reference.GetComponent<PlayerInput>().enabled = false;

        // playerInZone = false;
        canTeleport = false;
        UIGameManager.instance.PanelChangeTime(true);
        StartCoroutine(PlayerMovement());
    }

    IEnumerator PlayerMovement()
    {
        whileTeleport = true;

        while (whileTeleport)
        {
            player1Reference.transform.position = Vector3.MoveTowards(player1Reference.transform.position, player1Reference.doorInZone.position, 5f*Time.deltaTime);
            player2Reference.transform.position = Vector3.MoveTowards(player2Reference.transform.position, player2Reference.doorInZone.position, 5f*Time.deltaTime);

            if(Vector3.Distance(player1Reference.transform.position, player1Reference.doorInZone.position) < 0.2f && Vector3.Distance(player2Reference.transform.position, player2Reference.doorInZone.position) < 0.2f)
            {
                var newPosPlayer1 = player1Reference.transform.position;
                // Pasar Anim
                player1Reference.transform.position = player2Reference.transform.position;
                player2Reference.transform.position = newPosPlayer1;
                Invoke("EnablePlayer", 0.5f);
                whileTeleport = false;
                break;
            }
            yield return null;
        }
    }

    private void EnablePlayer()
    {
        UIGameManager.instance.PanelChangeTime(false);
        var door2 = player1Reference.doorInZone;
        player1Reference.doorInZone = player2Reference.doorInZone;
        player2Reference.doorInZone = door2;
        player1Reference.GetComponent<CharacterController>().enabled = true;
        player1Reference.GetComponent<PlayerInput>().enabled = true;
        player2Reference.GetComponent<CharacterController>().enabled = true;
        player2Reference.GetComponent<PlayerInput>().enabled = true;
        canTeleport = true;
    }
}
