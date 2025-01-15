using System;
using System.ServiceModel.Channels;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200045E RID: 1118
	internal interface IMessageHandler
	{
		// Token: 0x0600271A RID: 10010
		void ProcessMessage(Message msg);
	}
}
