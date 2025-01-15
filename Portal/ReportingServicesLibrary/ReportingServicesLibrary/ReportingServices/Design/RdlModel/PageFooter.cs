using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000408 RID: 1032
	public sealed class PageFooter : PageHeaderFooter
	{
		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x00010309 File Offset: 0x0000E509
		public override SectionType Type
		{
			get
			{
				return SectionType.PageFooter;
			}
		}
	}
}
