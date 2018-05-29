using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//死亡區
//附加於KillZone上

public class KillZone : MonoBehaviour
{
    public float damage = 100f;  //進入死亡區所造成的每秒傷害

    private void OnTriggerStay2D(Collider2D collision)  //當有東西進入trigger之後每一禎呼叫一次
    {
        if(!collision.CompareTag("Player"))  //如果進入的東西不是叫做Player
        {
            return;  //跳出
        }
        if(PlayerControl.playerInstance != null)  //如果玩家存在
        {
            PlayerControl.Health = PlayerControl.Health - (damage * Time.deltaTime);  //每禎扣玩家的血量
        }
    }

}
