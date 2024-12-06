﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Move
    [SerializeField] private float _playerMoveSpeed;
    [SerializeField] private float _playerTurnSpeed;
    [SerializeField] private float _playerBrakeForce;

    //Effect
    [SerializeField] private GameObject _brakeTrack;

    //wheel
    [SerializeField] private float _rotationSpeedMultiplier = 50f;
    [SerializeField] private GameObject _wheelFrontLeft;
    [SerializeField] private GameObject _wheelFrontRight;
    [SerializeField] private GameObject _wheelBackLeft;
    [SerializeField] private GameObject _wheelBackRight;

    private float _currentWheelRotation = 0f;

    //
    private Rigidbody _playerRb;
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();

        _playerRb.centerOfMass = new Vector3(0, -0.5f, 0);
    }
    void Update()
    {
        RotateWheel();
        TurnWheel();
    }
    private void FixedUpdate()
    {
        PlayerMovement();
        BrakeCarOfPlayer();
    }

    private void PlayerMovement()
    {
        float xDir;
        float yDir;

        xDir = Input.GetAxis("Horizontal");
        yDir = Input.GetAxis("Vertical");

        _playerRb.AddForce(transform.forward * yDir * _playerMoveSpeed * Time.deltaTime);

        if (_playerRb.velocity.magnitude > 0.1)
        {
            Quaternion TurnCar = Quaternion.Euler(Vector3.up * xDir * _playerTurnSpeed * Time.deltaTime);
            _playerRb.MoveRotation(_playerRb.rotation * TurnCar);
        }
        _brakeTrack.SetActive(false);
    }
    public float GetSpeedInKmh()
    {
        return _playerRb.velocity.magnitude * 3.6f;
    }
    private void BrakeCarOfPlayer()
    {
        if(_playerMoveSpeed > 0 && Input.GetKey(KeyCode.Space))
        {
            if (_playerRb.velocity.magnitude != 0)
            {
                _playerRb.AddRelativeForce(-Vector3.forward * _playerBrakeForce);
                _brakeTrack.SetActive(true);
            }
        }
    }
    private void RotateWheel()
    {
        if (_playerRb == null) return;

        float speed = _playerRb.velocity.magnitude;
        float direction = Vector3.Dot(_playerRb.velocity.normalized, transform.forward);

        _currentWheelRotation += direction * speed * _rotationSpeedMultiplier * Time.deltaTime;

        _wheelFrontLeft.transform.localRotation = Quaternion.Euler(_currentWheelRotation, 180f, 0f);
        _wheelFrontRight.transform.localRotation = Quaternion.Euler(_currentWheelRotation, 0f, 0f);
        _wheelBackLeft.transform.localRotation = Quaternion.Euler(_currentWheelRotation, 180f, 0f);
        _wheelBackRight.transform.localRotation = Quaternion.Euler(_currentWheelRotation, 0f, 0f);
    }
    private void TurnWheel()
    {
        if (_playerRb.velocity.magnitude > 0.1f)
        {
            string inputDirection = "Straight";

            if (Input.GetKey(KeyCode.A))
                inputDirection = "Left";
            else if (Input.GetKey(KeyCode.D))
                inputDirection = "Right";

            switch (inputDirection)
            {
                case "Left":
                    _wheelFrontLeft.transform.localRotation = Quaternion.Euler(_currentWheelRotation, 135f, 0f);
                    _wheelFrontRight.transform.localRotation = Quaternion.Euler(_currentWheelRotation, -45f, 0f);
                    break;

                case "Right":
                    _wheelFrontLeft.transform.localRotation = Quaternion.Euler(_currentWheelRotation, 225f, 0f);
                    _wheelFrontRight.transform.localRotation = Quaternion.Euler(_currentWheelRotation, 45f, 0f);
                    break;

                default:
                    _wheelFrontLeft.transform.localRotation = Quaternion.Euler(_currentWheelRotation, 180f, 0f);
                    _wheelFrontRight.transform.localRotation = Quaternion.Euler(_currentWheelRotation, 0f, 0f);
                    break;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            GameManager.Instance.CheckPoint();
        }
        if(other.gameObject.tag == "WinPoint")
        {
            GameManager.Instance.CompleteRaceTrack();
        }
    }
    
}