using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000055 RID: 85
	[Serializable]
	public sealed class AdomdCacheExpiredException : AdomdException
	{
		// Token: 0x0600053E RID: 1342 RVA: 0x0001FD0D File Offset: 0x0001DF0D
		internal AdomdCacheExpiredException()
		{
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001FD15 File Offset: 0x0001DF15
		internal AdomdCacheExpiredException(string message)
			: base(message)
		{
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001FD1E File Offset: 0x0001DF1E
		internal AdomdCacheExpiredException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001FD28 File Offset: 0x0001DF28
		private AdomdCacheExpiredException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
