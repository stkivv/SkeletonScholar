using System;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    private Camera perspCamera = null;
    public int defaultFOV = 60;

    void Update()
    {
        if (perspCamera == null)
        {
            perspCamera = GetComponent<Camera>();

        }
        float scroll = GetZoom();

        perspCamera.fieldOfView += scroll;
    }

    float GetZoom()
    {
        if (Application.isMobilePlatform)
        {
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                float zoom = prevTouchDeltaMag - touchDeltaMag;
                return zoom * 0.1f;
            }
            else
            {
                return 0f;
            }
        }
        else
        {
            return Input.GetAxis("Mouse ScrollWheel");
        }
    }

    public void Reset()
    {
        if (perspCamera)
        {
            perspCamera.fieldOfView = defaultFOV;
        }
    }
}
