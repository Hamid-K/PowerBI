using System;
using System.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200004E RID: 78
	internal sealed class IndexUpdateContext
	{
		// Token: 0x060002CA RID: 714 RVA: 0x0000DF48 File Offset: 0x0000C148
		public IndexUpdateContext(FuzzyLookupDefinition indexDefinition, IDomainManager domainManager, ITokenIdProvider tokenIdProvider, ITokenToClusterMap tokenClusterMap, JoinSide joinSide)
			: this(indexDefinition.RecordBinding, indexDefinition.Lookups.ToArray(), domainManager, tokenIdProvider, tokenClusterMap, joinSide)
		{
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000DF68 File Offset: 0x0000C168
		public IndexUpdateContext(RecordBinding indexBindings, Lookup[] lookups, IDomainManager domainManager, ITokenIdProvider tokenIdProvider, ITokenToClusterMap tokenClusterMap, JoinSide joinSide)
		{
			this.m_lookupDefinitions = (Lookup[])lookups.Clone();
			this.m_lookupUpdateContexts = new LookupUpdateContext[lookups.Length];
			for (int i = 0; i < lookups.Length; i++)
			{
				Lookup lookup = lookups[i];
				this.m_lookupUpdateContexts[i] = new LookupUpdateContext(domainManager, tokenIdProvider, indexBindings, lookup.Domains, lookup.ExactMatchDomains, joinSide);
				FuzzyLookup.CreateSignatureGenerator(lookup, tokenClusterMap, out this.m_lookupUpdateContexts[i].SignatureGenerator);
				this.m_lookupUpdateContexts[i].MultiDimSignatureGenerator = this.m_lookupUpdateContexts[i].SignatureGenerator as IMultiDimSignatureGenerator;
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000E000 File Offset: 0x0000C200
		public int AddRecord(IFuzzyLookupStateManager stateManager, IUpdateContext stateManagerUpdateContext, IDataRecord record)
		{
			int num2;
			lock (stateManager)
			{
				int num;
				stateManager.AddRecord(stateManagerUpdateContext, record, out num);
				for (int i = 0; i < this.m_lookupDefinitions.Length; i++)
				{
					Lookup lookup = this.m_lookupDefinitions[i];
					LookupUpdateContext lookupUpdateContext = this.m_lookupUpdateContexts[i];
					RecordContext recordContext = lookupUpdateContext.RecordContext;
					int lookupId = lookup.LookupId;
					lookupUpdateContext.Reset();
					lookupUpdateContext.TokenizeAndRuleMatch(record);
					if (stateManager.EnableReferenceContextCaching)
					{
						stateManager.CacheRecordContext(num, lookupId, lookupUpdateContext.RecordContext.Clone(HeapSegmentAllocator<int>.Instance, HeapSegmentAllocator<byte>.Instance));
					}
					lookupUpdateContext.SignatureGenerator.Reset(recordContext.TokenSequence, recordContext.TransformationMatchList);
					if (lookupUpdateContext.MultiDimSignatureGenerator != null)
					{
						for (int j = 0; j < lookupUpdateContext.MultiDimSignatureGenerator.NumHashtables; j++)
						{
							lookupUpdateContext.MultiDimSignatureGenerator.Reset(j);
							stateManager.AddRidSignatures(stateManagerUpdateContext, num, lookupId, j, lookupUpdateContext.MultiDimSignatureGenerator);
						}
					}
					else
					{
						stateManager.AddRidSignatures(stateManagerUpdateContext, num, lookupId, 0, lookupUpdateContext.SignatureGenerator);
					}
				}
				num2 = num;
			}
			return num2;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000E118 File Offset: 0x0000C318
		public void RemoveRecord(IFuzzyLookupStateManager stateManager, IUpdateContext stateManagerUpdateContext, FuzzyLookupDefinition indexDefinition, IDataRecord record)
		{
			lock (stateManager)
			{
				int num;
				if (!stateManager.TryRemoveRecord(stateManagerUpdateContext, record, out num))
				{
					throw new ArgumentOutOfRangeException("The specified record was not found!");
				}
				for (int i = 0; i < indexDefinition.Lookups.Count; i++)
				{
					Lookup lookup = indexDefinition.Lookups[i];
					LookupUpdateContext lookupUpdateContext = this.m_lookupUpdateContexts[i];
					RecordContext recordContext = lookupUpdateContext.RecordContext;
					lookupUpdateContext.Reset();
					lookupUpdateContext.TokenizeAndRuleMatch(record);
					lookupUpdateContext.SignatureGenerator.Reset(recordContext.TokenSequence, recordContext.TransformationMatchList);
					if (lookupUpdateContext.MultiDimSignatureGenerator != null)
					{
						for (int j = 0; j < lookupUpdateContext.MultiDimSignatureGenerator.NumHashtables; j++)
						{
							lookupUpdateContext.MultiDimSignatureGenerator.Reset(j);
							stateManager.RemoveRidSignatures(stateManagerUpdateContext, num, i, j, lookupUpdateContext.MultiDimSignatureGenerator);
						}
					}
					else
					{
						stateManager.RemoveRidSignatures(stateManagerUpdateContext, num, i, 0, lookupUpdateContext.SignatureGenerator);
					}
				}
			}
		}

		// Token: 0x040000E6 RID: 230
		internal LookupUpdateContext[] m_lookupUpdateContexts;

		// Token: 0x040000E7 RID: 231
		private Lookup[] m_lookupDefinitions;
	}
}
