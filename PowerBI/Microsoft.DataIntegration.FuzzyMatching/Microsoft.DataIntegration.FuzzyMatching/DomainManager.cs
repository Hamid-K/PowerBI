using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000063 RID: 99
	[Serializable]
	public sealed class DomainManager : IDomainManager, IDomainIdProvider, ITokenToClusterMap, IMemoryUsage, IRowsetConsumer, IObjectReferenceContainer
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00012220 File Offset: 0x00010420
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x00012228 File Offset: 0x00010428
		public string Name { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00012231 File Offset: 0x00010431
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x00012239 File Offset: 0x00010439
		public string ReferenceRowsetName { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00012242 File Offset: 0x00010442
		// (set) Token: 0x060003DA RID: 986 RVA: 0x0001224A File Offset: 0x0001044A
		public ITokenIdProvider TokenIdProvider { get; set; }

		// Token: 0x060003DB RID: 987 RVA: 0x00012254 File Offset: 0x00010454
		public DomainManager()
		{
			this.m_domainIdProvider = new DomainIdProvider();
			this.TokenIdProvider = new OneToOneTokenIdProvider();
			foreach (string text in this.m_domainIdProvider.DomainNames)
			{
				this.m_domains.Add(new DomainInstance(this.m_domainIdProvider.GetDomainId(text), text));
			}
			this.m_tokenClusterMap = new FastIntToIntHash(0.5f);
			this.ReferenceRowsetName = "default";
			this.m_referenceRowsetSinkPass1 = new DomainManager.ReferenceRowsetSink
			{
				Name = "ReferenceRowsetPass1",
				DomainManager = this
			};
			this.m_referenceRowsetSinkPass2 = new DomainManager.ReferenceRowsetSink
			{
				Name = "ReferenceRowsetPass2",
				DomainManager = this
			};
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00012338 File Offset: 0x00010538
		public DomainManager(XmlReader domainManagerXml, IRowsetManager rowsetManager)
			: this()
		{
			FuzzyLookupXmlBuilder.PopulateDomainManager(this, domainManagerXml, rowsetManager);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00012348 File Offset: 0x00010548
		public void AcquireReferences()
		{
			if (!this.m_referencesHaveBeenAcquired)
			{
				if (this.TokenIdProvider is IObjectReferenceContainer)
				{
					(this.TokenIdProvider as IObjectReferenceContainer).AcquireReferences();
				}
				foreach (DomainInstance domainInstance in this.m_domains)
				{
					domainInstance.AcquireReferences();
				}
				this.m_referencesHaveBeenAcquired = true;
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000123C4 File Offset: 0x000105C4
		public void UpdateReferences()
		{
			if (this.TokenIdProvider is IObjectReferenceContainer)
			{
				(this.TokenIdProvider as IObjectReferenceContainer).UpdateReferences();
			}
			foreach (DomainInstance domainInstance in this.m_domains)
			{
				domainInstance.UpdateReferences();
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00012434 File Offset: 0x00010634
		public void ReleaseReferences()
		{
			if (this.m_referencesHaveBeenAcquired)
			{
				if (this.TokenIdProvider is IObjectReferenceContainer)
				{
					(this.TokenIdProvider as IObjectReferenceContainer).ReleaseReferences();
				}
				foreach (DomainInstance domainInstance in this.m_domains)
				{
					domainInstance.ReleaseReferences();
				}
				this.m_referencesHaveBeenAcquired = false;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x000124B0 File Offset: 0x000106B0
		public IList<IRowsetSink> RowsetSinks
		{
			get
			{
				return new IRowsetSink[] { this.m_referenceRowsetSinkPass1, this.m_referenceRowsetSinkPass2 };
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000124CA File Offset: 0x000106CA
		public void RequestRowsets(IRowsetDistributor rowsetDistributor)
		{
			if (!string.IsNullOrEmpty(this.ReferenceRowsetName))
			{
				rowsetDistributor.RequestRowset(this.ReferenceRowsetName, this.m_referenceRowsetSinkPass1, 1);
				rowsetDistributor.RequestRowset(this.ReferenceRowsetName, this.m_referenceRowsetSinkPass1, 2);
			}
		}

		// Token: 0x170000E5 RID: 229
		public DomainInstance this[string domainName]
		{
			get
			{
				return this.m_domains[this.GetDomainId(domainName)];
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00012514 File Offset: 0x00010714
		public bool DomainExists(string domainName)
		{
			int num;
			return this.TryGetDomainId(domainName, out num);
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0001252C File Offset: 0x0001072C
		public long MemoryUsage
		{
			get
			{
				long num = this.m_tokenClusterMap.MemoryUsage;
				if (this.m_domainIdProvider is IMemoryUsage)
				{
					num += (this.m_domainIdProvider as IMemoryUsage).MemoryUsage;
				}
				if (this.TokenIdProvider is IMemoryUsage)
				{
					num += (this.TokenIdProvider as IMemoryUsage).MemoryUsage;
				}
				return num;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00012586 File Offset: 0x00010786
		public IEnumerable<string> DomainNames
		{
			get
			{
				foreach (DomainInstance domainInstance in this.m_domains)
				{
					yield return domainInstance.Name;
				}
				List<DomainInstance>.Enumerator enumerator = default(List<DomainInstance>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00012598 File Offset: 0x00010798
		[Obsolete("Use domainManager[domainName].(Left/Right)TransformationProvider.Add() instead.")]
		public void AddTransformationProvider(string domainName, JoinSide joinSide, ITransformationProvider provider)
		{
			if (joinSide == JoinSide.Left)
			{
				this.m_domains[this.GetDomainId(domainName)].LeftTransformationProvider.Add(provider);
				return;
			}
			if (joinSide == JoinSide.Right)
			{
				this.m_domains[this.GetDomainId(domainName)].RightTransformationProvider.Add(provider);
				return;
			}
			throw new ArgumentException();
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000125F0 File Offset: 0x000107F0
		[Obsolete("Use domainManager[domainName].(Left/Right)TransformationProvider.Remove() instead.")]
		public void RemoveTransformationProvider(string domainName, JoinSide joinSide, ITransformationProvider provider)
		{
			if (joinSide == JoinSide.Left)
			{
				this.m_domains[this.GetDomainId(domainName)].LeftTransformationProvider.Remove(provider);
				return;
			}
			if (joinSide == JoinSide.Right)
			{
				this.m_domains[this.GetDomainId(domainName)].RightTransformationProvider.Remove(provider);
				return;
			}
			throw new ArgumentException();
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00012646 File Offset: 0x00010846
		[Obsolete]
		public IEnumerable<ITransformationProvider> TransformationProviders(string domainName, JoinSide joinSide)
		{
			if (joinSide == JoinSide.Left)
			{
				return this.LeftTransformationProviders(domainName);
			}
			if (joinSide == JoinSide.Right)
			{
				return this.RightTransformationProviders(domainName);
			}
			throw new ArgumentException();
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00012665 File Offset: 0x00010865
		public IEnumerable<ITransformationProvider> LeftTransformationProviders(string domainName)
		{
			foreach (ITransformationProvider transformationProvider in this.m_domains[this.GetDomainId(domainName)].LeftTransformationProvider.TransformationProviders)
			{
				yield return transformationProvider;
			}
			IEnumerator<ITransformationProvider> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001267C File Offset: 0x0001087C
		public IEnumerable<ITransformationProvider> RightTransformationProviders(string domainName)
		{
			foreach (ITransformationProvider transformationProvider in this.m_domains[this.GetDomainId(domainName)].RightTransformationProvider.TransformationProviders)
			{
				yield return transformationProvider;
			}
			IEnumerator<ITransformationProvider> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00012693 File Offset: 0x00010893
		public IEnumerable<IPairSpecificTransformationProvider> PairSpecificTransformationProviders(string domainName)
		{
			foreach (IPairSpecificTransformationProvider pairSpecificTransformationProvider in this.m_domains[this.GetDomainId(domainName)].PairSpecificTransformationProvider.TransformationProviders)
			{
				yield return pairSpecificTransformationProvider;
			}
			IEnumerator<IPairSpecificTransformationProvider> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000126AA File Offset: 0x000108AA
		void ITokenToClusterMap.AddTokenClusterMapping(int token, int cluster)
		{
			if (cluster == 0)
			{
				throw new ArgumentException("ClusterId must be greater than zero.");
			}
			this.m_tokenClusterMap.Add(token, cluster);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000126C8 File Offset: 0x000108C8
		int ITokenToClusterMap.GetTokenClusterMapping(int token)
		{
			int num;
			if (this.m_tokenClusterMap.Count > 0 && this.m_tokenClusterMap.TryGetValue(token, out num))
			{
				return num;
			}
			return token;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000126F8 File Offset: 0x000108F8
		internal void ComputeTokenClusterMappingForDomains(IEnumerable<string> domainNames)
		{
			foreach (string text in domainNames)
			{
				int domainId = this.GetDomainId(text);
				if (!this.GetDomain(domainId).TokenClusterHasBeenComputed)
				{
					TransformationProviderAggregator transformationProviderAggregator = new TransformationProviderAggregator();
					transformationProviderAggregator.Add(this.GetLeftTransformationProvider(text));
					transformationProviderAggregator.Add(this.GetRightTransformationProvider(text));
					GreedyTokenClusterProvider greedyTokenClusterProvider = new GreedyTokenClusterProvider();
					greedyTokenClusterProvider.Reset();
					foreach (Transformation transformation in transformationProviderAggregator.Transformations(this.TokenIdProvider))
					{
						greedyTokenClusterProvider.AddRule(transformation);
					}
					greedyTokenClusterProvider.Cluster(this.TokenIdProvider, domainId, this);
					this.GetDomain(domainId).TokenClusterHasBeenComputed = true;
				}
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x000127E8 File Offset: 0x000109E8
		internal IEnumerable<string> Domains
		{
			get
			{
				foreach (DomainInstance domainInstance in this.m_domains)
				{
					yield return domainInstance.Name;
				}
				List<DomainInstance>.Enumerator enumerator = default(List<DomainInstance>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x000127F8 File Offset: 0x000109F8
		internal DomainInstance GetDomain(int id)
		{
			return this.m_domains[id];
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00012806 File Offset: 0x00010A06
		public IRecordTokenizer GetTokenizer(int domainId)
		{
			return this.m_domains[domainId].Tokenizer;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00012819 File Offset: 0x00010A19
		public ITransformationProvider GetLeftTransformationProvider(string domainName)
		{
			return this.GetLeftTransformationProvider(this.GetDomainId(domainName));
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00012828 File Offset: 0x00010A28
		public ITransformationProvider GetRightTransformationProvider(string domainName)
		{
			return this.GetRightTransformationProvider(this.GetDomainId(domainName));
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00012837 File Offset: 0x00010A37
		public IPairSpecificTransformationProvider GetPairSpecificTransformationProvider(string domainName)
		{
			return this.GetPairSpecificTransformationProvider(this.GetDomainId(domainName));
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00012846 File Offset: 0x00010A46
		public ITransformationProvider GetLeftTransformationProvider(int domainId)
		{
			return this.m_domains[domainId].LeftTransformationProvider;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00012859 File Offset: 0x00010A59
		public ITransformationProvider GetRightTransformationProvider(int domainId)
		{
			return this.m_domains[domainId].RightTransformationProvider;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001286C File Offset: 0x00010A6C
		public IPairSpecificTransformationProvider GetPairSpecificTransformationProvider(int domainId)
		{
			return this.m_domains[domainId].PairSpecificTransformationProvider;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001287F File Offset: 0x00010A7F
		public ITokenWeightProvider GetTokenWeightProvider(int domainId)
		{
			return this.m_domains[domainId].TokenWeightProvider;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00012892 File Offset: 0x00010A92
		public IRecordTokenizer GetTokenizer(string domainName)
		{
			return this.m_domains[this.GetDomainId(domainName)].Tokenizer;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000128AB File Offset: 0x00010AAB
		public ITokenWeightProvider GetTokenWeightProvider(string domainName)
		{
			return this.m_domains[this.GetDomainId(domainName)].TokenWeightProvider;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000128C4 File Offset: 0x00010AC4
		public void AddDomain(string domainName, int domainId)
		{
			this.m_domains.Add(new DomainInstance(domainId, domainName));
			this.m_domainIdProvider.AddDomain(domainName, domainId);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000128E5 File Offset: 0x00010AE5
		public void DropDomain(string domainName)
		{
			this.m_domainIdProvider.DropDomain(domainName);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000128F4 File Offset: 0x00010AF4
		public int CreateDomain(string domainName)
		{
			using (List<DomainInstance>.Enumerator enumerator = this.m_domains.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Name.Equals(domainName))
					{
						throw new InvalidOperationException(string.Format(StringResources.DomainAlreadyExists, domainName));
					}
				}
			}
			int count = this.m_domains.Count;
			this.AddDomain(domainName, count);
			return count;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00012974 File Offset: 0x00010B74
		public void SetTokenizer(string domainName, IRecordTokenizer tokenizer)
		{
			this.m_domains[this.GetDomainId(domainName)].Tokenizer = tokenizer;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001298E File Offset: 0x00010B8E
		public void SetTokenWeightProvider(string domainName, ITokenWeightProvider tokenWeightProvider)
		{
			this.m_domains[this.GetDomainId(domainName)].TokenWeightProvider = tokenWeightProvider;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000129A8 File Offset: 0x00010BA8
		public string GetDomainName(int domainId)
		{
			return this.m_domains[domainId].Name;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000129BC File Offset: 0x00010BBC
		public bool TryGetDomainId(string domainName, out int domainId)
		{
			domainId = -1;
			foreach (DomainInstance domainInstance in this.m_domains)
			{
				if (domainInstance.Name.Equals(domainName))
				{
					domainId = domainInstance.Id;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00012A28 File Offset: 0x00010C28
		public int GetDomainId(string domainName)
		{
			int num;
			if (!this.TryGetDomainId(domainName, out num))
			{
				throw new InvalidOperationException(string.Format(StringResources.DomainNotFound, domainName));
			}
			return num;
		}

		// Token: 0x0400014A RID: 330
		private DomainManager.ReferenceRowsetSink m_referenceRowsetSinkPass1;

		// Token: 0x0400014B RID: 331
		private DomainManager.ReferenceRowsetSink m_referenceRowsetSinkPass2;

		// Token: 0x0400014C RID: 332
		private List<DomainInstance> m_domains = new List<DomainInstance>();

		// Token: 0x0400014D RID: 333
		private IDomainIdProvider m_domainIdProvider;

		// Token: 0x0400014E RID: 334
		private FastIntToIntHash m_tokenClusterMap;

		// Token: 0x04000150 RID: 336
		[NonSerialized]
		private bool m_referencesHaveBeenAcquired;

		// Token: 0x0200014C RID: 332
		[Serializable]
		private class ReferenceRowsetSink : IRowsetSink, IRecordUpdate, IRecordContextUpdate
		{
			// Token: 0x1700025E RID: 606
			// (get) Token: 0x06000C80 RID: 3200 RVA: 0x00036877 File Offset: 0x00034A77
			// (set) Token: 0x06000C81 RID: 3201 RVA: 0x0003687F File Offset: 0x00034A7F
			public string Name { get; set; }

			// Token: 0x06000C82 RID: 3202 RVA: 0x00036888 File Offset: 0x00034A88
			public IUpdateContext BeginUpdate()
			{
				return this.BeginUpdate(null);
			}

			// Token: 0x06000C83 RID: 3203 RVA: 0x00036891 File Offset: 0x00034A91
			public IUpdateContext BeginUpdate(DataTable schemaTable)
			{
				return new DomainManager.ReferenceRowsetSink.UpdateContext();
			}

			// Token: 0x06000C84 RID: 3204 RVA: 0x00036898 File Offset: 0x00034A98
			public void EndUpdate(IUpdateContext context)
			{
			}

			// Token: 0x06000C85 RID: 3205 RVA: 0x0003689A File Offset: 0x00034A9A
			public void AddRecordContext(IUpdateContext _updateContext, RecordContext recordContext)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000C86 RID: 3206 RVA: 0x000368A1 File Offset: 0x00034AA1
			public void RemoveRecordContext(IUpdateContext _updateContext, RecordContext recordContext)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000C87 RID: 3207 RVA: 0x000368A8 File Offset: 0x00034AA8
			public void AddRecord(IUpdateContext _updateContext, IDataRecord record)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000C88 RID: 3208 RVA: 0x000368AF File Offset: 0x00034AAF
			public void RemoveRecord(IUpdateContext _updateContext, IDataRecord record)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0400057A RID: 1402
			public DomainManager DomainManager;

			// Token: 0x020001BD RID: 445
			private class UpdateContext : IUpdateContext
			{
			}
		}
	}
}
