using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Slider playerHealthBar;

    [SerializeField]
    private TextMeshProUGUI playerHealthText;
    
    [SerializeField]
    private TextMeshProUGUI playerAggressivePointText;
    
    [SerializeField]
    private TextMeshProUGUI playerPassivePointText;
    
    [SerializeField]
    private TextMeshProUGUI playerFurtivePointText;
    // Start is called before the first frame update
    void Start()
    {
        playerHealthBar.maxValue = GameController.Instance.playerStats.maxHealth;
        playerHealthText.text = $"{GameController.Instance.playerStats.currentHealth}/" +
                                $"{GameController.Instance.playerStats.maxHealth}";
    }

    // Update is called once per frame
    
    public void UpdateHealthBar()
    {
        playerHealthText.text = $"{GameController.Instance.playerStats.currentHealth}/" +
                                $"{GameController.Instance.playerStats.maxHealth}";
        playerHealthBar.value = GameController.Instance.playerStats.currentHealth;
    }
}
