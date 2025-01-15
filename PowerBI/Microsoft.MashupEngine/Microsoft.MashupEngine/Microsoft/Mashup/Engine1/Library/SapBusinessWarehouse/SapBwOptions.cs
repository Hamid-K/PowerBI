using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004CF RID: 1231
	internal sealed class SapBwOptions
	{
		// Token: 0x06002848 RID: 10312 RVA: 0x000771FA File Offset: 0x000753FA
		private SapBwOptions(IEngineHost host, OptionsRecord options, bool isVersion2)
		{
			this.host = host;
			this.options = options;
			this.isVersion2 = isVersion2;
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x00077218 File Offset: 0x00075418
		public static SapBwOptions New(IEngineHost host, string dataSourceName, Value optionsValue)
		{
			OptionsRecord optionsRecord = SapBusinessWarehouseModule.OptionRecord.CreateOptions(dataSourceName, optionsValue);
			bool flag = SapBwOptions.IsVersionTwo(optionsRecord);
			Keys keys = (optionsValue.IsRecord ? optionsValue.AsRecord.Keys : Keys.Empty);
			if (flag)
			{
				Value value;
				if (optionsRecord.TryGetValue("ExecutionMode", out value) && !Enum.IsDefined(typeof(SapBusinessWarehouseExecutionModeOption), value.AsNumber.AsInteger32))
				{
					throw ValueException.NewExpressionError<Message2>(Strings.InvalidValueForOption(value, "ExecutionMode"), null, null);
				}
				if (value.AsNumber.AsInteger32 != 67 && keys.Contains("ScaleMeasures") && optionsRecord.GetBool("ScaleMeasures", false))
				{
					throw ValueException.NewExpressionError<Message3>(Strings.OptionNotSupportedInImplementation("ScaleMeasures", true, "2.0"), null, null);
				}
				object obj;
				if (optionsRecord.TryGetValue("BatchSize", out obj))
				{
					int @int = optionsRecord.GetInt32("BatchSize");
					if (@int <= 0)
					{
						throw ValueException.NewExpressionError<Message2>(Strings.InvalidValueForOption(@int, "BatchSize"), null, null);
					}
				}
			}
			else
			{
				foreach (string text in new string[] { "ExecutionMode", "EnableStructures", "BatchSize" })
				{
					if (keys.Contains(text))
					{
						throw ValueException.NewExpressionError<Message2>(Strings.OptionOnlySupportedInImplementation(text, "2.0"), null, null);
					}
				}
			}
			string text2;
			if (optionsRecord.TryGetString("Culture", out text2) && optionsRecord.TryGetString("LanguageCode", out text2))
			{
				throw ValueException.NewExpressionError<Message2>(Strings.ConflictingProperties("Culture", "LanguageCode"), null, null);
			}
			if (optionsRecord.TryGetString("Query", out text2))
			{
				if (!optionsRecord.GetBool("ScaleMeasures", true))
				{
					throw ValueException.NewExpressionError<Message2>(Strings.ConflictingProperties("Query", "ScaleMeasures"), null, null);
				}
				if (keys.Contains("EnableStructures"))
				{
					throw ValueException.NewExpressionError<Message2>(Strings.ConflictingProperties("Query", "EnableStructures"), null, null);
				}
			}
			return new SapBwOptions(host, optionsRecord, flag);
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x00077414 File Offset: 0x00075614
		public static bool IsVersionTwo(OptionsRecord optionsRecord)
		{
			bool flag = false;
			string text;
			if (optionsRecord.TryGetString("Implementation", out text))
			{
				flag = text == "2.0";
				if (!flag)
				{
					throw ValueException.NewExpressionError<Message1>(Strings.InvalidOptionValue("Implementation"), null, null);
				}
			}
			return flag;
		}

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x0600284B RID: 10315 RVA: 0x00077454 File Offset: 0x00075654
		public bool ScaleMeasures
		{
			get
			{
				return this.options.GetBool("ScaleMeasures", true);
			}
		}

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x0600284C RID: 10316 RVA: 0x00077467 File Offset: 0x00075667
		public bool EnableStructures
		{
			get
			{
				return this.options.GetBool("EnableStructures", false);
			}
		}

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x0600284D RID: 10317 RVA: 0x0007747C File Offset: 0x0007567C
		public SapBusinessWarehouseExecutionModeOption ExecutionMode
		{
			get
			{
				if (this.executionMode == null)
				{
					Value value;
					this.options.TryGetValue("ExecutionMode", out value);
					this.executionMode = new SapBusinessWarehouseExecutionModeOption?((SapBusinessWarehouseExecutionModeOption)value.AsNumber.AsInteger32);
				}
				return this.executionMode.Value;
			}
		}

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x0600284E RID: 10318 RVA: 0x000774CA File Offset: 0x000756CA
		public int BatchSize
		{
			get
			{
				return this.options.GetInt32("BatchSize");
			}
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x0600284F RID: 10319 RVA: 0x000774DC File Offset: 0x000756DC
		public bool IsVersion2
		{
			get
			{
				return this.isVersion2;
			}
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x000774E4 File Offset: 0x000756E4
		public bool TryGetQuery(out string query)
		{
			if (!this.options.TryGetString("Query", out query))
			{
				return false;
			}
			if (!SapBwOlapDataSourceLocation.IsValidQuery(query))
			{
				throw ValueException.NewExpressionError<Message0>(Strings.SapBwInvalidQuery, TextValue.New(query), null);
			}
			return true;
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x00077518 File Offset: 0x00075718
		public string GetSapLanguageCode()
		{
			string sapLanguageCodeFromCulture;
			if (this.options.TryGetString("LanguageCode", out sapLanguageCodeFromCulture))
			{
				return sapLanguageCodeFromCulture;
			}
			CultureInfo value = this.GetCulture().Value;
			sapLanguageCodeFromCulture = SapBwOptions.GetSapLanguageCodeFromCulture(value);
			if (sapLanguageCodeFromCulture != null)
			{
				return sapLanguageCodeFromCulture;
			}
			throw ValueException.NewExpressionError<Message1>(Strings.SapBw_UnsupportedCulture(value.Name), null, null);
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x00077568 File Offset: 0x00075768
		public static string GetSapLanguageCodeFromCulture(CultureInfo culture)
		{
			string text;
			if (SapBwOptions.cultureToSapLanguageCode.TryGetValue(culture.Name, out text))
			{
				return text;
			}
			if (SapBwOptions.supportedCultures.Contains(culture.Name))
			{
				return culture.TwoLetterISOLanguageName;
			}
			return null;
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x000775A8 File Offset: 0x000757A8
		private ICulture GetCulture()
		{
			Value value;
			if (this.options.TryGetValue("Culture", out value))
			{
				return Culture.GetCulture(this.host, value);
			}
			return Culture.GetDefaultCulture(this.host);
		}

		// Token: 0x0400112E RID: 4398
		private static readonly Dictionary<string, string> cultureToSapLanguageCode = new Dictionary<string, string>
		{
			{ "az-Cyrl-AZ", "5R" },
			{ "bn-IN", "6B" },
			{ "zh-HK", "1C" },
			{ "zh-MO", "2C" },
			{ "zh-SG", "3C" },
			{ "zh-TW", "4C" },
			{ "en-GB", "6N" },
			{ "fil-PH", "F3" },
			{ "kok", "K4" },
			{ "kok-IN", "K4" },
			{ "dsb-DE", "DS" },
			{ "ms-BN", "1M" },
			{ "moh-CA", "OH" },
			{ "quz-BO", "QU" },
			{ "quz-EC", "QU" },
			{ "quz-PE", "QU" },
			{ "smn-FI", "IY" },
			{ "sms-FI", "XM" },
			{ "sma-NO", "X1" },
			{ "sma-SE", "X1" },
			{ "nso-ZA", "N6" },
			{ "syr", "SY" },
			{ "syr-SY", "SY" },
			{ "hsb-DE", "HS" },
			{ "uz-Cyrl-UZ", "3U" },
			{ "sah-RU", "YT" }
		};

		// Token: 0x0400112F RID: 4399
		private static readonly HashSet<string> supportedCultures = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"af", "af-ZA", "sq", "sq-AL", "am-ET", "ar", "ar-DZ", "ar-BH", "ar-EG", "ar-IQ",
			"ar-JO", "ar-KW", "ar-LB", "ar-LY", "ar-MA", "ar-OM", "ar-QA", "ar-SA", "ar-SY", "ar-TN",
			"ar-AE", "ar-YE", "hy", "hy-AM", "as-IN", "az", "az-Latn-AZ", "ba-RU", "eu", "eu-ES",
			"be", "be-BY", "bn-BD", "bs-Cyrl-BA", "bs-Latn-BA", "br-FR", "bg", "bg-BG", "ca", "ca-ES",
			"zh-CN", "zh-Hans", "zh-Hant", "co-FR", "hr", "hr-HR", "hr-BA", "cs", "cs-CZ", "da",
			"da-DK", "dv", "dv-MV", "nl", "nl-BE", "nl-NL", "en", "en-AU", "en-BZ", "en-CA",
			"en-029", "en-IN", "en-IE", "en-JM", "en-MY", "en-NZ", "en-PH", "en-SG", "en-ZA", "en-TT",
			"en-US", "en-ZW", "et", "et-EE", "fo", "fo-FO", "fi", "fi-FI", "fr", "fr-BE",
			"fr-CA", "fr-FR", "fr-LU", "fr-MC", "fr-CH", "fy-NL", "gl", "gl-ES", "ka", "ka-GE",
			"de", "de-AT", "de-DE", "de-LI", "de-LU", "de-CH", "el", "el-GR", "kl-GL", "gu",
			"gu-IN", "ha-Latn-NG", "he", "he-IL", "hi", "hi-IN", "hu", "hu-HU", "is", "is-IS",
			"ig-NG", "id", "id-ID", "iu-Latn-CA", "iu-Cans-CA", "ga-IE", "xh-ZA", "zu-ZA", "it", "it-IT",
			"it-CH", "ja", "ja-JP", "kn", "kn-IN", "kk", "kk-KZ", "km-KH", "rw-RW", "sw",
			"sw-KE", "ko", "ko-KR", "ky", "ky-KG", "lo-LA", "lv", "lv-LV", "lt", "lt-LT",
			"lb-LU", "mk", "mk-MK", "ms", "ms-MY", "ml-IN", "mt-MT", "mi-NZ", "mr", "mr-IN",
			"mn", "mn-MN", "mn-Mong-CN", "ne-NP", "no", "nb-NO", "nn-NO", "oc-FR", "or-IN", "ps-AF",
			"fa", "fa-IR", "pl", "pl-PL", "pt", "pt-BR", "pt-PT", "pa", "pa-IN", "ro",
			"ro-RO", "rm-CH", "ru", "ru-RU", "se-FI", "se-NO", "se-SE", "sa", "sa-IN", "sr",
			"sr-Cyrl-BA", "sr-Cyrl-SP", "sr-Latn-BA", "sr-Latn-SP", "tn-ZA", "si-LK", "sk", "sk-SK", "sl", "sl-SI",
			"es", "es-AR", "es-BO", "es-CL", "es-CO", "es-CR", "es-DO", "es-EC", "es-SV", "es-GT",
			"es-HN", "es-MX", "es-NI", "es-PA", "es-PY", "es-PE", "es-PR", "es-ES", "es-US", "es-UY",
			"es-VE", "sv", "sv-FI", "sv-SE", "tg-Cyrl-TJ", "ta", "ta-IN", "tt", "tt-RU", "te",
			"te-IN", "th", "th-TH", "bo-CN", "tr", "tr-TR", "tk-TM", "ug-CN", "uk", "uk-UA",
			"ur", "ur-PK", "uz", "uz-Latn-UZ", "vi", "vi-VN", "cy-GB", "wo-SN", "ii-CN", "yo-NG"
		};

		// Token: 0x04001130 RID: 4400
		public const string VersionTwoString = "2.0";

		// Token: 0x04001131 RID: 4401
		private readonly IEngineHost host;

		// Token: 0x04001132 RID: 4402
		private readonly OptionsRecord options;

		// Token: 0x04001133 RID: 4403
		private readonly bool isVersion2;

		// Token: 0x04001134 RID: 4404
		private SapBusinessWarehouseExecutionModeOption? executionMode;
	}
}
