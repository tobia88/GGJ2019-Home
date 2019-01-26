﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luggage : BaseEntity {
	public bool onPick = false;
	public float grav = -10;
	public float force = 10;
	public Animator effAnimCtrl;
	private Vector2 m_vel;
	private Controller2D m_ctrl;
	private bool m_waitForGrounded;
	private CircleCollider2D m_noiseTrigger;

	public override void OnStart()
	{
		m_ctrl = GetComponent<Controller2D>();
		m_ctrl.OnInit();

		m_noiseTrigger = transform.Find("EffTrigger").GetComponent<CircleCollider2D>();
		m_noiseTrigger.enabled = false;	
	}

	public void OnThrow(float dx)
	{
		m_vel = new Vector2(1f * dx, 1.2f).normalized * force;
		m_waitForGrounded = true;
	}

	public override void OnUpdate()
	{
		if (!onPick)
		{
			m_vel.y += grav * Time.deltaTime;
			m_ctrl.Move(m_vel * Time.deltaTime);

			if (m_waitForGrounded && m_ctrl.ci.btm)
			{
				m_waitForGrounded = false;
				m_vel.x = 0;
				StartCoroutine(CreateNoises());
			}
			if (m_ctrl.ci.btm || m_ctrl.ci.top)
				m_vel.y = 0;

			if (m_ctrl.ci.right || m_ctrl.ci.left)
				m_vel.x = 0;
		}
	}

	private IEnumerator CreateNoises()
	{
		m_noiseTrigger.transform.SetParent(null);
		m_noiseTrigger.enabled = true;

		effAnimCtrl.transform.position = transform.position;
		effAnimCtrl.Play("anim_noise");

		yield return new WaitForSeconds(0.1f);
		m_noiseTrigger.transform.SetParent(transform);
		m_noiseTrigger.enabled = false;
	}
}
