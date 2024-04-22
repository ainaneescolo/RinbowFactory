using TMPro;
using UnityEngine;

public class Item_gamedata_list : MonoBehaviour
{
    [Tooltip("Points made in the Game")] public int pointsMade;
    public string namePlayer1;
    public string namePlayer2;
    public int positionIndex;

    void Start()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{positionIndex}- " + $"{namePlayer1}/ " + $"{namePlayer2}-> " + $"{pointsMade}";
    }
}
