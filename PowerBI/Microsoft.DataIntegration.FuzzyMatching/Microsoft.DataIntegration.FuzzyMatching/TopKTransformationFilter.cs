using System;
using System.Collections.Generic;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000FF RID: 255
	[Serializable]
	public class TopKTransformationFilter : ITransformationFilter, ISessionable
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0002EEBF File Offset: 0x0002D0BF
		// (set) Token: 0x06000A73 RID: 2675 RVA: 0x0002EEC7 File Offset: 0x0002D0C7
		public int MaxTransformationsPerToken { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0002EED0 File Offset: 0x0002D0D0
		// (set) Token: 0x06000A75 RID: 2677 RVA: 0x0002EED8 File Offset: 0x0002D0D8
		public int MaxTransformations { get; set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0002EEE1 File Offset: 0x0002D0E1
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x0002EEE9 File Offset: 0x0002D0E9
		public Comparison<TransformationMatch> Comparison { get; set; }

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002EEF2 File Offset: 0x0002D0F2
		public TopKTransformationFilter()
		{
			this.MaxTransformationsPerToken = int.MaxValue;
			this.MaxTransformations = int.MaxValue;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0002EF10 File Offset: 0x0002D110
		public ISession CreateSession()
		{
			return new TopKTransformationFilter.Session();
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0002EF17 File Offset: 0x0002D117
		public void Prepare(ISession session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSequence)
		{
			((TopKTransformationFilter.Session)session).m_tokenSequence = tokenSequence;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002EF25 File Offset: 0x0002D125
		public bool AllowTransformations(ISession session, int fromTokenIndex)
		{
			return true;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002EF28 File Offset: 0x0002D128
		public bool AllowTransformation(ISession session, int fromTokenIndex, Transformation transformation)
		{
			return true;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002EF2C File Offset: 0x0002D12C
		public void FilterTransformations(ISession session, int fromTokenIndex, ArraySegment<TransformationMatch> transformationMatchList, out ArraySegment<TransformationMatch> filteredTransformationMatchList)
		{
			TopKTransformationFilter.Session session2 = (TopKTransformationFilter.Session)session;
			this.Filter(session2, transformationMatchList, this.MaxTransformationsPerToken, out filteredTransformationMatchList);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002EF50 File Offset: 0x0002D150
		public void FilterTransformations(ISession session, ArraySegment<TransformationMatch> transformationMatchList, out ArraySegment<TransformationMatch> filteredTransformationMatchList)
		{
			TopKTransformationFilter.Session session2 = (TopKTransformationFilter.Session)session;
			this.Filter(session2, transformationMatchList, this.MaxTransformations, out filteredTransformationMatchList);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002EF74 File Offset: 0x0002D174
		private void Filter(TopKTransformationFilter.Session s, ArraySegment<TransformationMatch> transformationMatchList, int k, out ArraySegment<TransformationMatch> filteredTransformationMatchList)
		{
			if (transformationMatchList.Count > k)
			{
				s.m_tempTransformationMatchesSort.Clear();
				for (int i = 0; i < transformationMatchList.Count; i++)
				{
					s.m_tempTransformationMatchesSort.Add(transformationMatchList.Array[transformationMatchList.Offset + i]);
				}
				if (this.Comparison != null)
				{
					s.m_tempTransformationMatchesSort.Sort(this.Comparison);
				}
				filteredTransformationMatchList = s.m_transformationMatchAllocator.New(k);
				for (int j = 0; j < k; j++)
				{
					filteredTransformationMatchList.Array[filteredTransformationMatchList.Offset + j] = new TransformationMatch
					{
						Position = s.m_tempTransformationMatchesSort[j].Position,
						Transformation = s.m_tempTransformationMatchesSort[j].Transformation
					};
				}
				return;
			}
			filteredTransformationMatchList = transformationMatchList;
		}

		// Token: 0x0200019E RID: 414
		private class Session : ISession
		{
			// Token: 0x06000DB3 RID: 3507 RVA: 0x0003A9DB File Offset: 0x00038BDB
			public void Reset()
			{
				this.m_tokenSequence.Clear();
				this.m_tempTransformationMatchesSort.Clear();
				this.m_transformationMatchAllocator.Reset();
			}

			// Token: 0x040006CB RID: 1739
			public TokenSequence m_tokenSequence;

			// Token: 0x040006CC RID: 1740
			public List<TransformationMatch> m_tempTransformationMatchesSort = new List<TransformationMatch>();

			// Token: 0x040006CD RID: 1741
			public BlockedSegmentArray<TransformationMatch> m_transformationMatchAllocator = new BlockedSegmentArray<TransformationMatch>();
		}
	}
}
