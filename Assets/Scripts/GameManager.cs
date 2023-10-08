using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	#region Singleton class: Level

	public static GameManager Instance;

	public void InitializeSingleton ()
	{
		if (Instance == null) {
			Instance = this;
		}
	}

	#endregion

	public int CurrentLevel { get; private set; }
	
	[SerializeField] ParticleSystem winFx;

	[Space]
	//remaining objects
	[HideInInspector] public int objectsInScene;
	//total objects at the beginning
	[HideInInspector] public int totalObjects;

	//the Objects parent
	[SerializeField] Transform objectsParent;

	[Space]
	[Header ("Materials & Sprites")]
	[SerializeField] Material groundMaterial;
	[SerializeField] Material objectMaterial;
	[SerializeField] Material obstacleMaterial;
	[SerializeField] SpriteRenderer groundBorderSprite;
	[SerializeField] SpriteRenderer groundSideSprite;
	[SerializeField] Image progressFillImage;

	[SerializeField] SpriteRenderer bgFadeSprite;

	[Space]
	[Header ("Colors")]
	[SerializeField] ColorSetup colorSetup;


	public void Initialize()
	{
		CurrentLevel = SaveManager.LoadLevel();
		InitLevel();
		CountObjects ();
		UpdateLevelColors ();
	}

	void InitLevel()
	{
		// objects & obstacles setup
		GameObject levelPrefab = PrefabsManager.Instance.GetLevelPrefab();
		GameObject levelObj = Instantiate(levelPrefab, Vector3.zero, Quaternion.identity, transform);
		objectsParent = levelObj.transform.Find("Objects");
		objectsParent.parent = transform;
		levelObj.transform.Find("Obstacles").parent = transform;
		Destroy(levelObj);
		
		// color setup
		colorSetup = PrefabsManager.Instance.GetColorSetup();
	}

	void CountObjects ()
	{
		//Count collectable white objects
		totalObjects = objectsParent.childCount;
		objectsInScene = totalObjects;
	}

	public void PlayWinFx ()
	{
		winFx.Play ();
	}

	public void LoadNextLevel ()
	{
		SaveManager.NextLevel();
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void RestartLevel ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	[ContextMenu ("Update Level Colors")]
	void UpdateLevelColors ()
	{
		groundMaterial.color = colorSetup.groundColor;
		groundSideSprite.color = colorSetup.sideColor;
		groundBorderSprite.color = colorSetup.bordersColor;

		obstacleMaterial.color = colorSetup.obstacleColor;
		objectMaterial.color = colorSetup.objectColor;

		progressFillImage.color = colorSetup.progressFillColor;

		Camera.main.backgroundColor = colorSetup.cameraColor;
		bgFadeSprite.color = colorSetup.fadeColor;
	}

	void OnValidate ()
	{
		UpdateLevelColors ();
	}
	
}