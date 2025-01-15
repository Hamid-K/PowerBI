using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000119 RID: 281
	[Serializable]
	public sealed class DistinctTokenProvider : ISessionable, IDeserializationCallback
	{
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x00033643 File Offset: 0x00031843
		// (set) Token: 0x06000BB8 RID: 3000 RVA: 0x0003364B File Offset: 0x0003184B
		public string DomainName { get; set; }

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00033654 File Offset: 0x00031854
		public DistinctTokenProvider(RecordBinding recordBinding, string domainName, IRecordTokenizer tokenizer, ITransformationProvider transformationProvider)
		{
			this.m_tokenizer = tokenizer;
			this.m_transformationProvider = transformationProvider;
			this.m_recordBinding = recordBinding;
			this.DomainName = domainName;
			this.m_session = new DistinctTokenProvider.Session(this.DomainName, this.m_recordBinding, this.m_tokenizer, this.m_transformationProvider);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x000336A7 File Offset: 0x000318A7
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.m_session = new DistinctTokenProvider.Session(this.DomainName, this.m_recordBinding, this.m_tokenizer, this.m_transformationProvider);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x000336CC File Offset: 0x000318CC
		public ISession CreateSession()
		{
			return new DistinctTokenProvider.Session();
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x000336D4 File Offset: 0x000318D4
		[Obsolete("Use GetDistinctTokens(Session")]
		public HashSet<int> GetDistinctTokens(ITokenIdProvider tokenIdProvider, int domainId, IDataRecord record)
		{
			DistinctTokenProvider.Session session = this.m_session;
			session.m_intAllocator.Reset();
			session.m_tokenizerContext.Reset();
			TokenSequence tokenSequence = TokenSequence.Create(this.m_tokenizer.Tokenize(session.m_tokenizerContext, record), domainId, tokenIdProvider, session.m_tokenIdSegmentBuilder, session.m_intAllocator);
			return this.GetDistinctTokens(session, tokenIdProvider, tokenSequence, this.m_transformationProvider);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00033734 File Offset: 0x00031934
		[Obsolete("Use GetDistinctTokens(Session")]
		public HashSet<int> GetDistinctTokens(ITokenIdProvider tokenIdProvider, TokenSequence sequence)
		{
			DistinctTokenProvider.Session session = this.m_session;
			return this.GetDistinctTokens(session, tokenIdProvider, sequence, this.m_transformationProvider);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00033758 File Offset: 0x00031958
		private HashSet<int> GetDistinctTokens(DistinctTokenProvider.Session session, ITokenIdProvider tokenIdProvider, TokenSequence sequence, ITransformationProvider transformationProvider)
		{
			session.m_distinctTokens.Clear();
			for (int i = 0; i < sequence.Count; i++)
			{
				if (!session.m_distinctTokens.Contains(sequence[i]))
				{
					session.m_distinctTokens.Add(sequence[i]);
				}
			}
			if (this.m_transformationProvider != null)
			{
				ArraySegment<TransformationMatch> arraySegment;
				this.m_transformationProvider.Match(session.m_transformationProviderSession, tokenIdProvider, sequence, out arraySegment);
				for (int j = 0; j < arraySegment.Count; j++)
				{
					TokenSequence to = arraySegment.Array[arraySegment.Offset + j].Transformation.To;
					for (int k = 0; k < to.Count; k++)
					{
						if (!session.m_distinctTokens.Contains(to[k]))
						{
							session.m_distinctTokens.Add(to[k]);
						}
					}
				}
				session.m_transformationProviderSession.Reset();
			}
			return session.m_distinctTokens;
		}

		// Token: 0x04000473 RID: 1139
		private IRecordTokenizer m_tokenizer;

		// Token: 0x04000474 RID: 1140
		private ITransformationProvider m_transformationProvider;

		// Token: 0x04000475 RID: 1141
		private RecordBinding m_recordBinding;

		// Token: 0x04000476 RID: 1142
		[NonSerialized]
		private DistinctTokenProvider.Session m_session;

		// Token: 0x020001B9 RID: 441
		private class Session : ISession
		{
			// Token: 0x06000E43 RID: 3651 RVA: 0x0003C6B3 File Offset: 0x0003A8B3
			public Session()
			{
			}

			// Token: 0x06000E44 RID: 3652 RVA: 0x0003C6E8 File Offset: 0x0003A8E8
			public Session(string domainName, RecordBinding binding, IRecordTokenizer tokenizer, ITransformationProvider transformationProvider)
				: this()
			{
				tokenizer.Prepare(binding.Schema, binding.GetDomainBinding(domainName), out this.m_tokenizerContext);
				if ((transformationProvider != null) & (transformationProvider is ISessionable))
				{
					this.m_transformationProviderSession = (transformationProvider as ISessionable).CreateSession();
				}
			}

			// Token: 0x06000E45 RID: 3653 RVA: 0x0003C738 File Offset: 0x0003A938
			public void Reset()
			{
				this.m_tokenIdSegmentBuilder.Reset();
				this.m_intAllocator.Reset();
				this.m_distinctTokens.Clear();
				this.m_tokenizerContext.Reset();
				this.m_transformationProviderSession.Reset();
			}

			// Token: 0x0400073E RID: 1854
			public ArraySegmentBuilder<int> m_tokenIdSegmentBuilder = new ArraySegmentBuilder<int>();

			// Token: 0x0400073F RID: 1855
			public BlockedSegmentArray<int> m_intAllocator = new BlockedSegmentArray<int>();

			// Token: 0x04000740 RID: 1856
			public ArraySegmentBuilder<TransformationMatch> m_tranMatchList = new ArraySegmentBuilder<TransformationMatch>();

			// Token: 0x04000741 RID: 1857
			public HashSet<int> m_distinctTokens = new HashSet<int>();

			// Token: 0x04000742 RID: 1858
			public TokenizerContext m_tokenizerContext;

			// Token: 0x04000743 RID: 1859
			public ISession m_transformationProviderSession;
		}
	}
}
