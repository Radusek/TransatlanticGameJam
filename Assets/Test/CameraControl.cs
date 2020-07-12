using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int tmp = cam1.Priority;
            cam1.Priority = cam2.Priority;
            cam2.Priority = tmp;
        }
    }
}
