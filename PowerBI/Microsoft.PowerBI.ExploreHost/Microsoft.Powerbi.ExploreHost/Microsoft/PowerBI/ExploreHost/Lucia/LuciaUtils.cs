using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.BusinessIntelligence;
using Microsoft.Lucia.Core;
using Microsoft.PowerBI.Lucia;
using Microsoft.PowerBI.Lucia.Interpret;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x0200006C RID: 108
	internal static class LuciaUtils
	{
		// Token: 0x06000300 RID: 768 RVA: 0x00009D68 File Offset: 0x00007F68
		internal static string CreateNewLuciaDataIndexWorkingDirectory(string workingDirectoryRoot)
		{
			return Path.Combine(workingDirectoryRoot, "Index-" + Guid.NewGuid().ToString());
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00009D98 File Offset: 0x00007F98
		internal static void AddExceptionDetails(this DataIndexTelemetry telemetry, string message, Exception ex)
		{
			if (ex is OperationCanceledException)
			{
				telemetry.Status = DataIndexOperationStatus.Canceled;
				return;
			}
			telemetry.Status = DataIndexOperationStatus.Failed;
			telemetry.Message = message;
			telemetry.Exception = ex;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00009DBF File Offset: 0x00007FBF
		internal static string SerializeInterpretResponse(Version requestedVersion, InterpretDiagnosticMessage diagnosticMessage)
		{
			return InterpretContractsSerializer.ToJsonString<DesktopResultContext>(InterpretResponseFactory<DesktopResultContext>.Create(requestedVersion, new InterpretResult<DesktopResultContext>
			{
				DiagnosticMessages = new List<InterpretDiagnosticMessage> { diagnosticMessage }
			}));
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00009DE4 File Offset: 0x00007FE4
		internal static IEnumerable<FeatureSwitch> GetFeatureSwitches(FeatureSwitches featureSwitches)
		{
			IFeatureSwitchesProxy featureSwitchesProxy = ((featureSwitches != null) ? featureSwitches.FeatureSwitchesProxy : null);
			ILuciaFeatureSwitchProxy luciaFeatureSwitchProxy = featureSwitchesProxy as ILuciaFeatureSwitchProxy;
			if (luciaFeatureSwitchProxy == null && featureSwitchesProxy != null)
			{
				luciaFeatureSwitchProxy = new LuciaUtils.LuciaFeatureSwitchProxyWrapper(featureSwitchesProxy);
			}
			return LuciaFeatureSwitchProxyExtensions.GetFeatureSwitches(luciaFeatureSwitchProxy);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00009E18 File Offset: 0x00008018
		internal static bool HasNullOrEmptyUtterance<TContext>(this InterpretRequest<TContext> interpretRequest)
		{
			return string.IsNullOrEmpty(interpretRequest.Utterance);
		}

		// Token: 0x020000CE RID: 206
		private sealed class LuciaFeatureSwitchProxyWrapper : ILuciaFeatureSwitchProxy
		{
			// Token: 0x06000444 RID: 1092 RVA: 0x0000EBA3 File Offset: 0x0000CDA3
			internal LuciaFeatureSwitchProxyWrapper(IFeatureSwitchesProxy featureSwitchesProxy)
			{
				this._featureSwitchesProxy = featureSwitchesProxy;
			}

			// Token: 0x06000445 RID: 1093 RVA: 0x0000EBB2 File Offset: 0x0000CDB2
			public bool GetSwitchValue(string featureSwitchName)
			{
				return this._featureSwitchesProxy.GetSwitchValue(featureSwitchName);
			}

			// Token: 0x040002C2 RID: 706
			private readonly IFeatureSwitchesProxy _featureSwitchesProxy;
		}
	}
}
