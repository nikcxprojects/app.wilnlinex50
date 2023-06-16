using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform Target { get; set; }

    private void Update()
    {
        if(!Target)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = (Vector2)Target.position;
    }
}
