using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ColInfo
{
	public bool top, btm;
	public bool left, right;
	public void Reset()
	{
		top = btm = left = right = false;
	}
}

public class Controller2D : RaycastController
{
	public ColInfo ci;

	public void Move(Vector2 v, bool standing = false)
	{
		UdInfo();
		ci.Reset();
		if (v.x != 0) HColTest(ref v);
		if (v.y != 0) VColTest(ref v);
		transform.Translate(v);

		if (standing)
			ci.btm = true;
	}

	protected void HColTest(ref Vector2 v)
	{
		var sign = Mathf.Sign(v.x);
		var rl = Mathf.Abs(v.x) + SW;
		var origin = (v.x > 0) ? m_info.br : m_info.bl;

		for (int i = 0; i < hRayAmt; i++)
		{
			var start = origin + Vector2.up * i * m_sh;
			var end = start + Vector2.right * rl * sign;

			var hit = Physics2D.Linecast(start, end, groundMask);
			if (hit)
			{
				if (hit.collider.CompareTag("Through"))
					continue;

				v.x = (hit.distance - SW) * sign;
				rl = hit.distance;

				ci.left = sign == -1;
				ci.right = sign == 1;
			}

			Debug.DrawLine(start, end, Color.red);
		}

	}

	protected void VColTest(ref Vector2 v)
	{
		var sign = Mathf.Sign(v.y);
		var rl = Mathf.Abs(v.y) + SW;
		var origin = (v.y > 0) ? m_info.tl : m_info.bl;

		for (int i = 0; i < vRayAmt; i++)
		{
			var start = origin + Vector2.right * i * m_sv;
			var end = start + Vector2.up * rl * sign;

			var hit = Physics2D.Linecast(start, end, groundMask);
			Collider2D c = null;
			if (hit)
			{
				if (hit.collider.CompareTag("Through") && sign == 1)
					continue;

				if (hit.collider.CompareTag("Switch") && c != hit.collider)
				{
					c = hit.collider;
					var s = hit.collider.GetComponent<Switch>();
					s.Unlocked = !s.Unlocked;
				}

				v.y = (hit.distance - SW) * sign;
				rl = hit.distance;

				ci.btm = sign == -1;
				ci.top = sign == 1;
			}

			Debug.DrawLine(start, end, Color.red);
		}

	}
}