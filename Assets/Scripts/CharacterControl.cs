using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterControl : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;

    public Animator animator;
    public Rigidbody2D rb2D;

    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;
    public bool grounded;



    public Image filler;

    public float counter; //laskee 0:sta kahteen ja sitten aloittaa alusta.
    public float maxCounter; //2 sec, määrittää kuinka nopeasti health bar liikkuu uuteen arvoon



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {


        //is the player grounded
        if (Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        Debug.Log(Input.GetAxisRaw("Horizontal"));

        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            //joko a tai d pohjassa, k��nnet��n hahmoa riippuen kulkusuunnasta && ollaan liikkeess�
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            rb2D.velocity = new Vector2(0, jumpForce);
            animator.SetTrigger("Jump");
        }

        if (counter > maxCounter)
        {
            GameManager.manager.previousHealth = GameManager.manager.health;
            counter = 0;
        }
        else
        {
            counter += Time.deltaTime;
        }
        
        filler.fillAmount = Mathf.Lerp(GameManager.manager.previousHealth / GameManager.manager.maxHealth, GameManager.manager.health / GameManager.manager.maxHealth, counter / maxCounter);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Debug.Log("osuttiin trappiin");
            TakeDamage(20);

        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("AddHealth"))
        {
            Destroy(other.gameObject);
            Heal(10);

        }

        if (other.CompareTag("AddMaxHealth"))
        {
            Destroy(other.gameObject);
            AddMaxHealth(50);

        }

        if (other.CompareTag("LevelEnd"))
        {
            SceneManager.LoadScene("Map");
        }
    }

    void Heal(float amt)
    {
        GameManager.manager.previousHealth = filler.fillAmount * GameManager.manager.maxHealth;
        counter = 0;
        GameManager.manager.health += amt;
        if (GameManager.manager.health > GameManager.manager.maxHealth)
        {
            GameManager.manager.health = GameManager.manager.maxHealth;
        }
    }

    void AddMaxHealth (float amt)
    {
        GameManager.manager.maxHealth += amt;

    }

    void TakeDamage(float dmg)
    {
        GameManager.manager.previousHealth = filler.fillAmount * GameManager.manager.maxHealth;
        counter = 0;
        GameManager.manager.health -= dmg;
    }
}
