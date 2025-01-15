using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200009A RID: 154
	internal class ReportItem
	{
		// Token: 0x060002F9 RID: 761 RVA: 0x0000CF5E File Offset: 0x0000B15E
		internal ReportItem(string rdlTagName, string name, ReportItemRect rect, int zIndex, ReportParsingDiagnosticContext diagnosticContext)
		{
			this._rdlTagName = rdlTagName;
			this._name = name;
			this._rect = rect;
			this._zIndex = zIndex;
			this._diagnosticContext = diagnosticContext;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000CF8B File Offset: 0x0000B18B
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000CF93 File Offset: 0x0000B193
		public string RdlTagName
		{
			get
			{
				return this._rdlTagName;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000CF9B File Offset: 0x0000B19B
		public ReportItemRect Rect
		{
			get
			{
				return this._rect;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000CFA3 File Offset: 0x0000B1A3
		public ReportParsingDiagnosticContext DiagnosticContext
		{
			get
			{
				return this._diagnosticContext;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000CFAB File Offset: 0x0000B1AB
		public int ZIndex
		{
			get
			{
				return this._zIndex;
			}
		}

		// Token: 0x040001FA RID: 506
		private readonly string _name;

		// Token: 0x040001FB RID: 507
		private readonly ReportItemRect _rect;

		// Token: 0x040001FC RID: 508
		private readonly int _zIndex;

		// Token: 0x040001FD RID: 509
		private readonly string _rdlTagName;

		// Token: 0x040001FE RID: 510
		private readonly ReportParsingDiagnosticContext _diagnosticContext;
	}
}
