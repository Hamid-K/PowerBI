using System;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.AdomdClient.Configuration;

namespace Microsoft.AnalysisServices.AdomdClient.Hosting
{
	// Token: 0x02000160 RID: 352
	internal static class ClientFeaturesManager
	{
		// Token: 0x06001108 RID: 4360 RVA: 0x0003B2C7 File Offset: 0x000394C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckIfUnrestrictedFallbackToInteractiveFlowIsEnabled()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<bool>(131074);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0003B2D3 File Offset: 0x000394D3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckIfWamBasedSsoIsEnabled()
		{
			return !ClientFeaturesManager.GetCurrentConfiguredValue<bool>(131075);
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0003B2E2 File Offset: 0x000394E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetHttpStreamBufferSize()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<int>(196609);
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0003B2EE File Offset: 0x000394EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetTcpStreamBufferSize()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<int>(196610);
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x0003B2FA File Offset: 0x000394FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetEndSessionTimeout()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<int>(196611);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0003B306 File Offset: 0x00039506
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckIfHttpClientIsSupportedByDefault()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<bool>(196612);
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0003B312 File Offset: 0x00039512
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long GetHttpClientPayloadQueueLimit()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<long>(196613);
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0003B320 File Offset: 0x00039520
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckIfBinaryXmlaIsEnabled(out bool isBinaryForced)
		{
			BinaryXmlSupport currentConfiguredValue = ClientFeaturesManager.GetCurrentConfiguredValue<BinaryXmlSupport>(196614);
			isBinaryForced = currentConfiguredValue == BinaryXmlSupport.Forced;
			return currentConfiguredValue != BinaryXmlSupport.Disabled;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0003B345 File Offset: 0x00039545
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string GetBaseDiagnosticsDirectoryPath()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<string>(262145);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0003B351 File Offset: 0x00039551
		private static TValue GetCurrentConfiguredValue<TValue>(int entry)
		{
			return (TValue)((object)ClientHostingManager.ClientConfiguration[entry]);
		}
	}
}
