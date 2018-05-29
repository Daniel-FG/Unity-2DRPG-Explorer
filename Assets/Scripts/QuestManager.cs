using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//任務管理員  附有任務
//附加於任務管理員上

[System.Serializable]  //此行的目的讓這個次類別可以顯示在Inspector中
public class Quest  //任務類別 - 普通的C#類別
{
    public enum QUESTSTATUS {UNASSIGNED = 0, ASSIGNED = 1, COMPLETE = 2 };  //任務狀態的列舉型別   未指派  已指派  完成
    public QUESTSTATUS status = QUESTSTATUS.UNASSIGNED;  //任務狀態預設為未指派
    public string questName = null;  //任務名稱
}
public class QuestManager : MonoBehaviour
{
    public Quest[] quests;  //存放所有任務的陣列
    private static QuestManager instance = null;  //任務管理類別的Singleton單例
    public static QuestManager Instance  //單例屬性
    {
        get  //當有外部程式要呼叫時
        {
            if(instance == null)  //如果單例不存在
            {
                GameObject questObject = new GameObject("Default");  //建立一個新物件  名稱為Default
                instance = questObject.AddComponent<QuestManager>();  //將QuestManager Component加入進新建立的物件中
            }
            return instance;  //回傳單例
        }
    }
    private void Awake()  //由Unity自動呼叫
    {
        if(instance)  //如果任務管理員存在
        {
            DestroyImmediate(gameObject);  //馬上摧毀該物件
            return;  //跳出函式
        }
        instance = this;  //建立單例
        DontDestroyOnLoad(gameObject);  //不在切換場景(Scene)時摧毀任務管理員
        //Unity在讀取新的場景(Scene)時會摧毀舊場景中所有物件  
        //之後才會建立新場景中的物件
    }
    public static Quest.QUESTSTATUS GetQuestStatus(string name)  //取得任務狀態  參數為欲取得任務狀態的任務名稱
    {
        foreach(Quest q in Instance.quests )  //循環過整個任務陣列
        {
            if (q.questName.Equals(name))  //如果有對應的任務名稱
                return q.status;  //回傳該任務的任務狀態
        }
        return Quest.QUESTSTATUS.UNASSIGNED;  //如果沒有找到則回傳未指派
    }
    public static void SetQuestStatus(string name, Quest.QUESTSTATUS newStatus)  //設定任務狀態函式  參數為任務名稱與新的任務狀態
    {
        foreach(Quest q in Instance.quests)  //循環過整個任務陣列
        {
            if (q.questName.Equals(name))  //如果任務陣列中有目標任務名稱
            {
                q.status = newStatus;  //該任務的任務狀態為新狀態
                return;  //跳出迴圈與函式
            }
        }
    }
    public static void ResetQuest()  //重設任務函式
    {
        if (Instance == null)  //如果任務管理員不存在
        {
            return;  //跳出函式
        }
        foreach (Quest q in Instance.quests)  //循環過整個任務陣列
        {
            q.status = Quest.QUESTSTATUS.UNASSIGNED;  //將任務陣列內的所有任務狀態改為未指派
        }
    }
}
