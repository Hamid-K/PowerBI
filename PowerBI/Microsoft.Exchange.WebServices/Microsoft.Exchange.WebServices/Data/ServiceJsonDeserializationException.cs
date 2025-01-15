using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000262 RID: 610
	[Serializable]
	public sealed class ServiceJsonDeserializationException : ServiceLocalException
	{
		// Token: 0x060015BA RID: 5562 RVA: 0x0003CEC7 File Offset: 0x0003BEC7
		public ServiceJsonDeserializationException()
		{
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x0003CECF File Offset: 0x0003BECF
		public ServiceJsonDeserializationException(string message)
			: base(message)
		{
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x0003CED8 File Offset: 0x0003BED8
		public ServiceJsonDeserializationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
