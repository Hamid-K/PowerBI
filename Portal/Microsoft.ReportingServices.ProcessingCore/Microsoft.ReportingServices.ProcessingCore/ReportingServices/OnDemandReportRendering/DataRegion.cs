using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000290 RID: 656
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class DataRegion : Microsoft.ReportingServices.OnDemandReportRendering.ReportItem, IPageBreakItem, IReportScope, IDataRegion
	{
		// Token: 0x0600196B RID: 6507 RVA: 0x000677E0 File Offset: 0x000659E0
		internal DataRegion(IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItemDef, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(null, parentDefinitionPath, indexIntoParentCollectionDef, reportItemDef, renderingContext)
		{
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x000677EE File Offset: 0x000659EE
		internal DataRegion(IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, bool inSubtotal, Microsoft.ReportingServices.ReportRendering.ReportItem renderDataRegion, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(parentDefinitionPath, indexIntoParentCollectionDef, inSubtotal, renderDataRegion, renderingContext)
		{
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x0600196D RID: 6509 RVA: 0x00067800 File Offset: 0x00065A00
		public PageBreak PageBreak
		{
			get
			{
				if (this.m_pageBreak == null)
				{
					if (this.m_isOldSnapshot)
					{
						Microsoft.ReportingServices.ReportRendering.DataRegion dataRegion = (Microsoft.ReportingServices.ReportRendering.DataRegion)this.m_renderReportItem;
						this.m_pageBreak = new PageBreak(this.m_renderingContext, this.ReportScope, PageBreakHelper.GetPageBreakLocation(dataRegion.PageBreakAtStart, dataRegion.PageBreakAtEnd));
					}
					else
					{
						IPageBreakOwner pageBreakOwner = (Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)this.m_reportItemDef;
						this.m_pageBreak = new PageBreak(this.m_renderingContext, this.ReportScope, pageBreakOwner);
					}
				}
				return this.m_pageBreak;
			}
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x0006787D File Offset: 0x00065A7D
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
						this.m_pageName = new ReportStringProperty(((Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)this.m_reportItemDef).PageName);
					}
				}
				return this.m_pageName;
			}
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x000678C0 File Offset: 0x00065AC0
		public ReportStringProperty NoRowsMessage
		{
			get
			{
				if (this.m_noRowsMessage == null)
				{
					if (this.m_isOldSnapshot)
					{
						Microsoft.ReportingServices.ReportProcessing.ExpressionInfo noRows = ((Microsoft.ReportingServices.ReportProcessing.DataRegion)this.m_renderReportItem.ReportItemDef).NoRows;
						this.m_noRowsMessage = new ReportStringProperty(noRows);
					}
					else
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo noRowsMessage = ((Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)this.m_reportItemDef).NoRowsMessage;
						this.m_noRowsMessage = new ReportStringProperty(noRowsMessage);
					}
				}
				return this.m_noRowsMessage;
			}
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x00067924 File Offset: 0x00065B24
		public string DataSetName
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return ((Microsoft.ReportingServices.ReportRendering.DataRegion)this.m_renderReportItem).DataSetName;
				}
				return ((Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)base.ReportItemDef).DataSetName;
			}
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x0006794F File Offset: 0x00065B4F
		public override Microsoft.ReportingServices.OnDemandReportRendering.Visibility Visibility
		{
			get
			{
				if (this.m_isOldSnapshot && this.m_snapshotDataRegionType == Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.List)
				{
					return null;
				}
				return base.Visibility;
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x0006796C File Offset: 0x00065B6C
		[Obsolete("Use PageBreak.BreakLocation instead.")]
		PageBreakLocation IPageBreakItem.PageBreakLocation
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					Microsoft.ReportingServices.ReportRendering.DataRegion dataRegion = (Microsoft.ReportingServices.ReportRendering.DataRegion)this.m_renderReportItem;
					return PageBreakHelper.GetPageBreakLocation(dataRegion.PageBreakAtStart, dataRegion.PageBreakAtEnd);
				}
				PageBreak pageBreak = this.PageBreak;
				if (pageBreak.HasEnabledInstance)
				{
					return pageBreak.BreakLocation;
				}
				return PageBreakLocation.None;
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x000679B6 File Offset: 0x00065BB6
		internal Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type DataRegionType
		{
			get
			{
				return this.m_snapshotDataRegionType;
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x000679BE File Offset: 0x00065BBE
		IReportScopeInstance IReportScope.ReportScopeInstance
		{
			get
			{
				return (IReportScopeInstance)base.Instance;
			}
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x000679CB File Offset: 0x00065BCB
		IRIFReportScope IReportScope.RIFReportScope
		{
			get
			{
				return (IRIFReportScope)this.m_reportItemDef;
			}
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x000679D8 File Offset: 0x00065BD8
		internal override IReportScope ReportScope
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06001977 RID: 6519 RVA: 0x000679DB File Offset: 0x00065BDB
		bool IDataRegion.HasDataCells
		{
			get
			{
				return this.HasDataCells;
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06001978 RID: 6520
		internal abstract bool HasDataCells { get; }

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06001979 RID: 6521 RVA: 0x000679E3 File Offset: 0x00065BE3
		IDataRegionRowCollection IDataRegion.RowCollection
		{
			get
			{
				return this.RowCollection;
			}
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x0600197A RID: 6522
		internal abstract IDataRegionRowCollection RowCollection { get; }

		// Token: 0x0600197B RID: 6523 RVA: 0x000679EC File Offset: 0x00065BEC
		public int[] GetRepeatSiblings()
		{
			if (this.m_isOldSnapshot)
			{
				return ((Microsoft.ReportingServices.ReportRendering.DataRegion)this.m_renderReportItem).GetRepeatSiblings();
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion = (Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)base.ReportItemDef;
			if (dataRegion.RepeatSiblings == null)
			{
				return new int[0];
			}
			int[] array = new int[dataRegion.RepeatSiblings.Count];
			dataRegion.RepeatSiblings.CopyTo(array);
			return array;
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00067A4B File Offset: 0x00065C4B
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_pageBreak != null)
			{
				this.m_pageBreak.SetNewContext();
			}
			if (!this.m_isOldSnapshot)
			{
				((Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)base.ReportItemDef).ClearStreamingScopeInstanceBinding();
			}
		}

		// Token: 0x04000CB6 RID: 3254
		private ReportStringProperty m_noRowsMessage;

		// Token: 0x04000CB7 RID: 3255
		private PageBreak m_pageBreak;

		// Token: 0x04000CB8 RID: 3256
		private ReportStringProperty m_pageName;

		// Token: 0x04000CB9 RID: 3257
		internal Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type m_snapshotDataRegionType;

		// Token: 0x02000941 RID: 2369
		internal enum Type
		{
			// Token: 0x04004028 RID: 16424
			None,
			// Token: 0x04004029 RID: 16425
			List,
			// Token: 0x0400402A RID: 16426
			Table,
			// Token: 0x0400402B RID: 16427
			Matrix,
			// Token: 0x0400402C RID: 16428
			Chart,
			// Token: 0x0400402D RID: 16429
			GaugePanel,
			// Token: 0x0400402E RID: 16430
			CustomReportItem,
			// Token: 0x0400402F RID: 16431
			MapDataRegion
		}
	}
}
