using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{

    public void LoadMap()
    {
        SceneManager.LoadScene("Map");
    }

    public void Save()
    {
        //ajetaan menusta kun painetaan save-painiketta
        GameManager.manager.Save();
    }

    public void Load()
    {
        //load paninike
        GameManager.manager.Load();
    }





}
