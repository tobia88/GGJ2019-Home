using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformCtrl2D))]
public class PlatformCtrl : BaseTrick
{
    public Vector3 start;
    public float spd;

    private Vector3 m_tp;
    private Vector3 m_ws, m_we;
    protected PlatformCtrl2D ctrl;

    public void Start()
    {
        ctrl = GetComponent<PlatformCtrl2D>();
        ctrl.OnInit();

        m_we = transform.position;
        m_ws = transform.position + start;
        transform.position = m_tp = m_ws;
    }

    public void Update()
    {
        var tp = Vector3.MoveTowards(transform.position, m_tp, spd * Time.deltaTime);
        var dist = tp - transform.position;
        ctrl.Move(dist);
    }

    protected override void SetUnlocked(bool v)
    {
        base.SetUnlocked(v);
        m_tp = (v) ? m_we : m_ws;
        Debug.Log("Result here: " + v );
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        var s = transform.position;
        var e = transform.position + start;
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