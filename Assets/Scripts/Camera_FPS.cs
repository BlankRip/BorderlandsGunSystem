using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Player {
    public class Camera_FPS : MonoBehaviour
    {
        [SerializeField] float sensitivity;
        [SerializeField] float mouseYMin = -90.0f;
        [SerializeField] float mouseYMax = 45.0f;
        [SerializeField] Transform camObjet;

        private float mouseX;
        private float mouseY;
        private Camera cam;

        private void Start() {
            cam = Camera.main;
        }

        void LateUpdate()
        {
            mouseX += Input.GetAxis("Mouse X") * sensitivity;
            mouseY -= Input.GetAxis("Mouse Y") * sensitivity;
            mouseY = Mathf.Clamp(mouseY, mouseYMin, mouseYMax);

            transform.rotation = Quaternion.Euler(0, mouseX, 0);                         // Rotating player with change in mouse X value
            camObjet.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);             // Rotationg camera with change in mouse X and Y values
        }
    }
}