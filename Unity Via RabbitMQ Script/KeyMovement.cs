using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMovement : MonoBehaviour
{
    public CharacterController controller;
    public float velocity = 6f; 
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");                      //左右鍵輸入
        float vertical = Input.GetAxisRaw("Vertical");                          //上下鍵輸入
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;   //移動方向正規化
        //Debug.Log("Horizontal"+horizontal);
        //Debug.Log("Vertical"+vertical);
        //print(Input.GetButtonDown("Jump"));                                     //空白鍵按下時顯示Jump
        if (direction.magnitude >= 0.1f)                                        //有移動輸入時
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;          //螢幕轉之後計算人物要跟著轉幾度
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime); //讓人物旋轉較流暢的函式
            transform.rotation = Quaternion.Euler(0f, angle, 0f);                                                   //轉動人物
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;                              //轉動輸入的移動方向至當前螢幕的參考方向
            controller.Move(moveDir.normalized * velocity * Time.deltaTime);                                        //傳送移動向量給人物控制器
        }
    }
}
