using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;

public class SlotManager : MonoBehaviour
{
    [SerializeField]
    private List<Bumper> _bumperArr = new List<Bumper>();

    private bool _isHitRed = false;
    private bool _isHitGreen = false;
    private bool _isHitBlue = false;

    public enum BumperColor
    {
        red = 0,
        green,
        blue
    }

    private void Start()
    {
        GameManager.initEvent += Init;

        foreach (Bumper bumper in _bumperArr)
        {
            bumper.OnHitBall += OnHit;
        }
    }

    private void OnHit(BumperColor bumperColor)
    {
        if (bumperColor == BumperColor.red && _isHitRed == false)
        {
            _isHitRed = true;
            slotNum1 = UnityEngine.Random.Range(2, 9);
            StartSlot(slotNum1, _slotText1);

        }
        else if (bumperColor == BumperColor.green && _isHitGreen == false)
        {
            _isHitGreen = true;
            slotNum2 = UnityEngine.Random.Range(2, 9);
            StartSlot(slotNum2, _slotText2);

        }
        else if (bumperColor == BumperColor.blue && _isHitBlue == false)
        {
            _isHitBlue = true;
            slotNum3 = UnityEngine.Random.Range(2, 9);
            StartSlot(slotNum3, _slotText3);
        }
    }

    [SerializeField]
    private TextMeshProUGUI _slotText1;
    [SerializeField]
    private TextMeshProUGUI _slotText2;
    [SerializeField]
    private TextMeshProUGUI _slotText3;
    [SerializeField]
    private GenParticle _genParticle;

    private int slotNum1 = 0;
    private int slotNum2 = 0;
    private int slotNum3 = 0;

    public void StartSlot(int num, TextMeshProUGUI text)
    {
        StartCoroutine(SlotCor(num, text));
    }

    private IEnumerator SlotCor(int slotNum, TextMeshProUGUI slotText)
    {

        float spinTime = 1.1f;
        float timer = 0f;

        bool isStop = false;

        while (timer < spinTime)
        {
            timer += Time.deltaTime;

            if (isStop == false)
                slotText.text = UnityEngine.Random.Range(1, 10).ToString();

            if (timer > 1f && isStop == false)
            {
                slotText.text = slotNum.ToString();
                isStop = true;
            }

            yield return null;
        }

        // 結果判定
        if (slotNum1 == slotNum2 && slotNum1 == slotNum3)
        {
            _genParticle.PlayFireWork();

            Debug.Log($"{slotNum1 * 100}");
            ScoreManager.addScore(slotNum1 * 100);
        }
        Init();
    }


    private void Init()
    {
        _isHitRed = false;
        _isHitGreen = false;
        _isHitBlue = false;

    }
}
