using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int Score { get; private set; }
    private int combo = 0;
    [SerializeField] Text scoreTextfield;

    private void Awake()
    {
        Instance = this;
        scoreTextfield.text = "Score : " + Score.ToString();
    }

    public void AddScore(int baseScore)
    {
        combo++;
        Score += baseScore * combo;
        scoreTextfield.text = "Score : " + Score.ToString();
    }

    public void ResetCombo()
    {
        combo = 0;
    }
}