using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCtrl : BaseTrick
{
    protected Vector3 m_startPos, m_endPos;
    protected Vector3 m_targetPos;

    public override void OnStart()
    {
        m_endPos = transform.position;
        transform.position = m_startPos = m_targetPos = transform.Find("StartPos").position;
    }

    public override void OnUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, m_targetPos, 0.1f);
    }

    protected override void SetUnlocked(bool v)
    {
        base.SetUnlocked(v);
        m_targetPos = (v) ? m_endPos : m_startPos;
        Debug.Log("Result here: " + v );
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if (Application.isPlaying)
        {
            Gizmos.DrawLine(m_startPos, m_endPos);
        }
        else
        {
            var t = transform.Find("StartPos");
            if (t == null)
                return;

            Gizmos.DrawLine(t.position, transform.position);

        }
    }
}