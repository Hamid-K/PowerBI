using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000099 RID: 153
	internal sealed class Rectangle : ReportItem
	{
		// Token: 0x060002F7 RID: 759 RVA: 0x0000CF3C File Offset: 0x0000B13C
		internal Rectangle(string name, ReportItemRect rect, int zIndex, ReportParsingDiagnosticContext diagnosticContext, List<ReportItem> reportItems)
			: base("Rectangle", name, rect, zIndex, diagnosticContext)
		{
			this._reportItems = reportItems;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000CF56 File Offset: 0x0000B156
		public List<ReportItem> ReportItems
		{
			get
			{
				return this._reportItems;
			}
		}

		// Token: 0x040001F9 RID: 505
		private List<ReportItem> _reportItems;
	}
}
