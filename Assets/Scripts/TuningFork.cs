using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundTypes
{
	Noise,
	Music
}

public class TuningFork : BaseEntity
{
	public BaseTrick trick;
	public SoundTypes sound;
	public Animator faceAnim;
	public Animator tfAnim;
	public System.Action<bool> onNoiseCb;

	void OnTriggerEnter2D(Collider2D c)
	{
		tfAnim.SetTrigger("Active");
		var result = c.CompareTag(sound.ToString());
		print("Affect Tuning: " + gameObject.name + ", Result: " + result);
		if (trick != null)
			trick.Unlocked = result;

		var anim = (result) ? "anim_happy" : "anim_sad";
		faceAnim.Play(anim);

		if (onNoiseCb != null)
			onNoiseCb(result);
	}
}