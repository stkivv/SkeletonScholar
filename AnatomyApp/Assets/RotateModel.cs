using System;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    private float? lastUpdatePosX = null;
    private float? lastUpdatePosY = null;

    private bool dragging = false;

    public float SpeedMultiplier = 0.5f;
    public float MaxRotationAmount = 10f;

    private float xRot = 0f;
    private float yRot = 0f;

    private void Update()
    {
        if (Input.touchCount > 1) return;
        if (Input.GetMouseButtonDown(0)) dragging = true;
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            lastUpdatePosX = null;
            lastUpdatePosY = null;
        }
        if (!dragging) return;

        lastUpdatePosX ??= Input.mousePosition.x;
        lastUpdatePosY ??= Input.mousePosition.y;

        float xWalk = Input.mousePosition.x - (float)lastUpdatePosX;
        float yWalk = Input.mousePosition.y - (float)lastUpdatePosY;

        xWalk = NormalizeWalk(xWalk, MaxRotationAmount);
        yWalk = NormalizeWalk(yWalk, MaxRotationAmount);

        Rotate(xWalk, yWalk);

        lastUpdatePosX = Input.mousePosition.x;
        lastUpdatePosY = Input.mousePosition.y;
    }

    private void Rotate(float amountX, float amountY)
    {
        yRot += amountX * SpeedMultiplier;
        xRot += -amountY * SpeedMultiplier * 0.5f;
        transform.rotation = Quaternion.Euler(xRot, yRot, 0f);
    }

    private float NormalizeWalk(float amount, float max)
    {
        while (Math.Abs(amount) > max)
        {
            if (amount > 0)
            {
                amount -= 0.01f;
            }
            else
            {
                amount += 0.01f;
            }
        }
        return amount;
    }

    public void Reset()
    {
        xRot = 0f;
        yRot = 0f;
        Rotate(0f, 0f);
    }

}
