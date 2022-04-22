using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class BusStation : MonoBehaviour
{
    GameObject character;
    [SerializeField]List<Spline> myRoutes;
    [SerializeField]GameObject bus;

    List<GameObject> busy;
    public float SpawnRatioInSecond;
    private float rate;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Character");
        busy = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        rate += (Time.deltaTime / Random.Range(SpawnRatioInSecond-1,SpawnRatioInSecond+1));
        if (rate > 1)
        {
            rate = 0;
            SpawnBus(Random.Range(0, myRoutes.Count));
        }

    }

    void SpawnBus(int i)
    {
        GameObject busObj = Instantiate(bus, transform.position, Quaternion.identity, transform);
        busObj.GetComponent<Bus>().route = myRoutes[i];
        busy.Add(busObj);
    }
}
