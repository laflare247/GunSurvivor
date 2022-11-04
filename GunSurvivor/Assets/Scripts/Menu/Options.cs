using UnityEngine;

public class Options : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<TMPro.TMP_Dropdown>().value = PlayerPrefs.GetInt("QualityLevel", 2);
    }

    public void CloseOptionsField()
    {
        gameObject.SetActive(false);
    }

    public void SetQualityLevel(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SaveSetting()
    {
        PlayerPrefs.SetInt("QualityLevel", FindObjectOfType<TMPro.TMP_Dropdown>().value);
    }
}