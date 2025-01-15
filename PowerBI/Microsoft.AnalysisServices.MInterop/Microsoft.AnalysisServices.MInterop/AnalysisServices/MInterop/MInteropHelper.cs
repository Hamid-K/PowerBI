using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.AnalysisServices.PlatformHost;
using Microsoft.ASPaaS.AnalysisServer.Interfaces.Common.MInterop;
using Microsoft.Data.Mashup.Preview;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x0200002C RID: 44
	[ComVisible(true)]
	[Guid("B11D3642-6184-41a4-BB47-57726A615DDC")]
	[ClassInterface(ClassInterfaceType.None)]
	public sealed class MInteropHelper : ManagedErrorHandler, IMInteropHelper
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00004AC4 File Offset: 0x00002CC4
		public EngineErrorInfo Initialize(IJwtTokenHelperMInterop jwtTokenHelper, IEngineTracer tracer, MEngineSettings settings, MEngineSettingsForContainerPool processingPoolSettings, ServerType serverType, string additionalExtensions, string disabledExtensions, string customExtensionDirectories = "", bool customExtensionDirectoryWatchForChanges = false, bool customExtensionDirectoryIncludeSubdirectories = false, string certifiedExtensionsAllowlist = "", string certifiedExtensionDirectories = "", bool certifiedExtensionDirectoryWatchForChanges = false, bool certifiedExtensionDirectoryIncludeSubdirectories = false)
		{
			EngineErrorInfo engineErrorInfo;
			using (new MInteropHelper.UILocaleContext())
			{
				this.engineTracer = tracer;
				engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.Initialize(jwtTokenHelper, this.engineTracer, settings, processingPoolSettings, serverType, additionalExtensions, disabledExtensions, customExtensionDirectories, customExtensionDirectoryWatchForChanges, customExtensionDirectoryIncludeSubdirectories, certifiedExtensionsAllowlist, certifiedExtensionDirectories, certifiedExtensionDirectoryWatchForChanges, certifiedExtensionDirectoryIncludeSubdirectories);
					this.serverType = serverType;
				}, "Initialize", this.IsPrivacyMarkupRequired(), null);
			}
			return engineErrorInfo;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004B90 File Offset: 0x00002D90
		public EngineErrorInfo DisposeHelper()
		{
			EngineErrorInfo engineErrorInfo;
			using (new MInteropHelper.UILocaleContext())
			{
				engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.DisposeHelper();
				}, "DisposeHelper", this.IsPrivacyMarkupRequired(), null);
			}
			return engineErrorInfo;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004BF4 File Offset: 0x00002DF4
		public EngineErrorInfo RemoveSecretsFromCredentialJson(string credentialJson, string dataSourceReferenceJson, out string result)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpResult = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					tmpResult = MInteropHelperImpl.RemoveSecretsFromCredentialJson(credentialJson, dataSourceReferenceJson);
				}, "RemoveSecretsFromCredentialJson", this.IsPrivacyMarkupRequired(), null);
				result = tmpResult;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004C68 File Offset: 0x00002E68
		public EngineErrorInfo RemoveSecretsFromConnectionString(string connectionString, out string result)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpResult = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					tmpResult = MInteropHelperImpl.RemoveSecretsFromConnectionString(connectionString);
				}, "RemoveSecretsFromConnectionString", this.IsPrivacyMarkupRequired(), null);
				result = tmpResult;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004CD4 File Offset: 0x00002ED4
		public EngineErrorInfo ConvertASCredentialsToM(string dataSourceName, string credentialProp, string unattendedUsername, string unattendedPassword, out string credentialsConverted, out int impersonationMode)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpCredentialsConverted = null;
				int tmpImpersonationMode = 0;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.ConvertASCredentialsToM(dataSourceName, credentialProp, unattendedUsername, unattendedPassword, out tmpCredentialsConverted, out tmpImpersonationMode);
				}, "ConvertASCredentialsToM", this.IsPrivacyMarkupRequired(), null);
				credentialsConverted = tmpCredentialsConverted;
				impersonationMode = tmpImpersonationMode;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004D68 File Offset: 0x00002F68
		public EngineErrorInfo ParseCredentials(string credentialsProp, out string usernameStr, out string passwordStr, out int impersonationMode)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpUsernameStr = null;
				string tmpPasswordStr = null;
				int tmpImpersonationMode = 0;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.ParseCredentials(credentialsProp, out tmpUsernameStr, out tmpPasswordStr, out tmpImpersonationMode);
				}, "ParseCredentials", this.IsPrivacyMarkupRequired(), null);
				usernameStr = tmpUsernameStr;
				passwordStr = tmpPasswordStr;
				impersonationMode = tmpImpersonationMode;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004DF4 File Offset: 0x00002FF4
		public EngineErrorInfo ConvertDataSourceToM(string connectionDetailsProp, string contextExpressionProp, string processedCredential, string optionsProp, out string dataSourceExpression, out string dataSourceSettingsJson, out string completeNormalizedResourcePath, out string sessionCredentials, out bool isCredentialKindOAuth)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpDataSourceExpression = null;
				string tmpDataSourceSettingsJson = null;
				string tmpCompleteNormalizedResourcePath = null;
				string tmpSessionCredentials = null;
				bool tmpIsCredentialKindOAuth = false;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					DSRConversionHelper.ConvertDataSourceToM(connectionDetailsProp, contextExpressionProp, processedCredential, optionsProp, out tmpDataSourceExpression, out tmpDataSourceSettingsJson, out tmpCompleteNormalizedResourcePath, out tmpSessionCredentials, out tmpIsCredentialKindOAuth);
				}, "ConvertDataSourceToM", this.IsPrivacyMarkupRequired(), null);
				dataSourceExpression = tmpDataSourceExpression;
				dataSourceSettingsJson = tmpDataSourceSettingsJson;
				completeNormalizedResourcePath = tmpCompleteNormalizedResourcePath;
				sessionCredentials = tmpSessionCredentials;
				isCredentialKindOAuth = tmpIsCredentialKindOAuth;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004EB8 File Offset: 0x000030B8
		public EngineErrorInfo ConvertConnectionStringToDetails(string connectionString, string provider, int impersonationMode, string username, string password, out string connectionDetails, out string connectionCredentials)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpConnectionDetails = null;
				string tmpConnectionCredentials = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.ConvertConnectionStringToDetails(connectionString, provider, impersonationMode, username, password, out tmpConnectionDetails, out tmpConnectionCredentials);
				}, "ConvertConnectionStringToDetails", this.IsPrivacyMarkupRequired(), null);
				connectionDetails = tmpConnectionDetails;
				connectionCredentials = tmpConnectionCredentials;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004F54 File Offset: 0x00003154
		public EngineErrorInfo EscapeIdentifier(string identifier, out string result)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpResult = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					tmpResult = MHelper.EscapeIdentifier(identifier);
				}, "EscapeIdentifier", this.IsPrivacyMarkupRequired(), null);
				result = tmpResult;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004FC0 File Offset: 0x000031C0
		public EngineErrorInfo EscapeString(string stringToEscape, out string result)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpResult = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					tmpResult = MHelper.EscapeString(stringToEscape);
				}, "EscapeString", this.IsPrivacyMarkupRequired(), null);
				result = tmpResult;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000502C File Offset: 0x0000322C
		public EngineErrorInfo BuildConnectionString(string MProgram, string sessionId, string dataSourcePool, string dataSourceSettings, string dataAccessOptionsProp, bool enableStaticFirewallPlan, string cachePath, int maxCacheSizeInMegaBytes, string tempPath, int maxTempSizeInMegaBytes, bool throwFoldingFailures, bool isPBIDesktop, out string connectionString)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpConnectionString = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					tmpConnectionString = ConnectionString.Build(MProgram, sessionId, dataSourcePool, dataSourceSettings, dataAccessOptionsProp, enableStaticFirewallPlan, cachePath, maxCacheSizeInMegaBytes, tempPath, maxTempSizeInMegaBytes, throwFoldingFailures, isPBIDesktop);
				}, "BuildConnectionString", this.IsPrivacyMarkupRequired(), null);
				connectionString = tmpConnectionString;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000050F0 File Offset: 0x000032F0
		public EngineErrorInfo GetNamedDependencies(string MQuery, out string[] mashupDependencies, out string[] mashupDependenciesByDSResourcePath)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string[] tmpMashupDependencies = null;
				string[] tmpMashupDependenciesByDSResourcePath = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.GetNamedDependencies(MQuery, out tmpMashupDependencies, out tmpMashupDependenciesByDSResourcePath);
				}, "GetNamedDependencies", this.IsPrivacyMarkupRequired(), null);
				mashupDependencies = tmpMashupDependencies;
				mashupDependenciesByDSResourcePath = tmpMashupDependenciesByDSResourcePath;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000516C File Offset: 0x0000336C
		public EngineErrorInfo GetEffectiveConnectionStringForPowerBI(string variable, string MProgram, string MQuery, bool useScaffolding, bool forDqTables, bool minimize, out string effectiveConnectionString)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpEffectiveConnectionString = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.GetEffectiveConnectionStringForPowerBI(variable, MProgram, MQuery, useScaffolding, minimize, forDqTables ? ConnectionStringFlavor.V3NonLegacy : ConnectionStringFlavor.PreV3, out tmpEffectiveConnectionString);
				}, MProgram, "GetEffectiveConnectionStringForPowerBI", this.IsPrivacyMarkupRequired(), null);
				effectiveConnectionString = tmpEffectiveConnectionString;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005204 File Offset: 0x00003404
		public EngineErrorInfo GetEffectiveConnectionStringForPowerBIDQV3(string variable, string MProgram, string MQuery, out string effectiveConnectionString)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpEffectiveConnectionString = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.GetEffectiveConnectionStringForPowerBI(variable, MProgram, MQuery, true, true, ConnectionStringFlavor.V3NonLegacy, out tmpEffectiveConnectionString);
				}, "GetEffectiveConnectionStringForPowerBIDQV3", this.IsPrivacyMarkupRequired(), null);
				effectiveConnectionString = tmpEffectiveConnectionString;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005280 File Offset: 0x00003480
		public EngineErrorInfo GetEffectiveConnectionStringForPowerBIDQV3LiftParameters(string variable, string MProgram, string MQuery, string[] parametersToLift, bool useLegacyConnectionStringFlavor, out string effectiveConnectionString, out string[] liftedQueries, out string[] mParamters, out MParameterType[] mParameterTypes)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				ConnectionStringFlavor csFlavor = (useLegacyConnectionStringFlavor ? ConnectionStringFlavor.V3Legacy : ConnectionStringFlavor.V3NonLegacy);
				string tmpEffectiveConnectionString = null;
				string[] tmpLiftedQueries = null;
				string[] tmpParameters = null;
				MParameterType[] tmpParameterTypes = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.GetEffectiveConnectionStringWithLiftParameters(variable, MProgram, MQuery, true, csFlavor, parametersToLift, out tmpEffectiveConnectionString, out tmpLiftedQueries, out tmpParameters, out tmpParameterTypes);
				}, MProgram, "GetEffectiveConnectionStringForPowerBIDQV3LiftParameters", this.IsPrivacyMarkupRequired(), null);
				effectiveConnectionString = tmpEffectiveConnectionString;
				liftedQueries = tmpLiftedQueries;
				mParamters = tmpParameters;
				mParameterTypes = tmpParameterTypes;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005348 File Offset: 0x00003548
		public EngineErrorInfo GetMParameterType(string parameterExpression, out string parameterType)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpParameterType = string.Empty;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.GetMParameterType(parameterExpression, out tmpParameterType);
				}, "GetMParameterType", this.IsPrivacyMarkupRequired(), null);
				parameterType = tmpParameterType;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000053B8 File Offset: 0x000035B8
		public EngineErrorInfo GetEffectiveConnectionStringForPowerBIDQV3Legacy(string variable, string MProgram, string MQuery, out string effectiveConnectionString)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpEffectiveConnectionString = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.GetEffectiveConnectionStringForPowerBI(variable, MProgram, MQuery, true, true, ConnectionStringFlavor.V3Legacy, out tmpEffectiveConnectionString);
				}, MProgram, "GetEffectiveConnectionStringForPowerBIDQV3Legacy", this.IsPrivacyMarkupRequired(), null);
				effectiveConnectionString = tmpEffectiveConnectionString;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005438 File Offset: 0x00003638
		public EngineErrorInfo ApplyParameterSubstitution(string connectionString, string[] paramNames, string[] paramValues, out string newConnectionString)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpNewConnectionString = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					tmpNewConnectionString = MInteropHelperImpl.ApplyParameterSubstitution(connectionString, paramNames, paramValues);
				}, "ApplyParameterSubstitution", this.IsPrivacyMarkupRequired(), null);
				newConnectionString = tmpNewConnectionString;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000054B4 File Offset: 0x000036B4
		public EngineErrorInfo AppendDataSourcePoolToConnectionString(string connectionString, string poolName, out string newConnectionString)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpNewConnectionString = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					tmpNewConnectionString = MInteropHelperImpl.AppendDataSourcePoolToConnectionString(connectionString, poolName);
				}, "AppendDataSourcePoolToConnectionString", this.IsPrivacyMarkupRequired(), null);
				newConnectionString = tmpNewConnectionString;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005528 File Offset: 0x00003728
		public EngineErrorInfo AddParameterToConnectionString(string connectionString, string paramName, string paramValue, bool append, bool appendToGatewayConnectionString, out string newConnectionString)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpNewConnectionString = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					tmpNewConnectionString = MInteropHelperImpl.AddParameterToConnectionString(connectionString, paramName, paramValue, append, appendToGatewayConnectionString);
				}, "AddParameterToConnectionString", this.IsPrivacyMarkupRequired(), null);
				newConnectionString = tmpNewConnectionString;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000055B4 File Offset: 0x000037B4
		public EngineErrorInfo TraceMProgramFromConnectionString(string connectionString)
		{
			EngineErrorInfo engineErrorInfo;
			using (new MInteropHelper.UILocaleContext())
			{
				MInteropHelperImpl.MProgramModifier <>9__1;
				engineErrorInfo = this.HandleExceptions(delegate
				{
					string connectionString2 = connectionString;
					MInteropHelperImpl.MProgramModifier mprogramModifier;
					if ((mprogramModifier = <>9__1) == null)
					{
						mprogramModifier = (<>9__1 = delegate(string MProgram, bool isDMTS)
						{
							this.TraceMProgram(MProgram, "MProgram_EXEC");
							return MProgram;
						});
					}
					MInteropHelperImpl.ApplyParametersMProgramInMashupConnectionString(connectionString2, mprogramModifier);
				}, "TraceMProgramFromConnectionString", this.IsPrivacyMarkupRequired(), null);
			}
			return engineErrorInfo;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005618 File Offset: 0x00003818
		public EngineErrorInfo GetDataSourcesUsedByQuery(string MProgram, string MQuery, string table, string partition, out string[] resourcePaths)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string[] tmpResourcePaths = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					tmpResourcePaths = MInteropHelperImpl.GetDataSourcesUsedByQuery(MProgram, MQuery, table, partition);
				}, MProgram, "GetDataSourcesUsedByQuery", this.IsPrivacyMarkupRequired(), null);
				resourcePaths = tmpResourcePaths;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000056A0 File Offset: 0x000038A0
		public EngineErrorInfo GetDataSourcesUsedByProgram(string MProgram, out string[] resourcePaths)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string[] tmpResourcePaths = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					tmpResourcePaths = MInteropHelperImpl.GetDataSourcesUsedByProgram(MProgram);
				}, MProgram, "GetDataSourcesUsedByProgram", this.IsPrivacyMarkupRequired(), null);
				resourcePaths = tmpResourcePaths;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005714 File Offset: 0x00003914
		public EngineErrorInfo GetDataSourceKindsFromMProgram(string MProgram, out string[] dataSourceKinds)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string[] tmpDataSources = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					tmpDataSources = MInteropHelperImpl.GetDataSourceKindsFromMProgram(MProgram);
				}, "GetDataSourceKindsFromMProgram", this.IsPrivacyMarkupRequired(), null);
				dataSourceKinds = tmpDataSources;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005780 File Offset: 0x00003980
		public EngineErrorInfo GetServerAndDatabase(string dataSourceReferenceJson, out string server, out string database)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpServer = null;
				string tmpDatabase = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.GetServerAndDatabase(dataSourceReferenceJson, out tmpServer, out tmpDatabase);
				}, "GetServerAndDatabase", this.IsPrivacyMarkupRequired(), null);
				server = tmpServer;
				database = tmpDatabase;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000057FC File Offset: 0x000039FC
		public EngineErrorInfo GetDataSourceKind(string dataSourceReferenceJson, out DQDataSourceKind kind)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				DQDataSourceKind tmpKind = DQDataSourceKind.OTHER;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					tmpKind = MInteropHelperImpl.GetDataSourceKind(dataSourceReferenceJson);
				}, "GetDataSourceKind", this.IsPrivacyMarkupRequired(), null);
				kind = tmpKind;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005868 File Offset: 0x00003A68
		public EngineErrorInfo GetNormalizedResourcePathByKindPath(string kind, string path, out string normalizedResourcePath)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpNormalizedResourcePath = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.GetNormalizedResourcePathByKindPath(kind, path, out tmpNormalizedResourcePath);
				}, "GetNormalizedResourcePathByKindPath", this.IsPrivacyMarkupRequired(), null);
				normalizedResourcePath = tmpNormalizedResourcePath;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000058DC File Offset: 0x00003ADC
		public EngineErrorInfo GetNormalizedResourcePathByKindPathJson(string kindPathJson, out string normalizedResourcePath)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpNormalizedResourcePath = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.GetNormalizedResourcePathByKindPathJson(kindPathJson, out tmpNormalizedResourcePath);
				}, "GetNormalizedResourcePathByKindPathJson", this.IsPrivacyMarkupRequired(), null);
				normalizedResourcePath = tmpNormalizedResourcePath;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005948 File Offset: 0x00003B48
		public EngineErrorInfo GetProviderSpecificConnectionDetails(string dataSourceName, string dataSourceReferenceJson, string credentialJson, string dataSourceOptionsJson, string serverVersion, string powerBIGlobalServiceFQDN, out string connectionString, out string managedProvider)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpConnectionString = null;
				string tmpManagedProvider = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.GetProviderSpecificConnectionDetails(dataSourceName, dataSourceReferenceJson, credentialJson, dataSourceOptionsJson, serverVersion, powerBIGlobalServiceFQDN, out tmpConnectionString, out tmpManagedProvider);
				}, "GetProviderSpecificConnectionDetails", false, null);
				connectionString = tmpConnectionString;
				managedProvider = tmpManagedProvider;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000059E8 File Offset: 0x00003BE8
		public EngineErrorInfo ValidateDataAccessOptionsJson(string json)
		{
			EngineErrorInfo engineErrorInfo;
			using (new MInteropHelper.UILocaleContext())
			{
				engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.ValidateDataAccessOptionsJson(json);
				}, "ValidateDataAccessOptionsJson", this.IsPrivacyMarkupRequired(), null);
			}
			return engineErrorInfo;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005A44 File Offset: 0x00003C44
		public EngineErrorInfo ValidateConnectionDetailsJson(string json, string dataSourceName)
		{
			EngineErrorInfo engineErrorInfo;
			using (new MInteropHelper.UILocaleContext())
			{
				engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.ValidateConnectionDetailsJson(json, dataSourceName);
				}, "ValidateConnectionDetailsJson", this.IsPrivacyMarkupRequired(), null);
			}
			return engineErrorInfo;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005AA8 File Offset: 0x00003CA8
		public EngineErrorInfo ValidateCredentialJson(string json, string dataSourceName)
		{
			EngineErrorInfo engineErrorInfo;
			using (new MInteropHelper.UILocaleContext())
			{
				engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.ValidateCredentialJson(json, dataSourceName);
				}, "ValidateCredentialJson", this.IsPrivacyMarkupRequired(), null);
			}
			return engineErrorInfo;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005B0C File Offset: 0x00003D0C
		public EngineErrorInfo ValidateOptionsJson(string json, string dataSourceName)
		{
			EngineErrorInfo engineErrorInfo;
			using (new MInteropHelper.UILocaleContext())
			{
				engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.ValidateOptionsJson(json, dataSourceName);
				}, "ValidateOptionsJson", this.IsPrivacyMarkupRequired(), null);
			}
			return engineErrorInfo;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005B70 File Offset: 0x00003D70
		public EngineErrorInfo ValidateContextExpressionSyntax(string expression, string dataSourceName)
		{
			EngineErrorInfo engineErrorInfo;
			using (new MInteropHelper.UILocaleContext())
			{
				engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.ValidateContextExpressionSyntax(expression, dataSourceName);
				}, "ValidateContextExpressionSyntax", this.IsPrivacyMarkupRequired(), null);
			}
			return engineErrorInfo;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005BD4 File Offset: 0x00003DD4
		public EngineErrorInfo ValidateSharedExpressionSyntax(string expression, string name)
		{
			EngineErrorInfo engineErrorInfo;
			using (new MInteropHelper.UILocaleContext())
			{
				engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.ValidateSharedExpressionSyntax(expression, name);
				}, "ValidateSharedExpressionSyntax", this.IsPrivacyMarkupRequired(), null);
			}
			return engineErrorInfo;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005C38 File Offset: 0x00003E38
		public EngineErrorInfo ValidatePartitionExpressionSyntax(string expression, string tableName, string partitionName)
		{
			EngineErrorInfo engineErrorInfo;
			using (new MInteropHelper.UILocaleContext())
			{
				engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.ValidatePartitionExpressionSyntax(expression, tableName, partitionName);
				}, "ValidatePartitionExpressionSyntax", this.IsPrivacyMarkupRequired(), null);
			}
			return engineErrorInfo;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public EngineErrorInfo ValidateRefreshPolicyExpressionSyntax(string expression, string tableName, string type)
		{
			EngineErrorInfo engineErrorInfo;
			using (new MInteropHelper.UILocaleContext())
			{
				engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.ValidateRefreshPolicyExpressionSyntax(expression, tableName, type);
				}, "ValidateRefreshPolicyExpressionSyntax", this.IsPrivacyMarkupRequired(), null);
			}
			return engineErrorInfo;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005D10 File Offset: 0x00003F10
		public EngineErrorInfo GetDataSourcePool(string name, out IMEngineDataSourcePool pool)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				IMEngineDataSourcePool tmpPool = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					tmpPool = MInteropHelperImpl.GetDataSourcePool(name);
				}, "GetDataSourcePool", this.IsPrivacyMarkupRequired(), null);
				pool = tmpPool;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005D7C File Offset: 0x00003F7C
		public EngineErrorInfo GetSessionHandle(Guid rootActivityId, Guid parentActivityId, string sessionId, string dataSourceAllCredentials, out IMEngineSessionHandle handle)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				IMEngineSessionHandle tmpHandle = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					tmpHandle = MInteropHelperImpl.GetSessionHandle(rootActivityId, parentActivityId, sessionId, dataSourceAllCredentials);
				}, "GetSessionHandle", this.IsPrivacyMarkupRequired(), null);
				handle = tmpHandle;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005E00 File Offset: 0x00004000
		public EngineErrorInfo GetSharedQueries(string[] dataSourceConnectionStrings, bool isMExpressionDQ, out string[] queryNames, out string[] queryExpressions, out long entryQueryCount)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string[] tmpQueryNames = null;
				string[] tmpQueryExpressions = null;
				long tmpEntryQueryCount = 0L;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.GetSharedQueries(dataSourceConnectionStrings, isMExpressionDQ, out tmpQueryNames, out tmpQueryExpressions, out tmpEntryQueryCount);
				}, "GetSharedQueries", this.IsPrivacyMarkupRequired(), null);
				queryNames = tmpQueryNames;
				queryExpressions = tmpQueryExpressions;
				entryQueryCount = tmpEntryQueryCount;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005E94 File Offset: 0x00004094
		public EngineErrorInfo CheckForPowerBIV1MashupConnectionString(string dataSourceConnectionString, out bool isPowerBIMashup)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				bool tmpIsPowerBIMashup = false;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.CheckForPowerBIV1MashupConnectionString(dataSourceConnectionString, out tmpIsPowerBIMashup);
				}, "CheckForPowerBIV1MashupConnectionString", this.IsPrivacyMarkupRequired(), null);
				isPowerBIMashup = tmpIsPowerBIMashup;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005F00 File Offset: 0x00004100
		public EngineErrorInfo GetEntryQuery(string dataSourceConnectionString, out string entryQuery)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpEntryQuery = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.GetEntryQuery(dataSourceConnectionString, out tmpEntryQuery, false);
				}, "GetEntryQuery", this.IsPrivacyMarkupRequired(), null);
				entryQuery = tmpEntryQuery;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005F6C File Offset: 0x0000416C
		public EngineErrorInfo GetDataSourceKindFromConnectionString(string dataSourceConnectionString, out DQDataSourceKind dataSourceKind)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				DQDataSourceKind tmpDataSourceKind = DQDataSourceKind.UNKNOWN;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.GetDataSourceKindFromConnectionString(dataSourceConnectionString, out tmpDataSourceKind);
				}, "GetDataSourceKindFromConnectionString", this.IsPrivacyMarkupRequired(), null);
				dataSourceKind = tmpDataSourceKind;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005FD8 File Offset: 0x000041D8
		public EngineErrorInfo GetDataSourceReferenceJsonsForV3(string batchKey, string MProgram, string MQuery, string table, string partition, bool importOnlyPartition, bool allowUnknownFunctions, bool allowUknownCallsites, out string[] dataSourceReferenceJsons, out string[] dataSourceOptions, out string[] analyzableDocs, out DQDataSourceKind[] DSKinds, out string[] dsrKinds, out string[] dsrPaths, out bool foundUnknownFunctions, out bool foundUnknownCallsites, out bool foundPdfTables, out bool sensitivityLabelsSupported, out bool foundExcelWorkbooks, out bool prePostSqlForDQ, out bool fabricOneLakeDS)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string[] tmpDataSourceReferenceJsons = new string[0];
				string[] tmpDataSourceOptions = new string[0];
				string[] tmpAnalyzableDocs = new string[0];
				DQDataSourceKind[] tmpDSKinds = new DQDataSourceKind[0];
				string[] tmpDsrKinds = new string[0];
				string[] tmpDsrPaths = new string[0];
				bool tmpFoundUnknownFunctions = false;
				bool tmpFoundUnknownCallsites = false;
				bool tmpPdfTablesFound = false;
				bool tmpSensitivityLabelsSupported = false;
				bool tmpFoundExcelWorkbooks = false;
				bool tmpPrePostSqlForDQ = false;
				bool tmpfabricOneLakeDS = false;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.GetDataSourceReferenceJsonsForV3(batchKey, MProgram, MQuery, table, partition, importOnlyPartition, allowUnknownFunctions, allowUknownCallsites, out tmpDataSourceReferenceJsons, out tmpDataSourceOptions, out tmpAnalyzableDocs, out tmpDSKinds, out tmpDsrKinds, out tmpDsrPaths, out tmpFoundUnknownFunctions, out tmpFoundUnknownCallsites, out tmpPdfTablesFound, out tmpSensitivityLabelsSupported, out tmpFoundExcelWorkbooks, out tmpPrePostSqlForDQ, out tmpfabricOneLakeDS);
				}, MProgram, "GetDataSourceReferenceJsonsForV3", this.IsPrivacyMarkupRequired(), null);
				dataSourceReferenceJsons = tmpDataSourceReferenceJsons;
				analyzableDocs = tmpAnalyzableDocs;
				DSKinds = tmpDSKinds;
				dsrKinds = tmpDsrKinds;
				dsrPaths = tmpDsrPaths;
				foundUnknownFunctions = tmpFoundUnknownFunctions;
				foundUnknownCallsites = tmpFoundUnknownCallsites;
				foundPdfTables = tmpPdfTablesFound;
				sensitivityLabelsSupported = tmpSensitivityLabelsSupported;
				dataSourceOptions = tmpDataSourceOptions;
				foundExcelWorkbooks = tmpFoundExcelWorkbooks;
				prePostSqlForDQ = tmpPrePostSqlForDQ;
				fabricOneLakeDS = tmpfabricOneLakeDS;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000616C File Offset: 0x0000436C
		public EngineErrorInfo GetMinimizedPartitionMProgram(string MProgram, string MQuery, out string minimizedMProgram)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpMinimizedMProgram = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.GetMinimizedPartitionMProgram(MProgram, MQuery, out tmpMinimizedMProgram);
				}, MProgram, "GetMinimizedPartitionMProgram", this.IsPrivacyMarkupRequired(), null);
				minimizedMProgram = tmpMinimizedMProgram;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000061E4 File Offset: 0x000043E4
		public EngineErrorInfo GetNativeConnectionDetailsForV3DQ(string dsrJson, string options, string sourceExpressionText, string powerBIGlobalServiceFQDN, out string effectiveConnectionString, out string managedProvider, out string modelName, out bool shouldUseReadOnlyAccessMode)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpEffectiveConnectionString = null;
				string tmpManagedProvider = null;
				string tmpModelName = null;
				bool tmpShouldUseReadOnlyAccessMode = false;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.GetNativeConnectionDetailsForV3DQ(dsrJson, options, sourceExpressionText, powerBIGlobalServiceFQDN, out tmpEffectiveConnectionString, out tmpManagedProvider, out tmpModelName, out tmpShouldUseReadOnlyAccessMode);
				}, dsrJson, "GetNativeConnectionDetailsForV3DQ", this.IsPrivacyMarkupRequired(), null);
				effectiveConnectionString = tmpEffectiveConnectionString;
				managedProvider = tmpManagedProvider;
				modelName = tmpModelName;
				shouldUseReadOnlyAccessMode = tmpShouldUseReadOnlyAccessMode;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000629C File Offset: 0x0000449C
		public EngineErrorInfo GetAnalyzableDocForDualMode(string connectionString, out string dualModeConnectionString)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpDualModeConnectionString = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.GetAnalyzableDocForDualMode(connectionString, out tmpDualModeConnectionString);
				}, "GetAnalyzableDocForDualMode", this.IsPrivacyMarkupRequired(), null);
				dualModeConnectionString = tmpDualModeConnectionString;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006308 File Offset: 0x00004508
		public EngineErrorInfo GetEntryQueryExpression(string dataSourceConnectionString, out string entryQueryExpression)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpEntryQueryExpression = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.GetEntryQueryExpression(dataSourceConnectionString, out tmpEntryQueryExpression);
				}, "GetEntryQueryExpression", this.IsPrivacyMarkupRequired(), null);
				entryQueryExpression = tmpEntryQueryExpression;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006374 File Offset: 0x00004574
		public EngineErrorInfo GetSqlCubeApplyParameters(string cubeName, string applyParametersOrig, string applyParametersNew, out string MSql)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpMSql = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					tmpMSql = MInteropHelperImpl.GetSqlCubeApplyParameters(cubeName, applyParametersOrig, applyParametersNew);
				}, "GetSqlCubeApplyParameters", this.IsPrivacyMarkupRequired(), null);
				MSql = tmpMSql;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000063F0 File Offset: 0x000045F0
		public EngineErrorInfo ReplaceCubeApplyParameters(string connectionString, out string newConnectionString, out string defaultParameters)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpNewConnectionString = null;
				string tmpDefaultParameters = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.ReplaceCubeApplyParameters(connectionString, out tmpNewConnectionString, out tmpDefaultParameters);
				}, "ReplaceCubeApplyParameters", this.IsPrivacyMarkupRequired(), null);
				newConnectionString = tmpNewConnectionString;
				defaultParameters = tmpDefaultParameters;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000646C File Offset: 0x0000466C
		public EngineErrorInfo OverrideMAttribute(string attributeBlock, string attribute, string attrValue, out string mattributes)
		{
			EngineErrorInfo engineErrorInfo2;
			using (new MInteropHelper.UILocaleContext())
			{
				string tmpattr = null;
				EngineErrorInfo engineErrorInfo = this.HandleExceptions(delegate
				{
					tmpattr = MInteropHelperImpl.OverrideMAttribute(attributeBlock, attribute, attrValue);
				}, "OverrideMAttribute", false, null);
				mattributes = tmpattr;
				engineErrorInfo2 = engineErrorInfo;
			}
			return engineErrorInfo2;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000064E4 File Offset: 0x000046E4
		public EngineErrorInfo ClearStaticAnalysisBatch(string batchKey)
		{
			EngineErrorInfo engineErrorInfo;
			using (new MInteropHelper.UILocaleContext())
			{
				engineErrorInfo = this.HandleExceptions(delegate
				{
					MInteropHelperImpl.ClearStaticAnalysisBatch(batchKey);
				}, "ClearStaticAnalysisBatch", false, null);
			}
			return engineErrorInfo;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000653C File Offset: 0x0000473C
		private bool IsPrivacyMarkupRequired()
		{
			return this.serverType > ServerType.OnPrem;
		}

		// Token: 0x0400010F RID: 271
		private ServerType serverType;

		// Token: 0x02000045 RID: 69
		internal class UILocaleContext : IDisposable
		{
			// Token: 0x06002129 RID: 8489
			[DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
			private static extern ushort GetThreadUILanguage();

			// Token: 0x0600212A RID: 8490 RVA: 0x0004DF94 File Offset: 0x0004C194
			public UILocaleContext()
			{
				this.savedCulture = Thread.CurrentThread.CurrentCulture;
				this.savedUICulture = Thread.CurrentThread.CurrentUICulture;
				CultureInfo cultureInfo = CultureInfo.GetCultureInfo((int)MInteropHelper.UILocaleContext.GetThreadUILanguage());
				Thread.CurrentThread.CurrentCulture = cultureInfo;
				Thread.CurrentThread.CurrentCulture = cultureInfo;
				Thread.CurrentThread.CurrentUICulture = cultureInfo;
			}

			// Token: 0x0600212B RID: 8491 RVA: 0x0004DFF3 File Offset: 0x0004C1F3
			public void Dispose()
			{
				if (this.savedUICulture != null)
				{
					Thread.CurrentThread.CurrentCulture = this.savedCulture;
					Thread.CurrentThread.CurrentUICulture = this.savedUICulture;
					this.savedUICulture = null;
					this.savedCulture = null;
				}
			}

			// Token: 0x04001270 RID: 4720
			private CultureInfo savedCulture;

			// Token: 0x04001271 RID: 4721
			private CultureInfo savedUICulture;
		}
	}
}
