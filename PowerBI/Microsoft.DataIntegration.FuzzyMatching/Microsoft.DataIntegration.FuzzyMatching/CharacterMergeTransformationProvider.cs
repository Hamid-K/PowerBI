using System;
using System.Collections.Generic;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000F5 RID: 245
	[Serializable]
	public class CharacterMergeTransformationProvider : ITransformationProvider, IProviderInitialize, ISessionable
	{
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0002D29A File Offset: 0x0002B49A
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x0002D2A2 File Offset: 0x0002B4A2
		public int MaxMergableTokenLength { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0002D2AB File Offset: 0x0002B4AB
		// (set) Token: 0x060009FD RID: 2557 RVA: 0x0002D2B3 File Offset: 0x0002B4B3
		public string DomainName { get; set; }

		// Token: 0x060009FE RID: 2558 RVA: 0x0002D2BC File Offset: 0x0002B4BC
		public CharacterMergeTransformationProvider()
		{
			this.MaxMergableTokenLength = 1;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0002D2CB File Offset: 0x0002B4CB
		public ISession CreateSession()
		{
			return new CharacterMergeTransformationProvider.Session();
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0002D2D2 File Offset: 0x0002B4D2
		public void Initialize(IDomainManager domainManager, string domainName)
		{
			this.DomainName = domainName;
			this.m_domainId = domainManager.GetDomainId(domainName);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002D2E8 File Offset: 0x0002B4E8
		public void Match(ISession session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSeq, out ArraySegment<TransformationMatch> transformationMatchList)
		{
			if (!tokenIdProvider.SupportsGetToken)
			{
				throw new InvalidOperationException("TokenIdProvider must support reverse mapping to string in order to use the CharacterMergeTransformationProvider.");
			}
			CharacterMergeTransformationProvider.Session session2 = (CharacterMergeTransformationProvider.Session)session;
			session.Reset();
			int num = 0;
			List<int> fromSequence = session2.m_fromSequence;
			for (int i = 0; i < tokenSeq.Count; i++)
			{
				if (tokenIdProvider.GetDomainId(tokenSeq[i]) == this.m_domainId)
				{
					StringExtent token = tokenIdProvider.GetToken(tokenSeq[i]);
					if (token.Length <= this.MaxMergableTokenLength)
					{
						if (session2.m_concatStrBuf.Length < num + token.Length)
						{
							Array.Resize<char>(ref session2.m_concatStrBuf, 2 * (num + token.Length));
						}
						Array.ConstrainedCopy(token.Array, token.Offset, session2.m_concatStrBuf, num, token.Length);
						num += token.Length;
						fromSequence.Add(tokenSeq[i]);
					}
				}
				if (fromSequence.Count > 1 && (i >= tokenSeq.Count - 1 || tokenIdProvider.GetDomainId(tokenSeq[i + 1]) != this.m_domainId || tokenIdProvider.GetToken(tokenSeq[i + 1]).Length > this.MaxMergableTokenLength))
				{
					int orCreateTokenId = tokenIdProvider.GetOrCreateTokenId(new StringExtent(session2.m_concatStrBuf, 0, num), this.m_domainId);
					ArraySegment<int> arraySegment = session2.m_intAllocator.New(fromSequence.Count);
					for (int j = 0; j < fromSequence.Count; j++)
					{
						arraySegment.Array[arraySegment.Offset + j] = fromSequence[j];
					}
					ArraySegment<int> arraySegment2 = session2.m_intAllocator.New(1);
					arraySegment2.Array[arraySegment2.Offset] = orCreateTokenId;
					session2.m_tranMatchListBuilder.Add(new TransformationMatch
					{
						Position = i + 1 - fromSequence.Count,
						Transformation = new Transformation
						{
							Type = TransformationType.TokenMerge,
							From = arraySegment,
							To = arraySegment2,
							Metadata = default(ArraySegment<byte>)
						}
					});
					num = 0;
					fromSequence.Clear();
				}
			}
			transformationMatchList = session2.m_tranMatchListBuilder;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0002D522 File Offset: 0x0002B722
		public IEnumerable<Transformation> Transformations(ITokenIdProvider tokenIdProvider)
		{
			yield break;
		}

		// Token: 0x040003D1 RID: 977
		private int m_domainId;

		// Token: 0x02000190 RID: 400
		private class Session : ISession
		{
			// Token: 0x06000D51 RID: 3409 RVA: 0x0003901A File Offset: 0x0003721A
			public Session()
			{
				this.m_concatStrBuf = new char[0];
			}

			// Token: 0x06000D52 RID: 3410 RVA: 0x0003904F File Offset: 0x0003724F
			public void Reset()
			{
				this.m_intAllocator.Reset();
				this.m_tranMatchListBuilder.Reset();
				this.m_fromSequence.Clear();
			}

			// Token: 0x0400067A RID: 1658
			public BlockedSegmentArray<int> m_intAllocator = new BlockedSegmentArray<int>();

			// Token: 0x0400067B RID: 1659
			public ArraySegmentBuilder<TransformationMatch> m_tranMatchListBuilder = new ArraySegmentBuilder<TransformationMatch>();

			// Token: 0x0400067C RID: 1660
			public List<int> m_fromSequence = new List<int>();

			// Token: 0x0400067D RID: 1661
			public char[] m_concatStrBuf;
		}
	}
}
