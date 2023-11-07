using UnityEngine;

public class TapToStart : MonoBehaviour
{
    public static bool isPressing;
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isPressing = true;
            Destroy(gameObject);
        }
    }
}
