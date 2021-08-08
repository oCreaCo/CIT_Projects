using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] Rifle Rifle;
    [SerializeField] UpgradeMenu Menu;
    int AssasultDmgLvl = 0;
    [SerializeField] Text AssasultDamageText;
    [SerializeField] Text AssasultDamageButtonText;
    [SerializeField] int[] AssasultDamagePerLevel;
    [SerializeField] int[] AssasultDamageUpgradePricePerLevel;
    int AssasultFrLvl = 0;
    [SerializeField] Text AssasultFirerateText;
    [SerializeField] Text AssasultFirerateButtonText;
    [SerializeField] int[] AssasultFireratePerLevel;
    [SerializeField] int[] AssasultFirerateUpgradePricePerLevel;
    int AssasultSpdLvl = 0;
    int SnipeDmgLvl = 0;
    int SnipeFrLvl = 0;
    int SnipePrcLvl = 0;
    public int Money = 0;
    
    void Start(){

    }


    public void AssasultDamageUpgrade(){
        if(Money >= AssasultDamageUpgradePricePerLevel[AssasultDmgLvl]){
            Money -= AssasultDamageUpgradePricePerLevel[AssasultDmgLvl];
            AssasultDmgLvl++;
            Rifle.AssasultDamageSet(AssasultDamagePerLevel[AssasultDmgLvl]);
            AssasultDamageText.text = "DPS: " + Rifle.AutoDamage/Rifle.automodeDelay + "(DMG: " + Rifle.AutoDamage + ")";
            if(AssasultDmgLvl!=5) AssasultDamageButtonText.text = "Upgrade\n$" + AssasultDamageUpgradePricePerLevel[AssasultDmgLvl];
            else AssasultDamageButtonText.text = "Maxed";
        }
        Menu.MoneyTextUpdate();
    }
    public void AssasultFirerateUpgrade(){
        if(Money >= AssasultFirerateUpgradePricePerLevel[AssasultFrLvl]){
            Money -= AssasultFirerateUpgradePricePerLevel[AssasultFrLvl];
            AssasultFrLvl++;
            Rifle.AssasultFirerateSet(AssasultFireratePerLevel[AssasultFrLvl]);
            AssasultDamageText.text = "DPS: " + Rifle.AutoDamage/Rifle.automodeDelay + "(DMG: " + Rifle.AutoDamage + ")";
            if(AssasultDmgLvl!=5) AssasultFirerateButtonText.text = "Upgrade\n$" + AssasultFirerateUpgradePricePerLevel[AssasultFrLvl];
            else AssasultFirerateButtonText.text = "Maxed";
        }
        Menu.MoneyTextUpdate();
    }
}
