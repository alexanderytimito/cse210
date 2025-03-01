using System;

class Scripture
{
    private List<Word> words;
    private bool allWordsHidden;
    public bool AllWordsHidden
    {
        get => allWordsHidden;
    }
    private Reference reference = new Reference("Moroni", 10, 32);

    public Scripture()
    {
        string verse =
            "Yea, come unto Christ, and be perfected in him, and deny yourselves of all ungodliness; "
            + "and if ye shall deny yourselves of all ungodliness, and love God with all your might,"
            + " mind and strength, then is his grace sufficient for you";

        words = new List<Word>();

        string[] splitText = verse.Split(' ');

        for (int i = 0; i < splitText.Length; i++)
        {
            words.Add(new Word(splitText[i]));
        }
    }

    public void HideRandomWords(int count)
    {
        var visibleWords = words.Where(word => !word.Hidden).ToList();
        /*
        List<Word> visibleWords = new List<Word>();

        foreach (Word word in words){
            if (!word.Hidden){
                visibleWords.Add(word);
            }
        }
        */

        //If there are fewer words than the count
        if (visibleWords.Count <= count)
        {
            foreach (var word in visibleWords)
            {
                word.Hide();
            }
            allWordsHidden = true;
            return;
        }

        Random rand = new Random();
        List<int> indices = new List<int>();

        while (indices.Count <= count)
        {
            int index = rand.Next(visibleWords.Count);

            if (!indices.Contains(index))
                indices.Add(index);
        }

        foreach (int index in indices)
        {
            visibleWords[index].Hide();
        }
    }

    public string GetVisibleWords()
    {
        string visibleWords = "";
        foreach (Word word in words)
        {
            if (word.Hidden)
            {
                visibleWords += "______";
            }
            else
            {
                visibleWords += word.TheWord;
            }

            visibleWords += " ";
        }
        return visibleWords;
    }

    public string GetFullReference()
    {
        return reference.GetReference();
    }
}
