using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGenerator : MonoBehaviour
{
    [SerializeField]GameObject eventPref;
    [SerializeField] List<GameObject> events = new List<GameObject>();
    [SerializeField] List<Transform> locations = new List<Transform>();
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //generate event at random location every 5 seconds
        if (timer > 5)
        {
            int rand = Random.Range(0, locations.Count);
            GameObject newEvent = Instantiate(eventPref, locations[rand].position, Quaternion.identity);
            events.Add(newEvent);
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
        //generate event at random location every 5 seconds

    }
}
