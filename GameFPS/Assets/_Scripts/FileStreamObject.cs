using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileStreamObject : MonoBehaviour
{
    private string path = Application.dataPath + "/FileStream.txt";
    
    // Start is called before the first frame update
    void Start()
    {
        FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
        for(byte i = 65; i < 90; i++)
        {
            fileStream.WriteByte(i);
        }
        fileStream.Close();
        WriteStream();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReadStream()
    {
        StreamReader sr = new StreamReader(path);
        string line = sr.ReadLine();
        sr.Close();
        Debug.Log(line);
    }

    private void WriteStream()
    {
        StreamWriter sr = new StreamWriter(path);
        sr.WriteLine(WriteCSVFormat());
        sr.Close();
        ReadCSVFormat();
    }

    private string WriteCSVFormat()
    {
        string monsterName = "Hulk";
        int hp = 10;
        int speed = 50;
        return string.Format("{0}, {1}, {2}", monsterName, hp, speed);
    }

    private void ReadCSVFormat()
    {
        StreamReader sr = new StreamReader(path);
        string line = sr.ReadLine();
        sr.Close();
        string[] value = line.Split(", ");
        foreach(string s in value)
        {
            Debug.Log(s);
        }
    }
}
