using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour
{
    [SerializeField] private Turret turret;
    [SerializeField] private StoneSpawner spawner;
    [SerializeField] private LevelState levelState; 
    [SerializeField] private Bag bag;
    [SerializeField] private CoinAmountText coinAmountText;

    [Header("LevelState")]
    public int currentLevel = 1;


    private void Awake()
    {
        Load();
        levelState.Passed.AddListener(Save);
    }
    

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1))
        {
            Reset();
        }
    }
#endif
    private void OnDestroy()
    {
        levelState.Passed.RemoveListener(Save);
    }

    private void Save()
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("Coins", bag.CoinsAmount);
        PlayerPrefs.SetInt("Damage", turret.Damage);
        PlayerPrefs.SetFloat("FireRate", turret.FireRate);
        PlayerPrefs.SetInt("ProjectileAmount", turret.ProjectileAmount);

        PlayerPrefs.Save();
    }

    private void Load()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);

        if (bag != null)
        {
            int coins = PlayerPrefs.GetInt("Coins", 0);
            bag.SetCoinsFromPlayerPrefs(coins);
       
        }
        else
        {
            Debug.LogWarning("Bag component is missing. Unable to load turret settings.");
        }

        if (turret != null)
        {
            int damage = PlayerPrefs.GetInt("Damage", 1);
            turret.SetDamage(damage);

            float fireRate = PlayerPrefs.GetFloat("FireRate", 0.5f);
            turret.SetFireRate(fireRate);

            int projectileAmount = PlayerPrefs.GetInt("ProjectileAmount", 1);
            turret.SetProjectileAmount(projectileAmount);
        }
        else
        {
            Debug.LogWarning("Turret component is missing. Unable to load turret settings.");
        }
    }

#if UNITY_EDITOR
    private void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.DeleteAll();
    }

#endif
}
