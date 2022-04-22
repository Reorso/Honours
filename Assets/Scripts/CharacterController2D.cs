using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField]float speed = 10;
    [SerializeField]float jspeed = 100;
    [SerializeField]float sSpeed = 20;
    public bool isUsingTransport = false, cooldown = true;
    GameObject lastTransport;
    [SerializeField] GameObject vis;
    Animator anim;
    public Vector3 forwardDir = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {
        anim = vis.GetComponent<Animator>();
        forwardDir = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isUsingTransport)
        {
            Vector3 dir;
            float curSpeed = speed;
            dir.z = - Input.GetAxis("Horizontal");
            dir.x = Input.GetAxis("Vertical");
            dir.y = 0;

            Vector3 trueDirx;
            Vector3 trueDiry;

            trueDirx = forwardDir * dir.x;
            trueDiry = -Vector3.Cross(Vector3.up, forwardDir) * dir.z;   

            dir = trueDirx + trueDiry;

            if (dir.magnitude > 0)
            {
                //check if the player is running and set the animation
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    curSpeed = sSpeed;
                }
                
                GetComponent<Rigidbody>().velocity = dir.normalized * curSpeed * Time.deltaTime;
                //lerp vis rotation to match dir value if not zero
                vis.transform.rotation = Quaternion.Lerp(vis.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * jspeed);
                //detect ground collider and place the player above the ground for 0.01 units
                RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector3.down, out hit, 10, LayerMask.GetMask("Ground")))
                {
                    transform.position = new Vector3(transform.position.x, hit.point.y + 0.03f, transform.position.z);
                }

            }

            anim.SetFloat("Speed", Mathf.InverseLerp( 0 , sSpeed * Time.deltaTime, GetComponent<Rigidbody>().velocity.magnitude));
            
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cooldown)
            {
                if (isUsingTransport)
                {
                    print("working");
                    Unpossess();
                    cooldown = false;
                    StartCoroutine(Cooldown(0.1f));
                }
                else
                {
                    if (Physics.OverlapSphere(transform.position, 10, LayerMask.GetMask("Veichle")).Length > 0)
                    {
                        Collider hit = Physics.OverlapSphere(transform.position, 0.5f, LayerMask.GetMask("Veichle"))[0];
                        //if (hit.collider.gameObject.tag == "Transport")
                        //{
                        Possess(hit.gameObject);
                        cooldown = false;
                        StartCoroutine(Cooldown(2));
                        //}
                    }
                    else
                    {
                        GetComponent<Rigidbody>().AddForce(Vector3.up * jspeed);
                    }
                }
            }

        }
        //jump when pressing space
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //   
        //}

    }


    public void Possess(GameObject vehicle)
    {
        if (!isUsingTransport)
        {
            isUsingTransport = true;
            lastTransport = vehicle;
            vis.SetActive(false);
            transform.parent = vehicle.transform;
            lastTransport.GetComponent<Bus>().isBusy = true;
        }
    }

    public void Unpossess()
    {
        lastTransport.GetComponent<Bus>().isBusy = false;
        transform.position = lastTransport.transform.position;
        transform.parent = null;
        isUsingTransport = false;
        vis.SetActive(true);
    }

    public IEnumerator Cooldown(float v)
    {
        yield return new WaitForSeconds(v);
        cooldown = true;
        print("ready "+cooldown+" "+isUsingTransport+" ");
    }
}

