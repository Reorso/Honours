using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEvent : MonoBehaviour
{
    float timer = 0;
    GameManager gm;
    [SerializeField]int index = 0,i = 0;
    bool showingText;
    [SerializeField] List<string> text;
    [SerializeField]int timeStart, minuteStart;
    [SerializeField]GameObject nextEvent;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.CheckTime(timeStart, minuteStart))
        {
            gm.listManager.CrossText(index);
            Time.timeScale = 1;
            if (nextEvent != null)
            {
                nextEvent.SetActive(true);
            }
            Destroy(gameObject);
        }
        
        if (showingText)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (i < text.Count)
                {
                    gm.SetText(text[i]);
                    i++;
                }
                else
                {
                    showingText = false;
                    gm.SetInActiveText();
                    gm.listManager.ChangeColor(index, Color.green);
                    Time.timeScale = 1;
                    if (nextEvent != null)
                    {
                        nextEvent.SetActive(true);
                    }
                    Destroy(gameObject);
                }
            }
        }
    }

    //check if character stays on trigger for 5 seconds, then destroy it
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (timer < 2)
            {
                timer += Time.deltaTime;
            }
            else
            {
                //Destroy(gameObject);
                GetComponent<MeshRenderer>().enabled = false;
                gm.SetActiveText(text[0]);
                
                showingText = true;
                i++;
                //gm.listManager.CrossText(index);
                Time.timeScale = 0;
                GetComponent<Collider>().enabled = false;
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        timer = 0;
    }
}
