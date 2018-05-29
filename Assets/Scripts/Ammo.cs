using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float damage = 100f;  //子彈傷害
    public float lifetime = 1f;  //子彈存活時間
    private void Start()  //由Unity自動呼叫
    {
        Invoke("Die", lifetime);  //在子彈存活時間過後呼叫Die()函式
    }
    private void OnTriggerEnter2D(Collider2D collision)  //
    {
        if(!collision.CompareTag("Player"))  //如果碰到的不是玩家
        {
            return;  //跳出函式
        }
        PlayerControl.Health = PlayerControl.Health - damage;  //玩家受到傷害
        Die();  //子彈打到玩家也要摧毀
    }
    void Die()  //死亡函式
    {
        Destroy(gameObject);  //摧毀此Script附加的GameObject
    }
}
