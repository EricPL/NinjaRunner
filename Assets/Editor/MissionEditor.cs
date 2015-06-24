using UnityEngine;
using UnityEditor;
using System.Collections;

public class MissionEditor : Editor {

	private static string GetSelectionFolder()
	{
		if (Selection.activeObject != null)
		{
			string path = AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID());
			
			if (!string.IsNullOrEmpty(path))
			{
				int dot = path.LastIndexOf('.');
				int slash = Mathf.Max(path.LastIndexOf('/'), path.LastIndexOf('\\'));
				if (slash > 0) return (dot > slash) ? path.Substring(0, slash + 1) : path + "/";
			}
		}
		return "Assets/";
	}
	
	[MenuItem("Eric/Create/Mission Database")]
	public static void CreateDatabase()
	{
		// Get the currently selected asset directory
		string currentPath = GetSelectionFolder();
		
		// New asset name
		string assetName = "New Mission Database.asset";
		
		MissionDatabase asset = ScriptableObject.CreateInstance("MissionDatabase") as MissionDatabase;  //scriptable object
		AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(currentPath + assetName));
		AssetDatabase.Refresh();
	}
	


}
