using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000407 RID: 1031
	public sealed class PageHeader : PageHeaderFooter
	{
		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x060020D4 RID: 8404 RVA: 0x00005BEF File Offset: 0x00003DEF
		public override SectionType Type
		{
			get
			{
				return SectionType.PageHeader;
			}
		}
	}
}
