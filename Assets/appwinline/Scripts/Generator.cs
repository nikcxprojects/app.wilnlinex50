using System.Collections;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(nameof(Spawning));
    }

    private void OnDestroy()
    {
        StopCoroutine(nameof(Spawning));
    }

    private IEnumerator Spawning()
    {
        var ballPrefab = Resources.Load<GameObject>("ball");

        while (true)
        {
            var ball = Instantiate(ballPrefab, transform);
            ball.transform.localPosition = new Vector2(0.0f, 7.5f);
            ball.name = ballPrefab.name;

            yield return new WaitForSeconds(3);
        }
    }
}
