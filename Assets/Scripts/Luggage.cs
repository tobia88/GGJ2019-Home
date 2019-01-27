using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luggage : BaseEntity {
	public bool onPick = false;
	public float grav = -10;
	public float force = 10;
	public float moveSpdWithLug = .7f;
	public Animator effAnimCtrl;
	private Vector2 m_vel;
	private Controller2D m_ctrl;
	private bool m_waitForGrounded;
	private CircleCollider2D m_noiseTrigger;

	public void Start()
	{
		m_ctrl = GetComponent<Controller2D>();
		m_ctrl.OnInit();

		m_noiseTrigger = transform.Find("EffTrigger").GetComponent<CircleCollider2D>();
		m_noiseTrigger.enabled = false;	
	}

	public void Reset()
	{
		onPick = false;
		GetComponent<Collider2D>().enabled = true;
		m_waitForGrounded = false;	
		m_noiseTrigger.enabled = false;
		m_vel = Vector2.zero;
	}

	public void OnThrow(float dx)
	{
		Reset();
		m_vel = new Vector2(1f * dx, 1.2f).normalized * force;
		m_waitForGrounded = true;
	}

	public void Update()
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

	public void PlayMelody()
	{
		StartCoroutine(CreateMelody());
	}

	private IEnumerator CreateMelody()
	{
		m_noiseTrigger.tag = "Music";
		m_noiseTrigger.enabled = true;
		effAnimCtrl.transform.position = transform.position;
		effAnimCtrl.Play("anim_melody");

		yield return new WaitForSeconds(0.1f);
		m_noiseTrigger.transform.SetParent(transform);
		m_noiseTrigger.enabled = false;
	}

	private IEnumerator CreateNoises()
	{
		m_noiseTrigger.tag = "Noise";
		m_noiseTrigger.enabled = true;

		effAnimCtrl.transform.position = transform.position;
		effAnimCtrl.Play("anim_noise");

		yield return new WaitForSeconds(0.1f);
		m_noiseTrigger.transform.SetParent(transform);
		m_noiseTrigger.enabled = false;
	}
}
