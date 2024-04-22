using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinsManager : MonoBehaviour
{
    [SerializeField] private Image imageUI;

    [Tooltip("List of All player prefabs to later select skins")]
    [SerializeField] private List<PlayerSkin> playersList;
    private int _playerListIndex;
    private GameObject _playerPrefab;

    public GameObject PlayerPrefab => _playerPrefab;

    private void Start()
    {
        imageUI.sprite = playersList[_playerListIndex].playerUnlockedIcon;
        _playerPrefab = playersList[_playerListIndex].playerPrefab;
    }

    public void NextSkinOption()
    {
        _playerListIndex = _playerListIndex + 1 > playersList.Count - 1 
            ? _playerListIndex = 0 : ++_playerListIndex;

        RefreshPlayer();
    }
    
    public void BackSkinOption()
    {
        _playerListIndex = _playerListIndex - 1 < 0
            ? playersList.Count - 1 : --_playerListIndex;

        RefreshPlayer();
    }

    private void RefreshPlayer()
    {
        // Change of sprite depending on if player has enough stars to unlock, and refresh selected prefab
        if (GameManager.instance.playerStars >= playersList[_playerListIndex].starsToUnlock)
        {
            imageUI.sprite = playersList[_playerListIndex].playerUnlockedIcon;
            _playerPrefab = playersList[_playerListIndex].playerPrefab;
        }
        else
        {
            imageUI.sprite = playersList[_playerListIndex].playerLockedIcon;
            _playerPrefab = null;
        }
    }
}
