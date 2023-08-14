using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    [SerializeField] private Rigidbody rb;
    [SerializeField] private int moveSpeed = 5;

    private PlayerController playerControls;
    private Vector2 moveDirection = Vector2.zero;
    private InputAction move;
    private InputAction fire;

    private void Awake() {
        //Debug.Log("Set");
        playerControls = new PlayerController();
    }

    private void OnEnable() {
        move = playerControls.Player.Move;
        //Debug.Log("Enabled");
        move.Enable();
    }

    private void OnDisable() {
        move.Disable();
    }

    void Update() {
        moveDirection = move.ReadValue<Vector2>(); 
    }

    private void FixedUpdate() {
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, 0, moveDirection.y * moveSpeed);
    }
}
