using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Microsoft.Data.Mashup;
using Microsoft.Data.Mashup.Preview;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x0200000A RID: 10
	internal static class ConnectionString
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000023B8 File Offset: 0x000005B8
		private static bool GetOptionalBoolFromJsonDict(Dictionary<string, object> dict, string property)
		{
			string text = char.ToLowerInvariant(property[0]).ToString() + property.Substring(1);
			object obj;
			return dict.TryGetValue(text, out obj) && (bool)Convert.ChangeType(obj, typeof(bool));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002408 File Offset: 0x00000608
		private static void AddDataAccessOptions(MashupConnectionStringBuilder builder, string dataAccessOptionsProp)
		{
			if (string.IsNullOrEmpty(dataAccessOptionsProp))
			{
				return;
			}
			Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(dataAccessOptionsProp);
			builder.FastCombine = ConnectionString.GetOptionalBoolFromJsonDict(dictionary, "FastCombine");
			builder.ReturnErrorValuesAsNull = ConnectionString.GetOptionalBoolFromJsonDict(dictionary, "ReturnErrorValuesAsNull");
			builder.LegacyRedirects = ConnectionString.GetOptionalBoolFromJsonDict(dictionary, "LegacyRedirects");
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002460 File Offset: 0x00000660
		public static string Build(string MProgram, string sessionId, string dataSourcePool, string dataSourceSettings, string dataAccessOptionsProp, bool enableStaticFirewallPlan, string cachePath, int maxCacheSizeInMB, string tempPath, int maxTempSizeInMB, bool throwFoldingFailure, bool isPBIDesktop)
		{
			MashupConnectionStringBuilder mashupConnectionStringBuilder = new MashupConnectionStringBuilder();
			if (!isPBIDesktop)
			{
				mashupConnectionStringBuilder.Mashup = MProgram;
				if (enableStaticFirewallPlan)
				{
					PrivacyPartitionDataSourcesExtensions.TryAddPrivacyPartitionDataSources(mashupConnectionStringBuilder);
				}
			}
			else
			{
				mashupConnectionStringBuilder.Mashup = MInteropHelperImpl.PrepareMashupPackageForPowerBI(MProgram, true);
			}
			if (!string.IsNullOrEmpty(sessionId))
			{
				mashupConnectionStringBuilder["Session"] = sessionId;
			}
			if (!string.IsNullOrEmpty(dataSourcePool))
			{
				mashupConnectionStringBuilder["DataSourcePool"] = dataSourcePool;
			}
			mashupConnectionStringBuilder.DataSourceSettings = dataSourceSettings;
			mashupConnectionStringBuilder.AllowNativeQueries = true;
			mashupConnectionStringBuilder["ThrowFoldingFailures"] = throwFoldingFailure;
			ConnectionString.AddDataAccessOptions(mashupConnectionStringBuilder, dataAccessOptionsProp);
			if (!string.IsNullOrEmpty(cachePath))
			{
				mashupConnectionStringBuilder.CachePath = cachePath;
			}
			if (!string.IsNullOrEmpty(tempPath))
			{
				mashupConnectionStringBuilder.TempPath = tempPath;
			}
			if (maxCacheSizeInMB > 0)
			{
				mashupConnectionStringBuilder.MaxCacheSize = (long)(maxCacheSizeInMB * 1024) * 1024L;
			}
			if (maxTempSizeInMB > 0)
			{
				mashupConnectionStringBuilder.MaxTempSize = (long)(maxTempSizeInMB * 1024) * 1024L;
			}
			return mashupConnectionStringBuilder.ConnectionString;
		}

		// Token: 0x0400005D RID: 93
		private const string FastCombine = "FastCombine";

		// Token: 0x0400005E RID: 94
		private const string ReturnErrorValuesAsNull = "ReturnErrorValuesAsNull";

		// Token: 0x0400005F RID: 95
		private const string LegacyRedirects = "LegacyRedirects";
	}
}
