using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]GameObject target;
    [SerializeField]Transform highMap;
    [SerializeField] float zoomSpeed = 0.1f;
    Vector3 offset;
    float height;
    bool map = false;
    float zoom;
    Vector3 distance,distance1;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.transform.position;
        offset.y = 0;
        height = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.mouseScrollDelta.y != 0)
        {
            zoom += Input.mouseScrollDelta.y * zoomSpeed ;
            
            
        }
        if (map)
        {
            distance1 = Vector3.up * -zoom;
            zoom = Mathf.Clamp(zoom, -1, 1);
            //lerp the position to a fixed vector3 and then stop there
            transform.position = Vector3.Lerp(transform.position, highMap.position+distance1, 0.01f);
            //lerp the rotation to a fixed vector3 and then stop there
            transform.rotation = Quaternion.Lerp(transform.rotation, highMap.rotation, 0.01f);
            //pause the game
            
        }
        else
        {

            zoom = Mathf.Clamp(zoom, -1, 1);
            distance = (transform.position - target.transform.position).normalized * (-zoom);
            //rotate around target maintaining the same height and distance from target to camera using mouse
            offset = Quaternion.Euler(0, Input.GetAxis("Mouse X") * Time.deltaTime * 400, 0) * offset;
            //transform.RotateAround(target.transform.position, Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * 400);
            //transform.RotateAround(target.transform.position, Vector3.right, -Input.GetAxis("Mouse Y") * Time.deltaTime * 400);
            //offset = transform.position - target.transform.position;
            //offset.y = 0;
            var v3f = transform.forward;
            v3f.y = 0;
            v3f = v3f.normalized;
            target.gameObject.GetComponent<CharacterController2D>().forwardDir = v3f;
            

            if ((transform.position - target.transform.position + offset + distance).magnitude > 0.1f)
            {
                //transform.LookAt(target.transform);
                var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 40 * Time.deltaTime);
                transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x, height, target.transform.position.z) + offset + distance, 5f * Time.deltaTime);
            }
            else
            {
                //smoothly follow the target
                transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x, height, target.transform.position.z) + offset + distance, 0.1f * Time.deltaTime);
                //transform.position = new Vector3(target.transform.position.x, height, target.transform.position.z) + offset;

                //smoothly rotate the camera to look at the target
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), 0.1f * Time.deltaTime);
                //transform.LookAt(target.transform);
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            map = !map;
            if (map)
            {
                Time.timeScale = 0;
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                transform.GetChild(0).gameObject.SetActive(false);

            }
        }
    }
}
