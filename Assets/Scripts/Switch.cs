using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : BaseTrick
{
    public Animator anim;
    public bool twoWay = false;

    protected override void SetUnlocked(bool v)
    {
        if (!twoWay && m_unlocked)
            return;

        base.SetUnlocked(v);
        anim.Play(v ? "anim_active" : "idle");
    }
}
