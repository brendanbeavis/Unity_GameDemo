using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Assets.Scripts.FileMgr
{
    public class SaveFileManager : MonoBehaviour
    {
        static public void Save()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/save.dat");

            string save = "test";

            bf.Serialize(file, save);
            file.Close();
        }

        static public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/MySharedData.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.Open(Application.persistentDataPath + "/MySharedData.dat", FileMode.Open);
                string load = bf.Deserialize(fs) as string;
                fs.Close();

                if (load != null)
                {
                    Debug.Log("load: " + load);
                }
            }
        }

    }
}