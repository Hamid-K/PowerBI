using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Dns
{
	// Token: 0x0200025C RID: 604
	[Serializable]
	internal class DnsException : Win32Exception
	{
		// Token: 0x060015AC RID: 5548 RVA: 0x0003CD4A File Offset: 0x0003BD4A
		internal DnsException(int errorCode)
			: base(errorCode)
		{
		}
	}
}
