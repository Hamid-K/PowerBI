using System;
using System.ServiceModel.Channels;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002C7 RID: 711
	internal interface ICreateMessage
	{
		// Token: 0x06001A4F RID: 6735
		Message CreateWcfMessage(ClientVersionInfo versionInfo);
	}
}
