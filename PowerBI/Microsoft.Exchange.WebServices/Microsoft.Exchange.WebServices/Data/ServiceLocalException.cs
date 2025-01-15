using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000255 RID: 597
	[Serializable]
	public class ServiceLocalException : Exception
	{
		// Token: 0x06001599 RID: 5529 RVA: 0x0003CC45 File Offset: 0x0003BC45
		public ServiceLocalException()
		{
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x0003CC4D File Offset: 0x0003BC4D
		public ServiceLocalException(string message)
			: base(message)
		{
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x0003CC56 File Offset: 0x0003BC56
		public ServiceLocalException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
