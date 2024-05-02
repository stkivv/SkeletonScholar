using UnityEngine.UIElements;
using UnityEngine;
using System.Linq;

public class LanguageMenuController : MonoBehaviour, IMenuBase
{
    private VisualElement root;
    private VisualElement selectionWrapper;
    private MenuManager menuManager;
    private SettingsManager settingsManager;

    private string currentSelectedKey;

    private void OnEnable()
    {
        menuManager = FindObjectOfType<MenuManager>();
        settingsManager = FindObjectOfType<SettingsManager>();
        currentSelectedKey = settingsManager.SelectedLanguage.Key;

        root = GetComponent<UIDocument>().rootVisualElement;
        root.style.display = DisplayStyle.None;

        selectionWrapper = root.Q<VisualElement>("SelectionWrapper");
        AddButtons();

        VisualElement btnsWrapper = root.Q<VisualElement>("ButtonsWrapper");

        Button closeBtn = btnsWrapper.Q<Button>("CloseBtn");
        closeBtn.clicked += menuManager.SwitchView<SettingsMenuController>;

        Button saveBtn = btnsWrapper.Q<Button>("SaveBtn");
        saveBtn.clicked += () =>
        {
            settingsManager.SetLanguage(currentSelectedKey);
            menuManager.SwitchView<SettingsMenuController>();
        };
    }

    private void AddButtons()
    {
        selectionWrapper.Clear();
        foreach (var l in settingsManager.Languages)
        {
            Button btn = new(() => { currentSelectedKey = l.Key; AddButtons(); })
            {
                text = l.Name,
            };

            btn.style.height = 50;
            btn.style.borderTopWidth = 0;
            btn.style.borderBottomWidth = 0;
            btn.style.borderLeftWidth = 0;
            btn.style.borderRightWidth = 0;
            btn.style.backgroundColor = new StyleColor(ColorUtility.TryParseHtmlString("#515158", out Color color) ? color : Color.gray);
            btn.style.color = Color.white;
            btn.style.marginBottom = 0;
            btn.style.marginLeft = 0;
            btn.style.marginRight = 0;
            btn.style.marginTop = 0;

            if (l.Key == currentSelectedKey)
            {
                btn.style.backgroundColor = new StyleColor(ColorUtility.TryParseHtmlString("#4C75A6", out Color color2) ? color2 : Color.gray);
                VisualElement checkmark = new();
                checkmark.style.backgroundImage = new StyleBackground(Resources.Load<Texture2D>("check@2x"));
                checkmark.style.width = 24;
                checkmark.style.height = 24;
                checkmark.style.alignSelf = Align.FlexEnd;
                btn.Add(checkmark);
            }

            if (l == settingsManager.Languages[0])
            {
                btn.style.borderTopLeftRadius = 16;
                btn.style.borderTopRightRadius = 16;
            }
            if (l == settingsManager.Languages.Last())
            {
                btn.style.borderBottomLeftRadius = 16;
                btn.style.borderBottomRightRadius = 16;
            }
            selectionWrapper.Add(btn);
        }
    }

    public void ShowMenu()
    {
        currentSelectedKey = settingsManager.SelectedLanguage.Key;
        AddButtons();
        root.style.display = DisplayStyle.Flex;
    }

    public void HideMenu()
    {
        root.style.display = DisplayStyle.None;

    }
}