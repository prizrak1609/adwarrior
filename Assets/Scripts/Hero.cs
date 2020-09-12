
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Hero : MonoBehaviour
{
    public float m_MoveSpeed;
    //public Animator animator;
    public enum PlayerState { Alive, Dead }
    public PlayerState playerState = PlayerState.Alive;
    public Vector2 lookFacing;
    public Vector2 respawnPoint;
    public bool dead = false;

    AudioSource audioSource;
    float dashCooldown = 0f;
    Rigidbody2D rb;
    SpriteRenderer heroRenderer;
    enum MoveState { Right, Left }
    MoveState previousMoveState = MoveState.Right;
    MoveState currentMoveState = MoveState.Right;

    Dictionary<MoveState, string> moveStateStrings = new Dictionary<MoveState, string>();

    void Start()
    {
        moveStateStrings.Add(MoveState.Right, "right");
        moveStateStrings.Add(MoveState.Left, "left");

        rb = GetComponent<Rigidbody2D>();
        heroRenderer = GetComponent<SpriteRenderer>();
        //animator.SetBool("alive", true);
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (playerState == PlayerState.Dead)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector3 tryMove = Vector3.zero;

        previousMoveState = currentMoveState;

        if (InputLeft())
        {
            currentMoveState = MoveState.Left;
            tryMove += Vector3Int.left;
        }
        else if (InputRight())
        {
            currentMoveState = MoveState.Right;
            tryMove += Vector3Int.right;
        }

        if (previousMoveState != currentMoveState)
        {
            heroRenderer.flipX = !heroRenderer.flipX;
        }

        rb.velocity = Vector3.ClampMagnitude(tryMove, 1f) * m_MoveSpeed;
        //animator.SetBool("moving", tryMove.magnitude > 0);
        //if (Mathf.Abs(tryMove.x) > 0) {
        //    animator.transform.localScale = tryMove.x < 0f ? new Vector3(-1f, 1f, 1f) : new Vector3(1f, 1f, 1f);
        //}
        if (tryMove.magnitude > 0f)
        {
            lookFacing = tryMove;
        }

        dashCooldown = Mathf.MoveTowards(dashCooldown, 0f, Time.deltaTime);

        if (InputJump())
        {
        }

        //animator.SetBool("dash_ready", dashCooldown <= 0f);

    }

    bool InputLeft()
    {
        return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
    }

    bool InputRight()
    {
        return Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
    }

    bool InputJump()
    {
        return Input.GetKey(KeyCode.Space);
    }

    public void LevelComplete()
    {
        Destroy(gameObject);
    }
}
