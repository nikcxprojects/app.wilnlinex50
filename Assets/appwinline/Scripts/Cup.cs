using System;
using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;

public class Cup : MonoBehaviour
{
    [SerializeField] Transform[] positions;
    [SerializeField] GameObject[] forDisable;

    private Coroutine Shaking { get; set; }
    private GameObject[] DicePrefabs { get; set; }
    private GameObject[] DiceSpawned { get; set; }

    public static Action OnStartShaking { get; set; }
    public static Action<int, int, int> OnEndShaking { get; set; }

    private void Awake()
    {
        DiceSpawned = new GameObject[positions.Length];
        DicePrefabs = Resources.LoadAll<GameObject>("dice");
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            DestroyObjects();
            if(Shaking != null)
            {
                StopCoroutine(Shaking);
            }

            Shaking = StartCoroutine(nameof(Shake));
        }
    }

    private void DestroyObjects()
    {
        foreach(GameObject g in forDisable)
        {
            Destroy(g);
        }

        foreach(GameObject g in DiceSpawned)
        {
            Destroy(g);
        }
    }

    private IEnumerator Shake()
    {
        OnStartShaking?.Invoke();

        var et = 0.0f;
        var duration = 0.2f;

        var initPosition = new Vector2(0, -3.68f);

        while(et < duration)
        {
            var x = Random.Range(-0.2f, 0.2f);
            var y = Random.Range(-0.2f, 0.2f);

            transform.position += (Vector3)new Vector2(x, y);
            yield return new WaitForSeconds(0.01f);
            transform.position = initPosition;
            
            et += 0.01f;
            yield return null;
        }

        for(int i = 0; i < DiceSpawned.Length; i++)
        {
            var _prefab = DicePrefabs[Random.Range(0, DicePrefabs.Length)];

            DiceSpawned[i] = Instantiate(_prefab, transform.parent);
            DiceSpawned[i].name = _prefab.name;

            DiceSpawned[i].transform.position = positions[i].position;
            DiceSpawned[i].transform.rotation = Quaternion.Euler(Vector3.forward * Random.Range(0.0f, 360.0f));
        }

        var dice1Value = int.Parse(DiceSpawned[0].name);
        var dice2Value = int.Parse(DiceSpawned[1].name);
        var dice3Value = int.Parse(DiceSpawned[2].name);

        var sound = Instantiate(Resources.Load<GameObject>("sound"), transform.parent);
        Destroy(sound, 0.5f);

        OnEndShaking?.Invoke(dice1Value, dice2Value, dice3Value);
    }
}
