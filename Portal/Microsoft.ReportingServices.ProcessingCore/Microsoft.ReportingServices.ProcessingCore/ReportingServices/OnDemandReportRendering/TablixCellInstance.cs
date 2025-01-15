using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000369 RID: 873
	public sealed class TablixCellInstance : BaseInstance, IReportScopeInstance
	{
		// Token: 0x06002155 RID: 8533 RVA: 0x00080DB2 File Offset: 0x0007EFB2
		internal TablixCellInstance(TablixCell cellDef, Tablix owner, int rowIndex, int colIndex)
			: base(cellDef)
		{
			this.m_cellDef = cellDef;
			this.m_owner = owner;
			this.m_rowIndex = rowIndex;
			this.m_columnIndex = colIndex;
		}

		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x06002156 RID: 8534 RVA: 0x00080DDF File Offset: 0x0007EFDF
		string IReportScopeInstance.UniqueName
		{
			get
			{
				return this.m_cellDef.Cell.UniqueName;
			}
		}

		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x06002157 RID: 8535 RVA: 0x00080DF1 File Offset: 0x0007EFF1
		// (set) Token: 0x06002158 RID: 8536 RVA: 0x00080DF9 File Offset: 0x0007EFF9
		bool IReportScopeInstance.IsNewContext
		{
			get
			{
				return this.m_isNewContext;
			}
			set
			{
				this.m_isNewContext = value;
			}
		}

		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x06002159 RID: 8537 RVA: 0x00080E02 File Offset: 0x0007F002
		IReportScope IReportScopeInstance.ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x00080E0A File Offset: 0x0007F00A
		internal override void SetNewContext()
		{
			if (this.m_isNewContext)
			{
				return;
			}
			this.m_isNewContext = true;
			base.SetNewContext();
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x00080E22 File Offset: 0x0007F022
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x040010BE RID: 4286
		private TablixCell m_cellDef;

		// Token: 0x040010BF RID: 4287
		private Tablix m_owner;

		// Token: 0x040010C0 RID: 4288
		private int m_rowIndex;

		// Token: 0x040010C1 RID: 4289
		private int m_columnIndex;

		// Token: 0x040010C2 RID: 4290
		private bool m_isNewContext = true;
	}
}
