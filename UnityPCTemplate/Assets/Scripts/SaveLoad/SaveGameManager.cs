using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// SaveGameManager
/// <summary>
/// Author: Sam Meijer
/// <summary>
/// Save and Read from binary files.
/// </summary>
public class SaveGameManager : MonoBehaviour {
    private static Dictionary<string, SaveModel> modelDictionary;
    private BinaryFormatter formatter = new BinaryFormatter();
    private static SaveGameManager manager;

    public static SaveGameManager instance {
        get {
            if (!manager) {
                manager = FindObjectOfType(typeof(SaveGameManager)) as SaveGameManager;

                if (!manager)
                    Debug.LogError("There needs to be one active ModelManager script on a GameObject in your scene.");
                else
                    manager.Init();
            }
            return manager;
        }
    }

    Dictionary<string, SaveModel> CreateSaveModelDictionary() {
        Dictionary<string, SaveModel> dic = new Dictionary<string, SaveModel>();

        // Add models below to the dictionary to save / load them
        dic.Add(GameOptionModel.ID, new GameOptionModel());
        dic.Add(AudioOptionModel.ID, new AudioOptionModel());

        return dic;
    }

    void Init() {
        if (modelDictionary == null) {
            modelDictionary = CreateSaveModelDictionary();

            // If a save directory exists try to load save files and overwrite 
            if (Directory.Exists("Saves")) {
                List<string> keys = new List<string>(modelDictionary.Keys);
                foreach (string key in keys) {
                    if (File.Exists("Saves/" + key + ".binary")) {
                        try {
                            // retrieve the saved model from a binary file.
                            FileStream fileStream = File.Open("Saves/" + key + ".binary", FileMode.Open);
                            SaveModel saveModel = (SaveModel)formatter.Deserialize(fileStream);

                            // Compare versions
                            if (saveModel.GetVersion() < modelDictionary[key].GetVersion())
                                modelDictionary[key].UpgradeModel(saveModel);
                            else
                                modelDictionary[key] = saveModel;

                            fileStream.Close();
                        } catch (Exception e) {
                            Debug.LogError("Somthing went wrong trying to read save file: " + key);
                            Debug.LogError(e.StackTrace);
                        }
                    }
                }
            }
        }
    }

    public object GetModel(string ID) {
        return modelDictionary[ID];
    }

    // Saves the game data from the models in the modelDictionary
    public void SaveData() {
        if (!Directory.Exists("Saves"))
            Directory.CreateDirectory("Saves");

        //Loop the dictionary and save each model
        foreach (KeyValuePair<string, SaveModel> entry in modelDictionary) {
            string path = "Saves/" + entry.Key + ".binary";
            FileStream saveFile;
            if (!File.Exists(path)) {
                saveFile = File.Create(path);
            } else {
                saveFile = File.Open(path, FileMode.Open);
            }

            formatter.Serialize(saveFile, entry.Value);

            saveFile.Close();
        }

    }
}
