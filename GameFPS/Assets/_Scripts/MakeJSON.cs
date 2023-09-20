using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static MakeJSON;

public class MakeJSON : MonoBehaviour
{
    [Serializable]
    public class Monster
    {
        public string name;
        public int hp;
        public int attackPower;
        public int speed;
    }

    [Serializable]
    public class ArrayWrapper<T>
    {
        public List<T> list;
    }

    // Start is called before the first frame update
    void Start()
    {
        ReadMonsterArray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Init1()
    {
        Monster monster = new Monster();
        monster.name = "Hulk";
        monster.hp = 100;
        monster.attackPower = 1000;
        monster.speed = 50;

        string jsonValue = JsonUtility.ToJson(monster);
        Debug.Log(jsonValue);

        MakeFromJsonToValue(jsonValue);
    }

    private void ReadMonsterArray()
    {
        FileStream stream = new FileStream(Application.dataPath + "/Monsters.json", FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(stream);
        ArrayWrapper<Monster> monsters = JsonUtility.FromJson<ArrayWrapper<Monster>>(sr.ReadToEnd());
        sr.Close();
        stream.Close();
        foreach (Monster monster in monsters.list)
        {
            Debug.Log(monster.name);
        }
    }

    private void WriteMonsterArray()
    {
        FileStream stream = new FileStream(Application.dataPath + "/Monsters.json", FileMode.Open, FileAccess.Write);
        StreamWriter sw = new StreamWriter(stream);
        ArrayWrapper<Monster> monsters = new ArrayWrapper<Monster>();
        monsters.list = new List<Monster>();

        Monster monster = new Monster();
        monster.name = "Hulk";
        monster.hp = 100;
        monster.attackPower = 1000;
        monster.speed = 50;
        monsters.list.Add(monster); 
        
        monster = new Monster();
        monster.name = "Hulk2";
        monster.hp = 100;
        monster.attackPower = 1000;
        monster.speed = 50;
        monsters.list.Add(monster);

        sw.Write(JsonUtility.ToJson(monsters));

        sw.Close();
        stream.Close();
    }

    private void MakeFromJsonToValue(string json)
    {
        Monster monster = JsonUtility.FromJson<Monster>(json);
        Debug.Log(monster.name);
    }
}
