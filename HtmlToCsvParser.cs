using System.Text.RegularExpressions;
using System.Linq;

namespace HtmlToCsv;

public static class HtmlToCsvParser
{
    private static List<(Regex Regex, string Replacement)> _toReplace = new List<(Regex Regex, string Replacement)>
    {
        (new Regex(@"\<\/?[a-z]*\>"), string.Empty), // Удаляет конструкции html <...> </...>
        (new Regex(@"&ndash;"), "-"), // Заменяет спецсимволы дефиса на "-"
    };   

    public static string[] Parse(string source)
    {
        foreach (var (regex, replacement) in _toReplace)
        {
            source = regex.Replace(source, replacement);
        }

        var result = source.Split(@"\n").Select(s => s.Replace(";", ",").Replace(" - ", ";")).ToArray();
        return result;
    }
}