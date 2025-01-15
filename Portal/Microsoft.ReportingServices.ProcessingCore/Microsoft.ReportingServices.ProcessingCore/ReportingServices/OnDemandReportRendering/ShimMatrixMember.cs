using System;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000377 RID: 887
	internal sealed class ShimMatrixMember : ShimTablixMember
	{
		// Token: 0x06002206 RID: 8710 RVA: 0x00082F64 File Offset: 0x00081164
		internal ShimMatrixMember(IDefinitionPath parentDefinitionPath, Tablix owner, ShimMatrixMember parent, int parentCollectionIndex, bool isColumn, int renderCollectionIndex, MatrixMember staticOrSubtotal, bool isAfterSubtotal, MatrixMemberInfoCache matrixMemberCellIndexes)
			: base(parentDefinitionPath, owner, parent, parentCollectionIndex, isColumn)
		{
			this.m_renderCollectionIndex = renderCollectionIndex;
			this.m_isAfterSubtotal = isAfterSubtotal;
			this.m_currentMatrixMemberCellIndexes = matrixMemberCellIndexes;
			this.m_definitionStartIndex = owner.GetCurrentMemberCellDefinitionIndex();
			this.m_staticOrSubtotal = staticOrSubtotal;
			this.GenerateInnerHierarchy(owner, parent, isColumn, staticOrSubtotal.Children);
			this.m_definitionEndIndex = owner.GetCurrentMemberCellDefinitionIndex();
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x00082FDC File Offset: 0x000811DC
		internal ShimMatrixMember(IDefinitionPath parentDefinitionPath, Tablix owner, ShimMatrixMember parent, int parentCollectionIndex, bool isColumn, int renderCollectionIndex, ShimRenderGroups renderGroups, MatrixMemberInfoCache matrixMemberCellIndexes)
			: base(parentDefinitionPath, owner, parent, parentCollectionIndex, isColumn)
		{
			this.m_renderCollectionIndex = renderCollectionIndex;
			this.m_currentMatrixMemberCellIndexes = matrixMemberCellIndexes;
			this.m_definitionStartIndex = owner.GetCurrentMemberCellDefinitionIndex();
			this.m_group = new Group(owner, renderGroups, this);
			this.GenerateInnerHierarchy(owner, parent, isColumn, ((MatrixMember)this.m_group.CurrentShimRenderGroup).Children);
			this.m_definitionEndIndex = owner.GetCurrentMemberCellDefinitionIndex();
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x00083064 File Offset: 0x00081264
		private void GenerateInnerHierarchy(Tablix owner, ShimMatrixMember parent, bool isColumn, MatrixMemberCollection children)
		{
			if (children != null)
			{
				MatrixMemberInfoCache matrixMemberInfoCache = null;
				if (this.m_isColumn)
				{
					if (children.MatrixHeadingDef.SubHeading != null)
					{
						matrixMemberInfoCache = new MatrixMemberInfoCache(-1, children.Count);
					}
					else
					{
						matrixMemberInfoCache = new MatrixMemberInfoCache((this.m_staticOrSubtotal != null) ? this.m_staticOrSubtotal.MemberCellIndex : this.AdjustedRenderCollectionIndex, -1);
					}
					this.m_currentMatrixMemberCellIndexes.Children[this.AdjustedRenderCollectionIndex] = matrixMemberInfoCache;
				}
				this.m_children = new ShimMatrixMemberCollection(this, owner, isColumn, this, children, matrixMemberInfoCache);
				return;
			}
			owner.GetAndIncrementMemberCellDefinitionIndex();
		}

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x06002209 RID: 8713 RVA: 0x000830EC File Offset: 0x000812EC
		internal double SizeDelta
		{
			get
			{
				if (this.m_children != null)
				{
					return this.m_children.SizeDelta;
				}
				return 0.0;
			}
		}

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x0600220A RID: 8714 RVA: 0x0008310B File Offset: 0x0008130B
		public override bool HideIfNoRows
		{
			get
			{
				return ((this.m_parent == null || this.m_parent.IsStatic) && this.m_staticOrSubtotal != null && this.m_staticOrSubtotal.IsTotal) || base.HideIfNoRows;
			}
		}

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x0008313F File Offset: 0x0008133F
		public override string DataElementName
		{
			get
			{
				if (this.m_staticOrSubtotal != null)
				{
					return this.m_staticOrSubtotal.DataElementName;
				}
				return base.DataElementName;
			}
		}

		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x0008315B File Offset: 0x0008135B
		public override DataElementOutputTypes DataElementOutput
		{
			get
			{
				if (this.m_staticOrSubtotal != null)
				{
					return (DataElementOutputTypes)this.m_staticOrSubtotal.DataElementOutput;
				}
				return base.DataElementOutput;
			}
		}

		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x0600220D RID: 8717 RVA: 0x00083177 File Offset: 0x00081377
		public override TablixHeader TablixHeader
		{
			get
			{
				if (this.m_header == null)
				{
					this.m_header = new TablixHeader(base.OwnerTablix, this);
				}
				return this.m_header;
			}
		}

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x00083199 File Offset: 0x00081399
		public override TablixMemberCollection Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x0600220F RID: 8719 RVA: 0x000831A1 File Offset: 0x000813A1
		public override bool FixedData
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x000831A4 File Offset: 0x000813A4
		public override bool IsStatic
		{
			get
			{
				return this.m_staticOrSubtotal != null;
			}
		}

		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x06002211 RID: 8721 RVA: 0x000831AF File Offset: 0x000813AF
		internal override int RowSpan
		{
			get
			{
				if (this.m_isColumn)
				{
					return this.CurrentRenderMatrixMember.RowSpan;
				}
				return this.m_definitionEndIndex - this.m_definitionStartIndex;
			}
		}

		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x000831D2 File Offset: 0x000813D2
		internal override int ColSpan
		{
			get
			{
				if (this.m_isColumn)
				{
					return this.m_definitionEndIndex - this.m_definitionStartIndex;
				}
				return this.CurrentRenderMatrixMember.ColumnSpan;
			}
		}

		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x06002213 RID: 8723 RVA: 0x000831F5 File Offset: 0x000813F5
		public override int MemberCellIndex
		{
			get
			{
				return this.m_definitionStartIndex;
			}
		}

		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x000831FD File Offset: 0x000813FD
		public override bool IsTotal
		{
			get
			{
				return this.m_group == null && this.m_staticOrSubtotal != null && this.m_staticOrSubtotal.IsTotal && this.m_staticOrSubtotal.Hidden && this.m_staticOrSubtotal.SharedHidden == SharedHiddenState.Always;
			}
		}

		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x06002215 RID: 8725 RVA: 0x00083239 File Offset: 0x00081439
		public override Visibility Visibility
		{
			get
			{
				if (this.m_visibility == null && this.m_group != null && this.m_group.CurrentShimRenderGroup.m_visibilityDef != null)
				{
					this.m_visibility = new ShimMatrixMemberVisibility(this);
				}
				return this.m_visibility;
			}
		}

		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x0008326F File Offset: 0x0008146F
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

		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x06002217 RID: 8727 RVA: 0x00083281 File Offset: 0x00081481
		public override bool KeepTogether
		{
			get
			{
				return !this.m_isColumn;
			}
		}

		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x06002218 RID: 8728 RVA: 0x00083290 File Offset: 0x00081490
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

		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x06002219 RID: 8729 RVA: 0x00083306 File Offset: 0x00081506
		internal int DefinitionStartIndex
		{
			get
			{
				return this.m_definitionStartIndex;
			}
		}

		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x0600221A RID: 8730 RVA: 0x0008330E File Offset: 0x0008150E
		internal int DefinitionEndIndex
		{
			get
			{
				return this.m_definitionEndIndex;
			}
		}

		// Token: 0x17001353 RID: 4947
		// (get) Token: 0x0600221B RID: 8731 RVA: 0x00083316 File Offset: 0x00081516
		internal int AdjustedRenderCollectionIndex
		{
			get
			{
				if (this.IsStatic)
				{
					return this.m_renderCollectionIndex;
				}
				return this.m_renderCollectionIndex + Math.Max(0, this.m_group.CurrentRenderGroupIndex);
			}
		}

		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x0600221C RID: 8732 RVA: 0x0008333F File Offset: 0x0008153F
		internal MatrixMemberInfoCache CurrentMatrixMemberCellIndexes
		{
			get
			{
				return this.m_currentMatrixMemberCellIndexes;
			}
		}

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x00083347 File Offset: 0x00081547
		internal MatrixMember CurrentRenderMatrixMember
		{
			get
			{
				if (this.m_staticOrSubtotal != null)
				{
					return this.m_staticOrSubtotal;
				}
				return this.m_group.CurrentShimRenderGroup as MatrixMember;
			}
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x00083368 File Offset: 0x00081568
		internal override bool SetNewContext(int index)
		{
			base.ResetContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_group == null)
			{
				return index <= 1;
			}
			if (base.OwnerTablix.RenderMatrix.NoRows)
			{
				return false;
			}
			if (index < 0 || index >= this.m_group.RenderGroups.Count)
			{
				return false;
			}
			this.m_group.CurrentRenderGroupIndex = index;
			MatrixMember matrixMember = this.m_group.RenderGroups[index] as MatrixMember;
			this.UpdateMatrixMemberInfoCache(this.m_group.RenderGroups.MatrixMemberCollectionCount, null);
			this.UpdateContext(matrixMember);
			return true;
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x0008340D File Offset: 0x0008160D
		internal override void ResetContext()
		{
			base.ResetContext();
			if (this.m_group.CurrentRenderGroupIndex >= 0)
			{
				this.ResetContext(null, -1, null, null);
			}
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x00083430 File Offset: 0x00081630
		internal void ResetContext(MatrixMember staticOrSubtotal, int newAfterSubtotalCollectionIndex, ShimRenderGroups renderGroups, MatrixMemberInfoCache newMatrixMemberCellIndexes)
		{
			int num = 1;
			if (this.m_group != null)
			{
				this.m_group.CurrentRenderGroupIndex = -1;
				if (renderGroups != null)
				{
					this.m_group.RenderGroups = renderGroups;
				}
				num = this.m_group.RenderGroups.MatrixMemberCollectionCount;
			}
			else if (staticOrSubtotal != null)
			{
				this.m_staticOrSubtotal = staticOrSubtotal;
				if (this.m_isAfterSubtotal && newAfterSubtotalCollectionIndex >= 0)
				{
					this.m_renderCollectionIndex = newAfterSubtotalCollectionIndex;
				}
			}
			this.UpdateMatrixMemberInfoCache(num, newMatrixMemberCellIndexes);
			if (this.IsStatic)
			{
				this.UpdateContext(this.m_staticOrSubtotal);
				return;
			}
			if (!base.OwnerTablix.RenderMatrix.NoRows && this.m_group.RenderGroups != null && this.m_group.RenderGroups.Count > 0)
			{
				this.UpdateContext(this.m_group.RenderGroups[0] as MatrixMember);
			}
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x00083500 File Offset: 0x00081700
		private void UpdateContext(MatrixMember currentRenderGroup)
		{
			if (this.m_header != null)
			{
				this.m_header.ResetCellContents();
			}
			if (this.m_children != null)
			{
				((ShimMatrixMemberCollection)this.m_children).ResetContext(currentRenderGroup.Children);
				return;
			}
			((ShimMatrixRowCollection)base.OwnerTablix.Body.RowCollection).UpdateCells(this);
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x0008355C File Offset: 0x0008175C
		private void UpdateMatrixMemberInfoCache(int currentAllocationSize, MatrixMemberInfoCache newMatrixMemberCellIndexes)
		{
			if (!this.m_isColumn)
			{
				return;
			}
			MatrixMemberInfoCache matrixMemberInfoCache = ((this.m_parent == null) ? null : ((ShimMatrixMember)this.m_parent).CurrentMatrixMemberCellIndexes);
			if (matrixMemberInfoCache == null)
			{
				if (newMatrixMemberCellIndexes != null)
				{
					this.m_currentMatrixMemberCellIndexes = newMatrixMemberCellIndexes;
					return;
				}
			}
			else
			{
				int adjustedRenderCollectionIndex = ((ShimMatrixMember)this.m_parent).AdjustedRenderCollectionIndex;
				MatrixMemberInfoCache matrixMemberInfoCache2 = matrixMemberInfoCache.Children[adjustedRenderCollectionIndex];
				if (matrixMemberInfoCache2 == null)
				{
					if (this.m_children != null)
					{
						matrixMemberInfoCache2 = new MatrixMemberInfoCache(-1, currentAllocationSize);
					}
					else
					{
						matrixMemberInfoCache2 = new MatrixMemberInfoCache(matrixMemberInfoCache.GetCellIndex((ShimMatrixMember)this.m_parent), -1);
					}
					matrixMemberInfoCache.Children[adjustedRenderCollectionIndex] = matrixMemberInfoCache2;
				}
				this.m_currentMatrixMemberCellIndexes = matrixMemberInfoCache2;
			}
		}

		// Token: 0x040010E5 RID: 4325
		private int m_definitionStartIndex = -1;

		// Token: 0x040010E6 RID: 4326
		private int m_definitionEndIndex = -1;

		// Token: 0x040010E7 RID: 4327
		private int m_renderCollectionIndex = -1;

		// Token: 0x040010E8 RID: 4328
		private bool m_isAfterSubtotal;

		// Token: 0x040010E9 RID: 4329
		internal MatrixMember m_staticOrSubtotal;

		// Token: 0x040010EA RID: 4330
		private MatrixMemberInfoCache m_currentMatrixMemberCellIndexes;
	}
}
