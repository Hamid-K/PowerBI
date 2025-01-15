using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000117 RID: 279
	[Serializable]
	public sealed class CustomWeightProvider : ITokenWeightProvider
	{
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x000332F6 File Offset: 0x000314F6
		// (set) Token: 0x06000B9E RID: 2974 RVA: 0x000332FE File Offset: 0x000314FE
		public int DefaultWeight { get; set; }

		// Token: 0x06000B9F RID: 2975 RVA: 0x00033307 File Offset: 0x00031507
		public CustomWeightProvider()
		{
			this.DefaultWeight = 5;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00033324 File Offset: 0x00031524
		public CustomWeightProvider(IDomainManager domainManager, ITokenIdProvider tokenIdProvider, string domainName, DataTable weightTable)
			: this()
		{
			int domainId = domainManager.GetDomainId(domainName);
			StringExtent stringExtent = default(StringExtent);
			using (IDataReader dataReader = weightTable.CreateDataReader())
			{
				IUpdateContext updateContext = this.BeginUpdate(dataReader.GetSchemaTable());
				while (dataReader.Read())
				{
					stringExtent.Array = dataReader.GetString(0).ToCharArray();
					stringExtent.Length = stringExtent.Array.Length;
					int num = (int)dataReader[1];
					this.SetWeight(tokenIdProvider.GetOrCreateTokenId(stringExtent, domainId), num);
				}
				this.EndUpdate(updateContext);
			}
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x000333CC File Offset: 0x000315CC
		public IUpdateContext BeginUpdate(DataTable schemaTable)
		{
			return null;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x000333CF File Offset: 0x000315CF
		public void SetWeight(int tokenId, int weight)
		{
			this.m_tokenWeights[tokenId] = weight;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x000333DE File Offset: 0x000315DE
		public void EndUpdate(IUpdateContext updateContext)
		{
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x000333E0 File Offset: 0x000315E0
		public int GetWeight(ITokenIdProvider tokenIdProvider, int tokenId)
		{
			int defaultWeight;
			if (!this.m_tokenWeights.TryGetValue(tokenId, ref defaultWeight))
			{
				defaultWeight = this.DefaultWeight;
			}
			return defaultWeight;
		}

		// Token: 0x04000467 RID: 1127
		private Dictionary<int, int> m_tokenWeights = new Dictionary<int, int>();
	}
}
