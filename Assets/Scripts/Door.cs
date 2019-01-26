using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : BaseTrick
{
    public Vector3 end;
    public float spd;

    private Vector3 m_tp;
    private Vector3 m_ws, m_we;
    private Animator m_animator;
    public override void OnStart()
    {
        m_ws = transform.position;
        m_we = transform.position + end;
        m_tp = m_ws;
        m_animator = GetComponentInChildren<Animator>();
    }

    protected override void SetUnlocked(bool v)
    {
        base.SetUnlocked(v);
        if (v)
        {
            StartCoroutine(DoorStartDelay());
        }
        else
        {
            m_tp = m_ws;
        }
    }

    IEnumerator DoorStartDelay()
    {
        m_animator.Play("anim_pass");
        yield return new WaitForSeconds(1f);
        m_tp = m_we;
    }

    public override void OnUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_tp, spd * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        var s = transform.position;
        var e = transform.position + end;
        if (Application.isPlaying)
        {
            s = m_ws;
            e = m_we;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(s, Vector3.one * .1f);
        Gizmos.DrawWireCube(e, Vector3.one * .1f);
        Gizmos.DrawLine(s, e);
    }
}