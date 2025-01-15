using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000267 RID: 615
	[Serializable]
	public sealed class ServiceXmlDeserializationException : ServiceLocalException
	{
		// Token: 0x060015CA RID: 5578 RVA: 0x0003CF7D File Offset: 0x0003BF7D
		public ServiceXmlDeserializationException()
		{
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0003CF85 File Offset: 0x0003BF85
		public ServiceXmlDeserializationException(string message)
			: base(message)
		{
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x0003CF8E File Offset: 0x0003BF8E
		public ServiceXmlDeserializationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
