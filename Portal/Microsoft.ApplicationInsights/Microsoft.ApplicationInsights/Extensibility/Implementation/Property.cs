using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000077 RID: 119
	internal static class Property
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x00010234 File Offset: 0x0000E434
		public static void Set<T>(ref T property, T value) where T : class
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			property = value;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00010250 File Offset: 0x0000E450
		public static void Initialize<T>(ref T? property, T? value) where T : struct
		{
			if (property == null)
			{
				property = value;
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00010261 File Offset: 0x0000E461
		public static void Initialize(ref string property, string value)
		{
			if (string.IsNullOrEmpty(property))
			{
				property = value;
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001026F File Offset: 0x0000E46F
		public static string SanitizeEventName(this string name)
		{
			return Property.TrimAndTruncate(name, 512);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001027C File Offset: 0x0000E47C
		public static string SanitizeName(this string name)
		{
			return Property.TrimAndTruncate(name, 1024);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00010289 File Offset: 0x0000E489
		public static string SanitizeDependencyType(this string value)
		{
			return Property.TrimAndTruncate(value, 1024);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00010296 File Offset: 0x0000E496
		public static string SanitizeResultCode(this string value)
		{
			return Property.TrimAndTruncate(value, 1024);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000102A3 File Offset: 0x0000E4A3
		public static string SanitizeValue(this string value)
		{
			return Property.TrimAndTruncate(value, 8192);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000102B0 File Offset: 0x0000E4B0
		public static string SanitizeMessage(this string message)
		{
			return Property.TrimAndTruncate(message, 32768);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000102BD File Offset: 0x0000E4BD
		public static string SanitizeData(this string message)
		{
			return Property.TrimAndTruncate(message, 8192);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000102CC File Offset: 0x0000E4CC
		public static Uri SanitizeUri(this Uri uri)
		{
			if (uri != null)
			{
				string text = uri.ToString();
				if (text.Length > 2048)
				{
					text = text.Substring(0, 2048);
					Uri uri2;
					if (Uri.TryCreate(text, UriKind.RelativeOrAbsolute, out uri2))
					{
						uri = uri2;
					}
				}
			}
			return uri;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00010312 File Offset: 0x0000E512
		public static string SanitizeTestName(this string value)
		{
			return Property.TrimAndTruncate(value, 1024);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001031F File Offset: 0x0000E51F
		public static string SanitizeRunLocation(this string value)
		{
			return Property.TrimAndTruncate(value, 2024);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001032C File Offset: 0x0000E52C
		public static string SanitizeAvailabilityMessage(this string value)
		{
			return Property.TrimAndTruncate(value, 8192);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001033C File Offset: 0x0000E53C
		public static void SanitizeProperties(this IDictionary<string, string> dictionary)
		{
			if (dictionary != null)
			{
				Dictionary<string, KeyValuePair<string, string>> dictionary2 = new Dictionary<string, KeyValuePair<string, string>>(dictionary.Count);
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					string text = Property.SanitizeKey(keyValuePair.Key);
					string text2 = keyValuePair.Value.SanitizeValue();
					if (string.IsNullOrEmpty(text2) || string.CompareOrdinal(text, keyValuePair.Key) != 0 || string.CompareOrdinal(text2, keyValuePair.Value) != 0)
					{
						dictionary2.Add(keyValuePair.Key, new KeyValuePair<string, string>(text, text2));
					}
				}
				foreach (KeyValuePair<string, KeyValuePair<string, string>> keyValuePair2 in dictionary2)
				{
					dictionary.Remove(keyValuePair2.Key);
					if (!string.IsNullOrEmpty(keyValuePair2.Value.Value))
					{
						string text3 = Property.MakeKeyUnique<string>(keyValuePair2.Value.Key, dictionary);
						dictionary.Add(text3, keyValuePair2.Value.Value);
					}
				}
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00010474 File Offset: 0x0000E674
		public static void SanitizeMeasurements(this IDictionary<string, double> dictionary)
		{
			if (dictionary != null)
			{
				Dictionary<string, KeyValuePair<string, double>> dictionary2 = new Dictionary<string, KeyValuePair<string, double>>(dictionary.Count);
				foreach (KeyValuePair<string, double> keyValuePair in dictionary)
				{
					string text = Property.SanitizeKey(keyValuePair.Key);
					bool flag;
					double num = Utils.SanitizeNanAndInfinity(keyValuePair.Value, out flag);
					if (string.CompareOrdinal(text, keyValuePair.Key) != 0 || flag)
					{
						dictionary2.Add(keyValuePair.Key, new KeyValuePair<string, double>(text, num));
					}
				}
				foreach (KeyValuePair<string, KeyValuePair<string, double>> keyValuePair2 in dictionary2)
				{
					dictionary.Remove(keyValuePair2.Key);
					string text2 = Property.MakeKeyUnique<double>(keyValuePair2.Value.Key, dictionary);
					dictionary.Add(text2, keyValuePair2.Value.Value);
				}
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00010584 File Offset: 0x0000E784
		public static string TrimAndTruncate(string value, int maxLength)
		{
			if (value != null)
			{
				value = value.Trim();
				value = Property.Truncate(value, maxLength);
			}
			return value;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001059B File Offset: 0x0000E79B
		private static string Truncate(string value, int maxLength)
		{
			if (value.Length <= maxLength)
			{
				return value;
			}
			return value.Substring(0, maxLength);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000105B0 File Offset: 0x0000E7B0
		private static string SanitizeKey(string key)
		{
			return Property.MakeKeyNonEmpty(Property.TrimAndTruncate(key, 150));
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000105C2 File Offset: 0x0000E7C2
		private static string MakeKeyNonEmpty(string key)
		{
			if (!string.IsNullOrEmpty(key))
			{
				return key;
			}
			return "required";
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000105D4 File Offset: 0x0000E7D4
		private static string MakeKeyUnique<TValue>(string key, IDictionary<string, TValue> dictionary)
		{
			if (dictionary.ContainsKey(key))
			{
				string text = Property.Truncate(key, 147);
				int num = 1;
				do
				{
					key = text + num;
					num++;
				}
				while (dictionary.ContainsKey(key));
			}
			return key;
		}

		// Token: 0x0400017E RID: 382
		public const int MaxDictionaryNameLength = 150;

		// Token: 0x0400017F RID: 383
		public const int MaxDependencyTypeLength = 1024;

		// Token: 0x04000180 RID: 384
		public const int MaxValueLength = 8192;

		// Token: 0x04000181 RID: 385
		public const int MaxResultCodeLength = 1024;

		// Token: 0x04000182 RID: 386
		public const int MaxEventNameLength = 512;

		// Token: 0x04000183 RID: 387
		public const int MaxNameLength = 1024;

		// Token: 0x04000184 RID: 388
		public const int MaxMessageLength = 32768;

		// Token: 0x04000185 RID: 389
		public const int MaxUrlLength = 2048;

		// Token: 0x04000186 RID: 390
		public const int MaxDataLength = 8192;

		// Token: 0x04000187 RID: 391
		public const int MaxTestNameLength = 1024;

		// Token: 0x04000188 RID: 392
		public const int MaxRunLocationLength = 2024;

		// Token: 0x04000189 RID: 393
		public const int MaxAvailabilityMessageLength = 8192;

		// Token: 0x0400018A RID: 394
		public const int MaxMetricNamespaceLength = 256;

		// Token: 0x0400018B RID: 395
		public static readonly IDictionary<string, int> TagSizeLimits = new Dictionary<string, int>
		{
			{
				ContextTagKeys.Keys.ApplicationVersion,
				1024
			},
			{
				ContextTagKeys.Keys.DeviceId,
				1024
			},
			{
				ContextTagKeys.Keys.DeviceModel,
				256
			},
			{
				ContextTagKeys.Keys.DeviceOEMName,
				256
			},
			{
				ContextTagKeys.Keys.DeviceOSVersion,
				256
			},
			{
				ContextTagKeys.Keys.DeviceType,
				64
			},
			{
				ContextTagKeys.Keys.LocationIp,
				45
			},
			{
				ContextTagKeys.Keys.OperationId,
				128
			},
			{
				ContextTagKeys.Keys.OperationName,
				1024
			},
			{
				ContextTagKeys.Keys.OperationParentId,
				128
			},
			{
				ContextTagKeys.Keys.OperationSyntheticSource,
				1024
			},
			{
				ContextTagKeys.Keys.OperationCorrelationVector,
				64
			},
			{
				ContextTagKeys.Keys.SessionId,
				64
			},
			{
				ContextTagKeys.Keys.UserId,
				128
			},
			{
				ContextTagKeys.Keys.UserAccountId,
				1024
			},
			{
				ContextTagKeys.Keys.UserAuthUserId,
				1024
			},
			{
				ContextTagKeys.Keys.CloudRole,
				256
			},
			{
				ContextTagKeys.Keys.CloudRoleInstance,
				256
			},
			{
				ContextTagKeys.Keys.InternalSdkVersion,
				64
			},
			{
				ContextTagKeys.Keys.InternalAgentVersion,
				64
			},
			{
				ContextTagKeys.Keys.InternalNodeName,
				256
			}
		};
	}
}
