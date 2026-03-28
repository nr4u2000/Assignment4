using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int Id;
    public bool IsMatched { get; private set; }

    [SerializeField] private GameObject front;
    [SerializeField] private GameObject back;
    [SerializeField] private Text ValueTextfield;

    private bool isFlipped = false;
    private bool isAnimating = false;

    public void Setup(int id)
    {
        Id = id;
        ValueTextfield.text = Id.ToString();

    }

    public void OnClick()
    {
        if (isFlipped || IsMatched || isAnimating) return;

        Flip();
        GameManager.Instance.OnCardSelected(this);
    }

    public void Flip()
    {
        StartCoroutine(FlipRoutine(true));
        AudioManager.Instance.PlayFlip();
    }

    public void FlipBack()
    {
        StartCoroutine(FlipRoutine(false));
    }

    IEnumerator FlipRoutine(bool showFront)
    {
        isAnimating = true;

        float duration = 0.2f;
        float time = 0;

        while (time < duration)
        {
            float scale = Mathf.Lerp(1, 0, time / duration);
            transform.localScale = new Vector3(scale, 1, 1);
            time += Time.deltaTime;
            yield return null;
        }

        front.SetActive(showFront);
        back.SetActive(!showFront);

        time = 0;
        while (time < duration)
        {
            float scale = Mathf.Lerp(0, 1, time / duration);
            transform.localScale = new Vector3(scale, 1, 1);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = Vector3.one;

        isFlipped = showFront;
        isAnimating = false;
    }

    public void SetMatched()
    {
        IsMatched = true;
    }
}