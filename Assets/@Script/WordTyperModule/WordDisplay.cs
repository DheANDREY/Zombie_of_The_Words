using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
public class WordDisplay : MonoBehaviour
{
	public Color32 correct, incorrect,nextLetter,resetColor;
	public TextMeshProUGUI text;
	public bool isNeedChange;
	[ShowIf("isNeedChange")] public Canvas canvas;
	[ShowIf("isNeedChange")] public GameObject panelWord, parent;
	private int index;
	Color32[] newVertexColors;
	Color32 c0;
	private Camera playerCamera;
	private Transform target;

	private SoundManager soundManager;
    private void Start()
    {
		soundManager = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
		playerCamera = Camera.main;
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

		//EnemyController.instance.Initialize2(target);
		panelWord.SetActive(true);
    }	

	public static WordDisplay instance;
    private void Awake()
    {
		instance = this;
    }

    public void SetWord(string word)
	{
		text.text = word;
		index = 0;
	}
	public void ResetColorWord()
    {
		text.color = resetColor;
    }

	public EnemyController enemyController;
	public GameObject vfxHitted; public Transform[] posVFXSpawn;
	//[SerializeField] private VFXPooling vfxPooling;
	public void RemoveLetter()
	{
		if(isNeedChange)
		{
			text.text = text.text.Remove(0, 1);
			enemyController.ZomDamagedAnim();
			text.color = correct;
			//Instantiate(vfxHitted, posVFXSpawn[Random.Range(0, posVFXSpawn.Length - 1)].position, Quaternion.identity);
			//vfxPooling.SpawnVFX(0, posVFXSpawn[Random.Range(0, posVFXSpawn.Length - 1)].position, Quaternion.identity);
			ObjectPoolManager.SpawnObject(vfxHitted, posVFXSpawn[Random.Range(0, posVFXSpawn.Length - 1)].position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
		}
		else
		{
			c0 = text.color;
			if (text.textInfo.characterInfo[index].isVisible)
			{
				// Get the index of the material used by the current character.
				int materialIndex = text.textInfo.characterInfo[index].materialReferenceIndex;

				// Get the vertex colors of the mesh used by this text element (character or sprite).
				newVertexColors = text.textInfo.meshInfo[materialIndex].colors32;

				// Get the index of the first vertex used by this text element.
				int vertexIndex = text.textInfo.characterInfo[index].vertexIndex;

				// Only change the vertex color if the text element is visible.
				c0 = correct;

				newVertexColors[vertexIndex + 0] = c0;
				newVertexColors[vertexIndex + 1] = c0;
				newVertexColors[vertexIndex + 2] = c0;
				newVertexColors[vertexIndex + 3] = c0;

				// New function which pushes (all) updated vertex data to the appropriate meshes when using either the Mesh Renderer or CanvasRenderer.

			}

			index++;
			if(index<text.text.Length&& text.textInfo.characterInfo[index].isVisible)
			{
				// Get the index of the material used by the current character.
				int materialIndexN = text.textInfo.characterInfo[index].materialReferenceIndex;

				// Get the vertex colors of the mesh used by this text element (character or sprite).
				newVertexColors = text.textInfo.meshInfo[materialIndexN].colors32;

				// Get the index of the first vertex used by this text element.
				int vertexIndexN = text.textInfo.characterInfo[index].vertexIndex;

				newVertexColors[vertexIndexN + 0] = nextLetter;
				newVertexColors[vertexIndexN + 1] = nextLetter;
				newVertexColors[vertexIndexN + 2] = nextLetter;
				newVertexColors[vertexIndexN + 3] = nextLetter;
			}
		
			text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
		}
	}
	public void FalseLeter()
	{
		if (!isNeedChange)
		{
			c0 = text.color;
			if (text.textInfo.characterInfo[index].isVisible)
			{
				// Get the index of the material used by the current character.
				int materialIndex = text.textInfo.characterInfo[index].materialReferenceIndex;

				// Get the vertex colors of the mesh used by this text element (character or sprite).
				newVertexColors = text.textInfo.meshInfo[materialIndex].colors32;

				// Get the index of the first vertex used by this text element.
				int vertexIndex = text.textInfo.characterInfo[index].vertexIndex;

				// Only change the vertex color if the text element is visible.
				c0 = incorrect;

				newVertexColors[vertexIndex + 0] = c0;
				newVertexColors[vertexIndex + 1] = c0;
				newVertexColors[vertexIndex + 2] = c0;
				newVertexColors[vertexIndex + 3] = c0;

			}


			index++;
			if (index < text.text.Length && text.textInfo.characterInfo[index].isVisible)
			{
				// Get the index of the material used by the current character.
				int materialIndexN = text.textInfo.characterInfo[index].materialReferenceIndex;

				// Get the vertex colors of the mesh used by this text element (character or sprite).
				newVertexColors = text.textInfo.meshInfo[materialIndexN].colors32;

				// Get the index of the first vertex used by this text element.
				int vertexIndexN = text.textInfo.characterInfo[index].vertexIndex;

				newVertexColors[vertexIndexN + 0] = nextLetter;
				newVertexColors[vertexIndexN + 1] = nextLetter;
				newVertexColors[vertexIndexN + 2] = nextLetter;
				newVertexColors[vertexIndexN + 3] = nextLetter;
			}
			// New function which pushes (all) updated vertex data to the appropriate meshes when using either the Mesh Renderer or CanvasRenderer.
			text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
		}
	}

	public Animator enemyAnim;
	public bool isZomDead;	public CapsuleCollider offCollider;
	public static int zomDeadCounter;
	public void RemoveWord()
	{
		if (parent)
		{
			offCollider.enabled = false;
			isZomDead = true;
			//Debug.Log("ZomDead= "+isZomDead);
			offCollider.enabled = false;
			soundManager.PlaySound(SoundEnum.zomDead); zomDeadCounter++;Debug.Log("Add ZomCounter");	
			//Debug.Log("ZomDeadCounter= " + SpawnerZombie.zombieDieCounter);
			CharMoveController CMC = GameObject.FindAnyObjectByType<CharMoveController>();
			CMC.isDamaged = false;
			enemyAnim.SetTrigger("isDeath"); //Invoke("ZombieDie", 2.5f);
			panelWord.SetActive(false);
			EnemyController.instance.isAttackZom = false;	CharMoveController.instance.isDamaged = false;
			StartCoroutine(ReturnToPoolDelayed(3.5f));
			SpawnerZombie.instance.zombieCounter--;	
			//StartCoroutine(ReturnToPoolDelayed(3.5f));
		}
		else
		{
			Debug.Log("Done");
			isZomDead = false;
		}
	}
	public GameObject thisZombie;
	private IEnumerator ReturnToPoolDelayed(float delay)
	{		
		yield return new WaitForSeconds(delay);	Debug.Log("ReturnZombie");
		if (thisZombie != null)
		{
			// Dapatkan komponen EnemyController dari thisZombie
			EnemyController enemyController = thisZombie.GetComponent<EnemyController>();

			// Pastikan enemyController tidak null
			if (enemyController != null)
			{
				// Panggil metode BackToPool pada enemyController
				enemyController.BackToPool();
			}
			else
			{
				Debug.LogWarning("EnemyController component not found on thisZombie.");
			}
		}
		else
		{
			Debug.LogWarning("thisZombie is null.");
		}
		//ObjectPoolManager.ReturnObjectToPool(thisZombie);
	}

	public void sfxDead()
    {
		soundManager.PlaySound(SoundEnum.zomDead); Debug.Log("SFX Dead");
	}
}
