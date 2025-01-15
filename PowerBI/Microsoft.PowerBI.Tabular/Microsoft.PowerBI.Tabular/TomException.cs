using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000118 RID: 280
	[Serializable]
	public class TomException : AmoException
	{
		// Token: 0x0600120F RID: 4623 RVA: 0x0007E908 File Offset: 0x0007CB08
		internal TomException()
		{
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0007E910 File Offset: 0x0007CB10
		internal TomException(string message)
			: base(message)
		{
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0007E919 File Offset: 0x0007CB19
		internal TomException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0007E923 File Offset: 0x0007CB23
		internal TomException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
