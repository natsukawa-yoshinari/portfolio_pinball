using TMPro;
using UnityEngine;
using UniRx;
using LitMotion;

public class ScoreManager : MonoBehaviour
{

    static public System.Action<int> addScore;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    public int Score => _score.Value;


    private void Start()
    {
        // Eventの追加
        addScore += AddScore;
        GameManager.initEvent += Init;

        // 初期表示
        scoreText.text = $"Score: {_score.Value.ToString("00000")}";

        _score.Subscribe(_ =>
        {
            scoreText.text = $"Score: {_score.Value.ToString("00000")}";
        }).AddTo(this);
    }

    private void Init()
    {
        _score.Value = 0;
        scoreText.text = $"Score: {_score.Value.ToString("00000")}";
    }


    /// <summary>
    /// バンパーに衝突することでAddScoreが発火される。
    /// スコア加算前の_displayedScoreをStartScore、加算後をendScoreとし、
    /// Tweenライブラリ"LitMotion"を用いて数字にイージングをかけている。
    /// 
    /// Startで_scoreを購読することで、効果音を鳴らすといったこともできる。
    /// 
    /// 改善としてはLinqとして_score.pairwiseを使うことで、
    /// AddScore外でイージングをかけることができた。
    /// </summary>
    private ReactiveProperty<int> _score = new ReactiveProperty<int>(0);
    private int _displayedScore = 0; // 表示しているScore
    public void AddScore(int addValue)
    {
        int startScore = _displayedScore;
        _score.Value += addValue; // Scoreに加算
        int endScore = _score.Value;

        // start から end に0.2秒のイージング
        LMotion.Create(startScore, endScore, 0.2f)
            .WithEase(Ease.OutCubic)
            .Bind(value =>
            {
                _displayedScore = Mathf.RoundToInt(value);
                scoreText.text = $"Score: {_displayedScore.ToString("00000")}";
            });
    }
}
