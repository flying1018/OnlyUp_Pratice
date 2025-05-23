using UnityEngine;

public class ScriptableObject : MonoBehaviour
{
    public float jumpForce;
    public float damage;

    private void OnTriggerEnter(Collider target)
    {
        if (this.CompareTag("Jump") && target.CompareTag("Player"))
        {
            Rigidbody _rigidbody = target.GetComponent<Rigidbody>();

            if (_rigidbody != null)
            {
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        else if (this.CompareTag("Hurt") && target.CompareTag("Player"))
        {
            CharacterManager.Instance._player.Damage(damage);
        }
    }
}