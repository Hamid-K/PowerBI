using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x02000048 RID: 72
	internal class DsrObjectWriterBase : StreamingDsrWriterWrapperBase
	{
		// Token: 0x0600018A RID: 394 RVA: 0x00004CC9 File Offset: 0x00002EC9
		internal override void Begin()
		{
			base.Writer.BeginObject();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00004CD6 File Offset: 0x00002ED6
		internal override void End()
		{
			base.Writer.EndObject();
		}
	}
}
