using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SaveData (ProgressData Progress)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		
		string path = Application.persistentDataPath + "/progress.save";
		FileStream stream = new FileStream(path, FileMode.Create);
		
		PlayerData data = new PlayerData(Progress);
		
		formatter.Serialize(stream, data);
		stream.Close();
	}
	
	public static PlayerData LoadData()
	{
		string path = Application.persistentDataPath + "/progress.save";
		if (File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);
			
			PlayerData data = formatter.Deserialize(stream) as PlayerData;
			stream.Close();
			
			return data;
			
		} 
		else 
		{			
			return null;
		} 
	}
}
