using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundTypes
{
	Noise,
	Music
}

public class TuningFork : MonoBehaviour
{
	public BaseTrick trick;
	public SoundTypes sound;

	void OnTriggerEnter2D(Collider2D c)
	{
		trick.Unlocked = c.CompareTag(sound.ToString());
	}
}