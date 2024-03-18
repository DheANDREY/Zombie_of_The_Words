using System.Collections.Generic;
using UnityEngine;

public class WordManager :MonoBehaviour
{
	public static WordManager Instance;
	public SO_Word wordList;
	public List<Word> words = new List<Word>();
	public Transform parentTutor;
	public WordDisplay prefabWordTutor;

	private bool hasActiveWord;
	private Word activeWord;
	public static float wordTypedCount;
	public static float hitCount;
	public static float correctHitCount;

	private string tutorialWord;
	private List<Word> selectedWords = new List<Word>();
	private List<Word> splitTutorWord = new List<Word>();
	private void Awake()
	{
		Instance = this;
		wordTypedCount = 0;
		hitCount = 0;
		words = new List<Word>();
	}

	private WORD_SELECTION randomSelection;
	public void WordInit()
	{
		hasActiveWord = false;
		activeWord = null;
		wordTypedCount = 0;
		correctHitCount = 0;
		hitCount = 0;
		//WORD_SELECTION _selection = (WORD_SELECTION)Random.Range(0, 3);
		randomSelection = (WORD_SELECTION)Random.Range(0, 4); Debug.Log("Hasil Random=" + randomSelection);
		SelectWordEndless(randomSelection);		
	}

	private void SelectWordEndless(WORD_SELECTION _selection)
	{
		switch (_selection)
		{
			case WORD_SELECTION.COMMAND:
				selectedWords = wordList.words.commandWords.words;
				break;
			case WORD_SELECTION.UPPER:
				selectedWords = wordList.words.upperRowWords.words;
				break;
			case WORD_SELECTION.MID:
				selectedWords = wordList.words.midRowWords.words;
				break;
			case WORD_SELECTION.BOT:
				selectedWords = wordList.words.bottomRowWords.words;
				break;
			//default:
			//	selectedWords = wordList.words.commandWords.words;
			//	break;
		}
	}
	public void SelectWord(int index)
	{
		switch ((WORD_SELECTION)index)
		{
			case WORD_SELECTION.COMMAND:
				tutorialWord = wordList.words.commandWords.tutorialWord;
				break;
			case WORD_SELECTION.UPPER:
				tutorialWord = wordList.words.upperRowWords.tutorialWord;
				break;
			case WORD_SELECTION.MID:
				tutorialWord = wordList.words.midRowWords.tutorialWord;
				break;
			case WORD_SELECTION.BOT:
				tutorialWord = wordList.words.bottomRowWords.tutorialWord;
				break;
			default:
				tutorialWord = wordList.words.commandWords.tutorialWord;
				break;
		}
	}
	public void AddWord(WordDisplay _wordDisplay)
	{
		randomSelection = (WORD_SELECTION)Random.Range(0, 4); Debug.Log("Hasil Random=" + randomSelection);
		SelectWordEndless(randomSelection);
		Word word = new Word(selectedWords[Random.Range(0, selectedWords.Count-1)].word, _wordDisplay);//Random.Range(0, selectedWords.Count-1)
		//Debug.Log(selectedWords[Random.Range(0, selectedWords.Count-1)].word, _wordDisplay);//
		//Debug.Log(selectedWords);

		words.Add(word);
	}
	public void AddWordTutor(WordDisplay _wordDisplay)
	{
		Word word = new Word(tutorialWord, _wordDisplay);
		//Debug.Log(_wordDisplay.word);

		words.Add(word);
	}

	public void TypeLetter(char letter)
	{
		if (hasActiveWord)
		{
			if (activeWord.GetNextLetter() == letter)
			{
				
				correctHitCount++;
				activeWord.TypeLetter();
			}
			else
			{
				EventManager.DoOnFalse();
				//if (GameManager.Instance.mode == GAME_MODE.ENDLESS) return;
				//activeWord.TypeFalse();
			}
		}
		else
		{
			foreach (Word word in words)
			{
				if (word.GetNextLetter() == letter)
				{
					correctHitCount++;
					   activeWord = word;
					activeWord.SetFirstLayer();
					hasActiveWord = true;
					word.TypeLetter();
					break;
				}
			}
			if(!hasActiveWord)    EventManager.DoOnFalse();
			
		}

		if (hasActiveWord && activeWord.WordTyped())
		{
			hasActiveWord = false;
			words.Remove(activeWord);
		}
		
	}
	
	public float GetBestWPM(float _stopWatch)
	{
		return FormulaCalculator.GetWPM(wordTypedCount, _stopWatch);
	}
	public float GetBestEPM(float _stopWatch)
	{
		return FormulaCalculator.GetEPM(hitCount, _stopWatch);
	}
	public float GetAccuracy()
	{
		return FormulaCalculator.GetAccuracy(correctHitCount, hitCount);
	}
}