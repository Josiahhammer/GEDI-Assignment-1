using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////////////////////////////////////////////////////////////////////////////////////////
// MoveCommand implementation CLASS /////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
public class MoveCommand : ICommand
{
    private CharacterController controller;
    private Vector3 movement;
    private float speed;
    private float gravity;
    private Vector3 previousPosition;

    public MoveCommand(CharacterController controller, Vector3 movement, float speed, float gravity)
    {
        this.controller = controller;
        this.movement = movement;
        this.speed = speed;
        this.gravity = gravity;
        this.previousPosition = controller.transform.position; // Record position before movement
    }

    // Excecute Commands /////////////////////////////////////////////////////////////////////////////////////////////
    public void Execute()
    {
        Vector3 move = movement * Time.deltaTime * speed;
        controller.Move(move);

        Vector3 gravityEffect = new Vector3(0, gravity * Time.deltaTime, 0);
        controller.Move(gravityEffect);
    }

    // Undo Commands /////////////////////////////////////////////////////////////////////////////////////////////
    public void Undo()
    {
        // Revert to the previous position
        controller.transform.position = previousPosition;
    }
}