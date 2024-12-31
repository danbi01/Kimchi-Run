using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Jump Force")]
    public float JumpForce;

    [Header("References")]
    public Rigidbody2D PlayerRigidBody;
    public Animator PlayerAnimator;
    public BoxCollider2D PlayerCollider;

    private bool isGrounded = true;

    private bool isInvincible = false;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //PlayerRigidBody.linearVelocityX=10;
            //PlayerRigidBody.linearVelocityY=20;

            PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            PlayerAnimator.SetInteger("State", 1);
        }
    }

    public void KillPlayer()
    {
        PlayerCollider.enabled = false;
        PlayerAnimator.enabled = false;
        PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
    }

    void Hit()
    {
        GameManager.Instance.Lives -= 1;
        if (GameManager.Instance.Lives == 0)
        {
            KillPlayer();
        }
    }

    void Heal()
    {
        GameManager.Instance.Lives = Mathf.Min(3, GameManager.Instance.Lives + 1);
    }

    void StartInvincible()
    {
        isInvincible = true;
        Invoke("StopInvincible", 5f);
    }

    void StopInvincible()
    {
        isInvincible = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Platform")
        {
            if (!isGrounded)
            {
                PlayerAnimator.SetInteger("State", 2);
            }
            isGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "enemy")
        {
            if (!isInvincible)
            {
                Destroy(collider.gameObject);
            }
            Hit();
        }
        else if (collider.gameObject.tag == "food")
        {
            Destroy(collider.gameObject);

            Heal();
        }
        else if (collider.gameObject.tag == "golden")
        {
            Destroy(collider.gameObject);
            StartInvincible();
        }
    }
}