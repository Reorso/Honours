using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MyListManager : MonoBehaviour
{
    [SerializeField]int counter;
    [SerializeField] RectTransform alfa, beta;
    [SerializeField] float speed = 20;
    bool pointerOn = false;

    [SerializeField] List<TextMeshProUGUI> places;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            pointerOn = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            pointerOn = false;
        }
        
        if (pointerOn)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, alfa.position.y, transform.position.z), Time.deltaTime * speed);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, beta.position.y, transform.position.z), Time.deltaTime * speed);
        }
        
    }

    // with an index as input and change it's color to red
    public void ChangeColor(int index, Color col)
    {
        places[index].color = col;
    }

    //change text inside the list at a specific index to be crossed
    public void CrossText(int index)
    {
        places[index].text = "<s>" + places[index].text + "<s>";
        ChangeColor(index, Color.red);
    }

}
