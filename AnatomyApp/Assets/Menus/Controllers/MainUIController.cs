using UnityEngine;
using UnityEngine.UIElements;

public class MainUIController : MonoBehaviour
{
    private SelectionManager selectionManager;
    private MenuManager menuManager;
    private SettingsManager settingsManager;
    private RotateModel rotateModel;
    private PanCamera panCamera;
    private ZoomCamera zoomCamera;

    private Label anatomyLabel;
    private Button settingsButton;
    private Button resetButton;

    private void OnEnable()
    {
        selectionManager = FindObjectOfType<SelectionManager>();
        menuManager = FindObjectOfType<MenuManager>();
        settingsManager = FindObjectOfType<SettingsManager>();
        rotateModel = FindObjectOfType<RotateModel>();
        panCamera = FindObjectOfType<PanCamera>();
        zoomCamera = FindObjectOfType<ZoomCamera>();

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        VisualElement wrapper = root.Q<VisualElement>("AnatomyLabelWrapper");
        anatomyLabel = wrapper.Q<Label>("AnatomyLabel");

        VisualElement sideButtons = root.Q<VisualElement>("SideButtons");
        settingsButton = sideButtons.Q<Button>("SettingsBtn");
        settingsButton.clicked += menuManager.SwitchView<SettingsMenuController>;

        resetButton = sideButtons.Q<Button>("ResetBtn");
        resetButton.clicked += rotateModel.Reset;
        resetButton.clicked += panCamera.Reset;
        resetButton.clicked += zoomCamera.Reset;
        resetButton.clicked += selectionManager.DeselectObject;

        UpdateAnatomyLabel();

        selectionManager.OnSelectedObjectChanged += UpdateAnatomyLabel;
    }

    private void UpdateAnatomyLabel()
    {
        if (selectionManager.SelectedObject == null)
        {
            anatomyLabel.text = Translator.GetTranslation(settingsManager.SelectedLanguage, "None selected");
        }
        else
        {
            anatomyLabel.text = Translator.GetTranslation(settingsManager.SelectedLanguage, selectionManager.SelectedObject.name);
        }
    }
}