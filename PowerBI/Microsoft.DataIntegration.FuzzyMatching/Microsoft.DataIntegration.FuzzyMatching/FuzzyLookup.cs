using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Diagnostics;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200004A RID: 74
	[Serializable]
	public sealed class FuzzyLookup : FuzzyLookupDefinition, IDisposable, IRowsetConsumer
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000D883 File Offset: 0x0000BA83
		[Obsolete]
		public FuzzyLookupDefinition IndexDefinition
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000D886 File Offset: 0x0000BA86
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0000D88E File Offset: 0x0000BA8E
		public DomainManager DomainManager { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000D897 File Offset: 0x0000BA97
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0000D89F File Offset: 0x0000BA9F
		public new IFuzzyLookupStateManager StateManager { get; private set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
		public IStatistics Statistics
		{
			get
			{
				return this.m_statistics;
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000D8B0 File Offset: 0x0000BAB0
		public FuzzyLookup(DomainManager domainManager, FuzzyLookupDefinition indexDefinition)
			: base(indexDefinition)
		{
			this.m_referenceRowsetSink = new FuzzyLookup.ReferenceRowsetSink
			{
				Name = "ReferenceRowset",
				fuzzyLookup = this
			};
			IFuzzyLookupStateManager fuzzyLookupStateManager = indexDefinition.StateManager.CreateInstance() as IFuzzyLookupStateManager;
			if (fuzzyLookupStateManager is IFuzzyLookupStateManagerInitialize)
			{
				(fuzzyLookupStateManager as IFuzzyLookupStateManagerInitialize).Initialize(indexDefinition, null);
			}
			this.Initialize(domainManager, fuzzyLookupStateManager);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000D910 File Offset: 0x0000BB10
		public FuzzyLookup(FuzzyLookupDefinition indexDefinition, DomainManager domainManager, IRowsetManager rowsetManager)
			: this(domainManager, indexDefinition)
		{
			using (ConnectionManager connectionManager = new ConnectionManager(rowsetManager.Connections))
			{
				if (base.RecordBinding == null || base.RecordBinding.Schema == null)
				{
					string text = ((base.RecordBinding == null) ? base.ReferenceRowsetName : base.RecordBinding.RowsetName);
					if (!string.IsNullOrEmpty(text))
					{
						throw new Exception("Must define the rowsetName property on the RecordBinding or the ReferenceRowsetName on the FuzzyLookup.");
					}
					base.RecordBinding = rowsetManager.GetRecordBinding(text, connectionManager);
				}
				RowsetDistributor rowsetDistributor = new RowsetDistributor(rowsetManager);
				((IRowsetConsumer)this).RequestRowsets(rowsetDistributor);
				rowsetDistributor.DistributeRowsets(connectionManager, domainManager, domainManager.TokenIdProvider);
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000D9BC File Offset: 0x0000BBBC
		public FuzzyLookup(DomainManager domainManager, FuzzyLookupDefinition indexDefinition, IDataReader referenceTableReader)
			: this(domainManager, indexDefinition)
		{
			this.PopulateFuzzyLookup(referenceTableReader);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000D9CD File Offset: 0x0000BBCD
		public FuzzyLookup(DomainManager domainManager, FuzzyLookupDefinition indexDefinition, IFuzzyLookupStateManager stateManager)
			: this(domainManager, indexDefinition)
		{
			this.Initialize(domainManager, stateManager);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000D9E0 File Offset: 0x0000BBE0
		private void AssignLookupIds(List<Lookup> lookups)
		{
			for (int i = 0; i < lookups.Count; i++)
			{
				lookups[i].LookupId = i;
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000DA0C File Offset: 0x0000BC0C
		private void Initialize(DomainManager domainManager, IFuzzyLookupStateManager stateManager)
		{
			this.DomainManager = domainManager;
			this.AssignLookupIds(base.Lookups);
			base.Validate(domainManager);
			this.StateManager = stateManager;
			this.m_statistics = new FuzzyLookup.FuzzyLookupStatistics(this);
			foreach (Lookup lookup in base.Lookups)
			{
				domainManager.ComputeTokenClusterMappingForDomains(lookup.Domains);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000DA94 File Offset: 0x0000BC94
		public IList<IRowsetSink> RowsetSinks
		{
			get
			{
				return new IRowsetSink[] { this.m_referenceRowsetSink };
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000DAA5 File Offset: 0x0000BCA5
		public void RequestRowsets(IRowsetDistributor rowsetDistributor)
		{
			if (!string.IsNullOrEmpty(base.ReferenceRowsetName))
			{
				rowsetDistributor.RequestRowset(base.ReferenceRowsetName, this.m_referenceRowsetSink);
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000DAC6 File Offset: 0x0000BCC6
		public void Dispose()
		{
			if (this.StateManager is IDisposable)
			{
				(this.StateManager as IDisposable).Dispose();
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000DAE8 File Offset: 0x0000BCE8
		private void PopulateFuzzyLookup(IDataReader reader)
		{
			IRecordUpdate recordUpdate = this.RowsetSinks[0] as IRecordUpdate;
			IUpdateContext updateContext = recordUpdate.BeginUpdate(reader.GetSchemaTable());
			while (reader.Read())
			{
				recordUpdate.AddRecord(updateContext, reader);
			}
			recordUpdate.EndUpdate(updateContext);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000DB2D File Offset: 0x0000BD2D
		public void DropIndex(SqlConnection connection)
		{
			if (this.StateManager is SqlStateManager)
			{
				(this.StateManager as SqlStateManager).DropIndex(connection);
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000DB4D File Offset: 0x0000BD4D
		internal static void CreateSignatureGenerator(Lookup lookup, ITokenToClusterMap tokenClusterMap, out IOneDimSignatureGenerator sigGen)
		{
			sigGen = (IOneDimSignatureGenerator)lookup.SignatureGenerator.CreateInstance();
			if (sigGen is ISignatureGeneratorInitialize)
			{
				(sigGen as ISignatureGeneratorInitialize).Initialize(tokenClusterMap);
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000DB78 File Offset: 0x0000BD78
		public FuzzyQuery CreateQuery(int lookupId, double minQueryThreshold, RecordBinding inputBinding)
		{
			FuzzyQuery fuzzyQuery = new FuzzyQuery(this, lookupId, inputBinding, null);
			fuzzyQuery.Threshold = minQueryThreshold;
			Lookup lookup = this.IndexDefinition.Lookups[lookupId];
			IFuzzyComparer fuzzyComparer = (IFuzzyComparer)lookup.Comparer.CreateInstance();
			fuzzyComparer.Initialize(inputBinding, this.IndexDefinition.RecordBinding, lookup.Domains, lookup.ExactMatchDomains);
			fuzzyQuery.Comparer = fuzzyComparer;
			return fuzzyQuery;
		}

		// Token: 0x040000DD RID: 221
		private FuzzyLookup.FuzzyLookupStatistics m_statistics;

		// Token: 0x040000DE RID: 222
		private FuzzyLookup.ReferenceRowsetSink m_referenceRowsetSink;

		// Token: 0x0200013D RID: 317
		[Serializable]
		private class ReferenceRowsetSink : IRowsetSink, IRecordUpdate, IRecordWithIdUpdate, IName
		{
			// Token: 0x17000253 RID: 595
			// (get) Token: 0x06000C43 RID: 3139 RVA: 0x0003570C File Offset: 0x0003390C
			// (set) Token: 0x06000C44 RID: 3140 RVA: 0x00035714 File Offset: 0x00033914
			public string Name { get; set; }

			// Token: 0x06000C45 RID: 3141 RVA: 0x00035720 File Offset: 0x00033920
			private void Validate(DataTable schemaTable)
			{
				foreach (Lookup lookup in this.fuzzyLookup.Lookups)
				{
					if (lookup.Domains == null || lookup.Domains.Count == 0)
					{
						throw new Exception("Must define at least one non-ExactMatch Domain in Lookup.");
					}
					this.fuzzyLookup.RecordBinding.Validate();
				}
				for (int i = 0; i < this.fuzzyLookup.KeyColumns.Count; i++)
				{
					SchemaUtils.FindColumnSchemaRow(schemaTable, this.fuzzyLookup.KeyColumns[i].Name, false);
				}
			}

			// Token: 0x06000C46 RID: 3142 RVA: 0x000357DC File Offset: 0x000339DC
			public IUpdateContext BeginUpdate(DataTable schemaTable)
			{
				FuzzyLookup fuzzyLookup = this.fuzzyLookup;
				IUpdateContext updateContext;
				lock (fuzzyLookup)
				{
					this.Validate(schemaTable);
					updateContext = new FuzzyLookup.ReferenceRowsetSink.UpdateContext
					{
						IndexUpdateContext = new IndexUpdateContext(this.fuzzyLookup.IndexDefinition, this.fuzzyLookup.DomainManager, this.fuzzyLookup.DomainManager.TokenIdProvider, this.fuzzyLookup.DomainManager, JoinSide.Right),
						StateManagerUpdateContext = this.fuzzyLookup.StateManager.BeginUpdate(schemaTable)
					};
				}
				return updateContext;
			}

			// Token: 0x06000C47 RID: 3143 RVA: 0x00035874 File Offset: 0x00033A74
			public void EndUpdate(IUpdateContext _updateContext)
			{
				FuzzyLookup.ReferenceRowsetSink.UpdateContext updateContext = (FuzzyLookup.ReferenceRowsetSink.UpdateContext)_updateContext;
				FuzzyLookup fuzzyLookup = this.fuzzyLookup;
				lock (fuzzyLookup)
				{
					this.fuzzyLookup.StateManager.EndUpdate(updateContext.StateManagerUpdateContext);
				}
			}

			// Token: 0x06000C48 RID: 3144 RVA: 0x000358C4 File Offset: 0x00033AC4
			public void AddRecord(IUpdateContext _updateContext, IDataRecord record)
			{
				FuzzyLookup.ReferenceRowsetSink.UpdateContext updateContext = (FuzzyLookup.ReferenceRowsetSink.UpdateContext)_updateContext;
				Interlocked.Increment(ref this.fuzzyLookup.m_statistics.RowsIndexed);
				updateContext.IndexUpdateContext.AddRecord(this.fuzzyLookup.StateManager, updateContext.StateManagerUpdateContext, record);
			}

			// Token: 0x06000C49 RID: 3145 RVA: 0x0003590C File Offset: 0x00033B0C
			public void AddRecord(IUpdateContext _updateContext, IDataRecord record, out int recordId)
			{
				FuzzyLookup.ReferenceRowsetSink.UpdateContext updateContext = (FuzzyLookup.ReferenceRowsetSink.UpdateContext)_updateContext;
				Interlocked.Increment(ref this.fuzzyLookup.m_statistics.RowsIndexed);
				recordId = updateContext.IndexUpdateContext.AddRecord(this.fuzzyLookup.StateManager, updateContext.StateManagerUpdateContext, record);
			}

			// Token: 0x06000C4A RID: 3146 RVA: 0x00035958 File Offset: 0x00033B58
			public void RemoveRecord(IUpdateContext _updateContext, IDataRecord record)
			{
				FuzzyLookup.ReferenceRowsetSink.UpdateContext updateContext = (FuzzyLookup.ReferenceRowsetSink.UpdateContext)_updateContext;
				updateContext.IndexUpdateContext.RemoveRecord(this.fuzzyLookup.StateManager, updateContext.StateManagerUpdateContext, this.fuzzyLookup.IndexDefinition, record);
				Interlocked.Decrement(ref this.fuzzyLookup.m_statistics.RowsIndexed);
			}

			// Token: 0x06000C4B RID: 3147 RVA: 0x000359AC File Offset: 0x00033BAC
			public bool TryRemoveRecord(IUpdateContext updateContext, IDataRecord record, out int rid)
			{
				FuzzyLookup.ReferenceRowsetSink.UpdateContext updateContext2 = (FuzzyLookup.ReferenceRowsetSink.UpdateContext)updateContext;
				if (this.fuzzyLookup.StateManager.TryRemoveRecord(updateContext2.StateManagerUpdateContext, record, out rid))
				{
					Interlocked.Increment(ref this.fuzzyLookup.m_statistics.RowsIndexed);
					return true;
				}
				return false;
			}

			// Token: 0x04000519 RID: 1305
			public FuzzyLookup fuzzyLookup;

			// Token: 0x020001BB RID: 443
			private class UpdateContext : IUpdateContext
			{
				// Token: 0x04000744 RID: 1860
				public IndexUpdateContext IndexUpdateContext;

				// Token: 0x04000745 RID: 1861
				public IUpdateContext StateManagerUpdateContext;
			}
		}

		// Token: 0x0200013E RID: 318
		[Serializable]
		private class FuzzyLookupStatistics : StatisticsBase, IDeserializationCallback
		{
			// Token: 0x17000254 RID: 596
			// (get) Token: 0x06000C4D RID: 3149 RVA: 0x000359FB File Offset: 0x00033BFB
			public IStatistics StateManagerStatistics
			{
				get
				{
					return this.m_fuzzyLookup.StateManager.Statistics;
				}
			}

			// Token: 0x17000255 RID: 597
			// (get) Token: 0x06000C4E RID: 3150 RVA: 0x00035A0D File Offset: 0x00033C0D
			[Statistic(0)]
			public double AverageRidsPerSignature
			{
				get
				{
					return this.StateManagerStatistics["NonDistinctSignaturesIndexed"] / this.StateManagerStatistics["DistinctSignaturesIndexed"];
				}
			}

			// Token: 0x06000C4F RID: 3151 RVA: 0x00035A31 File Offset: 0x00033C31
			internal FuzzyLookupStatistics(FuzzyLookup fuzzyLookup)
			{
				this.m_fuzzyLookup = fuzzyLookup;
			}

			// Token: 0x06000C50 RID: 3152 RVA: 0x00035A56 File Offset: 0x00033C56
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				this.TransformationClusteringTime = new Stopwatch();
				this.IndexBuildTime = new Stopwatch();
			}

			// Token: 0x06000C51 RID: 3153 RVA: 0x00035A6E File Offset: 0x00033C6E
			public new void Reset()
			{
				this.RowsIndexed = 0L;
				this.m_fuzzyLookup.StateManager.Statistics.Reset();
				if (base.EnableTimers)
				{
					this.TransformationClusteringTime.Reset();
					this.IndexBuildTime.Reset();
				}
			}

			// Token: 0x17000256 RID: 598
			// (get) Token: 0x06000C52 RID: 3154 RVA: 0x00035AAB File Offset: 0x00033CAB
			public new virtual IEnumerable<string> Properties
			{
				get
				{
					foreach (string text in base.Properties)
					{
						yield return text;
					}
					IEnumerator<string> enumerator = null;
					foreach (string text2 in this.StateManagerStatistics.Properties)
					{
						yield return text2;
					}
					enumerator = null;
					yield break;
					yield break;
				}
			}

			// Token: 0x17000257 RID: 599
			public override double this[string propertyName]
			{
				get
				{
					if (Enumerable.Contains<string>(base.Properties, propertyName))
					{
						return base[propertyName];
					}
					if (Enumerable.Contains<string>(this.StateManagerStatistics.Properties, propertyName))
					{
						return this.StateManagerStatistics[propertyName];
					}
					throw new ArgumentException(string.Format("Property {0} not found.", propertyName));
				}
			}

			// Token: 0x06000C54 RID: 3156 RVA: 0x00035B10 File Offset: 0x00033D10
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(string.Format("RowsIndexed: {0}", this.RowsIndexed));
				stringBuilder.Append(this.StateManagerStatistics.ToString());
				if (base.EnableTimers)
				{
					stringBuilder.AppendLine(string.Format("TransformationClusteringTime: {0:F3} ms", this.TransformationClusteringTime.Elapsed.TotalMilliseconds));
					stringBuilder.AppendLine(string.Format("IndexBuildTime: {0:F3} ms", this.IndexBuildTime.Elapsed.TotalMilliseconds));
				}
				return stringBuilder.ToString();
			}

			// Token: 0x0400051A RID: 1306
			private FuzzyLookup m_fuzzyLookup;

			// Token: 0x0400051B RID: 1307
			[Statistic(0)]
			public long RowsIndexed;

			// Token: 0x0400051C RID: 1308
			[NonSerialized]
			public Stopwatch TransformationClusteringTime = new Stopwatch();

			// Token: 0x0400051D RID: 1309
			[NonSerialized]
			public Stopwatch IndexBuildTime = new Stopwatch();
		}
	}
}
