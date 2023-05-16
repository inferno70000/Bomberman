using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Input Manager")]

    private static InputManager instance;

    public static InputManager Instance { get => instance; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Only 1 InputManager allow to exist");
        }

        instance = this;
    }

    public virtual Vector2 GetMovementDirection()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            return Input.GetAxisRaw("Horizontal") * Vector2.right;
        }
        else if (Input.GetAxisRaw("Vertical") != 0)
        {
            return Input.GetAxisRaw("Vertical") * Vector2.up;
        }

        return Vector2.zero;
    }

    public virtual bool GetBombKey()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }

        return false;
    }
}
