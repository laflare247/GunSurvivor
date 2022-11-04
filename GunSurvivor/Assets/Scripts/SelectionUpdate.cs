using UnityEngine;
using UnityEngine.UI;

public class SelectionUpdate : MonoBehaviour
{
    [SerializeField] private Button[] _transformButtons;
    private int[] _transformIndexArray;

    [SerializeField] private Button[] _upgradeButtons;
    private int[] _updateIndexArray;

    [SerializeField] private Button[] _bulletButtons;
    private int[] _bulletIndexArray;

    private Button[] _createdButtons = new Button[3];

    private Button[] _usedButtons;

    [SerializeField] private Transform _border;

    private CharacterController _characterController;

    public Button[] UsedButtons
    {
        get { return _usedButtons; }
        set { _usedButtons = value; }
    }


    private void Awake()
    {
        InitializeTransformIndexArray();
        InitializeUpdateIndexArray();
        InitializeBulletIndexArray();

        _characterController = FindObjectOfType<CharacterController>().GetComponent<CharacterController>();
    }

    public void ActivateUpdateField()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;

        if (_characterController.ExperienceLevel == 10)
        {
            GenerateUpgradeButtons();
        }
        else if (_characterController.ExperienceLevel == 20)
        {
            GenerateBulletButtons();
        }
        else if (_characterController.ExperienceLevel == 30)
        {
            GenerateTransformButtons();
        }
        else
        {
            GenerateUpgradeButtons();
        }
    }

    public void DeactivateUpdateField()
    {
        gameObject.SetActive(false);
        DeleteButtons();
        Time.timeScale = 1;
    }

    private void InitializeTransformIndexArray()
    {
        _transformIndexArray = new int[_transformButtons.Length];

        for (int i = 0; i < _transformButtons.Length; i++)
        {
            _transformIndexArray[i] = i;
        }
    }

    private void InitializeUpdateIndexArray()
    {
        _updateIndexArray = new int[_upgradeButtons.Length];

        for (int i = 0; i < _upgradeButtons.Length; i++)
        {
            _updateIndexArray[i] = i;
        }
    }

    private void InitializeBulletIndexArray()
    {
        _bulletIndexArray = new int[_bulletButtons.Length];

        for (int i = 0; i < _bulletButtons.Length; i++)
        {
            _bulletIndexArray[i] = i;
        }
    }

    private void GenerateTransformButtons()
    {
        int[] mixedArray = MixIntArray(_transformIndexArray);

        int button1Index = mixedArray[0];
        int button2Index = mixedArray[1];
        int button3Index = mixedArray[2];

        Button button1 = Instantiate(_transformButtons[button1Index], transform.position + new Vector3(200, 0), transform.rotation, _border);
        Button button2 = Instantiate(_transformButtons[button2Index], transform.position, transform.rotation, _border);
        Button button3 = Instantiate(_transformButtons[button3Index], transform.position - new Vector3(200, 0), transform.rotation, _border);

        button1.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
        button1.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
        button3.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
        button3.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);

        _createdButtons[0] = button1;
        _createdButtons[1] = button2;
        _createdButtons[2] = button3;
    }

    private void GenerateUpgradeButtons()
    {
        int[] mixedArray = MixIntArray(_updateIndexArray);

        int button1Index = mixedArray[0];
        int button2Index = mixedArray[1];
        int button3Index = mixedArray[2];

        Button button1 = Instantiate(_upgradeButtons[button1Index], transform.position + new Vector3(200, 0), transform.rotation, _border);
        Button button2 = Instantiate(_upgradeButtons[button2Index], transform.position, transform.rotation, _border);
        Button button3 = Instantiate(_upgradeButtons[button3Index], transform.position - new Vector3(200, 0), transform.rotation, _border);

        button1.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
        button1.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
        button3.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
        button3.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);

        _createdButtons[0] = button1;
        _createdButtons[1] = button2;
        _createdButtons[2] = button3;
    }

    private void GenerateBulletButtons()
    {
        int[] mixedArray = MixIntArray(_bulletIndexArray);

        int button1Index = mixedArray[0];
        int button2Index = mixedArray[1];
        int button3Index = mixedArray[2];

        Button button1 = Instantiate(_bulletButtons[button1Index], transform.position + new Vector3(200, 0), transform.rotation, _border);
        Button button2 = Instantiate(_bulletButtons[button2Index], transform.position, transform.rotation, _border);
        Button button3 = Instantiate(_bulletButtons[button3Index], transform.position - new Vector3(200, 0), transform.rotation, _border);

        button1.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
        button1.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
        button3.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
        button3.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);

        _createdButtons[0] = button1;
        _createdButtons[1] = button2;
        _createdButtons[2] = button3;
    }

    private void DeleteButtons()
    {
        foreach (Button button in _createdButtons)
        {
            if (button != null)
            {
                Destroy(button.gameObject);
            }
        }
    }

    private int[] MixIntArray(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int currentValue = array[i];
            int randomIndex = Random.Range(0, array.Length);
            array[i] = array[randomIndex];
            array[randomIndex] = currentValue;
        }

        return array;
    }
}