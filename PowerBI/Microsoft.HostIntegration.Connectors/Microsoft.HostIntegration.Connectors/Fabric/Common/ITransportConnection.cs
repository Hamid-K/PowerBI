using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200043F RID: 1087
	internal interface ITransportConnection : ITransportObject
	{
		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002608 RID: 9736
		Uri RemoteAddress { get; }

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06002609 RID: 9737
		Uri LocalAddress { get; }
	}
}
