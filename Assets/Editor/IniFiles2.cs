using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
 
public class IniFile2 {
    private string path;
    private Dictionary<string, Dictionary<string, string>> IniDictionary = new Dictionary<string, Dictionary<string, string>>();
    private bool Initialized = false;
 
    public IniFile2(string IniPath)
    {
        path = new FileInfo(IniPath).FullName.ToString();
    }

    private bool FirstRead() {
        if (File.Exists(path)) {
            using (StreamReader sr = new StreamReader(path)) {
                string line;
                string theSection = "";
                string theKey = "";
                string theValue = "";
                while (!string.IsNullOrEmpty(line = sr.ReadLine())) {
                    line.Trim();
                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        theSection = line.Substring(1, line.Length - 2);
                    }
                    else
                    {
                        string[] ln = line.Split(new char[] { '=' });
                        theKey = ln[0].Trim();
                        theValue = ln[1].Trim();
                    }
                    if (theSection == "" || theKey == "" || theValue == "")
                        continue;
                    PopulateIni(theSection, theKey, theValue);
                }
            }
        }
        return true;
    }
 
    private void PopulateIni(string _Section, string _Key, string _Value) {
        if (IniDictionary.ContainsKey(_Section)) {
            if (IniDictionary[_Section].ContainsKey(_Key))
                IniDictionary[_Section][_Key] = _Value;
            else
                IniDictionary[_Section].Add(_Key, _Value);
        } else {
            Dictionary<string, string> neuVal = new Dictionary<string, string>();
            neuVal.Add(_Key.ToString(), _Value);
            IniDictionary.Add(_Section.ToString(), neuVal);
        }
    }

    /// <summary>
    /// Read data from INI file. Section and Key no in enum.
    /// </summary>
    /// <param name="_Section"></param>
    /// <param name="_Key"></param>
    /// <returns></returns>
    public string ReadINI(string _Section, string _Key) {
        if (!Initialized)
            FirstRead();
        if (IniDictionary.ContainsKey(_Section))
            if (IniDictionary[_Section].ContainsKey(_Key))
                return IniDictionary[_Section][_Key];
        return null;
    }
}
