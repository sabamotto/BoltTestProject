using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Bolt.AdvancedTutorial;

public class BoltPlayerController : EntityBehaviour<IBoltPlayerState> {
    const float MOUSE_SENSITIVITY = 2f;

    bool _forward;
    bool _backward;
    bool _left;
    bool _right;
    bool _jump;

    float _yaw;
    float _pitch;

    PlayerMotor _moter;

    void Awake() {
        _moter = GetComponent<PlayerMotor>();
    }

    public override void Attached() {
        state.SetTransforms(state.Transform, transform);
    }

    void PollKeys(bool mouse) {
        _forward = Input.GetKey(KeyCode.W);
        _backward = Input.GetKey(KeyCode.S);
        _left = Input.GetKey(KeyCode.A);
        _right = Input.GetKey(KeyCode.D);
        _jump = Input.GetKeyDown(KeyCode.Space);

        if (mouse) {
            _yaw += Input.GetAxisRaw("Mouse X") * MOUSE_SENSITIVITY;
            _yaw %= 360f;

            _pitch = -Input.GetAxisRaw("Mouse Y") * MOUSE_SENSITIVITY;
            _pitch = Mathf.Clamp(_pitch, -85f, +85f);
        }
    }

    void Update() {
        PollKeys(true);
    }

    public override void SimulateController() {
        PollKeys(false);

        var input = BoltPlayerCommand.Create();

        input.Forward = _forward;
        input.Backward = _backward;
        input.Left = _left;
        input.Right = _right;
        input.Jump = _jump;
        input.Yaw = _yaw;
        input.Pitch = _pitch;

        entity.QueueInput(input);
    }

    public override void ExecuteCommand(Command command, bool resetState) {
        var cmd = (BoltPlayerCommand)command;

        if (resetState) {
            _moter.SetState(cmd.Result.Position, cmd.Result.Velocity, cmd.Result.IsGrounded, cmd.Result.JumpFrames);
        } else {
            var moterState = _moter.Move(cmd.Input.Forward, cmd.Input.Backward, cmd.Input.Left, cmd.Input.Right, cmd.Input.Jump, cmd.Input.Yaw);

            cmd.Result.Position = moterState.position;
            cmd.Result.Velocity = moterState.velocity;
            cmd.Result.IsGrounded = moterState.isGrounded;
            cmd.Result.JumpFrames = moterState.jumpFrames;
        }
    }
}
