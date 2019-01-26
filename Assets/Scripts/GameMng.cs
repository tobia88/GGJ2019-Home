using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng : MonoBehaviour
{
	public static GameMng Instance;
	public Player player;
	public Luggage luggage;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		player = FindObjectOfType<Player>();
		player.OnStart();

		luggage = FindObjectOfType<Luggage>();
		luggage.OnStart();
	}

	private void Update()
	{
		player.OnUpdate();
		luggage.OnUpdate();
	}
}