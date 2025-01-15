using System;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000257 RID: 599
	[Serializable]
	public class AutodiscoverRemoteException : ServiceRemoteException
	{
		// Token: 0x0600159F RID: 5535 RVA: 0x0003CC7B File Offset: 0x0003BC7B
		public AutodiscoverRemoteException(AutodiscoverError error)
		{
			this.error = error;
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0003CC8A File Offset: 0x0003BC8A
		public AutodiscoverRemoteException(string message, AutodiscoverError error)
			: base(message)
		{
			this.error = error;
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x0003CC9A File Offset: 0x0003BC9A
		public AutodiscoverRemoteException(string message, AutodiscoverError error, Exception innerException)
			: base(message, innerException)
		{
			this.error = error;
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x0003CCAB File Offset: 0x0003BCAB
		public AutodiscoverError Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040012B0 RID: 4784
		private AutodiscoverError error;
	}
}
