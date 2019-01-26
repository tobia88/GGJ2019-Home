using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng : MonoBehaviour
{
	public static GameMng Instance;
	public Player player;
	public Luggage luggage;
	public BaseTrick[] tricks;
	public TuningFork[] forks;
	public BaseEntity[] npcs;

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

		tricks = FindObjectsOfType<BaseTrick>();
		foreach(var t in tricks)
			t.OnStart();

		forks = FindObjectsOfType<TuningFork>();
		foreach(var f in forks)
			f.OnStart();

		foreach(var n in npcs)
			n.OnStart();
	}

	private void Update()
	{
		player.OnUpdate();
		luggage.OnUpdate();
		
		foreach (var t in tricks)
			t.OnUpdate();

		foreach(var f in forks)
			f.OnUpdate();
			
		foreach(var n in npcs)
			n.OnUpdate();
	}
}