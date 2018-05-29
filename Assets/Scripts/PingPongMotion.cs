using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//平台移動動畫
//附加於平台上

public class PingPongMotion : MonoBehaviour
{
    private Transform platformTransform = null;  //平台的Transform
    private Vector3 originalPosition = Vector3.zero;  //平台的原點
    public Vector3 movingAxis = Vector2.zero;  //平台移動的軸
    public float distance = 3f;
    private void Awake()
    {
        platformTransform = GetComponent<Transform>();  //取得平台的Transform Component
        originalPosition = platformTransform.position;  //將原點設為平台的目前位置
    }
    private void Update()
    {
        //Mathf.PingPong(time, distance)會回傳在0~distance之間的值
        //藉由這個函式讓平台上下移動或左右移動  更改軸即可
        platformTransform.position = originalPosition + movingAxis * Mathf.PingPong(Time.time, distance);
    }
}
