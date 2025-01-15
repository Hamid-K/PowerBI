using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000364 RID: 868
	internal sealed class MatrixMemberInfoCache
	{
		// Token: 0x0600212D RID: 8493 RVA: 0x0008084C File Offset: 0x0007EA4C
		internal MatrixMemberInfoCache(int startIndex, int length)
		{
			this.m_startIndex = startIndex;
			if (!this.IsOptimizedNode)
			{
				this.m_cellIndexes = new int[length];
				this.m_children = new MatrixMemberInfoCache[length];
				for (int i = 0; i < length; i++)
				{
					this.m_cellIndexes[i] = -1;
				}
			}
		}

		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x0600212E RID: 8494 RVA: 0x0008089B File Offset: 0x0007EA9B
		internal bool IsOptimizedNode
		{
			get
			{
				return this.m_startIndex >= 0;
			}
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x000808AC File Offset: 0x0007EAAC
		internal int GetCellIndex(ShimMatrixMember member)
		{
			if (this.IsOptimizedNode)
			{
				return this.m_startIndex + member.AdjustedRenderCollectionIndex;
			}
			int adjustedRenderCollectionIndex = member.AdjustedRenderCollectionIndex;
			if (this.m_cellIndexes[adjustedRenderCollectionIndex] < 0)
			{
				this.m_cellIndexes[adjustedRenderCollectionIndex] = member.CurrentRenderMatrixMember.CachedMemberCellIndex;
			}
			return this.m_cellIndexes[adjustedRenderCollectionIndex];
		}

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x06002130 RID: 8496 RVA: 0x000808FC File Offset: 0x0007EAFC
		internal MatrixMemberInfoCache[] Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x040010A6 RID: 4262
		private int m_startIndex;

		// Token: 0x040010A7 RID: 4263
		private int[] m_cellIndexes;

		// Token: 0x040010A8 RID: 4264
		private MatrixMemberInfoCache[] m_children;
	}
}
