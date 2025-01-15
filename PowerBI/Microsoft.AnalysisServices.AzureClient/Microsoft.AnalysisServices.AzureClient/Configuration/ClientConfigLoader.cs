using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.AnalysisServices.AzureClient.Runtime;
using Microsoft.AnalysisServices.AzureClient.Utilities;

namespace Microsoft.AnalysisServices.AzureClient.Configuration
{
	// Token: 0x02000045 RID: 69
	internal static class ClientConfigLoader
	{
		// Token: 0x060001FE RID: 510 RVA: 0x00009D70 File Offset: 0x00007F70
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

		// Token: 0x060001FF RID: 511 RVA: 0x00009DC8 File Offset: 0x00007FC8
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

		// Token: 0x06000200 RID: 512 RVA: 0x00009F60 File Offset: 0x00008160
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

		// Token: 0x06000201 RID: 513 RVA: 0x0000A174 File Offset: 0x00008374
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

		// Token: 0x06000202 RID: 514 RVA: 0x0000A338 File Offset: 0x00008538
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

		// Token: 0x06000203 RID: 515 RVA: 0x0000A390 File Offset: 0x00008590
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

		// Token: 0x06000204 RID: 516 RVA: 0x0000A810 File Offset: 0x00008A10
		private static void AdjustConfigurationBasedOnAppSettings(IDictionary<int, object> config)
		{
			ClientConfigLoader.LoadAppSettingsIntValue(196609, "AS_HttpStreamBufferSize", new Predicate<int>(ClientConfigLoader.IsValidValueForStreamBufferSize), 1024, config);
			ClientConfigLoader.LoadAppSettingsIntValue(196610, "AS_TcpStreamBufferSize", new Predicate<int>(ClientConfigLoader.IsValidValueForStreamBufferSize), 1024, config);
			ClientConfigLoader.LoadAppSettingsIntValue(196611, "AS_EndSessionTimeout", new Predicate<int>(ClientConfigLoader.IsValidValueForTimeout), 1, config);
			ClientConfigLoader.LoadAppSettingsLongValue(196613, "AS_HttpClientPayloadQueueLimit", new Predicate<long>(ClientConfigLoader.IsValidValueForHttpClientPayloadQueueLimit), 1024L, config);
			ClientConfigLoader.LoadBinaryXmlSupportValueFromAppSettings(config);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000A8A4 File Offset: 0x00008AA4
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

		// Token: 0x06000206 RID: 518 RVA: 0x0000A908 File Offset: 0x00008B08
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

		// Token: 0x06000207 RID: 519 RVA: 0x0000A96C File Offset: 0x00008B6C
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

		// Token: 0x06000208 RID: 520 RVA: 0x0000A9C0 File Offset: 0x00008BC0
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

		// Token: 0x06000209 RID: 521 RVA: 0x0000AA38 File Offset: 0x00008C38
		private static void LoadBoolValueFromEnvironment(int entry, string envVariable, IDictionary<int, object> config)
		{
			bool flag;
			if (ConvertHelper.TryParseBool(Environment.GetEnvironmentVariable(envVariable), false, out flag))
			{
				config[entry] = flag;
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000AA64 File Offset: 0x00008C64
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

		// Token: 0x0600020B RID: 523 RVA: 0x0000AAD4 File Offset: 0x00008CD4
		private static bool IsValidValueForStreamBufferSize(int value)
		{
			return value > 0 && value <= 10240;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000AAE7 File Offset: 0x00008CE7
		private static bool IsValidValueForTimeout(int value)
		{
			return value >= -1;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000AAF0 File Offset: 0x00008CF0
		private static bool IsValidValueForHttpClientPayloadQueueLimit(long value)
		{
			return value > 0L && value <= 131072L;
		}

		// Token: 0x0400012D RID: 301
		private const string DefaultAppSettingsJsonFile = "AnalysisServices.AppSettings.json";

		// Token: 0x0400012E RID: 302
		private const string JsonPropertyName_Root = "asConfiguration";

		// Token: 0x0400012F RID: 303
		private const string JsonPropertyName_Runtime = "runtime";

		// Token: 0x04000130 RID: 304
		private const string JsonPropertyName_Runtime_IsProcessWithUI = "isProcessWithUI";

		// Token: 0x04000131 RID: 305
		private const string JsonPropertyName_Runtime_InfoRestrictionNeeded = "infoRestrictionNeeded";

		// Token: 0x04000132 RID: 306
		private const string JsonPropertyName_Authentication = "authentication";

		// Token: 0x04000133 RID: 307
		private const string JsonPropertyName_Authentication_UnrestrictedFallbackToInteractiveFlow = "unrestrictedFallbackToInteractiveFlow";

		// Token: 0x04000134 RID: 308
		private const string JsonPropertyName_Authentication_DisableWamBasedSSO = "disableWamBasedSSO";

		// Token: 0x04000135 RID: 309
		private const string JsonPropertyName_Connectivity = "connectivity";

		// Token: 0x04000136 RID: 310
		private const string JsonPropertyName_Connectivity_HttpStreamBufferSize = "httpStreamBufferSize";

		// Token: 0x04000137 RID: 311
		private const string JsonPropertyName_Connectivity_TcpStreamBufferSize = "tcpStreamBufferSize";

		// Token: 0x04000138 RID: 312
		private const string JsonPropertyName_Connectivity_EndSessionTimeout = "endSessionTimeout";

		// Token: 0x04000139 RID: 313
		private const string JsonPropertyName_Connectivity_HttpClientSupport = "httpClientSupport";

		// Token: 0x0400013A RID: 314
		private const string JsonPropertyName_Connectivity_HttpClientPayloadQueueLimit = "httpClientPayloadQueueLimit";

		// Token: 0x0400013B RID: 315
		private const string JsonPropertyName_Connectivity_BinaryXmlSupport = "binaryXmlSupport";

		// Token: 0x0400013C RID: 316
		private const string JsonPropertyName_Diagnostics = "diagnostics";

		// Token: 0x0400013D RID: 317
		private const string JsonPropertyName_Diagnostics_RootDirectoryPath = "rootDirectoryPath";

		// Token: 0x0400013E RID: 318
		private const string JsonPropertyName_Diagnostics_TrackUndisposedConnections = "trackUndisposedConnections";

		// Token: 0x0400013F RID: 319
		private const string JsonPropertyName_Diagnostics_TraceXmlaMessages = "traceXmlaMessages";

		// Token: 0x04000140 RID: 320
		private const string JsonPropertyName_Diagnostics_TraceXmlaHttpMessages = "traceXmlaHttpMessages";

		// Token: 0x04000141 RID: 321
		private const string AppSettingValue_AppSettingsOverridePath = "AS_AppSettingsPath";

		// Token: 0x04000142 RID: 322
		private const string AppSettingValue_HttpStreamBufferSize = "AS_HttpStreamBufferSize";

		// Token: 0x04000143 RID: 323
		private const string AppSettingValue_TcpStreamBufferSize = "AS_TcpStreamBufferSize";

		// Token: 0x04000144 RID: 324
		private const string AppSettingValue_EndSessionTimeout = "AS_EndSessionTimeout";

		// Token: 0x04000145 RID: 325
		private const string AppSettingValue_HttpClientPayloadQueueLimit = "AS_HttpClientPayloadQueueLimit";

		// Token: 0x04000146 RID: 326
		private const string AppSettingValue_BinaryXmlSupport = "AS_BinaryXmlSupport";

		// Token: 0x04000147 RID: 327
		private const string EnvironmentValue_AppSettingsOverridePath = "MS_AS_AppSettingsPath";

		// Token: 0x04000148 RID: 328
		private const string EnvironmentValue_UnrestrictedFallbackToInteractiveFlow = "MS_AS_UnrestrictedFallbackToInteractiveFlow";

		// Token: 0x04000149 RID: 329
		private const string EnvironmentValue_DisableWamBasedSSO = "MS_AS_DisableWamBasedSSO";

		// Token: 0x0400014A RID: 330
		private const string EnvironmentValue_BinaryXmlSupport = "MS_AS_BinaryXmlSupport";

		// Token: 0x0400014B RID: 331
		private const string EnvironmentValue_ForceUseBinaryXML = "AS_FORCE_USE_BINARY_XML";

		// Token: 0x0400014C RID: 332
		private const string EnvironmentValue_InfoRestrictionNeeded = "MS_AS_InfoRestrictionNeeded";

		// Token: 0x0400014D RID: 333
		private const int DefaultHttpStreamBufferSize = 131072;

		// Token: 0x0400014E RID: 334
		private const int DefaultTcpStreamBufferSize = 8192;

		// Token: 0x0400014F RID: 335
		private const int DefaultEndSessionTimeout = 30000;

		// Token: 0x04000150 RID: 336
		private const long DefaultHttpClientPayloadQueueLimit = 2097152L;

		// Token: 0x04000151 RID: 337
		private const string DefaultBaseDiagnosticsDirectoryPath = "ASClientDiagnostics";

		// Token: 0x04000152 RID: 338
		private static readonly DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(ClientConfigLoader.Config));

		// Token: 0x0200007A RID: 122
		[DataContract]
		private sealed class Config
		{
			// Token: 0x17000093 RID: 147
			// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000C593 File Offset: 0x0000A793
			// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000C59B File Offset: 0x0000A79B
			[DataMember(Name = "asConfiguration")]
			public ClientConfigLoader.ASConfiguration Configuration { get; set; }
		}

		// Token: 0x0200007B RID: 123
		[DataContract(Name = "asConfiguration")]
		private sealed class ASConfiguration
		{
			// Token: 0x17000094 RID: 148
			// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000C5AC File Offset: 0x0000A7AC
			// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000C5B4 File Offset: 0x0000A7B4
			[DataMember(Name = "runtime")]
			public ClientConfigLoader.ASRuntimeConfiguration Runtime { get; set; }

			// Token: 0x17000095 RID: 149
			// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000C5BD File Offset: 0x0000A7BD
			// (set) Token: 0x060002F6 RID: 758 RVA: 0x0000C5C5 File Offset: 0x0000A7C5
			[DataMember(Name = "authentication")]
			public ClientConfigLoader.ASAuthenticationConfiguration Authentication { get; set; }

			// Token: 0x17000096 RID: 150
			// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000C5CE File Offset: 0x0000A7CE
			// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000C5D6 File Offset: 0x0000A7D6
			[DataMember(Name = "connectivity")]
			public ClientConfigLoader.ASConnectivityConfiguration Connectivity { get; set; }

			// Token: 0x17000097 RID: 151
			// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000C5DF File Offset: 0x0000A7DF
			// (set) Token: 0x060002FA RID: 762 RVA: 0x0000C5E7 File Offset: 0x0000A7E7
			[DataMember(Name = "diagnostics")]
			public ClientConfigLoader.ASDiagnosticsConfiguration Diagnostics { get; set; }
		}

		// Token: 0x0200007C RID: 124
		[DataContract(Name = "runtime")]
		private sealed class ASRuntimeConfiguration
		{
			// Token: 0x17000098 RID: 152
			// (get) Token: 0x060002FC RID: 764 RVA: 0x0000C5F8 File Offset: 0x0000A7F8
			// (set) Token: 0x060002FD RID: 765 RVA: 0x0000C600 File Offset: 0x0000A800
			[DataMember(Name = "isProcessWithUI")]
			public bool? IsProcessWithUI { get; set; }

			// Token: 0x17000099 RID: 153
			// (get) Token: 0x060002FE RID: 766 RVA: 0x0000C609 File Offset: 0x0000A809
			// (set) Token: 0x060002FF RID: 767 RVA: 0x0000C611 File Offset: 0x0000A811
			[DataMember(Name = "infoRestrictionNeeded")]
			public bool? InfoRestrictionNeeded { get; set; }
		}

		// Token: 0x0200007D RID: 125
		[DataContract(Name = "authentication")]
		private sealed class ASAuthenticationConfiguration
		{
			// Token: 0x1700009A RID: 154
			// (get) Token: 0x06000301 RID: 769 RVA: 0x0000C622 File Offset: 0x0000A822
			// (set) Token: 0x06000302 RID: 770 RVA: 0x0000C62A File Offset: 0x0000A82A
			[DataMember(Name = "unrestrictedFallbackToInteractiveFlow")]
			public bool? UnrestrictedFallbackToInteractiveFlow { get; set; }

			// Token: 0x1700009B RID: 155
			// (get) Token: 0x06000303 RID: 771 RVA: 0x0000C633 File Offset: 0x0000A833
			// (set) Token: 0x06000304 RID: 772 RVA: 0x0000C63B File Offset: 0x0000A83B
			[DataMember(Name = "disableWamBasedSSO")]
			public bool? DisableWamBasedSSO { get; set; }
		}

		// Token: 0x0200007E RID: 126
		[DataContract(Name = "connectivity")]
		private sealed class ASConnectivityConfiguration
		{
			// Token: 0x1700009C RID: 156
			// (get) Token: 0x06000306 RID: 774 RVA: 0x0000C64C File Offset: 0x0000A84C
			// (set) Token: 0x06000307 RID: 775 RVA: 0x0000C654 File Offset: 0x0000A854
			[DataMember(Name = "httpStreamBufferSize")]
			public int? HttpStreamBufferSize { get; set; }

			// Token: 0x1700009D RID: 157
			// (get) Token: 0x06000308 RID: 776 RVA: 0x0000C65D File Offset: 0x0000A85D
			// (set) Token: 0x06000309 RID: 777 RVA: 0x0000C665 File Offset: 0x0000A865
			[DataMember(Name = "tcpStreamBufferSize")]
			public int? TcpStreamBufferSize { get; set; }

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x0600030A RID: 778 RVA: 0x0000C66E File Offset: 0x0000A86E
			// (set) Token: 0x0600030B RID: 779 RVA: 0x0000C676 File Offset: 0x0000A876
			[DataMember(Name = "endSessionTimeout")]
			public int? EndSessionTimeout { get; set; }

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x0600030C RID: 780 RVA: 0x0000C67F File Offset: 0x0000A87F
			// (set) Token: 0x0600030D RID: 781 RVA: 0x0000C687 File Offset: 0x0000A887
			[DataMember(Name = "httpClientSupport")]
			public bool? HttpClientSupport { get; set; }

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x0600030E RID: 782 RVA: 0x0000C690 File Offset: 0x0000A890
			// (set) Token: 0x0600030F RID: 783 RVA: 0x0000C698 File Offset: 0x0000A898
			[DataMember(Name = "httpClientPayloadQueueLimit")]
			public long? HttpClientPayloadQueueLimit { get; set; }

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x06000310 RID: 784 RVA: 0x0000C6A1 File Offset: 0x0000A8A1
			// (set) Token: 0x06000311 RID: 785 RVA: 0x0000C6A9 File Offset: 0x0000A8A9
			[DataMember(Name = "binaryXmlSupport")]
			public string BinaryXmlSupport { get; set; }
		}

		// Token: 0x0200007F RID: 127
		[DataContract(Name = "diagnostics")]
		private sealed class ASDiagnosticsConfiguration
		{
			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x06000313 RID: 787 RVA: 0x0000C6BA File Offset: 0x0000A8BA
			// (set) Token: 0x06000314 RID: 788 RVA: 0x0000C6C2 File Offset: 0x0000A8C2
			[DataMember(Name = "rootDirectoryPath")]
			public string RootDirectoryPath { get; set; }

			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x06000315 RID: 789 RVA: 0x0000C6CB File Offset: 0x0000A8CB
			// (set) Token: 0x06000316 RID: 790 RVA: 0x0000C6D3 File Offset: 0x0000A8D3
			[DataMember(Name = "trackUndisposedConnections")]
			public bool? TrackUndisposedConnections { get; set; }

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x06000317 RID: 791 RVA: 0x0000C6DC File Offset: 0x0000A8DC
			// (set) Token: 0x06000318 RID: 792 RVA: 0x0000C6E4 File Offset: 0x0000A8E4
			[DataMember(Name = "traceXmlaMessages")]
			public bool? TraceXmlaMessages { get; set; }

			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x06000319 RID: 793 RVA: 0x0000C6ED File Offset: 0x0000A8ED
			// (set) Token: 0x0600031A RID: 794 RVA: 0x0000C6F5 File Offset: 0x0000A8F5
			[DataMember(Name = "traceXmlaHttpMessages")]
			public bool? TraceXmlaHttpMessages { get; set; }
		}
	}
}
