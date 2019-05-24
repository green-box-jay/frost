using System.Collections;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject settings;
    public GameObject lvls;
    public void StartGame()
    {
        Application.LoadLevel(1);
        
    }

    public void LoadGame()
    {

    }

    public void Setting()
    {
        settings.SetActive(!settings.activeSelf);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
   public void setMusic(float value)
    {
        Global.music = value;
    }
    public void setSound(float value)
    {
        Global.sound = value;
    }
    public void ScrollSpinnerL()
    {

    }
    public void ScrollSpinnerR()
    {

    }
    public void LVLs()
    {
        lvls.SetActive(!lvls.activeSelf);
    }
}