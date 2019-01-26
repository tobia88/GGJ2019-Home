using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct rInfo
{
	public Vector2 tl, bl, tr, br;

	public void Reset()
	{
		tl = bl = tr = br = Vector2.zero;
	}
}

public class RaycastController : MonoBehaviour
{
	public const float SW = .0015f;
	public const float dbr = .025f;

	protected BoxCollider2D m_col;
	protected rInfo m_info;
	protected float m_sh, m_sv;
	public LayerMask groundMask;

	[HideInInspector]
	public int hRayAmt, vRayAmt;

	public void OnInit()
	{
		m_col = GetComponent<BoxCollider2D>();
		hRayAmt = Mathf.RoundToInt(m_col.size.y / dbr);
		vRayAmt = Mathf.RoundToInt(m_col.size.x / dbr);
		m_sh = m_col.size.y / (hRayAmt - 1);
		m_sv = m_col.size.x / (vRayAmt - 1);
	}


	public void UdInfo()
	{
		var bound = m_col.bounds;
		bound.Expand(-2 * SW);

		var min = m_col.bounds.min;
		var max = m_col.bounds.max;

		m_info.bl = new Vector2(min.x, min.y);
		m_info.br = new Vector2(max.x, min.y);
		m_info.tl = new Vector2(min.x, max.y);
		m_info.tr = new Vector2(max.x, min.y);
	}
}