using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;

    private float _xInput;
    private float _yInput;

    private string _playerType = "Default";

    public string PlayerType
    {
        get { return _playerType; }
        set { _playerType = value; }
    }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    
    private void Update()
    {
        _xInput = Input.GetAxisRaw("Horizontal");
        _yInput = Input.GetAxisRaw("Vertical");

        if (!_controller.IsPaused) _controller.RotateToMouse();

        if (Input.GetButtonDown("Pause"))
        {
            PauseGame();
        }
    }

    private void FixedUpdate()
    {
        _controller.Move(_xInput, _yInput);
        
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetButton("Fire"))
        {
            if (PlayerType == "Default")
            {
                _controller.Shoot();
            }
            else if (PlayerType == "DoubleGun")
            {
                _controller.Shoot_DoubleGun();
            }
            else if (PlayerType == "MachineGun")
            {
                _controller.Shoot_MachineGun();
            }
            else if (PlayerType == "SniperGun")
            {
                _controller.Shoot_SniperGun();
            }
        }
        
    }

    private void PauseGame()
    {
        _controller.PauseGame();
    }
}
