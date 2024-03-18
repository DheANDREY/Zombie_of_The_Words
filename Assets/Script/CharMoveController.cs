using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMoveController : MonoBehaviour
{
    [Header("Movement")]
    public float moveAccel;
    public float maxSpeed;

    [Header("Jump")]
    //public float jumpAccel;

    [Header("Ground Raycast")]
    public float groundRaycastDistance;
    public LayerMask groundLayerMask;

    [Header("Scoring")]
    //public ScoreController score;
    public float scoringRatio;

    [Header("GameOver")]
    //public GameObject gameOverScreen;
    //public float fallPositionY;

    [Header("Camera")]
    public CameraMoveController gameCamera;

    private Rigidbody rig;
    //private Animator anim;
   //private CharSoundController sound;

    //private bool isJumping;
    private bool IsOnGround;

    private float lastPositionX;

    public static CharMoveController instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();
        //sound = GetComponent<CharSoundController>();

        lastPositionX = transform.position.x;
    }

    private void Update()
    {
        // read input
        //if (Input.GetKey(KeyCode.W))
        //{
        //    if (IsOnGround)
        //    {
        //        isJumping = true;

        //        //sound.PlayJump();
        //    }
        //}

        // change animation
        //anim.SetBool("IsOnGround", IsOnGround);

        // calculate score
        int distancePassed = Mathf.FloorToInt(transform.position.x - lastPositionX);
        int scoreIncrement = Mathf.FloorToInt(distancePassed / scoringRatio);

        if (scoreIncrement > 0)
        {
            //score.IncreaseCurrentScore(scoreIncrement);
            lastPositionX += distancePassed;
        }

        // game over
        //if (transform.position.y < fallPositionY)
        //{
        //    GameOver();
        //}
    }

    public bool isDamaged;
    private void FixedUpdate()
    {
        // raycast ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRaycastDistance, groundLayerMask);
        if (hit)
        {
            if (!IsOnGround && rig.velocity.y <= 0)
            {
                IsOnGround = true;
            }
        }
        else
        {
            IsOnGround = false;
        }

        // calculate velocity vector
        Vector2 velocityVector = rig.velocity;

        //if (isJumping)
        //{
        //    velocityVector.y += jumpAccel;
        //    isJumping = false;
        //}

        velocityVector.x = Mathf.Clamp(velocityVector.x + moveAccel * Time.deltaTime, 0.0f, maxSpeed);
        if (!isDamaged)
        {
            rig.velocity = velocityVector;
            //Debug.Log("Normal= "+velocityVector);
        }
        else
        {
            rig.velocity = new Vector2(0,0f);
            //Debug.Log("Damaged= " + velocityVector);
        }
    }

    private void GameOver()
    {
        // set high score
        //score.FinishScoring();

        // stop camera movement
        gameCamera.enabled = false;

        // show gameover
        //gameOverScreen.SetActive(true);

        // disable this too
        this.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + (Vector3.down * groundRaycastDistance), Color.white);
    }
}
