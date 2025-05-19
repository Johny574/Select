using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public static class MultiSelect
{
    [Shortcut("SelectSameFileTypeDirectory", KeyCode.L, ShortcutModifiers.Control)]
    public static void SelectSameFileTypeDirectory()
    {
        var selectedObj = Selection.objects[0];
        var selectedObjPath = AssetDatabase.GetAssetPath(selectedObj);
        var splitSelectedObjPath = selectedObjPath.Split("/");        
        var selectedObjDir = string.Join("/", splitSelectedObjPath[..^1]);
        var selectedObjFileType = splitSelectedObjPath[splitSelectedObjPath.Length-1].Split(".")[1];

        string[] files = Directory.GetFiles(selectedObjDir);
    
        List<Object> objects = new();
        Debug.Log(files.Length);

        for (int i = 0; i < files.Length; i++)
        {
            if (!files[i].EndsWith("."+selectedObjFileType))
            {
                Debug.Log(i);
                return;
            }
            Debug.Log("here");
            var obj = AssetDatabase.LoadAssetAtPath<Object>(files[i]);
            objects.Add(obj);
        }    

        Selection.objects = objects.ToArray(); 
    }   
}