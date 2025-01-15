using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.AnalysisServices.AdomdClient.Runtime;
using Microsoft.AnalysisServices.AdomdClient.Utilities;

namespace Microsoft.AnalysisServices.AdomdClient.Configuration
{
	// Token: 0x02000166 RID: 358
	internal static class ClientConfigLoader
	{
		// Token: 0x06001116 RID: 4374 RVA: 0x0003B548 File Offset: 0x00039748
		public static IReadOnlyDictionary<int, object> LoadClientConfiguration()
		{
			string text = ClientConfigLoader.CheckForAppSettingsOverride();
			if (string.IsNullOrEmpty(text))
			{
				text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AnalysisServices.AppSettings.json");
			}
			Dictionary<int, object> dictionary = new Dictionary<int, object>();
			ClientConfigLoader.TryLoadConfigurationFromJson(text, dictionary);
			if (!FrameworkRuntimeHelper.IsNetCoreDomain)
			{
				ClientConfigLoader.AdjustConfigurationBasedOnAppSettings(dictionary);
			}
			ClientConfigLoader.AdjustConfigurationBasedOnEnvironment(dictionary);
			ClientConfigLoader.AddDefaultConfigurationValues(dictionary);
			return dictionary;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0003B5A0 File Offset: 0x000397A0
		private static string CheckForAppSettingsOverride()
		{
			string text = Environment.GetEnvironmentVariable("MS_AS_AppSettingsPath");
			if (!string.IsNullOrEmpty(text))
			{
				int num = text.IndexOf('|');
				string text10;
				if (num != -1)
				{
					string text2 = null;
					bool flag = false;
					int num2;
					string text3;
					string text4;
					bool flag2;
					if (HostRuntimeHelper.TryGetHostingProcessInfo(out num2, out text3, out text4, out flag2))
					{
						string text5 = null;
						int num3 = 0;
						do
						{
							if (num == num3)
							{
								num3++;
							}
							else
							{
								if (num == -1)
								{
									num = text.Length;
								}
								string text6;
								string text7;
								if (ClientConfigLoader.TryParseOverrideSegmentProcessAndPath(text, num3, num, out text6, out text7))
								{
									if (string.IsNullOrEmpty(text6))
									{
										text5 = text7;
									}
									else if (string.Compare(text6, text3, StringComparison.InvariantCultureIgnoreCase) == 0)
									{
										text2 = text7;
										flag = true;
									}
								}
								num3 = num + 1;
							}
							if (num3 < text.Length)
							{
								num = text.IndexOf('|', num3);
							}
						}
						while (!flag && num3 < text.Length);
						if (!flag && !string.IsNullOrEmpty(text5))
						{
							text2 = text5;
							flag = true;
						}
					}
					else
					{
						int num4 = 0;
						do
						{
							if (num == num4)
							{
								num4++;
							}
							else
							{
								if (num == -1)
								{
									num = text.Length;
								}
								string text8;
								string text9;
								if (ClientConfigLoader.TryParseOverrideSegmentProcessAndPath(text, num4, num, out text8, out text9) && string.IsNullOrEmpty(text8))
								{
									text2 = text9;
									flag = true;
								}
								num4 = num + 1;
							}
							if (num4 < text.Length)
							{
								num = text.IndexOf('|', num4);
							}
						}
						while (!flag && num4 < text.Length);
					}
					text = (flag ? text2 : null);
				}
				else if (ClientConfigLoader.TryParseOverrideSegmentProcessAndPath(text, 0, text.Length, out text10, out text))
				{
					int num2;
					string text4;
					bool flag2;
					string text11;
					if (!string.IsNullOrEmpty(text10) && (!HostRuntimeHelper.TryGetHostingProcessInfo(out num2, out text11, out text4, out flag2) || string.Compare(text10, text11, StringComparison.InvariantCultureIgnoreCase) != 0))
					{
						text = null;
					}
				}
				else
				{
					text = null;
				}
			}
			if (string.IsNullOrEmpty(text) && !FrameworkRuntimeHelper.IsNetCoreDomain)
			{
				text = ConfigurationManager.AppSettings["AS_AppSettingsPath"];
			}
			return text;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0003B738 File Offset: 0x00039938
		private static bool TryParseOverrideSegmentProcessAndPath(string appSettingsPath, int startIndex, int endIndex, out string processName, out string path)
		{
			while (startIndex < endIndex && char.IsWhiteSpace(appSettingsPath, startIndex))
			{
				startIndex++;
			}
			if (startIndex == endIndex)
			{
				processName = null;
				path = null;
				return false;
			}
			char[] array = new char[endIndex - startIndex];
			int num = 0;
			bool flag = false;
			while (!flag && startIndex < endIndex)
			{
				char c = appSettingsPath[startIndex];
				if (c == '=')
				{
					if (startIndex == endIndex - 1 || appSettingsPath[startIndex + 1] != '=')
					{
						flag = true;
					}
					else
					{
						startIndex += 2;
					}
				}
				else
				{
					startIndex++;
				}
				if (!flag)
				{
					array[num++] = c;
				}
			}
			if (!flag)
			{
				processName = null;
				while (num > 0 && char.IsWhiteSpace(array[num - 1]))
				{
					num--;
				}
				if (num >= 2 && ((array[0] == '"' && array[num - 1] == '"') || (array[0] == '\'' && array[num - 1] == '\'')))
				{
					path = ((num > 2) ? new string(array, 1, num - 2) : null);
				}
				else if (num > 0)
				{
					path = new string(array, 0, num);
				}
				else
				{
					path = null;
				}
				return !string.IsNullOrEmpty(path);
			}
			startIndex++;
			while (num > 0 && char.IsWhiteSpace(array[num - 1]))
			{
				num--;
			}
			if (num > 0)
			{
				if (num >= 2 && ((array[0] == '"' && array[num - 1] == '"') || (array[0] == '\'' && array[num - 1] == '\'')))
				{
					processName = ((num > 2) ? new string(array, 1, num - 2) : null);
				}
				else
				{
					processName = new string(array, 0, num);
				}
			}
			else
			{
				processName = null;
			}
			while (startIndex < endIndex && char.IsWhiteSpace(appSettingsPath, startIndex))
			{
				startIndex++;
			}
			if (startIndex == endIndex)
			{
				path = null;
				return !string.IsNullOrEmpty(processName);
			}
			while (startIndex < endIndex && char.IsWhiteSpace(appSettingsPath, endIndex - 1))
			{
				endIndex--;
			}
			if (startIndex + 2 <= endIndex && ((appSettingsPath[startIndex] == '"' && appSettingsPath[endIndex - 1] == '"') || (appSettingsPath[startIndex] == '\'' && appSettingsPath[endIndex - 1] == '\'')))
			{
				path = ((startIndex + 2 < endIndex) ? appSettingsPath.Substring(startIndex + 1, endIndex - startIndex - 2) : null);
			}
			else
			{
				path = ((startIndex < endIndex) ? appSettingsPath.Substring(startIndex, endIndex - startIndex) : null);
			}
			return !string.IsNullOrEmpty(processName) || !string.IsNullOrEmpty(path);
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0003B94C File Offset: 0x00039B4C
		private static void AddDefaultConfigurationValues(IDictionary<int, object> config)
		{
			if (!config.ContainsKey(65538))
			{
				config.Add(65538, false);
			}
			config[131073] = false;
			if (!config.ContainsKey(131074))
			{
				config.Add(131074, false);
			}
			if (!config.ContainsKey(131075))
			{
				config.Add(131075, false);
			}
			if (!config.ContainsKey(196609))
			{
				config.Add(196609, 131072);
			}
			if (!config.ContainsKey(196610))
			{
				config.Add(196610, 8192);
			}
			if (!config.ContainsKey(196611))
			{
				config.Add(196611, 30000);
			}
			if (!config.ContainsKey(196612))
			{
				config.Add(196612, true);
			}
			if (!config.ContainsKey(196613))
			{
				config.Add(196613, 2097152L);
			}
			if (!config.ContainsKey(196614))
			{
				config.Add(196614, 0);
			}
			if (!config.ContainsKey(262145))
			{
				config.Add(262145, Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "ASClientDiagnostics"));
			}
			if (!config.ContainsKey(262146))
			{
				config.Add(262146, false);
			}
			if (!config.ContainsKey(262147))
			{
				config.Add(262147, false);
			}
			if (!config.ContainsKey(262148))
			{
				config.Add(262148, false);
			}
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0003BB10 File Offset: 0x00039D10
		private static bool TryLoadConfigurationFromJson(string filePath, IDictionary<int, object> config)
		{
			if (!File.Exists(filePath))
			{
				return false;
			}
			bool flag;
			try
			{
				using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					flag = ClientConfigLoader.TryLoadConfigurationFromJsonImpl(stream, config);
				}
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0003BB68 File Offset: 0x00039D68
		private static bool TryLoadConfigurationFromJsonImpl(Stream file, IDictionary<int, object> config)
		{
			ClientConfigLoader.Config config2 = (ClientConfigLoader.Config)ClientConfigLoader.jsonSerializer.ReadObject(file);
			if (config2.Configuration == null)
			{
				return false;
			}
			if (config2.Configuration.Runtime != null)
			{
				if (config2.Configuration.Runtime.IsProcessWithUI != null)
				{
					config[65537] = config2.Configuration.Runtime.IsProcessWithUI.Value;
				}
				if (config2.Configuration.Runtime.InfoRestrictionNeeded != null)
				{
					config[65538] = config2.Configuration.Runtime.InfoRestrictionNeeded.Value;
				}
			}
			if (config2.Configuration.Authentication != null)
			{
				if (config2.Configuration.Authentication.UnrestrictedFallbackToInteractiveFlow != null)
				{
					config[131074] = config2.Configuration.Authentication.UnrestrictedFallbackToInteractiveFlow.Value;
				}
				if (config2.Configuration.Authentication.DisableWamBasedSSO != null)
				{
					config[131075] = config2.Configuration.Authentication.DisableWamBasedSSO.Value;
				}
			}
			if (config2.Configuration.Connectivity != null)
			{
				if (config2.Configuration.Connectivity.HttpStreamBufferSize != null && ClientConfigLoader.IsValidValueForStreamBufferSize(config2.Configuration.Connectivity.HttpStreamBufferSize.Value))
				{
					config[196609] = config2.Configuration.Connectivity.HttpStreamBufferSize.Value * 1024;
				}
				if (config2.Configuration.Connectivity.TcpStreamBufferSize != null && ClientConfigLoader.IsValidValueForStreamBufferSize(config2.Configuration.Connectivity.TcpStreamBufferSize.Value))
				{
					config[196610] = config2.Configuration.Connectivity.TcpStreamBufferSize.Value * 1024;
				}
				if (config2.Configuration.Connectivity.EndSessionTimeout != null && ClientConfigLoader.IsValidValueForTimeout(config2.Configuration.Connectivity.EndSessionTimeout.Value))
				{
					config[196611] = config2.Configuration.Connectivity.EndSessionTimeout.Value;
				}
				if (config2.Configuration.Connectivity.HttpClientSupport != null)
				{
					config[196612] = config2.Configuration.Connectivity.HttpClientSupport.Value;
				}
				if (config2.Configuration.Connectivity.HttpClientPayloadQueueLimit != null && ClientConfigLoader.IsValidValueForHttpClientPayloadQueueLimit(config2.Configuration.Connectivity.HttpClientPayloadQueueLimit.Value))
				{
					config[196613] = config2.Configuration.Connectivity.HttpClientPayloadQueueLimit.Value * 1024L;
				}
				if (!string.IsNullOrEmpty(config2.Configuration.Connectivity.BinaryXmlSupport))
				{
					config[196614] = (int)ConvertHelper.ParseRawEnumValue<BinaryXmlSupport>(config2.Configuration.Connectivity.BinaryXmlSupport, true, false);
				}
			}
			if (config2.Configuration.Diagnostics != null)
			{
				if (!string.IsNullOrEmpty(config2.Configuration.Diagnostics.RootDirectoryPath))
				{
					config[262145] = config2.Configuration.Diagnostics.RootDirectoryPath;
				}
				if (config2.Configuration.Diagnostics.TrackUndisposedConnections != null)
				{
					config[262146] = config2.Configuration.Diagnostics.TrackUndisposedConnections.Value;
				}
				if (config2.Configuration.Diagnostics.TraceXmlaMessages != null)
				{
					config[262147] = config2.Configuration.Diagnostics.TraceXmlaMessages.Value;
				}
				if (config2.Configuration.Diagnostics.TraceXmlaHttpMessages != null)
				{
					config[262148] = config2.Configuration.Diagnostics.TraceXmlaHttpMessages.Value;
				}
			}
			return true;
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0003BFE8 File Offset: 0x0003A1E8
		private static void AdjustConfigurationBasedOnAppSettings(IDictionary<int, object> config)
		{
			ClientConfigLoader.LoadAppSettingsIntValue(196609, "AS_HttpStreamBufferSize", new Predicate<int>(ClientConfigLoader.IsValidValueForStreamBufferSize), 1024, config);
			ClientConfigLoader.LoadAppSettingsIntValue(196610, "AS_TcpStreamBufferSize", new Predicate<int>(ClientConfigLoader.IsValidValueForStreamBufferSize), 1024, config);
			ClientConfigLoader.LoadAppSettingsIntValue(196611, "AS_EndSessionTimeout", new Predicate<int>(ClientConfigLoader.IsValidValueForTimeout), 1, config);
			ClientConfigLoader.LoadAppSettingsLongValue(196613, "AS_HttpClientPayloadQueueLimit", new Predicate<long>(ClientConfigLoader.IsValidValueForHttpClientPayloadQueueLimit), 1024L, config);
			ClientConfigLoader.LoadBinaryXmlSupportValueFromAppSettings(config);
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0003C07C File Offset: 0x0003A27C
		private static void LoadAppSettingsIntValue(int entry, string key, Predicate<int> validator, int factor, IDictionary<int, object> config)
		{
			try
			{
				string text = ConfigurationManager.AppSettings[key];
				int num;
				if (!string.IsNullOrEmpty(text) && int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num) && (validator == null || validator(num)))
				{
					config[entry] = num * factor;
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0003C0E0 File Offset: 0x0003A2E0
		private static void LoadAppSettingsLongValue(int entry, string key, Predicate<long> validator, long factor, IDictionary<int, object> config)
		{
			try
			{
				string text = ConfigurationManager.AppSettings[key];
				long num;
				if (!string.IsNullOrEmpty(text) && long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num) && (validator == null || validator(num)))
				{
					config[entry] = num * factor;
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0003C144 File Offset: 0x0003A344
		private static void LoadBinaryXmlSupportValueFromAppSettings(IDictionary<int, object> config)
		{
			try
			{
				string text = ConfigurationManager.AppSettings["AS_BinaryXmlSupport"];
				if (!string.IsNullOrEmpty(text))
				{
					config[196614] = (int)ConvertHelper.ParseRawEnumValue<BinaryXmlSupport>(text, false, true);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0003C198 File Offset: 0x0003A398
		private static void AdjustConfigurationBasedOnEnvironment(IDictionary<int, object> config)
		{
			if (!config.ContainsKey(65538))
			{
				ClientConfigLoader.LoadBoolValueFromEnvironment(65538, "MS_AS_InfoRestrictionNeeded", config);
			}
			if (!config.ContainsKey(131074))
			{
				ClientConfigLoader.LoadBoolValueFromEnvironment(131074, "MS_AS_UnrestrictedFallbackToInteractiveFlow", config);
			}
			if (!config.ContainsKey(131075))
			{
				ClientConfigLoader.LoadBoolValueFromEnvironment(131075, "MS_AS_DisableWamBasedSSO", config);
			}
			if (!config.ContainsKey(196614))
			{
				ClientConfigLoader.LoadBinaryXmlSupportValueFromEnvironment(config);
			}
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0003C210 File Offset: 0x0003A410
		private static void LoadBoolValueFromEnvironment(int entry, string envVariable, IDictionary<int, object> config)
		{
			bool flag;
			if (ConvertHelper.TryParseBool(Environment.GetEnvironmentVariable(envVariable), false, out flag))
			{
				config[entry] = flag;
			}
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0003C23C File Offset: 0x0003A43C
		private static void LoadBinaryXmlSupportValueFromEnvironment(IDictionary<int, object> config)
		{
			string environmentVariable = Environment.GetEnvironmentVariable("MS_AS_BinaryXmlSupport");
			if (!string.IsNullOrEmpty(environmentVariable))
			{
				try
				{
					config[196614] = (int)ConvertHelper.ParseRawEnumValue<BinaryXmlSupport>(environmentVariable, false, true);
					return;
				}
				catch (Exception)
				{
				}
			}
			if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("AS_FORCE_USE_BINARY_XML")))
			{
				config[196614] = 2;
			}
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0003C2AC File Offset: 0x0003A4AC
		private static bool IsValidValueForStreamBufferSize(int value)
		{
			return value > 0 && value <= 10240;
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0003C2BF File Offset: 0x0003A4BF
		private static bool IsValidValueForTimeout(int value)
		{
			return value >= -1;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0003C2C8 File Offset: 0x0003A4C8
		private static bool IsValidValueForHttpClientPayloadQueueLimit(long value)
		{
			return value > 0L && value <= 131072L;
		}

		// Token: 0x04000B6F RID: 2927
		private const string DefaultAppSettingsJsonFile = "AnalysisServices.AppSettings.json";

		// Token: 0x04000B70 RID: 2928
		private const string JsonPropertyName_Root = "asConfiguration";

		// Token: 0x04000B71 RID: 2929
		private const string JsonPropertyName_Runtime = "runtime";

		// Token: 0x04000B72 RID: 2930
		private const string JsonPropertyName_Runtime_IsProcessWithUI = "isProcessWithUI";

		// Token: 0x04000B73 RID: 2931
		private const string JsonPropertyName_Runtime_InfoRestrictionNeeded = "infoRestrictionNeeded";

		// Token: 0x04000B74 RID: 2932
		private const string JsonPropertyName_Authentication = "authentication";

		// Token: 0x04000B75 RID: 2933
		private const string JsonPropertyName_Authentication_UnrestrictedFallbackToInteractiveFlow = "unrestrictedFallbackToInteractiveFlow";

		// Token: 0x04000B76 RID: 2934
		private const string JsonPropertyName_Authentication_DisableWamBasedSSO = "disableWamBasedSSO";

		// Token: 0x04000B77 RID: 2935
		private const string JsonPropertyName_Connectivity = "connectivity";

		// Token: 0x04000B78 RID: 2936
		private const string JsonPropertyName_Connectivity_HttpStreamBufferSize = "httpStreamBufferSize";

		// Token: 0x04000B79 RID: 2937
		private const string JsonPropertyName_Connectivity_TcpStreamBufferSize = "tcpStreamBufferSize";

		// Token: 0x04000B7A RID: 2938
		private const string JsonPropertyName_Connectivity_EndSessionTimeout = "endSessionTimeout";

		// Token: 0x04000B7B RID: 2939
		private const string JsonPropertyName_Connectivity_HttpClientSupport = "httpClientSupport";

		// Token: 0x04000B7C RID: 2940
		private const string JsonPropertyName_Connectivity_HttpClientPayloadQueueLimit = "httpClientPayloadQueueLimit";

		// Token: 0x04000B7D RID: 2941
		private const string JsonPropertyName_Connectivity_BinaryXmlSupport = "binaryXmlSupport";

		// Token: 0x04000B7E RID: 2942
		private const string JsonPropertyName_Diagnostics = "diagnostics";

		// Token: 0x04000B7F RID: 2943
		private const string JsonPropertyName_Diagnostics_RootDirectoryPath = "rootDirectoryPath";

		// Token: 0x04000B80 RID: 2944
		private const string JsonPropertyName_Diagnostics_TrackUndisposedConnections = "trackUndisposedConnections";

		// Token: 0x04000B81 RID: 2945
		private const string JsonPropertyName_Diagnostics_TraceXmlaMessages = "traceXmlaMessages";

		// Token: 0x04000B82 RID: 2946
		private const string JsonPropertyName_Diagnostics_TraceXmlaHttpMessages = "traceXmlaHttpMessages";

		// Token: 0x04000B83 RID: 2947
		private const string AppSettingValue_AppSettingsOverridePath = "AS_AppSettingsPath";

		// Token: 0x04000B84 RID: 2948
		private const string AppSettingValue_HttpStreamBufferSize = "AS_HttpStreamBufferSize";

		// Token: 0x04000B85 RID: 2949
		private const string AppSettingValue_TcpStreamBufferSize = "AS_TcpStreamBufferSize";

		// Token: 0x04000B86 RID: 2950
		private const string AppSettingValue_EndSessionTimeout = "AS_EndSessionTimeout";

		// Token: 0x04000B87 RID: 2951
		private const string AppSettingValue_HttpClientPayloadQueueLimit = "AS_HttpClientPayloadQueueLimit";

		// Token: 0x04000B88 RID: 2952
		private const string AppSettingValue_BinaryXmlSupport = "AS_BinaryXmlSupport";

		// Token: 0x04000B89 RID: 2953
		private const string EnvironmentValue_AppSettingsOverridePath = "MS_AS_AppSettingsPath";

		// Token: 0x04000B8A RID: 2954
		private const string EnvironmentValue_UnrestrictedFallbackToInteractiveFlow = "MS_AS_UnrestrictedFallbackToInteractiveFlow";

		// Token: 0x04000B8B RID: 2955
		private const string EnvironmentValue_DisableWamBasedSSO = "MS_AS_DisableWamBasedSSO";

		// Token: 0x04000B8C RID: 2956
		private const string EnvironmentValue_BinaryXmlSupport = "MS_AS_BinaryXmlSupport";

		// Token: 0x04000B8D RID: 2957
		private const string EnvironmentValue_ForceUseBinaryXML = "AS_FORCE_USE_BINARY_XML";

		// Token: 0x04000B8E RID: 2958
		private const string EnvironmentValue_InfoRestrictionNeeded = "MS_AS_InfoRestrictionNeeded";

		// Token: 0x04000B8F RID: 2959
		private const int DefaultHttpStreamBufferSize = 131072;

		// Token: 0x04000B90 RID: 2960
		private const int DefaultTcpStreamBufferSize = 8192;

		// Token: 0x04000B91 RID: 2961
		private const int DefaultEndSessionTimeout = 30000;

		// Token: 0x04000B92 RID: 2962
		private const long DefaultHttpClientPayloadQueueLimit = 2097152L;

		// Token: 0x04000B93 RID: 2963
		private const string DefaultBaseDiagnosticsDirectoryPath = "ASClientDiagnostics";

		// Token: 0x04000B94 RID: 2964
		private static readonly DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(ClientConfigLoader.Config));

		// Token: 0x02000208 RID: 520
		[DataContract]
		private sealed class Config
		{
			// Token: 0x17000725 RID: 1829
			// (get) Token: 0x060014B1 RID: 5297 RVA: 0x00046B23 File Offset: 0x00044D23
			// (set) Token: 0x060014B2 RID: 5298 RVA: 0x00046B2B File Offset: 0x00044D2B
			[DataMember(Name = "asConfiguration")]
			public ClientConfigLoader.ASConfiguration Configuration { get; set; }
		}

		// Token: 0x02000209 RID: 521
		[DataContract(Name = "asConfiguration")]
		private sealed class ASConfiguration
		{
			// Token: 0x17000726 RID: 1830
			// (get) Token: 0x060014B4 RID: 5300 RVA: 0x00046B3C File Offset: 0x00044D3C
			// (set) Token: 0x060014B5 RID: 5301 RVA: 0x00046B44 File Offset: 0x00044D44
			[DataMember(Name = "runtime")]
			public ClientConfigLoader.ASRuntimeConfiguration Runtime { get; set; }

			// Token: 0x17000727 RID: 1831
			// (get) Token: 0x060014B6 RID: 5302 RVA: 0x00046B4D File Offset: 0x00044D4D
			// (set) Token: 0x060014B7 RID: 5303 RVA: 0x00046B55 File Offset: 0x00044D55
			[DataMember(Name = "authentication")]
			public ClientConfigLoader.ASAuthenticationConfiguration Authentication { get; set; }

			// Token: 0x17000728 RID: 1832
			// (get) Token: 0x060014B8 RID: 5304 RVA: 0x00046B5E File Offset: 0x00044D5E
			// (set) Token: 0x060014B9 RID: 5305 RVA: 0x00046B66 File Offset: 0x00044D66
			[DataMember(Name = "connectivity")]
			public ClientConfigLoader.ASConnectivityConfiguration Connectivity { get; set; }

			// Token: 0x17000729 RID: 1833
			// (get) Token: 0x060014BA RID: 5306 RVA: 0x00046B6F File Offset: 0x00044D6F
			// (set) Token: 0x060014BB RID: 5307 RVA: 0x00046B77 File Offset: 0x00044D77
			[DataMember(Name = "diagnostics")]
			public ClientConfigLoader.ASDiagnosticsConfiguration Diagnostics { get; set; }
		}

		// Token: 0x0200020A RID: 522
		[DataContract(Name = "runtime")]
		private sealed class ASRuntimeConfiguration
		{
			// Token: 0x1700072A RID: 1834
			// (get) Token: 0x060014BD RID: 5309 RVA: 0x00046B88 File Offset: 0x00044D88
			// (set) Token: 0x060014BE RID: 5310 RVA: 0x00046B90 File Offset: 0x00044D90
			[DataMember(Name = "isProcessWithUI")]
			public bool? IsProcessWithUI { get; set; }

			// Token: 0x1700072B RID: 1835
			// (get) Token: 0x060014BF RID: 5311 RVA: 0x00046B99 File Offset: 0x00044D99
			// (set) Token: 0x060014C0 RID: 5312 RVA: 0x00046BA1 File Offset: 0x00044DA1
			[DataMember(Name = "infoRestrictionNeeded")]
			public bool? InfoRestrictionNeeded { get; set; }
		}

		// Token: 0x0200020B RID: 523
		[DataContract(Name = "authentication")]
		private sealed class ASAuthenticationConfiguration
		{
			// Token: 0x1700072C RID: 1836
			// (get) Token: 0x060014C2 RID: 5314 RVA: 0x00046BB2 File Offset: 0x00044DB2
			// (set) Token: 0x060014C3 RID: 5315 RVA: 0x00046BBA File Offset: 0x00044DBA
			[DataMember(Name = "unrestrictedFallbackToInteractiveFlow")]
			public bool? UnrestrictedFallbackToInteractiveFlow { get; set; }

			// Token: 0x1700072D RID: 1837
			// (get) Token: 0x060014C4 RID: 5316 RVA: 0x00046BC3 File Offset: 0x00044DC3
			// (set) Token: 0x060014C5 RID: 5317 RVA: 0x00046BCB File Offset: 0x00044DCB
			[DataMember(Name = "disableWamBasedSSO")]
			public bool? DisableWamBasedSSO { get; set; }
		}

		// Token: 0x0200020C RID: 524
		[DataContract(Name = "connectivity")]
		private sealed class ASConnectivityConfiguration
		{
			// Token: 0x1700072E RID: 1838
			// (get) Token: 0x060014C7 RID: 5319 RVA: 0x00046BDC File Offset: 0x00044DDC
			// (set) Token: 0x060014C8 RID: 5320 RVA: 0x00046BE4 File Offset: 0x00044DE4
			[DataMember(Name = "httpStreamBufferSize")]
			public int? HttpStreamBufferSize { get; set; }

			// Token: 0x1700072F RID: 1839
			// (get) Token: 0x060014C9 RID: 5321 RVA: 0x00046BED File Offset: 0x00044DED
			// (set) Token: 0x060014CA RID: 5322 RVA: 0x00046BF5 File Offset: 0x00044DF5
			[DataMember(Name = "tcpStreamBufferSize")]
			public int? TcpStreamBufferSize { get; set; }

			// Token: 0x17000730 RID: 1840
			// (get) Token: 0x060014CB RID: 5323 RVA: 0x00046BFE File Offset: 0x00044DFE
			// (set) Token: 0x060014CC RID: 5324 RVA: 0x00046C06 File Offset: 0x00044E06
			[DataMember(Name = "endSessionTimeout")]
			public int? EndSessionTimeout { get; set; }

			// Token: 0x17000731 RID: 1841
			// (get) Token: 0x060014CD RID: 5325 RVA: 0x00046C0F File Offset: 0x00044E0F
			// (set) Token: 0x060014CE RID: 5326 RVA: 0x00046C17 File Offset: 0x00044E17
			[DataMember(Name = "httpClientSupport")]
			public bool? HttpClientSupport { get; set; }

			// Token: 0x17000732 RID: 1842
			// (get) Token: 0x060014CF RID: 5327 RVA: 0x00046C20 File Offset: 0x00044E20
			// (set) Token: 0x060014D0 RID: 5328 RVA: 0x00046C28 File Offset: 0x00044E28
			[DataMember(Name = "httpClientPayloadQueueLimit")]
			public long? HttpClientPayloadQueueLimit { get; set; }

			// Token: 0x17000733 RID: 1843
			// (get) Token: 0x060014D1 RID: 5329 RVA: 0x00046C31 File Offset: 0x00044E31
			// (set) Token: 0x060014D2 RID: 5330 RVA: 0x00046C39 File Offset: 0x00044E39
			[DataMember(Name = "binaryXmlSupport")]
			public string BinaryXmlSupport { get; set; }
		}

		// Token: 0x0200020D RID: 525
		[DataContract(Name = "diagnostics")]
		private sealed class ASDiagnosticsConfiguration
		{
			// Token: 0x17000734 RID: 1844
			// (get) Token: 0x060014D4 RID: 5332 RVA: 0x00046C4A File Offset: 0x00044E4A
			// (set) Token: 0x060014D5 RID: 5333 RVA: 0x00046C52 File Offset: 0x00044E52
			[DataMember(Name = "rootDirectoryPath")]
			public string RootDirectoryPath { get; set; }

			// Token: 0x17000735 RID: 1845
			// (get) Token: 0x060014D6 RID: 5334 RVA: 0x00046C5B File Offset: 0x00044E5B
			// (set) Token: 0x060014D7 RID: 5335 RVA: 0x00046C63 File Offset: 0x00044E63
			[DataMember(Name = "trackUndisposedConnections")]
			public bool? TrackUndisposedConnections { get; set; }

			// Token: 0x17000736 RID: 1846
			// (get) Token: 0x060014D8 RID: 5336 RVA: 0x00046C6C File Offset: 0x00044E6C
			// (set) Token: 0x060014D9 RID: 5337 RVA: 0x00046C74 File Offset: 0x00044E74
			[DataMember(Name = "traceXmlaMessages")]
			public bool? TraceXmlaMessages { get; set; }

			// Token: 0x17000737 RID: 1847
			// (get) Token: 0x060014DA RID: 5338 RVA: 0x00046C7D File Offset: 0x00044E7D
			// (set) Token: 0x060014DB RID: 5339 RVA: 0x00046C85 File Offset: 0x00044E85
			[DataMember(Name = "traceXmlaHttpMessages")]
			public bool? TraceXmlaHttpMessages { get; set; }
		}
	}
}
