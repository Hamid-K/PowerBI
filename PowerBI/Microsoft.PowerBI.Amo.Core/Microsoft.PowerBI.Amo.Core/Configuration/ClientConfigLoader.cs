using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.AnalysisServices.Runtime;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Configuration
{
	// Token: 0x02000161 RID: 353
	internal static class ClientConfigLoader
	{
		// Token: 0x060011BE RID: 4542 RVA: 0x0003E208 File Offset: 0x0003C408
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

		// Token: 0x060011BF RID: 4543 RVA: 0x0003E260 File Offset: 0x0003C460
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

		// Token: 0x060011C0 RID: 4544 RVA: 0x0003E3F8 File Offset: 0x0003C5F8
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

		// Token: 0x060011C1 RID: 4545 RVA: 0x0003E60C File Offset: 0x0003C80C
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

		// Token: 0x060011C2 RID: 4546 RVA: 0x0003E7D0 File Offset: 0x0003C9D0
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

		// Token: 0x060011C3 RID: 4547 RVA: 0x0003E828 File Offset: 0x0003CA28
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

		// Token: 0x060011C4 RID: 4548 RVA: 0x0003ECA8 File Offset: 0x0003CEA8
		private static void AdjustConfigurationBasedOnAppSettings(IDictionary<int, object> config)
		{
			ClientConfigLoader.LoadAppSettingsIntValue(196609, "AS_HttpStreamBufferSize", new Predicate<int>(ClientConfigLoader.IsValidValueForStreamBufferSize), 1024, config);
			ClientConfigLoader.LoadAppSettingsIntValue(196610, "AS_TcpStreamBufferSize", new Predicate<int>(ClientConfigLoader.IsValidValueForStreamBufferSize), 1024, config);
			ClientConfigLoader.LoadAppSettingsIntValue(196611, "AS_EndSessionTimeout", new Predicate<int>(ClientConfigLoader.IsValidValueForTimeout), 1, config);
			ClientConfigLoader.LoadAppSettingsLongValue(196613, "AS_HttpClientPayloadQueueLimit", new Predicate<long>(ClientConfigLoader.IsValidValueForHttpClientPayloadQueueLimit), 1024L, config);
			ClientConfigLoader.LoadBinaryXmlSupportValueFromAppSettings(config);
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0003ED3C File Offset: 0x0003CF3C
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

		// Token: 0x060011C6 RID: 4550 RVA: 0x0003EDA0 File Offset: 0x0003CFA0
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

		// Token: 0x060011C7 RID: 4551 RVA: 0x0003EE04 File Offset: 0x0003D004
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

		// Token: 0x060011C8 RID: 4552 RVA: 0x0003EE58 File Offset: 0x0003D058
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

		// Token: 0x060011C9 RID: 4553 RVA: 0x0003EED0 File Offset: 0x0003D0D0
		private static void LoadBoolValueFromEnvironment(int entry, string envVariable, IDictionary<int, object> config)
		{
			bool flag;
			if (ConvertHelper.TryParseBool(Environment.GetEnvironmentVariable(envVariable), false, out flag))
			{
				config[entry] = flag;
			}
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0003EEFC File Offset: 0x0003D0FC
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

		// Token: 0x060011CB RID: 4555 RVA: 0x0003EF6C File Offset: 0x0003D16C
		private static bool IsValidValueForStreamBufferSize(int value)
		{
			return value > 0 && value <= 10240;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0003EF7F File Offset: 0x0003D17F
		private static bool IsValidValueForTimeout(int value)
		{
			return value >= -1;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0003EF88 File Offset: 0x0003D188
		private static bool IsValidValueForHttpClientPayloadQueueLimit(long value)
		{
			return value > 0L && value <= 131072L;
		}

		// Token: 0x04000B35 RID: 2869
		private const string DefaultAppSettingsJsonFile = "AnalysisServices.AppSettings.json";

		// Token: 0x04000B36 RID: 2870
		private const string JsonPropertyName_Root = "asConfiguration";

		// Token: 0x04000B37 RID: 2871
		private const string JsonPropertyName_Runtime = "runtime";

		// Token: 0x04000B38 RID: 2872
		private const string JsonPropertyName_Runtime_IsProcessWithUI = "isProcessWithUI";

		// Token: 0x04000B39 RID: 2873
		private const string JsonPropertyName_Runtime_InfoRestrictionNeeded = "infoRestrictionNeeded";

		// Token: 0x04000B3A RID: 2874
		private const string JsonPropertyName_Authentication = "authentication";

		// Token: 0x04000B3B RID: 2875
		private const string JsonPropertyName_Authentication_UnrestrictedFallbackToInteractiveFlow = "unrestrictedFallbackToInteractiveFlow";

		// Token: 0x04000B3C RID: 2876
		private const string JsonPropertyName_Authentication_DisableWamBasedSSO = "disableWamBasedSSO";

		// Token: 0x04000B3D RID: 2877
		private const string JsonPropertyName_Connectivity = "connectivity";

		// Token: 0x04000B3E RID: 2878
		private const string JsonPropertyName_Connectivity_HttpStreamBufferSize = "httpStreamBufferSize";

		// Token: 0x04000B3F RID: 2879
		private const string JsonPropertyName_Connectivity_TcpStreamBufferSize = "tcpStreamBufferSize";

		// Token: 0x04000B40 RID: 2880
		private const string JsonPropertyName_Connectivity_EndSessionTimeout = "endSessionTimeout";

		// Token: 0x04000B41 RID: 2881
		private const string JsonPropertyName_Connectivity_HttpClientSupport = "httpClientSupport";

		// Token: 0x04000B42 RID: 2882
		private const string JsonPropertyName_Connectivity_HttpClientPayloadQueueLimit = "httpClientPayloadQueueLimit";

		// Token: 0x04000B43 RID: 2883
		private const string JsonPropertyName_Connectivity_BinaryXmlSupport = "binaryXmlSupport";

		// Token: 0x04000B44 RID: 2884
		private const string JsonPropertyName_Diagnostics = "diagnostics";

		// Token: 0x04000B45 RID: 2885
		private const string JsonPropertyName_Diagnostics_RootDirectoryPath = "rootDirectoryPath";

		// Token: 0x04000B46 RID: 2886
		private const string JsonPropertyName_Diagnostics_TrackUndisposedConnections = "trackUndisposedConnections";

		// Token: 0x04000B47 RID: 2887
		private const string JsonPropertyName_Diagnostics_TraceXmlaMessages = "traceXmlaMessages";

		// Token: 0x04000B48 RID: 2888
		private const string JsonPropertyName_Diagnostics_TraceXmlaHttpMessages = "traceXmlaHttpMessages";

		// Token: 0x04000B49 RID: 2889
		private const string AppSettingValue_AppSettingsOverridePath = "AS_AppSettingsPath";

		// Token: 0x04000B4A RID: 2890
		private const string AppSettingValue_HttpStreamBufferSize = "AS_HttpStreamBufferSize";

		// Token: 0x04000B4B RID: 2891
		private const string AppSettingValue_TcpStreamBufferSize = "AS_TcpStreamBufferSize";

		// Token: 0x04000B4C RID: 2892
		private const string AppSettingValue_EndSessionTimeout = "AS_EndSessionTimeout";

		// Token: 0x04000B4D RID: 2893
		private const string AppSettingValue_HttpClientPayloadQueueLimit = "AS_HttpClientPayloadQueueLimit";

		// Token: 0x04000B4E RID: 2894
		private const string AppSettingValue_BinaryXmlSupport = "AS_BinaryXmlSupport";

		// Token: 0x04000B4F RID: 2895
		private const string EnvironmentValue_AppSettingsOverridePath = "MS_AS_AppSettingsPath";

		// Token: 0x04000B50 RID: 2896
		private const string EnvironmentValue_UnrestrictedFallbackToInteractiveFlow = "MS_AS_UnrestrictedFallbackToInteractiveFlow";

		// Token: 0x04000B51 RID: 2897
		private const string EnvironmentValue_DisableWamBasedSSO = "MS_AS_DisableWamBasedSSO";

		// Token: 0x04000B52 RID: 2898
		private const string EnvironmentValue_BinaryXmlSupport = "MS_AS_BinaryXmlSupport";

		// Token: 0x04000B53 RID: 2899
		private const string EnvironmentValue_ForceUseBinaryXML = "AS_FORCE_USE_BINARY_XML";

		// Token: 0x04000B54 RID: 2900
		private const string EnvironmentValue_InfoRestrictionNeeded = "MS_AS_InfoRestrictionNeeded";

		// Token: 0x04000B55 RID: 2901
		private const int DefaultHttpStreamBufferSize = 131072;

		// Token: 0x04000B56 RID: 2902
		private const int DefaultTcpStreamBufferSize = 8192;

		// Token: 0x04000B57 RID: 2903
		private const int DefaultEndSessionTimeout = 30000;

		// Token: 0x04000B58 RID: 2904
		private const long DefaultHttpClientPayloadQueueLimit = 2097152L;

		// Token: 0x04000B59 RID: 2905
		private const string DefaultBaseDiagnosticsDirectoryPath = "ASClientDiagnostics";

		// Token: 0x04000B5A RID: 2906
		private static readonly DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(ClientConfigLoader.Config));

		// Token: 0x020001E5 RID: 485
		[DataContract]
		private sealed class Config
		{
			// Token: 0x17000670 RID: 1648
			// (get) Token: 0x06001419 RID: 5145 RVA: 0x0004527F File Offset: 0x0004347F
			// (set) Token: 0x0600141A RID: 5146 RVA: 0x00045287 File Offset: 0x00043487
			[DataMember(Name = "asConfiguration")]
			public ClientConfigLoader.ASConfiguration Configuration { get; set; }
		}

		// Token: 0x020001E6 RID: 486
		[DataContract(Name = "asConfiguration")]
		private sealed class ASConfiguration
		{
			// Token: 0x17000671 RID: 1649
			// (get) Token: 0x0600141C RID: 5148 RVA: 0x00045298 File Offset: 0x00043498
			// (set) Token: 0x0600141D RID: 5149 RVA: 0x000452A0 File Offset: 0x000434A0
			[DataMember(Name = "runtime")]
			public ClientConfigLoader.ASRuntimeConfiguration Runtime { get; set; }

			// Token: 0x17000672 RID: 1650
			// (get) Token: 0x0600141E RID: 5150 RVA: 0x000452A9 File Offset: 0x000434A9
			// (set) Token: 0x0600141F RID: 5151 RVA: 0x000452B1 File Offset: 0x000434B1
			[DataMember(Name = "authentication")]
			public ClientConfigLoader.ASAuthenticationConfiguration Authentication { get; set; }

			// Token: 0x17000673 RID: 1651
			// (get) Token: 0x06001420 RID: 5152 RVA: 0x000452BA File Offset: 0x000434BA
			// (set) Token: 0x06001421 RID: 5153 RVA: 0x000452C2 File Offset: 0x000434C2
			[DataMember(Name = "connectivity")]
			public ClientConfigLoader.ASConnectivityConfiguration Connectivity { get; set; }

			// Token: 0x17000674 RID: 1652
			// (get) Token: 0x06001422 RID: 5154 RVA: 0x000452CB File Offset: 0x000434CB
			// (set) Token: 0x06001423 RID: 5155 RVA: 0x000452D3 File Offset: 0x000434D3
			[DataMember(Name = "diagnostics")]
			public ClientConfigLoader.ASDiagnosticsConfiguration Diagnostics { get; set; }
		}

		// Token: 0x020001E7 RID: 487
		[DataContract(Name = "runtime")]
		private sealed class ASRuntimeConfiguration
		{
			// Token: 0x17000675 RID: 1653
			// (get) Token: 0x06001425 RID: 5157 RVA: 0x000452E4 File Offset: 0x000434E4
			// (set) Token: 0x06001426 RID: 5158 RVA: 0x000452EC File Offset: 0x000434EC
			[DataMember(Name = "isProcessWithUI")]
			public bool? IsProcessWithUI { get; set; }

			// Token: 0x17000676 RID: 1654
			// (get) Token: 0x06001427 RID: 5159 RVA: 0x000452F5 File Offset: 0x000434F5
			// (set) Token: 0x06001428 RID: 5160 RVA: 0x000452FD File Offset: 0x000434FD
			[DataMember(Name = "infoRestrictionNeeded")]
			public bool? InfoRestrictionNeeded { get; set; }
		}

		// Token: 0x020001E8 RID: 488
		[DataContract(Name = "authentication")]
		private sealed class ASAuthenticationConfiguration
		{
			// Token: 0x17000677 RID: 1655
			// (get) Token: 0x0600142A RID: 5162 RVA: 0x0004530E File Offset: 0x0004350E
			// (set) Token: 0x0600142B RID: 5163 RVA: 0x00045316 File Offset: 0x00043516
			[DataMember(Name = "unrestrictedFallbackToInteractiveFlow")]
			public bool? UnrestrictedFallbackToInteractiveFlow { get; set; }

			// Token: 0x17000678 RID: 1656
			// (get) Token: 0x0600142C RID: 5164 RVA: 0x0004531F File Offset: 0x0004351F
			// (set) Token: 0x0600142D RID: 5165 RVA: 0x00045327 File Offset: 0x00043527
			[DataMember(Name = "disableWamBasedSSO")]
			public bool? DisableWamBasedSSO { get; set; }
		}

		// Token: 0x020001E9 RID: 489
		[DataContract(Name = "connectivity")]
		private sealed class ASConnectivityConfiguration
		{
			// Token: 0x17000679 RID: 1657
			// (get) Token: 0x0600142F RID: 5167 RVA: 0x00045338 File Offset: 0x00043538
			// (set) Token: 0x06001430 RID: 5168 RVA: 0x00045340 File Offset: 0x00043540
			[DataMember(Name = "httpStreamBufferSize")]
			public int? HttpStreamBufferSize { get; set; }

			// Token: 0x1700067A RID: 1658
			// (get) Token: 0x06001431 RID: 5169 RVA: 0x00045349 File Offset: 0x00043549
			// (set) Token: 0x06001432 RID: 5170 RVA: 0x00045351 File Offset: 0x00043551
			[DataMember(Name = "tcpStreamBufferSize")]
			public int? TcpStreamBufferSize { get; set; }

			// Token: 0x1700067B RID: 1659
			// (get) Token: 0x06001433 RID: 5171 RVA: 0x0004535A File Offset: 0x0004355A
			// (set) Token: 0x06001434 RID: 5172 RVA: 0x00045362 File Offset: 0x00043562
			[DataMember(Name = "endSessionTimeout")]
			public int? EndSessionTimeout { get; set; }

			// Token: 0x1700067C RID: 1660
			// (get) Token: 0x06001435 RID: 5173 RVA: 0x0004536B File Offset: 0x0004356B
			// (set) Token: 0x06001436 RID: 5174 RVA: 0x00045373 File Offset: 0x00043573
			[DataMember(Name = "httpClientSupport")]
			public bool? HttpClientSupport { get; set; }

			// Token: 0x1700067D RID: 1661
			// (get) Token: 0x06001437 RID: 5175 RVA: 0x0004537C File Offset: 0x0004357C
			// (set) Token: 0x06001438 RID: 5176 RVA: 0x00045384 File Offset: 0x00043584
			[DataMember(Name = "httpClientPayloadQueueLimit")]
			public long? HttpClientPayloadQueueLimit { get; set; }

			// Token: 0x1700067E RID: 1662
			// (get) Token: 0x06001439 RID: 5177 RVA: 0x0004538D File Offset: 0x0004358D
			// (set) Token: 0x0600143A RID: 5178 RVA: 0x00045395 File Offset: 0x00043595
			[DataMember(Name = "binaryXmlSupport")]
			public string BinaryXmlSupport { get; set; }
		}

		// Token: 0x020001EA RID: 490
		[DataContract(Name = "diagnostics")]
		private sealed class ASDiagnosticsConfiguration
		{
			// Token: 0x1700067F RID: 1663
			// (get) Token: 0x0600143C RID: 5180 RVA: 0x000453A6 File Offset: 0x000435A6
			// (set) Token: 0x0600143D RID: 5181 RVA: 0x000453AE File Offset: 0x000435AE
			[DataMember(Name = "rootDirectoryPath")]
			public string RootDirectoryPath { get; set; }

			// Token: 0x17000680 RID: 1664
			// (get) Token: 0x0600143E RID: 5182 RVA: 0x000453B7 File Offset: 0x000435B7
			// (set) Token: 0x0600143F RID: 5183 RVA: 0x000453BF File Offset: 0x000435BF
			[DataMember(Name = "trackUndisposedConnections")]
			public bool? TrackUndisposedConnections { get; set; }

			// Token: 0x17000681 RID: 1665
			// (get) Token: 0x06001440 RID: 5184 RVA: 0x000453C8 File Offset: 0x000435C8
			// (set) Token: 0x06001441 RID: 5185 RVA: 0x000453D0 File Offset: 0x000435D0
			[DataMember(Name = "traceXmlaMessages")]
			public bool? TraceXmlaMessages { get; set; }

			// Token: 0x17000682 RID: 1666
			// (get) Token: 0x06001442 RID: 5186 RVA: 0x000453D9 File Offset: 0x000435D9
			// (set) Token: 0x06001443 RID: 5187 RVA: 0x000453E1 File Offset: 0x000435E1
			[DataMember(Name = "traceXmlaHttpMessages")]
			public bool? TraceXmlaHttpMessages { get; set; }
		}
	}
}
