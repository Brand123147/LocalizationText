using System.Collections.Generic;
using System.IO;
using UnityEngine;


public static class Localization
{
    /// <summary>
    /// ÿһ������
    /// </summary>
    static string line;

    /// <summary>
    /// �Ƿ���ع�����
    /// </summary>
    static bool isLoadFile = false;

    /// <summary>
    /// unity����api  ��ȡassets�ļ���·��
    /// </summary>
    static string path = Application.dataPath;

    /// <summary>
    /// ����.txt�е������ֵ�   ��Ҫ������ֵ����
    /// </summary>
    public static Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();

    /// <summary>
    /// ���õ�ǰ����
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    static string lastLanguage = null;
    static public string SetLanguage(string languagetype)
    {
        //if (languagetype == lastLanguage)
        //{
        //    return lastLanguage;
        //}
        LoadDictionary();
        List<string> lan = dictionary["KEY"];
        for (int i = 0; i < lan.Count; i++)
        {

            if (languagetype==lan[i])
            {
                PlayerPrefs.SetInt("language", i);
                lastLanguage = languagetype;
                LocalizationText.SetLanguage();
                return lan[i];
            }
        }
        Debug.LogError("there is no this language:" + languagetype);
        return null;
    }

    /// <summary>
    /// ����key����ȡ��ǰ�洢���������֣������һ��û�д���Ϊ��int��������Ĭ��Ϊ0������Chinese
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    static public string Get(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return null;
        }
        LoadDictionary();
        List<string> languages = dictionary[key];
        int index = PlayerPrefs.GetInt("language");
        if (index < languages.Count)
        {
            if (index < 0)
            {
                index = 0;
            }
            return languages[index];
        }
        Debug.LogError("Which key = " + key + " this line have untranslated words???");
        return null;
    }


    /// <summary>
    /// ���ض�ȡLocalization.txt�ļ�
    /// </summary>
    public static void LoadDictionary()
    {
        if (isLoadFile && Application.isPlaying)
        {
            return;
        }
        else
        {
            dictionary.Clear();
            // ��ȡLocalization.csv�ļ���·��     �������޸Ķ�Ӧ·��
          
            TextAsset textAsset = Resources.Load("Localization") as TextAsset;
            string alltext = textAsset.text;
            string[] line = alltext.Split(new char[2] { '\r', '\n' });
            foreach (var item in line)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                string[] content = item.Split(',');   //�����Ÿ���
                dictionary.Add(content[0], GetLanguages(content));
            }
            //StreamReader read = File.OpenText(path + @"/Scripts/Localization/Localization.txt");
            //while (!string.IsNullOrEmpty(line = read.ReadLine()))
            //{
            //string[] content = line.Split(',');   //�����Ÿ���
            //dictionary.Add(content[0], GetLanguages(content));
            //}
            isLoadFile = true;
        }
    }


    /// <summary>
    /// �Ӽ��ص�ÿһ��������ѡ������
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    static List<string> GetLanguages(string[] content)
    {
        List<string> strList = new List<string>();
        for (int i = 1; i < content.Length; i++)
        {
            if (!string.IsNullOrEmpty(content[i]))
            {
                strList.Add(content[i]);
            }
        }
        return strList;
    }
}
