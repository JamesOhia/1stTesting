using System.Collections;
using UnityEngine;
public class Movement : MonoBehaviour
{
    //private float Running;
    public static Vector3 MoveDirection;
    private Rigidbody rb;
    private bool isjump;
    private Animator anim;
    private float JumpPower;
    private float gravity;
    public static int Speed;
    //private BoxCollider collide;
    //Ground Variables
    public static bool OnGround;
    [SerializeField] private bool onground;

    //Delete *
    //private Vector3 vel;
    void Start()
    {
        //
        //collide = this.GetComponent<BoxCollider>();
        //MoveDirection = Vector3.zero;
        JumpPower = 90;
        Speed = 50;
        anim = this.GetComponent<Animator>();
        //isjump = false;
        rb = this.GetComponent<Rigidbody>();
       

        this.GetComponent<Animator>().enabled = true;
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            onground = true;
           
        }
    } 
  /*  void OnTriggerExit(Collider col)
    {
        onground = false;
    }*/

    private void Update()
    {
        
        OnGround = onground;
        MoveDirection = transform.position;
        isjump = SwipeManager.swipeUp;
        if (SwipeManager.swipeRight)
        {
            if ((this.transform.position.z < 156) && (this.transform.position.z > 140))
            {
               // MoveDirection.z = 130;
               // this.transform.position = MoveDirection;
                 while(this.transform.position.z > 130)
                 {
                     MoveDirection.z -= 0.2f * Time.deltaTime;
                     this.transform.position = MoveDirection;
                 }
            }
            else if ((this.transform.position.z > 156) && (this.transform.position.z < 176))
            {
               // MoveDirection.z = 148;
                //this.transform.position = MoveDirection;
                 while(this.transform.position.z > 149)
                 {
                     MoveDirection.z -= 0.2f * Time.deltaTime;
                     this.transform.position = MoveDirection;
                 }
            }
        }

        if (SwipeManager.swipeLeft)
        {
            if ((this.transform.position.z < 156) && (this.transform.position.z > 140))
            {
               // MoveDirection.z = 169;
               // this.transform.position = MoveDirection;
                while (this.transform.position.z < 166)
                {
                    MoveDirection.z += 0.2f * Time.deltaTime;
                    this.transform.position = MoveDirection;
                }
            }
            else if ((this.transform.position.z > 124) && (this.transform.position.z < 140))
            {
                //MoveDirection.z = 148;
                //this.transform.position = MoveDirection;
                while (this.transform.position.z < 148)
                {
                    MoveDirection.z += 0.2f * Time.deltaTime;
                    this.transform.position = MoveDirection;
                }
            }
        }
        if(ArmyCaught.PlayerDamaged && PoliceCaught.PlayerDamaged)//Once the player gets caught
        {
            this.enabled = false;
        }
    }
    void FixedUpdate()
    {

       rb.velocity = new Vector3(Speed, 0, 0);

        if (isjump && onground)
        {
            onground = false;
            //collide.enabled = false;
            rb.velocity = new Vector3(rb.velocity.x, JumpPower, 0);
            //rb.AddForce(new Vector3(0, 80, 0), ForceMode.Impulse);
            anim.SetBool("Jumped", true);
            StartCoroutine(Jumped());
        }

        //vel = rb.velocity;
       /* if(vel.magnitude == 0)
        {
            this.transform.Translate(0, 0, 0);
        }*/

        
    }
        IEnumerator Jumped()
        {
            yield return new WaitForSeconds(0.8f);
            anim.SetBool("Jumped", false);
            //collide.enabled = true;
            isjump = false;
            MoveDirection.y = 6.1f;
            transform.position = MoveDirection;
            yield return new WaitForSeconds(1.2f);
            onground = true;
        
        }
    }
