using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] int sceneNum;
    [SerializeField] PlayerScript player;
    [SerializeField] Enemy[] enemies;

    void Awake()
    {
        Debug.Log(Application.dataPath+"/data");
        ObjectData data = SaveManager.LoadFile<ObjectData>("Player");
        if (data != null)
        {
            player.SetDefault(data.curHealth, new Vector3(data.pos.x, data.pos.y, data.pos.z), data.rot.x, data.rot.y);
        }
        
        enemies = FindObjectsOfType<Enemy>();
        if (enemies.Any())
        {
            for (int i = 0; i < enemies.Length; ++i)
            {
                data = SaveManager.LoadFile<ObjectData>("Enemy_"+sceneNum.ToString()+"_"+i.ToString());
                if (data != null)
                {
                    // enemies[i].SetDefault();
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Space"))
        {
            Save();
            Debug.Log("Saved");
        }
    }

    public void Save()
    {
        Vector3 pos = player.transform.position;
        Quaternion rot = player.transform.rotation;
        ObjectData data = new ObjectData();

        data.pos.x = pos.x;
        data.pos.y = pos.y;
        data.pos.z = pos.z;

        data.rot.x = player.GetComponent<PlayerScript>().Head.localRotation.eulerAngles.x;
        data.rot.y = rot.eulerAngles.y;

        data.curHealth = player.GetComponent<PlayerScript>().GetHP();

        SaveManager.SaveFile<ObjectData>(data, "Player");

        if (enemies.Any())
        {
            for (int i = 0; i < enemies.Length; ++i)
            {
                Transform enemy = enemies[i].transform;
                pos = enemy.position;
                rot = enemy.rotation;

                data.pos.x = pos.x;
                data.pos.y = pos.y;
                data.pos.z = pos.z;

                data.rot.x = rot.eulerAngles.x;
                data.rot.y = rot.eulerAngles.y;

                data.curHealth = enemies[i].GetHP();

                SaveManager.SaveFile<ObjectData>(data, "Enemy_"+sceneNum.ToString()+"_"+i.ToString());
            }
        }
    }
}
