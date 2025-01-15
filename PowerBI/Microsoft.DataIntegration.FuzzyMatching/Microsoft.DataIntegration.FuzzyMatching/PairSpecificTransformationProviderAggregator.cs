using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000115 RID: 277
	[Serializable]
	public sealed class PairSpecificTransformationProviderAggregator : IPairSpecificTransformationProvider, ISessionable, IObjectReferenceContainer
	{
		// Token: 0x06000B8F RID: 2959 RVA: 0x00033030 File Offset: 0x00031230
		public ISession CreateSession()
		{
			PairSpecificTransformationProviderAggregator.Session session = new PairSpecificTransformationProviderAggregator.Session();
			session.Providers = new List<IPairSpecificTransformationProvider>(this.Providers);
			session.m_sessions = new ISession[this.Providers.Count];
			for (int i = 0; i < this.Providers.Count; i++)
			{
				if (this.Providers[i] is ISessionable)
				{
					session.m_sessions[i] = (this.Providers[i] as ISessionable).CreateSession();
				}
			}
			return session;
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x000330B2 File Offset: 0x000312B2
		public IEnumerable<IPairSpecificTransformationProvider> TransformationProviders
		{
			get
			{
				foreach (IPairSpecificTransformationProvider pairSpecificTransformationProvider in this.Providers)
				{
					yield return pairSpecificTransformationProvider;
				}
				List<IPairSpecificTransformationProvider>.Enumerator enumerator = default(List<IPairSpecificTransformationProvider>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x000330C2 File Offset: 0x000312C2
		public void Add(IPairSpecificTransformationProvider transformationProvider)
		{
			if (transformationProvider != null)
			{
				this.Providers.Add(transformationProvider);
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000330D3 File Offset: 0x000312D3
		public void Remove(IPairSpecificTransformationProvider transformationProvider)
		{
			if (transformationProvider != null)
			{
				this.Providers.Remove(transformationProvider);
			}
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x000330E8 File Offset: 0x000312E8
		public void Match(ISession _session, ITokenIdProvider tokenIdProvider, IDataRecord leftRecord, IDataRecord rightRecord, RecordBinding leftBinding, RecordBinding rightBinding, TokenSequence leftTokenSeq, TokenSequence rightTokenSeq, out ArraySegment<TransformationMatch> leftTransformationMatchList, out ArraySegment<TransformationMatch> rightTransformationMatchList)
		{
			PairSpecificTransformationProviderAggregator.Session session = _session as PairSpecificTransformationProviderAggregator.Session;
			session.Reset();
			for (int i = 0; i < session.Providers.Count; i++)
			{
				session.Providers[i].Match(session.m_sessions[i], tokenIdProvider, leftRecord, rightRecord, leftBinding, rightBinding, leftTokenSeq, rightTokenSeq, out leftTransformationMatchList, out rightTransformationMatchList);
				session.m_leftTranMatchListBuilder.Add(leftTransformationMatchList);
				session.m_rightTranMatchListBuilder.Add(rightTransformationMatchList);
			}
			leftTransformationMatchList = session.m_leftTranMatchListBuilder;
			rightTransformationMatchList = session.m_rightTranMatchListBuilder;
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0003318C File Offset: 0x0003138C
		public void AcquireReferences()
		{
			foreach (IPairSpecificTransformationProvider pairSpecificTransformationProvider in this.Providers)
			{
				if (pairSpecificTransformationProvider is IObjectReferenceContainer)
				{
					(pairSpecificTransformationProvider as IObjectReferenceContainer).AcquireReferences();
				}
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x000331EC File Offset: 0x000313EC
		public void UpdateReferences()
		{
			foreach (IPairSpecificTransformationProvider pairSpecificTransformationProvider in this.Providers)
			{
				if (pairSpecificTransformationProvider is IObjectReferenceContainer)
				{
					(pairSpecificTransformationProvider as IObjectReferenceContainer).UpdateReferences();
				}
			}
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0003324C File Offset: 0x0003144C
		public void ReleaseReferences()
		{
			foreach (IPairSpecificTransformationProvider pairSpecificTransformationProvider in this.Providers)
			{
				if (pairSpecificTransformationProvider is IObjectReferenceContainer)
				{
					(pairSpecificTransformationProvider as IObjectReferenceContainer).ReleaseReferences();
				}
			}
		}

		// Token: 0x04000464 RID: 1124
		public List<IPairSpecificTransformationProvider> Providers = new List<IPairSpecificTransformationProvider>();

		// Token: 0x020001B5 RID: 437
		private sealed class Session : ISession
		{
			// Token: 0x06000E26 RID: 3622 RVA: 0x0003C12C File Offset: 0x0003A32C
			public void Reset()
			{
				foreach (ISession session in this.m_sessions)
				{
					if (session != null)
					{
						session.Reset();
					}
				}
				this.m_leftTranMatchListBuilder.Reset();
				this.m_rightTranMatchListBuilder.Reset();
				this.m_tranMatchAllocator.Reset();
			}

			// Token: 0x04000730 RID: 1840
			public List<IPairSpecificTransformationProvider> Providers;

			// Token: 0x04000731 RID: 1841
			public ISession[] m_sessions;

			// Token: 0x04000732 RID: 1842
			public ArraySegmentBuilder<TransformationMatch> m_leftTranMatchListBuilder = new ArraySegmentBuilder<TransformationMatch>();

			// Token: 0x04000733 RID: 1843
			public ArraySegmentBuilder<TransformationMatch> m_rightTranMatchListBuilder = new ArraySegmentBuilder<TransformationMatch>();

			// Token: 0x04000734 RID: 1844
			public BlockedSegmentArray<TransformationMatch> m_tranMatchAllocator = new BlockedSegmentArray<TransformationMatch>();
		}
	}
}
