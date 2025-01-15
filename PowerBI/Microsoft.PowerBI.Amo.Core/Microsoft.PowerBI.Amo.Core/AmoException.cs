using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200006E RID: 110
	[Serializable]
	public class AmoException : Exception
	{
		// Token: 0x060005F0 RID: 1520 RVA: 0x00022596 File Offset: 0x00020796
		public AmoException()
		{
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0002259E File Offset: 0x0002079E
		protected AmoException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000225A8 File Offset: 0x000207A8
		public AmoException(string message)
			: base(message)
		{
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x000225B1 File Offset: 0x000207B1
		public AmoException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
