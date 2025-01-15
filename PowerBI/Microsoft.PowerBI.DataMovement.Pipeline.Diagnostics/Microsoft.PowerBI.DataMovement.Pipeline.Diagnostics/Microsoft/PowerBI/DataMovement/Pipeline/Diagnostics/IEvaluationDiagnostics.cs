using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	public interface IEvaluationDiagnostics
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000095 RID: 149
		// (set) Token: 0x06000096 RID: 150
		EvaluationTraceContext EvaluationTraceContext { get; set; }
	}
}
