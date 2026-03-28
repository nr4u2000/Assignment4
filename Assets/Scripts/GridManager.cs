using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform gridParent;

    private List<Card> cards = new List<Card>();

    private int rows;
    private int cols;

    private int totalPairs;
    private int matchedPairs;

    private void Start()
    {
        SetGrid();
    }
    public void SetGrid()
    {
        rows = 2;
        cols = 3;
        gridParent.GetComponent<GridLayoutGroup>().constraintCount = rows;
        GenerateGrid();
    }

    public void ClearGrid()
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        cards.Clear();
    }

    void GenerateGrid()
    {
        int total = rows * cols;

        if (total % 2 != 0 || total > 24)
        {
            Debug.LogError("Invalid Grid!");
            return;
        }

        List<int> ids = new List<int>();

        for (int i = 0; i < total / 2; i++)
        {
            ids.Add(i);
            ids.Add(i);
        }

        Shuffle(ids);

        for (int i = 0; i < total; i++)
        {
            GameObject obj = Instantiate(cardPrefab, gridParent);
            Card card = obj.GetComponent<Card>();
            card.Setup(ids[i]);
            cards.Add(card);
        }

        totalPairs = total / 2;
        matchedPairs = 0;
    }

    void Shuffle(List<int> list)
    {
        int shuffleCount = Mathf.Clamp(10, 1, 5);

        for (int s = 0; s < shuffleCount; s++)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int rand = Random.Range(i, list.Count);
                (list[i], list[rand]) = (list[rand], list[i]);
            }
        }
    }

}