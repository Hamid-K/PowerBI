using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200028E RID: 654
	internal sealed class ShimDataMember : Microsoft.ReportingServices.OnDemandReportRendering.DataMember, IShimDataRegionMember
	{
		// Token: 0x06001944 RID: 6468 RVA: 0x000670E0 File Offset: 0x000652E0
		internal ShimDataMember(IDefinitionPath parentDefinitionPath, Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem owner, ShimDataMember parent, int parentCollectionIndex, bool isColumn, bool isStatic, DataMemberCollection renderMembers, int staticIndex)
			: base(parentDefinitionPath, owner, parent, parentCollectionIndex)
		{
			this.m_definitionStartIndex = owner.GetCurrentMemberCellDefinitionIndex();
			this.m_isColumn = isColumn;
			this.m_isStatic = isStatic;
			this.m_renderMembers = renderMembers;
			this.m_staticIndex = staticIndex;
			DataGroupingCollection dataGroupingCollection;
			if (isStatic)
			{
				dataGroupingCollection = renderMembers[staticIndex].Children;
			}
			else
			{
				this.m_group = new Group(owner, new ShimRenderGroups(renderMembers));
				dataGroupingCollection = renderMembers[0].Children;
			}
			if (dataGroupingCollection != null)
			{
				this.m_children = new ShimDataMemberCollection(this, owner, isColumn, this, dataGroupingCollection);
			}
			else
			{
				owner.GetAndIncrementMemberCellDefinitionIndex();
			}
			this.m_definitionEndIndex = owner.GetCurrentMemberCellDefinitionIndex();
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x00067198 File Offset: 0x00065398
		internal override string UniqueName
		{
			get
			{
				return this.ID;
			}
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x000671A0 File Offset: 0x000653A0
		public override string ID
		{
			get
			{
				if (this.m_isStatic)
				{
					return this.m_renderMembers[this.m_staticIndex].ID;
				}
				return ((Microsoft.ReportingServices.ReportRendering.DataMember)this.m_group.CurrentShimRenderGroup).ID;
			}
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x000671D8 File Offset: 0x000653D8
		public override CustomPropertyCollection CustomProperties
		{
			get
			{
				if (this.m_customPropertyCollection == null)
				{
					if (this.m_group != null && this.m_group.CustomProperties != null)
					{
						this.m_customPropertyCollection = this.m_group.CustomProperties;
					}
					else
					{
						this.m_customPropertyCollection = new CustomPropertyCollection();
					}
				}
				return this.m_customPropertyCollection;
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06001948 RID: 6472 RVA: 0x00067226 File Offset: 0x00065426
		public override bool IsStatic
		{
			get
			{
				return this.m_isStatic;
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06001949 RID: 6473 RVA: 0x0006722E File Offset: 0x0006542E
		public override bool IsColumn
		{
			get
			{
				return this.m_isColumn;
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x00067238 File Offset: 0x00065438
		public override int RowSpan
		{
			get
			{
				if (!this.m_isColumn)
				{
					return this.m_definitionEndIndex - this.m_definitionStartIndex;
				}
				if (this.m_isStatic)
				{
					return this.m_renderMembers[this.m_staticIndex].MemberHeadingSpan;
				}
				return ((Microsoft.ReportingServices.ReportRendering.DataMember)this.m_group.CurrentShimRenderGroup).MemberHeadingSpan;
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x00067290 File Offset: 0x00065490
		public override int ColSpan
		{
			get
			{
				if (this.m_isColumn)
				{
					return this.m_definitionEndIndex - this.m_definitionStartIndex;
				}
				if (this.IsStatic)
				{
					return this.m_renderMembers[this.m_staticIndex].MemberHeadingSpan;
				}
				return ((Microsoft.ReportingServices.ReportRendering.DataMember)this.m_group.CurrentShimRenderGroup).MemberHeadingSpan;
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x000672E7 File Offset: 0x000654E7
		public override int MemberCellIndex
		{
			get
			{
				return this.m_definitionStartIndex;
			}
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x000672EF File Offset: 0x000654EF
		internal override Microsoft.ReportingServices.ReportIntermediateFormat.DataMember MemberDefinition
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x000672F2 File Offset: 0x000654F2
		internal override IRIFReportScope RIFReportScope
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x0600194F RID: 6479 RVA: 0x000672F5 File Offset: 0x000654F5
		internal override IReportScopeInstance ReportScopeInstance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x000672F8 File Offset: 0x000654F8
		internal override IReportScope ReportScope
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x000672FC File Offset: 0x000654FC
		public override DataMemberInstance Instance
		{
			get
			{
				if (base.OwnerCri.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					if (this.IsStatic)
					{
						this.m_instance = new DataMemberInstance(base.OwnerCri, this);
					}
					else
					{
						DataDynamicMemberInstance dataDynamicMemberInstance = new DataDynamicMemberInstance(base.OwnerCri, this, new InternalShimDynamicMemberLogic(this));
						base.OwnerCri.RenderingContext.AddDynamicInstance(dataDynamicMemberInstance);
						this.m_instance = dataDynamicMemberInstance;
					}
				}
				return this.m_instance;
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06001952 RID: 6482 RVA: 0x00067372 File Offset: 0x00065572
		internal int DefinitionStartIndex
		{
			get
			{
				return this.m_definitionStartIndex;
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x0006737A File Offset: 0x0006557A
		internal int DefinitionEndIndex
		{
			get
			{
				return this.m_definitionEndIndex;
			}
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06001954 RID: 6484 RVA: 0x00067382 File Offset: 0x00065582
		internal Microsoft.ReportingServices.ReportRendering.DataMember CurrentRenderDataMember
		{
			get
			{
				if (this.m_isStatic)
				{
					return this.m_renderMembers[this.m_staticIndex];
				}
				return this.m_group.CurrentShimRenderGroup as Microsoft.ReportingServices.ReportRendering.DataMember;
			}
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x000673B0 File Offset: 0x000655B0
		internal bool SetNewContext(int index)
		{
			base.ResetContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_isStatic)
			{
				return index <= 1;
			}
			if (base.OwnerCri.RenderCri.CustomData.NoRows)
			{
				return false;
			}
			if (index < 0 || index >= this.m_group.RenderGroups.Count)
			{
				return false;
			}
			this.m_group.CurrentRenderGroupIndex = index;
			this.UpdateInnerContext(this.m_group.RenderGroups[index] as Microsoft.ReportingServices.ReportRendering.DataMember);
			return true;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x00067441 File Offset: 0x00065641
		internal override void ResetContext()
		{
			this.ResetContext(null);
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0006744C File Offset: 0x0006564C
		internal void ResetContext(DataMemberCollection renderMembers)
		{
			if (renderMembers != null)
			{
				this.m_renderMembers = renderMembers;
			}
			if (this.m_group != null)
			{
				this.m_group.CurrentRenderGroupIndex = -1;
			}
			Microsoft.ReportingServices.ReportRendering.DataMember dataMember = (this.IsStatic ? this.m_renderMembers[this.m_staticIndex] : (this.m_group.CurrentShimRenderGroup as Microsoft.ReportingServices.ReportRendering.DataMember));
			this.UpdateInnerContext(dataMember);
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x000674AA File Offset: 0x000656AA
		private void UpdateInnerContext(Microsoft.ReportingServices.ReportRendering.DataMember currentRenderMember)
		{
			if (this.m_children != null)
			{
				((ShimDataMemberCollection)this.m_children).ResetContext(currentRenderMember.Children);
				return;
			}
			((ShimDataRowCollection)base.OwnerCri.CustomData.RowCollection).UpdateCells(this);
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x000674E6 File Offset: 0x000656E6
		bool IShimDataRegionMember.SetNewContext(int index)
		{
			return this.SetNewContext(index);
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x000674EF File Offset: 0x000656EF
		void IShimDataRegionMember.ResetContext()
		{
			this.ResetContext();
		}

		// Token: 0x04000CAC RID: 3244
		private bool m_isColumn;

		// Token: 0x04000CAD RID: 3245
		private bool m_isStatic;

		// Token: 0x04000CAE RID: 3246
		private int m_staticIndex = -1;

		// Token: 0x04000CAF RID: 3247
		private int m_definitionStartIndex = -1;

		// Token: 0x04000CB0 RID: 3248
		private int m_definitionEndIndex = -1;

		// Token: 0x04000CB1 RID: 3249
		private DataMemberCollection m_renderMembers;
	}
}
