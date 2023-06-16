using UnityEngine;

public class Player : MonoBehaviour
{
    public static int direction;
    private const float force = 3.0f;

    private Rigidbody2D Rigidbody { get; set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(direction == 0)
        {
            Rigidbody.angularVelocity = 0;
        }
    }

    private void FixedUpdate()
    {
        Rigidbody.AddTorque(direction * force);
    }
}
