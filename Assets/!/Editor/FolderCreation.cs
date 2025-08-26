using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class FolderCreation : EditorWindow
{
    [MenuItem("Tools/Create Project Folders MyStyle")]

    public static void CreateFolders()
    {
        List<string> folders = new List<string>
    {
        "Scenes",
        "Scripts",
        "Scripts",
        "Scripts",
        "Prefabs",
        "Models",
        "Textures",
        "Materials",
        "Audio",
        "Animations",
        "Shaders",
        "Editor",
        "Resources",
        "Plugins"
    };
        List<string> scriptSubfolders = new List<string>
    {
        "Common",
        "UITL",
        "Movement",
        "Camera"
        };
        string mainFolder = "Assets/!";
        string subFolder = Path.Combine(mainFolder, "!");


        if (!AssetDatabase.IsValidFolder(mainFolder))
        {
            AssetDatabase.CreateFolder("Assets", "!");
            Debug.Log("Created folder: " + mainFolder);
        }

        for (int i = 0; i < folders.Count; i++)
        {
            if (!AssetDatabase.IsValidFolder(subFolder))
            {
                AssetDatabase.CreateFolder(mainFolder, folders[i]);
                Debug.Log("Created subfolder: " + subFolder);
            }
        }
        //for (int i = 0; i < folders.Count; i++)
        //{
        //    if (!AssetDatabase.IsValidFolder(subFolder))
        //    {
        //        AssetDatabase.CreateFolder(mainFolder, folders[i]);
        //        Debug.Log("Created subfolder: " + subFolder);
        //    }
        //}

        AssetDatabase.Refresh();
    }
}
