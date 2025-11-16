class Word
{
    private string aWord;
    private bool hidden;

    public string TheWord { get => aWord; }
    public bool Hidden { get => hidden; }

    public Word(string aWord)
    {
        this.aWord = aWord;
        hidden = false;
    }

    public void Hide()
    {
        hidden = true;
    }
}