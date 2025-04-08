using UnityEngine;

public class MazePlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 10f;
    private Vector3 moveDirection;

    private void Update()
    {
        // Handle movement input (Arrow Keys or WASD)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
        {
            moveDirection = Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
        {
            moveDirection = Vector3.back;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
        {
            moveDirection = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
        {
            moveDirection = Vector3.right;
        }
        else 
        {
            moveDirection = Vector3.zero;  // Stop movement if no key is pressed
        }

        // If there is any movement direction, move the player
        if (moveDirection != Vector3.zero)
        {
            // Smoothly rotate the player towards the direction of movement
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);

            // Move the player forward in the direction
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }
}
