using System;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000370 RID: 880
	internal sealed class ShimMatrixMemberCollection : ShimMemberCollection
	{
		// Token: 0x06002180 RID: 8576 RVA: 0x000819DC File Offset: 0x0007FBDC
		internal ShimMatrixMemberCollection(IDefinitionPath parentDefinitionPath, Tablix owner, bool isColumnGroup, ShimMatrixMember parent, MatrixMemberCollection renderMemberCollection, MatrixMemberInfoCache matrixMemberCellIndexes)
			: base(parentDefinitionPath, owner, isColumnGroup)
		{
			this.m_definitionStartIndex = owner.GetCurrentMemberCellDefinitionIndex();
			int count = renderMemberCollection.Count;
			if (renderMemberCollection[0].IsStatic)
			{
				DataRegionMember[] array = new ShimMatrixMember[count];
				this.m_children = array;
				for (int i = 0; i < count; i++)
				{
					this.m_children[i] = new ShimMatrixMember(this, owner, parent, i, isColumnGroup, i, renderMemberCollection[i], false, matrixMemberCellIndexes);
				}
			}
			else
			{
				this.m_dynamicSubgroupChildIndex = 0;
				bool flag = renderMemberCollection.MatrixHeadingDef.Subtotal != null;
				bool flag2 = flag && renderMemberCollection.MatrixHeadingDef.Subtotal.Position == Subtotal.PositionType.After;
				DataRegionMember[] array = new ShimMatrixMember[flag ? 2 : 1];
				this.m_children = array;
				if (flag)
				{
					this.m_subtotalChildIndex = 0;
					if (flag2)
					{
						this.m_subtotalChildIndex++;
					}
					else
					{
						this.m_dynamicSubgroupChildIndex++;
					}
				}
				if (flag)
				{
					Microsoft.ReportingServices.ReportRendering.ReportItem reportItem = renderMemberCollection[this.m_subtotalChildIndex].ReportItem;
					if (reportItem != null)
					{
						if (isColumnGroup)
						{
							this.m_sizeDelta += reportItem.Width.ToMillimeters();
						}
						else
						{
							this.m_sizeDelta += reportItem.Height.ToMillimeters();
						}
					}
				}
				if (flag && !flag2)
				{
					this.m_children[this.m_subtotalChildIndex] = new ShimMatrixMember(this, owner, parent, this.m_subtotalChildIndex, isColumnGroup, 0, renderMemberCollection[0], flag2, matrixMemberCellIndexes);
				}
				ShimRenderGroups shimRenderGroups = new ShimRenderGroups(renderMemberCollection, flag && !flag2, flag && flag2);
				ShimMatrixMember shimMatrixMember = (this.m_children[this.m_dynamicSubgroupChildIndex] = new ShimMatrixMember(this, owner, parent, this.m_dynamicSubgroupChildIndex, isColumnGroup, this.m_dynamicSubgroupChildIndex, shimRenderGroups, matrixMemberCellIndexes));
				if (flag && flag2)
				{
					this.m_children[this.m_subtotalChildIndex] = new ShimMatrixMember(this, owner, parent, this.m_subtotalChildIndex, isColumnGroup, count - 1, renderMemberCollection[count - 1], flag2, matrixMemberCellIndexes);
				}
				this.m_sizeDelta += shimMatrixMember.SizeDelta;
			}
			this.m_definitionEndIndex = owner.GetCurrentMemberCellDefinitionIndex();
		}

		// Token: 0x170012DA RID: 4826
		public override TablixMember this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return (TablixMember)this.m_children[index];
			}
		}

		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x00081C54 File Offset: 0x0007FE54
		internal override double SizeDelta
		{
			get
			{
				return this.m_sizeDelta;
			}
		}

		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x06002183 RID: 8579 RVA: 0x00081C5C File Offset: 0x0007FE5C
		public override int Count
		{
			get
			{
				return this.m_children.Length;
			}
		}

		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x00081C66 File Offset: 0x0007FE66
		internal PageBreakLocation PropagatedGroupBreakLocation
		{
			get
			{
				if (this.m_dynamicSubgroupChildIndex < 0)
				{
					return PageBreakLocation.None;
				}
				return ((TablixMember)this.m_children[this.m_dynamicSubgroupChildIndex]).PropagatedGroupBreak;
			}
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x00081C8C File Offset: 0x0007FE8C
		internal void UpdateContext(MatrixMemberInfoCache matrixMemberCellIndexes)
		{
			if (this.m_children == null)
			{
				return;
			}
			if (this.m_isColumnGroup)
			{
				this.ResetContext(base.OwnerTablix.RenderMatrix.ColumnMemberCollection, matrixMemberCellIndexes);
				return;
			}
			this.ResetContext(base.OwnerTablix.RenderMatrix.RowMemberCollection);
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x00081CD8 File Offset: 0x0007FED8
		internal void ResetContext(MatrixMemberCollection newRenderMemberCollection)
		{
			this.ResetContext(newRenderMemberCollection, null);
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x00081CE4 File Offset: 0x0007FEE4
		internal void ResetContext(MatrixMemberCollection newRenderMemberCollection, MatrixMemberInfoCache matrixMemberCellIndexes)
		{
			if (this.m_children == null)
			{
				return;
			}
			MatrixMember matrixMember = null;
			int num = -1;
			ShimRenderGroups shimRenderGroups = null;
			if (newRenderMemberCollection != null)
			{
				shimRenderGroups = new ShimRenderGroups(newRenderMemberCollection, this.m_subtotalChildIndex == 0, 1 == this.m_subtotalChildIndex);
				int count = newRenderMemberCollection.Count;
				if (this.m_subtotalChildIndex == 0)
				{
					matrixMember = newRenderMemberCollection[0];
				}
				else if (1 == this.m_subtotalChildIndex)
				{
					matrixMember = newRenderMemberCollection[count - 1];
					num = count - 1;
				}
			}
			if (this.m_dynamicSubgroupChildIndex >= 0)
			{
				((ShimMatrixMember)this.m_children[this.m_dynamicSubgroupChildIndex]).ResetContext(null, -1, shimRenderGroups, matrixMemberCellIndexes);
				if (this.m_subtotalChildIndex >= 0)
				{
					((ShimMatrixMember)this.m_children[this.m_subtotalChildIndex]).ResetContext(matrixMember, num, null, matrixMemberCellIndexes);
					return;
				}
			}
			else
			{
				for (int i = 0; i < this.m_children.Length; i++)
				{
					matrixMember = ((newRenderMemberCollection != null) ? newRenderMemberCollection[i] : null);
					((ShimMatrixMember)this.m_children[i]).ResetContext(matrixMember, -1, null, matrixMemberCellIndexes);
				}
			}
		}

		// Token: 0x040010CA RID: 4298
		private int m_definitionStartIndex = -1;

		// Token: 0x040010CB RID: 4299
		private int m_definitionEndIndex = -1;

		// Token: 0x040010CC RID: 4300
		private int m_dynamicSubgroupChildIndex = -1;

		// Token: 0x040010CD RID: 4301
		private int m_subtotalChildIndex = -1;

		// Token: 0x040010CE RID: 4302
		private double m_sizeDelta;
	}
}
