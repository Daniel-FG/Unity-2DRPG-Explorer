using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public GameObject ammoPrefab = null;  //利用Unity放上子彈的Prefab
    private Transform ammoTransform = null;  //取得子彈的Transform
    public Vector2 timeDelayRange = Vector2.zero;  //利用Unity更改產生的時間間隔
    public float ammoSpeed = 10f;  //子彈速度
    public float ammoDamage = 10f;  //子彈傷害
    public float ammoLifetime = 6f;  //子彈存活時間
    private void Awake()  //由Unity自動呼叫
    {
        ammoTransform = GetComponent<Transform>();  //取得子彈的Transform Component
    }
    private void Start()  //由Unity自動呼叫
    {
        FireAmmo();  //呼叫發射子彈函式
    }
    public void FireAmmo()  //發射子彈函式
    {
        //產生一個新的子彈物件
        GameObject ammoObject = Instantiate(ammoPrefab, ammoTransform.position, ammoTransform.rotation) as GameObject;
        Ammo ammoComponent = ammoObject.GetComponent<Ammo>();  //取得新產生出來的子彈裡的Ammo Component
        Mover moverComponent = ammoObject.GetComponent<Mover>();  //取得新產生出來的子彈裡的Mover Component
        moverComponent.speed = ammoSpeed;  //將Mover內的子彈速度賦值
        ammoComponent.damage = ammoDamage;  //將Ammo內的子彈傷害賦值
        ammoComponent.lifetime = ammoLifetime;  //將Ammo內的子彈存活時間賦值
        Invoke("FireAmmo", Random.Range(timeDelayRange.x, timeDelayRange.y));  //在自行設定的間距內隨機產生時間間隔並再執行一次
    }
}
