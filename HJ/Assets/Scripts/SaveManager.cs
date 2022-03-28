using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

[System.Serializable]
public struct Position
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public struct Rotation
{
    public float x;
    public float y;
}

[System.Serializable]
public class ObjectData
{
    public int curHealth;
    public Position pos;
    public Rotation rot;
}

[System.Serializable]
public class PlayerUpgradeData
{
   public float automodeDelay;
   public int autoDamage;
   public float autoSpeed;
   public float normalDelay;
   public int NormalDamage;
   public int NormalPierce;
   public int magazineSize;
   public int snipeMagazineSize;
   public bool isBulletBeam;
   public bool isNormalBulletInf;
   public bool isSnipebulletInf;
}

public static class SaveManager
{
    public static void SaveFile<T>(T file, string fileName)
    {
        string path = Application.dataPath + "/data/" + fileName + ".dat";
        FileStream fileStream = new FileStream(path, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fileStream, file);
        fileStream.Close();
    }

    public static T LoadFile<T>(string fileName)
    {
        string path = Application.dataPath + "/data/" + fileName + ".dat";
        if (File.Exists(path))
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            T obj = (T)formatter.Deserialize(fileStream);

            return obj;
        }
        else
        {
            // Debug.LogError("File doesn't exist");
            return default(T);
        }
    }
}
