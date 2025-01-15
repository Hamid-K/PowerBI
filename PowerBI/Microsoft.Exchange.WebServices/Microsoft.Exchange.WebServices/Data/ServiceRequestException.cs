using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000264 RID: 612
	[Serializable]
	public class ServiceRequestException : ServiceRemoteException
	{
		// Token: 0x060015C1 RID: 5569 RVA: 0x0003CF2C File Offset: 0x0003BF2C
		public ServiceRequestException()
		{
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x0003CF34 File Offset: 0x0003BF34
		public ServiceRequestException(string message)
			: base(message)
		{
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x0003CF3D File Offset: 0x0003BF3D
		public ServiceRequestException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
