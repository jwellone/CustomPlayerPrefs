using System;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;

namespace jwellone
{
	public sealed class AesPlayerPrefsProvider : IPlayerPrefsProvider
	{
		private Rijndael Rijndael
		{
			get
			{
				if (null == _rijndael)
				{
					_rijndael = new RijndaelManaged();
					_rijndael.Padding = _paddingMode;
					_rijndael.Mode = CipherMode.CBC;
					_rijndael.KeySize = _keySize;
					_rijndael.BlockSize = _blockSize;
					_rijndael.Key = _key;
					_rijndael.IV = _iv;
				}
				return _rijndael;
			}
		}

		private int _keySize { get; set; }
		private int _blockSize { get; set; }
		private byte[] _key { get; set; }
		private byte[] _iv { get; set; }
		private PaddingMode _paddingMode { get; set; }
		private Rijndael _rijndael = null;

		public AesPlayerPrefsProvider(byte[] key, byte[] iv, int keySize = 128, int blockSize = 128, PaddingMode paddingMode = PaddingMode.PKCS7)
		{
			_key = key;
			_iv = iv;
			_keySize = keySize;
			_blockSize = blockSize;
			_paddingMode = paddingMode;
		}

		public AesPlayerPrefsProvider(string key, string iv, int keySize = 128, int blockSize = 128, PaddingMode paddingMode = PaddingMode.PKCS7)
		{
			_key = Encoding.UTF8.GetBytes(key);
			_iv = Encoding.UTF8.GetBytes(iv);
			_keySize = keySize;
			_blockSize = blockSize;
			_paddingMode = paddingMode;
		}

		~AesPlayerPrefsProvider()
		{
			if (null != _rijndael)
			{
				_rijndael.Clear();
				_rijndael = null;
			}
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
			var encKey = Encrypt(key);
			PlayerPrefs.DeleteKey(encKey);
		}

		public bool HasKey(string key)
		{
			var encKey = Encrypt(key);
			return PlayerPrefs.HasKey(encKey);
		}

		public void SetInt(string key, int value)
		{
			SetString(key, value.ToString());
		}

		public int GetInt(string key)
		{
			return int.Parse(GetString(key));
		}

		public int GetInt(string key, int defaultValue)
		{
			return int.Parse(GetString(key, defaultValue.ToString()));
		}

		public void SetFloat(string key, float value)
		{
			SetString(key, value.ToString());
		}

		public float GetFloat(string key)
		{
			return float.Parse(GetString(key));
		}

		public float GetFloat(string key, float defaultValue)
		{
			return float.Parse(GetString(key, defaultValue.ToString()));
		}

		public void SetString(string key, string value)
		{
			var encKey = Encrypt(key);
			var encValue = Encrypt(value);
			PlayerPrefs.SetString(encKey, encValue);
		}

		public string GetString(string key)
		{
			var encKey = Encrypt(key);
			return Decrypt(PlayerPrefs.GetString(encKey));
		}

		public string GetString(string key, string defaultValue)
		{
			var encKey = Encrypt(key);
			if (!PlayerPrefs.HasKey(encKey))
			{
				return defaultValue;
			}
			return Decrypt(PlayerPrefs.GetString(encKey));
		}

		private string Encrypt(string data)
		{
			using (var crypt = Rijndael.CreateEncryptor())
			{
				var bytes = Encoding.UTF8.GetBytes(data);
				return Convert.ToBase64String(crypt.TransformFinalBlock(bytes, 0, bytes.Length));
			}
		}

		private string Decrypt(string data)
		{
			using (var crypt = Rijndael.CreateDecryptor())
			{
				var bytes = Convert.FromBase64String(data);
				var decData = crypt.TransformFinalBlock(bytes, 0, bytes.Length);
				return Encoding.UTF8.GetString(decData);
			}
		}
	}
}