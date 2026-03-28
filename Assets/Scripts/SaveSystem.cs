using UnityEngine;
using UnityEngine.UI;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("score", ScoreManager.Instance.Score);
    }

    public void SaveLevel(int level)
    {
        PlayerPrefs.SetInt("level", level);
    }

    public int LoadLevel()
    {
        return PlayerPrefs.GetInt("level", 1);
    }
}