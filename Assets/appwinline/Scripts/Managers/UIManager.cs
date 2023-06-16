using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance 
    { 
        get => FindObjectOfType<UIManager>(); 
    }

    private int score;
    private GameObject _gameRef;

    [Space(10)]
    [SerializeField] GameObject menu;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject top;
    [SerializeField] GameObject game;

    [Space(10)]
    [SerializeField] Text scoreText;

    private void Awake()
    {
        Cup.OnStartShaking += () =>
        {
            score = 0;
            scoreText.text = $"{score}";
        };

        Cup.OnEndShaking += (dice1Value, dice2Value, dice3Value) =>
        {
            score += (dice1Value + dice2Value + dice3Value);
            scoreText.text = $"{score}";
        };
    }

    private void Start()
    {
        OpenMenu();
    }

    public void StartGame()
    {
        score = 0;
        scoreText.text = $"{score}";

        var _parent = GameObject.Find("Environment").transform;
        var _prefab = Resources.Load<GameObject>("level");

        _gameRef = Instantiate(_prefab, _parent);

        menu.SetActive(false);
        game.SetActive(true);
    }

    public void OpenSettings()
    {
        settings.SetActive(true);
        menu.SetActive(false);
    }

    public void OpenTop()
    {
        top.SetActive(true);
        menu.SetActive(false);
    }

    public void OpenMenu()
    {
        if (_gameRef)
        {
            Destroy(_gameRef);
        }

        game.SetActive(false);
        settings.SetActive(false);
        top.SetActive(false);

        menu.SetActive(true);
    }
}
