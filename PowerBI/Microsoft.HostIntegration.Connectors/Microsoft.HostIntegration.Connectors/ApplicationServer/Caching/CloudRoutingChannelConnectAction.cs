using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002A7 RID: 679
	[DataContract]
	internal enum CloudRoutingChannelConnectAction
	{
		// Token: 0x04000D8E RID: 3470
		[EnumMember]
		ConnectAlways = 1,
		// Token: 0x04000D8F RID: 3471
		[EnumMember]
		ConnectDesiredEndpointOnly
	}
}
