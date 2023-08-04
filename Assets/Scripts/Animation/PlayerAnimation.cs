using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : CharacterAnimation
{
    // Update is called once per frame
    void Update()
    {
        Animate(InputManager.Instance.GetMovementDirection());
    }
}
