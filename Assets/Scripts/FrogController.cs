using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    [SerializeField]
    private float time = 30.0f;

    [SerializeField]
    private GameOverlayUI gameOverlayUI;

    private Transform frogTransform;
    private Rigidbody2D rigidBody2D;
    private Animator animator;

    public static int currentLevel = 0;

    private bool lockMoving;
    private bool dying;
    private bool levelEnded = false;

    private int lives = 3;
    private int levelPoints = 0;
    private float progress;
    private int score = 0;
    private float currentTime;

    private int moveHash;
    private int dieHash;

    private Vector2 startingPosition;

    private RaycastHit2D[] collisions = new RaycastHit2D[1];
    private ContactFilter2D walkableFilter = new ContactFilter2D();

    //Called at the end of move animation as event
    private void EndMove()
    {
        lockMoving = false;
        LaneProgress();
        ProcessGround();
    }

    //Adds points for crossing new highest lane.
    private void LaneProgress()
    {
        if (rigidBody2D.position.y > progress + 0.5f)
        {
            AddScore(10);
            progress = rigidBody2D.position.y;
        }
    }

    //Checks what's under the frog after moving.
    private void ProcessGround()
    {
        int hitCount = rigidBody2D.Cast(Vector2.zero, walkableFilter, collisions);
        if (hitCount == 0)
        {
            rigidBody2D.velocity = Vector2.zero;
            Die();
            return;
        }
        else
        {
            Rigidbody2D collisionRigidbody = collisions[0].rigidbody;
            if (collisionRigidbody != null)
            {
                rigidBody2D.velocity = collisionRigidbody.velocity;
            }

            if (collisions[0].collider.CompareTag("Finish"))
            {
                FinishReached();
            }
        }
    }

    private void FinishReached()
    {
        AddScore((int)(1000 * currentTime / time));
        levelPoints++;
        if (levelPoints == 3)
        {
            WinLevel();
            return;
        }

        Respawn();
    }

    public void Move(Vector2 direction)
    {
        rigidBody2D.velocity = Vector2.zero;
        rigidBody2D.SetRotation(Vector2.SignedAngle(Vector2.down, direction));
        rigidBody2D.MovePosition(rigidBody2D.position + direction);
        animator.Play(moveHash);
        lockMoving = true;
    }

    private void Die()
    {
        animator.Play(dieHash);

        lives--;
        gameOverlayUI.RemoveLife(lives);
        lockMoving = true;
        rigidBody2D.SetRotation(0.0f);
        rigidBody2D.velocity = Vector2.zero;
        dying = true;
    }

    private void Respawn()
    {
        if (lives <= 0)
        {
            levelEnded = true;
            gameOverlayUI.ShowSummary(false);
        }
        else
        {
            frogTransform.position = startingPosition;
            rigidBody2D.SetRotation(180.0f);
            lockMoving = false;
            progress = startingPosition.y + 0.5f;
            currentTime = time;
            dying = false;
        }
    }

    private void AddScore(int value)
    {
        score += value;
        gameOverlayUI.SetScore(score);
    }

    public void WinLevel()
    {
        gameOverlayUI.ShowSummary(true);
        levelEnded = true;
    }

    //Since there is only one layer (Obstacle) colliding with Frog nothing needs to be checked.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Die();
        lockMoving = true;
    }

    //Input only for testing. Should be changed for bigger scale projects, other platforms and other uses.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (levelEnded)
        {
            return;
        }

        if (currentTime < 0.0f)
        {
            if (!dying)
            {
                Die();
            }
            return;
        }
        currentTime -= Time.deltaTime;
        gameOverlayUI.SetTime(currentTime / time);

        if (lockMoving)
        {
            return;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Move(Vector2.up);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Move(Vector2.down);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(Vector2.right);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(Vector2.left);
        }
    }

    //Get reference in editor to avoid using FindObjectOfType in game.
    private void OnValidate()
    {
        if (gameOverlayUI == null)
        {
            gameOverlayUI = FindObjectOfType<GameOverlayUI>();
        }
    }

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);

        frogTransform = transform;
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        moveHash = Animator.StringToHash("Move");
        dieHash = Animator.StringToHash("Die");

        walkableFilter.layerMask = 1 << LayerMask.NameToLayer("Walkable");
        startingPosition = rigidBody2D.position;
        currentTime = time;
        progress = startingPosition.y + 0.5f;
        score = 0;
        lives = 3;
    }
}