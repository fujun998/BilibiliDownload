using System.Text.Json;
using System.Text.RegularExpressions;

class UnderscoreNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        return Regex.Replace(name, @"[A-Z]+[a-z]+", new MatchEvaluator(Convert));
    }

    string Convert(Match match)
    {
        Console.WriteLine(match.Value);
        return match.Index > 0 ? match.Value.ToLower().Insert(0, "_") : match.Value.ToLower();
    }
}