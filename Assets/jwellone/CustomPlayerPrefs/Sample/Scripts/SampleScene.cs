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

			DefaultPlayerPrefs.SetInt("DEFAULT_KEY_INT", 10);
			DefaultPlayerPrefs.SetFloat("DEFAULT_KEY_FLOAT", 0.123f);
			DefaultPlayerPrefs.SetString("DEFAULT_KEY_STRING", "testHoge");
			DefaultPlayerPrefs.SetData("DEFAULT_KEY_DATA", data);
			DefaultPlayerPrefs.Save();

			var iValue = DefaultPlayerPrefs.GetInt("DEFAULT_KEY_INT");
			var fValue = DefaultPlayerPrefs.GetFloat("DEFAULT_KEY_FLOAT");
			var sValue = DefaultPlayerPrefs.GetString("DEFAULT_KEY_STRING");
			var text = DefaultPlayerPrefs.GetData<TestData>("DEFAULT_KEY_DATA").ToString();
			text = $"int={iValue} float={fValue} string={sValue} data={text}";
			Log(typeof(DefaultPlayerPrefs), text, Color.yellow);

			AesPlayerPrefs.SetInt("AES_KEY_INT", 10);
			AesPlayerPrefs.SetFloat("AES_KEY_FLOAT", 0.123f);
			AesPlayerPrefs.SetString("AES_KEY_STRING", "testHoge");
			AesPlayerPrefs.SetData("AES_KEY_DATA", data);
			AesPlayerPrefs.Save();

			iValue = AesPlayerPrefs.GetInt("AES_KEY_INT");
			fValue = AesPlayerPrefs.GetFloat("AES_KEY_FLOAT");
			sValue = AesPlayerPrefs.GetString("AES_KEY_STRING");
			text = AesPlayerPrefs.GetData<TestData>("AES_KEY_DATA").ToString();
			text = $"int={iValue} float={fValue} string={sValue} data={text}";
			Log(typeof(AesPlayerPrefs), text, Color.cyan);

			XorPlayerPrefs.SetInt("XOR_KEY_INT", 10);
			XorPlayerPrefs.SetFloat("XOR_KEY_FLOAT", 0.123f);
			XorPlayerPrefs.SetString("XOR_KEY_STRING", "testHoge");
			XorPlayerPrefs.SetData("XOR_KEY_DATA", data);
			XorPlayerPrefs.Save();

			iValue = XorPlayerPrefs.GetInt("XOR_KEY_INT");
			fValue = XorPlayerPrefs.GetFloat("XOR_KEY_FLOAT");
			sValue = XorPlayerPrefs.GetString("XOR_KEY_STRING");
			text = XorPlayerPrefs.GetData<TestData>("XOR_KEY_DATA").ToString();
			text = $"int={iValue} float={fValue} string={sValue} data={text}";
			Log(typeof(XorPlayerPrefs), text, Color.green);
		}

		void Log(in Type type, string text, in Color color)
		{
			var colorCode = ColorUtility.ToHtmlStringRGB(color);
			Debug.Log($"<color=#{colorCode}>{type.Name}  {text}</color>");
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
