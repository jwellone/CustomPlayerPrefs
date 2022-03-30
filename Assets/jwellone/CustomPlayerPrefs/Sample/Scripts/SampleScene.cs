using System;
using UnityEngine;
using jwellone;

namespace jwellone.CustomPlayerPrefs.Sample
{
	public class SampleScene : MonoBehaviour
	{
		[Serializable]
		class TestData
		{
			public int iValue;
			public float fValue;
			public string sValue;

			public TestData(int iValue, float fValue, string sValue)
			{
				this.iValue = iValue;
				this.fValue = fValue;
				this.sValue = sValue;
			}

			public override string ToString()
			{
				return $"iValue->{iValue} fValue->{fValue} sValue->{sValue}";
			}
		}

		private void Awake()
		{
			var data = new TestData(123, 4.56f, "789A");
			DefaultPlayerPrefs.DeleteAll();

			DefaultPlayerPrefs.SetInt("DEFAULT_KEY_INT", 10);
			DefaultPlayerPrefs.SetFloat("DEFAULT_KEY_FLOAT", 0.123f);
			DefaultPlayerPrefs.SetString("DEFAULT_KEY_STRING", "testHoge");
			DefaultPlayerPrefs.SetData("DEFAULT_KEY_DATA", data);
			DefaultPlayerPrefs.Save();

			Debug.Log($"DefaultPlayerPrefs");
			Debug.Log($"int->{DefaultPlayerPrefs.GetInt("DEFAULT_KEY_INT")}");
			Debug.Log($"float->{DefaultPlayerPrefs.GetFloat("DEFAULT_KEY_FLOAT")}");
			Debug.Log($"string->{DefaultPlayerPrefs.GetString("DEFAULT_KEY_STRING")}");
			Debug.Log($"data->{DefaultPlayerPrefs.GetData<TestData>("DEFAULT_KEY_DATA").ToString()}");
			Debug.Log($"");

			AesPlayerPrefs.SetInt("AES_KEY_INT", 10);
			AesPlayerPrefs.SetFloat("AES_KEY_FLOAT", 0.123f);
			AesPlayerPrefs.SetString("AES_KEY_STRING", "testHoge");
			AesPlayerPrefs.SetData("AES_KEY_DATA", data);
			AesPlayerPrefs.Save();

			Debug.Log($"AesPlayerPrefs");
			Debug.Log($"int->{AesPlayerPrefs.GetInt("AES_KEY_INT")}");
			Debug.Log($"float->{AesPlayerPrefs.GetFloat("AES_KEY_FLOAT")}");
			Debug.Log($"string->{AesPlayerPrefs.GetString("AES_KEY_STRING")}");
			Debug.Log($"data->{AesPlayerPrefs.GetData<TestData>("AES_KEY_DATA").ToString()}");
			Debug.Log($"");

			XorPlayerPrefs.SetInt("XOR_KEY_INT", 10);
			XorPlayerPrefs.SetFloat("XOR_KEY_FLOAT", 0.123f);
			XorPlayerPrefs.SetString("XOR_KEY_STRING", "testHoge");
			XorPlayerPrefs.SetData("XOR_KEY_DATA", data);
			XorPlayerPrefs.Save();

			Debug.Log($"XorPlayerPrefs");
			Debug.Log($"int->{XorPlayerPrefs.GetInt("XOR_KEY_INT")}");
			Debug.Log($"float->{XorPlayerPrefs.GetFloat("XOR_KEY_FLOAT")}");
			Debug.Log($"string->{XorPlayerPrefs.GetString("XOR_KEY_STRING")}");
			Debug.Log($"data->{XorPlayerPrefs.GetData<TestData>("XOR_KEY_DATA").ToString()}");
			Debug.Log($"");
		}
	}

	public sealed class DefaultPlayerPrefs : CustomPlayerPrefs<DefaultPlayerPrefs>
	{
		protected override IPlayerPrefsProvider Generate()
		{
			return new DefaultPlayerPrefsProvider();
		}
	}

	public sealed class AesPlayerPrefs : CustomPlayerPrefs<AesPlayerPrefs>
	{
		protected override IPlayerPrefsProvider Generate()
		{
			return new AesPlayerPrefsProvider("VYCewWN4iAc7nYXS", "JmzSCr8X7xTxRRHt");
		}
	}

	public sealed class XorPlayerPrefs : CustomPlayerPrefs<XorPlayerPrefs>
	{
		protected override IPlayerPrefsProvider Generate()
		{
			return new XorPlayerPrefsProvider("ugg26t35");
		}
	}
}
