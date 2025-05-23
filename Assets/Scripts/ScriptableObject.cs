using UnityEngine;

public class ScriptableObject : MonoBehaviour
{
    public float jumpForce;
    [SerializeField] private string name = "none";

    private void OnTriggerEnter(Collider target)
    {
        if (target.CompareTag("Player"))
        {
            Rigidbody _rigidbody = target.GetComponent<Rigidbody>();

            if (_rigidbody != null)
            {
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}