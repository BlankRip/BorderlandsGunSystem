using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Player {
    public class SimplePlayer_FPS : MonoBehaviour
    {
        [SerializeField] CharacterController cc;
        [SerializeField] float movementSpeed = 10;
        [SerializeField] float gravity = -19.2f;
        [SerializeField] float jumpHight = 5;

        private Vector3 gravityVector;
        private float horizontalInput, verticalInput;
        private bool jump;
        private bool grounded;

        private void Start() {
            if(cc == null) {
                cc = GetComponent<CharacterController>();
                if(cc == null)
                    cc = GetComponentInChildren<CharacterController>();
            }
            
            if(gravity > 0)
                gravity *= -1;
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