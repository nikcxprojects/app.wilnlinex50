using UnityEngine;

public class NoInet : MonoBehaviour
{
    public static void Instant()
    {
        Instantiate(Resources.Load<GameObject>("noInet"));
    }
}
