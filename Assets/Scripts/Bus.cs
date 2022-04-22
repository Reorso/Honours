using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;
public class Bus : MonoBehaviour
{
    [SerializeField] float speed = 1;
    public Spline route;
    public CharacterController2D ch;
    public float ratio = 0;
    bool cooldown = false;
    private float rate = 0;
    public bool isForward = true;
    public float durationInSecond;
    public bool isBusy=false;
    // Start is called before the first frame update
    void Start()
    {
        
        
        isForward = Random.Range(0, 2) == 0;
        if (isForward)
        {
            speed *= -1;
            rate = route.Length -1;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, -transform.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        rate += (Time.deltaTime / durationInSecond) * speed * route.Length;
        //check if rate is in between route.lenght and 0
        if (rate < route.Length && rate > 0)
        {
            route.GetComponent<ExampleFollowSpline>().PlaceFollower(transform, rate);
        }
        else
        {
            if (isBusy)
            {
                
            }
            else
            {
                Destroy(gameObject);
            }
            
        }

    }


}
