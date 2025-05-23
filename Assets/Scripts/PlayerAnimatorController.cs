using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Rigidbody _rigidbody;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float speed = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z).magnitude;
        animator.SetFloat("Speed", speed); // 이속 감지해서 애니메이션 변경

        bool isFalling = _rigidbody.velocity.y < -0.6f;
        animator.SetBool("FreeFall", isFalling);

        if (Input.GetKeyDown(KeyCode.Space)) { animator.SetBool("Jump", true); }
        else if (_rigidbody.velocity.y <= 0) { animator.SetBool("Jump", false); }
    }

    public void OnLand()
    {

    }
}