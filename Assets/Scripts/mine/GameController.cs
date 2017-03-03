using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject camera;				// get the MainCamera
	public Transform playerPos;				// get the player's transform
	public Transform[] checkPoints;			// get the transforms of all checkPoints
	public Transform[] rebornPoints;		// get the transforms of the rebornPoints
	public Transform[] previewPoints;

	public GameObject wall;					// the "PREFAB" wall which will be instantiated when player arrives the checkPoint
	public GameObject missionComplete;		// mission complete title

	public GameObject rebornDialog;


	private bool[] checkPointPass;			// decide whether checkPoints are passed
	private int curCP;						// current checkPoint
	private int enemySurvivedNum;			// how many enemies still alived
	private GameObject invisWall;			// the instantiation of the wall
	private float cameraWidth;				// the width size of the camera
	private bool inCheckPoint;				// whether the player in a checkPoint event

	private int pendingRebornPoint;
	private int pendingPreviewPoint;


	// Use this for initialization
	void Start () {
		int n = checkPoints.Length;
		checkPointPass = new bool[n];		
		curCP = 0;
		cameraWidth = camera.GetComponent<Camera> ().aspect * camera.GetComponent<Camera> ().orthographicSize;
		inCheckPoint = false;
		enemySurvivedNum = 0;
		pendingRebornPoint = 0;
		pendingPreviewPoint = 0;
	}
		
	// Update is called once per frame
	void Update () {
		// if a player is in a checkPoint and find that all enemies are destroyed
		// current checkPoint pass
		if (inCheckPoint && enemySurvivedNum == 0) {
			checkPointPass [curCP] = true;
			inCheckPoint = false;
		}

		// if there are remaining checkPoints
		if (curCP < checkPointPass.Length) {
			// if the player arrives the checkPoint and hasn't passed the checkPoint
			if (playerPos.position.x > checkPoints [curCP].position.x && !checkPointPass [curCP]) {
				// at first, set the enemyNum to be destroyed and stop the camera
				if (!inCheckPoint) {
					GetEnemyNum ();
					StopCameraMoving ();
				}
			}
			// if the player arrives the checkPoint and has passed the checkPoint
			else if (playerPos.position.x > checkPoints [curCP].position.x && checkPointPass [curCP]) {
				// make camera move
				ResumeCameraMoving ();
				// set current checkPoint to the next
				++curCP;
				// all checkPoints passed means the mission completed
				if (curCP >= checkPointPass.Length) {
					MissionComplete ();
				}
			}
		}

		if (pendingRebornPoint < rebornPoints.Length && playerPos.position.x > rebornPoints [pendingRebornPoint].position.x) {
			++pendingRebornPoint;
		}

		if (!inCheckPoint && pendingPreviewPoint < previewPoints.Length && playerPos.position.x > previewPoints [pendingPreviewPoint].position.x) {
			camera.GetComponent<MyCamera> ().previewMoving (previewPoints [pendingPreviewPoint].GetChild (0), 3.0f);
			++pendingPreviewPoint;
		}
	}

	public void disablePlayer(){
		playerPos.gameObject.GetComponent<PlayerControl> ().enabled = false;
	}

	public void enablePlayer(){
		playerPos.gameObject.GetComponent<PlayerControl> ().enabled = true;
	}


	private void MissionComplete(){
		// mission complete, disable all the control script
		playerPos.gameObject.GetComponent<PlayerControl> ().enabled = false;
		playerPos.gameObject.GetComponentInChildren<Gun> ().enabled = false;

		// set the text
		missionComplete.GetComponent<Text> ().text = "MissionComplete!";
	}

	private void SetInvisWall(){
		// the the position of this invisWall
		Vector3 tmp = new Vector3 (camera.transform.position.x + cameraWidth, 0, 0);
		// instantiate the invisWall
		invisWall = Instantiate (wall, tmp, wall.transform.rotation) as GameObject;

	}

	// get the enemyNum to be destroyed
	private void GetEnemyNum(){
		// now player is in the checkPoint event
		inCheckPoint = true;
		// get all the spawners in this checkPoint
		MySpawner[] list = checkPoints [curCP].GetComponentsInChildren<MySpawner> ();
		// sum the enemies up
		foreach (MySpawner sp in list){
			sp.StartSpawn ();
			enemySurvivedNum += sp.numToSpawn;
		}

	}


	// not used yet
	public void IncreaseEnemy(){
		++enemySurvivedNum;

	}

	// if enemy destroyed, decrease the enemyNum
	public void DecreaseEnemy(){
		--enemySurvivedNum;

	}

	// make the camera move
	void ResumeCameraMoving(){
		// destroy the invisWall
		if (invisWall != null) {
			Destroy (invisWall);
			invisWall = null;
		}
		camera.GetComponent<CameraFollow> ().fixedPos = false;
	}

	// set the invisWall and make the camera stop
	void StopCameraMoving(){
		SetInvisWall ();
		camera.GetComponent<CameraFollow> ().fixedPos = true;
	}

	public void RebornPlayer(){
		rebornDialog.SetActive (true);


	//	Transform tmp = rebornPoints [pendingRebornPoint - 1];
	//	playerPos.position = tmp.position;
	}

	public void StartReborn(){
		Transform tmp = rebornPoints [pendingRebornPoint - 1];
		playerPos.position = tmp.position;

		MonkeyControl mc = playerPos.gameObject.GetComponent<MonkeyControl> ();
		mc.enabled = true;
		mc.reset ();
		rebornDialog.SetActive (false);
	}




}
