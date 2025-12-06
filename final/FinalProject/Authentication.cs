class Authentication
{
    private Dictionary<string, string> authorizedUsers = new Dictionary<string, string>()
    {
        { "Alex", "0000" }
    };

    public bool Authenticate(string username, string password)
    {
        if (authorizedUsers.ContainsKey(username) && authorizedUsers[username] == password)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
