using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
	public float jh = 4;
	public float apex = .4f;
	public Vector2 input;
	public float mvSpd = 6f;
	public float moveSpdWithLug = .5f;
	public bool onPickupLuggage;
	private float m_accAir = .2f;
	private float m_accGround = .1f;

	private float m_grav;
	private float m_jumpV;
	private Vector3 m_vel;
	private float m_velSmooth;
	private Luggage m_luggage;
	private Transform m_lSlot;

	Controller2D m_ctrl;

	public void OnStart()
	{
		m_ctrl = GetComponent<Controller2D>();
		m_ctrl.OnInit();

		m_lSlot = transform.Find("LSlot");

		m_grav = -(2 * jh) / Mathf.Pow(apex, 2);
		m_jumpV = Mathf.Abs(m_grav) * apex;
		print("Grav: " + m_grav + " Jump Vel: " + m_jumpV);
	}

	public void OnUpdate()
	{
		if (m_ctrl.ci.top || m_ctrl.ci.btm)
		{
			m_vel.y = 0;
		}

		input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		if (input.x != 0)
			transform.localScale = new Vector3(input.x, 1f, 1f);

		if (Input.GetKeyDown(KeyCode.X) && m_ctrl.ci.btm && !onPickupLuggage)
		{
			m_vel.y = m_jumpV;
		}

		if (onPickupLuggage)
		{
			if (Input.GetKeyDown(KeyCode.C))
			{
				ThrowLuggage();
			}
		}
		else
		{
			if (m_luggage != null && Input.GetKeyDown(KeyCode.C))
			{
				PickupLuggage();
			}

		}

		var cspd = (onPickupLuggage) ? mvSpd * moveSpdWithLug : mvSpd;
		var tvx = input.x * cspd;
		m_vel.x = Mathf.SmoothDamp(m_vel.x, tvx, ref m_velSmooth, (m_ctrl.ci.btm) ? m_accGround : m_accAir);
		m_vel.y += m_grav * Time.deltaTime;
		m_ctrl.Move(m_vel * Time.deltaTime);
	}

	private void ThrowLuggage()
	{

		m_luggage.onPick = false;

		m_luggage.transform.SetParent(null);
		m_luggage.transform.rotation = Quaternion.identity;

		onPickupLuggage = false;

		if (m_vel.x != 0 && input.y == -1)
		{
			// Put on ground
		}
		else
		{
			m_luggage.OnThrow(input.x);
		}
	}

	private void PickupLuggage()
	{
		onPickupLuggage = true;
		m_luggage.onPick = true;
		m_luggage.transform.SetParent(m_lSlot);
		m_luggage.transform.rotation = Quaternion.identity;
		m_luggage.transform.localPosition = Vector3.zero;
	}

	protected void OnTriggerEnter2D(Collider2D c)
	{
		if (c.CompareTag("Luggage"))
		{
			m_luggage = c.GetComponent<Luggage>();
		}
	}

	protected void OnTriggerExit2D(Collider2D c)
	{
		if (c.CompareTag("Luggage") && !onPickupLuggage)
		{
			m_luggage = null;
		}
	}
}