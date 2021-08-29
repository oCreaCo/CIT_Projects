using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManagement : MonoBehaviour
{
    [SerializeField] Rifle Rifle;
    [SerializeField] UpgradeMenu Menu;
    [SerializeField] Properties[] Property;
    int[] Lvl = new int[100];
    public int Money = 0;
    int WhatToUpgrade = 0;

    public void SetUpgrade0(){
        WhatToUpgrade = 0;
    }
    public void SetUpgrade1(){
        WhatToUpgrade = 1;
    }
    public void SetUpgrade2(){
        WhatToUpgrade = 2;
    }
    public void SetUpgrade3(){
        WhatToUpgrade = 3;
    }
    public void SetUpgrade4(){
        WhatToUpgrade = 4;
    }
    public void SetUpgrade5(){
        WhatToUpgrade = 5;
    }

    public void Upgrade(){
        if(Property[WhatToUpgrade].CostPerLevel[Lvl[WhatToUpgrade]]<=Money){
            Money -= Property[WhatToUpgrade].CostPerLevel[Lvl[WhatToUpgrade]];
            Lvl[WhatToUpgrade]++;
            Rifle.SetProperty(WhatToUpgrade, Property[WhatToUpgrade].LevelProperties[Lvl[WhatToUpgrade]].StatPerLevel);
            for(int i = 0 ; i < Property[WhatToUpgrade].LevelProperties[Lvl[WhatToUpgrade]].StatIndicator.Length; i++){
                Property[WhatToUpgrade].LevelProperties[Lvl[WhatToUpgrade]].StatIndicator[i].color = Property[WhatToUpgrade].LevelProperties[Lvl[WhatToUpgrade]].StatIndicatorColor;
            }
            if(Lvl[WhatToUpgrade] != Property[WhatToUpgrade].CostPerLevel.Length){
                Property[WhatToUpgrade].ButtonText.text = "Upgrade\n$" + Property[WhatToUpgrade].CostPerLevel[Lvl[WhatToUpgrade]];
            }
            else{
                Property[WhatToUpgrade].ButtonText.text = "Maxed";
            }
            Property[WhatToUpgrade].DisplayText.text = Property[WhatToUpgrade].WhatIsThis + ": " + Property[WhatToUpgrade].LevelProperties[Lvl[WhatToUpgrade]].StatPerLevel;
        }
        Menu.MoneyTextUpdate();
    }
}
[System.Serializable] class Stat{
    public float StatPerLevel;
    public Color32 StatIndicatorColor;
    public Image[] StatIndicator;
}
[System.Serializable] class Properties{
    public string WhatIsThis;
    public Text ButtonText;
    public Text DisplayText;
    public int[] CostPerLevel;
    public Stat[] LevelProperties;
}