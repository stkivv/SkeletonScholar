using UnityEngine;

public class MeshController : MonoBehaviour
{
    private SelectionManager selectionManager;

    void Start()
    {
        selectionManager = FindObjectOfType<SelectionManager>();
    }

    public void OnMouseDown()
    {
        selectionManager.SelectObject(gameObject);
    }
}
