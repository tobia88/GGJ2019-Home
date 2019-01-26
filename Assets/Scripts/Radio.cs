using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : BaseEntity
{
    public Note prefab;
    public Vector3[] path;
    public float spd = 3f;
    public float interval = 1f;
    private float m_delay;
    private Vector3[] m_worldPaths;

    public override void OnStart()
    {
        m_worldPaths = new Vector3[path.Length];
        for (int i = 0; i < path.Length; i++)
        {
            m_worldPaths[i] = transform.position + path[i];
        }
    }

    public override void OnUpdate()
    {
        m_delay -= Time.deltaTime;
        if (m_delay <= 0)
        {
            SpawnPrefab();
            m_delay = interval;
        }
    }

    private void SpawnPrefab()
    {
        var n = Instantiate<Note>(prefab);
        n.paths = m_worldPaths;
        n.spd = spd;
        GameMng.Instance.stage.notes.Add(n);
        n.OnStart();
        Debug.Log("Spawning");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (Application.isPlaying)
        {
            if (m_worldPaths == null)
                return;

            for(int i = 0; i < m_worldPaths.Length - 1; i++)
            {
                var start = m_worldPaths[i];
                var end = m_worldPaths[i + 1];
                Gizmos.DrawWireCube(start, Vector3.one * 0.1f);
                Gizmos.DrawWireCube(end, Vector3.one * 0.1f);
                Gizmos.DrawLine(start, end);
            }
        }
        else
        {
            for (int i = 0; i < path.Length - 1; i++)
            {
                var start = transform.position + path[i];
                var end = transform.position + path[i + 1];
                Gizmos.DrawWireCube(start, Vector3.one * 0.1f);
                Gizmos.DrawWireCube(end, Vector3.one * 0.1f);
                Gizmos.DrawLine(start, end);
            }
        }
    }
}
