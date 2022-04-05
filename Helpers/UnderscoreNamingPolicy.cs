using System.Text.Json;
using System.Text.RegularExpressions;

class UnderscoreNamingPolicy : JsonNamingPolicy
{
    static readonly MatchEvaluator convertEvaluator = new(Convert);

    static string Convert(Match match)
    {
        return match.Index > 0 ? match.Value.ToLower().Insert(0, "_") : match.Value.ToLower();
    }

    public override string ConvertName(string name)
    {
        return Regex.Replace(name, @"[A-Z]+[a-z]+", convertEvaluator);
    }
}