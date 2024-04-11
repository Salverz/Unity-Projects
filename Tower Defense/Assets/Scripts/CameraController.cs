using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public GameObject cinemachineVirtualCamera;
    private CinemachineVirtualCamera virtualCameraComponent;

    private bool doMovement = true;
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 320f;

    void Start()
    {
        virtualCameraComponent = cinemachineVirtualCamera.GetComponent<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            cinemachineVirtualCamera.SetActive(!cinemachineVirtualCamera.activeInHierarchy);
            doMovement = !doMovement;

            if (!cinemachineVirtualCamera.activeInHierarchy)
            {
                transform.position = new Vector3(43f, 51.8f, -38.8f);
                transform.rotation = Quaternion.Euler(35f, 0f, 0f);
                virtualCameraComponent.m_Lens.FieldOfView = 60f;
                GetComponent<Camera>().fieldOfView = 60;
            }
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (!doMovement)
        {
            virtualCameraComponent.m_Lens.FieldOfView -= scroll * 25;
            if (virtualCameraComponent.m_Lens.FieldOfView > 100f)
            {
                virtualCameraComponent.m_Lens.FieldOfView = 100f;
            }
            else if (virtualCameraComponent.m_Lens.FieldOfView < 5f)
            {
                virtualCameraComponent.m_Lens.FieldOfView = 5f;
            }

            return;
        }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness) 
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness) 
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness) 
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness) 
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }


        Vector3 position = transform.position;

            position.y -= scroll * 1000 * scrollSpeed * Time.deltaTime; 
            position.y = Mathf.Clamp(position.y, minY, maxY);

        transform.position = position;
    }
}
