using System;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Evaluator.Services;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019AB RID: 6571
	internal static class DefaultRemoteServiceFactories
	{
		// Token: 0x040056AB RID: 22187
		public static readonly IRemoteServiceFactory[] Factories = new IRemoteServiceFactory[]
		{
			new RemoteEvaluationConstantsFactory(),
			new RemoteGarbageCollectionService(),
			new RemoteEnvironment(),
			new RemoteTracingServiceFactory(),
			new RemoteEngineFactory(),
			new RemoteThreadPoolServiceFactory(),
			new RemoteLibraryServiceFactory(),
			new RemoteDiagnosticsServiceFactory(),
			new RemoteAccessReportingServiceFactory(),
			new RemoteCredentialServiceFactory(),
			new RemoteCultureServiceFactory(),
			new RemoteTimeZoneServiceFactory(),
			new RemoteEmbeddedValueLoggingServiceFactory(),
			new RemoteFirewallRuleServiceFactory(),
			new RemoteConnectionGovernanceServiceFactory(),
			new RemoteCurrentTimeServiceFactory(),
			new RemoteUniqueIdServiceFactory(),
			new RemoteGetStackFrameExtendedInfoFactory(),
			new RemotePartitionDisplayNameServiceFactory(),
			new RemotePartitionProgressServiceFactory(),
			new RemoteTempPageServiceFactory(),
			new RemotePersistentCacheFactory(),
			new RemoteClearableTransientCacheFactory(),
			new RemoteQueryPermissionServiceFactory(),
			new RemoteActionPermissionServiceFactory(),
			new RemoteResourcePathServiceFactory(),
			new RemoteSourceErrorExceptionServiceFactory(),
			new RemoteTempDirectoryServiceFactory(),
			new RemoteValueBufferingServiceFactory(),
			new RemoteVariableServiceFactory(),
			new RemoteSamplingServiceFactory(),
			new RemotePackageSectionConfigValidatorFactory(),
			new RemoteFeatureLoggingServiceFactory(),
			new RemoteRedirectPolicyServiceFactory(),
			new RemoteLifetimeServiceFactory(),
			new RemoteCancellationServiceFactory(),
			new RemoteApplicationConfigurationService(),
			new RemoteFoldingFailureServiceFactory(),
			new RemoteKnownExceptionServiceFactory(),
			new RemoteFirewallPlanServiceFactory(),
			new RemoteRequestTracingServiceFactory(),
			new RemoteConfigurationPropertyServiceFactory(),
			new RemoteDocumentEvaluationConfigServiceFactory(),
			new RemoteTraitTrackingServiceFactory(),
			new RemoteInformationProtectionPrivacyServiceFactory(),
			new RemoteMipService(),
			new RemoteHttpUriRewritingService()
		};
	}
}
