using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _criticalDamageColor;
    [SerializeField] private float _lifeTime = 0.5f;
    [SerializeField] private float _minSpeed = 1f;
    [SerializeField] private float _maxSpeed = 5f;
    private float counter = 0;

    public Color DefaultColor
    {
        get { return _defaultColor; }
    }

    public Color CriticalDamageColor
    {
        get { return _criticalDamageColor; }
    }

    private void Start()
    {
        Invoke(nameof(Delete), _lifeTime);
    }

    private void Update()
    {
        counter += Time.deltaTime;

        if (counter < _lifeTime / 2 && Time.timeScale == 1)
        {
            GetComponent<TextMeshPro>().fontSize += 0.1f;
        }
        else if (counter > _lifeTime / 2 && Time.timeScale == 1)
        {
            GetComponent<TextMeshPro>().fontSize -= 0.1f;
        }
        
        transform.position += Vector3.up * Random.Range(_minSpeed, _maxSpeed) * Time.deltaTime;
    }

    private void Delete()
    {
        Destroy(gameObject);
    }
}
