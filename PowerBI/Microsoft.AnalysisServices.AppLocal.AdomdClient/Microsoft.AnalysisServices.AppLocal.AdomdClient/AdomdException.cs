using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000060 RID: 96
	[Serializable]
	public class AdomdException : Exception
	{
		// Token: 0x06000648 RID: 1608 RVA: 0x00022460 File Offset: 0x00020660
		internal AdomdException()
		{
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00022468 File Offset: 0x00020668
		protected AdomdException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00022472 File Offset: 0x00020672
		internal AdomdException(string message)
			: base(message)
		{
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0002247B File Offset: 0x0002067B
		internal AdomdException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
