using UnityEngine;
# nullable enable

public class SelectionManager : MonoBehaviour
{
    public Material? DefaultMaterial;
    public Material? SelectedMaterial;

    public delegate void SelectedObjectChangedEventHandler();
    public event SelectedObjectChangedEventHandler OnSelectedObjectChanged;

    private GameObject _selectedObject;
    public GameObject? SelectedObject
    {
        get { return _selectedObject; }
        set
        {
            _selectedObject = value;
            OnSelectedObjectChanged?.Invoke();
        }
    }

    public void SelectObject(GameObject newSelectedObject)
    {
        if (newSelectedObject == SelectedObject)
        {
            DeselectObject();
            return;
        }
        
        if (SelectedObject != null) DeselectObject();

        Material[] materials = new Material[2] { DefaultMaterial!, SelectedMaterial! };

        newSelectedObject.GetComponent<Renderer>().materials = materials;
        SelectedObject = newSelectedObject;
    }

    public void DeselectObject()
    {
        if (SelectedObject != null)
        {
            Material[] materials = new Material[1] { DefaultMaterial! };
            SelectedObject.GetComponent<Renderer>().materials = materials;
        }
        SelectedObject = null;
    }
}