using System;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.AdomdClient.Configuration;

namespace Microsoft.AnalysisServices.AdomdClient.Hosting
{
	// Token: 0x02000160 RID: 352
	internal static class ClientFeaturesManager
	{
		// Token: 0x060010FB RID: 4347 RVA: 0x0003AF97 File Offset: 0x00039197
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckIfUnrestrictedFallbackToInteractiveFlowIsEnabled()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<bool>(131074);
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0003AFA3 File Offset: 0x000391A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckIfWamBasedSsoIsEnabled()
		{
			return !ClientFeaturesManager.GetCurrentConfiguredValue<bool>(131075);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0003AFB2 File Offset: 0x000391B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetHttpStreamBufferSize()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<int>(196609);
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0003AFBE File Offset: 0x000391BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetTcpStreamBufferSize()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<int>(196610);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0003AFCA File Offset: 0x000391CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetEndSessionTimeout()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<int>(196611);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0003AFD6 File Offset: 0x000391D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckIfHttpClientIsSupportedByDefault()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<bool>(196612);
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0003AFE2 File Offset: 0x000391E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long GetHttpClientPayloadQueueLimit()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<long>(196613);
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0003AFF0 File Offset: 0x000391F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckIfBinaryXmlaIsEnabled(out bool isBinaryForced)
		{
			BinaryXmlSupport currentConfiguredValue = ClientFeaturesManager.GetCurrentConfiguredValue<BinaryXmlSupport>(196614);
			isBinaryForced = currentConfiguredValue == BinaryXmlSupport.Forced;
			return currentConfiguredValue != BinaryXmlSupport.Disabled;
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0003B015 File Offset: 0x00039215
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string GetBaseDiagnosticsDirectoryPath()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<string>(262145);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0003B021 File Offset: 0x00039221
		private static TValue GetCurrentConfiguredValue<TValue>(int entry)
		{
			return (TValue)((object)ClientHostingManager.ClientConfiguration[entry]);
		}
	}
}
