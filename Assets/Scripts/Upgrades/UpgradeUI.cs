using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class UpgradeUI : MonoBehaviour
{
    public Image Image;

    public TextMeshProUGUI Text;

    public TextMeshProUGUI Cost;

    public Upgrade upgrade;

    public GameObject Boutton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitialiseUpgrade(Upgrade Upgrade)
    {
        upgrade = Upgrade;
        Image.sprite = Upgrade.TypeImage;
        Text.text = Upgrade.Description;
        Cost.text = $"{Upgrade.Cost}";
    }

    void OnClick()
    {
        bool verif = this.upgrade.AddUpgrade();
        if (verif)
        {
            Boutton.SetActive(false);   
        }
        
    }
}
