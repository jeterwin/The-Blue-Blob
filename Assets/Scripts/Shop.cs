using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public static Shop Instance { get; private set; }

    [SerializeField] SaveDataSO SaveDataSO;

    [SerializeField] ParticleSystem SuccessParticleSystem;

    [SerializeField] Image HealthImage;

    [SerializeField] AudioSource AudioSource;

    [SerializeField] TextMeshProUGUI CoinsText;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InitializeUpgrades();
    }

    private void InitializeUpgrades()
    {
        CoinsText.text = "Coins: " + SaveDataSO.GetCoins;
        HealthImage.fillAmount = (float)SaveDataSO.GetHealth / SaveDataSO.GetMaxHealth;
    }

    public void Upgrade(UpgradeType upgradeType)
    {
        bool successful = false;
        switch(upgradeType)
        {
            case UpgradeType.Health:
                if(SaveDataSO.GetCoins >= (int)UpgradeType.Health && SaveDataSO.GetHealth < SaveDataSO.GetMaxHealth)
                {
                    UpgradeHealth();
                    successful = true;
                }
                break;
        }
        if(!successful) { return; }
        SaveManager.Instance.SaveGame();
        SuccessParticleSystem.Play();
        AudioSource.Play();
    }

    private void UpgradeHealth()
    {
        SaveDataSO.GetCoins -= (int)UpgradeType.Health;
        SaveDataSO.GetHealth += 1;
        CoinsText.text = "Coins: " + SaveDataSO.GetCoins;
        HealthImage.fillAmount = (float)SaveDataSO.GetHealth / SaveDataSO.GetMaxHealth;
    }
}

public enum UpgradeType
{
    Health = 5
}