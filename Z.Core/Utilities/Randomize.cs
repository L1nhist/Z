using System.Security.Cryptography;

namespace Z.Core.Utilities;

public class Randomize
{
    internal const string _charseeds = "UM7vgDhK1afd5ERrWs2tGHu3BwLYcmZ8jyn4pJFQz6bSBox9qCXTkeR0";
    private static int _last = -1;

    public static byte[] NewBytes(int length)
        => RandomNumberGenerator.GetBytes(length);

    public static int NewNumber(int min = int.MinValue, int max = int.MaxValue)
    {
        var result = _last;
        while (result == _last)
        {
            result = RandomNumberGenerator.GetInt32(min, max);
        }

        return (_last = result);
    }

    public static int NewNumber(int length = 1)
    {
        if (length < 1) return 0;

        var result = _last;
        var min = (int)Math.Pow(10, length - 1);
        var max = (int)Math.Pow(10, length) - 1;
        while (result == _last)
        {
            result = RandomNumberGenerator.GetInt32(min, max);
        }

        return result;
    }

    public static char NewChar()
        => _charseeds[NewNumber(0, _charseeds.Length - 1)];

    public static string NewString(int length)
    {
        var sb = new StringBuilder();
        while (sb.Length < length)
        {
            sb.Append(NewChar());
        }

        return sb.ToString();
    }

    public static string Replace(string text, params string[] replaces)
    {
        if (Util.IsEmpty(text)) return "";
        if (Util.IsEmpty(replaces)) return text;

        var arr = replaces.Distinct().ToArray();
        var len = arr.Length;
        var idx = 0;
        var rep = "";


        foreach (var s in arr)
        {
            if (!text.Contains(s)) continue;

            idx++;
            if (idx > len)
            {
                idx = 0;
                rep = "";
            }

            var r = NewChar().ToString();
            while (r == s || rep.Contains(r))
            {
                r = NewChar().ToString();
            }

            text = text.Replace(s, r);
        }

        return text;
    }

    public static string NewGuid(bool noSpec = false)
    {
        var code = Convert.ToBase64String(Guid.NewGuid().ToByteArray())[..^2];
        return noSpec ? Replace(code, "+", "/") : code.Replace("+", "%").Replace("/", "-");
    }
}