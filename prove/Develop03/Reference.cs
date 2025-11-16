class Reference
{
    private string book;
    private int chapter;
    private int startVerse;
    private int endVerse;

    public string Book { get => book; }
    public int Chapter { get => chapter; }
    public int StartVerse { get => startVerse; }
    public int EndVerse { get => endVerse; }

    public Reference(string book, int chapter, int startVerse)
    {
        this.book = book;
        this.chapter = chapter;
        this.startVerse = startVerse;
        this.endVerse = startVerse;
    }

    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        this.book = book;
        this.chapter = chapter;
        this.startVerse = startVerse;
        this.endVerse = endVerse;
    }

    public string GetReference()
    {
        if (startVerse == endVerse)
            return $"{book} {chapter}:{startVerse}";
        else
            return $"{book} {chapter}:{startVerse}-{endVerse}";
    }
}
