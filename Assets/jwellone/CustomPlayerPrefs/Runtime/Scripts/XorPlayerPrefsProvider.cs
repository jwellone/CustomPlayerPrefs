using System;
using System.Text;
using UnityEngine;

namespace jwellone
{
	public class XorPlayerPrefsProvider : IPlayerPrefsProvider
	{
		readonly byte[] _key;

		public XorPlayerPrefsProvider(string key)
		{
			_key = Encoding.UTF8.GetBytes(key);
		}

		public void Save()
		{
			PlayerPrefs.Save();
		}

		public void DeleteAll()
		{
			PlayerPrefs.DeleteAll();
		}

		public void DeleteKey(string key)
		{
			PlayerPrefs.DeleteKey(key);
		}

		public bool HasKey(string key)
		{
			return PlayerPrefs.HasKey(Encrypt(key));
		}

		public void SetInt(string key, int value)
		{
			var encValue = BitConverter.ToInt32(XorBytes(BitConverter.GetBytes(value)), 0);
			PlayerPrefs.SetInt(Encrypt(key), encValue);
		}

		public int GetInt(string key)
		{
			var value = PlayerPrefs.GetInt(Encrypt(key));
			return BitConverter.ToInt32(XorBytes(BitConverter.GetBytes(value)), 0);
		}

		public int GetInt(string key, int defaultValue)
		{
			var encKey = Encrypt(key);
			if (PlayerPrefs.HasKey(encKey))
			{
				var value = PlayerPrefs.GetInt(encKey);
				return BitConverter.ToInt32(XorBytes(BitConverter.GetBytes(value)), 0);
			}

			return defaultValue;
		}

		public void SetFloat(string key, float value)
		{
			var encValue = BitConverter.ToSingle(XorBytes(BitConverter.GetBytes(value)), 0);
			PlayerPrefs.SetFloat(Encrypt(key), encValue);
		}

		public float GetFloat(string key)
		{
			var value = PlayerPrefs.GetFloat(Encrypt(key));
			return BitConverter.ToSingle(XorBytes(BitConverter.GetBytes(value)), 0);
		}

		public float GetFloat(string key, float defaultValue)
		{
			var encKey = Encrypt(key);
			if (PlayerPrefs.HasKey(encKey))
			{
				var value = PlayerPrefs.GetFloat(encKey);
				return BitConverter.ToSingle(XorBytes(BitConverter.GetBytes(value)), 0);
			}
			return defaultValue;
		}

		public void SetString(string key, string value)
		{
			var encKey = Encrypt(key);
			var encValue = Encrypt(value);
			PlayerPrefs.SetString(encKey, encValue);
		}

		public string GetString(string key)
		{
			return Decrypt(PlayerPrefs.GetString(Encrypt(key)));
		}

		public string GetString(string key, string defaultValue)
		{
			var decKey = Encrypt(key);
			if (PlayerPrefs.HasKey(decKey))
			{
				return Decrypt(PlayerPrefs.GetString(decKey));
			}
			return defaultValue;
		}

		private string Encrypt(string data)
		{
			var bytes = Encoding.UTF8.GetBytes(data);
			return Convert.ToBase64String(XorBytes(bytes), 0, bytes.Length);
		}

		private string Decrypt(string data)
		{
			var bytes = Convert.FromBase64String(data);
			return Encoding.UTF8.GetString(XorBytes(bytes), 0, bytes.Length);
		}

		private byte[] XorBytes(byte[] bytes)
		{
			for (var i = 0; i < bytes.Length; ++i)
			{
				var index = i % _key.Length;
				bytes[i] = (byte)(bytes[i] ^ _key[index]);
			}

			return bytes;
		}
	}
}