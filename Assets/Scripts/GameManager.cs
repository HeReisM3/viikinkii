using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{

    public static GameManager manager; //staticciin päästään käsiks kaikkialta
    public string currentLevel;
    public float health;
    public float previousHealth;
    public float maxHealth;

    //jokaista tasoa varten on muuttuja, koska on vähän tasoja.. Muuttujan nimenpitää olla sama kuin LoadLevel-scriptissä olevan leveltoload-muuttujan arvo
    //esim. mulla ei aluks toiminut, koska kirjotin levelin pienellä alkukirjaimella näissä.
    public bool Level1;
    public bool Level2;
    public bool Level3;



    void Awake() 
    {
        //tarkistetaan onko manageria olemassa
        if (manager == null)
        {
            //jos ei ole, niin kerrotaan että tämä luokka on manageri ja kerrotaan että tää ei saa tuhoutua jos vaihetaa sceneä
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else
        {
            //jos on jo olemassa manageri ja ollaan luomassa toinen, niin tuhotaan tämä
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MainMenu");
        }
        
    }

    //save ja load

    public void Save()
    {
        Debug.Log("Game Saved");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        data.currentLevel = currentLevel;
        data.health = health; //tallennetaan playerdata-luokan muuttujiin nykyiset muuttujien arvot
        data.previousHealth = previousHealth;
        data.maxHealth = maxHealth;
        data.Level1 = Level1;
        data.Level2 = Level2;
        data.Level3 = Level3;
        bf.Serialize(file, data);
        file.Close();

    }

    public void Load()
    {
        //katotaa onko tallennettua tiedostoa ees olemassa, jos on ni load tapahtuu
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            Debug.Log("Game Loaded");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);//castaa PlayerData-muotoon
            file.Close();

            //siirretään ladattu info manageriin
            health = data.health;
            previousHealth = data.previousHealth;
            maxHealth = data.maxHealth;
            currentLevel = data.currentLevel;
            Level1 = data.Level1;
            Level2 = data.Level2;
            Level3 = data.Level3;
        }
    }
}

//Game managerin halutaan olevan aina olemassa, ja kulkee koko pelin läpi joka scenessä. Koskaan ei pitäisi olla kahta pelissä (singleton).

//Toinen luokka, joka voidaan serialisoida. Pitää sisällään vain sen datan mitä serialisoidaan.
[Serializable]
class PlayerData
{
    public string currentLevel;
    public float health;
    public float previousHealth;
    public float maxHealth;
    public bool Level1;
    public bool Level2;
    public bool Level3;




}
