namespace WebApi.Helpers;

public static class Helper
{
    public static T Parser<T>(this string input, IFormatProvider formatProvider = null)
         where T : IParsable<T>
    {
        return T.Parse(input, formatProvider);   
    }
}
