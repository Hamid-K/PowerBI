using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000265 RID: 613
	[Serializable]
	public sealed class ServiceValidationException : ServiceLocalException
	{
		// Token: 0x060015C4 RID: 5572 RVA: 0x0003CF47 File Offset: 0x0003BF47
		public ServiceValidationException()
		{
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x0003CF4F File Offset: 0x0003BF4F
		public ServiceValidationException(string message)
			: base(message)
		{
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x0003CF58 File Offset: 0x0003BF58
		public ServiceValidationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
