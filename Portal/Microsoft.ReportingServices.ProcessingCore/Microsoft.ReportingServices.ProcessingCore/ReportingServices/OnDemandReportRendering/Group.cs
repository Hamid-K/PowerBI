using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200029A RID: 666
	public sealed class Group : IPageBreakItem
	{
		// Token: 0x060019A6 RID: 6566 RVA: 0x00067D07 File Offset: 0x00065F07
		internal Group(Microsoft.ReportingServices.OnDemandReportRendering.DataRegion owner, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode memberDef, DataRegionMember dataMember)
		{
			this.m_isOldSnapshot = false;
			this.m_ownerItem = owner;
			this.m_memberDef = memberDef;
			this.m_dataMember = dataMember;
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x00067D32 File Offset: 0x00065F32
		internal Group(Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem owner, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode memberDef, DataRegionMember dataMember)
		{
			this.m_isOldSnapshot = false;
			this.m_criOwner = owner;
			this.m_memberDef = memberDef;
			this.m_dataMember = dataMember;
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x00067D5D File Offset: 0x00065F5D
		internal Group(Microsoft.ReportingServices.OnDemandReportRendering.DataRegion owner, ShimRenderGroups renderGroups, ShimTablixMember dynamicMember)
		{
			this.m_isOldSnapshot = true;
			this.m_ownerItem = owner;
			this.m_renderGroups = renderGroups;
			this.m_dynamicMember = dynamicMember;
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x00067D88 File Offset: 0x00065F88
		internal Group(Microsoft.ReportingServices.OnDemandReportRendering.DataRegion owner, ShimRenderGroups renderGroups)
		{
			this.m_isOldSnapshot = true;
			this.m_ownerItem = owner;
			this.m_renderGroups = renderGroups;
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x00067DAC File Offset: 0x00065FAC
		internal Group(Microsoft.ReportingServices.OnDemandReportRendering.DataRegion owner, ShimTableMember tableDetailMember)
		{
			this.m_isOldSnapshot = true;
			this.m_isDetailGroup = true;
			this.m_tableDetailMember = tableDetailMember;
			this.m_dynamicMember = tableDetailMember;
			this.m_ownerItem = owner;
			this.m_renderGroups = null;
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x00067DE5 File Offset: 0x00065FE5
		internal Group(Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem owner, ShimRenderGroups renderGroups)
		{
			this.m_isOldSnapshot = true;
			this.m_renderGroups = renderGroups;
			this.m_criOwner = owner;
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x00067E09 File Offset: 0x00066009
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_pageBreak != null)
			{
				this.m_pageBreak.SetNewContext();
			}
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x00067E34 File Offset: 0x00066034
		public string ID
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					if (this.m_currentRenderGroupIndex < 0 || this.m_renderGroups == null)
					{
						return null;
					}
					return this.m_renderGroups[this.m_currentRenderGroupIndex].ID;
				}
				else
				{
					if (this.m_memberDef != null)
					{
						return this.m_memberDef.RenderingModelID;
					}
					return null;
				}
			}
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x060019AE RID: 6574 RVA: 0x00067E88 File Offset: 0x00066088
		public string Name
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					if (this.m_renderGroups == null)
					{
						return null;
					}
					return this.CurrentShimRenderGroup.Name;
				}
				else
				{
					if (this.m_memberDef != null && this.m_memberDef.Grouping != null)
					{
						return this.m_memberDef.Grouping.Name;
					}
					return null;
				}
			}
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x00067EDC File Offset: 0x000660DC
		public ReportStringProperty DocumentMapLabel
		{
			get
			{
				if (this.m_documentMapLabel == null)
				{
					if (this.m_isOldSnapshot)
					{
						if (this.m_renderGroups == null)
						{
							return null;
						}
						if (this.CurrentShimRenderGroup.m_groupingDef != null)
						{
							Microsoft.ReportingServices.ReportProcessing.ExpressionInfo groupLabel = this.CurrentShimRenderGroup.m_groupingDef.GroupLabel;
							this.m_documentMapLabel = new ReportStringProperty(groupLabel);
						}
					}
					else
					{
						if (this.m_memberDef == null || this.m_memberDef.Grouping == null)
						{
							return null;
						}
						Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo groupLabel2 = this.m_memberDef.Grouping.GroupLabel;
						if (groupLabel2 != null)
						{
							this.m_documentMapLabel = new ReportStringProperty(groupLabel2.IsExpression, groupLabel2.OriginalText, groupLabel2.StringValue);
						}
						else
						{
							this.m_documentMapLabel = new ReportStringProperty();
						}
					}
				}
				return this.m_documentMapLabel;
			}
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x00067F90 File Offset: 0x00066190
		public PageBreak PageBreak
		{
			get
			{
				if (this.m_pageBreak == null)
				{
					Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext = ((this.m_criOwner != null) ? this.m_criOwner.RenderingContext : this.m_ownerItem.RenderingContext);
					if (this.IsOldSnapshot)
					{
						if (this.m_dynamicMember != null)
						{
							this.m_pageBreak = new PageBreak(renderingContext, this.m_dataMember, this.m_dynamicMember.PropagatedGroupBreak);
						}
						else
						{
							this.m_pageBreak = new PageBreak(renderingContext, null, PageBreakLocation.None);
						}
					}
					else if (this.m_memberDef != null && this.m_memberDef.Grouping != null)
					{
						this.m_pageBreak = new PageBreak(renderingContext, this.m_dataMember, this.m_memberDef.Grouping);
					}
					else
					{
						this.m_pageBreak = new PageBreak(renderingContext, this.m_dataMember, PageBreakLocation.None);
					}
				}
				return this.m_pageBreak;
			}
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x00068058 File Offset: 0x00066258
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
					else if (this.m_memberDef != null && this.m_memberDef.Grouping != null)
					{
						this.m_pageName = new ReportStringProperty(this.m_memberDef.Grouping.PageName);
					}
					else
					{
						this.m_pageName = new ReportStringProperty();
					}
				}
				return this.m_pageName;
			}
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x000680C8 File Offset: 0x000662C8
		public GroupExpressionCollection GroupExpressions
		{
			get
			{
				if (this.m_groupExpressions == null)
				{
					if (this.m_isOldSnapshot)
					{
						if (this.CurrentShimRenderGroup != null && this.CurrentShimRenderGroup.m_groupingDef != null)
						{
							this.m_groupExpressions = new GroupExpressionCollection(this.CurrentShimRenderGroup.m_groupingDef);
						}
					}
					else
					{
						this.m_groupExpressions = new GroupExpressionCollection(this.m_memberDef.Grouping);
					}
				}
				return this.m_groupExpressions;
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x00068130 File Offset: 0x00066330
		internal CustomPropertyCollection CustomProperties
		{
			get
			{
				if (this.m_customProperties == null && this.m_isOldSnapshot && this.CurrentShimRenderGroup != null && this.CurrentShimRenderGroup.CustomProperties != null)
				{
					Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext = ((this.m_criOwner != null) ? this.m_criOwner.RenderingContext : this.m_ownerItem.RenderingContext);
					this.m_customProperties = new CustomPropertyCollection(renderingContext, this.CurrentShimRenderGroup.CustomProperties);
					if (this.m_currentRenderGroupIndex < 0)
					{
						this.m_customProperties.UpdateCustomProperties(null);
					}
				}
				return this.m_customProperties;
			}
		}

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x000681B8 File Offset: 0x000663B8
		public string DataElementName
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					if (this.m_renderGroups != null)
					{
						if (this.m_criOwner == null)
						{
							return this.CurrentShimRenderGroup.DataElementName;
						}
						return null;
					}
					else
					{
						if (Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Table == this.m_ownerItem.DataRegionType)
						{
							return ((Microsoft.ReportingServices.OnDemandReportRendering.Tablix)this.m_ownerItem).RenderTable.DetailDataElementName;
						}
						return null;
					}
				}
				else
				{
					if (this.m_memberDef == null || this.m_memberDef.Grouping == null)
					{
						return null;
					}
					return this.m_memberDef.Grouping.DataElementName;
				}
			}
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x00068238 File Offset: 0x00066438
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					if (this.m_renderGroups != null)
					{
						if (this.m_criOwner == null)
						{
							return (DataElementOutputTypes)this.CurrentShimRenderGroup.DataElementOutput;
						}
						return DataElementOutputTypes.Output;
					}
					else
					{
						if (Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Table == this.m_ownerItem.DataRegionType)
						{
							return (DataElementOutputTypes)((Microsoft.ReportingServices.OnDemandReportRendering.Tablix)this.m_ownerItem).RenderTable.DetailDataElementOutput;
						}
						return DataElementOutputTypes.Output;
					}
				}
				else
				{
					if (this.m_memberDef == null || this.m_memberDef.Grouping == null)
					{
						return DataElementOutputTypes.Output;
					}
					return this.m_memberDef.Grouping.DataElementOutput;
				}
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x000682B8 File Offset: 0x000664B8
		public bool IsRecursive
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					if (this.m_renderGroups != null)
					{
						Microsoft.ReportingServices.ReportProcessing.Grouping groupingDef = this.CurrentShimRenderGroup.m_groupingDef;
						if (groupingDef != null)
						{
							return groupingDef.Parent != null && groupingDef.Parent.Count > 0;
						}
					}
				}
				else if (this.m_memberDef != null && this.m_memberDef.Grouping != null)
				{
					return this.m_memberDef.Grouping.Parent != null && this.m_memberDef.Grouping.Parent.Count > 0;
				}
				return false;
			}
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x060019B7 RID: 6583 RVA: 0x00068340 File Offset: 0x00066540
		internal Microsoft.ReportingServices.OnDemandReportRendering.DataRegion OwnerDataRegion
		{
			get
			{
				return this.m_ownerItem;
			}
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x00068348 File Offset: 0x00066548
		[Obsolete("Use PageBreak.BreakLocation instead.")]
		PageBreakLocation IPageBreakItem.PageBreakLocation
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					if (this.m_dynamicMember != null)
					{
						return this.m_dynamicMember.PropagatedGroupBreak;
					}
				}
				else if (this.m_memberDef != null && this.m_memberDef.Grouping != null && this.m_memberDef.Grouping.PageBreak != null)
				{
					return this.m_memberDef.Grouping.PageBreak.BreakLocation;
				}
				return PageBreakLocation.None;
			}
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x060019B9 RID: 6585 RVA: 0x000683AF File Offset: 0x000665AF
		// (set) Token: 0x060019BA RID: 6586 RVA: 0x000683B7 File Offset: 0x000665B7
		internal ShimRenderGroups RenderGroups
		{
			get
			{
				return this.m_renderGroups;
			}
			set
			{
				this.m_renderGroups = value;
			}
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x060019BB RID: 6587 RVA: 0x000683C0 File Offset: 0x000665C0
		// (set) Token: 0x060019BC RID: 6588 RVA: 0x000683C8 File Offset: 0x000665C8
		internal int CurrentRenderGroupIndex
		{
			get
			{
				return this.m_currentRenderGroupIndex;
			}
			set
			{
				if (value != 0 || this.m_currentRenderGroupIndex != -1)
				{
					this.m_currentRenderGroupCache = null;
				}
				this.m_currentRenderGroupIndex = value;
				if (this.m_instance != null)
				{
					this.m_instance.SetNewContext();
				}
				if (this.m_isOldSnapshot && this.m_renderGroups != null && this.m_customProperties != null)
				{
					if (this.m_currentRenderGroupIndex < 0)
					{
						this.m_customProperties.UpdateCustomProperties(null);
						return;
					}
					this.m_customProperties.UpdateCustomProperties(this.m_renderGroups[this.m_currentRenderGroupIndex].CustomProperties);
				}
			}
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x060019BD RID: 6589 RVA: 0x00068454 File Offset: 0x00066654
		internal Group CurrentShimRenderGroup
		{
			get
			{
				if (this.m_isOldSnapshot && this.m_renderGroups != null)
				{
					if (this.m_currentRenderGroupCache == null)
					{
						if (this.m_currentRenderGroupIndex < 0)
						{
							this.m_currentRenderGroupCache = this.m_renderGroups[0];
						}
						else
						{
							this.m_currentRenderGroupCache = this.m_renderGroups[this.m_currentRenderGroupIndex];
						}
					}
					return this.m_currentRenderGroupCache;
				}
				return null;
			}
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x000684B5 File Offset: 0x000666B5
		internal bool IsOldSnapshot
		{
			get
			{
				return this.m_isOldSnapshot;
			}
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x000684BD File Offset: 0x000666BD
		internal bool IsDetailGroup
		{
			get
			{
				return this.m_isDetailGroup;
			}
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x060019C0 RID: 6592 RVA: 0x000684C5 File Offset: 0x000666C5
		internal ShimTableMember TableDetailMember
		{
			get
			{
				return this.m_tableDetailMember;
			}
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x000684CD File Offset: 0x000666CD
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode MemberDefinition
		{
			get
			{
				return this.m_memberDef;
			}
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x060019C2 RID: 6594 RVA: 0x000684D8 File Offset: 0x000666D8
		public GroupInstance Instance
		{
			get
			{
				if ((this.m_ownerItem != null && this.m_ownerItem.RenderingContext.InstanceAccessDisallowed) || (this.m_criOwner != null && this.m_criOwner.RenderingContext.InstanceAccessDisallowed))
				{
					return null;
				}
				if (this.m_instance == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_instance = new GroupInstance(this);
					}
					else
					{
						this.m_instance = new GroupInstance(this, this.m_dataMember);
					}
				}
				return this.m_instance;
			}
		}

		// Token: 0x04000CC8 RID: 3272
		private bool m_isOldSnapshot;

		// Token: 0x04000CC9 RID: 3273
		private bool m_isDetailGroup;

		// Token: 0x04000CCA RID: 3274
		private Microsoft.ReportingServices.OnDemandReportRendering.DataRegion m_ownerItem;

		// Token: 0x04000CCB RID: 3275
		private Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode m_memberDef;

		// Token: 0x04000CCC RID: 3276
		private PageBreak m_pageBreak;

		// Token: 0x04000CCD RID: 3277
		private ReportStringProperty m_pageName;

		// Token: 0x04000CCE RID: 3278
		private GroupExpressionCollection m_groupExpressions;

		// Token: 0x04000CCF RID: 3279
		private CustomPropertyCollection m_customProperties;

		// Token: 0x04000CD0 RID: 3280
		private ReportStringProperty m_documentMapLabel;

		// Token: 0x04000CD1 RID: 3281
		private GroupInstance m_instance;

		// Token: 0x04000CD2 RID: 3282
		private DataRegionMember m_dataMember;

		// Token: 0x04000CD3 RID: 3283
		private ShimTablixMember m_dynamicMember;

		// Token: 0x04000CD4 RID: 3284
		private ShimTableMember m_tableDetailMember;

		// Token: 0x04000CD5 RID: 3285
		private Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem m_criOwner;

		// Token: 0x04000CD6 RID: 3286
		private ShimRenderGroups m_renderGroups;

		// Token: 0x04000CD7 RID: 3287
		private Group m_currentRenderGroupCache;

		// Token: 0x04000CD8 RID: 3288
		private int m_currentRenderGroupIndex = -1;
	}
}
