[System.Serializable]
public class Word
{
	public string word;
	internal int typeIndex;

	WordDisplay display;
	private string wordChecker;
	public Word(string _word, WordDisplay _display)
	{
		word = _word;
		wordChecker = word;
		typeIndex = 0;

		display = _display;
		display.SetWord(word);
	}

	public void SetFirstLayer()
	{
		if(display.canvas)
		display.canvas.sortingOrder = 1;

		if(display.parent!=null)
		{
			EventManager.DoOnAddTarget(display.parent.GetComponent<EnemyController>());
		}
	}

	public char GetNextLetter()
	{
		if(GameManager.Instance.mode== GAME_MODE.ENDLESS)
		{
			return wordChecker.ToLower()[typeIndex];
		}
		return wordChecker[typeIndex];
	}

	public void TypeLetter()
	{
		EventManager.DoOnType();
		typeIndex++;
		display.RemoveLetter();
	}

	public void TypeFalse()
	{
		typeIndex++;
		display.FalseLeter();
	}

	public bool WordTyped()
	{
		bool wordTyped = (typeIndex >= word.Length);
		if (wordTyped)
		{
			WordManager.wordTypedCount += 1;
			display.RemoveWord();
		}
		return wordTyped;
	}
}