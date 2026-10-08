using TMPro;
using UnityEngine;
using LitMotion;

public class GameManager : MonoBehaviour
{
    static public System.Action initEvent;

    [SerializeField]
    private DeadZone deadZone;

    [SerializeField]
    private int remainingLife = 3;
    [SerializeField]
    private TextMeshProUGUI remainingLifeText;

    [SerializeField]
    private GameObject ball;

    [SerializeField]
    private GameObject Background;
    [SerializeField]
    private GameObject startBackground;
    [SerializeField]
    private TextMeshProUGUI ResultsText;
    [SerializeField]
    private ScoreManager scoreManager;

    private Vector3 firstPos = new Vector3(0.6f, 0.05f, -0.065f);
    private bool _isGameover = false;

    private void Start()
    {
        initEvent += Init;
        deadZone.onDead += ReSpawn;
        remainingLifeText.text = $"Life: {remainingLife.ToString()}";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Retry();
        }
    }

    private void ReSpawn()
    {
        remainingLife -= 1;

        if (remainingLife < 0)
        {
            Gameover();
        }

        if (_isGameover) return;
        remainingLifeText.text = $"Life: {remainingLife.ToString()}";


        Instantiate(ball, firstPos, Quaternion.identity);
    }

    private void Gameover()
    {
        _isGameover = true;
        Background.SetActive(true);
        int targetInt = scoreManager.Score;
        LMotion.Create(0, targetInt, 3f)
                .WithEase(Ease.Linear)
                .Bind(_ =>
                {
                    ResultsText.text = $"Score: {_}";
                });
    }

    public void Retry()
    {
        _isGameover = false;
        Background.SetActive(false);
        initEvent.Invoke();
    }

    public void OnStart()
    {
        startBackground.SetActive(false);
        initEvent.Invoke();
    }

    private void Init()
    {
        remainingLife = 3;
        remainingLifeText.text = $"Life: {remainingLife.ToString()}";
        Instantiate(ball, firstPos, Quaternion.identity);
    }
}
