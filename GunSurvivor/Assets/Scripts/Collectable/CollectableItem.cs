using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    [SerializeField] protected float _value = 25f;

    protected CharacterController _player;
    private Rigidbody2D _rb;

    private void Start()
    {
        _player = FindObjectOfType<CharacterController>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        float distance = Vector2.Distance(transform.position, _player.transform.position);

        if (distance <= _player.CollectDistance)
        {
            Vector3 direction = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.fixedDeltaTime);
            _rb.MovePosition(direction);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            PlayerCollect();
        }
    }

    protected virtual void PlayerCollect()
    {
        Destroy(gameObject);
    }
}