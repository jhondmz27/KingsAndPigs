using Unity.Cinemachine;
using UnityEngine;

public class CustomCameraOffset : MonoBehaviour
{
    [SerializeField] CinemachineCamera cinemachineCamera;
    [SerializeField] CinemachinePositionComposer positionComposer;

    private void Start()
    {
        positionComposer = cinemachineCamera.GetComponent<CinemachinePositionComposer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entro");
        positionComposer.TargetOffset.y = -1.8f;
    }
}
