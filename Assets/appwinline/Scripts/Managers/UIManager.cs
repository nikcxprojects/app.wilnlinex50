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
    [SerializeField] GameObject pause;
    [SerializeField] GameObject result;

    [Space(10)]
    [SerializeField] Text scoreText;
    [SerializeField] Text finalScoreText;

    private void Awake()
    {
        Box.OnCollisionAction += (ball, boxDirection) =>
        {
            var trueDirection = ball.Direction == boxDirection;
            if(trueDirection)
            {
                score++;

                scoreText.text = $"{score}";
                finalScoreText.text = $"<size=50><color=#17263D>SCORE</color></size>\r\n<color=#D42A28>{score}</color>";
            }
            else
            {
                GameOver();
            }
        };
    }


    private void Start()
    {
        OpenMenu();
    }

    public void StartGame()
    {
        Time.timeScale = 1;

        score = 0;

        scoreText.text = $"{score}";
        finalScoreText.text = $"<size=50><color=#17263D>SCORE</color></size>\r\n<color=#D42A28>{score}</color>";

        var _parent = GameObject.Find("Environment").transform;
        var _prefab = Resources.Load<GameObject>("level");

        _gameRef = Instantiate(_prefab, _parent);

        menu.SetActive(false);
        result.SetActive(false);
        
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

    public void SetPause(bool IsPause)
    {
        Time.timeScale = IsPause ? 0 : 1;
        pause.SetActive(IsPause);
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
        pause.SetActive(false);
        result.SetActive(false);

        menu.SetActive(true);
    }

    public void GameOver()
    {
        if (_gameRef)
        {
            Destroy(_gameRef);
        }

        game.SetActive(false);
        result.SetActive(true);
    }
}
