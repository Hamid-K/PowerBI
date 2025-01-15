using System;
using System.ServiceModel;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000453 RID: 1107
	[Serializable]
	internal class RemoteDownException : CommunicationException
	{
		// Token: 0x060026BB RID: 9915 RVA: 0x00076B87 File Offset: 0x00074D87
		public RemoteDownException(Exception e)
			: base("Connection rejected by the remote side", e)
		{
		}
	}
}
