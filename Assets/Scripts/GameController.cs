using TMPro;
using UnityEngine;



public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public SceneManager sceneManager;
    
    public UIManager uiManager;

    public MotherController mother;

    private PlayerController _player;

    public PlayerStats playerStats;
    
    public Window houseWindow;
    
    public GameObject door;
    
    public bool isRunning = true;

    public GameObject upgrades;

    private int _passiveScore ;

    private int _aggressiveScore ;

    private int _furtiveScore;

    public TextMeshProUGUI aggressiveText;

    public TextMeshProUGUI passiveText;
    
    public TextMeshProUGUI furtiveText;

    public int demonKilled ;
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Error there should only be one instance of game controller in the scene");
        }
        Instance = this;
    }

    void Start()
    {
        upgrades.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isRunning)
            {
                CheckUpgrades();
            }
            else
            {
                ResumeGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            houseWindow.ChangeSprite();
        }
    }

    void CheckUpgrades()
    {
        isRunning = false;
        upgrades.SetActive(true);
    }
    void ResumeGame()
    {
        upgrades.SetActive(false);
        isRunning = true;
    }

    void UpdateScoreValues()
    {
        aggressiveText.text = $"{_aggressiveScore}";
        passiveText.text = $"{_passiveScore}";
        furtiveText.text = $"{_furtiveScore}";
    }

    public void SetAggressive(int score)
    {
        _aggressiveScore = score;
        UpdateScoreValues();
    }

    public void SetPassive(int score)
    {
        _passiveScore = score;
        UpdateScoreValues();
    }

    public void SetFurtive(int score)
    {
        _furtiveScore = score;
        UpdateScoreValues();
    }

    public void LookingPhase(int spawn)
    {
        switch (spawn)
        {
            case 1:
                door.SetActive(false);
                break;
            case 2:
                houseWindow.ChangeSprite();
                break;
                
            default:
                break;
        }
    }

    public void WaitingPhase()
    {
        if (door.activeSelf)
        {
            houseWindow.ChangeSprite();
        }
        else
        {
            door.SetActive(true);
        }
    }

    public void PlayerHitDemon(DemonController demon, PlayerController playerController)
    {
        demon.TakeDamage(playerStats.strength, playerController);
        if (demon.isDetected && playerController.isDetected)
        {
            mother.SpotPlayerHittingSomething();
            playerStats.TakeDamage(4);
        }
        
    }

    public void DemonHitPlayer(DemonController demon, PlayerController playerController)
    { 
        playerStats.TakeDamage(demon.damage);
        if (demon.isDetected && playerController.isDetected)
        {
            mother.SpotPlayerBeingHit();
            demon.TakeDamage(999, playerController);
        }
       
    }

    public void KilledMonster()
    {
        demonKilled++;
    }

    public void ResetCountKill()
    {
        demonKilled = 0;
    }

    public int GetPassiveScore()
    {
        return _passiveScore;
    }

    public void IncreasePassiveScore(int value)
    {
        _passiveScore += value;
    }
    public int GetFurtiveScore()
    {
        return _furtiveScore;
    }

    public void IncreaseFurtiveScore(int value)
    {
        _furtiveScore += value;
    }
    
    public int GetAggressiveScore()
    {
        return _aggressiveScore;
    }

    public void IncreaseAggressiveScore(int value)
    {
        _aggressiveScore += value;
    }
    
    public void GameOver()
    {
        Instance.sceneManager.LoadGameOverScene();
    }
}
