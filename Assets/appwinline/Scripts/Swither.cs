using UnityEngine;
using UnityEngine.UI;

public class Swither : MonoBehaviour
{
    [SerializeField] Image img;

    public void Switch(Sprite spriteOn, Sprite spriteOff, bool enable)
    {
        img.sprite = enable ? spriteOn : spriteOff;
        img.SetNativeSize();
    }
}
