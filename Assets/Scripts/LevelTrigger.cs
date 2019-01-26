using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : BaseEntity
{
    public Stage nextStage;
    public BoxCollider2D stageBlocker;
    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.CompareTag("Player"))
        {
            GameMng.Instance.EnterNextStage(this);
        }
    }
}
