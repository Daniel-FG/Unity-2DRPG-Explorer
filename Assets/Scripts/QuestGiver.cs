using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public string questName = null;  //讓玩家可讀的任務名稱
    public Text caption = null;  //顯示文字的UI
    public string[] captionText;  //任務內容
    private void OnTriggerEnter2D(Collider2D collision)  //有東西進入trigger時由Unity自動呼叫
    {
        if(! collision.CompareTag("Player"))  //如果進入trigger的不是玩家
        {
            return;  //跳出函式
        }
        Quest.QUESTSTATUS status = QuestManager.GetQuestStatus(questName);  //取得該任務的任務狀態
        caption.text = captionText[(int)status];
    }
    private void OnTriggerExit2D(Collider2D collision)  //有東西離開trigger時由Unity自動呼叫
    {
        if (!collision.CompareTag("Player"))  //如果離開trigger的不是玩家
        {
            return;  //跳出函式
        }
        Quest.QUESTSTATUS status = QuestManager.GetQuestStatus(questName);  //取得該任務的任務狀態
        if(status == Quest.QUESTSTATUS.UNASSIGNED)  //如果任務狀態為未指派
        {
            QuestManager.SetQuestStatus(questName, Quest.QUESTSTATUS.ASSIGNED);  //更改該任務狀態為已指派
        }
        if(status == Quest.QUESTSTATUS.COMPLETE)  //如果任務狀態為完成任務
        {
            Application.LoadLevel(5);  //遊戲完成
        }
    }
}
