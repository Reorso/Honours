using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]TMP_Text clock,text;
    [SerializeField] GameObject textPanel;
    [SerializeField]public MyListManager listManager;
    float minutes=0, hours=9;
    
    void Start()
    {
        SetInActiveText();
    }

    // Update is called once per frame
    void Update()
    {
        if (minutes >= 60)
        {
            hours += 1;
            minutes = 0;
        }
        minutes += Time.deltaTime;
        //update text with time since start of game in hours and minutes converting x seconds to y minutes
        clock.text = "Time: " + hours.ToString("00") + ":" + minutes.ToString("00") + " ";
        //if time is greater than 60 minutes then add 1 to hours and subtract 60 minutes from time

    }

    //setactive the text panel with the text
    public void SetActiveText(string text)
    {
        this.text.text = text;
        textPanel.SetActive(true);
    }

    //set the text panel to false
    public void SetInActiveText()
    {
        textPanel.SetActive(false);
    }

    //set the text to another text
    public void SetText(string text)
    {
        this.text.text = text;
    }

    public bool CheckTime(int hours, int minutes)
    {
        if (this.hours < hours)
        {
            return true;
        }
        else if(hours == this.hours && minutes > this.minutes)
        {
            return true;
        }
        return false;

    }
    //load scene with name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
