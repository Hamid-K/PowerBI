using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000027 RID: 39
	internal sealed class ReportItemRendering : MemberBase
	{
		// Token: 0x06000468 RID: 1128 RVA: 0x0000D5DC File Offset: 0x0000B7DC
		internal ReportItemRendering()
			: base(false)
		{
		}

		// Token: 0x040000CD RID: 205
		internal RenderingContext m_renderingContext;

		// Token: 0x040000CE RID: 206
		internal ReportItem m_reportItemDef;

		// Token: 0x040000CF RID: 207
		internal ReportItemInstance m_reportItemInstance;

		// Token: 0x040000D0 RID: 208
		internal ReportItemInstanceInfo m_reportItemInstanceInfo;

		// Token: 0x040000D1 RID: 209
		internal MatrixHeadingInstance m_headingInstance;
	}
}
