using UnityEngine;

public class TargetWobble : MonoBehaviour
{
    public float wobbleDuration = 0.5f;
    public float wobbleIntensity = 0.1f;

    private bool isWobbling = false;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void TriggerWobble()
    {
        if (!isWobbling)
            StartCoroutine(Wobble());
    }

    private System.Collections.IEnumerator Wobble()
    {
        isWobbling = true;
        float elapsed = 0f;

        while (elapsed < wobbleDuration)
        {
            float wobble = Mathf.Sin(elapsed * 20f) * wobbleIntensity;
            transform.localScale = originalScale + new Vector3(wobble, -wobble, wobble);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
        isWobbling = false;
    }
}
