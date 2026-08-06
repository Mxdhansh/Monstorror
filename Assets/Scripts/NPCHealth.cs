using System.Collections;
using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    private bool isDead = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("Player"))
        {
            isDead = true;

            GameManager.Instance.AddScore();

            StartCoroutine(ShrinkAndDestroy());
        }
    }

    IEnumerator ShrinkAndDestroy()
    {
        Vector3 originalScale = transform.localScale;

        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}