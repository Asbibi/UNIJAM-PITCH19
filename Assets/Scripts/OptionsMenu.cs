using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject highscore = null;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("ToggleSound") == 0)
        {
            GameObject.Find("Sound ON").SetActive(false);
            GameObject.Find("Sound OFF").SetActive(true);
        }
    }

    public void UpdateScore()
    {
        highscore.GetComponent<TMPro.TextMeshProUGUI>().text = PlayerPrefs.GetInt("highscore").ToString();
    }
    public void SoundON()
    {
        PlayerPrefs.SetInt("ToggleSound", 1);
        AudioListener.pause = false;
    }

    public void SoundOFF()
    {
        PlayerPrefs.SetInt("ToggleSound", 0);
        AudioListener.pause = true;
    }
}
