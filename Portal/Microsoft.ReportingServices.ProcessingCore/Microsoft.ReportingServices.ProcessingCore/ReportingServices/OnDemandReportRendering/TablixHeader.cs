using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000372 RID: 882
	public sealed class TablixHeader : IDefinitionPath
	{
		// Token: 0x060021A0 RID: 8608 RVA: 0x00081E5E File Offset: 0x0008005E
		internal TablixHeader(Tablix owner, TablixMember tablixMember)
		{
			this.m_owner = owner;
			this.m_tablixMember = tablixMember;
		}

		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x060021A1 RID: 8609 RVA: 0x00081E74 File Offset: 0x00080074
		public string DefinitionPath
		{
			get
			{
				if (this.m_definitionPath == null)
				{
					this.m_definitionPath = this.ParentDefinitionPath.DefinitionPath + "xH";
				}
				return this.m_definitionPath;
			}
		}

		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x060021A2 RID: 8610 RVA: 0x00081E9F File Offset: 0x0008009F
		public IDefinitionPath ParentDefinitionPath
		{
			get
			{
				return this.m_tablixMember;
			}
		}

		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x060021A3 RID: 8611 RVA: 0x00081EA8 File Offset: 0x000800A8
		public ReportSize Size
		{
			get
			{
				if (!this.m_owner.IsOldSnapshot)
				{
					TablixHeader tablixHeader = this.m_tablixMember.MemberDefinition.TablixHeader;
					if (tablixHeader.SizeForRendering == null)
					{
						tablixHeader.SizeForRendering = new ReportSize(tablixHeader.Size, tablixHeader.SizeValue);
					}
					return tablixHeader.SizeForRendering;
				}
				if (this.m_owner.SnapshotTablixType != Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix)
				{
					return null;
				}
				ShimMatrixMember shimMatrixMember = this.m_tablixMember as ShimMatrixMember;
				if (shimMatrixMember.IsColumn)
				{
					return new ReportSize(shimMatrixMember.CurrentRenderMatrixMember.Height);
				}
				return new ReportSize(shimMatrixMember.CurrentRenderMatrixMember.Width);
			}
		}

		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x060021A4 RID: 8612 RVA: 0x00081F40 File Offset: 0x00080140
		public CellContents CellContents
		{
			get
			{
				if (this.m_cellContents == null)
				{
					if (this.m_owner.IsOldSnapshot)
					{
						if (this.m_owner.SnapshotTablixType == Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix)
						{
							ShimMatrixMember shimMatrixMember = this.m_tablixMember as ShimMatrixMember;
							Microsoft.ReportingServices.ReportRendering.ReportItem reportItem = (shimMatrixMember.IsStatic ? shimMatrixMember.m_staticOrSubtotal.ReportItem : ((MatrixMember)shimMatrixMember.Group.CurrentShimRenderGroup).ReportItem);
							this.m_cellContents = new CellContents(this, this.m_owner.InSubtotal, reportItem, shimMatrixMember.RowSpan, shimMatrixMember.ColSpan, this.m_owner.RenderingContext, shimMatrixMember.SizeDelta, shimMatrixMember.IsColumn);
						}
					}
					else
					{
						this.m_cellContents = new CellContents(this.m_tablixMember.ReportScope, this, this.m_tablixMember.MemberDefinition.TablixHeader.CellContents, this.m_tablixMember.RowSpan, this.m_tablixMember.ColSpan, this.m_owner.RenderingContext);
					}
				}
				else if (this.m_owner.IsOldSnapshot)
				{
					this.OnDemandUpdateCellContents();
				}
				return this.m_cellContents;
			}
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x00082055 File Offset: 0x00080255
		internal void SetNewContext()
		{
			if (this.m_cellContents != null)
			{
				this.m_cellContents.SetNewContext();
			}
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x0008206A File Offset: 0x0008026A
		internal void ResetCellContents()
		{
			this.m_cacheRenderReportItem = null;
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x00082073 File Offset: 0x00080273
		private void OnDemandUpdateCellContents()
		{
			if (this.m_cacheRenderReportItem == null && this.m_cellContents != null)
			{
				this.m_cacheRenderReportItem = ((ShimMatrixMember)this.m_tablixMember).CurrentRenderMatrixMember.ReportItem;
				this.m_cellContents.UpdateRenderReportItem(this.m_cacheRenderReportItem);
			}
		}

		// Token: 0x040010D3 RID: 4307
		private Tablix m_owner;

		// Token: 0x040010D4 RID: 4308
		private TablixMember m_tablixMember;

		// Token: 0x040010D5 RID: 4309
		private string m_definitionPath;

		// Token: 0x040010D6 RID: 4310
		private CellContents m_cellContents;

		// Token: 0x040010D7 RID: 4311
		private Microsoft.ReportingServices.ReportRendering.ReportItem m_cacheRenderReportItem;
	}
}
