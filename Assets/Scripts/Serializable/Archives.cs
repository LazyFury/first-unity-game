using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Archives : ScriptableObject
{

    public string archiveName;
    public int maxHealthy;
    public int healthy;
    public int level;
    public int score;
    string _key = "default_archive_key";
    string key
    {
        get { return _key ?? "broken archive"; }
        set
        {
            _key = value;
        }
    }

    public void save(ArchivesList archivesList)
    {
        save(archivesList, key);
    }
    public void save(ArchivesList archivesList, string key)

    {
        archivesList.updateKeyOrAddNewOne(key);
        this.key = key;
        PlayerPrefs.SetString(key, JsonUtility.ToJson(this));
        PlayerPrefs.Save();
    }

    public Archives load()
    {
        return load(key);
    }

    public Archives load(string key)
    {
        if (key != "")
        {
            this.key = key;
            var str = PlayerPrefs.GetString(key);
            try
            {
                Console.WriteLine(str);
                JsonUtility.FromJsonOverwrite(str, this);
            }
            catch (Exception e)
            {
                Debug.Log("Got Archive err:" + e.ToString());
            }
        }

        return this;

    }



}

public class ArchivesList
{
    public List<string> _keys = new List<string>();
    public List<string> keys
    {
        get
        {
            return load();
        }
        set
        {
            save(value);
        }
    }

    // int _index = 0;
    public int index
    {
        get { return PlayerPrefs.GetInt("archives_index"); }
        set
        {
            PlayerPrefs.GetInt("archives_index", value);
        }
    }
    public string currentKey
    {
        get
        {
            if (index > keys.Count - 1)
            {
                return "";
            }
            return keys[index] ?? "";
        }
    }

    public bool hasCurrentArchive()
    {
        return currentKey != "";
    }
    void deepClone<T>(T obj, T newVal)
    {
        JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(obj), newVal);
    }
    public Archives newArchive(Archives def, string key)
    {
        Debug.Log(key);
        var newArchive = archive(def);
        newArchive.save(this, key);
        return newArchive.load(key);
    }

    public Archives archive(Archives def, int i)
    {
        index = i;
        return archive(def);
    }

    public Archives archive(Archives def)
    {
        var newArchive = UnityEngine.Object.Instantiate<Archives>(def);
        return newArchive.load(currentKey);
    }

    public void cleanAll()
    {
        foreach (string s in keys)
        {
            PlayerPrefs.DeleteKey(s);
        }
        PlayerPrefs.DeleteKey(nameof(ArchivesList));
        _keys = new List<string>();
        load();
    }

    public List<string> load()
    {
        _keys = new List<string>();
        var str = PlayerPrefs.GetString(nameof(ArchivesList));
        if (str != "")
        {
            try
            {

                string[] strs = str.Split(',');
                foreach (string s in strs)
                {
                    _keys.Add(s);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
                PlayerPrefs.DeleteKey(nameof(ArchivesList));
            }
        }
        return _keys;
    }

    public string keysToString()
    {
        var list = keys.ToArray();
        var str = "[";
        foreach (string s in list)
        {
            str += "," + s;
        }
        str += "]";
        return str;
    }

    public void updateKeyOrAddNewOne(string key)
    {
        var list = keys;
        var index = list.IndexOf(key);
        if (index > -1)
        {
            list[index] = key;
        }
        else
        {
            list.Add(key);
        }
        save(list);
    }

    public void delKey(int i)
    {
        var list = keys;
        Debug.Log(i);
        Debug.Log(list);
        var key = list[i];
        list.RemoveAt(i);
        save(list);
        if (key != "")
        {
            PlayerPrefs.DeleteKey(key);
        }
    }

    public void save()
    {
        save(_keys);
    }
    public void save(List<string> val)
    {
        if (val.Count > 0)
        {
            PlayerPrefs.SetString(nameof(ArchivesList), String.Join(",", val));
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.DeleteKey(nameof(ArchivesList));
        }
    }
}
