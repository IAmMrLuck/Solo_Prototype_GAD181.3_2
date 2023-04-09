using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace ConaLuk.TopDown
{
    public class PlayerScript1 : MonoBehaviour
    {

        [Header("Player Movement")]

        [SerializeField] private float playerMoveSpeed;
        [SerializeField] private Rigidbody2D playerRB;
        private Vector2 playerMovement;
        //[SerializeField] private KeyCode sprintKey;
        //[SerializeField] private float sprintSpeed;

        [Header("Interaction With Throwables")]

        [SerializeField] private GameObject throwableGameObject;
        private Rigidbody2D throwableRB;
        private bool isHoldingThrowable = false;
        [SerializeField] private float rayDistance;
        [SerializeField] private Transform rayPoint;
        [SerializeField] private Transform pickUpPoint;
        [SerializeField] private LayerMask throwableObjectLayer;
        [SerializeField] private float throwPower;
        [SerializeField] KeyCode interactWithThrowable;


        void Update()
        {
            // sprint function to be added
            //if (Input.GetKeyDown(sprintKey))
            //{
            //    playerMoveSpeed = sprintSpeed;
            //}
            //if (Input.GetKeyUp(sprintKey))
            //{
            //    playerMoveSpeed = 5f;
            //}
            //if (isHoldingThrowable == true)
            //{
            //    playerMoveSpeed = 3f;
            //}

            playerMovement.x = Input.GetAxisRaw("Horizontal");
            playerMovement.y = Input.GetAxisRaw("Vertical");

            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.up);

            if (hit2D.collider!= null)
            {
                if (isHoldingThrowable == false && Input.GetKeyDown(interactWithThrowable))
                {
                    PickUpThrowable();
                }
            }

            if (isHoldingThrowable == true && Input.GetKeyDown(interactWithThrowable))
            {
                ThrowObject();
            }

            Debug.DrawRay(rayPoint.position, transform.right * rayDistance);
        }


        private void FixedUpdate()
        {
            playerRB.MovePosition(playerRB.position + playerMovement * playerMoveSpeed * Time.deltaTime);
        }



        private void ThrowObject()
        {
            throwableGameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            throwableGameObject.transform.SetParent(null);
            throwableRB.velocity = new Vector2(1, 0);
        }

        private void PickUpThrowable()
        {
            throwableGameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            throwableGameObject.transform.position = pickUpPoint.position;
            throwableGameObject.transform.SetParent(transform);
            isHoldingThrowable = true;
        }


    }

}