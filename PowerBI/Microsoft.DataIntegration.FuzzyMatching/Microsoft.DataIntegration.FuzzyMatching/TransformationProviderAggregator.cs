using System;
using System.Collections.Generic;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000114 RID: 276
	[Serializable]
	public sealed class TransformationProviderAggregator : ITransformationProvider, ISessionable, IObjectReferenceContainer
	{
		// Token: 0x06000B83 RID: 2947 RVA: 0x00032D5A File Offset: 0x00030F5A
		public TransformationProviderAggregator()
		{
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00032D6D File Offset: 0x00030F6D
		public TransformationProviderAggregator(IEnumerable<ITransformationProvider> providers)
		{
			this.Add(providers);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00032D88 File Offset: 0x00030F88
		public ISession CreateSession()
		{
			TransformationProviderAggregator.Session session = new TransformationProviderAggregator.Session();
			session.Providers = new List<ITransformationProvider>(this.Providers);
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

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x00032E0A File Offset: 0x0003100A
		public IEnumerable<ITransformationProvider> TransformationProviders
		{
			get
			{
				foreach (ITransformationProvider transformationProvider in this.Providers)
				{
					yield return transformationProvider;
				}
				List<ITransformationProvider>.Enumerator enumerator = default(List<ITransformationProvider>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00032E1C File Offset: 0x0003101C
		public void Add(IEnumerable<ITransformationProvider> providers)
		{
			foreach (ITransformationProvider transformationProvider in providers)
			{
				this.Add(transformationProvider);
			}
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00032E64 File Offset: 0x00031064
		public void Add(ITransformationProvider transformationProvider)
		{
			if (transformationProvider != null)
			{
				this.Providers.Add(transformationProvider);
			}
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00032E75 File Offset: 0x00031075
		public void Remove(ITransformationProvider transformationProvider)
		{
			if (transformationProvider != null)
			{
				this.Providers.Remove(transformationProvider);
			}
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00032E88 File Offset: 0x00031088
		public void Match(ISession _session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSeq, out ArraySegment<TransformationMatch> transformationMatchList)
		{
			TransformationProviderAggregator.Session session = _session as TransformationProviderAggregator.Session;
			session.Reset();
			for (int i = 0; i < session.Providers.Count; i++)
			{
				session.Providers[i].Match(session.m_sessions[i], tokenIdProvider, tokenSeq, out transformationMatchList);
				session.m_tranMatchListBuilder.Add(transformationMatchList);
			}
			transformationMatchList = session.m_tranMatchListBuilder;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00032EF9 File Offset: 0x000310F9
		public IEnumerable<Transformation> Transformations(ITokenIdProvider tokenIdProvider)
		{
			foreach (ITransformationProvider transformationProvider in this.Providers)
			{
				IEnumerable<Transformation> enumerable = null;
				try
				{
					enumerable = transformationProvider.Transformations(tokenIdProvider);
				}
				catch (NotImplementedException)
				{
				}
				if (enumerable != null)
				{
					foreach (Transformation transformation in enumerable)
					{
						yield return transformation;
					}
					IEnumerator<Transformation> enumerator2 = null;
				}
			}
			List<ITransformationProvider>.Enumerator enumerator = default(List<ITransformationProvider>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00032F10 File Offset: 0x00031110
		public void AcquireReferences()
		{
			foreach (ITransformationProvider transformationProvider in this.Providers)
			{
				if (transformationProvider is IObjectReferenceContainer)
				{
					(transformationProvider as IObjectReferenceContainer).AcquireReferences();
				}
			}
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00032F70 File Offset: 0x00031170
		public void UpdateReferences()
		{
			foreach (ITransformationProvider transformationProvider in this.Providers)
			{
				if (transformationProvider is IObjectReferenceContainer)
				{
					(transformationProvider as IObjectReferenceContainer).UpdateReferences();
				}
			}
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00032FD0 File Offset: 0x000311D0
		public void ReleaseReferences()
		{
			foreach (ITransformationProvider transformationProvider in this.Providers)
			{
				if (transformationProvider is IObjectReferenceContainer)
				{
					(transformationProvider as IObjectReferenceContainer).ReleaseReferences();
				}
			}
		}

		// Token: 0x04000463 RID: 1123
		public List<ITransformationProvider> Providers = new List<ITransformationProvider>();

		// Token: 0x020001B2 RID: 434
		private sealed class Session : ISession
		{
			// Token: 0x06000E11 RID: 3601 RVA: 0x0003BCF8 File Offset: 0x00039EF8
			public void Reset()
			{
				for (int i = 0; i < this.m_sessions.Length; i++)
				{
					if (this.m_sessions[i] != null)
					{
						this.m_sessions[i].Reset();
					}
				}
				this.m_tranMatchListBuilder.Reset();
				this.m_tranMatchAllocator.Reset();
			}

			// Token: 0x0400071F RID: 1823
			public List<ITransformationProvider> Providers;

			// Token: 0x04000720 RID: 1824
			public ISession[] m_sessions;

			// Token: 0x04000721 RID: 1825
			public ArraySegmentBuilder<TransformationMatch> m_tranMatchListBuilder = new ArraySegmentBuilder<TransformationMatch>();

			// Token: 0x04000722 RID: 1826
			public BlockedSegmentArray<TransformationMatch> m_tranMatchAllocator = new BlockedSegmentArray<TransformationMatch>();
		}
	}
}
