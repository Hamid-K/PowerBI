using System;
using System.Runtime.Serialization;

namespace Microsoft.Identity.Client.Extensions.Msal
{
	// Token: 0x02000018 RID: 24
	public class MsalCachePersistenceException : Exception
	{
		// Token: 0x0600006A RID: 106 RVA: 0x000035EB File Offset: 0x000017EB
		public MsalCachePersistenceException()
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000035F3 File Offset: 0x000017F3
		public MsalCachePersistenceException(string message)
			: base(message)
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000035FC File Offset: 0x000017FC
		public MsalCachePersistenceException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003606 File Offset: 0x00001806
		protected MsalCachePersistenceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
