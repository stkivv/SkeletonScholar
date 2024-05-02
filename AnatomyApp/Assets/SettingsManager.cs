using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
#nullable enable

public class SettingsManager : MonoBehaviour
{
    public Language SelectedLanguage { get; set; }
    public List<Language> Languages;

    private void OnEnable()
    {
        Languages = new List<Language>();
        GetAllLanguages();
        SetLanguage(GetSavedLanguageSelection());
        Application.targetFrameRate = 60;
    }

    private string GetSavedLanguageSelection()
    {
        return PlayerPrefs.GetString("Language", "ENG");
    }

    private void SaveLanguage(string key)
    {
        PlayerPrefs.SetString("Language", key);
    }

    public void SetLanguage(string key)
    {
        foreach (var language in Languages)
        {
            if (language.Key == key)
            {
                SelectedLanguage = language;
                SaveLanguage(language.Key);
                return;
            }
        }
        if (Languages.Count >= 0)
        {
            SelectedLanguage = Languages[0];
            SaveLanguage(Languages[0].Key);
        }
        else
        {
            throw new Exception("No languages!");
        }
    }

    private void GetAllLanguages()
    {
        var assetBundlePath = Path.Combine(Application.streamingAssetsPath, "AssetBundles", "languages");
        var assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
        string[] assetNames = assetBundle.GetAllAssetNames();
        foreach (string assetName in assetNames)
        {
            if (assetName.EndsWith(".csv"))
            {
                TextAsset csvAsset = assetBundle.LoadAsset<TextAsset>(assetName);
                string[] lines = csvAsset.text.Split('\n');

                if (lines.Count() < 2)
                {
                    continue; //file too short, skip it
                }

                Language language = new()
                {
                    Key = lines[0].Split(";")[1],
                    Name = lines[1].Split(";")[1],
                    Values = new List<Tuple<string, string>>()
                };
                for (int i = 2; i < lines.Count(); i++)
                {
                    if (String.IsNullOrEmpty(lines[i])) continue;

                    string[] v = lines[i].Split(";");
                    language.Values.Add(new Tuple<string, string>(v[0], v[1]));
                }
                Languages.Add(language);
            }
        }
    }
}

public class Language
{
    public string Key { get; set; }
    public string Name { get; set; }
    public List<Tuple<string, string>> Values { get; set; }
}
