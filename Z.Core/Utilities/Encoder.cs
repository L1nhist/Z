namespace Z.Core.Utilities;

public class Encoder
{
    public static Encoder ASII => new(1);

    public static Encoder UTF8 => new(2);

    public static Encoder UNICODE => new(3);

    public static Encoder BASE64 => new(4);

    public static Encoder HEX => new(0);

    private readonly int _type;

    private Encoder(int type)
    {
        _type = type;
    }

    public byte[] AsBytes(string text)
    {
        if (Util.IsEmpty(text)) return [];

        try
        {
            switch (_type)
            {
                case 1: return Encoding.ASCII.GetBytes(text);
                case 2: return Encoding.UTF8.GetBytes(text);
                case 3: return Encoding.Unicode.GetBytes(text);
                case 4: return Convert.FromBase64String(text);
                default:
                    var bytes = new byte[text.Length / 2];
                    for (var i = 0; i < bytes.Length; i++)
                    {
                        bytes[i] = Convert.ToByte(text.Substring(i * 2, 2), 16);
                    }
                    return bytes;
            }
        }
        catch { }

        return [];
    }

    public string AsString(byte[]? bytes)
    {
        if (Util.IsEmpty(bytes)) return "";

        try
        {
            return _type switch
            {
                1 => Encoding.ASCII.GetString(bytes),
                2 => Encoding.UTF8.GetString(bytes),
                3 => Encoding.Unicode.GetString(bytes),
                4 => Convert.ToBase64String(bytes),
                _ => new(bytes.SelectMany(b => b.ToString("X2").ToCharArray()).ToArray()),
            };
        }
        catch { }

        return "";
    }

    public string Change(string text, Encoder? encode = null)
        => Util.IsEmpty(text) ? "" : AsString((encode ?? UTF8).AsBytes(text));
}