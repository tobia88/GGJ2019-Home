using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mom : BaseEntity
{
    public TuningFork refTrick;
    public Animator m_animator;

    public void Start() 
    {
        m_animator = GetComponentInChildren<Animator>();
        refTrick.onNoiseCb += (r) =>
        {
            var animName = (r) ? "anim_cry" : "anim_angry";
            m_animator.Play(animName);
        };
    }
    public void Update()
    {
        var dist = GameMng.Instance.stage.player.transform.position - transform.position;
        if (dist.x == 0)
            return;
        GetComponentInChildren<SpriteRenderer>().flipX = dist.x < 0;
    }
}