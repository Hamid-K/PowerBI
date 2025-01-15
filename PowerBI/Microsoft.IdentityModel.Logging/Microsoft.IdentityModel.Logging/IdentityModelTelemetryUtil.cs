using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;

namespace Microsoft.IdentityModel.Logging
{
	// Token: 0x02000007 RID: 7
	public static class IdentityModelTelemetryUtil
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002614 File Offset: 0x00000814
		public static string ClientSku
		{
			get
			{
				return "ID_NET462";
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000261B File Offset: 0x0000081B
		public static string ClientVer
		{
			get
			{
				return typeof(IdentityModelTelemetryUtil).GetTypeInfo().Assembly.GetName().Version.ToString();
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002640 File Offset: 0x00000840
		public static bool AddTelemetryData(string key, string value)
		{
			if (string.IsNullOrEmpty(key))
			{
				LogHelper.LogArgumentNullException("key");
				return false;
			}
			if (string.IsNullOrEmpty(value))
			{
				LogHelper.LogArgumentNullException("value");
				return false;
			}
			if (IdentityModelTelemetryUtil.defaultTelemetryValues.Contains(key))
			{
				LogHelper.LogExceptionMessage(new ArgumentException("MIML10003: Sku and version telemetry cannot be manipulated. They are added by default."));
				return false;
			}
			IdentityModelTelemetryUtil.telemetryData[key] = value;
			return true;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026A4 File Offset: 0x000008A4
		public static bool RemoveTelemetryData(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				LogHelper.LogArgumentNullException("key");
				return false;
			}
			if (IdentityModelTelemetryUtil.defaultTelemetryValues.Contains(key))
			{
				LogHelper.LogExceptionMessage(new ArgumentException("MIML10003: Sku and version telemetry cannot be manipulated. They are added by default."));
				return false;
			}
			string text;
			return IdentityModelTelemetryUtil.telemetryData.TryRemove(key, out text);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000026F4 File Offset: 0x000008F4
		internal static void SetTelemetryData(HttpRequestMessage request, IDictionary<string, string> additionalHeaders)
		{
			if (request == null)
			{
				return;
			}
			foreach (KeyValuePair<string, string> keyValuePair in IdentityModelTelemetryUtil.telemetryData)
			{
				request.Headers.Remove(keyValuePair.Key);
				request.Headers.Add(keyValuePair.Key, keyValuePair.Value);
			}
			if (additionalHeaders != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair2 in additionalHeaders)
				{
					request.Headers.Add(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000027B8 File Offset: 0x000009B8
		internal static bool UpdateDefaultTelemetryData(string key, string value)
		{
			if (string.IsNullOrEmpty(key))
			{
				LogHelper.LogArgumentNullException("key");
				return false;
			}
			if (string.IsNullOrEmpty(value))
			{
				LogHelper.LogArgumentNullException("value");
				return false;
			}
			IdentityModelTelemetryUtil.telemetryData[key] = value;
			return true;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027F4 File Offset: 0x000009F4
		// Note: this type is marked as 'beforefieldinit'.
		static IdentityModelTelemetryUtil()
		{
			ConcurrentDictionary<string, string> concurrentDictionary = new ConcurrentDictionary<string, string>();
			concurrentDictionary["x-client-SKU"] = IdentityModelTelemetryUtil.ClientSku;
			concurrentDictionary["x-client-Ver"] = IdentityModelTelemetryUtil.ClientVer;
			IdentityModelTelemetryUtil.telemetryData = concurrentDictionary;
		}

		// Token: 0x04000026 RID: 38
		internal const string skuTelemetry = "x-client-SKU";

		// Token: 0x04000027 RID: 39
		internal const string versionTelemetry = "x-client-Ver";

		// Token: 0x04000028 RID: 40
		internal static readonly List<string> defaultTelemetryValues = new List<string> { "x-client-SKU", "x-client-Ver" };

		// Token: 0x04000029 RID: 41
		internal static readonly ConcurrentDictionary<string, string> telemetryData;
	}
}
