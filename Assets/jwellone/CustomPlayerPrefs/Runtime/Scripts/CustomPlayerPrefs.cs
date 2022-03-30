using UnityEngine;

#nullable enable

namespace jwellone
{

	public interface IPlayerPrefsProvider
	{
		void Save();
		void DeleteAll();
		void DeleteKey(string key);

		bool HasKey(string key);

		void SetInt(string key, int value);
		int GetInt(string key);
		int GetInt(string key, int defaultValue);

		void SetFloat(string key, float value);
		float GetFloat(string key);
		float GetFloat(string key, float defaultValue);

		void SetString(string key, string value);
		string GetString(string key);
		string GetString(string key, string defaultValue);
	}

	public abstract class CustomPlayerPrefs<T> where T : CustomPlayerPrefs<T>, new()
	{
		protected static readonly IPlayerPrefsProvider _provider = new T().Generate();

		protected abstract IPlayerPrefsProvider Generate();

		public static void Save()
		{
			_provider.Save();
		}

		public static void DeleteKey(string key)
		{
			_provider.DeleteKey(key);
		}

		public static void DeleteAll()
		{
			_provider.DeleteAll();
		}

		public static bool HasKey(string key)
		{
			return _provider.HasKey(key);
		}


		public static void SetInt(string key, int value)
		{
			_provider.SetInt(key, value);
		}

		public static int GetInt(string key)
		{
			return _provider.GetInt(key);
		}

		public static int GetInt(string key, int defaultValue)
		{
			return _provider.GetInt(key, defaultValue);
		}

		public static void SetFloat(string key, float value)
		{
			_provider.SetFloat(key, value);
		}

		public static float GetFloat(string key)
		{
			return _provider.GetFloat(key);
		}

		public static float GetFloat(string key, float defaultValue)
		{
			return _provider.GetFloat(key, defaultValue);
		}

		public static void SetString(string key, string value)
		{
			_provider.SetString(key, value);
		}

		public static string GetString(string key)
		{
			return _provider.GetString(key);
		}
		public static string GetString(string key, string defaultValue)
		{
			return _provider.GetString(key, defaultValue);
		}

		public static void SetData<TDATA>(string key, in TDATA data)
		{
			var json = JsonUtility.ToJson(data);
			_provider.SetString(key, json);
		}

		public static TDATA GetData<TDATA>(string key)
		{
			var json = _provider.GetString(key);
			return JsonUtility.FromJson<TDATA>(json);
		}
	}
}