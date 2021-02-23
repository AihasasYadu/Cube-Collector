using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Player
{
    public int score;
}

public class JSonManager : MonoSingletonGeneric<JSonManager>
{
    public TextAsset txtJson;
    private Player highScoreList = new Player();
    public Player GetSaveData { get {
                                            highScoreList = JsonUtility.FromJson<Player>(txtJson.text);
                                            return highScoreList;
                                        } }
    public Player SetSaveData { set { 
                                            highScoreList = value;
                                            SaveData();
                                        } }

    private void SaveData()
    {
        string dataToSave = JsonUtility.ToJson(highScoreList);
        File.WriteAllText("D:/Unity Projects (2020)/Cubes Collector/Assets/JSONs/HighScores.json", dataToSave);
    }
}
