using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    //player state machine
    walk,
    interact,
    idle,
    stagger
}
public class player : MonoBehaviour
{
    public FloatValue PlayerHealth;
    public float currentHealth;
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D playerRigidBody;
    private Vector3 move;
    public Animator animator;
    public SignalSender Playerhealthchange;
    public float maxPlayerHealth;
    public bool goldenSword;
    public GameObject projectile;
    public string fountainColor;
    public int coins;
    public GameObject crown;
    public GameObject deathEffect;
    public GameObject spawnEffect;
    public AudioSource audioSource;
    public AudioClip hitSound;

    void Start()
    {
        //get data 
        coins = PlayerPrefs.GetInt("coins");
        GameObject spawneffect = Instantiate(spawnEffect, transform.position, Quaternion.identity);
        spawneffect.SetActive(true);
        fountainColor = PlayerPrefs.GetString("fountainColor");
        Scene c_scene = SceneManager.GetActiveScene();
        string sceneName = c_scene.name;
        if (sceneName == "Lobby")
        {
            //reset fountain color after run is finished
            PlayerPrefs.SetString("fountainColor", "");
            PlayerPrefs.SetInt("GoldenSword", 0);
        }
        currentHealth = PlayerHealth.intialValue;
        currentState = PlayerState.walk;
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (playerRigidBody.velocity.magnitude > 0)
        {
            if (currentState == PlayerState.walk)
            {
                playerRigidBody.velocity = Vector2.zero;
            }
        }

    }
    void Update()
    {
        if (PlayerPrefs.GetInt("GoldenSword") == 1)
        {
            goldenSword = true;
        }
        else
        {
            goldenSword = false;
        }
        //check is player is stagerred before allowing any action
        if (currentState != PlayerState.stagger)
        {
            if (Input.GetKeyDown(KeyCode.Return) && goldenSword == true)
            {
                Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
                GoldenSword golden = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<GoldenSword>();
                golden.Setup(temp, ChooseSwordDirection());
                goldenSword = false;
                PlayerPrefs.SetInt("GoldenSword", 0);


            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                Scene cScene = SceneManager.GetActiveScene();
                string sceneName = cScene.name;
                if (sceneName != "Lobby") 
                {
                    SceneManager.LoadScene("Lobby");
                }
            }
        }
        move = Vector3.zero;
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    void UpdateAnimationAndMove()
    {
        if (move != Vector3.zero)
        {
            CharacterMovement();
            animator.SetFloat("moveX", move.x);
            animator.SetFloat("moveY", move.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);

        }
    }

    void CharacterMovement()
    {
        move.Normalize();
        playerRigidBody.MovePosition(
           transform.position + move * speed * Time.deltaTime
           );
    }

    public void Knock(float knocktime, float damage)
    {
        currentHealth -= damage;
        if (currentHealth > 0)
        {
            StartCoroutine(KnockCo(knocktime));
            audioSource.PlayOneShot(hitSound, 0.2f);
            //player state is set to staggr in the knockback script
            Playerhealthchange.Raise();
        }
        else{
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            effect.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockCo(float knockbackTime)
    {
        if (playerRigidBody != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            playerRigidBody.velocity = Vector2.zero;
            //reset the playerstate from staggerred to idle
            currentState = PlayerState.idle;


        }
    }
    public void Refresh()
    {
        currentHealth = PlayerHealth.intialValue;
    }
    Vector3 ChooseSwordDirection()
    {
        //fix the sword orientation using the values taken from the animator
        float temp = Mathf.Atan2(-animator.GetFloat("moveX"), animator.GetFloat("moveY")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Golden"))
        {
            goldenSword = true;
            PlayerPrefs.SetInt("GoldenSword", 1);
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyFlock"))
        {
            currentHealth -= 10;
            if(currentHealth <= 0)
            {
                //death effect
                GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
                effect.SetActive(true);
                this.gameObject.SetActive(false);
               
            }
        }
    }
    public void Crown()
     {
        crown.SetActive(true);
     }

}
