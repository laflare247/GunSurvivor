using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    private CharacterController _player;

    private void Start()
    {
        _player = FindObjectOfType<CharacterController>();
    }

    private void Update()
    {
        if (_player.IsAlive) transform.position = _player.gameObject.transform.position;
    }
}
