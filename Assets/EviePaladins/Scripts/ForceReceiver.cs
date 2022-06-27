using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] CharacterController _controller;
    float verticalVelocity;
    Vector3 dampingVelocity;
    Vector3 _impact;
    public Vector3 ForceApplied => _impact + Vector3.up * verticalVelocity;
    public bool ConsiderGravity { get; private set; } = true;

    private void Update()
    {
        if (ConsiderGravity)
            CalculateGravity();

        _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref dampingVelocity, .2f);
    }

    public void AddForce(Vector3 _direction)
    {
        _impact += _direction;
    }

    void CalculateGravity()
    {
        if (_controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * .5f * Time.deltaTime;
        }
    }

    public void ToggleGravity(bool _value)
    {
        ConsiderGravity = _value;

        if(_value == false)
        {
            verticalVelocity = 0;
        }
    }

    public void ResetVerticalVelocity()
    {
        verticalVelocity = 0;
    }
}