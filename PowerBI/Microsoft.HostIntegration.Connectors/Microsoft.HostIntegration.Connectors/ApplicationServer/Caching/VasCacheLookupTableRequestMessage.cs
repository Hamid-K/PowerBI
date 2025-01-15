using System;
using System.ServiceModel.Channels;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020003AB RID: 939
	internal class VasCacheLookupTableRequestMessage : ICreateMessage
	{
		// Token: 0x0600215E RID: 8542 RVA: 0x00066F27 File Offset: 0x00065127
		public VasCacheLookupTableRequestMessage(CacheLookupTableRequest messge, string action)
		{
			this._request = messge;
			this._action = action;
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x00066F3D File Offset: 0x0006513D
		public Message CreateWcfMessage(ClientVersionInfo versionInfo)
		{
			return Utility.CreateMessage(this._action, this._request);
		}

		// Token: 0x04001540 RID: 5440
		private CacheLookupTableRequest _request;

		// Token: 0x04001541 RID: 5441
		private string _action;
	}
}
