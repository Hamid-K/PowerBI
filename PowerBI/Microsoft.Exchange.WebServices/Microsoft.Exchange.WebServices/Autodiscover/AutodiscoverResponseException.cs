using System;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000258 RID: 600
	[Serializable]
	public class AutodiscoverResponseException : ServiceRemoteException
	{
		// Token: 0x060015A3 RID: 5539 RVA: 0x0003CCB3 File Offset: 0x0003BCB3
		internal AutodiscoverResponseException(AutodiscoverErrorCode errorCode, string message)
			: base(message)
		{
			this.errorCode = errorCode;
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x0003CCC3 File Offset: 0x0003BCC3
		public AutodiscoverErrorCode ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x040012B1 RID: 4785
		private AutodiscoverErrorCode errorCode;
	}
}
