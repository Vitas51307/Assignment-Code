using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class RabbitMovement : MonoBehaviour
{
    public CharacterController controller;
    public float velocity = 6f; 
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
    private CoordinatesTrans player;
    private Animator animator;
    private float PythonHorizontal;
    private float PythonVertical;
    private float PythonJump;
    
    [SerializeField]
    private float jumpspeed,gravity;
    void Awake()
    {
        player = GameObject.FindObjectOfType<CoordinatesTrans>();        //初始化 CoordinatesTrans
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        string[] Coor=null;

        //print(Input.GetAxisRaw("Horizontal"));
        //print(Input.GetAxis("Vertical"));
        //float MotionX = Input.GetAxis("Horizontal") * Time.deltaTime * velocity;
        //float MotionY = Input.GetAxis("Vertical") * Time.deltaTime * velocity;
        Coor = player.Coordinate;                                            //向 CoordinatesTrans 取值
        //Debug.Log("Length:"+Coor.Length);
        //Debug.Log("PythonAfterSplit:"+Coor[0]+Coor[1]+Coor[2]);
        //Debug.Log("x:"+horizontal);
        //Debug.Log("y:"+vertical);
        //print(Input.GetButtonDown("Jump"));                                  //空白鍵按下時顯示Jump
        if(!Array.TrueForAll(Coor, Judge))                                          //有Python移動輸入時
        {
            bool button;
            PythonHorizontal = float.Parse(Coor[0]);
            PythonVertical = float.Parse(Coor[2]); 
            PythonJump = float.Parse(Coor[1]);
            //print("Coor X: "+PythonHorizontal+" Z: "+PythonVertical+"  Y: "+Coor[2]); 
            Vector3 pdir = new Vector3(PythonHorizontal, 0f, PythonVertical).normalized;
            //print("PythonAfterJudge&Norm X: "+pdir.x+"  Y: "+pdir.y+"  Z: "+pdir.z);
            Move(pdir);

            if (PythonJump!=0)
            {
                button=true;
                if (button)
                {
                    Vector3 Jump = new Vector3 (0f, jumpspeed, 0f) ;
                    Jump.y -= gravity * Time.deltaTime;
                    controller.Move(Jump * Time.deltaTime);
                    System.Threading.Thread.Sleep(70);
                    if(controller.isGrounded)
                    button = false;
                }
            }
            

        }
        if(Input.GetAxisRaw("Vertical")==0&&Input.GetAxisRaw("Horizontal")==0)
        {
            animator.SetBool("isRunning", PythonVertical!= 0);
            if (PythonVertical == 0)
            {
                animator.SetBool("isRunning", PythonHorizontal!= 0);
            }
        }
    }
    private void Move(Vector3 direction)
    {
            //Debug.Log("InToMove X: "+direction.x+"  Y: "+direction.y+"  Z: "+direction.z);
            controller.Move(direction.normalized * velocity * Time.deltaTime);                                        //傳送移動向量給人物控制器
            //Debug.Log("ToController X: "+direction.normalized.x+"  Y: "+direction.normalized.y+"  Z: "+direction.normalized.z);
            //float MotionX = horizontal * Time.deltaTime * velocity;
            //float MotionY = vertical * Time.deltaTime * velocity;
            //transform.Translate(MotionX, 0, MotionY);
    }
   private static bool Judge(string value)
   {
      return value=="0";
   }
}
