using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            PlayerAnimator.SetInteger("state", 1);
        }

    }

    public void KillPlayer()
    {
        PlayerCollider.enabled = false;
        PlayerAnimator.enabled = false;
        PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Platform")
        {
            if (!isGrounded)
            {
                PlayerAnimator.SetInteger("state", 2);
            }
            isGrounded = true;
        }
    }
    void Hit()
    {
        GameManager.Instance.Lives -= 1;
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
        isInvincible = false;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            if (!isInvincible)
                Destroy(collider.gameObject);
            Hit();

        }
        else if (collider.gameObject.tag == "Food")
        {
            Destroy(collider.gameObject);
            Heal();

        }
        else if (collider.gameObject.tag == "Golden")
        {
            Destroy(collider.gameObject);
            StartInvincible();
        }
    }
}