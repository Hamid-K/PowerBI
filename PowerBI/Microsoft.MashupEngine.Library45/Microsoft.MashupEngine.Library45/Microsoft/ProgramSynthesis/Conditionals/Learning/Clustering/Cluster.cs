using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Conditionals.Learning.Clustering
{
	// Token: 0x02000A67 RID: 2663
	internal class Cluster
	{
		// Token: 0x0600420C RID: 16908 RVA: 0x000CE948 File Offset: 0x000CCB48
		public Cluster(IReadOnlyList<LearningCacheSubstring> inputs, bool supportRegex)
		{
			HashSet<PredicateFeature>[] array = new HashSet<PredicateFeature>[inputs.Count];
			HashSet<PredicateFeature> hashSet = new HashSet<PredicateFeature>();
			for (int i = 0; i < inputs.Count; i++)
			{
				HashSet<PredicateFeature> featuresFor = PredicateFeature.GetFeaturesFor(inputs[i], supportRegex);
				hashSet.AddRange(featuresFor);
				array[i] = featuresFor;
			}
			this.AllFeatures = hashSet.OrderByDescending((PredicateFeature feature) => feature.Score).ToList<PredicateFeature>();
			this.DataPoints = (from tup in array.ZipWith(inputs)
				select new DataPoint(tup.Item2, tup.Item1, this.AllFeatures)).ToList<DataPoint>();
			this.DescriptiveFeatures = new List<PredicateFeature>();
		}

		// Token: 0x0600420D RID: 16909 RVA: 0x000CE9F5 File Offset: 0x000CCBF5
		internal Cluster(IReadOnlyList<DataPoint> points, IReadOnlyList<PredicateFeature> allFeatures, IReadOnlyList<PredicateFeature> descriptiveFeatures)
		{
			this.DataPoints = points;
			this.AllFeatures = allFeatures;
			this.DescriptiveFeatures = descriptiveFeatures;
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x0600420E RID: 16910 RVA: 0x000CEA12 File Offset: 0x000CCC12
		public IReadOnlyList<PredicateFeature> AllFeatures { get; }

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x0600420F RID: 16911 RVA: 0x000CEA1A File Offset: 0x000CCC1A
		public IReadOnlyList<DataPoint> DataPoints { get; }

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06004210 RID: 16912 RVA: 0x000CEA22 File Offset: 0x000CCC22
		public IReadOnlyList<PredicateFeature> DescriptiveFeatures { get; }

		// Token: 0x06004211 RID: 16913 RVA: 0x000CEA2C File Offset: 0x000CCC2C
		public Record<Cluster, Cluster>? Bisect(IReadOnlyList<HashSet<string>> inSameCluster, IReadOnlyList<HashSet<string>> inDifferentClusters)
		{
			if (this.DataPoints.Count <= 1)
			{
				return null;
			}
			int count = this.AllFeatures.Count;
			Record<Cluster, Cluster>? record = null;
			int i;
			int j;
			for (i = 0; i < count; i = j + 1)
			{
				int num = this.DataPoints.Count((DataPoint p) => p.FeatureVector[i]);
				if (num != 0 && num != this.DataPoints.Count)
				{
					Record<Cluster, Cluster> record2 = this.BisectUsingFeatureAt(i);
					if (inSameCluster.Count > 0 || inDifferentClusters.Count > 0)
					{
						HashSet<string> leftSet = record2.Item1.DataPoints.Select((DataPoint p) => p.Input).ConvertToHashSet<string>();
						if (inSameCluster.Any((HashSet<string> set) => !Cluster.<Bisect>g__SatisfySameCluster|11_2(leftSet, set)) || inDifferentClusters.Any((HashSet<string> set) => leftSet.Intersect(set).Count<string>() > 1))
						{
							goto IL_0171;
						}
					}
					if ((double)record2.Item1.DataPoints.Count > 0.3 * (double)this.DataPoints.Count)
					{
						return new Record<Cluster, Cluster>?(record2);
					}
					if (record2.Item1.DescriptiveFeatures.Last<PredicateFeature>().Type == PredicateType.IsNullOrWhiteSpace)
					{
						return new Record<Cluster, Cluster>?(record2);
					}
					Record<Cluster, Cluster> record3 = record.GetValueOrDefault();
					if (record == null)
					{
						record3 = record2;
						record = new Record<Cluster, Cluster>?(record3);
					}
				}
				IL_0171:
				j = i;
			}
			return record;
		}

		// Token: 0x06004212 RID: 16914 RVA: 0x000CEBCC File Offset: 0x000CCDCC
		private Record<Cluster, Cluster> BisectUsingFeatureAt(int bisectFeatureIndex)
		{
			List<DataPoint> list = new List<DataPoint>();
			List<DataPoint> list2 = new List<DataPoint>();
			foreach (DataPoint dataPoint in this.DataPoints)
			{
				if (dataPoint[bisectFeatureIndex])
				{
					list.Add(dataPoint);
				}
				else
				{
					list2.Add(dataPoint);
				}
			}
			PredicateFeature predicateFeature = this.AllFeatures[bisectFeatureIndex];
			Cluster cluster = new Cluster(list, this.AllFeatures, this.DescriptiveFeatures.AppendItem(predicateFeature).ToList<PredicateFeature>());
			BitArray bitArray = new BitArray(this.AllFeatures.Count);
			bitArray.SetAll(true);
			BitArray bitArray2 = list2.Select((DataPoint p) => p.FeatureVector).Aggregate(bitArray, (BitArray x, BitArray y) => x.And(y));
			BitArray bitArray3 = new BitArray(this.AllFeatures.Count);
			BitArray bitArray4 = list.Select((DataPoint p) => p.FeatureVector).Aggregate(bitArray3, (BitArray x, BitArray y) => x.Or(y)).Not();
			Optional<int> optional = bitArray2.And(bitArray4).OfType<bool>().IndexOf(true)
				.SomeIfNotNull<int>();
			PredicateFeature predicateFeature2 = (optional.HasValue ? this.AllFeatures[optional.Value] : predicateFeature.Negate());
			Cluster cluster2 = new Cluster(list2, this.AllFeatures, this.DescriptiveFeatures.AppendItem(predicateFeature2).ToList<PredicateFeature>());
			return Record.Create<Cluster, Cluster>(cluster, cluster2);
		}

		// Token: 0x06004214 RID: 16916 RVA: 0x000CEDB4 File Offset: 0x000CCFB4
		[CompilerGenerated]
		internal static bool <Bisect>g__SatisfySameCluster|11_2(HashSet<string> inputs, HashSet<string> sameSet)
		{
			List<string> list = inputs.Intersect(sameSet).ToList<string>();
			return list.Count == 0 || list.Count == sameSet.Count;
		}
	}
}
