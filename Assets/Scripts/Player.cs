using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;

    public float speed;
    public float jumpHeight;
    private float jumpVelocity;
    public float gravity;

    public float horizontalSpeed;
    private bool isMovingLeft;
    private bool isMovingRight;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.forward * speed; // avança no eixo z
        /*if ( controller.isGrounded ) {
            print("CharacterController is grounded");
            if (Input.GetKeyDown(KeyCode.Space)) {
                jumpVelocity = jumpHeight;
            }
        } else {
            jumpVelocity -= gravity;
            print("Não grounded");
        }*/

        // Isso que está aqui abaixo na verdade tem que estar após o if do keycode.space
        if ( Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < 3f && !isMovingRight ){
            isMovingRight = true;
            StartCoroutine(RightMove());
        }

        if ( Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > -3f && !isMovingLeft ){
            isMovingRight = false;
            StartCoroutine(LeftMove());
        }

        direction.y = jumpVelocity;

        controller.Move(direction * Time.deltaTime); // deltaTime se relaciona ao tempo em si, não fps
    }

    IEnumerator LeftMove(){
        for ( float i = 0 ; i < 10 ; i+= 0.1f){
            controller.Move(Vector3.left * Time.deltaTime * horizontalSpeed);
            yield return null;
        }
        isMovingLeft = false;
    }

    IEnumerator RightMove(){
        for ( float i = 0 ; i < 10 ; i+= 0.1f){
            controller.Move(Vector3.right * Time.deltaTime * horizontalSpeed);
            yield return null;
        }
        isMovingRight = false; // termina de mover para poder executar de novo
    }
}
