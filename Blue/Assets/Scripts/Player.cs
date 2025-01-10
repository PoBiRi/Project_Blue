using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 7f; // move speed
    public float dashSpeed = 20f; // dash speed
    public float dashDuration = 0.1f; // dash duration
    public float damageDuration = 1f; // damage duration
    public float dashCooldown = 1f; // dash cooltime

    public Slider PlayerHpBar;

    private Rigidbody2D rb;
    private Vector2 movement;
    private float PlayerHP = 100;
    private bool isDashing = false; // dashing
    private bool isRaging = false; // damaging
    private float dashCooldownTimer = 0f; // dash cooltimer
    private float ragingDuration = 0.5f; 

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
        if (!isDashing && !isRaging) // moving while not danshing and not damaging
        {
            rb.velocity = movement.normalized * moveSpeed;
        }
    }
    
    private IEnumerator Dash()
    {
        isDashing = true; // dashing
        gameObject.tag = "Dashing"; 
        Vector2 dashDirection = movement.normalized; // dash to move direction

        rb.velocity = dashDirection * dashSpeed; // dash speed cal

        yield return new WaitForSeconds(dashDuration); // wait for dash duration

        isDashing = false; // dashing off
        gameObject.tag = "Player";
        rb.velocity = Vector2.zero; // dash over
        dashCooldownTimer = dashCooldown; // cooltime check
    }

    private IEnumerator Damaging()
    {
        gameObject.tag = "Dashing";
        moveSpeed = 3f;

        yield return new WaitForSeconds(damageDuration); // 일정 시간 대기

        moveSpeed = 7f;
        gameObject.tag = "Player";
    }

    private IEnumerator Raging()
    {
        isRaging = true;
        gameObject.tag = "Dashing";

        yield return new WaitForSeconds(ragingDuration); // 일정 시간 대기

        gameObject.tag = "Player";
        isRaging = false;
    }

    public void ragingPush()
    {
        // 캐릭터를 총알에서 멀어지는 방향으로 밀어냄
        Vector2 pushDirection = (Vector2)rb.position;
        rb.velocity = Vector2.zero;  // 기존 속도 초기화
        rb.AddForce(pushDirection.normalized * 1000f, ForceMode2D.Force);

        StartCoroutine(Raging());
    }

    private void OnTriggerEnter2D(Collider2D other) // if on Platform rotate
    {
        if (other.CompareTag("Platform"))
        {
            transform.SetParent(other.transform);
            Debug.Log("OnPlatformon");
        }
    }

    private void OnTriggerStay2D(Collider2D other) // if on Platform rotate
    {
        if (other.CompareTag("Platform"))
        {
            transform.SetParent(other.transform);
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
        StartCoroutine(Damaging());
        PlayerHP -= dmg;
        if (PlayerHP <= 0)
        {
            PlayerHP = 0;
            Time.timeScale = 0f;
            GameObject.Find("EventSystem").GetComponent<MenuManage>().isWin = false;
            GameObject.Find("EventSystem").GetComponent<MenuManage>().isGameOver = true;
            Debug.Log("PlayerDead");
        }
        PlayerHpBar.value = PlayerHP / 100;
    }

    public void recoverHP(int tmp) //recoverHP
    {
        PlayerHP += tmp;
        if (PlayerHP > 100) PlayerHP = 100;
        PlayerHpBar.value = PlayerHP / 100;
    }

    public void respawn() //respawn Player
    {
        recoverHP(100);
        transform.position = new Vector3(0, -8, 0);
        gameObject.tag = "Player";
        isDashing = false; // dashing off
    }
}
