using System;
using System.ServiceModel.Channels;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003E5 RID: 997
	internal interface ISecurityProvider
	{
		// Token: 0x060022FF RID: 8959
		BindingElementCollection InitializeCommunication(BindingElementCollection collection);
	}
}
