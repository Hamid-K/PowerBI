using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.SemanticQueryEngine
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	internal sealed class SemanticQueryEngineException : RSException
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00004BA0 File Offset: 0x00002DA0
		public SemanticQueryEngineException(string message)
			: this(message, null)
		{
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004BAA File Offset: 0x00002DAA
		public SemanticQueryEngineException(string message, Exception inner)
			: base(ErrorCode.rsSemanticQueryEngineError, message, inner, RSTrace.SQETracer, null, Array.Empty<object>())
		{
		}
	}
}
