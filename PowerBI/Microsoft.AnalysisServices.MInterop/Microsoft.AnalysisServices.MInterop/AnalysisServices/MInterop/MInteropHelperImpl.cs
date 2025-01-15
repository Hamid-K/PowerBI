using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Microsoft.AnalysisServices.PlatformHost;
using Microsoft.ASPaaS.AnalysisServer.Interfaces.Common.MInterop;
using Microsoft.Data.Mashup;
using Microsoft.Data.Mashup.Preview;
using Microsoft.Mashup.OAuth;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x0200002D RID: 45
	internal static class MInteropHelperImpl
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000654F File Offset: 0x0000474F
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00006556 File Offset: 0x00004756
		public static ServerType ServerType { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000655E File Offset: 0x0000475E
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00006565 File Offset: 0x00004765
		public static bool UseManagedOracleProvider { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000656D File Offset: 0x0000476D
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00006574 File Offset: 0x00004774
		public static bool UseMicrosoftDataSqlClient { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000657C File Offset: 0x0000477C
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00006583 File Offset: 0x00004783
		private static MashupConnectionPool MashupOAuthPool { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000658B File Offset: 0x0000478B
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00006592 File Offset: 0x00004792
		internal static IEngineTracer EngineTracer { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000659A File Offset: 0x0000479A
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000065A1 File Offset: 0x000047A1
		internal static IJwtTokenHelperMInterop JwtTokenHelper { get; private set; }

		// Token: 0x0600010F RID: 271 RVA: 0x000065AC File Offset: 0x000047AC
		internal static string PrepareMashupPackageForPowerBI(string minimizedProgram, bool useSection1Formula)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (Package package = Package.Open(memoryStream, FileMode.Create))
				{
					Uri uri = (useSection1Formula ? new Uri("/Formulas/" + Uri.EscapeDataString("Section1.m"), UriKind.Relative) : new Uri("/Formulas/" + Uri.EscapeDataString("main.m"), UriKind.Relative));
					using (Stream stream = package.CreatePart(uri, "application/x-ms-m", CompressionOption.Maximum).GetStream(FileMode.Create, FileAccess.Write))
					{
						byte[] bytes = Encoding.UTF8.GetBytes(minimizedProgram);
						stream.Write(bytes, 0, bytes.Length);
					}
					Uri uri2 = new Uri("/Config/" + Uri.EscapeDataString("Package.xml"), UriKind.Relative);
					using (Stream stream2 = package.CreatePart(uri2, "text/xml", CompressionOption.Maximum).GetStream(FileMode.Create, FileAccess.Write))
					{
						byte[] bytes2 = Encoding.UTF8.GetBytes("<?xml version=\"1.0\" encoding=\"utf-8\"?><Package xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Version>1.5.3296.2082</Version><MinVersion>1.5.3296.0</MinVersion><Culture>en-US</Culture></Package>");
						stream2.Write(bytes2, 0, bytes2.Length);
					}
					package.Flush();
					package.Close();
				}
				text = Convert.ToBase64String(memoryStream.ToArray());
			}
			return text;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006738 File Offset: 0x00004938
		public static void Initialize(IJwtTokenHelperMInterop jwtTokenHelper, IEngineTracer tracer, MEngineSettings settings, MEngineSettingsForContainerPool processingPoolSettings, ServerType serverType, string additionalExtensions, string disabledExtensions, string customExtensionDirectories = "", bool customExtensionDirectoryWatchForChanges = false, bool customExtensionDirectoryIncludeSubdirectories = false, string certifiedExtensionsAllowlist = "", string certifiedExtensionDirectories = "", bool certifiedExtensionDirectoryWatchForChanges = false, bool certifiedExtensionDirectoryIncludeSubdirectories = false)
		{
			MInteropHelperImpl.ServerType = serverType;
			if (MInteropHelperImpl.ServerType != ServerType.PBIDedicated && MInteropHelperImpl.ServerType != ServerType.PowerBI)
			{
				MInteropHelperImpl.ApplyGlobalSettings(settings, MInteropHelperImpl.ServerType);
				MInteropHelperImpl.ApplyProcessingPoolSettings(processingPoolSettings);
				MInteropHelperImpl.StartOAuthContainerPool(settings);
				ModuleLoader.LoadModules(additionalExtensions, disabledExtensions, customExtensionDirectories, customExtensionDirectoryIncludeSubdirectories, customExtensionDirectoryWatchForChanges, certifiedExtensionsAllowlist, certifiedExtensionDirectories, certifiedExtensionDirectoryWatchForChanges, certifiedExtensionDirectoryIncludeSubdirectories);
			}
			MInteropHelperImpl.UseManagedOracleProvider = settings.UseManagedOracleProvider;
			MInteropHelperImpl.UseMicrosoftDataSqlClient = settings.UseMicrosoftDataSqlClient;
			MInteropHelperImpl.EngineTracer = tracer;
			MInteropHelperImpl.JwtTokenHelper = jwtTokenHelper;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000067AC File Offset: 0x000049AC
		public static void DisposeHelper()
		{
			MInteropHelperImpl.StopOAuthContainerPool();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000067B4 File Offset: 0x000049B4
		public static string RemoveSecretsFromCredentialJson(string credentialJson, string dataSourceReferenceJson)
		{
			string kind = new DataSourceReference(dataSourceReferenceJson).DataSource.Kind;
			Credential credential = Credential.FromJson(credentialJson, "");
			credential.AuthenticationProperties = MInteropHelperImpl.RemoveSecretsFromCredentialProperties(Credential.FromJson(credentialJson, ""), kind);
			return credential.ToJson();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000067FC File Offset: 0x000049FC
		internal static Dictionary<string, object> RemoveSecretsFromCredentialProperties(Credential credential, string dataSourceKind)
		{
			List<string> list = (from credProp in DataSourceKindInfo.FromKind(dataSourceKind).AuthenticationInfos.SelectMany((AuthenticationInfo authInfo) => authInfo.Properties)
				where credProp.IsSecret
				select credProp.Name).Distinct<string>().ToList<string>();
			if (!list.Contains("ConnectionString"))
			{
				list.Add("ConnectionString");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>(credential.AuthenticationProperties, StringComparer.OrdinalIgnoreCase);
			foreach (string text in list)
			{
				if (string.Equals("ConnectionString", text, StringComparison.OrdinalIgnoreCase))
				{
					object obj = null;
					if (dictionary.TryGetValue("ConnectionString", out obj))
					{
						dictionary[text] = MInteropHelperImpl.RemoveSecretsFromConnectionString((string)obj);
					}
				}
				else
				{
					dictionary.Remove(text);
				}
			}
			return dictionary;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000692C File Offset: 0x00004B2C
		public static string RemoveSecretsFromConnectionString(string connectionString)
		{
			return MInteropHelperImpl.RemoveSecretsFromConnectionString(connectionString, false);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006938 File Offset: 0x00004B38
		private static string RemoveSecretsFromConnectionString(string connectionString, bool useOdbcRules)
		{
			DbConnectionStringBuilder connectionBuilder = new DbConnectionStringBuilder(useOdbcRules)
			{
				ConnectionString = connectionString
			};
			if (!connectionBuilder.ContainsKey("Extended Properties") && !MInteropHelperImpl.knownPasswordPropNames.Any((string prop) => connectionBuilder.ContainsKey(prop)))
			{
				return connectionString;
			}
			if (connectionBuilder.ContainsKey("Extended Properties"))
			{
				string text = (string)connectionBuilder["Extended Properties"];
				bool flag = false;
				if (connectionBuilder.ContainsKey("Provider") && MInteropHelperImpl.IsOLEDBForODBCProvider((string)connectionBuilder["Provider"]))
				{
					flag = true;
				}
				connectionBuilder["Extended Properties"] = MInteropHelperImpl.RemoveSecretsFromConnectionString(text, flag);
			}
			foreach (string text2 in MInteropHelperImpl.knownPasswordPropNames)
			{
				connectionBuilder.Remove(text2);
			}
			return connectionBuilder.ConnectionString;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006A54 File Offset: 0x00004C54
		public static bool IsOLEDBForODBCProvider(string provider)
		{
			return MInteropHelperImpl.ProvidersOLEDBForODBC.Any((string p) => p.Equals(provider, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006A84 File Offset: 0x00004C84
		public static void ConvertASCredentialsToM(string dataSourceName, string credentialProp, string unattendedUsername, string unattendedPassword, out string credentialsConverted, out int impersonationMode)
		{
			Credential credential = Credential.FromJson(credentialProp, "");
			impersonationMode = (int)credential.ImpersonationMode;
			credentialsConverted = credential.ToMJson(unattendedUsername, unattendedPassword, dataSourceName);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006AB4 File Offset: 0x00004CB4
		public static void ParseCredentials(string credentialsProp, out string usernameStr, out string passwordStr, out int impersonationMode)
		{
			Credential credential = Credential.FromJson(credentialsProp, "");
			impersonationMode = (int)credential.ImpersonationMode;
			usernameStr = credential.GetPropertyOrNull("Username") ?? "";
			passwordStr = credential.GetPropertyOrNull("Password") ?? "";
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006B04 File Offset: 0x00004D04
		public static void ConvertConnectionStringToDetails(string connectionString, string provider, int impersonationMode, string username, string password, out string connectionDetails, out string connectionCredentials)
		{
			MConnectionDetailsBuilder mconnectionDetailsBuilder = new MConnectionDetailsBuilder(connectionString, provider);
			connectionDetails = mconnectionDetailsBuilder.ToString();
			Credential credential = mconnectionDetailsBuilder.GetCredential();
			if (credential != null)
			{
				connectionCredentials = credential.ToJson();
				return;
			}
			connectionCredentials = Credential.FromDataSourceProperties((ImpersonationMode)impersonationMode, username, password).ToJson();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006B48 File Offset: 0x00004D48
		private static void ApplyGlobalSettings(MEngineSettings settings, ServerType serverType)
		{
			MashupConnectionHelper.AllowFileAccess = settings.AllowFileAccess;
			MashupConnectionHelper.AllowWindowsCredentials = settings.AllowWindowsCredentials;
			MashupConnectionHelper.EnablePreviewConnectionStringProperties = true;
			if (!settings.EnableHtmlExtension || serverType != ServerType.OnPrem)
			{
				if (serverType > ServerType.PowerBI)
				{
					throw new Exception("New Server Type added. Need to decide the error message explicitly.");
				}
				MashupConnectionHelper.AllowBrowserAccess = false;
			}
			MashupConnectionHelper.ContainerInheritsHostJob = settings.ContainerInheritsHostJob;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006B9C File Offset: 0x00004D9C
		private static void StartOAuthContainerPool(MEngineSettings settings)
		{
			if (settings.OAuthContainerPoolSize > 0)
			{
				MInteropHelperImpl.MashupOAuthPool = new MashupConnectionPool("AS.OAuthContainerPool")
				{
					ContainerMaxCount = settings.OAuthContainerPoolSize,
					ContainerMinCount = 0,
					ContainerInheritsHostJob = true
				};
				MInteropHelperImpl.MashupOAuthPool.Start();
				DataSourceSettingsHelper.OAuthContainerPool = "AS.OAuthContainerPool";
				DataSourceSettingsHelper.EnableOAuthContainer = true;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006BF5 File Offset: 0x00004DF5
		private static void StopOAuthContainerPool()
		{
			DataSourceSettingsHelper.EnableOAuthContainer = false;
			if (MInteropHelperImpl.MashupOAuthPool != null)
			{
				MInteropHelperImpl.MashupOAuthPool.TryStop();
				MInteropHelperImpl.MashupOAuthPool = null;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006C15 File Offset: 0x00004E15
		private static void ApplyProcessingPoolSettings(MEngineSettingsForContainerPool processingPoolSettings)
		{
			if (processingPoolSettings.MaxContainerCount > 0)
			{
				MashupConnection.ContainerMaxCount = processingPoolSettings.MaxContainerCount;
			}
			else
			{
				MashupConnection.ContainerMaxCount = 1000000;
			}
			if (processingPoolSettings.MinContainerCount > 0)
			{
				MashupConnectionHelper.ContainerMinCount = processingPoolSettings.MinContainerCount;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006C4C File Offset: 0x00004E4C
		public static void GetNamedDependencies(string MQuery, out string[] mashupDependencies, out string[] mashupDependenciesByDSResourcePath)
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			string text = "section main;\r\n";
			string text2 = MHelper.EscapeIdentifier(Guid.NewGuid().ToString());
			string text3 = string.Format("{0}\r\nshared {1} = {2}\r\n;", text, text2, MQuery);
			foreach (MashupDiscovery mashupDiscovery in new List<MashupDiscovery>(new MashupConnection(new MashupConnectionStringBuilder
			{
				Mashup = text3
			}.ToString()).FindReferencedDataSources()))
			{
				switch (mashupDiscovery.Kind)
				{
				case MashupDiscoveryKind.DataSource:
					list2.Add(mashupDiscovery.DataSourceReference.DataSource.ToString());
					break;
				case MashupDiscoveryKind.UnknownNativeQuery:
				{
					IEngineTracer engineTracer = MInteropHelperImpl.EngineTracer;
					if (engineTracer != null)
					{
						engineTracer.LogPrivateMessage(string.Format("GetNamedDependencies, ignoring UnknownNativeQuery '{0}'.", mashupDiscovery.FunctionName.MarkAsCustomerContent()));
					}
					break;
				}
				case MashupDiscoveryKind.UnknownFunction:
					list.Add(mashupDiscovery.FunctionName);
					break;
				case MashupDiscoveryKind.UnknownCallSite:
				{
					IEngineTracer engineTracer2 = MInteropHelperImpl.EngineTracer;
					if (engineTracer2 != null)
					{
						engineTracer2.LogPrivateMessage(string.Format("GetNamedDependencies, ignoring UnknownCallSite '{0}'.", mashupDiscovery.FunctionName.MarkAsCustomerContent()));
					}
					break;
				}
				case MashupDiscoveryKind.Unsupported:
				{
					IEngineTracer engineTracer3 = MInteropHelperImpl.EngineTracer;
					if (engineTracer3 != null)
					{
						engineTracer3.LogPrivateMessage(string.Format("GetNamedDependencies, ignoring Unsupported '{0}'.", mashupDiscovery.FunctionName.MarkAsCustomerContent()));
					}
					break;
				}
				default:
					throw EngineException.PFE_XA_DISCOVER_CALC_DEPENDENCY_ERROR();
				}
			}
			mashupDependencies = list.ToArray<string>();
			mashupDependenciesByDSResourcePath = list2.ToArray<string>();
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006DD8 File Offset: 0x00004FD8
		public static void GetEffectiveConnectionStringForPowerBI(string sharedQueryName, string MProgram, string MQuery, bool useScaffolding, bool minimize, ConnectionStringFlavor versionAndType, out string effectiveConnectionString)
		{
			string[] array;
			string[] array2;
			MParameterType[] array3;
			MInteropHelperImpl.GetEffectiveConnectionStringForPowerBIInternal(sharedQueryName, MProgram, MQuery, useScaffolding, versionAndType, null, minimize, out effectiveConnectionString, out array, out array2, out array3);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006DFC File Offset: 0x00004FFC
		private static void GetEffectiveConnectionStringForPowerBIInternal(string sharedQueryName, string MProgram, string MQuery, bool useScaffolding, ConnectionStringFlavor versionAndType, string[] parametersToLift, bool minimize, out string effectiveConnectionString, out string[] liftedQueries, out string[] mParameters, out MParameterType[] mParameterTypes)
		{
			string text = MHelper.EscapeIdentifier(sharedQueryName);
			string text2;
			if (useScaffolding)
			{
				text2 = string.Format("{0}\r\nshared {1} = {2}\r\n;", MProgram, text, MQuery);
			}
			else
			{
				text2 = MProgram;
			}
			string text3;
			if (minimize)
			{
				text3 = MashupSourceHelper.Minimize(text2, text);
			}
			else
			{
				text3 = text2;
			}
			liftedQueries = null;
			mParameters = null;
			mParameterTypes = null;
			if (parametersToLift != null && parametersToLift.Count<string>() > 0)
			{
				string text4;
				MInteropHelperImpl.LiftParameters(text3, parametersToLift, out text4, out liftedQueries, out mParameters, out mParameterTypes);
				text3 = text4;
			}
			effectiveConnectionString = MInteropHelperImpl.BuildMashupConnectionString(text3, sharedQueryName, versionAndType);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006E70 File Offset: 0x00005070
		public static void GetEffectiveConnectionStringWithLiftParameters(string sharedQueryName, string MProgram, string MQuery, bool useScaffolding, ConnectionStringFlavor versionAndType, string[] parametersToLift, out string effectiveConnectionString, out string[] liftedQueries, out string[] mParameters, out MParameterType[] mParameterTypes)
		{
			MInteropHelperImpl.GetEffectiveConnectionStringForPowerBIInternal(sharedQueryName, MProgram, MQuery, useScaffolding, versionAndType, parametersToLift, true, out effectiveConnectionString, out liftedQueries, out mParameters, out mParameterTypes);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006E94 File Offset: 0x00005094
		public static string ApplyParameterSubstitution(string connectionString, string[] paramNames, string[] paramValues)
		{
			if (paramNames.Length != paramValues.Length)
			{
				throw EngineException.PFE_INVALIDARG();
			}
			string text;
			try
			{
				text = MInteropHelperImpl.ApplyParametersMProgramInMashupConnectionString(connectionString, (string MProgram, bool isDMTS) => MInteropHelperImpl.ApplyParameterSubstitutionOnPackageSegment(MProgram, paramNames, paramValues));
			}
			catch (Exception ex)
			{
				throw EngineException.PFE_M_ENGINE_PARAMETER_SUBSTITUTION_ERROR(string.Format("Failed to modify the connection string due to error \"{0}\"", ex.GetType().ToString()));
			}
			return text;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006F10 File Offset: 0x00005110
		public static string AppendDataSourcePoolToConnectionString(string connectionString, string poolName)
		{
			if (string.IsNullOrEmpty(poolName))
			{
				return connectionString;
			}
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder
			{
				ConnectionString = connectionString
			};
			if (dbConnectionStringBuilder.ContainsKey("MashupConnectionString"))
			{
				DbConnectionStringBuilder dbConnectionStringBuilder2 = new DbConnectionStringBuilder
				{
					ConnectionString = (string)dbConnectionStringBuilder["MashupConnectionString"]
				};
				if (MInteropHelperImpl.IsMashupConnectionString(dbConnectionStringBuilder2))
				{
					dbConnectionStringBuilder2["DataSourcePool"] = poolName;
					dbConnectionStringBuilder["MashupConnectionString"] = dbConnectionStringBuilder2.ConnectionString;
					return dbConnectionStringBuilder.ConnectionString;
				}
				return connectionString;
			}
			else
			{
				if (MInteropHelperImpl.IsMashupConnectionString(dbConnectionStringBuilder))
				{
					dbConnectionStringBuilder["DataSourcePool"] = poolName;
					return dbConnectionStringBuilder.ConnectionString;
				}
				return connectionString;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006FA8 File Offset: 0x000051A8
		public static string AddParameterToConnectionString(string connectionString, string paramName, string paramValue, bool append = false, bool appendToGatewayConnectionString = true)
		{
			if (string.IsNullOrEmpty(paramName) || string.IsNullOrEmpty(paramValue))
			{
				return connectionString;
			}
			DbConnectionStringBuilderEx dbConnectionStringBuilderEx = new DbConnectionStringBuilderEx
			{
				ConnectionString = connectionString
			};
			if (dbConnectionStringBuilderEx.IsDMTSConnection())
			{
				if (appendToGatewayConnectionString)
				{
					DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder
					{
						ConnectionString = dbConnectionStringBuilderEx.GatewayConnectionString
					};
					if (!MInteropHelperImpl.IsMashupWrapperConnectionString(dbConnectionStringBuilder))
					{
						if (dbConnectionStringBuilder.ContainsKey(paramName) && !string.IsNullOrEmpty(dbConnectionStringBuilderEx[paramName].ToString()) && append)
						{
							DbConnectionStringBuilder dbConnectionStringBuilder2 = dbConnectionStringBuilder;
							DbConnectionStringBuilder dbConnectionStringBuilder3 = dbConnectionStringBuilder2;
							object obj = dbConnectionStringBuilder2[paramName];
							dbConnectionStringBuilder3[paramName] = ((obj != null) ? obj.ToString() : null) + "," + paramValue;
						}
						else
						{
							dbConnectionStringBuilder[paramName] = paramValue;
						}
						dbConnectionStringBuilderEx.GatewayConnectionString = dbConnectionStringBuilder.ConnectionString;
					}
				}
			}
			else if (MInteropHelperImpl.IsMashupWrapperConnectionString(dbConnectionStringBuilderEx))
			{
				DbConnectionStringBuilder dbConnectionStringBuilder4 = new DbConnectionStringBuilder
				{
					ConnectionString = (string)dbConnectionStringBuilderEx["MashupConnectionString"]
				};
				if (MInteropHelperImpl.IsMashupConnectionString(dbConnectionStringBuilder4))
				{
					if (dbConnectionStringBuilder4.ContainsKey(paramName) && !string.IsNullOrEmpty(dbConnectionStringBuilder4[paramName].ToString()) && append)
					{
						DbConnectionStringBuilder dbConnectionStringBuilder2 = dbConnectionStringBuilder4;
						DbConnectionStringBuilder dbConnectionStringBuilder5 = dbConnectionStringBuilder2;
						object obj2 = dbConnectionStringBuilder2[paramName];
						dbConnectionStringBuilder5[paramName] = ((obj2 != null) ? obj2.ToString() : null) + "," + paramValue;
					}
					else
					{
						dbConnectionStringBuilder4[paramName] = paramValue;
					}
					dbConnectionStringBuilderEx["MashupConnectionString"] = dbConnectionStringBuilder4.ConnectionString;
				}
			}
			else if (dbConnectionStringBuilderEx.ContainsKey(paramName) && !string.IsNullOrEmpty(dbConnectionStringBuilderEx[paramName].ToString()) && append)
			{
				DbConnectionStringBuilderEx dbConnectionStringBuilderEx2 = dbConnectionStringBuilderEx;
				DbConnectionStringBuilder dbConnectionStringBuilder6 = dbConnectionStringBuilderEx2;
				object obj3 = dbConnectionStringBuilderEx2[paramName];
				dbConnectionStringBuilder6[paramName] = ((obj3 != null) ? obj3.ToString() : null) + "," + paramValue;
			}
			else
			{
				dbConnectionStringBuilderEx[paramName] = paramValue;
			}
			return dbConnectionStringBuilderEx.ConnectionString;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007170 File Offset: 0x00005370
		internal static string ApplyParametersMProgramInMashupConnectionString(string connectionString, MInteropHelperImpl.MProgramModifier mProgramModifier)
		{
			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw EngineException.PFE_INVALIDARG();
			}
			DbConnectionStringBuilderEx dbConnectionStringBuilderEx = new DbConnectionStringBuilderEx
			{
				ConnectionString = connectionString
			};
			bool flag = dbConnectionStringBuilderEx.IsDMTSConnection();
			if (flag)
			{
				DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder
				{
					ConnectionString = dbConnectionStringBuilderEx.GatewayConnectionString
				};
				if (!dbConnectionStringBuilder.ContainsKey("Provider"))
				{
					throw EngineException.PFE_INVALIDARG(new Exception("DMTS connection string is expected to have provider in the gateway connection string"));
				}
				if (!(dbConnectionStringBuilder["Provider"] as string).StartsWith("Microsoft.Mashup.OleDb."))
				{
					throw EngineException.PFE_INVALIDARG(new Exception("DMTS connection string should use mashup provider."));
				}
				if (!dbConnectionStringBuilder.ContainsKey("Extended Properties"))
				{
					throw EngineException.PFE_INVALIDARG(new Exception("Extended properties not found in DMTS connection string."));
				}
				MInteropHelperImpl.ModifyMProgram(dbConnectionStringBuilder, "Extended Properties", flag, mProgramModifier);
				dbConnectionStringBuilderEx.GatewayConnectionString = dbConnectionStringBuilder.ConnectionString;
			}
			else if (dbConnectionStringBuilderEx.ContainsKey("Package"))
			{
				MInteropHelperImpl.ModifyMProgram(dbConnectionStringBuilderEx, "Package", flag, mProgramModifier);
			}
			else if (dbConnectionStringBuilderEx.ContainsKey("MashupConnectionString"))
			{
				DbConnectionStringBuilder dbConnectionStringBuilder2 = new DbConnectionStringBuilder
				{
					ConnectionString = (string)dbConnectionStringBuilderEx["MashupConnectionString"]
				};
				if (dbConnectionStringBuilder2.ContainsKey("Package"))
				{
					MInteropHelperImpl.ModifyMProgram(dbConnectionStringBuilder2, "Package", flag, mProgramModifier);
					dbConnectionStringBuilderEx["MashupConnectionString"] = dbConnectionStringBuilder2.ConnectionString;
				}
			}
			return dbConnectionStringBuilderEx.ConnectionString;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000072B0 File Offset: 0x000054B0
		private static void ModifyMProgram(DbConnectionStringBuilder connectionStringBuilder, string packagePropertyName, bool isDMTSConnection, MInteropHelperImpl.MProgramModifier mProgramModifier)
		{
			bool flag;
			string mprogram = MInteropHelperImpl.GetMProgram((string)connectionStringBuilder[packagePropertyName], out flag);
			string text = mProgramModifier(mprogram, isDMTSConnection);
			if (mprogram != text)
			{
				connectionStringBuilder[packagePropertyName] = MInteropHelperImpl.PrepareMashupPackageForPowerBI(text, flag);
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000072EC File Offset: 0x000054EC
		private static string ApplyParameterSubstitutionOnPackageSegment(string minimizedMProgram, string[] paramNames, string[] paramValues)
		{
			Dictionary<string, string> sharedMembers = MHelper.GetSharedMembers(minimizedMProgram);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			for (int i = 0; i < paramNames.Length; i++)
			{
				if (!sharedMembers.ContainsKey(paramNames[i]))
				{
					IEngineTracer engineTracer = MInteropHelperImpl.EngineTracer;
					if (engineTracer != null)
					{
						engineTracer.LogPrivateMessage(string.Format("ApplyParameterSubstitutionOnPackageSegment, parameter {0} not found in M program: {1}", paramNames[i].MarkAsCustomerContent(), minimizedMProgram.RedactSensitiveStrings().MarkAsCustomerContent()));
					}
					throw EngineException.PFE_INVALIDARG();
				}
				dictionary[paramNames[i]] = paramValues[i];
			}
			return MHelper.ReplaceSharedMembers(minimizedMProgram, dictionary);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00007368 File Offset: 0x00005568
		private static bool IsMashupConnectionString(DbConnectionStringBuilder connectionStringBuilder)
		{
			string text = (connectionStringBuilder.ContainsKey("Provider") ? (connectionStringBuilder["Provider"] as string) : string.Empty);
			return connectionStringBuilder.ContainsKey("Mashup") || connectionStringBuilder.ContainsKey("Package") || "Microsoft.PowerBI.OleDb".Equals(text, StringComparison.OrdinalIgnoreCase) || "Microsoft.Mashup.OleDb.1".Equals(text, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000073D0 File Offset: 0x000055D0
		private static bool IsMashupWrapperConnectionString(DbConnectionStringBuilder connectionStringBuilder)
		{
			return connectionStringBuilder.ContainsKey("MashupConnectionString");
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000073E0 File Offset: 0x000055E0
		public static void ExtractCubeApplyParameters(string mProgram, out string defaultParametersInDocument)
		{
			if (string.IsNullOrWhiteSpace(mProgram))
			{
				throw EngineException.PFE_INVALIDARG();
			}
			int num;
			int num2;
			if (!CubeApplyParametersHelper.TryGetCubeTransformLocation(mProgram, ref num, ref num2))
			{
				throw EngineException.PFE_M_CUBEAPPLYPARAMETER_INTERNAL("Cube transform snippet location not found in given mDocument.", null);
			}
			string text = mProgram.Substring(num, num2);
			if (string.IsNullOrEmpty(text))
			{
				throw EngineException.PFE_M_CUBEAPPLYPARAMETER_INTERNAL("Empty cube transform snippet found in given m document.", null);
			}
			defaultParametersInDocument = CubeApplyParametersHelper.GetCubeApplyParameters(text);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00007438 File Offset: 0x00005638
		public static void ReplaceCubeApplyParameters(string connectionString, out string newConnectionString, out string defaultParametersInDocument)
		{
			string tmpDefaultParametersInDocument = null;
			newConnectionString = MInteropHelperImpl.ApplyParametersMProgramInMashupConnectionString(connectionString, delegate(string minimizedMProgram, bool isDMTS)
			{
				if (!isDMTS)
				{
					throw EngineException.PFE_M_CUBEAPPLYPARAMETER_INTERNAL("CubeApplyParameters - neither mashup nor gateway connection string detected to replace CubeApplyParameters");
				}
				string text;
				MInteropHelperImpl.GetNeutralMDocument(minimizedMProgram, out text, out tmpDefaultParametersInDocument);
				return text;
			});
			defaultParametersInDocument = tmpDefaultParametersInDocument;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000746E File Offset: 0x0000566E
		public static string OverrideMAttribute(string attributeBlock, string attribute, string attrValue)
		{
			return MHelper.SetRecordField(attributeBlock, attribute, attrValue);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00007478 File Offset: 0x00005678
		public static void ClearStaticAnalysisBatch(string batchKey)
		{
			foreach (object obj in Enum.GetValues(typeof(MEngineDiscoveryOptions)))
			{
				MEngineDiscoveryOptions mengineDiscoveryOptions = (MEngineDiscoveryOptions)obj;
				MEngineProgramForAnalysis.ClearBatch(batchKey, mengineDiscoveryOptions);
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000074DC File Offset: 0x000056DC
		internal static void GetNeutralMDocument(string mProgram, out string mDocumentWithoutParams, out string parametersInDocument)
		{
			int num;
			int num2;
			if (!CubeApplyParametersHelper.TryGetCubeTransformLocation(mProgram, ref num, ref num2))
			{
				throw EngineException.PFE_M_CUBEAPPLYPARAMETER_INTERNAL("Cube transform snippet location not found in given mDocument.", null);
			}
			string text = mProgram.Substring(num, num2);
			if (string.IsNullOrEmpty(text))
			{
				throw EngineException.PFE_M_CUBEAPPLYPARAMETER_INTERNAL("Empty cube transform snippet found in given m document.", null);
			}
			string text2 = "{}";
			string text3 = CubeApplyParametersHelper.SetCubeApplyParameters(text, text2);
			if (string.IsNullOrEmpty(text3))
			{
				throw EngineException.PFE_M_CUBEAPPLYPARAMETER_INTERNAL("Empty cube transform snippet found after empty params apply.", null);
			}
			parametersInDocument = CubeApplyParametersHelper.GetCubeApplyParameters(text);
			mDocumentWithoutParams = mProgram.Substring(0, num) + text3 + mProgram.Substring(num + num2);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007564 File Offset: 0x00005764
		internal static void LiftParameters(string MProgram, string[] parametersToLift, out string liftedMProgram, out string[] liftedEntities, out string[] mParamters, out MParameterType[] mParameterTypes)
		{
			liftedMProgram = MProgram;
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			List<MParameterType> list3 = new List<MParameterType>();
			if (!string.IsNullOrWhiteSpace(MProgram) && parametersToLift != null && parametersToLift.Length != 0)
			{
				Dictionary<string, string[]> dictionary = MHelper.LiftMembers(MProgram, parametersToLift.ToList<string>(), ref liftedMProgram);
				if (dictionary != null && dictionary.Count > 0)
				{
					list = dictionary.Keys.ToList<string>();
					foreach (KeyValuePair<string, Dictionary<string, object>> keyValuePair in MHelper.GetParameterQueries(MProgram))
					{
						object obj;
						if (keyValuePair.Value.TryGetValue("Type", out obj))
						{
							list2.Add(keyValuePair.Key);
							list3.Add(MInteropHelperImpl.GetMParameterType(obj));
						}
					}
				}
			}
			liftedEntities = list.ToArray();
			mParamters = list2.ToArray();
			mParameterTypes = list3.ToArray();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007650 File Offset: 0x00005850
		internal static void GetMParameterType(string parameterExpression, out string parameterType)
		{
			parameterType = string.Empty;
			object obj;
			if (!string.IsNullOrWhiteSpace(parameterExpression) && MHelper.GetParameterQueries(string.Format("section section1; shared Parameter = {0};", parameterExpression)).FirstOrDefault<KeyValuePair<string, Dictionary<string, object>>>().Value.TryGetValue("Type", out obj))
			{
				parameterType = obj.ToString();
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000076A0 File Offset: 0x000058A0
		public static string[] GetDataSourcesUsedByQuery(string MProgram, string MQuery, string table, string partition)
		{
			MEngineProgramForAnalysis mengineProgramForAnalysis = new MEngineProgramForAnalysis(MProgram, MQuery, MStaticAnalysisMode.Simple, null);
			HashSet<string> resourcePaths = new HashSet<string>();
			bool flag = false;
			bool flag2 = false;
			MInteropHelperImpl.CollectAndTransformDataSources(mengineProgramForAnalysis, null, delegate(MashupDiscovery d, ISet<MEngineLibrarySymbol> s)
			{
				resourcePaths.Add(DSRConversionHelper.CreateCompleteResourcePath(d.DataSourceReference));
			}, table, partition, false, false, out flag, out flag2);
			List<string> list = resourcePaths.ToList<string>();
			list.Sort();
			return list.ToArray();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000076FC File Offset: 0x000058FC
		public static string[] GetDataSourcesUsedByProgram(string MProgram)
		{
			MEngineProgramForAnalysis mengineProgramForAnalysis = new MEngineProgramForAnalysis(MProgram, MStaticAnalysisMode.Simple, null);
			List<string> resourcePaths = new List<string>();
			bool flag = false;
			bool flag2 = false;
			MInteropHelperImpl.CollectAndTransformDataSources(mengineProgramForAnalysis, null, delegate(MashupDiscovery d, ISet<MEngineLibrarySymbol> s)
			{
				resourcePaths.Add(DSRConversionHelper.CreateCompleteResourcePath(d.DataSourceReference));
			}, string.Empty, string.Empty, false, false, out flag, out flag2);
			resourcePaths.Sort();
			return resourcePaths.ToArray();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00007760 File Offset: 0x00005960
		public static List<DQDataSourceKind> GetDataSourceKindsUsedByQuery(string MProgram, string MQuery, string table, string partition)
		{
			HashSet<DQDataSourceKind> dataSourceKinds = new HashSet<DQDataSourceKind>();
			Exception ex = null;
			try
			{
				MEngineProgramForAnalysis mengineProgramForAnalysis = new MEngineProgramForAnalysis(MProgram, MQuery, MStaticAnalysisMode.Simple, null);
				try
				{
					bool flag = false;
					bool flag2 = false;
					MInteropHelperImpl.CollectAndTransformDataSources(mengineProgramForAnalysis, null, delegate(MashupDiscovery d, ISet<MEngineLibrarySymbol> s)
					{
						dataSourceKinds.Add(MInteropHelperImpl.GetDataSourceKind(d.DataSourceReference));
					}, table, partition, false, false, out flag, out flag2);
				}
				catch (Exception ex)
				{
				}
			}
			catch (Exception ex2)
			{
				if (dataSourceKinds.Count > 0)
				{
					return dataSourceKinds.ToList<DQDataSourceKind>();
				}
				throw ex2;
			}
			List<DQDataSourceKind> list = dataSourceKinds.ToList<DQDataSourceKind>();
			if (ex != null && list.Count == 0)
			{
				throw ex;
			}
			return list;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00007810 File Offset: 0x00005A10
		public static void GetDataSourceReferenceJsonsForV3(string batchKey, string MProgram, string MQuery, string table, string partition, bool importOnlyPartition, bool allowUnknownFunctions, bool allowUnknownCallsites, out string[] dsrs, out string[] options, out string[] analyzableDocs, out DQDataSourceKind[] dataSourceKinds, out string[] dsrKinds, out string[] dsrPaths, out bool unknownFunctionsFound, out bool unknownCallsitesFound, out bool foundPdfTables, out bool sensitivityLabelsSupported, out bool foundExcelWorkbooks, out bool prePostSqlForDQ, out bool fabricOneLakeDS)
		{
			MEngineProgramForAnalysis programForAnalysis = new MEngineProgramForAnalysis(MProgram, MQuery, MStaticAnalysisMode.Batch, batchKey);
			List<string> tmpDsrs = new List<string>();
			List<string> tmpOptions = new List<string>();
			List<string> tmpAnalyzableDocs = new List<string>();
			List<string> tmpDsrKinds = new List<string>();
			List<string> tmpDsrPaths = new List<string>();
			List<DQDataSourceKind> tmpdataSourceKinds = new List<DQDataSourceKind>();
			bool flag = false;
			bool flag2 = false;
			bool tmpSensitivityLabelsSupported = false;
			bool tmpfoundPdfTables = false;
			bool tmpFoundExcelWorkbooks = false;
			bool tmpPrePostSqlForDQ = false;
			bool tmpFabricOneLakeDS = false;
			MInteropHelperImpl.CollectAndTransformDataSources(programForAnalysis, MQuery, delegate(MashupDiscovery d, ISet<MEngineLibrarySymbol> s)
			{
				if (d.DataSourceReference != null)
				{
					List<MashupDiscovery> list = new List<MashupDiscovery>();
					list.Add(d);
					string text = d.DataSourceReference.ToJson();
					string text2 = DataSourceReferenceExtensions.MakeAnalyzableDocument(list);
					bool flag3 = s.Contains(MEngineLibrarySymbol.WebPage);
					if (flag3)
					{
						text2 += "\r\nshared #\"48e8a057-cb8e-4c58-be5e-7556bcad440c\" = Web.Page(Text.ToBinary(\"<html></html>\"));";
					}
					tmpDsrs.Add(text);
					tmpAnalyzableDocs.Add(MInteropHelperImpl.BuildMashupConnectionString(text2, importOnlyPartition ? text : null, ConnectionStringFlavor.PreV3));
					tmpdataSourceKinds.Add(MInteropHelperImpl.GetDataSourceKind(d.DataSourceReference));
					tmpDsrKinds.Add(d.DataSourceReference.DataSource.Kind);
					tmpDsrPaths.Add(d.DataSourceReference.DataSource.Path);
					tmpFoundExcelWorkbooks = s.Contains(MEngineLibrarySymbol.ExcelWorkbook);
					tmpSensitivityLabelsSupported = !flag3 && (tmpFoundExcelWorkbooks || string.Equals(d.DataSourceReference.DataSource.Kind, "SQL", StringComparison.InvariantCultureIgnoreCase));
					tmpfoundPdfTables = s.Contains(MEngineLibrarySymbol.PdfTables);
					DataSourceReference dataSourceReference = d.DataSourceReference;
					string text3;
					if (dataSourceReference == null)
					{
						text3 = null;
					}
					else
					{
						DataSource dataSource = dataSourceReference.DataSource;
						text3 = ((dataSource != null) ? dataSource.Kind : null);
					}
					if (text3 == "SQL")
					{
						Dictionary<string, object> dictionary = ((d.Options != null) ? MInteropHelperImpl.Serializer.Deserialize<Dictionary<string, object>>(d.Options) : new Dictionary<string, object>());
						Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
						object obj;
						if (dictionary.TryGetValue("MultiSubnetFailover", out obj) && (bool)obj)
						{
							dictionary2["MultiSubnetFailover"] = obj;
						}
						tmpOptions.Add(MInteropHelperImpl.Serializer.Serialize(dictionary2));
						return;
					}
					DataSourceReference dataSourceReference2 = d.DataSourceReference;
					string text4;
					if (dataSourceReference2 == null)
					{
						text4 = null;
					}
					else
					{
						DataSource dataSource2 = dataSourceReference2.DataSource;
						text4 = ((dataSource2 != null) ? dataSource2.Kind : null);
					}
					if (text4 == "Snowflake")
					{
						tmpPrePostSqlForDQ = true;
						return;
					}
					DataSourceReference dataSourceReference3 = d.DataSourceReference;
					string text5;
					if (dataSourceReference3 == null)
					{
						text5 = null;
					}
					else
					{
						DataSource dataSource3 = dataSourceReference3.DataSource;
						text5 = ((dataSource3 != null) ? dataSource3.Kind : null);
					}
					if (text5 == "AzureDataLakeStorage")
					{
						tmpFabricOneLakeDS = MInteropHelperImpl.IsFabricOneLakeDS(d.DataSourceReference.Address);
						return;
					}
				}
				else
				{
					IEngineTracer engineTracer = MInteropHelperImpl.EngineTracer;
					if (engineTracer == null)
					{
						return;
					}
					engineTracer.LogPrivateMessage(string.Format("GetDataSourceReferenceJsonsForV3 Static analyis discovery with null DSR found. This might be due to native queries. M Progam follows: {0}.", programForAnalysis.MProgram.RedactSensitiveStrings().MarkAsCustomerContent()));
				}
			}, table, partition, allowUnknownFunctions, allowUnknownCallsites, out flag, out flag2);
			dsrs = tmpDsrs.ToArray();
			options = tmpOptions.ToArray();
			analyzableDocs = tmpAnalyzableDocs.ToArray();
			dataSourceKinds = tmpdataSourceKinds.ToArray();
			dsrKinds = tmpDsrKinds.ToArray();
			dsrPaths = tmpDsrPaths.ToArray();
			unknownFunctionsFound = flag;
			unknownCallsitesFound = flag2;
			foundPdfTables = tmpfoundPdfTables;
			sensitivityLabelsSupported = tmpSensitivityLabelsSupported;
			foundExcelWorkbooks = tmpFoundExcelWorkbooks;
			prePostSqlForDQ = tmpPrePostSqlForDQ;
			fabricOneLakeDS = tmpFabricOneLakeDS;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00007950 File Offset: 0x00005B50
		public static void GetMinimizedPartitionMProgram(string MProgram, string MQuery, out string minimizedMProgram)
		{
			string text;
			minimizedMProgram = MInteropHelperImpl.GetMinimizedMProgram(MProgram, MQuery, out text);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007968 File Offset: 0x00005B68
		internal static string GetMProgram(string encodedMPackage, out bool useSection1Formula)
		{
			string @string;
			using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(encodedMPackage)))
			{
				using (Package package = Package.Open(memoryStream, FileMode.Open))
				{
					Uri uri = new Uri("/Formulas/" + Uri.EscapeDataString("Section1.m"), UriKind.Relative);
					Uri uri2 = new Uri("/Formulas/" + Uri.EscapeDataString("main.m"), UriKind.Relative);
					useSection1Formula = package.PartExists(uri);
					using (Stream stream = (useSection1Formula ? package.GetPart(uri) : package.GetPart(uri2)).GetStream())
					{
						byte[] array = new byte[stream.Length];
						stream.Read(array, 0, (int)stream.Length);
						stream.Close();
						@string = Encoding.UTF8.GetString(array);
					}
				}
			}
			return @string;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007A68 File Offset: 0x00005C68
		private static bool IsFabricOneLakeDS(IEnumerable<AddressPart> addressParts)
		{
			if (!addressParts.Any<AddressPart>())
			{
				return false;
			}
			string value = addressParts.FirstOrDefault((AddressPart a) => a.Name == "server").Value;
			if (value == null || !MInteropHelperImpl.FabricOneLakeFQDNRegex.IsMatch(value))
			{
				return false;
			}
			string value2 = addressParts.FirstOrDefault((AddressPart a) => a.Name == "path").Value;
			if (value2 == null)
			{
				return false;
			}
			MatchCollection matchCollection = MInteropHelperImpl.FabricOneLakePathRegex.Matches(value2);
			if (matchCollection.Count != 1 || matchCollection[0].Groups.Count != 3)
			{
				return false;
			}
			GroupCollection groups = matchCollection[0].Groups;
			Guid guid;
			return Guid.TryParse(groups[1].Value, out guid) && Guid.TryParse(groups[2].Value, out guid);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007B5C File Offset: 0x00005D5C
		private static string GetDSRAddressField(DataSourceReference dsr, string fieldName)
		{
			return (from ap in dsr.Address
				where ap.Name.ToLowerInvariant() == fieldName
				select ap.Value).First<string>();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007BB8 File Offset: 0x00005DB8
		public static void GetServerAndDatabase(string dataSourceReferenceJson, out string server, out string database)
		{
			DataSourceReference dataSourceReference = new DataSourceReference(dataSourceReferenceJson);
			server = MInteropHelperImpl.GetDSRAddressField(dataSourceReference, "server");
			database = null;
			if (dataSourceReference.DataSource.Kind == "SQL")
			{
				database = MInteropHelperImpl.GetDSRAddressField(dataSourceReference, "database");
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007C00 File Offset: 0x00005E00
		public static DQDataSourceKind GetDataSourceKind(string dataSourceReferenceJson)
		{
			DataSourceReference dataSourceReference = new DataSourceReference(dataSourceReferenceJson);
			DQDataSourceKind dqdataSourceKind = DQDataSourceKind.OTHER;
			string kind = dataSourceReference.DataSource.Kind;
			if (!(kind == "SQL"))
			{
				if (!(kind == "Teradata"))
				{
					if (!(kind == "Oracle"))
					{
						if (kind == "PowerBI")
						{
							dqdataSourceKind = DQDataSourceKind.DATAFLOWS;
						}
					}
					else
					{
						dqdataSourceKind = DQDataSourceKind.ORACLE;
					}
				}
				else
				{
					dqdataSourceKind = DQDataSourceKind.TERADATA;
				}
			}
			else
			{
				dqdataSourceKind = DQDataSourceKind.SQL;
			}
			return dqdataSourceKind;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007C68 File Offset: 0x00005E68
		public static void GetNormalizedResourcePathByKindPathJson(string kindPathJson, out string normalizedResourcePath)
		{
			string text = kindPathJson.ToLower();
			Dictionary<string, string> dictionary = MInteropHelperImpl.Serializer.Deserialize<Dictionary<string, string>>(text);
			string text2;
			string text3;
			if (dictionary.TryGetValue("kind", out text2) && dictionary.TryGetValue("path", out text3))
			{
				MInteropHelperImpl.GetNormalizedResourcePathByKindPath(text2, text3, out normalizedResourcePath);
				return;
			}
			throw EngineException.PFE_M_ENGINE_INVALID_RESOURCE_PATH(kindPathJson);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007CB5 File Offset: 0x00005EB5
		public static void GetNormalizedResourcePathByKindPath(string kind, string path, out string normalizedResourcePath)
		{
			normalizedResourcePath = string.Format("kind:{0},path:{1}", kind, path);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00007CC8 File Offset: 0x00005EC8
		private static MParameterType GetMParameterType(object parameterTypeObj)
		{
			MParameterType mparameterType = MParameterType.UNKNOWN;
			string text = parameterTypeObj as string;
			if (text != null)
			{
				if (!(text == "Number"))
				{
					if (!(text == "Text"))
					{
						if (!(text == "Date"))
						{
							if (!(text == "DateTime"))
							{
								if (!(text == "DateTimeZone"))
								{
									if (text == "Logical")
									{
										mparameterType = MParameterType.LOGICAL;
									}
								}
								else
								{
									mparameterType = MParameterType.DATETIMEZONE;
								}
							}
							else
							{
								mparameterType = MParameterType.DATETIME;
							}
						}
						else
						{
							mparameterType = MParameterType.DATE;
						}
					}
					else
					{
						mparameterType = MParameterType.TEXT;
					}
				}
				else
				{
					mparameterType = MParameterType.NUMBER;
				}
			}
			return mparameterType;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007D48 File Offset: 0x00005F48
		public static DQDataSourceKind GetDataSourceKind(DataSourceReference dataSourceReference)
		{
			DQDataSourceKind dqdataSourceKind = DQDataSourceKind.OTHER;
			string kind = dataSourceReference.DataSource.Kind;
			if (kind != null)
			{
				switch (kind.Length)
				{
				case 1:
					if (kind == "R")
					{
						dqdataSourceKind = DQDataSourceKind.R;
					}
					break;
				case 2:
					if (kind == "MQ")
					{
						dqdataSourceKind = DQDataSourceKind.MQ;
					}
					break;
				case 3:
				{
					char c = kind[0];
					if (c <= 'F')
					{
						if (c != 'D')
						{
							if (c == 'F')
							{
								if (kind == "Ftp")
								{
									dqdataSourceKind = DQDataSourceKind.FTP;
								}
							}
						}
						else if (kind == "DB2")
						{
							dqdataSourceKind = DQDataSourceKind.DB2;
						}
					}
					else if (c != 'S')
					{
						if (c == 'W')
						{
							if (kind == "Web")
							{
								dqdataSourceKind = DQDataSourceKind.WEB;
							}
						}
					}
					else if (kind == "SQL")
					{
						dqdataSourceKind = DQDataSourceKind.SQL;
					}
					break;
				}
				case 4:
				{
					char c = kind[0];
					if (c != 'F')
					{
						if (c != 'H')
						{
							if (c == 'O')
							{
								if (kind == "Odbc")
								{
									dqdataSourceKind = DQDataSourceKind.ODBC;
								}
							}
						}
						else if (kind == "Hdfs")
						{
							dqdataSourceKind = DQDataSourceKind.HDFS;
						}
					}
					else if (kind == "File")
					{
						dqdataSourceKind = DQDataSourceKind.FILE;
					}
					break;
				}
				case 5:
				{
					char c = kind[1];
					if (c <= 'l')
					{
						if (c != 'D')
						{
							if (c == 'l')
							{
								if (kind == "OleDb")
								{
									dqdataSourceKind = DQDataSourceKind.OLEDB;
								}
							}
						}
						else if (kind == "OData")
						{
							dqdataSourceKind = DQDataSourceKind.ODATA;
						}
					}
					else if (c != 'p')
					{
						if (c == 'y')
						{
							if (kind == "MySql")
							{
								dqdataSourceKind = DQDataSourceKind.MYSQL;
							}
						}
					}
					else if (kind == "Spark")
					{
						dqdataSourceKind = DQDataSourceKind.SPARK;
					}
					break;
				}
				case 6:
				{
					char c = kind[0];
					if (c != 'F')
					{
						if (c != 'O')
						{
							if (c == 'S')
							{
								if (kind == "Sybase")
								{
									dqdataSourceKind = DQDataSourceKind.SYBASE;
								}
							}
						}
						else if (kind == "Oracle")
						{
							dqdataSourceKind = DQDataSourceKind.ORACLE;
						}
					}
					else if (kind == "Folder")
					{
						dqdataSourceKind = DQDataSourceKind.FOLDER;
					}
					break;
				}
				case 7:
				{
					char c = kind[0];
					if (c != 'P')
					{
						if (c == 'S')
						{
							if (kind == "SapHana")
							{
								dqdataSourceKind = DQDataSourceKind.SAPHANA;
							}
						}
					}
					else if (kind == "PowerBI")
					{
						dqdataSourceKind = DQDataSourceKind.DATAFLOWS;
					}
					break;
				}
				case 8:
				{
					char c = kind[0];
					if (c != 'F')
					{
						if (c != 'I')
						{
							if (c == 'T')
							{
								if (kind == "Teradata")
								{
									dqdataSourceKind = DQDataSourceKind.TERADATA;
								}
							}
						}
						else if (kind == "Informix")
						{
							dqdataSourceKind = DQDataSourceKind.INFORMIX;
						}
					}
					else if (kind == "Facebook")
					{
						dqdataSourceKind = DQDataSourceKind.FACEBOOK;
					}
					break;
				}
				case 9:
				{
					char c = kind[0];
					if (c != 'A')
					{
						if (c != 'H')
						{
							if (c == 'S')
							{
								if (kind == "Snowflake")
								{
									dqdataSourceKind = DQDataSourceKind.SNOWFLAKE;
								}
							}
						}
						else if (kind == "HDInsight")
						{
							dqdataSourceKind = DQDataSourceKind.HDINSIGHT;
						}
					}
					else if (kind == "AdoDotNet")
					{
						dqdataSourceKind = DQDataSourceKind.ADODOTNET;
					}
					break;
				}
				case 10:
				{
					char c = kind[7];
					if (c <= 'i')
					{
						if (c != 'S')
						{
							if (c != 'c')
							{
								if (c == 'i')
								{
									if (kind == "SharePoint")
									{
										dqdataSourceKind = DQDataSourceKind.SHAREPOINT;
									}
								}
							}
							else if (kind == "Databricks")
							{
								dqdataSourceKind = DQDataSourceKind.DATABRICKS;
							}
						}
						else if (kind == "PostgreSQL")
						{
							dqdataSourceKind = DQDataSourceKind.POSTGRESQL;
						}
					}
					else if (c != 'k')
					{
						if (c != 'o')
						{
							if (c == 'r')
							{
								if (kind == "Salesforce")
								{
									dqdataSourceKind = DQDataSourceKind.SALESFORCE;
								}
							}
						}
						else if (kind == "AzureBlobs")
						{
							dqdataSourceKind = DQDataSourceKind.AZUREBLOBS;
						}
					}
					else if (kind == "DataMarket")
					{
						dqdataSourceKind = DQDataSourceKind.DATAMARKET;
					}
					break;
				}
				case 11:
					if (kind == "AzureTables")
					{
						dqdataSourceKind = DQDataSourceKind.AZURETABLES;
					}
					break;
				case 14:
				{
					char c = kind[1];
					if (c != 'd')
					{
						if (c != 'm')
						{
							if (c == 'o')
							{
								if (kind == "GoogleBigQuery")
								{
									dqdataSourceKind = DQDataSourceKind.GOOGLEBIGQUERY;
								}
							}
						}
						else if (kind == "AmazonRedshift")
						{
							dqdataSourceKind = DQDataSourceKind.AMAZONREDSHIFT;
						}
					}
					else if (kind == "AdobeAnalytics")
					{
						dqdataSourceKind = DQDataSourceKind.ADOBEANALYTICS;
					}
					break;
				}
				case 15:
				{
					char c = kind[0];
					if (c != 'A')
					{
						if (c != 'C')
						{
							if (c == 'G')
							{
								if (kind == "GoogleAnalytics")
								{
									dqdataSourceKind = DQDataSourceKind.GOOGLEANALYTICS;
								}
							}
						}
						else if (kind == "CurrentWorkbook")
						{
							dqdataSourceKind = DQDataSourceKind.CURRENTWORKBOOK;
						}
					}
					else if (kind == "ActiveDirectory")
					{
						dqdataSourceKind = DQDataSourceKind.ACTIVEDIRECTORY;
					}
					break;
				}
				case 16:
					if (kind == "AnalysisServices")
					{
						dqdataSourceKind = DQDataSourceKind.ANALYSISSERVICES;
					}
					break;
				case 17:
				{
					char c = kind[0];
					if (c != 'A')
					{
						if (c == 'C')
						{
							if (kind == "CommonDataService")
							{
								dqdataSourceKind = DQDataSourceKind.COMMONDATASERVICE;
							}
						}
					}
					else if (kind == "AzureDataExplorer")
					{
						dqdataSourceKind = DQDataSourceKind.AZUREDATAEXPLORER;
					}
					break;
				}
				case 18:
					if (kind == "SapBusinessObjects")
					{
						dqdataSourceKind = DQDataSourceKind.SAPBUSINESSOBJECTS;
					}
					break;
				case 20:
				{
					char c = kind[0];
					if (c != 'A')
					{
						if (c != 'D')
						{
							if (c == 'S')
							{
								if (kind == "SapBusinessWarehouse")
								{
									dqdataSourceKind = DQDataSourceKind.SAPBUSINESSWAREHOUSE;
								}
							}
						}
						else if (kind == "DatabricksMultiCloud")
						{
							dqdataSourceKind = DQDataSourceKind.DATABRICKSMULTICLOUD;
						}
					}
					else if (kind == "AzureDataLakeStorage")
					{
						dqdataSourceKind = DQDataSourceKind.AZUREDATALAKESTORAGE;
					}
					break;
				}
				}
			}
			return dqdataSourceKind;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00008464 File Offset: 0x00006664
		public static void GetProviderSpecificConnectionDetails(string dataSourceName, string dataSourceReferenceJson, string credentialJson, string dataSourceOptionsJson, string serverVersion, string powerBIGlobalServiceFQDN, out string connectionString, out string managedProvider)
		{
			DataSourceReference dataSourceReference = new DataSourceReference(dataSourceReferenceJson);
			if (dataSourceReference.Query != null)
			{
				throw EngineException.PFE_M_ENGINE_DQ_DATA_SOURCE_WITH_QUERY(dataSourceName);
			}
			Credential credential = Credential.FromJson(credentialJson, dataSourceName);
			DirectQueryConnectionStringBuilder directQueryConnectionStringBuilder = DirectQueryConnectionStringBuilderFactory.GetDirectQueryConnectionStringBuilder(dataSourceName, dataSourceReference, credential, serverVersion, dataSourceOptionsJson, powerBIGlobalServiceFQDN);
			connectionString = directQueryConnectionStringBuilder.ConnectionString;
			managedProvider = directQueryConnectionStringBuilder.ManagedProvider;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000084B0 File Offset: 0x000066B0
		private static string ParseModelName(string sourceExpressionText)
		{
			Match match = new Regex("Cubes{\\[Id=\\\"(.*?)\\\"(, Kind=\\\"(?'kind'Cube|Subcube)\\\")?\\]}\\[Data\\]", RegexOptions.IgnoreCase).Match(sourceExpressionText);
			if (match.Success)
			{
				return match.Groups[1].Value;
			}
			return string.Empty;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000084F0 File Offset: 0x000066F0
		public static void GetNativeConnectionDetailsForV3DQ(string dsrJson, string options, string sourceExpressionText, string powerBIGlobalServiceFQDN, out string connectionString, out string managedProvider, out string modelName, out bool shouldUseReadOnlyAccessMode)
		{
			DataSourceReference dataSourceReference = new DataSourceReference(dsrJson);
			DirectQueryConnectionStringBuilder directQueryConnectionStringBuilder = DirectQueryConnectionStringBuilderFactory.GetDirectQueryConnectionStringBuilder(dsrJson, dataSourceReference, null, null, options, powerBIGlobalServiceFQDN);
			if (dataSourceReference.DataSource.Kind == "AnalysisServices")
			{
				modelName = MInteropHelperImpl.ParseModelName(sourceExpressionText);
				string text = directQueryConnectionStringBuilder["Data Source"].ToString().Trim();
				shouldUseReadOnlyAccessMode = MInteropHelperImpl.ShouldEnforceReadOnlyAccessMode(text);
			}
			else
			{
				modelName = string.Empty;
				shouldUseReadOnlyAccessMode = false;
			}
			connectionString = directQueryConnectionStringBuilder.ConnectionString;
			managedProvider = directQueryConnectionStringBuilder.ManagedProvider;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00008570 File Offset: 0x00006770
		public static void GetAnalyzableDocForDualMode(string connectionString, out string dualModeConnectionString)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder
			{
				ConnectionString = connectionString
			};
			if (dbConnectionStringBuilder.ContainsKey("Location"))
			{
				dbConnectionStringBuilder.Remove("Location");
			}
			dualModeConnectionString = dbConnectionStringBuilder.ConnectionString;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000085AC File Offset: 0x000067AC
		private static bool ShouldEnforceReadOnlyAccessMode(string dataSource)
		{
			if (!MInteropHelperImpl.IsPbiPublicXmlaDataSource(dataSource))
			{
				return MInteropHelperImpl.IsAsAzureDataSource(dataSource) && !dataSource.EndsWith(":rw", StringComparison.OrdinalIgnoreCase);
			}
			if (!dataSource.Contains("?"))
			{
				return true;
			}
			string[] array = new Uri(dataSource).Query.TrimStart(new char[] { '?' }).Split(new char[] { '&' });
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Equals("readwrite", StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00008637 File Offset: 0x00006837
		private static bool IsPbiPublicXmlaDataSource(string dataSource)
		{
			return !string.IsNullOrEmpty(dataSource) && dataSource.StartsWith("powerbi" + Uri.SchemeDelimiter, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00008659 File Offset: 0x00006859
		private static bool IsAsAzureDataSource(string dataSource)
		{
			return !string.IsNullOrEmpty(dataSource) && dataSource.StartsWith("asazure" + Uri.SchemeDelimiter, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000867B File Offset: 0x0000687B
		private static void ParseJson(string json)
		{
			MInteropHelperImpl.Serializer.Deserialize<Dictionary<string, object>>(json);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000868C File Offset: 0x0000688C
		internal static void ValidateDataAccessOptionsJson(string json)
		{
			try
			{
				MInteropHelperImpl.ParseJson(json);
			}
			catch (Exception ex)
			{
				throw EngineException.PFE_M_ENGINE_DATA_ACCESS_OPTIONS_JSON_IS_MALFORMED(ex.Message, ex);
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000086C0 File Offset: 0x000068C0
		internal static void ValidateConnectionDetailsJson(string json, string dataSourceName)
		{
			try
			{
				MInteropHelperImpl.ParseJson(json);
			}
			catch (Exception ex)
			{
				throw EngineException.PFE_M_ENGINE_CONNECTION_DETAILS_JSON_IS_MALFORMED(dataSourceName, ex.Message, ex);
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000086F4 File Offset: 0x000068F4
		internal static void ValidateCredentialJson(string json, string dataSourceName)
		{
			Credential.FromJson(json, dataSourceName);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00008700 File Offset: 0x00006900
		internal static void ValidateOptionsJson(string json, string dataSourceName)
		{
			try
			{
				MInteropHelperImpl.ParseJson(json);
			}
			catch (Exception ex)
			{
				throw EngineException.PFE_M_ENGINE_OPTIONS_JSON_IS_MALFORMED(dataSourceName, ex.Message, ex);
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00008734 File Offset: 0x00006934
		private static void ValidateSyntax(string expression)
		{
			string text = MHelper.EscapeIdentifier(Guid.NewGuid().ToString());
			string text2 = string.Format("section main; shared {0} = {1}\r\n;", text, expression);
			IEngineTracer engineTracer = MInteropHelperImpl.EngineTracer;
			if (engineTracer != null)
			{
				engineTracer.LogPrivateMessage(string.Format("Validating M engine syntax: {0}", text2.RedactSensitiveStrings().MarkAsCustomerContent()));
			}
			MashupSourceHelper.Minimize(text2, text);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00008794 File Offset: 0x00006994
		internal static void ValidateContextExpressionSyntax(string expression, string dataSourceName)
		{
			try
			{
				MInteropHelperImpl.ValidateSyntax(expression);
			}
			catch (Exception ex)
			{
				throw EngineException.PFE_M_ENGINE_CONTEXT_EXPRESSION_SYNTAX_ERROR(dataSourceName, ex.Message, ex);
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000087C8 File Offset: 0x000069C8
		internal static void ValidateSharedExpressionSyntax(string expression, string name)
		{
			try
			{
				MInteropHelperImpl.ValidateSyntax(expression);
			}
			catch (Exception ex)
			{
				throw EngineException.PFE_M_ENGINE_SHARED_EXPRESSION_SYNTAX_ERROR(name, ex.Message, ex);
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000087FC File Offset: 0x000069FC
		internal static void ValidatePartitionExpressionSyntax(string expression, string tableName, string partitionName)
		{
			try
			{
				MInteropHelperImpl.ValidateSyntax(expression);
			}
			catch (Exception ex)
			{
				throw EngineException.PFE_M_ENGINE_PARTITION_EXPRESSION_SYNTAX_ERROR(ex.Message, partitionName, tableName, ex);
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00008838 File Offset: 0x00006A38
		internal static void ValidateRefreshPolicyExpressionSyntax(string expression, string tableName, string type)
		{
			try
			{
				MInteropHelperImpl.ValidateSyntax(expression);
			}
			catch (Exception ex)
			{
				throw EngineException.PFE_M_ENGINE_REFRESH_POLICY_EXPRESSION_SYNTAX_ERROR(type, ex.Message, tableName, ex);
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00008870 File Offset: 0x00006A70
		internal static IMEngineDataSourcePool GetDataSourcePool(string name)
		{
			return new MEngineDataSourcePool(name, MInteropHelperImpl.EngineTracer);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00008880 File Offset: 0x00006A80
		internal static IMEngineSessionHandle GetSessionHandle(Guid rootActivityId, Guid parentActivityId, string sessionId, string dataSourceAllCredentials)
		{
			List<Dictionary<string, string>> list = MInteropHelperImpl.Serializer.Deserialize<List<Dictionary<string, string>>>("[" + dataSourceAllCredentials + "]");
			Dictionary<string, Credential> dataSourceCredentialsDict = new Dictionary<string, Credential>();
			foreach (Dictionary<string, string> dictionary in list)
			{
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					dataSourceCredentialsDict[keyValuePair.Key] = Credential.FromJson(keyValuePair.Value, "");
				}
			}
			MEngineSessionHandle sessionHandle = new MEngineSessionHandle(sessionId);
			sessionHandle.GetHandle().DataSourceSettingNeeded += delegate(object s, DataSourceSettingNeededEventArgs e)
			{
				try
				{
					IEngineTracer engineTracer = MInteropHelperImpl.EngineTracer;
					if (engineTracer != null)
					{
						engineTracer.LogPrivateMessage(rootActivityId, parentActivityId, string.Concat(new string[]
						{
							"Mashup data source setting needed, reason: ",
							e.Details.Reason,
							", details: ",
							e.Details.ToString().MarkAsCustomerContent(),
							"."
						}));
					}
					if (!(e.Details.Reason != MashupCredentialExceptionReason.CredentialNeedsRefresh))
					{
						foreach (DataSource dataSource in e.Details.DataSources)
						{
							string text = dataSource.Kind + "/" + dataSource.NormalizedPath;
							Credential credential = dataSourceCredentialsDict[text];
							if (!(credential.AuthenticationKind != "OAuth2"))
							{
								Credential credential2 = credential;
								lock (credential2)
								{
									TokenCredential refreshedTokenCredentialsForDataSource = sessionHandle.GetRefreshedTokenCredentialsForDataSource(text);
									TokenCredential tokenCredential;
									if (refreshedTokenCredentialsForDataSource != null)
									{
										string expires = refreshedTokenCredentialsForDataSource.Expires;
										IEngineTracer engineTracer2 = MInteropHelperImpl.EngineTracer;
										if (engineTracer2 != null)
										{
											engineTracer2.LogPrivateMessage(rootActivityId, parentActivityId, string.Format("Refreshed token is available for data source {0}, expiry: {1}, refresh token available: {2}", dataSource.Path.MarkAsCustomerContent(), expires, !string.IsNullOrEmpty(refreshedTokenCredentialsForDataSource.RefreshToken)));
										}
										if (string.IsNullOrEmpty(expires) || MInteropHelperImpl.ExpiresIn(expires, TimeSpan.FromMinutes(30.0)))
										{
											tokenCredential = MInteropHelperImpl.RefreshToken(rootActivityId, parentActivityId, refreshedTokenCredentialsForDataSource, dataSource);
											sessionHandle.UpdateRefreshToken(text, tokenCredential);
										}
										else
										{
											tokenCredential = refreshedTokenCredentialsForDataSource;
										}
									}
									else
									{
										TokenCredential tokenCredential2 = new TokenCredential(credential.AuthenticationProperties["AccessToken"].ToString(), credential.AuthenticationProperties["Expires"].ToString(), credential.AuthenticationProperties["RefreshToken"].ToString(), MInteropHelperImpl.ExtractRelevantPropertiesFromCredential(credential, dataSource.Kind));
										IEngineTracer engineTracer3 = MInteropHelperImpl.EngineTracer;
										if (engineTracer3 != null)
										{
											engineTracer3.LogPrivateMessage(rootActivityId, parentActivityId, string.Format("Refreshed token is not available for data source {0}, expiry: {1}, refresh token available: {2}", dataSource.Path.MarkAsCustomerContent(), tokenCredential2.Expires, !string.IsNullOrEmpty(tokenCredential2.RefreshToken)));
										}
										tokenCredential = MInteropHelperImpl.RefreshToken(rootActivityId, parentActivityId, tokenCredential2, dataSource);
										sessionHandle.UpdateRefreshToken(text, tokenCredential);
									}
									e.NewSettings[dataSource] = tokenCredential.ToRefreshableOAuth2Credential();
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					IEngineTracer engineTracer4 = MInteropHelperImpl.EngineTracer;
					if (engineTracer4 != null)
					{
						engineTracer4.LogMessage(rootActivityId, parentActivityId, string.Format("Exception in DataSourceSettingNeeded handler: {0}", ex));
					}
					throw;
				}
			};
			return sessionHandle;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00008984 File Offset: 0x00006B84
		internal static Dictionary<string, string> ExtractRelevantPropertiesFromCredential(Credential credential, string datasourceKind)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (KeyValuePair<string, object> keyValuePair in MInteropHelperImpl.RemoveSecretsFromCredentialProperties(credential, datasourceKind))
			{
				string text = keyValuePair.Value as string;
				if (text != null)
				{
					dictionary.Add(keyValuePair.Key, text);
				}
			}
			return dictionary;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000089F8 File Offset: 0x00006BF8
		internal static bool IsCustomClientApplicationRequiredForTokenRefresh(Guid rootActivityId, Guid parentActivityId, TokenCredential expiredToken, DataSource dataSource)
		{
			DataSourceKindInfo dataSourceKindInfo = DataSourceKindInfo.FromKind(dataSource.Kind, DataSourceKindInfoOptions.ReturnAad);
			if (dataSourceKindInfo != null)
			{
				if (dataSourceKindInfo.AuthenticationInfos.Any((AuthenticationInfo info) => info.Kind == "AAD"))
				{
					ClaimMInterop claimMInterop;
					if (!MInteropHelperImpl.JwtTokenHelper.TryGetClaimFromAccessToken(rootActivityId, parentActivityId, expiredToken.AccessToken, "appid", out claimMInterop))
					{
						IEngineTracer engineTracer = MInteropHelperImpl.EngineTracer;
						if (engineTracer != null)
						{
							engineTracer.LogPrivateMessage(rootActivityId, parentActivityId, "'appid' claim type was not found in the token for data source " + dataSource.Path.MarkAsCustomerContent() + ".");
						}
						return false;
					}
					IEngineTracer engineTracer2 = MInteropHelperImpl.EngineTracer;
					if (engineTracer2 != null)
					{
						engineTracer2.LogPrivateMessage(rootActivityId, parentActivityId, string.Concat(new string[]
						{
							"'appid' claim type with value ",
							claimMInterop.Value,
							" was found in the token for data source ",
							dataSource.Path.MarkAsCustomerContent(),
							"."
						}));
					}
					return string.Compare(claimMInterop.Value, "a672d62c-fc7b-4e81-a576-e60dc46e951d", StringComparison.OrdinalIgnoreCase) == 0;
				}
			}
			return false;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00008AF0 File Offset: 0x00006CF0
		private static TokenCredential RefreshToken(Guid rootActivityId, Guid parentActivityId, TokenCredential expiredToken, DataSource dataSource)
		{
			IEngineTracer engineTracer = MInteropHelperImpl.EngineTracer;
			if (engineTracer != null)
			{
				engineTracer.LogPrivateMessage(rootActivityId, parentActivityId, string.Concat(new string[]
				{
					"Refreshing token to datasource ",
					dataSource.Path.MarkAsCustomerContent(),
					", current expiry: ",
					expiredToken.Expires,
					"."
				}));
			}
			IOAuthProvider ioauthProvider;
			if (MInteropHelperImpl.IsCustomClientApplicationRequiredForTokenRefresh(rootActivityId, parentActivityId, expiredToken, dataSource))
			{
				ioauthProvider = dataSource.GetOAuthProvider(new OAuthClientApplication("a672d62c-fc7b-4e81-a576-e60dc46e951d", string.Empty, "https://login.microsoftonline.com/common/oauth2/nativeclient", ClientApplicationSecretType.Default));
			}
			else
			{
				ioauthProvider = dataSource.GetOAuthProvider();
			}
			TokenCredential tokenCredential = ioauthProvider.Refresh(expiredToken);
			IEngineTracer engineTracer2 = MInteropHelperImpl.EngineTracer;
			if (engineTracer2 != null)
			{
				engineTracer2.LogPrivateMessage(rootActivityId, parentActivityId, string.Concat(new string[]
				{
					"Refreshed token to datasource ",
					dataSource.Path.MarkAsCustomerContent(),
					", new expiry: ",
					tokenCredential.Expires,
					"."
				}));
			}
			return tokenCredential;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00008BCE File Offset: 0x00006DCE
		private static bool ExpiresIn(string expires, TimeSpan expirationThreshold)
		{
			return DateTime.Parse(expires) < DateTime.Now + expirationThreshold;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00008BE8 File Offset: 0x00006DE8
		internal static void GetEntryQuery(string dataSourceConnectionString, out string entryQuery, bool isMExpressionDQ = false)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();
			dbConnectionStringBuilder.ConnectionString = dataSourceConnectionString;
			object obj = null;
			if (!dbConnectionStringBuilder.TryGetValue("Location", out obj) && !isMExpressionDQ)
			{
				throw EngineException.PFE_M_ENGINE_NO_LOCATION_IN_DATASOURCECONNECTIONSTRING();
			}
			entryQuery = (string)obj;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00008C24 File Offset: 0x00006E24
		internal static void GetSharedQueries(string[] dataSourceConnectionStrings, bool isMExpressionDQ, out string[] queryNames, out string[] queryExpressions, out long entryQueryCount)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			foreach (string text in dataSourceConnectionStrings)
			{
				string text2 = null;
				MInteropHelperImpl.GetEntryQuery(text, out text2, isMExpressionDQ);
				MInteropHelperImpl.DecodeMashupAndGetEntryMquery(text, text2, dictionary, dictionary2);
			}
			entryQueryCount = (long)dictionary.Count;
			long num = entryQueryCount + (long)dictionary2.Count<KeyValuePair<string, string>>();
			queryNames = new string[num];
			queryExpressions = new string[num];
			int num2 = 0;
			foreach (KeyValuePair<string, string> keyValuePair in dictionary)
			{
				queryNames[num2] = keyValuePair.Key;
				queryExpressions[num2] = keyValuePair.Value;
				num2++;
			}
			foreach (KeyValuePair<string, string> keyValuePair2 in dictionary2)
			{
				queryNames[num2] = keyValuePair2.Key;
				queryExpressions[num2] = keyValuePair2.Value;
				num2++;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008D40 File Offset: 0x00006F40
		internal static void CheckForPowerBIV1MashupConnectionString(string dataSourceConnectionString, out bool isPowerBIMashup)
		{
			isPowerBIMashup = false;
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder(false)
			{
				ConnectionString = dataSourceConnectionString
			};
			if (!dbConnectionStringBuilder.ContainsKey("Provider"))
			{
				return;
			}
			if (!"Microsoft.PowerBI.OleDb".Equals((string)dbConnectionStringBuilder["Provider"], StringComparison.OrdinalIgnoreCase))
			{
				return;
			}
			isPowerBIMashup = true;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008D90 File Offset: 0x00006F90
		internal static void GetDataSourceKindFromConnectionString(string dataSourceConnectionString, out DQDataSourceKind dataSourceKind)
		{
			Dictionary<string, string> dictionary = MInteropHelperImpl.DecodeMashupAndGetQueries(dataSourceConnectionString);
			HashSet<DQDataSourceKind> hashSet = new HashSet<DQDataSourceKind>();
			Exception ex = null;
			foreach (string text in dictionary.Values)
			{
				try
				{
					hashSet.UnionWith(MInteropHelperImpl.GetDataSourceKindsUsedByQuery("section Section1;", text, "table", "partition"));
				}
				catch (Exception ex)
				{
				}
			}
			dataSourceKind = DQDataSourceKind.UNKNOWN;
			if (hashSet.Count<DQDataSourceKind>() > 0)
			{
				dataSourceKind = hashSet.First<DQDataSourceKind>();
				return;
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00008E30 File Offset: 0x00007030
		private static Dictionary<string, string> ExtractQueryDefinitions(string dataSourceConnectionString)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();
			dbConnectionStringBuilder.ConnectionString = dataSourceConnectionString;
			object obj = null;
			if (!dbConnectionStringBuilder.TryGetValue("Mashup", out obj))
			{
				throw EngineException.PFE_M_ENGINE_NO_MASHUP_IN_DATASOURCECONNECTIONSTRING();
			}
			return MHelper.GetMembers(MInteropHelperImpl.UnzipFormulaContent(MInteropHelperImpl.FromBase64String((string)obj)), true, true);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00008E78 File Offset: 0x00007078
		private static Dictionary<string, string> DecodeMashupAndGetQueries(string dataSourceConnectionString)
		{
			Dictionary<string, string> dictionary = MInteropHelperImpl.ExtractQueryDefinitions(dataSourceConnectionString);
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			foreach (KeyValuePair<string, string> keyValuePair in dictionary)
			{
				string text = MInteropHelperImpl.RemoveExtraContextFromEntryQuery(keyValuePair.Value);
				dictionary2.Add(keyValuePair.Key, text);
			}
			return dictionary2;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00008EE8 File Offset: 0x000070E8
		internal static void GetEntryQueryExpression(string dataSourceConnectionString, out string entryQueryExpression)
		{
			string text = null;
			MInteropHelperImpl.GetEntryQuery(dataSourceConnectionString, out text, false);
			Dictionary<string, string> dictionary = MInteropHelperImpl.ExtractQueryDefinitions(dataSourceConnectionString);
			entryQueryExpression = null;
			string text2;
			if (dictionary.TryGetValue(text, out text2))
			{
				entryQueryExpression = MInteropHelperImpl.RemoveExtraContextFromEntryQuery(text2);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00008F1C File Offset: 0x0000711C
		private static void DecodeMashupAndGetEntryMquery(string dataSourceConnectionString, string entryQueryName, Dictionary<string, string> entryQueriesDictionary, Dictionary<string, string> sharedQueriesDictionary)
		{
			foreach (KeyValuePair<string, string> keyValuePair in MInteropHelperImpl.ExtractQueryDefinitions(dataSourceConnectionString))
			{
				if (keyValuePair.Key.Equals(entryQueryName, StringComparison.Ordinal))
				{
					entryQueriesDictionary.Add(keyValuePair.Key, MInteropHelperImpl.RemoveExtraContextFromEntryQuery(keyValuePair.Value));
					if (sharedQueriesDictionary.ContainsKey(keyValuePair.Key))
					{
						sharedQueriesDictionary.Remove(keyValuePair.Key);
					}
				}
				else if (!entryQueriesDictionary.ContainsKey(keyValuePair.Key) && !sharedQueriesDictionary.ContainsKey(keyValuePair.Key))
				{
					sharedQueriesDictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00008FE8 File Offset: 0x000071E8
		private static string RemoveExtraContextFromEntryQuery(string expression)
		{
			return MInteropHelperImpl.regex.Replace(expression, "${inFormat}${previousStep}");
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00008FFA File Offset: 0x000071FA
		private static byte[] FromBase64String(string mashup)
		{
			return Convert.FromBase64String(mashup);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00009004 File Offset: 0x00007204
		private static string UnzipFormulaContent(byte[] packagePartBytes)
		{
			string text = string.Empty;
			using (MemoryStream memoryStream = new MemoryStream(packagePartBytes))
			{
				using (Package package = Package.Open(memoryStream, FileMode.Open))
				{
					Regex regex = new Regex("/Formulas/\\w+.m");
					using (Stream stream = (from p in package.GetParts()
						where regex.Match(p.Uri.ToString()).Success
						select p).First<PackagePart>().GetStream())
					{
						byte[] array = new byte[stream.Length];
						stream.Read(array, 0, (int)stream.Length);
						stream.Close();
						text = Encoding.UTF8.GetString(array);
					}
				}
			}
			return text;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000090E0 File Offset: 0x000072E0
		public static string[] GetDataSourceKindsFromMProgram(string MProgram)
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (MashupDiscovery mashupDiscovery in new MashupConnection(new MashupConnectionStringBuilder
			{
				Mashup = MProgram
			}.ToString()).FindReferencedDataSources())
			{
				MashupDiscoveryKind kind = mashupDiscovery.Kind;
				if (kind <= MashupDiscoveryKind.UnknownNativeQuery)
				{
					hashSet.Add(mashupDiscovery.DataSourceReference.DataSource.Kind);
				}
			}
			List<string> list = hashSet.ToList<string>();
			list.Sort();
			return list.ToArray();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00009174 File Offset: 0x00007374
		public static EngineException InternalError([CallerFilePath] string filePath = null, [CallerMemberName] string functionName = null, [CallerLineNumber] int lineNumber = 0)
		{
			return EngineException.PFE_INTERNAL(filePath, functionName, lineNumber.ToString());
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00009184 File Offset: 0x00007384
		private static void CollectAndTransformDataSources(MEngineProgramForAnalysis programForAnalysis, string memberName, Action<MashupDiscovery, ISet<MEngineLibrarySymbol>> action, string table, string partition, bool allowUknownFunction, bool allowUnknownCallsites, out bool foundUnknownFunctions, out bool foundUnknownCallsites)
		{
			try
			{
				bool flag = false;
				foundUnknownFunctions = false;
				foundUnknownCallsites = false;
				IMEngineDataSourceDiscovery dataSourceDiscovery = programForAnalysis.GetDataSourceDiscovery(MEngineDiscoveryOptions.Default);
				ISet<MEngineLibrarySymbol> set = dataSourceDiscovery.FindReferencedLibrarySymbols(memberName);
				foreach (MashupDiscovery mashupDiscovery in dataSourceDiscovery.FindReferencedDataSources(memberName))
				{
					switch (mashupDiscovery.Kind)
					{
					case MashupDiscoveryKind.DataSource:
					case MashupDiscoveryKind.UnknownNativeQuery:
					{
						DataSourceReference dataSourceReference = mashupDiscovery.DataSourceReference;
						string text;
						if (dataSourceReference == null)
						{
							text = null;
						}
						else
						{
							DataSource dataSource = dataSourceReference.DataSource;
							text = ((dataSource != null) ? dataSource.Kind : null);
						}
						if (MInteropHelperImpl.IsTridentDataSource(text))
						{
							flag = true;
							continue;
						}
						action(mashupDiscovery, set);
						continue;
					}
					case MashupDiscoveryKind.UnknownFunction:
					{
						IEngineTracer engineTracer = MInteropHelperImpl.EngineTracer;
						if (engineTracer != null)
						{
							engineTracer.LogPrivateMessage("Encountered unknown function " + mashupDiscovery.FunctionName.MarkAsCustomerContent() + " in CollectAndTransformDataSources().");
						}
						if (!allowUknownFunction)
						{
							throw EngineException.PFE_M_ENGINE_UNKNOWN_NAME_REFERENCED(mashupDiscovery.FunctionName, partition, table);
						}
						if (MInteropHelperImpl.IsValidMashupLibraryFunctionName(mashupDiscovery.FunctionName))
						{
							foundUnknownFunctions = true;
							continue;
						}
						continue;
					}
					case MashupDiscoveryKind.UnknownCallSite:
						if (allowUnknownCallsites)
						{
							foundUnknownCallsites = true;
							continue;
						}
						throw EngineException.PFE_M_ENGINE_UNKNOWN_CALLSITE_REFERENCED(mashupDiscovery.FunctionName, partition, table);
					case MashupDiscoveryKind.Unsupported:
						if (allowUnknownCallsites)
						{
							foundUnknownCallsites = true;
							continue;
						}
						break;
					}
					throw EngineException.PFE_M_ENGINE_UNKNOWN_ENTITY_REFERENCED(partition, table);
				}
				if (flag)
				{
					foreach (MashupDiscovery mashupDiscovery2 in programForAnalysis.GetDataSourceDiscovery(MEngineDiscoveryOptions.ForTridentDataSource).FindReferencedDataSources(memberName))
					{
						switch (mashupDiscovery2.Kind)
						{
						case MashupDiscoveryKind.DataSource:
						case MashupDiscoveryKind.UnknownNativeQuery:
						{
							DataSourceReference dataSourceReference2 = mashupDiscovery2.DataSourceReference;
							string text2;
							if (dataSourceReference2 == null)
							{
								text2 = null;
							}
							else
							{
								DataSource dataSource2 = dataSourceReference2.DataSource;
								text2 = ((dataSource2 != null) ? dataSource2.Kind : null);
							}
							if (MInteropHelperImpl.IsTridentDataSource(text2))
							{
								action(mashupDiscovery2, set);
								continue;
							}
							continue;
						}
						case MashupDiscoveryKind.UnknownFunction:
							if (!allowUknownFunction)
							{
								throw EngineException.PFE_M_ENGINE_UNKNOWN_NAME_REFERENCED(mashupDiscovery2.FunctionName, partition, table);
							}
							continue;
						case MashupDiscoveryKind.UnknownCallSite:
							if (!allowUnknownCallsites)
							{
								throw EngineException.PFE_M_ENGINE_UNKNOWN_CALLSITE_REFERENCED(mashupDiscovery2.FunctionName, partition, table);
							}
							continue;
						case MashupDiscoveryKind.Unsupported:
							if (allowUnknownCallsites)
							{
								continue;
							}
							break;
						}
						throw EngineException.PFE_M_ENGINE_UNKNOWN_ENTITY_REFERENCED(partition, table);
					}
				}
			}
			catch (EngineException ex)
			{
				IEngineTracer engineTracer2 = MInteropHelperImpl.EngineTracer;
				if (engineTracer2 != null)
				{
					engineTracer2.LogPrivateMessage(string.Format("CollectAndTransformDataSources failed for memberName: {0}, exception: {1}, M program: {2}", memberName.RedactSensitiveStrings().MarkAsCustomerContent(), ex.ToString(), programForAnalysis.MProgram.MarkAsCustomerContent()));
				}
				throw;
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00009420 File Offset: 0x00007620
		internal static string GetMinimizedMProgram(string MProgram, string MQuery, out string newMemberName)
		{
			newMemberName = Guid.NewGuid().ToString();
			string text = MHelper.EscapeIdentifier(newMemberName);
			return MashupSourceHelper.Minimize(string.Format("{0}\r\nshared {1} = {2}\r\n;", MProgram, text, MQuery), text);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000945D File Offset: 0x0000765D
		private static bool IsTridentDataSource(string dataSourceKind)
		{
			return dataSourceKind == "PowerBI" || dataSourceKind == "PowerPlatformDataflows" || dataSourceKind == "Lakehouse";
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00009488 File Offset: 0x00007688
		private static string BuildMashupConnectionString(string MProgram, string location, ConnectionStringFlavor connectionStringFlavor)
		{
			switch (connectionStringFlavor)
			{
			case ConnectionStringFlavor.PreV3:
				return new DbConnectionStringBuilder
				{
					{ "Provider", "Microsoft.PowerBI.OleDb" },
					{
						"Mashup",
						MInteropHelperImpl.PrepareMashupPackageForPowerBI(MProgram, true)
					},
					{ "Location", location }
				}.ConnectionString;
			case ConnectionStringFlavor.V3NonLegacy:
				return new DbConnectionStringBuilder
				{
					{ "Provider", "Microsoft.PowerBI.OleDb" },
					{
						"Mashup",
						MInteropHelperImpl.PrepareMashupPackageForPowerBI(MProgram, true)
					},
					{ "Location", null }
				}.ConnectionString;
			case ConnectionStringFlavor.V3Legacy:
				return ConnectionString.Build(MProgram, string.Empty, string.Empty, "[]", string.Empty, false, string.Empty, 0, string.Empty, 0, true, true);
			default:
				throw new Exception("unknown model version");
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00009554 File Offset: 0x00007754
		private static bool IsValidMashupLibraryFunctionName(string functionName)
		{
			if (string.IsNullOrEmpty(functionName))
			{
				return false;
			}
			int num = functionName.IndexOf('.');
			return num > 0 && num < functionName.Length - 1;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00009588 File Offset: 0x00007788
		internal static string GetSqlCubeApplyParameters(string cubeName, string applyParametersOrig, string applyParametersNew)
		{
			string text;
			if (!string.IsNullOrEmpty(applyParametersOrig) && !string.IsNullOrEmpty(applyParametersNew))
			{
				text = CubeApplyParametersHelper.MergeCubeApplyParameters(applyParametersOrig, applyParametersNew);
			}
			else if (!string.IsNullOrEmpty(applyParametersNew))
			{
				text = applyParametersNew;
			}
			else if (!string.IsNullOrEmpty(applyParametersOrig))
			{
				text = applyParametersOrig;
			}
			else
			{
				text = "{}";
			}
			return CubeApplyParametersHelper.GetSqlCubeApplyParameters(cubeName, text);
		}

		// Token: 0x04000110 RID: 272
		private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

		// Token: 0x04000111 RID: 273
		private const string PowerQueryForExcelAppId = "a672d62c-fc7b-4e81-a576-e60dc46e951d";

		// Token: 0x04000112 RID: 274
		private const string DefaultRedirectUri = "https://login.microsoftonline.com/common/oauth2/nativeclient";

		// Token: 0x04000113 RID: 275
		private const string AppIdClaimType = "appid";

		// Token: 0x04000114 RID: 276
		private const string PbiPublicXmlaScheme = "powerbi";

		// Token: 0x04000115 RID: 277
		private const string AsAzureXmlaScheme = "asazure";

		// Token: 0x04000116 RID: 278
		private const string MashupOAuthPoolName = "AS.OAuthContainerPool";

		// Token: 0x04000117 RID: 279
		public const string ExtendedPropertiesPropName = "Extended Properties";

		// Token: 0x04000118 RID: 280
		public const string ProviderPropName = "Provider";

		// Token: 0x04000119 RID: 281
		private static readonly string[] ProvidersOLEDBForODBC = new string[] { "MSDASQL", "MSDASQL.1" };

		// Token: 0x0400011A RID: 282
		public const string ProviderForOleDb = "Microsoft.PowerBI.OleDb";

		// Token: 0x0400011B RID: 283
		public const string ProviderForPQ = "Microsoft.Mashup.OleDb.1";

		// Token: 0x0400011C RID: 284
		public const string MashupPropName = "Mashup";

		// Token: 0x0400011D RID: 285
		public const string PackagePropName = "Package";

		// Token: 0x0400011E RID: 286
		public const string LocationPropName = "Location";

		// Token: 0x0400011F RID: 287
		public const string PackageVersion = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Package xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Version>1.5.3296.2082</Version><MinVersion>1.5.3296.0</MinVersion><Culture>en-US</Culture></Package>";

		// Token: 0x04000120 RID: 288
		public const string RemoveExtraContextReguarExpression = ",\\s*AutoRemovedColumns[0-9]*\\s*=\\s*let\\s*t = Table\\.FromValue\\((?<previousStep>.*), \\[DefaultColumnName \\=.*\\]\\),\\s*removed = Table\\.RemoveColumns\\(t, Table\\.ColumnsOfType\\(t, \\{type table, type record, type list\\}\\)\\)\\s*in\\s*Table\\.TransformColumnNames\\(removed, Text.Clean\\)(?<inFormat>\\s*in\\s*)AutoRemovedColumns[0-9]*";

		// Token: 0x04000121 RID: 289
		public const string DataSourcePoolPropName = "DataSourcePool";

		// Token: 0x04000122 RID: 290
		public const string DMTSConnectionDetailsPropName = "DMTSConnectionDetails";

		// Token: 0x04000123 RID: 291
		public const string MashupConnectionStringPropName = "MashupConnectionString";

		// Token: 0x04000124 RID: 292
		public const string UnknownDataSourceKind = "UnknownDataSourceKind";

		// Token: 0x04000125 RID: 293
		public const string UnknownDataSourcePath = "UnknownDataSourcePath";

		// Token: 0x04000126 RID: 294
		public static Regex regex = new Regex(",\\s*AutoRemovedColumns[0-9]*\\s*=\\s*let\\s*t = Table\\.FromValue\\((?<previousStep>.*), \\[DefaultColumnName \\=.*\\]\\),\\s*removed = Table\\.RemoveColumns\\(t, Table\\.ColumnsOfType\\(t, \\{type table, type record, type list\\}\\)\\)\\s*in\\s*Table\\.TransformColumnNames\\(removed, Text.Clean\\)(?<inFormat>\\s*in\\s*)AutoRemovedColumns[0-9]*", RegexOptions.Multiline);

		// Token: 0x0400012B RID: 299
		private static readonly List<string> knownPasswordPropNames = new List<string> { "password", "pwd", "pwddbms" };

		// Token: 0x0400012E RID: 302
		private static readonly Regex FabricOneLakeFQDNRegex = new Regex("^([a-z]+-)*onelake.dfs.fabric.microsoft.com$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x0400012F RID: 303
		private static readonly Regex FabricOneLakePathRegex = new Regex("^/([a-zA-Z0-9-]{36})/([a-zA-Z0-9-]{36})/$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x0200007A RID: 122
		// (Invoke) Token: 0x06002197 RID: 8599
		internal delegate string MProgramModifier(string minimizedMProgram, bool isDMTS);
	}
}
