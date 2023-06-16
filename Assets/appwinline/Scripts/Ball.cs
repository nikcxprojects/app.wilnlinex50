using UnityEngine;

public class Ball : MonoBehaviour
{
    public int Direction { get; private set; }

    private void Start()
    {
        Direction = Random.Range(0, 100) > 50 ? 1 : -1;

        var arrowPrefab = Resources.Load<Arrow>("arrow");
        var arrow = Instantiate(arrowPrefab, transform.parent);

        arrow.Target = transform;
        arrow.transform.rotation = Quaternion.Euler(0 , Direction > 0 ? 180 : 0, 0);
    }
}
