using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//移動物件
//附加在任何會移動的GameObject上

public class Mover : MonoBehaviour
{
    public float speed = 10f;  //移動速度
    private Transform moverTransform = null;  //此Object的Transfome component
    private void Awake()  //由Unity自動呼叫
    {
        moverTransform = GetComponent<Transform>();  //取得該物件的Transform Component
    }
    private void Update()  //由Unity自動每禎呼叫
    {
        //該物件的位置為物件前進方向乘上速率與每一禎經過的時間
        //Position = Normalized Direction * Speed * Time  (P = VT)
        //Transform.forward的方向為藍色箭頭的指示方向
        moverTransform.position = moverTransform.position + (moverTransform.forward * speed * Time.deltaTime);
    }
}
