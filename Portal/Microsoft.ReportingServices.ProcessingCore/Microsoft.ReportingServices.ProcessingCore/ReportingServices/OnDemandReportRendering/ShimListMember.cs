using System;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000375 RID: 885
	internal sealed class ShimListMember : ShimTablixMember
	{
		// Token: 0x060021D8 RID: 8664 RVA: 0x0008261B File Offset: 0x0008081B
		internal ShimListMember(IDefinitionPath parentDefinitionPath, Tablix owner, ShimRenderGroups renderGroups, int parentCollectionIndex, bool isColumn)
			: base(parentDefinitionPath, owner, null, parentCollectionIndex, isColumn)
		{
			this.m_group = new Group(owner, renderGroups, this);
		}

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x00082638 File Offset: 0x00080838
		public override string ID
		{
			get
			{
				return base.DefinitionPath;
			}
		}

		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x060021DA RID: 8666 RVA: 0x00082640 File Offset: 0x00080840
		public override TablixMemberCollection Children
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x060021DB RID: 8667 RVA: 0x00082643 File Offset: 0x00080843
		public override bool IsStatic
		{
			get
			{
				return this.m_group == null || this.m_group.RenderGroups == null;
			}
		}

		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x060021DC RID: 8668 RVA: 0x0008265D File Offset: 0x0008085D
		internal override int RowSpan
		{
			get
			{
				if (this.IsColumn)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x060021DD RID: 8669 RVA: 0x0008266A File Offset: 0x0008086A
		internal override int ColSpan
		{
			get
			{
				if (this.IsColumn)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x060021DE RID: 8670 RVA: 0x00082677 File Offset: 0x00080877
		public override int MemberCellIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x060021DF RID: 8671 RVA: 0x0008267A File Offset: 0x0008087A
		public override bool KeepTogether
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x060021E0 RID: 8672 RVA: 0x0008267D File Offset: 0x0008087D
		public override bool IsTotal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x00082680 File Offset: 0x00080880
		public override TablixHeader TablixHeader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x060021E2 RID: 8674 RVA: 0x00082683 File Offset: 0x00080883
		public override Visibility Visibility
		{
			get
			{
				if (this.m_visibility == null && !this.IsColumn && base.OwnerTablix.RenderList.ReportItemDef.Visibility != null)
				{
					this.m_visibility = new ShimListMemberVisibility(this);
				}
				return this.m_visibility;
			}
		}

		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x060021E3 RID: 8675 RVA: 0x000826BE File Offset: 0x000808BE
		internal override PageBreakLocation PropagatedGroupBreak
		{
			get
			{
				if (this.IsStatic)
				{
					return PageBreakLocation.None;
				}
				return this.m_propagatedPageBreak;
			}
		}

		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x060021E4 RID: 8676 RVA: 0x000826D0 File Offset: 0x000808D0
		public override TablixMemberInstance Instance
		{
			get
			{
				if (base.OwnerTablix.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					if (this.IsStatic)
					{
						this.m_instance = new TablixMemberInstance(base.OwnerTablix, this);
					}
					else
					{
						TablixDynamicMemberInstance tablixDynamicMemberInstance = new TablixDynamicMemberInstance(base.OwnerTablix, this, new InternalShimDynamicMemberLogic(this));
						this.m_owner.RenderingContext.AddDynamicInstance(tablixDynamicMemberInstance);
						this.m_instance = tablixDynamicMemberInstance;
					}
				}
				return this.m_instance;
			}
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x00082748 File Offset: 0x00080948
		internal override bool SetNewContext(int index)
		{
			base.ResetContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_group == null || this.m_group.RenderGroups == null)
			{
				return index <= 1;
			}
			if (base.OwnerTablix.RenderList.NoRows)
			{
				return false;
			}
			if (index < 0 || index >= this.m_group.RenderGroups.Count)
			{
				return false;
			}
			this.m_group.CurrentRenderGroupIndex = index;
			((ShimListRow)((ShimListRowCollection)base.OwnerTablix.Body.RowCollection)[0]).UpdateCells(this.m_group.RenderGroups[index] as ListContent);
			return true;
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x00082803 File Offset: 0x00080A03
		internal override void ResetContext()
		{
			base.ResetContext();
			if (this.m_group.CurrentRenderGroupIndex >= 0)
			{
				this.ResetContext(null);
			}
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x00082820 File Offset: 0x00080A20
		internal void ResetContext(ShimRenderGroups renderGroups)
		{
			if (this.m_group != null)
			{
				this.m_group.CurrentRenderGroupIndex = -1;
				if (renderGroups != null)
				{
					this.m_group.RenderGroups = renderGroups;
				}
			}
		}
	}
}
