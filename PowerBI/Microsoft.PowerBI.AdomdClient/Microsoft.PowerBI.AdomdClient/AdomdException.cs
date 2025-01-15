using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000060 RID: 96
	[Serializable]
	public class AdomdException : Exception
	{
		// Token: 0x0600063B RID: 1595 RVA: 0x00022130 File Offset: 0x00020330
		internal AdomdException()
		{
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00022138 File Offset: 0x00020338
		protected AdomdException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00022142 File Offset: 0x00020342
		internal AdomdException(string message)
			: base(message)
		{
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0002214B File Offset: 0x0002034B
		internal AdomdException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
