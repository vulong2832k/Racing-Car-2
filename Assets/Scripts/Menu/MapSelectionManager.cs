using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectionManager : MonoBehaviour
{
    [SerializeField] private string[] _mapSceneNames;
    [SerializeField] private GameObject _mapSelectionPanel;

    public void LoadMap(int mapIndex)
    {
        if(mapIndex >= 0 && mapIndex < _mapSceneNames.Length)
        {
            SceneManager.LoadScene(_mapSceneNames[mapIndex]);
        }
    }
}
