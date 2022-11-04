using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private CharacterController _player;

    [SerializeField] private Vector3 _offset;
    [Range(0, 1)] [SerializeField] private float _smoothness = 0.125f;

    [Header("Limits")]
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;


    private void Start()
    {
        _player = FindObjectOfType<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (_player.IsAlive) MoveCameraToPlayer();
    }

    private void MoveCameraToPlayer()
    {
        Vector3 targetPosition = _player.transform.position + _offset;
        Vector3 valueToMove = Vector3.Lerp(transform.position, targetPosition, _smoothness);

        Vector3 limitedMovement = new Vector3(Mathf.Clamp(valueToMove.x, xMin, xMax), Mathf.Clamp(valueToMove.y, yMin, yMax), _offset.z);

        transform.position = limitedMovement;
    }
}
