using System;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField]
    private int _additionalScore = 10; // 当たったときの追加得点
    private float _bounceForce = 1f;

    // 色情報
    private SlotManager.BumperColor _myColor;

    private void Start()
    {
        // Tagで自身の色情報を取得、保存する
        if (this.gameObject.CompareTag("Red"))
        {
            _myColor = SlotManager.BumperColor.red;
        }
        else if (this.gameObject.CompareTag("Green"))
        {
            _myColor = SlotManager.BumperColor.green;
        }
        else if (this.gameObject.CompareTag("Blue"))
        {
            _myColor = SlotManager.BumperColor.blue;
        }
    }

    public Action<SlotManager.BumperColor> OnHitBall;

    private void OnCollisionEnter(Collision collision)
    {
        // Ballが当たったときのみ処理
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRb = collision.rigidbody;

            if (ballRb != null)
            {
                // 跳ね返す方向を計算
                Vector3 bounceDir = (collision.transform.position - transform.position).normalized;
                ballRb.AddForce(bounceDir * _bounceForce, ForceMode.Impulse);

                AddScore(_additionalScore);
                OnHitBall.Invoke(_myColor); // 自身の色に応じたイベントを発火
            }
        }
    }

    private void AddScore(int score)
    {
        ScoreManager.addScore.Invoke(score);
    }


}
