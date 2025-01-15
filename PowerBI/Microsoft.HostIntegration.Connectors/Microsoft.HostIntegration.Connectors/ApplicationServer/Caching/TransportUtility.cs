using System;
using System.ServiceModel.Channels;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002BA RID: 698
	internal static class TransportUtility
	{
		// Token: 0x0600198B RID: 6539 RVA: 0x0004BD98 File Offset: 0x00049F98
		public static void AddAuthenticationHeader(Message message, DataCacheSecurity dataCacheSecurity, TimeSpan timeout)
		{
			string text;
			switch (dataCacheSecurity.AuthorizationType)
			{
			case AuthorizationType.Token:
				text = dataCacheSecurity.AcsTokenManager.GetOrRefreshToken(timeout);
				goto IL_0039;
			case AuthorizationType.SharedKey:
				text = dataCacheSecurity.SharedKey;
				goto IL_0039;
			}
			throw new NotSupportedException();
			IL_0039:
			MessageHeader messageHeader = MessageHeader.CreateHeader("Authorization", "urn:AppFabricCaching", text, true);
			message.Headers.Add(messageHeader);
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0004BDFC File Offset: 0x00049FFC
		public static void AddSmartRoutingHeader(Message message, bool isSmartRouting)
		{
			MessageHeader messageHeader = MessageHeader.CreateHeader("SmartRouting", "urn:AppFabricCaching", isSmartRouting, true);
			message.Headers.Add(messageHeader);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0004BE2C File Offset: 0x0004A02C
		public static string GetAuthenticationHeader(Message message)
		{
			return Utility.GetMessageHeader<string>(message, "Authorization", "urn:AppFabricCaching");
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0004BE40 File Offset: 0x0004A040
		public static void AddVipToDipHeader(Message message, EndpointID vipEndpoint)
		{
			MessageHeader messageHeader = MessageHeader.CreateHeader("VipForDip", "urn:AppFabricCaching", vipEndpoint.UriString, true);
			message.Headers.Add(messageHeader);
		}
	}
}
