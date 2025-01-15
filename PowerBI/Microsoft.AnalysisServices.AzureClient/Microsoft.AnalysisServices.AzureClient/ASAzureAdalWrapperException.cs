using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.AzureClient
{
	// Token: 0x0200000C RID: 12
	[Serializable]
	public sealed class ASAzureAdalWrapperException : Exception
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000025D4 File Offset: 0x000007D4
		public ASAzureAdalWrapperException()
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025DC File Offset: 0x000007DC
		public ASAzureAdalWrapperException(string message)
			: base(message)
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000025E5 File Offset: 0x000007E5
		public ASAzureAdalWrapperException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000025EF File Offset: 0x000007EF
		internal ASAzureAdalWrapperException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
