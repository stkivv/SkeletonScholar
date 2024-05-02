using System;

public static class Translator
{
    public static string GetTranslation(Language language, string objName)
    {
        foreach (Tuple<string,string> pair in language.Values)
        {
            if (pair.Item1 == objName) {
                return pair.Item2;
            };
        }
        // defaults to object name when matching translation was not found
        return objName;
    }

}