using System;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] int selfDirection;
    public static Action<Ball, int> OnCollisionAction { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollisionAction?.Invoke(collision.GetComponent<Ball>(), selfDirection);
        Destroy(collision.gameObject);
    }
}
