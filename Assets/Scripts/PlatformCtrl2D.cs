using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCtrl2D : RaycastController
{
    public LayerMask passengerMask;
    public List<PsgMovement> passengers = new List<PsgMovement>();
    public Dictionary<Transform, Controller2D> pDict =new Dictionary<Transform, Controller2D>();

    public void Move(Vector3 v)
    {
        UdInfo();
        CalcPsgMove(v);
        MovePassengers(true);
        transform.Translate(v);
        MovePassengers(false);
    }


    public void MovePassengers(bool moveB4)
    {
        foreach(var p in passengers)
        {
            if (!pDict.ContainsKey(p.t))
            {
                pDict.Add(p.t, p.t.GetComponent<Controller2D>());
            }

            if(p.moveB4me == moveB4)
            {
                pDict[p.t].Move(p.vel, p.standingOn);
            }
        }
    }

    private void CalcPsgMove(Vector3 v)
    {
        HashSet<Transform> tmpHash = new HashSet<Transform>();
        passengers = new List<PsgMovement>();

        if (v.y == 0)
            return;

        var sign = Mathf.Sign(v.y);
        var rl = Mathf.Abs(v.y) + SW;
        var origin = (v.y > 0) ? m_info.tl : m_info.bl;

        for (int i = 0; i < vRayAmt; i++)
        {
            var start = origin + Vector2.right * i * m_sv;
            var end = start + Vector2.up * rl * sign;

            var hit = Physics2D.Linecast(start, end, groundMask);
            if (hit && hit.distance != 0)
            {
                if (tmpHash.Contains(hit.transform))
                    continue;

                tmpHash.Add(hit.transform);
                var py = v.y - (hit.distance - SW) * sign;

                var newPsg = new PsgMovement();
                newPsg.t = hit.transform;
                newPsg.vel = Vector3.up * py;
                newPsg.standingOn = sign == 1;
                newPsg.moveB4me = true;
                passengers.Add(newPsg);
            }

            Debug.DrawLine(start, end, Color.red);
        }

        if (sign == -1)
        {
            rl = SW * 2f;

            for (int i = 0; i < vRayAmt; i++)
            {
                var start = m_info.tl + Vector2.right * (m_sv * i);
                var end = start + Vector2.up * rl;
                var hit = Physics2D.Linecast(start, end, passengerMask);

                if (hit && hit.distance != 0)
                {
                    if (tmpHash.Contains(hit.transform))
                        continue;

                    tmpHash.Add(hit.transform);
                    var py = v.y;

                    var newPsg = new PsgMovement();
                    newPsg.t = hit.transform;
                    newPsg.vel = Vector3.up * py;
                    newPsg.standingOn = sign == 1;
                    newPsg.moveB4me = true;
                    passengers.Add(newPsg);
                }
            }
        }
    }
}

public struct PsgMovement
{
    public Transform t;
    public Vector3 vel;
    public bool standingOn;
    public bool moveB4me;
}