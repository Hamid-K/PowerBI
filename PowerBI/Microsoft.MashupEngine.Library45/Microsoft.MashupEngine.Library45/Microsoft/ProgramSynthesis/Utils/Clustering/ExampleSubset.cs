using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Clustering
{
	// Token: 0x0200061C RID: 1564
	internal struct ExampleSubset : IEnumerable<State>, IEnumerable
	{
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x000609AA File Offset: 0x0005EBAA
		internal readonly IReadOnlyList<State> States { get; }

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x000609B2 File Offset: 0x0005EBB2
		internal readonly uint Cardinality { get; }

		// Token: 0x060021E7 RID: 8679 RVA: 0x000609BC File Offset: 0x0005EBBC
		internal ExampleSubset(IReadOnlyList<State> allExamples, IReadOnlyList<uint> allExampleCounts, Predicate<State> p)
		{
			this.Cardinality = 0U;
			List<State> list = new List<State>();
			this._bitset = new BitArray(allExamples.Count, false);
			for (int i = 0; i < allExamples.Count; i++)
			{
				State state = allExamples[i];
				if (p(state))
				{
					this.Cardinality += allExampleCounts[i];
					list.Add(state);
					this._bitset.Set(i, true);
				}
			}
			this.States = list;
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x00060A3C File Offset: 0x0005EC3C
		private ExampleSubset(IReadOnlyList<State> allExamples, IReadOnlyList<uint> allExampleCounts, BitArray bitset)
		{
			this.Cardinality = 0U;
			List<State> list = new List<State>();
			this._bitset = bitset;
			foreach (int num in bitset.GetEnabledIndices())
			{
				list.Add(allExamples[num]);
				this.Cardinality += allExampleCounts[num];
			}
			this.States = list;
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x00060AC0 File Offset: 0x0005ECC0
		internal IEnumerable<State> SampleWithReplacement(Random rng, int sampleSize)
		{
			return this.States.RandomlySampleWithReplacement(rng, sampleSize);
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x00060ACF File Offset: 0x0005ECCF
		internal bool HasIntersectionWith(ExampleSubset other)
		{
			return this._bitset.HasIntersectionWith(other._bitset);
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x00060AE4 File Offset: 0x0005ECE4
		internal static ExampleSubset UnionComplement(IReadOnlyList<State> allExamples, IReadOnlyList<uint> allExampleCounts, IEnumerable<ExampleSubset> subsets)
		{
			BitArray bitArray = new BitArray(allExamples.Count);
			foreach (ExampleSubset exampleSubset in subsets)
			{
				bitArray.Or(exampleSubset._bitset);
			}
			bitArray.Not();
			return new ExampleSubset(allExamples, allExampleCounts, bitArray);
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x00060B50 File Offset: 0x0005ED50
		internal static ExampleSubset Union(IReadOnlyList<State> allExamples, IReadOnlyList<uint> allExampleCounts, IEnumerable<ExampleSubset> subsets)
		{
			BitArray bitArray = new BitArray(allExamples.Count);
			foreach (ExampleSubset exampleSubset in subsets)
			{
				bitArray.Or(exampleSubset._bitset);
			}
			return new ExampleSubset(allExamples, allExampleCounts, bitArray);
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x00060BB4 File Offset: 0x0005EDB4
		internal ExampleSubset Difference(IReadOnlyList<State> allExamples, IReadOnlyList<uint> allExampleCounts, ExampleSubset other)
		{
			return new ExampleSubset(allExamples, allExampleCounts, this._bitset.Clone().And(other._bitset.Clone().Not()));
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x00060BDD File Offset: 0x0005EDDD
		internal bool SuperSetOf(ExampleSubset other)
		{
			BitArray bitArray = other._bitset.Clone();
			bitArray.And(this._bitset.Clone().Not());
			return bitArray.AllFalse();
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x00060C06 File Offset: 0x0005EE06
		internal IEnumerable<int> GetEnabledIndices()
		{
			return this._bitset.GetEnabledIndices();
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x00060C13 File Offset: 0x0005EE13
		public IEnumerator<State> GetEnumerator()
		{
			return this.States.GetEnumerator();
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x00060C20 File Offset: 0x0005EE20
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.States.GetEnumerator();
		}

		// Token: 0x04001039 RID: 4153
		private readonly BitArray _bitset;
	}
}
