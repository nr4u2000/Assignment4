using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentLevel = 1;
    [SerializeField] Text levelTextfield;

    private List<Card> selectedCards = new List<Card>();

    [System.Serializable]
    public struct GridSize
    {
        public int rows;
        public int cols;

        public GridSize(int r, int c)
        {
            rows = r;
            cols = c;
        }
    }

    private List<GridSize> levels = new List<GridSize>()
    {
        new GridSize(2,2),
        new GridSize(2,3),
        new GridSize(3,4),
        new GridSize(4,4),
        new GridSize(4,5),
        new GridSize(3,6),
        new GridSize(4,6) // max 24 cards
    };

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentLevel = SaveSystem.Instance.LoadLevel();
        LoadLevel();
    }

    void LoadLevel()
    {
        GridSize size = GetCurrentGrid();

        GridManager grid = FindObjectOfType<GridManager>();
        grid.ClearGrid();
        grid.SetGrid(size.rows, size.cols);
        levelTextfield.text = "Level : "+currentLevel.ToString();
    }

    GridSize GetCurrentGrid()
    {
        int index = Mathf.Clamp(currentLevel - 1, 0, levels.Count - 1);
        return levels[index];
    }

    public void OnCardSelected(Card card)
    {
        if (selectedCards.Contains(card) || card.IsMatched) return;
        if (selectedCards.Count >= 2) return;

        selectedCards.Add(card);

        if (selectedCards.Count == 2)
        {
            StartCoroutine(CheckMatch(selectedCards[0], selectedCards[1]));
        }
    }

    IEnumerator CheckMatch(Card c1, Card c2)
    {
        yield return new WaitForSeconds(GetFlipDelay());

        if (c1.Id == c2.Id)
        {
            c1.SetMatched();
            c2.SetMatched();

            ScoreManager.Instance.AddScore(10);
            AudioManager.Instance.PlayMatch();

            FindObjectOfType<GridManager>().OnPairMatched();
        }
        else
        {
            c1.FlipBack();
            c2.FlipBack();

            ScoreManager.Instance.ResetCombo();
            AudioManager.Instance.PlayMismatch();
        }

        selectedCards.Clear();

        SaveSystem.Instance.SaveGame();
    }

    float GetFlipDelay()
    {
        return Mathf.Clamp(0.7f - currentLevel * 0.05f, 0.2f, 0.7f);
    }

    public void OnGameCompleted()
    {
        AudioManager.Instance.PlayGameComplete();

        currentLevel++;
        SaveSystem.Instance.SaveLevel(currentLevel);

        StartCoroutine(NextLevelRoutine());
    }

    IEnumerator NextLevelRoutine()
    {
        yield return new WaitForSeconds(AudioManager.Instance.gameComplete.length);
        LoadLevel();
    }
}