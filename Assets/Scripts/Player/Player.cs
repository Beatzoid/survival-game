using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController cc;
    private CameraLook cam;

    [SerializeField] private float crouchSpeed = 2f;
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float jumpForce = 15f;

    [Space]

    [SerializeField] private float crouchTransitionSpeed = 5f;

    [SerializeField] private float gravity = -7f;

    private float gravityAcceleration;
    private float yVelocity;

    public void Start()
    {
        cc = GetComponent<CharacterController>();
        cam = GetComponentInChildren<CameraLook>();

        gravityAcceleration = gravity * gravity;
        gravityAcceleration *= Time.deltaTime;
    }

    public void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            // Forward
            moveDir.z += 1;
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            // Backward
            moveDir.z -= 1;
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            // Right
            moveDir.x += 1;
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            // Left
            moveDir.x -= 1;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Running

            moveDir *= runSpeed;

            // Smoothly move the camera from its current position to the location position
            // of (0, 2, 0), which is 2 units in the up direction, at the crouch transition speed
            cam.transform.localPosition = Vector3.Lerp(
                cam.transform.localPosition,
                new Vector3(0, 2, 0),
                crouchTransitionSpeed * Time.deltaTime
            );

            // Smoothly move the character controller's height 2 units up at the crouch transition speed
            cc.height = Mathf.Lerp(cc.height, 2, crouchTransitionSpeed * Time.deltaTime);
            // Smoothly move the character controller's center 1 unit up at the crouch transition speed
            cc.center = Vector3.Lerp(cc.center, new Vector3(0, 1, 0), crouchTransitionSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
        {
            // Crouching
            moveDir *= crouchSpeed;

            // Smoothly move the camera from its current position to the location position
            // of (0, 1, 0), which is 1 unit in the up direction, at the crouch transition speed
            cam.transform.localPosition = Vector3.Lerp(
                cam.transform.localPosition,
                new Vector3(0, 1, 0),
                crouchTransitionSpeed * Time.deltaTime
            );

            // Smoothly move the character controller's height 1.2 units up at the crouch transition speed
            cc.height = Mathf.Lerp(cc.height, 1.2f, crouchTransitionSpeed * Time.deltaTime);
            // Smoothly move the character controller's center 0.59 units up at the crouch transition speed
            cc.center = Vector3.Lerp(cc.center, new Vector3(0, 0.59f, 0), crouchTransitionSpeed * Time.deltaTime);
        }
        else
        {
            // Walking
            moveDir *= walkSpeed;

            // Smoothly move the camera from its current position to the location position
            // of (0, 2, 0), which is 2 units in the up direction, at the crouch transition speed
            cam.transform.localPosition = Vector3.Lerp(
                cam.transform.localPosition,
                new Vector3(0, 2, 0),
                crouchTransitionSpeed * Time.deltaTime
            );

            // Smoothly move the character controller's height 2 units up at the crouch transition speed
            cc.height = Mathf.Lerp(cc.height, 2, crouchTransitionSpeed * Time.deltaTime);
            // Smoothly move the character controller's center 1 unit up at the crouch transition speed
            cc.center = Vector3.Lerp(cc.center, new Vector3(0, 1, 0), crouchTransitionSpeed * Time.deltaTime);
        }

        if (cc.isGrounded)
        {
            yVelocity = 0;

            if (Input.GetKey(KeyCode.Space))
                yVelocity = jumpForce;
        }
        else
            yVelocity -= gravityAcceleration;

        moveDir.y = yVelocity;

        moveDir = transform.TransformDirection(moveDir);
        moveDir *= Time.deltaTime;

        cc.Move(moveDir);
    }
}
