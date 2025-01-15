using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000FE RID: 254
	[Serializable]
	public class TransformationFilterAggregator : List<ITransformationFilter>, ITransformationFilter, ISessionable
	{
		// Token: 0x06000A6B RID: 2667 RVA: 0x0002ECE0 File Offset: 0x0002CEE0
		public ISession CreateSession()
		{
			TransformationFilterAggregator.Session session = new TransformationFilterAggregator.Session();
			foreach (ITransformationFilter transformationFilter in this)
			{
				if (transformationFilter is ISessionable)
				{
					session.sessions.Add((transformationFilter as ISessionable).CreateSession());
				}
				else
				{
					session.sessions.Add(null);
				}
			}
			return session;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002ED5C File Offset: 0x0002CF5C
		public void Prepare(ISession session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSequence)
		{
			TransformationFilterAggregator.Session session2 = (TransformationFilterAggregator.Session)session;
			for (int i = 0; i < base.Count; i++)
			{
				base[i].Prepare(session2.sessions[i], tokenIdProvider, tokenSequence);
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0002ED9C File Offset: 0x0002CF9C
		public bool AllowTransformations(ISession session, int fromTokenIndex)
		{
			TransformationFilterAggregator.Session session2 = (TransformationFilterAggregator.Session)session;
			for (int i = 0; i < base.Count; i++)
			{
				if (!base[i].AllowTransformations(session2.sessions[i], fromTokenIndex))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002EDE0 File Offset: 0x0002CFE0
		public bool AllowTransformation(ISession session, int fromTokenIndex, Transformation transformation)
		{
			TransformationFilterAggregator.Session session2 = (TransformationFilterAggregator.Session)session;
			for (int i = 0; i < base.Count; i++)
			{
				if (!base[i].AllowTransformation(session2.sessions[i], fromTokenIndex, transformation))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0002EE24 File Offset: 0x0002D024
		public void FilterTransformations(ISession session, int fromTokenIndex, ArraySegment<TransformationMatch> transformationMatchList, out ArraySegment<TransformationMatch> filteredTransformationMatchList)
		{
			TransformationFilterAggregator.Session session2 = (TransformationFilterAggregator.Session)session;
			for (int i = 0; i < base.Count; i++)
			{
				base[i].FilterTransformations(session2.sessions[i], fromTokenIndex, transformationMatchList, out transformationMatchList);
			}
			filteredTransformationMatchList = transformationMatchList;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002EE70 File Offset: 0x0002D070
		public void FilterTransformations(ISession session, ArraySegment<TransformationMatch> transformationMatchList, out ArraySegment<TransformationMatch> filteredTransformationMatchList)
		{
			TransformationFilterAggregator.Session session2 = (TransformationFilterAggregator.Session)session;
			for (int i = 0; i < base.Count; i++)
			{
				base[i].FilterTransformations(session2.sessions[i], transformationMatchList, out transformationMatchList);
			}
			filteredTransformationMatchList = transformationMatchList;
		}

		// Token: 0x0200019D RID: 413
		private class Session : ISession
		{
			// Token: 0x06000DB1 RID: 3505 RVA: 0x0003A994 File Offset: 0x00038B94
			public void Reset()
			{
				for (int i = 0; i < this.sessions.Count; i++)
				{
					this.sessions[i].Reset();
				}
			}

			// Token: 0x040006CA RID: 1738
			public List<ISession> sessions = new List<ISession>();
		}
	}
}
