using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : MonoBehaviour{
    private MoveableByJoystick[] bodiesToJump;
    private FeetScript[] feetScripts;
    [HideInInspector] public float lastJumpPower;
    protected bool holdsButton;

    private void Start() {
        bodiesToJump = FindObjectsOfType<MoveableByJoystick>();
        feetScripts = new FeetScript[bodiesToJump.Length];
        foreach(var i in bodiesToJump) i.jumpButton = this;
        for (int i = 0; i < bodiesToJump.Length; i++)
            feetScripts[i] = bodiesToJump[i].feetScript;
        lastJumpPower = 4.5f;
    }

    private void FixedUpdate(){  if (holdsButton) Jump();  }

    public void Jump(){
        foreach (var i in bodiesToJump) i.MoveUp(lastJumpPower * Time.deltaTime);
        holdsButton = true;
    }
    
    public void OnPointerExitCustom() => holdsButton = false;
}
