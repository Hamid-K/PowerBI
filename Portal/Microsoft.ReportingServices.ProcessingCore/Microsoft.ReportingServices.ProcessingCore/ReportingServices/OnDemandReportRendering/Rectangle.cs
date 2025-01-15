using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200033F RID: 831
	public sealed class Rectangle : Microsoft.ReportingServices.OnDemandReportRendering.ReportItem, IPageBreakItem
	{
		// Token: 0x06001F20 RID: 7968 RVA: 0x00077B18 File Offset: 0x00075D18
		internal Rectangle(IReportScope reportScope, IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItemDef, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(reportScope, parentDefinitionPath, indexIntoParentCollectionDef, reportItemDef, renderingContext)
		{
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x00077B27 File Offset: 0x00075D27
		internal Rectangle(IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, bool inSubtotal, Microsoft.ReportingServices.ReportRendering.Rectangle renderRectangle, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(parentDefinitionPath, indexIntoParentCollectionDef, inSubtotal, renderRectangle, renderingContext)
		{
			this.m_renderRectangle = renderRectangle;
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x00077B3E File Offset: 0x00075D3E
		internal Rectangle(IDefinitionPath parentDefinitionPath, bool inSubtotal, ListContent renderContents, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(parentDefinitionPath, inSubtotal, renderingContext)
		{
			this.m_listContents = renderContents;
		}

		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x06001F23 RID: 7971 RVA: 0x00077B51 File Offset: 0x00075D51
		public override string Name
		{
			get
			{
				if (this.m_isListContentsRectangle)
				{
					return this.m_listContents.OwnerDataRegion.Name + "_Contents";
				}
				return base.Name;
			}
		}

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x06001F24 RID: 7972 RVA: 0x00077B7C File Offset: 0x00075D7C
		public override int LinkToChild
		{
			get
			{
				if (!this.m_isOldSnapshot)
				{
					return ((Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle)base.ReportItemDef).LinkToChild;
				}
				if (this.m_isListContentsRectangle)
				{
					return -1;
				}
				return this.m_renderRectangle.LinkToChild;
			}
		}

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x06001F25 RID: 7973 RVA: 0x00077BAC File Offset: 0x00075DAC
		public override DataElementOutputTypes DataElementOutput
		{
			get
			{
				if (this.m_isOldSnapshot && this.m_isListContentsRectangle)
				{
					return DataElementOutputTypes.ContentsOnly;
				}
				return base.DataElementOutput;
			}
		}

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x00077BC6 File Offset: 0x00075DC6
		public override ReportSize Top
		{
			get
			{
				if (this.m_isListContentsRectangle)
				{
					return new ReportSize("0 mm", 0.0);
				}
				return base.Top;
			}
		}

		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x06001F27 RID: 7975 RVA: 0x00077BEA File Offset: 0x00075DEA
		public override ReportSize Left
		{
			get
			{
				if (this.m_isListContentsRectangle)
				{
					return new ReportSize("0 mm", 0.0);
				}
				return base.Left;
			}
		}

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x06001F28 RID: 7976 RVA: 0x00077C0E File Offset: 0x00075E0E
		public bool IsSimple
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_isListContentsRectangle;
				}
				return ((Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle)this.m_reportItemDef).IsSimple;
			}
		}

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x00077C30 File Offset: 0x00075E30
		public Microsoft.ReportingServices.OnDemandReportRendering.ReportItemCollection ReportItemCollection
		{
			get
			{
				if (this.m_reportItems == null)
				{
					if (this.m_isOldSnapshot)
					{
						if (this.m_isListContentsRectangle)
						{
							this.m_reportItems = new Microsoft.ReportingServices.OnDemandReportRendering.ReportItemCollection(this, this.m_inSubtotal, this.m_listContents.ReportItemCollection, this.m_renderingContext);
						}
						else
						{
							this.m_reportItems = new Microsoft.ReportingServices.OnDemandReportRendering.ReportItemCollection(this, this.m_inSubtotal, this.m_renderRectangle.ReportItemCollection, this.m_renderingContext);
						}
					}
					else
					{
						this.m_reportItems = new Microsoft.ReportingServices.OnDemandReportRendering.ReportItemCollection(this.ReportScope, this, ((Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle)this.m_reportItemDef).ReportItems, this.m_renderingContext);
					}
				}
				return this.m_reportItems;
			}
		}

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x06001F2A RID: 7978 RVA: 0x00077CD0 File Offset: 0x00075ED0
		public PageBreak PageBreak
		{
			get
			{
				if (this.m_pageBreak == null)
				{
					if (this.m_isOldSnapshot)
					{
						PageBreakLocation pageBreakLocation;
						if (this.m_isListContentsRectangle)
						{
							pageBreakLocation = PageBreakHelper.GetPageBreakLocation(false, false);
						}
						else
						{
							pageBreakLocation = PageBreakHelper.GetPageBreakLocation(this.m_renderRectangle.PageBreakAtStart, this.m_renderRectangle.PageBreakAtEnd);
						}
						this.m_pageBreak = new PageBreak(base.RenderingContext, this.ReportScope, pageBreakLocation);
					}
					else
					{
						IPageBreakOwner pageBreakOwner = (Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle)this.m_reportItemDef;
						this.m_pageBreak = new PageBreak(base.RenderingContext, this.ReportScope, pageBreakOwner);
					}
				}
				return this.m_pageBreak;
			}
		}

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x06001F2B RID: 7979 RVA: 0x00077D5F File Offset: 0x00075F5F
		public ReportStringProperty PageName
		{
			get
			{
				if (this.m_pageName == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_pageName = new ReportStringProperty();
					}
					else
					{
						this.m_pageName = new ReportStringProperty(((Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle)this.m_reportItemDef).PageName);
					}
				}
				return this.m_pageName;
			}
		}

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x06001F2C RID: 7980 RVA: 0x00077D9F File Offset: 0x00075F9F
		public bool KeepTogether
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_isListContentsRectangle;
				}
				return ((Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle)base.ReportItemDef).KeepTogether;
			}
		}

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x00077DC0 File Offset: 0x00075FC0
		public bool OmitBorderOnPageBreak
		{
			get
			{
				return !this.m_isOldSnapshot && ((Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle)base.ReportItemDef).OmitBorderOnPageBreak;
			}
		}

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x06001F2E RID: 7982 RVA: 0x00077DDC File Offset: 0x00075FDC
		[Obsolete("Use PageBreak.BreakLocation instead.")]
		PageBreakLocation IPageBreakItem.PageBreakLocation
		{
			get
			{
				if (!this.m_isOldSnapshot)
				{
					if (((IPageBreakOwner)base.ReportItemDef).PageBreak != null)
					{
						PageBreak pageBreak = this.PageBreak;
						if (pageBreak.HasEnabledInstance)
						{
							return pageBreak.BreakLocation;
						}
					}
					return PageBreakLocation.None;
				}
				if (this.m_isListContentsRectangle)
				{
					return PageBreakHelper.GetPageBreakLocation(false, false);
				}
				return PageBreakHelper.GetPageBreakLocation(this.m_renderRectangle.PageBreakAtStart, this.m_renderRectangle.PageBreakAtEnd);
			}
		}

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x00077E46 File Offset: 0x00076046
		internal override Microsoft.ReportingServices.ReportRendering.ReportItem RenderReportItem
		{
			get
			{
				if (this.m_isListContentsRectangle)
				{
					return this.m_listContents.OwnerDataRegion;
				}
				return this.m_renderReportItem;
			}
		}

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x06001F30 RID: 7984 RVA: 0x00077E62 File Offset: 0x00076062
		public override Microsoft.ReportingServices.OnDemandReportRendering.Visibility Visibility
		{
			get
			{
				if (this.m_isListContentsRectangle)
				{
					return null;
				}
				return base.Visibility;
			}
		}

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x00077E74 File Offset: 0x00076074
		internal bool IsListContentsRectangle
		{
			get
			{
				return this.m_isListContentsRectangle;
			}
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x00077E7C File Offset: 0x0007607C
		internal override ReportItemInstance GetOrCreateInstance()
		{
			if (this.m_instance == null)
			{
				this.m_instance = new RectangleInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x00077E98 File Offset: 0x00076098
		internal override void SetNewContextChildren()
		{
			if (this.m_reportItems != null)
			{
				this.m_reportItems.SetNewContext();
			}
			if (this.m_pageBreak != null)
			{
				this.m_pageBreak.SetNewContext();
			}
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x00077EC0 File Offset: 0x000760C0
		internal void UpdateListContents(ListContent listContents)
		{
			if (!this.m_isOldSnapshot)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			this.SetNewContext();
			if (listContents != null)
			{
				this.m_listContents = listContents;
			}
			if (this.m_reportItems != null)
			{
				this.m_reportItems.UpdateRenderReportItem(listContents.ReportItemCollection);
			}
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x00077EFE File Offset: 0x000760FE
		internal override void UpdateRenderReportItem(Microsoft.ReportingServices.ReportRendering.ReportItem renderReportItem)
		{
			base.UpdateRenderReportItem(renderReportItem);
			this.m_renderRectangle = (Microsoft.ReportingServices.ReportRendering.Rectangle)renderReportItem;
			if (this.m_reportItems != null && renderReportItem is Microsoft.ReportingServices.ReportRendering.Rectangle)
			{
				this.m_reportItems.UpdateRenderReportItem(this.m_renderRectangle.ReportItemCollection);
			}
		}

		// Token: 0x04000FC6 RID: 4038
		private Microsoft.ReportingServices.ReportRendering.Rectangle m_renderRectangle;

		// Token: 0x04000FC7 RID: 4039
		private ListContent m_listContents;

		// Token: 0x04000FC8 RID: 4040
		private Microsoft.ReportingServices.OnDemandReportRendering.ReportItemCollection m_reportItems;

		// Token: 0x04000FC9 RID: 4041
		private PageBreak m_pageBreak;

		// Token: 0x04000FCA RID: 4042
		private ReportStringProperty m_pageName;
	}
}
