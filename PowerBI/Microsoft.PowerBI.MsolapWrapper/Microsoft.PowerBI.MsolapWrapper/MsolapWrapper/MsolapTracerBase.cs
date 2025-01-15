using System;
using System.Diagnostics;

namespace MsolapWrapper
{
	// Token: 0x02000083 RID: 131
	public abstract class MsolapTracerBase
	{
		// Token: 0x060001CF RID: 463
		public abstract void Trace(TraceLevel in_eTraceLevel, string in_strMessage);

		// Token: 0x060001D0 RID: 464 RVA: 0x00006034 File Offset: 0x00005434
		public MsolapTracerBase()
		{
		}
	}
}
