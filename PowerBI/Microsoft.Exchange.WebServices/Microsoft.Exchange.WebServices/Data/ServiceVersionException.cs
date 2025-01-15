using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000266 RID: 614
	[Serializable]
	public sealed class ServiceVersionException : ServiceLocalException
	{
		// Token: 0x060015C7 RID: 5575 RVA: 0x0003CF62 File Offset: 0x0003BF62
		public ServiceVersionException()
		{
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x0003CF6A File Offset: 0x0003BF6A
		public ServiceVersionException(string message)
			: base(message)
		{
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x0003CF73 File Offset: 0x0003BF73
		public ServiceVersionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
