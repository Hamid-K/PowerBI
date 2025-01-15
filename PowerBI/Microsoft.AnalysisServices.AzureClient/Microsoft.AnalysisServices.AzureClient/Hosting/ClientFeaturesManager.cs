using System;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.AzureClient.Configuration;

namespace Microsoft.AnalysisServices.AzureClient.Hosting
{
	// Token: 0x0200003F RID: 63
	internal static class ClientFeaturesManager
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x000097BC File Offset: 0x000079BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckIfUnrestrictedFallbackToInteractiveFlowIsEnabled()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<bool>(131074);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000097C8 File Offset: 0x000079C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckIfWamBasedSsoIsEnabled()
		{
			return !ClientFeaturesManager.GetCurrentConfiguredValue<bool>(131075);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000097D7 File Offset: 0x000079D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetHttpStreamBufferSize()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<int>(196609);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000097E3 File Offset: 0x000079E3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetTcpStreamBufferSize()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<int>(196610);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000097EF File Offset: 0x000079EF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetEndSessionTimeout()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<int>(196611);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000097FC File Offset: 0x000079FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckIfBinaryXmlaIsEnabled(out bool isBinaryForced)
		{
			BinaryXmlSupport currentConfiguredValue = ClientFeaturesManager.GetCurrentConfiguredValue<BinaryXmlSupport>(196614);
			isBinaryForced = currentConfiguredValue == BinaryXmlSupport.Forced;
			return currentConfiguredValue != BinaryXmlSupport.Disabled;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00009821 File Offset: 0x00007A21
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string GetBaseDiagnosticsDirectoryPath()
		{
			return ClientFeaturesManager.GetCurrentConfiguredValue<string>(262145);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000982D File Offset: 0x00007A2D
		private static TValue GetCurrentConfiguredValue<TValue>(int entry)
		{
			return (TValue)((object)ClientHostingManager.ClientConfiguration[entry]);
		}
	}
}
