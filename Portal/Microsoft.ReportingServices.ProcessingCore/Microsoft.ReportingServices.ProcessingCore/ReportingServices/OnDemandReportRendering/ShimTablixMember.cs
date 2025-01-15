using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000374 RID: 884
	internal abstract class ShimTablixMember : TablixMember, IShimDataRegionMember
	{
		// Token: 0x060021C3 RID: 8643 RVA: 0x000824FD File Offset: 0x000806FD
		internal ShimTablixMember(IDefinitionPath parentDefinitionPath, Tablix owner, TablixMember parent, int parentCollectionIndex, bool isColumn)
			: base(parentDefinitionPath, owner, parent, parentCollectionIndex)
		{
			this.m_isColumn = isColumn;
		}

		// Token: 0x17001310 RID: 4880
		// (get) Token: 0x060021C4 RID: 8644 RVA: 0x00082512 File Offset: 0x00080712
		internal override string UniqueName
		{
			get
			{
				return this.ID;
			}
		}

		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x060021C5 RID: 8645 RVA: 0x0008251A File Offset: 0x0008071A
		public override string ID
		{
			get
			{
				if (this.m_group != null && this.m_group.RenderGroups != null)
				{
					return this.m_group.CurrentShimRenderGroup.ID;
				}
				return base.DefinitionPath;
			}
		}

		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x060021C6 RID: 8646 RVA: 0x00082548 File Offset: 0x00080748
		public override string DataElementName
		{
			get
			{
				if (this.m_group != null && this.m_group.CurrentShimRenderGroup != null)
				{
					return this.m_group.CurrentShimRenderGroup.DataCollectionName;
				}
				return null;
			}
		}

		// Token: 0x17001313 RID: 4883
		// (get) Token: 0x060021C7 RID: 8647 RVA: 0x00082571 File Offset: 0x00080771
		public override DataElementOutputTypes DataElementOutput
		{
			get
			{
				if (!this.IsStatic)
				{
					return DataElementOutputTypes.Output;
				}
				if (this.TablixHeader != null)
				{
					return DataElementOutputTypes.Output;
				}
				return DataElementOutputTypes.ContentsOnly;
			}
		}

		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x00082588 File Offset: 0x00080788
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

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x060021C9 RID: 8649 RVA: 0x000825D6 File Offset: 0x000807D6
		public override TablixHeader TablixHeader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x000825D9 File Offset: 0x000807D9
		public override bool IsColumn
		{
			get
			{
				return this.m_isColumn;
			}
		}

		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x000825E1 File Offset: 0x000807E1
		public override bool HideIfNoRows
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x060021CC RID: 8652 RVA: 0x000825E4 File Offset: 0x000807E4
		internal override TablixMember MemberDefinition
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x000825E7 File Offset: 0x000807E7
		internal override void ResetContext()
		{
			base.ResetContext();
		}

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x000825EF File Offset: 0x000807EF
		public override bool FixedData
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x000825F2 File Offset: 0x000807F2
		public override KeepWithGroup KeepWithGroup
		{
			get
			{
				return KeepWithGroup.None;
			}
		}

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x000825F5 File Offset: 0x000807F5
		public override bool RepeatOnNewPage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x000825F8 File Offset: 0x000807F8
		internal virtual void SetPropagatedPageBreak(PageBreakLocation pageBreakLocation)
		{
			this.m_propagatedPageBreak = pageBreakLocation;
		}

		// Token: 0x060021D2 RID: 8658
		internal abstract bool SetNewContext(int index);

		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x00082601 File Offset: 0x00080801
		internal override IRIFReportScope RIFReportScope
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x060021D4 RID: 8660 RVA: 0x00082604 File Offset: 0x00080804
		internal override IReportScopeInstance ReportScopeInstance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x060021D5 RID: 8661 RVA: 0x00082607 File Offset: 0x00080807
		internal override IReportScope ReportScope
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x0008260A File Offset: 0x0008080A
		bool IShimDataRegionMember.SetNewContext(int index)
		{
			return this.SetNewContext(index);
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x00082613 File Offset: 0x00080813
		void IShimDataRegionMember.ResetContext()
		{
			this.ResetContext();
		}

		// Token: 0x040010DB RID: 4315
		protected bool m_isColumn;

		// Token: 0x040010DC RID: 4316
		protected PageBreakLocation m_propagatedPageBreak;
	}
}
