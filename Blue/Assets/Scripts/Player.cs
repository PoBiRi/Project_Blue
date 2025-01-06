using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Slider PlayerHpBar;

    private Rigidbody2D rb;
    private Vector2 movement;
    private float PlayerHP = 100;

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
    }
    void FixedUpdate()
    {
        // Use Rigidbody2D, Move
        rb.velocity = movement.normalized * moveSpeed;
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
