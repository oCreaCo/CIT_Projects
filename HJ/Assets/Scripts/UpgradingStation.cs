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

    void Update(){
        UpgradingStation.upgradingStation = this;

        if(canOpen && Input.GetButtonDown("OpenTab") && open == false){
            open = true;
            Player.canMove = false;
            UpgradePanel.Apper(false);
        }
        else if(open == true && Input.GetButtonDown("OpenTab")){
            open = false;
            Player.canMove = true;
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
