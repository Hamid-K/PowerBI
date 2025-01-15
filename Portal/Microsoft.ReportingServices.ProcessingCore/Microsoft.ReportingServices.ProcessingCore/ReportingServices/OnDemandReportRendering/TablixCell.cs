using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000359 RID: 857
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class TablixCell : IDataRegionCell, IDefinitionPath, IReportScope
	{
		// Token: 0x060020C3 RID: 8387 RVA: 0x0007F543 File Offset: 0x0007D743
		internal TablixCell(Cell cell, Tablix owner, int rowIndex, int colIndex)
		{
			this.m_cell = cell;
			this.m_owner = owner;
			this.m_rowIndex = rowIndex;
			this.m_columnIndex = colIndex;
		}

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x060020C4 RID: 8388
		public abstract string ID { get; }

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x060020C5 RID: 8389 RVA: 0x0007F568 File Offset: 0x0007D768
		public string DefinitionPath
		{
			get
			{
				if (this.m_definitionPath == null)
				{
					this.m_definitionPath = DefinitionPathConstants.GetTablixCellDefinitionPath(this.m_owner, this.m_rowIndex, this.m_columnIndex, true);
				}
				return this.m_definitionPath;
			}
		}

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x060020C6 RID: 8390 RVA: 0x0007F596 File Offset: 0x0007D796
		public IDefinitionPath ParentDefinitionPath
		{
			get
			{
				return this.m_owner;
			}
		}

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x060020C7 RID: 8391
		public abstract CellContents CellContents { get; }

		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x060020C8 RID: 8392
		public abstract DataElementOutputTypes DataElementOutput { get; }

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x060020C9 RID: 8393
		public abstract StructureTypeOverwriteType StructureTypeOverwrite { get; }

		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x060020CA RID: 8394
		public abstract string DataElementName { get; }

		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x0007F59E File Offset: 0x0007D79E
		IReportScopeInstance IReportScope.ReportScopeInstance
		{
			get
			{
				return this.Instance;
			}
		}

		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x060020CC RID: 8396 RVA: 0x0007F5A6 File Offset: 0x0007D7A6
		IRIFReportScope IReportScope.RIFReportScope
		{
			get
			{
				return this.m_cell;
			}
		}

		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x060020CD RID: 8397 RVA: 0x0007F5AE File Offset: 0x0007D7AE
		internal Cell Cell
		{
			get
			{
				return this.m_cell;
			}
		}

		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x060020CE RID: 8398 RVA: 0x0007F5B8 File Offset: 0x0007D7B8
		public virtual TablixCellInstance Instance
		{
			get
			{
				if (this.m_owner.m_renderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new TablixCellInstance(this, this.m_owner, this.m_rowIndex, this.m_columnIndex);
				}
				return this.m_instance;
			}
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x0007F605 File Offset: 0x0007D805
		void IDataRegionCell.SetNewContext()
		{
			this.SetNewContext();
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x0007F60D File Offset: 0x0007D80D
		internal virtual void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_cellContents != null)
			{
				this.m_cellContents.SetNewContext();
			}
			if (this.m_cell != null)
			{
				this.m_cell.ClearStreamingScopeInstanceBinding();
			}
		}

		// Token: 0x04001076 RID: 4214
		private Cell m_cell;

		// Token: 0x04001077 RID: 4215
		protected Tablix m_owner;

		// Token: 0x04001078 RID: 4216
		protected int m_rowIndex;

		// Token: 0x04001079 RID: 4217
		protected int m_columnIndex;

		// Token: 0x0400107A RID: 4218
		protected CellContents m_cellContents;

		// Token: 0x0400107B RID: 4219
		protected TablixCellInstance m_instance;

		// Token: 0x0400107C RID: 4220
		protected string m_definitionPath;
	}
}
