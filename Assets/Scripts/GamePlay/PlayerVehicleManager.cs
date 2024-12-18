using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVehicleManager : MonoBehaviour
{
    [SerializeField] private Transform _playerParent;
    [SerializeField] private VehicleData[] _vehicleData;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        SelectCarFromMenu();
    }
    private void SelectCarFromMenu()
    {
        string selectedVehicleName =  PlayerPrefs.GetString("SelectedVehicle", string.Empty);

        if (string.IsNullOrEmpty(selectedVehicleName)) return;
        {
            foreach (VehicleData vehicle in _vehicleData)
            {
                if(vehicle.name == selectedVehicleName)
                {
                    //Spawn Player
                    GameObject selectedVehicle = Instantiate(vehicle.vehiclePrefabs, _playerParent);
                    selectedVehicle.transform.localPosition = Vector3.zero;
                    selectedVehicle.transform.localRotation = Quaternion.Euler(0, 180, 0);
                    //Camera Follow Player
                    Transform followTarget = selectedVehicle.transform.Find("PointCameraFollow");
                    if (followTarget != null)
                    {
                        // Gán Follow Target cho Cinemachine Virtual Camera
                        _virtualCamera.Follow = followTarget;
                    }
                    else
                    {
                        Debug.LogWarning("CameraFollowTarget not found in selected vehicle prefab!");
                    }

                    break;
                }
            }
        }
    }
}
