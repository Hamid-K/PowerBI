using System;
using System.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000050 RID: 80
	[Serializable]
	public class FuzzyLookupTuner : IRecordUpdate
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000E8C4 File Offset: 0x0000CAC4
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x0000E8CC File Offset: 0x0000CACC
		public bool EnableLocalitySensitiveHashing { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000E8D5 File Offset: 0x0000CAD5
		// (set) Token: 0x060002DB RID: 731 RVA: 0x0000E8DD File Offset: 0x0000CADD
		public bool EnablePrefixFiltering { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000E8E6 File Offset: 0x0000CAE6
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0000E8EE File Offset: 0x0000CAEE
		public double LSHSuccessProbability { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000E8F7 File Offset: 0x0000CAF7
		// (set) Token: 0x060002DF RID: 735 RVA: 0x0000E8FF File Offset: 0x0000CAFF
		public double SampleFraction { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000E908 File Offset: 0x0000CB08
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x0000E910 File Offset: 0x0000CB10
		internal bool UseFullEstimator { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000E919 File Offset: 0x0000CB19
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x0000E921 File Offset: 0x0000CB21
		internal int RidListSpaceFactorUpperBound { get; set; }

		// Token: 0x060002E4 RID: 740 RVA: 0x0000E92C File Offset: 0x0000CB2C
		public FuzzyLookupTuner()
		{
			this.EnableLocalitySensitiveHashing = true;
			this.EnablePrefixFiltering = true;
			this.LSHSuccessProbability = 0.95;
			this.UseFullEstimator = false;
			this.RidListSpaceFactorUpperBound = 50;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000E984 File Offset: 0x0000CB84
		public bool Prepare(DomainManager domainManager, FuzzyLookupDefinition indexDefinition)
		{
			bool flag = false;
			this.m_domainManager = domainManager;
			this.m_indexDefinition = indexDefinition;
			this.m_random = new Random(4);
			this.m_tableCardinality = 0;
			foreach (Lookup lookup in indexDefinition.Lookups)
			{
				domainManager.ComputeTokenClusterMappingForDomains(lookup.Domains);
			}
			if (this.EnableLocalitySensitiveHashing)
			{
				this.m_lshFL = new IndexUpdateContext[indexDefinition.Lookups.Count][];
				this.m_lshSJ = new ISelfJoinEstimator[indexDefinition.Lookups.Count][];
				this.m_lshRidListSpace = new int[indexDefinition.Lookups.Count][];
				for (int i = 0; i < indexDefinition.Lookups.Count; i++)
				{
					this.m_lshFL[i] = new IndexUpdateContext[this.m_lshDims.Length];
					this.m_lshSJ[i] = new ISelfJoinEstimator[this.m_lshDims.Length];
					this.m_lshRidListSpace[i] = new int[this.m_lshDims.Length];
				}
			}
			if (this.EnablePrefixFiltering)
			{
				this.m_pfFL = new IndexUpdateContext[indexDefinition.Lookups.Count];
				this.m_pfSJ = new ISelfJoinEstimator[indexDefinition.Lookups.Count];
				this.m_pfRidListSpace = new int[indexDefinition.Lookups.Count];
			}
			for (int j = 0; j < indexDefinition.Lookups.Count; j++)
			{
				Lookup lookup2 = indexDefinition.Lookups[j];
				if (lookup2.TunerSettings.Enabled && lookup2.TunerSettings.ComparisonType == FuzzyComparisonType.Jaccard)
				{
					flag = true;
					if (this.EnableLocalitySensitiveHashing)
					{
						for (int k = 0; k < this.m_lshDims.Length; k++)
						{
							lookup2.SignatureGenerator = SignatureGenerator.Create(FuzzyIndexType.LocalitySensitiveHashing, 1, this.m_lshDims[k]);
							this.m_lshFL[j][k] = new IndexUpdateContext(indexDefinition, this.m_domainManager, this.m_domainManager.TokenIdProvider, this.m_domainManager, JoinSide.Right);
							if (this.UseFullEstimator)
							{
								this.m_lshSJ[j][k] = new FullEstimator();
							}
							else
							{
								this.m_lshSJ[j][k] = new SketchEstimator();
							}
							this.m_lshSJ[j][k].Begin();
							this.m_lshRidListSpace[j][k] = 0;
						}
					}
					if (this.EnablePrefixFiltering)
					{
						lookup2.SignatureGenerator = SignatureGenerator.Create(FuzzyIndexType.PrefixFiltering, 0.0);
						this.m_pfFL[j] = new IndexUpdateContext(indexDefinition, this.m_domainManager, this.m_domainManager.TokenIdProvider, this.m_domainManager, JoinSide.Right);
						if (this.UseFullEstimator)
						{
							this.m_pfSJ[j] = new FullEstimator();
						}
						else
						{
							this.m_pfSJ[j] = new SketchEstimator();
						}
						this.m_pfSJ[j].Begin();
						this.m_pfRidListSpace[j] = 0;
					}
				}
			}
			return flag;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000EC78 File Offset: 0x0000CE78
		public IUpdateContext BeginUpdate(DataTable schemaTable)
		{
			if (this.InUpdateMode)
			{
				throw new InvalidOperationException("Only one update context is allowed at a time.");
			}
			this.InUpdateMode = true;
			return new FuzzyLookupTuner.UpdateContext();
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000EC99 File Offset: 0x0000CE99
		public void AddRecord(IUpdateContext _updateContext, IDataRecord r)
		{
			FuzzyLookupTuner.UpdateContext updateContext = (FuzzyLookupTuner.UpdateContext)_updateContext;
			if (this.m_random.NextDouble() >= this.SampleFraction)
			{
				return;
			}
			this.m_tableCardinality++;
			this.ProcessRecord(r);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000ECCB File Offset: 0x0000CECB
		public void RemoveRecord(IUpdateContext _updateContext, IDataRecord r)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000ECD4 File Offset: 0x0000CED4
		public void EndUpdate(IUpdateContext _updateContext)
		{
			FuzzyLookupTuner.UpdateContext updateContext = (FuzzyLookupTuner.UpdateContext)_updateContext;
			for (int i = 0; i < this.m_indexDefinition.Lookups.Count; i++)
			{
				Lookup lookup = this.m_indexDefinition.Lookups[i];
				if (lookup.TunerSettings.Enabled && lookup.TunerSettings.ComparisonType == FuzzyComparisonType.Jaccard)
				{
					long num = 0L;
					if (this.EnablePrefixFiltering)
					{
						lookup.SignatureGenerator = SignatureGenerator.Create(FuzzyIndexType.PrefixFiltering, 0.0);
						this.m_pfSJ[i].End();
						if (this.EnableLocalitySensitiveHashing)
						{
							num = this.m_pfSJ[i].SelfJoinSize();
						}
					}
					if (this.EnableLocalitySensitiveHashing)
					{
						int num2 = 0;
						int num3 = 0;
						bool flag = false;
						if (!this.EnablePrefixFiltering)
						{
							flag = true;
						}
						for (int j = 0; j < this.m_lshDims.Length; j++)
						{
							this.m_lshSJ[i][j].End();
							int num4 = this.m_lshDims[j];
							int num5 = LshSignatureGeneratorV1.NumHashTables(num4, lookup.TunerSettings.MinQueryThreshold, this.LSHSuccessProbability);
							long num6 = this.m_lshSJ[i][j].SelfJoinSize() * (long)num5;
							float num7 = 0f;
							if (this.m_tableCardinality > 0)
							{
								num7 = (float)(this.m_lshRidListSpace[i][j] * num5) / (float)this.m_tableCardinality;
							}
							if (j == 0 && !this.EnablePrefixFiltering)
							{
								num = num6;
								num2 = num4;
								num3 = num5;
							}
							if (num6 < num && num7 <= (float)this.RidListSpaceFactorUpperBound)
							{
								flag = true;
								num = num6;
								num2 = num4;
								num3 = num5;
							}
						}
						if (flag)
						{
							lookup.SignatureGenerator = SignatureGenerator.Create(FuzzyIndexType.LocalitySensitiveHashing, num3, num2);
						}
					}
				}
			}
			this.Clear();
			this.InUpdateMode = false;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000EE80 File Offset: 0x0000D080
		private void ProcessRecord(IDataRecord r)
		{
			for (int i = 0; i < this.m_indexDefinition.Lookups.Count; i++)
			{
				Lookup lookup = this.m_indexDefinition.Lookups[i];
				if (lookup.TunerSettings.Enabled && lookup.TunerSettings.ComparisonType == FuzzyComparisonType.Jaccard)
				{
					if (this.EnableLocalitySensitiveHashing)
					{
						for (int j = 0; j < this.m_lshDims.Length; j++)
						{
							this.ProcessRecord(this.m_lshFL[i][j], r, i, j, this.m_lshSJ[i][j]);
						}
					}
					if (this.EnablePrefixFiltering)
					{
						this.ProcessRecord(this.m_pfFL[i], r, i, 0, this.m_pfSJ[i]);
					}
				}
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000EF34 File Offset: 0x0000D134
		private void ProcessRecord(IndexUpdateContext updateContext, IDataRecord r, int lookupidx, int gidx, ISelfJoinEstimator SJEst)
		{
			Lookup lookup = this.m_indexDefinition.Lookups[lookupidx];
			LookupUpdateContext lookupUpdateContext = updateContext.m_lookupUpdateContexts[lookupidx];
			RecordContext recordContext = lookupUpdateContext.RecordContext;
			lookupUpdateContext.Reset();
			lookupUpdateContext.TokenizeAndRuleMatch(r);
			lookupUpdateContext.SignatureGenerator.Reset(recordContext.TokenSequence, recordContext.TransformationMatchList);
			if (lookupUpdateContext.MultiDimSignatureGenerator != null)
			{
				for (int i = 0; i < lookupUpdateContext.MultiDimSignatureGenerator.NumHashtables; i++)
				{
					lookupUpdateContext.MultiDimSignatureGenerator.Reset(i);
					foreach (int num in lookupUpdateContext.MultiDimSignatureGenerator)
					{
						SJEst.Add(num);
						this.m_lshRidListSpace[lookupidx][gidx]++;
					}
				}
				return;
			}
			foreach (int num2 in lookupUpdateContext.SignatureGenerator)
			{
				SJEst.Add(num2);
				this.m_pfRidListSpace[lookupidx]++;
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000F05C File Offset: 0x0000D25C
		private void Clear()
		{
			for (int i = 0; i < this.m_indexDefinition.Lookups.Count; i++)
			{
				Lookup lookup = this.m_indexDefinition.Lookups[i];
				if (lookup.TunerSettings.Enabled && lookup.TunerSettings.ComparisonType == FuzzyComparisonType.Jaccard)
				{
					if (this.EnablePrefixFiltering)
					{
						this.m_pfSJ[i].Clear();
					}
					if (this.EnableLocalitySensitiveHashing)
					{
						for (int j = 0; j < this.m_lshDims.Length; j++)
						{
							this.m_lshSJ[i][j].Clear();
						}
					}
				}
				Array.Clear(this.m_lshFL[i], 0, this.m_lshDims.Length);
				Array.Clear(this.m_lshSJ[i], 0, this.m_lshDims.Length);
				Array.Clear(this.m_lshRidListSpace[i], 0, this.m_lshDims.Length);
			}
			Array.Clear(this.m_lshFL, 0, this.m_indexDefinition.Lookups.Count);
			Array.Clear(this.m_lshSJ, 0, this.m_indexDefinition.Lookups.Count);
			Array.Clear(this.m_lshRidListSpace, 0, this.m_indexDefinition.Lookups.Count);
			Array.Clear(this.m_pfFL, 0, this.m_indexDefinition.Lookups.Count);
			Array.Clear(this.m_pfSJ, 0, this.m_indexDefinition.Lookups.Count);
			Array.Clear(this.m_pfRidListSpace, 0, this.m_indexDefinition.Lookups.Count);
		}

		// Token: 0x040000F9 RID: 249
		private DomainManager m_domainManager;

		// Token: 0x040000FA RID: 250
		private FuzzyLookupDefinition m_indexDefinition;

		// Token: 0x040000FB RID: 251
		private int[] m_lshDims = new int[] { 2, 3, 4, 5, 6, 7, 8 };

		// Token: 0x040000FC RID: 252
		private IndexUpdateContext[][] m_lshFL;

		// Token: 0x040000FD RID: 253
		private ISelfJoinEstimator[][] m_lshSJ;

		// Token: 0x040000FE RID: 254
		private int[][] m_lshRidListSpace;

		// Token: 0x040000FF RID: 255
		private IndexUpdateContext[] m_pfFL;

		// Token: 0x04000100 RID: 256
		private ISelfJoinEstimator[] m_pfSJ;

		// Token: 0x04000101 RID: 257
		private int[] m_pfRidListSpace;

		// Token: 0x04000102 RID: 258
		private Random m_random;

		// Token: 0x04000103 RID: 259
		private int m_tableCardinality;

		// Token: 0x04000104 RID: 260
		private bool InUpdateMode;

		// Token: 0x02000140 RID: 320
		private class UpdateContext : IUpdateContext, IDisposable
		{
			// Token: 0x06000C59 RID: 3161 RVA: 0x00035BF0 File Offset: 0x00033DF0
			public void Dispose()
			{
			}
		}
	}
}
