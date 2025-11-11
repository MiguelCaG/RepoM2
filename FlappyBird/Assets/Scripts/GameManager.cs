
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State { Ready, Playing, GameOver }

    public State actualState = State.Ready;

    private string path;

    public static GameManager instance = null;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        path = Application.persistentDataPath + "/save.dat";
        Debug.Log("Ruta de guardado: " + path);
    }

    public void SaveHighScore(int highScore)
    {
        PlayerData playerData = new PlayerData();
        playerData.highScore = highScore;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, playerData);
        stream.Close();

        Debug.Log($"Highscore guardado correctamente: {highScore}");
    }

    public int LoadHighScore()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData playerData = (PlayerData)formatter.Deserialize(stream);
            stream.Close();
            Debug.Log($"Highscore cargado: {playerData.highScore}");
            return playerData.highScore;
        }
        else
        {
            Debug.Log("No hay datos");
            return 0;
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public int highScore;
    }
}
