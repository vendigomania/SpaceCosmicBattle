using GTranslatorAPI;
using System.Threading.Tasks;
using UnityEngine;

public class TranslationWrapper
{
    public static async Task<string> TranslateOnline(SystemLanguage to, string text, SystemLanguage from = SystemLanguage.English)
    {
        Translator translator = new Translator();
        Languages fromCode = LanguagesUtil.GetLanguageCodeFromLanguageName(from.ToString()).Value;
        Languages toCode = LanguagesUtil.GetLanguageCodeFromLanguageName(to.ToString()).Value;

        try
        {
            Translation res = await translator.TranslateAsync(fromCode, toCode, text);
            return res.TranslatedText;
        }
        catch
        {
            return "ERROR";
        }
    }
}
