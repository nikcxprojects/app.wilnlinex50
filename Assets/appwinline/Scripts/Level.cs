using UnityEngine;

public class Level : MonoBehaviour
{
    private void Start()
    {
        transform.position += Vector3.right * 25.0f;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, 30.0f * Time.deltaTime);
    }
}
