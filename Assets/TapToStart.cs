using UnityEngine;

public class TapToStart : MonoBehaviour
{
    private bool isTap;
    [SerializeField] GameObject loadingGo;

    private void Update()
    {
        if(!isTap && Input.GetMouseButtonDown(0))
        {
            isTap = true;
            loadingGo.SetActive(true);
        }
    }
}
