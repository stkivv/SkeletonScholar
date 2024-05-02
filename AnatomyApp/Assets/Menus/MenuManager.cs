using UnityEngine;
using System.Collections.Generic;
#nullable enable

public class MenuManager : MonoBehaviour
{
    private SettingsMenuController settings;
    private LanguageMenuController language;

    private List<IMenuBase> menus;

    private void OnEnable()
    {
        language = FindObjectOfType<LanguageMenuController>();
        settings = FindObjectOfType<SettingsMenuController>();

        menus = new List<IMenuBase> { language, settings };
    }

    public void SwitchView<T>() where T : IMenuBase
    {
        menus.ForEach(m =>
        {
            if (m.GetType() == typeof(T))
            {
                m.ShowMenu();
            }
            else
            {
                m.HideMenu();
            }
        });
    }
}
