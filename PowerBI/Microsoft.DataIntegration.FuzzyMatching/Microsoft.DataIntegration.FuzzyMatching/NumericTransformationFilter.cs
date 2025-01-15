using System;
using Microsoft.DataIntegration.FuzzyMatchingCommon;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000042 RID: 66
	[Serializable]
	public class NumericTransformationFilter : ITransformationFilter, ISessionable
	{
		// Token: 0x0600028B RID: 651 RVA: 0x0000C464 File Offset: 0x0000A664
		public ISession CreateSession()
		{
			return new NumericTransformationFilter.Session();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000C46B File Offset: 0x0000A66B
		public void Prepare(ISession session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSequence)
		{
			NumericTransformationFilter.Session session2 = (NumericTransformationFilter.Session)session;
			session2.m_tokenIdProvider = tokenIdProvider;
			session2.m_tokenSequence = tokenSequence;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000C480 File Offset: 0x0000A680
		public bool AllowTransformations(ISession session, int fromTokenIndex)
		{
			NumericTransformationFilter.Session session2 = (NumericTransformationFilter.Session)session;
			if (!session2.m_tokenIdProvider.SupportsGetToken)
			{
				throw new InvalidOperationException("NumericTransformationFilter does not work if the token id provider does not support GetToken().");
			}
			return !NumericTransformationFilter.IsNumericToken(session2.m_tokenIdProvider.GetToken(session2.m_tokenSequence[fromTokenIndex]));
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000C4CB File Offset: 0x0000A6CB
		public bool AllowTransformation(ISession session, int fromTokenIndex, Transformation transformation)
		{
			return true;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000C4CE File Offset: 0x0000A6CE
		public void FilterTransformations(ISession session, int fromTokenIndex, ArraySegment<TransformationMatch> transformationMatchList, out ArraySegment<TransformationMatch> filteredTransformationMatchList)
		{
			filteredTransformationMatchList = transformationMatchList;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000C4D8 File Offset: 0x0000A6D8
		public void FilterTransformations(ISession session, ArraySegment<TransformationMatch> transformationMatchList, out ArraySegment<TransformationMatch> filteredTransformationMatchList)
		{
			filteredTransformationMatchList = transformationMatchList;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000C4E4 File Offset: 0x0000A6E4
		protected static bool IsNumericToken(StringExtent token)
		{
			for (int i = 0; i < token.Length; i++)
			{
				if (char.IsDigit(token[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x02000132 RID: 306
		private class Session : ISession
		{
			// Token: 0x06000BF9 RID: 3065 RVA: 0x00033E13 File Offset: 0x00032013
			public void Reset()
			{
				this.m_tokenSequence = default(TokenSequence);
				this.m_tokenIdProvider = null;
			}

			// Token: 0x040004BC RID: 1212
			public ITokenIdProvider m_tokenIdProvider;

			// Token: 0x040004BD RID: 1213
			public TokenSequence m_tokenSequence;
		}
	}
}
