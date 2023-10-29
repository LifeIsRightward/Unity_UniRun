using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어함.
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip deathClip;
    public float jumpForce = 700f; //내가 임의로 줄임 ㅇㅇ

    int jumpCount = 0;
    bool isGrounded = false;
    bool isDead = false;

    Rigidbody2D playerRigidbody;
    Animator animator;
    AudioSource platerAudio;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        platerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }

        if(Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            jumpCount++;
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce * 0.7f));
            platerAudio.Play();
        }else if(Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.4f; // 내가 임의로 줄임
        }

        animator.SetBool("Grounded", isGrounded);
    }

    private void Die()
    {
        animator.SetTrigger("Die");

        platerAudio.clip = deathClip;
        platerAudio.Play();

        playerRigidbody.velocity = Vector2.zero;
        isDead = true;

        GameManager.Instance.OnPlayerDead();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Dead" && !isDead) { 
            Die();  
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f){
            isGrounded = true;
            jumpCount = 0;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
