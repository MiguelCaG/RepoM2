using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private static string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/save.dat";

    public static void SaveHighScore(int highScore)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, highScore);
        stream.Close();
    }

    public static int LoadHighScore()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int highScore = (int)formatter.Deserialize(stream);
            stream.Close();

            return highScore;
        }
        else
        {
            return 0;
        }
    }
}
