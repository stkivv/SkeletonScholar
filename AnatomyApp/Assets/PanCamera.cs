using UnityEngine;

public class PanCamera : MonoBehaviour
{
    private Vector3 defaultPos;
    void OnEnable()
    {
        defaultPos = transform.position;
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {

            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 differenceZero = touchZero.position - (touchZero.position - touchZero.deltaPosition);

            Vector2 differenceOne = touchOne.position - (touchOne.position - touchOne.deltaPosition);

            Vector2 avgDifference = (differenceOne + differenceZero) / 2;

            transform.Translate(-avgDifference / 250);

        }
    }

    public void Reset()
    {
        transform.position = defaultPos;
    }
}
