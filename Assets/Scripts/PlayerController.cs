using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    public float speed = 1f;
    public float jumpSpeed = 5f;
    // Start is called before the first frame update
    bool isGrounded = false;
    public Transform groundCheck;
    public float groundCheckRudias = 1f;
    public LayerMask whatIsGround;

    public int defaultJumpCount = 3;

    int jumpCount;


    Animator animator;

    void resetJumpCount()
    {
        jumpCount = defaultJumpCount;
    }
    public enum Face { Left, Right }
    public Face face = Face.Right;
    SpriteRenderer spriteRenderer;
    public GameObject particleSystemPrefabs;

    private Cinemachine.CinemachineCollisionImpulseSource MyInpulse;
    private bool canMove = true;
    public bool isDeath = false;

    public GameManager gameManager;

    public AudioClip deathAudio;
    public AudioClip jumpAudio;
    public AudioClip rewardsAudio;
    public AudioSource audioPlayer;
    void Start()
    {
        resetJumpCount();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        MyInpulse = GetComponent<Cinemachine.CinemachineCollisionImpulseSource>();
        // audioPlayer = GetComponent<AudioSource>();

        gameManager = FindObjectOfType<GameManager>();

    }
    private void OnDestroy()
    {

    }
    // Update is called once per frame
    void Update()
    {

        if (isDeath) return;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRudias, whatIsGround);
        checkJump();
        checkMove();
        updateFace();
        animator.SetBool("onGrounded", isGrounded);

        checkFallingToDeath();

    }

    private void checkFallingToDeath()
    {
        if (transform.position.y < -20)
        {
            gameManager.DeathEvent?.Invoke();
        }
    }

    private void updateFace()
    {

        if (face == Face.Left)
        {
            spriteRenderer.flipX = true;
        }
        else if (face == Face.Right)
        {
            spriteRenderer.flipX = false;
        }
    }

    void checkJump()
    {
        if (isGrounded) resetJumpCount();
        if (Input.GetButtonDown(nameof(InputButton.Jump)) && jumpCount > 0)
        {
            // 如果是从平台掉落，减少一次起跳，在空中只能跳两次
            if (jumpCount == defaultJumpCount && !isGrounded) jumpCount--;
            // reduce jump count
            jumpCount--;
            //jump
            body.velocity = (Vector2.up * jumpSpeed);
            animator.SetTrigger("jump");

            if (particleSystemPrefabs != null)
            {
                var _particle = Instantiate(particleSystemPrefabs, transform);
                _particle.transform.SetParent(null);
                Destroy(_particle, 1f);
            }
            audioPlayer.PlayOneShot(jumpAudio);
        }

    }
    void checkMove()
    {
        if (Input.GetButtonDown(nameof(InputButton.Horizontal)))
        {
            canMove = true;
        }
        if (!canMove) return;

        var horizontalAxis = Input.GetAxis(nameof(InputButton.Horizontal));
        if (horizontalAxis > 0)
        {
            face = Face.Right;
            move(horizontalAxis);
        }
        else if (horizontalAxis < 0)
        {
            face = Face.Left;
            move(horizontalAxis);
        }
        else
        {
            animator.SetFloat("speed", 0);
        }

        void move(float horizontalAxis)
        {
            body.velocity = new Vector2(horizontalAxis * speed, body.velocity.y);
            animator.SetFloat("speed", Mathf.Abs(horizontalAxis));
        }



    }

    private void FixedUpdate()
    {

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(nameof(GameManager.Tag.Enemy)))
        {
            canMove = false;
            // 敌人攻击
            var x = -1;
            if (face == Face.Left)
            {
                x = 1;
            }
            else if (face == Face.Right)
            {
                x = -1;
            }
            transform.Translate(new Vector3(x, 0.05f, 0), Space.World);
            animator.SetFloat("speed", 0);
            MyInpulse.GenerateImpulse();

            audioPlayer.PlayOneShot(jumpAudio);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //for now
        if (other.gameObject.CompareTag(nameof(GameManager.Tag.rewards)))
        {
            audioPlayer.PlayOneShot(rewardsAudio);
        }
    }

    public void death()
    {
        GetComponent<Collider2D>().enabled = false;
        audioPlayer.PlayOneShot(deathAudio);
        isDeath = true;
        StartCoroutine(frezon());
    }

    private IEnumerator frezon()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
