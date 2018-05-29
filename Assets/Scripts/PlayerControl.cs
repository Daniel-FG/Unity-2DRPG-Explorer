using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour
{
    public enum PlayerFaceDierction  //新增一個列舉型別  玩家面對的方向  向左值為-1  向右值為+1
    {
        LEFT = -1, RIGHT = 1
    };
    public PlayerFaceDierction facing = PlayerFaceDierction.RIGHT;  //玩家初始面向右邊

    public LayerMask groundLayer;  //地表層
    public Rigidbody2D playerBody = null;  //玩家的RigidBody Component
    public Transform playerTransform = null;  //玩家的Transform Component
    public CircleCollider2D feetCollider2D = null;  //玩家的Circle Collider 2D Component

    public string horizontalAxis = "Horizontal";  //水平軸
    public string jumpButton = "Jump";  //跳躍按鈕

    public float maxSpeed = 50f;  //玩家行進的最大速度
    public float jumpForce = 600;  //跳起來的力道
    public float jumpTimeOut = 1f;  //跳躍時間

    public bool isOnTheGround = false;  //是否在站在地上
    private bool canJump = true;  //是否現在可以跳
    public bool canControl = true;  //是否現在可以操控角色

    public static PlayerControl playerInstance = null;  //將此類別變成Singleton單例

    [SerializeField]  //讓private類別的變數也能夠顯示在Object Inspector裡面
    private static float health = 100f;  //玩家血量
    public static float Health  //玩家血量屬性
    {
        get  //呼叫存取子
        {
            return health;  //呼叫時回傳上方private的玩家血量
        }
        set  //更改存取子  每次修改血量時都會呼叫此大括號內的程式碼
        {
            health = value;  //設定血量
            if(health <= 0)  //如果血量歸零
            {
                Die();  //呼叫死亡函式
            }
        }
    }
    //======Unity自動呼叫的函式======
    private void Awake()  //在物件產生時即呼叫此函式
    {
        playerBody = GetComponent<Rigidbody2D>();  //取得玩家的Rigid Body 2D Component
        playerTransform = GetComponent<Transform>();  //取得玩家的Transform Component

        playerInstance = this;  //將此類別變成Singleton單例
        FlipDirection();
    }
    private void FixedUpdate()  //由Unity每幾禎呼叫一次  把物理相關程式碼放在此處 e.g. AddForce()
    {
        if(!canControl || health <= 0f)  //如果目前不能控制角色或角色已經死亡
        {
            return;  //跳出函式
        }
        isOnTheGround = IsGrounded();  //透過函式更新玩家是否在地上的布林變數
        float horizontal = CrossPlatformInputManager.GetAxis(horizontalAxis);
        playerBody.AddForce(Vector2.right * maxSpeed * horizontal);
        if (CrossPlatformInputManager.GetButton(jumpButton))
        {
            Jump();
        }

        //將玩家角色箝制在固定範圍內  水平方向速度介在正負最大速度之間  垂直方向則介在負無限大到單次跳躍產生的力道中間
        playerBody.velocity = new Vector2
            (Mathf.Clamp(playerBody.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(playerBody.velocity.y, -Mathf.Infinity, jumpForce));

        //如果水平方向速度為正但是面朝左方  或是  水平方向為負但是面朝右方
        if((horizontal > 0f && facing != PlayerFaceDierction.RIGHT)||(horizontal < 0f && facing != PlayerFaceDierction.LEFT))
        {
            FlipDirection();  //將玩家角色翻面
        }
    }
    private void OnDestroy()  //當玩家物件被摧毀時由Unity自動呼叫
    {
        playerInstance = null;  //將此單例Singleton移除
    }
    //======自定函式======
    private static void Die()  //死亡函式
    {
        Destroy(playerInstance.gameObject);  //摧毀玩家物件
    }
    private bool IsGrounded()  //檢查玩家是否在地上的函式
    {
        Vector2 circleColliderCenter = new Vector2(playerTransform.position.x, playerTransform.position.y) + feetCollider2D.offset;
        Collider2D[] hitCollider = Physics2D.OverlapCircleAll(circleColliderCenter, feetCollider2D.radius, groundLayer);
        if(hitCollider.Length > 0)
        {
            return true;
        }
        return false;
    }
    private void FlipDirection()  //翻轉角色的面朝方向
    {
        facing = (PlayerFaceDierction)((int)facing * -1f);  //將面對方向變數轉型為整數進行乘上-1運算再轉回原本型態  用以紀錄目前面對方向
        Vector3 localScale = playerTransform.localScale;  //取得玩家角色的目前大小
        localScale.x = localScale.x * -1f;  //在x方向乘上-1達到讓角色水平翻轉的目的
        playerTransform.localScale = localScale;  //修改玩家角色的樣子
    }
    public static void Reset()  //重設函式
    {
        Health = 100f;  //將血量重新回復到100
    }
    private void Jump()  //跳躍函式
    {
        if(! isOnTheGround || ! canJump)  //如果呼叫時沒有在地上或是不能跳
        {
            return;  //跳出  不執行跳躍動作
        }
        playerBody.AddForce(Vector2.up * jumpForce);  //在玩家鋼體身上施加一個方向為向上、大小為跳躍力的力
        canJump = false;  //因為跳起來了  所以在空中不能跳
        Invoke("ActivateJump", jumpTimeOut);  //在跳躍時間結束之後  呼叫啟動跳躍功能函式  讓玩家重新可以跳
    }
    private void ActivateJump()  //啟動跳躍功能
    {
        canJump = true;  //現在可以跳
    }
}
