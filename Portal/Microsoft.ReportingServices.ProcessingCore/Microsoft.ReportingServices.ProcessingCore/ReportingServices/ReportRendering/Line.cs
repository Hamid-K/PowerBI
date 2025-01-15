using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200002A RID: 42
	internal sealed class Line : ReportItem
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x0000D7F2 File Offset: 0x0000B9F2
		internal Line(string uniqueName, int intUniqueName, Line reportItemDef, LineInstance reportItemInstance, RenderingContext renderingContext)
			: base(uniqueName, intUniqueName, reportItemDef, reportItemInstance, renderingContext)
		{
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0000D801 File Offset: 0x0000BA01
		public bool Slant
		{
			get
			{
				return ((Line)base.ReportItemDef).LineSlant;
			}
		}
	}
}
