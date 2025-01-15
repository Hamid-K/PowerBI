using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.ReportServer.WebApi.ASConnection
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	public class RlsNotAuthorizedForModelException : Exception
	{
		// Token: 0x06000133 RID: 307 RVA: 0x000079D4 File Offset: 0x00005BD4
		public RlsNotAuthorizedForModelException()
		{
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000079E7 File Offset: 0x00005BE7
		public RlsNotAuthorizedForModelException(string message)
			: base(message)
		{
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000079FB File Offset: 0x00005BFB
		public RlsNotAuthorizedForModelException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007A10 File Offset: 0x00005C10
		protected RlsNotAuthorizedForModelException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x040000C6 RID: 198
		public readonly string ErrorCode = "RLSNotAuthorizedForModel";
	}
}
