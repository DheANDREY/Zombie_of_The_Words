using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
public class WordDisplay : MonoBehaviour
{
	public Color32 correct, incorrect,nextLetter;
	public TextMeshProUGUI text;
	public bool isNeedChange;
	[ShowIf("isNeedChange")] public Canvas canvas;
	[ShowIf("isNeedChange")] public GameObject parent;
	private int index;
	Color32[] newVertexColors;
	Color32 c0;
	public void SetWord(string word)
	{
		text.text = word;
		index = 0;
	}

	public void RemoveLetter()
	{
		if(isNeedChange)
		{
			text.text = text.text.Remove(0, 1);
			text.color = correct;
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
	public void RemoveWord()
	{
		if (parent)
		{
			Destroy(parent);
		}
		else
		{
			Debug.Log("Done");
		}
	}
}
