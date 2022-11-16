using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Guns;

namespace Gameplay.Player {
    public class SimplePlayer_FPS : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] CharacterController cc;
        [SerializeField] float movementSpeed = 10;
        [SerializeField] float gravity = -19.2f;
        [SerializeField] float jumpHight = 5;

        [Header("Testing")]
        [SerializeField] Gun testGun;
        private Camera cachedCam;
        private AmmoSupply mySupply;

        private Vector3 gravityVector;
        private float horizontalInput, verticalInput;
        private bool jump;
        private bool grounded;

        private void Start() {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if(cc == null) {
                cc = GetComponent<CharacterController>();
                if(cc == null)
                    cc = GetComponentInChildren<CharacterController>();
            }
            
            if(gravity > 0)
                gravity *= -1;

            cachedCam = Camera.main;
            mySupply = GetComponent<AmmoSupply>();
            testGun.Equip(cachedCam, mySupply);
        }

        private void Update() {
            grounded = cc.isGrounded;
            UpdateInputData();
            Move();
        }

        private void UpdateInputData() {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            if(Input.GetKeyDown(KeyCode.Space))
                jump = true;

            if(Input.GetKeyDown(KeyCode.Mouse0))
                testGun.StartFiring();
            if(Input.GetKeyUp(KeyCode.Mouse0))
                testGun.StopFiring();
            if(Input.GetKeyDown(KeyCode.Mouse1))
                testGun.EnterADS();
            if(Input.GetKeyUp(KeyCode.Mouse1))
                testGun.ExitADS();
            if(Input.GetKeyDown(KeyCode.R))
                testGun.StartReload();
            if(Input.GetKeyDown(KeyCode.C))
                testGun.StartModeSwitch();
        }

        private void Move() {
            if (grounded && gravityVector.y < 0)
                gravityVector.y = -2;
            Vector3 _moveDir = ((transform.forward * verticalInput) + (transform.right * horizontalInput)).normalized;
            Vector3 _move = _moveDir * movementSpeed * Time.deltaTime;
            cc.Move(_move);

            if (jump) {
                if(grounded)
                    gravityVector.y = Mathf.Sqrt(jumpHight * -2 * gravity);
                else
                    jump = false;
            }
            
            gravityVector.y += gravity * Time.deltaTime;
            cc.Move(gravityVector * Time.deltaTime);
        }
    }
}