using System;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.PlatformHost;
using Microsoft.ASPaaS.AnalysisServer.Interfaces.Common.MInterop;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x02000024 RID: 36
	[ComVisible(true)]
	[Guid("2EE1A2FC-D4CA-40ef-A195-B19871D7572D")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IMInteropHelper
	{
		// Token: 0x0600008A RID: 138
		EngineErrorInfo Initialize(IJwtTokenHelperMInterop jwtTokenHelper, IEngineTracer tracer, MEngineSettings settings, MEngineSettingsForContainerPool processingPoolSettings, ServerType serverType, string additionalExtensions, string disabledExtensions, string customExtensionDirectories, bool customExtensionDirectoryWatchForChanges, bool customExtensionDirectoryIncludeSubdirectories, string certifiedExtensionsAllowlist, string certifiedExtensionDirectories, bool certifiedExtensionDirectoryWatchForChanges, bool certifiedExtensionDirectoryIncludeSubdirectories);

		// Token: 0x0600008B RID: 139
		EngineErrorInfo DisposeHelper();

		// Token: 0x0600008C RID: 140
		EngineErrorInfo RemoveSecretsFromCredentialJson(string credentialJson, string dataSourceReferenceJson, out string result);

		// Token: 0x0600008D RID: 141
		EngineErrorInfo RemoveSecretsFromConnectionString(string connectionString, out string result);

		// Token: 0x0600008E RID: 142
		EngineErrorInfo EscapeIdentifier(string identifier, out string result);

		// Token: 0x0600008F RID: 143
		EngineErrorInfo EscapeString(string stringToEscape, out string result);

		// Token: 0x06000090 RID: 144
		EngineErrorInfo ConvertASCredentialsToM(string dataSourceName, string credentialProp, string unattendedUsername, string unattendedPassword, out string credentialsConverted, out int impersonationMode);

		// Token: 0x06000091 RID: 145
		EngineErrorInfo ParseCredentials(string credentialsProp, out string usernameStr, out string passwordStr, out int impersonationMode);

		// Token: 0x06000092 RID: 146
		EngineErrorInfo ConvertDataSourceToM(string connectionDetailsProp, string contextExpressionProp, string processedCredential, string optionsProp, out string dataSourceExpression, out string dataSourceSettingsJson, out string completeNormalizedResourcePath, out string sessionCredentials, out bool isCredentialKindOAuth);

		// Token: 0x06000093 RID: 147
		EngineErrorInfo ConvertConnectionStringToDetails(string connectionString, string provider, int impersonationMode, string username, string password, out string connectionDetails, out string connectionCredentials);

		// Token: 0x06000094 RID: 148
		EngineErrorInfo BuildConnectionString(string MProgram, string sessionId, string dataSourcePool, string dataSourceSettings, string dataAccessOptionsProp, bool enableStaticFirewallPlan, string cachePath, int maxCacheSizeInMegaBytes, string tempPath, int maxTempSizeInMegaBytes, bool throwFoldingFailures, bool isPBIDesktop, out string connectionString);

		// Token: 0x06000095 RID: 149
		EngineErrorInfo GetNamedDependencies(string MQuery, out string[] mashupDependencies, out string[] mashupDependenciesByDSResourcePath);

		// Token: 0x06000096 RID: 150
		EngineErrorInfo GetEffectiveConnectionStringForPowerBI(string variable, string MProgram, string MQuery, bool useScaffolding, bool forDqTables, bool minimize, out string effectiveConnectionString);

		// Token: 0x06000097 RID: 151
		EngineErrorInfo GetEffectiveConnectionStringForPowerBIDQV3(string variable, string MProgram, string MQuery, out string effectiveConnectionString);

		// Token: 0x06000098 RID: 152
		EngineErrorInfo GetEffectiveConnectionStringForPowerBIDQV3LiftParameters(string variable, string MProgram, string MQuery, string[] parametersToLift, bool useLegacyConnectionStringFlavor, out string effectiveConnectionString, out string[] liftedQueries, out string[] mParameters, out MParameterType[] mParameterTypes);

		// Token: 0x06000099 RID: 153
		EngineErrorInfo GetEffectiveConnectionStringForPowerBIDQV3Legacy(string variable, string MProgram, string MQuery, out string effectiveConnectionString);

		// Token: 0x0600009A RID: 154
		EngineErrorInfo ApplyParameterSubstitution(string connectionString, string[] paramNames, string[] paramValues, out string newConnectionString);

		// Token: 0x0600009B RID: 155
		EngineErrorInfo AppendDataSourcePoolToConnectionString(string connectionString, string poolName, out string newConnectionString);

		// Token: 0x0600009C RID: 156
		EngineErrorInfo AddParameterToConnectionString(string connectionString, string paramName, string paramValue, bool append, bool appendToGatewayConnectionString, out string newConnectionString);

		// Token: 0x0600009D RID: 157
		EngineErrorInfo TraceMProgramFromConnectionString(string connectionString);

		// Token: 0x0600009E RID: 158
		EngineErrorInfo GetDataSourcesUsedByQuery(string MProgram, string MQuery, string table, string partition, out string[] resourcePaths);

		// Token: 0x0600009F RID: 159
		EngineErrorInfo GetDataSourceKindsFromMProgram(string MProgram, out string[] dataSourceKinds);

		// Token: 0x060000A0 RID: 160
		EngineErrorInfo GetDataSourcesUsedByProgram(string MProgram, out string[] resourcePaths);

		// Token: 0x060000A1 RID: 161
		EngineErrorInfo GetMParameterType(string parameterExpression, out string parameterType);

		// Token: 0x060000A2 RID: 162
		EngineErrorInfo GetServerAndDatabase(string dataSourceReferenceJson, out string server, out string database);

		// Token: 0x060000A3 RID: 163
		EngineErrorInfo GetDataSourceKind(string dataSourceReferenceJson, out DQDataSourceKind kind);

		// Token: 0x060000A4 RID: 164
		EngineErrorInfo GetNormalizedResourcePathByKindPath(string kind, string path, out string normalizedResourcePath);

		// Token: 0x060000A5 RID: 165
		EngineErrorInfo GetNormalizedResourcePathByKindPathJson(string kindPathJson, out string normalizedResourcePath);

		// Token: 0x060000A6 RID: 166
		EngineErrorInfo GetProviderSpecificConnectionDetails(string dataSourceName, string dataSourceReferenceJson, string credentialJson, string dataSourceOptionsJson, string serverVersion, string powerBIGlobalServiceFQDN, out string connectionString, out string managedProvider);

		// Token: 0x060000A7 RID: 167
		EngineErrorInfo ValidateDataAccessOptionsJson(string json);

		// Token: 0x060000A8 RID: 168
		EngineErrorInfo ValidateConnectionDetailsJson(string json, string dataSourceName);

		// Token: 0x060000A9 RID: 169
		EngineErrorInfo ValidateCredentialJson(string json, string dataSourceName);

		// Token: 0x060000AA RID: 170
		EngineErrorInfo ValidateOptionsJson(string json, string dataSourceName);

		// Token: 0x060000AB RID: 171
		EngineErrorInfo ValidateContextExpressionSyntax(string expression, string dataSourceName);

		// Token: 0x060000AC RID: 172
		EngineErrorInfo ValidateSharedExpressionSyntax(string expression, string name);

		// Token: 0x060000AD RID: 173
		EngineErrorInfo ValidatePartitionExpressionSyntax(string expression, string tableName, string partitionName);

		// Token: 0x060000AE RID: 174
		EngineErrorInfo ValidateRefreshPolicyExpressionSyntax(string expression, string tableName, string type);

		// Token: 0x060000AF RID: 175
		EngineErrorInfo GetDataSourcePool(string poolName, out IMEngineDataSourcePool pool);

		// Token: 0x060000B0 RID: 176
		EngineErrorInfo GetSessionHandle(Guid rootActivityId, Guid parentActivityId, string sessionId, string dataSourceAllCredentials, out IMEngineSessionHandle handle);

		// Token: 0x060000B1 RID: 177
		EngineErrorInfo GetSharedQueries(string[] dataSourceConnectionStrings, bool isMExpressionDQ, out string[] queryNames, out string[] queryExpressions, out long entryQueryCount);

		// Token: 0x060000B2 RID: 178
		EngineErrorInfo GetEntryQuery(string dataSourceConnectionString, out string entryQuery);

		// Token: 0x060000B3 RID: 179
		EngineErrorInfo GetEntryQueryExpression(string dataSourceConnectionString, out string entryQueryExpression);

		// Token: 0x060000B4 RID: 180
		EngineErrorInfo GetDataSourceKindFromConnectionString(string dataSourceConnectionString, out DQDataSourceKind dataSourceKind);

		// Token: 0x060000B5 RID: 181
		EngineErrorInfo CheckForPowerBIV1MashupConnectionString(string dataSourceConnectionString, out bool isPowerBIMashup);

		// Token: 0x060000B6 RID: 182
		EngineErrorInfo GetDataSourceReferenceJsonsForV3(string batchKey, string MProgram, string MQuery, string table, string partition, bool importOnlyPartition, bool allowUnknownFunctions, bool allowUknownCallsites, out string[] dataSourceReferenceJsons, out string[] dataSourceOptions, out string[] analyzableDocs, out DQDataSourceKind[] DSKinds, out string[] dsrKinds, out string[] dsrPaths, out bool foundUnknownFunctions, out bool foundUnknownCallsites, out bool foundPdfTables, out bool sensitivityLabelsSupported, out bool foundExcelWorkbooks, out bool prePostSqlForDQ, out bool fabricOneLakeDS);

		// Token: 0x060000B7 RID: 183
		EngineErrorInfo GetMinimizedPartitionMProgram(string MProgram, string MQuery, out string minimizedMProgram);

		// Token: 0x060000B8 RID: 184
		EngineErrorInfo GetNativeConnectionDetailsForV3DQ(string dsrJson, string options, string sourceExpressionText, string powerBIGlobalServiceFQDN, out string effectiveConnectionString, out string managedProvider, out string modelName, out bool shouldUseReadOnlyAccessMode);

		// Token: 0x060000B9 RID: 185
		EngineErrorInfo GetAnalyzableDocForDualMode(string connectionString, out string dualModeConnectionString);

		// Token: 0x060000BA RID: 186
		EngineErrorInfo GetSqlCubeApplyParameters(string cubeName, string applyParametersOrig, string applyParametersNew, out string MSql);

		// Token: 0x060000BB RID: 187
		EngineErrorInfo ReplaceCubeApplyParameters(string connectionString, out string newConnectionString, out string defaultParameters);

		// Token: 0x060000BC RID: 188
		EngineErrorInfo OverrideMAttribute(string attributeBlock, string attribute, string attrValue, out string mattributes);

		// Token: 0x060000BD RID: 189
		EngineErrorInfo ClearStaticAnalysisBatch(string batchKey);
	}
}
