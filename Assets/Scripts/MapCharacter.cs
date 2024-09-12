using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCharacter : MonoBehaviour
{

    public float speed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.manager.currentLevel != "")
        {
            GameObject.Find(GameManager.manager.currentLevel).GetComponent<LoadLevel>().Cleared(true);
            //current lvl on jotai muuta kuin tyhjä, eli ollaan tultu karttaan jostain tasosta takaisin. Asetetaan siis pelaajan sijainti spawniin
            transform.position = GameObject.Find(GameManager.manager.currentLevel).transform.GetChild(0).transform.position;
            
            // if (!GameObject.Find(GameManager.manager.currentLevel).transform.GetChild(1).gameObject.activeInHierarchy)
            // {
            //     GameObject.Find(GameManager.manager.currentLevel).transform.GetChild(1).gameObject.SetActive(true);
            // }

        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime; //tää on paskaa koodia, koska se luo joka updatessa nää, hyi
        float verticalMove = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(horizontalMove, verticalMove, 0);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("LevelTrigger"))
        {
            GameManager.manager.currentLevel = other.gameObject.name;


            SceneManager.LoadScene(other.gameObject.GetComponent<LoadLevel>().levelToLoad);
        }
    }
}
