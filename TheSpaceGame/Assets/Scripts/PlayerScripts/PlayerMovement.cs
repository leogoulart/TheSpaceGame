using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    MasterControlls _masterControlls;
    CharacterController _controller;
    Transform _playerCam;
    Vector3 _velocity;
    float _gravity = -9.81f;

    [SerializeField]
    private float walk_speed;
    [SerializeField]
    private float run_speed;

    public void Awake()
    {
        _masterControlls = new MasterControlls();
        _controller = GetComponent<CharacterController>();
        _playerCam = Camera.main.transform;
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Movement(_masterControlls.PlayerMap.Movement.ReadValue<Vector2>());
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, _playerCam.eulerAngles.y, 0), 100 * Time.deltaTime);
    }

    private void Movement(Vector2 input)
    {
        float finalSpeed = walk_speed;

        Vector3 move_direction = transform.TransformVector(new Vector3(input.x, 0, input.y));

        _controller.Move(move_direction * finalSpeed * Time.deltaTime);

        _velocity.y += _gravity * Time.deltaTime;

        if (_velocity.y < 0)
            _velocity.y = -2f;

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void OnEnable()
    {
        _masterControlls.Enable();
    }

    private void OnDisable()
    {
        _masterControlls.Disable();
    }
}
