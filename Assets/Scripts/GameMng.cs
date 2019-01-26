using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng : MonoBehaviour
{
	public static GameMng Instance;
	public Camera cam;
	public float camMoveSpd = 5f;
	public Stage lastStage;
	public Stage stage;
	public Stage startStagePrefab;
	private Vector3 m_camTp;

	public void EnterNextStage(LevelTrigger t)
	{
		if(t.nextStage == null)
		{
			Debug.LogWarning("No next stage, ending");
			return;
		}
		lastStage = stage;
		lastStage.enabled = false;
		lastStage.stageEnded = true;
		lastStage.Destroy();

		stage = Instantiate(t.nextStage, Vector3.zero, Quaternion.identity);
		stage.OnStart();
	}

	private void Awake()
	{
		Instance = this;
		cam = Camera.main;
		m_camTp = cam.transform.position;
	}

	private void Start()
	{
		stage = FindObjectOfType<Stage>();

		if (stage == null)
			stage = Instantiate(startStagePrefab);

		stage.OnStart();
	}

	private void Update()
	{
		cam.transform.position = Vector3.MoveTowards(cam.transform.position, m_camTp, camMoveSpd * Time.deltaTime);
		if (stage != null)
			stage.OnUpdate();


		if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha1))
			EnterNextStage(stage.levelTrigger);
	}
}