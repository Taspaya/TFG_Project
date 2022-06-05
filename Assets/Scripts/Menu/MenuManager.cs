using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] string[] Resolutions;
    [SerializeField] string[] Difficulty;

    

    int []low = new int[2];
    int []medium = new int[2];
    int []high = new int[2];
    int []extreme = new int[2];

    [SerializeField] TextMeshProUGUI resText;
    [SerializeField] TextMeshProUGUI difText;
    int resIndex = 0;
    int difIndex = 0;
    FullScreenMode fullscreen = FullScreenMode.FullScreenWindow;
    
    private void Start()
    {
        low[0]      = 1280; low[1]      = 720;
        medium[0]   = 1920; medium[1]   = 1080;
        high[0]     = 2560; high[1]     = 1440;
        extreme[0]  = 3840; extreme[1]  = 2160;

        Debug.Log(difIndex);
        Debug.Log(Difficulty.Length);
        difText.text = Difficulty[difIndex];
        resText.text = Resolutions[resIndex];
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void TriggerSettings(bool value)
    {
        GetComponent<Animator>().SetBool("Settings", value);
    }
    public void TriggerHome()
    {
        GetComponent<Animator>().SetTrigger("Settings");
    }

    public void IncreaseRes()
    {
        resIndex++;
        if (resIndex >= Resolutions.Length-1) resIndex = 0;
        resText.text = Resolutions[resIndex];
        ChangeResolution();
    }

    void ChangeResolution()
    {
        //Screen.SetResolution ((int)r.x, (int)r.y, Screen.fullScreen);
        switch (resIndex)
        {
            case 0:
                Screen.SetResolution(low[0], low[1],FullScreenMode.FullScreenWindow);
                break;
            case 1:
                Screen.SetResolution(medium[0], medium[1], FullScreenMode.FullScreenWindow);
                break;
            case 2:
                Screen.SetResolution(high[0], high[1], FullScreenMode.FullScreenWindow);
                break;
            case 3:
                Screen.SetResolution(extreme[0], extreme[1], FullScreenMode.FullScreenWindow);
                break;
            default:
                break;
        }
    }
    public void IncreaseDif()
    {
        difIndex++;
        if (difIndex >= Difficulty.Length-1) difIndex = 0;
        difText.text = Difficulty[difIndex];
    }

    public void DecreaseRes()
    {
        resIndex--;
        if (resIndex < 0) resIndex = Resolutions.Length - 1;
        resText.text = Resolutions[resIndex];
        ChangeResolution();
    }
    public void DecreaseDif()
    {
        difIndex--;
        if (difIndex < 0) difIndex = Difficulty.Length - 1;
        difText.text = Difficulty[difIndex];
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Play()
    {
        SceneManager.LoadScene("LoadScene");
    }


    IEnumerator LoadSceneLoader()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("LoadingScene");
    }
    }
