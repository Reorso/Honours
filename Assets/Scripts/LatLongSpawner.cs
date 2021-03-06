using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatLongSpawner : MonoBehaviour
{

	float[] lng = { -4.264535f,
-4.2494f,
-4.245483f,
-4.269117f,
-4.255292f,
-4.237467f,
-4.23375f,
-4.263521f,
-4.287211f,
-4.281414f,
-4.288487f,
-4.280371f,
-4.289405f,
-4.291872f,
-4.271852f,
-4.241f, 
-4.25885f,
-4.249976f,
-4.251367f,
-4.208477f,
-4.251945f,
-4.244671f,
-4.2419f,
-4.27807f,
-4.308459f,
-4.280677f,
-4.224511f,
-4.235294f,
-4.226823f,
-4.248127f,
-4.279158f,
-4.29449f,
-4.311378f,
-4.262164f,
-4.265485f,
-4.272346f,
-4.252567f,
-4.31046f,
-4.341761f,
-4.270055f,
-4.286127f,
-4.282675f,
-4.277989f,
-4.29567f,
-4.217704f,
-4.210282f,
-4.213473f,
-4.260426f,
-4.246983f,
-4.273478f,
-4.274764f,
-4.261756f,
-4.270753f,
-4.265319f,
-4.259255f,
-4.223158f,
-4.254817f,
-4.26877f,
-4.275232f,
-4.258597f,
-4.285022f,
-4.254465f,
-4.260291f,
-4.201594f,
-4.305367f,
-4.301448f,
-4.259847f,
-4.26887f,
-4.26136f,
-4.25164f,
-4.259101f,
-4.24695f,
-4.306222f,
-4.321625f,
-4.286476f,
-4.242535f,
-4.230428f,
-4.292972f,
-4.262688f,
-4.335149f,
-4.332797f,
-4.269701f,
-4.135251f,
-4.192788f,
-4.290879f,
-4.324766f,
-4.250996f,
-4.344079f,
-4.289201f,
-4.252307f,
-4.176167f,
-4.314376f,
-4.333357f,
-4.20064f,
-4.278874f,
-4.292988f,
-4.213115f,
-4.339927f,
};
    //declare an array of float values
    float[] lat = {55.86155f,
55.86077f,
55.858167f,
55.871367f,
55.856829f,
55.862883f,
55.84945f,
55.8566f,
55.859081f,
55.8849f,
55.878278f,
55.87501f,
55.868352f,
55.872681f,
55.864788f,
55.862983f,
55.8525f,
55.857575f,
55.865817f,
55.846515f,
55.859716f,
55.855961f,
55.860633f,
55.871763f,
55.869707f,
55.863422f,
55.858123f,
55.855152f,
55.848939f,
55.850554f,
55.85378f,
55.851918f,
55.863f,
55.84332f,
55.834976f,
55.835042f,
55.864993f,
55.883971f,
55.86304f,
55.864563f,
55.865222f,
55.829057f,
55.831951f,
55.827371f,
55.842561f,
55.863128f,
55.858222f,
55.870992f,
55.853194f,
55.855185f,
55.854391f,
55.865291f,
55.874571f,
55.827395f,
55.827282f,
55.864466f,
55.877822f,
55.878787f,
55.845807f,
55.860776f,
55.886934f,
55.881493f,
55.865063f,
55.852705f,
55.865683f,
55.850987f,
55.858587f,
55.85658f,
55.87422f,
55.85476f,
55.864132f,
55.85586f,
55.876975f,
55.871029f,
55.845518f,
55.87823f,
55.882232f,
55.8585f,
55.817338f,
55.836355f,
55.846552f,
55.841494f,
55.840336f,
55.867405f,
55.891127f,
55.890398f,
55.848564f,
55.824213f,
55.863858f,
55.862102f,
55.84425f,
55.849072f,
55.874555f,
55.848885f,
55.86698f,
55.864135f,
55.860071f,
55.831024f,
 };
    [SerializeField]float minLat = 55.837583f, maxLat = 55.879861f, minLng = -4.325722f, maxLng = -4.181056f, maxY, minY, maxX, minX;
    [SerializeField]BoxCollider col;
    [SerializeField]GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        
        //calculate min and max x and y values based on object bounding box
        minX = transform.position.x - (col.bounds.size.x / 2);
        maxX = transform.position.x + (col.bounds.size.x / 2);
        minY = transform.position.y - (col.bounds.size.y / 2);
        maxY = transform.position.y + (col.bounds.size.y / 2);

        //take lat and lng and convert them to x and y relative to the map
        for (int i = 0; i < lat.Length; i++)
        {
            //check if the lat and lng are within the max and min lat and lng
            if (lat[i] > minLat && lat[i] < maxLat && lng[i] > minLng && lng[i] < maxLng)
            {
                //convert lat and lng to x and y
                float x = (lng[i] - minLng) / (maxLng - minLng) * (maxX - minX) + minX;
                float y = (lat[i] - minLat) / (maxLat - minLat) * (maxY - minY) + minY;

                //create a new gameobject at the x and y position
                GameObject newObj = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
                newObj.transform.parent = transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
