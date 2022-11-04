using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;

    private void Start()
    {
        Update_Coins();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenUpgradeField()
    {
        transform.Find("UpgradeField").gameObject.SetActive(true);
    }

    public void OpenOptionsField()
    {
        transform.Find("OptionsField").gameObject.SetActive(true);
    }

    public void Update_Coins()
    {
        _coinsText.text = PlayerPrefs.GetInt("Coins", 0).ToString();
    }

    private void SetGrapicsLevel()
    {
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualityLevel", 2));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
