using UnityEngine;

public class Plunger : MonoBehaviour
{
    [SerializeField]
    private float _currentForce = 300f;
    private Rigidbody _ballRigidbody;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            _ballRigidbody = collision.rigidbody;
            Debug.Log("BallCols");
            _ballRigidbody.AddForce(0f, 0f, _currentForce);
        }
    }
}
