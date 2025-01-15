using System;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.Configuration;

namespace Microsoft.AnalysisServices.Hosting
{
	// Token: 0x02000156 RID: 342
	internal static class ClientFeaturesManager
	{
		// Token: 0x06001197 RID: 4503 RVA: 0x0003DC57 File Offset: 0x0003BE57
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckIfUnrestrictedFallbackToInteractiveFlowIsEnabled()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<bool>(131074);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0003DC63 File Offset: 0x0003BE63
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckIfWamBasedSsoIsEnabled()
		{
			return !ClientFeaturesManager.GetCurrentConfiguredValue<bool>(131075);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0003DC72 File Offset: 0x0003BE72
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetHttpStreamBufferSize()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<int>(196609);
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0003DC7E File Offset: 0x0003BE7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetTcpStreamBufferSize()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<int>(196610);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0003DC8A File Offset: 0x0003BE8A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetEndSessionTimeout()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<int>(196611);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0003DC96 File Offset: 0x0003BE96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckIfHttpClientIsSupportedByDefault()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<bool>(196612);
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0003DCA2 File Offset: 0x0003BEA2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long GetHttpClientPayloadQueueLimit()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<long>(196613);
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0003DCB0 File Offset: 0x0003BEB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckIfBinaryXmlaIsEnabled(out bool isBinaryForced)
		{
			BinaryXmlSupport currentConfiguredValue = ClientFeaturesManager.GetCurrentConfiguredValue<BinaryXmlSupport>(196614);
			isBinaryForced = currentConfiguredValue == BinaryXmlSupport.Forced;
			return currentConfiguredValue != BinaryXmlSupport.Disabled;
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0003DCD5 File Offset: 0x0003BED5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string GetBaseDiagnosticsDirectoryPath()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<string>(262145);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x0003DCE1 File Offset: 0x0003BEE1
		private static TValue GetCurrentConfiguredValue<TValue>(int entry)
		{
			return (TValue)((object)ClientHostingManager.ClientConfiguration[entry]);
		}
	}
}
