using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class Flipper : MonoBehaviour
{
    private float _pressedPosition = 55f; // ボタン入力時の位置
    [SerializeField]
    private float _hitStrength = 10000f; // うつ強さ
    private float _flipperDamper = 150f; // 戻る力
    [SerializeField]
    private KeyCode _inputKey;
    private HingeJoint _hinge;

    void Start()
    {
        _hinge = GetComponent<HingeJoint>();
        _hinge.useSpring = true;
    }

    void Update()
    {
        JointSpring spring = new JointSpring();
        spring.spring = _hitStrength;
        spring.damper = _flipperDamper;

        if (Input.GetKey(_inputKey))
        {
            spring.targetPosition = _pressedPosition;
        }

        _hinge.spring = spring;
        _hinge.useLimits = true;
    }
}
