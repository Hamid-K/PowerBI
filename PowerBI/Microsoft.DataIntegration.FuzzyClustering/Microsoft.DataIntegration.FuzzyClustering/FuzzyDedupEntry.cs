using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.DataIntegration.FuzzyMatching;

namespace Microsoft.DataIntegration.FuzzyClustering
{
	// Token: 0x02000009 RID: 9
	public class FuzzyDedupEntry
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002D28 File Offset: 0x00000F28
		public static IEnumerable<DuplicateGroup> Dedup(DataTable inputTable, FuzzyLookupEntry.FuzzyLookupParameters parameters, DataTable transformTable = null, FuzzyDedupEntry.DedupConstraints constraints = null)
		{
			if (inputTable == null)
			{
				throw new ArgumentNullException("Input Table");
			}
			if (inputTable.Columns.Count < 2)
			{
				throw new ArgumentException("Table must have 2 or more columns: ID column and data columns");
			}
			if (inputTable.Columns[0].DataType != typeof(int))
			{
				throw new ArgumentException("The 1st column of input table must be IDs (int)");
			}
			for (int i = 1; i < inputTable.Columns.Count; i++)
			{
				if (inputTable.Columns[i].DataType != typeof(string))
				{
					throw new ArgumentException("The data column [" + i + "] of input table must be strings");
				}
			}
			FuzzyLookupEntry.FuzzyLookupParameters fuzzyLookupParameters = new FuzzyLookupEntry.FuzzyLookupParameters(parameters.SimilarityThreshold, parameters.IgnoreCase, parameters.IgnoreSpace, int.MaxValue, parameters.LocaleId, parameters.Concurrency, false);
			return new HierarchicalFuzzyGrouper().Group(inputTable, fuzzyLookupParameters, transformTable, constraints);
		}

		// Token: 0x02000016 RID: 22
		public class DedupConstraints
		{
			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000072 RID: 114 RVA: 0x0000456D File Offset: 0x0000276D
			public IEnumerable<FuzzyDedupEntry.PositiveConstraint> PositiveConstraints
			{
				get
				{
					return this.positiveConstraints;
				}
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000073 RID: 115 RVA: 0x00004575 File Offset: 0x00002775
			public IEnumerable<FuzzyDedupEntry.NegativeConstraint> NegativeConstraints
			{
				get
				{
					return this.negativeConstraints;
				}
			}

			// Token: 0x06000074 RID: 116 RVA: 0x0000457D File Offset: 0x0000277D
			public void AddPositiveConstraint(FuzzyDedupEntry.PositiveConstraint positiveConstraint)
			{
				this.positiveConstraints.Add(positiveConstraint);
			}

			// Token: 0x06000075 RID: 117 RVA: 0x0000458B File Offset: 0x0000278B
			public void AddNegativeConstraint(FuzzyDedupEntry.NegativeConstraint negativeConstraint)
			{
				this.negativeConstraints.Add(negativeConstraint);
			}

			// Token: 0x0400003B RID: 59
			private List<FuzzyDedupEntry.PositiveConstraint> positiveConstraints = new List<FuzzyDedupEntry.PositiveConstraint>();

			// Token: 0x0400003C RID: 60
			private List<FuzzyDedupEntry.NegativeConstraint> negativeConstraints = new List<FuzzyDedupEntry.NegativeConstraint>();
		}

		// Token: 0x02000017 RID: 23
		public class PositiveConstraint
		{
			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000077 RID: 119 RVA: 0x000045B7 File Offset: 0x000027B7
			// (set) Token: 0x06000078 RID: 120 RVA: 0x000045BF File Offset: 0x000027BF
			public int RepresentId { get; private set; }

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000079 RID: 121 RVA: 0x000045C8 File Offset: 0x000027C8
			// (set) Token: 0x0600007A RID: 122 RVA: 0x000045D0 File Offset: 0x000027D0
			public List<int> IdToBeCollapsed { get; private set; }

			// Token: 0x0600007B RID: 123 RVA: 0x000045D9 File Offset: 0x000027D9
			public PositiveConstraint(List<int> idsToBeCollapsed)
			{
				this.IdToBeCollapsed = idsToBeCollapsed;
			}

			// Token: 0x0600007C RID: 124 RVA: 0x000045E8 File Offset: 0x000027E8
			public PositiveConstraint(int representative, List<int> idsToBeCollapsed)
			{
				this.RepresentId = representative;
				this.IdToBeCollapsed = idsToBeCollapsed;
			}
		}

		// Token: 0x02000018 RID: 24
		public class NegativeConstraint
		{
			// Token: 0x17000017 RID: 23
			// (get) Token: 0x0600007D RID: 125 RVA: 0x000045FE File Offset: 0x000027FE
			// (set) Token: 0x0600007E RID: 126 RVA: 0x00004606 File Offset: 0x00002806
			public int Id { get; private set; }

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x0600007F RID: 127 RVA: 0x0000460F File Offset: 0x0000280F
			// (set) Token: 0x06000080 RID: 128 RVA: 0x00004617 File Offset: 0x00002817
			public List<int> IncompatibleIds { get; private set; }

			// Token: 0x06000081 RID: 129 RVA: 0x00004620 File Offset: 0x00002820
			public NegativeConstraint(int id, List<int> imcompatibleIds)
			{
				this.Id = id;
				this.IncompatibleIds = imcompatibleIds;
			}
		}
	}
}
