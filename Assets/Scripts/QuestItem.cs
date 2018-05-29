using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    public string questName = null;
    public AudioSource audioSource = null;
    public SpriteRenderer spriteRenderer = null;
    public CircleCollider2D circleCollider = null;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
    private void Start()
    {
        gameObject.SetActive(false);
        if(QuestManager.GetQuestStatus(questName) == Quest.QUESTSTATUS.ASSIGNED)
        {
            gameObject.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            return;
        }
        if(!gameObject.activeSelf)
        {
            return;
        }
        QuestManager.SetQuestStatus(questName, Quest.QUESTSTATUS.COMPLETE);
        spriteRenderer.enabled = circleCollider.enabled = false;
        if(audioSource != null)
        {
            audioSource.Play();
        }
    }
}
