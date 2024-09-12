using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{

    public string levelToLoad;

    public bool cleared;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //katotaan map-scenen alussa onko game-managerissa merkattu et kyseinen taso on läpäisty
        //jos on, ajetaan Cleared -funktio, joka tekee tarpeelliset muutokset objektiin, eli näyttää Stage clear kyltin ja poistaa colliderin.

        if (GameManager.manager.GetType().GetField(levelToLoad).GetValue(GameManager.manager).ToString() == "True")
        {
            Cleared(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cleared(bool isClear)
    {
        if (isClear == true)
        {
            cleared = true;
            //asetetaan GameManagerissa oikea boolean trueksi
            GameManager.manager.GetType().GetField(levelToLoad).SetValue(GameManager.manager, true);
            //Laitetaan stage clear -kyltti näkyviin
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            //Koska taso on läpäisty, poistetaan level trigger objektilta collider
            GetComponent<CircleCollider2D>().enabled = false;
        }

    }
}
