using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="VehicleData", menuName ="Vehicle/Vehicle Data")]
public class VehicleData : ScriptableObject
{
    [Header("Attributes:")]
    public string vehicleName;
    public float speed;
    public float steeringForce;
    public float brakeForce;

    [Header("Image:")]
    public Sprite vehicleImage;

    [Header("Prefabs:")]
    public GameObject vehiclePrefabs;

}
