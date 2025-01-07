using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // move speed
    public float dashSpeed = 20f; // dash speed
    public float dashDuration = 0.1f; // dash duration
    public float dashCooldown = 1f; // dash cooltime

    public Slider PlayerHpBar;

    private Rigidbody2D rb;
    private Vector2 movement;
    private float PlayerHP = 100;
    private bool isDashing = false; // dashing
    private float dashCooldownTimer = 0f; // dash cooltimer

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        // Shift to dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0 && movement != Vector2.zero)
        {
            StartCoroutine(Dash());
        }
    }
    void FixedUpdate()
    {
        // Use Rigidbody2D, Move
        if (!isDashing) // moving while not dancing
        {
            rb.velocity = movement.normalized * moveSpeed;
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true; // dashing
        Vector2 dashDirection = movement.normalized; // dash to move direction

        rb.velocity = dashDirection * dashSpeed; // dash speed cal

        yield return new WaitForSeconds(dashDuration); // wait for dash duration

        isDashing = false; // dashing off
        rb.velocity = Vector2.zero; // dash over
        dashCooldownTimer = dashCooldown; // cooltime check
    }

    private void OnTriggerEnter2D(Collider2D other) // if on Platform rotate
    {
        if (other.CompareTag("Platform"))
        {
            transform.SetParent(other.transform);
            Debug.Log("OnPlatformon");
        }
    }

    private void OnTriggerExit2D(Collider2D other) // if out Platform stop
    {
        if (other.CompareTag("Platform"))
        {
            transform.SetParent(null);
            Debug.Log("OnPlatformoff");
        }
    }

    public void getDamage(int dmg) //getDamage to Player
    {
        PlayerHP -= dmg;
        PlayerHpBar.value = PlayerHP / 100;
    }
}
