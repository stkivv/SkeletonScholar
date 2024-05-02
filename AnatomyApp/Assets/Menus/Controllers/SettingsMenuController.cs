using UnityEngine.UIElements;
using UnityEngine;
#nullable enable


public class SettingsMenuController : MonoBehaviour, IMenuBase
{
    private VisualElement root;

    private Button languageSelectBtn;
    private MenuManager menuManager;

    private SettingsManager settingsManager;

    private void OnEnable()
    {
        menuManager = FindObjectOfType<MenuManager>();
        settingsManager = FindObjectOfType<SettingsManager>();

        root = GetComponent<UIDocument>().rootVisualElement;
        root.style.display = DisplayStyle.None;

        languageSelectBtn = root.Q<Button>("LanguageSelectBtn");
        languageSelectBtn.text = settingsManager.SelectedLanguage.Name;
        languageSelectBtn.clicked += menuManager.SwitchView<LanguageMenuController>;

        Button closeBtn = root.Q<Button>("CloseBtn");
        closeBtn.clicked += HideMenu;
    }

    public void ShowMenu()
    {
        languageSelectBtn.text = settingsManager.SelectedLanguage.Name;
        root.style.display = DisplayStyle.Flex;
    }

    public void HideMenu()
    {
        root.style.display = DisplayStyle.None;

    }
}
