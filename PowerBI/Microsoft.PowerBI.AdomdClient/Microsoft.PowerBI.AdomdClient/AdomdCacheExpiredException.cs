using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000055 RID: 85
	[Serializable]
	public sealed class AdomdCacheExpiredException : AdomdException
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x0001F9DD File Offset: 0x0001DBDD
		internal AdomdCacheExpiredException()
		{
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001F9E5 File Offset: 0x0001DBE5
		internal AdomdCacheExpiredException(string message)
			: base(message)
		{
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001F9EE File Offset: 0x0001DBEE
		internal AdomdCacheExpiredException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001F9F8 File Offset: 0x0001DBF8
		private AdomdCacheExpiredException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
