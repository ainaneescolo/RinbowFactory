using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LocalRequest_GameData : MonoBehaviour
{
    public static LocalRequest_GameData instance;
    
    public GameData_List game_data_localRequest;
    [SerializeField] private GameObject content_gamedata_list;
    [SerializeField] private Item_gamedata_list itemGamedata;

    private string saveFile;
    
    private void Awake()
    {
        instance = this;

        saveFile = Application.persistentDataPath + "/playerdata.json";
    }

    private void Start()
    {
        ReadFile();
    }

    [ContextMenu("Read")]
    public void ReadFile()
    {
        // Does the file exist?
        if (File.Exists(saveFile))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);
            
            // Work with JSON
            game_data_localRequest = JsonUtility.FromJson<GameData_List>(fileContents);
        }
    }
    
    [ContextMenu("Write")]
    public void WriteFile()
    {
        File.WriteAllText(saveFile, JsonUtility.ToJson(game_data_localRequest));
    }
    
    public void Create_ScoreList(int pointsMade, string namePlayer1, string namePlayer2)
    {
        var gameDataList_TMP = new List<GameData>();
           
        GameData new_data = new GameData();
        new_data.pointsMade = pointsMade;
        new_data.namePlayer1 = namePlayer1;
        new_data.namePlayer2 = namePlayer2;

        gameDataList_TMP.AddRange(game_data_localRequest.gameDataList);
        gameDataList_TMP.Add(new_data);

        gameDataList_TMP = gameDataList_TMP.OrderByDescending(o => o.pointsMade).ToList();
        
        if (gameDataList_TMP.Count > 10)
        {
            gameDataList_TMP.RemoveRange(10, gameDataList_TMP.Count - 10);
        }
        
        game_data_localRequest.gameDataList = gameDataList_TMP.ToArray();
        WriteFile();
    }
    
    public void Refresh_Game_List()
    {
        Clean_Game_List_UI();
        ReadFile();
        var index = 1;
        
        foreach (GameData gameData in game_data_localRequest.gameDataList)
        {
            Item_gamedata_list _itemGamedata;
            _itemGamedata = Instantiate(itemGamedata, content_gamedata_list.transform);
            _itemGamedata.positionIndex = index;
            _itemGamedata.pointsMade = gameData.pointsMade;
            _itemGamedata.namePlayer1 = gameData.namePlayer1;
            _itemGamedata.namePlayer2 = gameData.namePlayer2;
            
            ++index;
        }
    }    
    
    private void Clean_Game_List_UI()
    {
        foreach (Transform child in content_gamedata_list.transform)
        {
            Destroy(child.gameObject);
        }
    }
    
    private void Clean_Game_List()
    {
        var gameDataList_TMP = new List<GameData>();
        
        game_data_localRequest.gameDataList = gameDataList_TMP.ToArray();
        WriteFile();
    }
}
