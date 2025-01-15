using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000040 RID: 64
	[Serializable]
	public class LhsLengthTransformationFilter : ITransformationFilter, ISessionable
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000C35C File Offset: 0x0000A55C
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000C364 File Offset: 0x0000A564
		public int MaxLength { get; set; }

		// Token: 0x06000282 RID: 642 RVA: 0x0000C36D File Offset: 0x0000A56D
		public LhsLengthTransformationFilter(ITokenIdProvider tokenIdProvider)
		{
			this.MaxLength = 1;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000C37C File Offset: 0x0000A57C
		public ISession CreateSession()
		{
			return new LhsLengthTransformationFilter.Session();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000C383 File Offset: 0x0000A583
		public void Prepare(ISession session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSequence)
		{
			LhsLengthTransformationFilter.Session session2 = (LhsLengthTransformationFilter.Session)session;
			session2.m_tokenIdProvider = tokenIdProvider;
			session2.m_tokenSequence = tokenSequence;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000C398 File Offset: 0x0000A598
		public bool AllowTransformations(ISession session, int fromTokenIndex)
		{
			LhsLengthTransformationFilter.Session session2 = (LhsLengthTransformationFilter.Session)session;
			return !session2.m_tokenIdProvider.SupportsGetToken || session2.m_tokenIdProvider.GetToken(session2.m_tokenSequence[fromTokenIndex]).Length <= this.MaxLength;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000C3E5 File Offset: 0x0000A5E5
		public bool AllowTransformation(ISession session, int fromTokenIndex, Transformation transformation)
		{
			return true;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000C3E8 File Offset: 0x0000A5E8
		public void FilterTransformations(ISession session, int fromTokenIndex, ArraySegment<TransformationMatch> transformationMatchList, out ArraySegment<TransformationMatch> filteredTransformationMatchList)
		{
			filteredTransformationMatchList = transformationMatchList;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000C3F2 File Offset: 0x0000A5F2
		public void FilterTransformations(ISession session, ArraySegment<TransformationMatch> transformationMatchList, out ArraySegment<TransformationMatch> filteredTransformationMatchList)
		{
			filteredTransformationMatchList = transformationMatchList;
		}

		// Token: 0x02000131 RID: 305
		private class Session : ISession
		{
			// Token: 0x06000BF7 RID: 3063 RVA: 0x00033DFE File Offset: 0x00031FFE
			public void Reset()
			{
				this.m_tokenSequence.Clear();
			}

			// Token: 0x040004BA RID: 1210
			public ITokenIdProvider m_tokenIdProvider;

			// Token: 0x040004BB RID: 1211
			public TokenSequence m_tokenSequence;
		}
	}
}
