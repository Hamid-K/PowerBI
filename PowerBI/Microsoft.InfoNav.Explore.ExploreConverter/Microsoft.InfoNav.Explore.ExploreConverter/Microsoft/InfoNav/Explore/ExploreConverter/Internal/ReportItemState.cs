using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200008B RID: 139
	internal sealed class ReportItemState
	{
		// Token: 0x060002BC RID: 700 RVA: 0x0000CAC0 File Offset: 0x0000ACC0
		internal ReportItemState(string name, List<Filter> filters, ReportParsingDiagnosticContext diagnosticContext)
		{
			this._name = name;
			this._filters = filters;
			this._diagnosticContext = diagnosticContext;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000CADD File Offset: 0x0000ACDD
		public List<Filter> Filters
		{
			get
			{
				return this._filters;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000CAE5 File Offset: 0x0000ACE5
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000CAED File Offset: 0x0000ACED
		public ReportParsingDiagnosticContext DiagnosticContext
		{
			get
			{
				return this._diagnosticContext;
			}
		}

		// Token: 0x040001D1 RID: 465
		private readonly string _name;

		// Token: 0x040001D2 RID: 466
		private readonly List<Filter> _filters;

		// Token: 0x040001D3 RID: 467
		private readonly ReportParsingDiagnosticContext _diagnosticContext;
	}
}
