using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5.0f;
    public float cameraDragSpeed = 10.0f;
    public float cameraRotateSpeed = 10.0f;

    public float cameraHeight;

    // Start is called before the first frame update
    void Start()
    {
        cameraHeight = Camera.main.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float translateX = Input.GetAxis("Horizontal");
        float translateZ = Input.GetAxis("Vertical");
        Vector3 translation = new Vector3(translateX, 0, translateZ) * Time.deltaTime * cameraSpeed;
        this.transform.Translate(translation, Space.Self);

        float dHeight = cameraHeight - this.gameObject.transform.position.y;
        Vector3 translateY = new Vector3(0, dHeight, 0);
        this.transform.Translate(translateY, Space.World);

        if (Input.GetMouseButton(0))
        {
            translateX = Input.GetAxis("Mouse X");
            translateZ = Input.GetAxis("Mouse Y");
            translation = new Vector3(-translateX, 0, -translateZ) * Time.deltaTime * cameraSpeed;
            this.transform.Translate(translation, Space.Self);

            dHeight = cameraHeight - this.gameObject.transform.position.y;
            translateY = new Vector3(0, dHeight, 0);
            this.transform.Translate(translateY, Space.World);
        }

        if (Input.GetMouseButton(1))
        {
            float rotateX = Input.GetAxis("Mouse X");
            Vector3 rotation = new Vector3(0, -rotateX, 0) * Time.deltaTime * cameraRotateSpeed;
            this.transform.Rotate(rotation, Space.World);
        }
    }
}
