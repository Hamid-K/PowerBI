using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000268 RID: 616
	[Serializable]
	public class ServiceXmlSerializationException : ServiceLocalException
	{
		// Token: 0x060015CD RID: 5581 RVA: 0x0003CF98 File Offset: 0x0003BF98
		public ServiceXmlSerializationException()
		{
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0003CFA0 File Offset: 0x0003BFA0
		public ServiceXmlSerializationException(string message)
			: base(message)
		{
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x0003CFA9 File Offset: 0x0003BFA9
		public ServiceXmlSerializationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
