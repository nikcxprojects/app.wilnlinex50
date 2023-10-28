using UnityEngine;

public class Spinner : MonoBehaviour
{
    public static void Instant()
    {
        Instantiate(Resources.Load<GameObject>("spinner"));
    }
}
