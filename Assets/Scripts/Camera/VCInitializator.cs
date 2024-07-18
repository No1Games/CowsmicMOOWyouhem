using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VCInitializator : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;
    private Transform _playerTransform;

    void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();

        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        _camera.LookAt = _playerTransform;
        _camera.Follow = _playerTransform;

        Camera.main.AddComponent<CinemachineBrain>();
    }
}
