using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000253 RID: 595
	[Serializable]
	public class ServiceRemoteException : Exception
	{
		// Token: 0x06001593 RID: 5523 RVA: 0x0003CC08 File Offset: 0x0003BC08
		public ServiceRemoteException()
		{
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x0003CC10 File Offset: 0x0003BC10
		public ServiceRemoteException(string message)
			: base(message)
		{
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0003CC19 File Offset: 0x0003BC19
		public ServiceRemoteException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
