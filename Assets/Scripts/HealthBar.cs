using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//綠色血條  配合玩家血量減少或增加其寬度
//附加於綠色血條上

public class HealthBar : MonoBehaviour
{
    public RectTransform barTransform = null;  //方型的Transform component
    public float maxSpeed = 1f;  //每秒最大速率
    private void Awake()
    {
        barTransform = GetComponent<RectTransform>();  //取得血條的RectTransform component
    }
    private void Start()
    {
        if(PlayerControl.playerInstance != null)  //如果玩家物件存在
        {
            barTransform.sizeDelta = new Vector2(Mathf.Clamp(PlayerControl.Health, 0, 100), barTransform.sizeDelta.y);
        }
    }
    private void Update()
    {
        float healthUpdate = 0f;
        if(PlayerControl.playerInstance != null)
        {
            healthUpdate = Mathf.MoveTowards(barTransform.rect.width, PlayerControl.Health, maxSpeed);
        }
        barTransform.sizeDelta = new Vector2(Mathf.Clamp(PlayerControl.Health, 0, 100), barTransform.sizeDelta.y);
    }
}
