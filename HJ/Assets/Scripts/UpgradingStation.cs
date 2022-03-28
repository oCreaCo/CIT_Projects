using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradingStation : MonoBehaviour
{
    public static UpgradingStation upgradingStation;

    [SerializeField] GameObject menuOpenUI;
    [SerializeField] PlayerScript Player;
    [SerializeField] UpgradeMenu UpgradePanel;
    public bool open = false;
    bool canOpen = false;
    float Cooldown = 0.0f;

    void Update(){
        UpgradingStation.upgradingStation = this;
        if(Cooldown > 0) Cooldown -= Time.deltaTime;

        if(canOpen && Input.GetButtonDown("OpenTab") && open == false && Cooldown <= 0){
            open = true;
            Player.canMove = false;
            Cooldown = 1.5f;
            UpgradePanel.Apper(false);
        }
        else if(open == true && Input.GetButtonDown("OpenTab") && Cooldown <= 0){
            open = false;
            Player.canMove = true;
            Cooldown = 1.5f;
            UpgradePanel.Apper(true);
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            menuOpenUI.GetComponent<UpgradeMenuAvailableIndicator>().MoveCall(false);
            canOpen = true;
        }
    }
    void OnTriggerExit(Collider other){
        if(other.tag == "Player"){
            menuOpenUI.GetComponent<UpgradeMenuAvailableIndicator>().MoveCall(true);
            canOpen = false;
        }
    }
}
