using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils.Caching;

namespace Microsoft.ProgramSynthesis.Utils.Clustering
{
	// Token: 0x020005FE RID: 1534
	internal class ClusterPool<TNode> : IEnumerable<Cluster<TNode>>, IEnumerable where TNode : IProgramNodeBuilder
	{
		// Token: 0x0600216B RID: 8555 RVA: 0x0005EF64 File Offset: 0x0005D164
		internal ClusterPool(IReadOnlyList<State> examples, IReadOnlyList<uint> exampleCounts, ClusterConstraints clusterConstraints)
		{
			this._examples = examples;
			this._exampleCounts = exampleCounts;
			this._knownClusters = new List<Cluster<TNode>>();
			this._clusterSubsets = new Dictionary<Cluster<TNode>, ExampleSubset>();
			this._clusterConstraints = clusterConstraints;
			this._clusterDescriptions = new Dictionary<Cluster<TNode>, string>();
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x0005EFB8 File Offset: 0x0005D1B8
		internal int Count
		{
			get
			{
				return this._knownClusters.Count;
			}
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0005EFC5 File Offset: 0x0005D1C5
		public IEnumerator<Cluster<TNode>> GetEnumerator()
		{
			return this._knownClusters.GetEnumerator();
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x0005EFD7 File Offset: 0x0005D1D7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this._knownClusters).GetEnumerator();
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x0005EFE4 File Offset: 0x0005D1E4
		internal Cluster<TNode> AddClusterFromPredicate(ClusterPredicate<TNode> clusterPredicate, IReadOnlyList<State> generators)
		{
			TNode program = clusterPredicate.Program;
			if (this.FindCluster(program) != null)
			{
				return null;
			}
			if (!this.Valid(clusterPredicate.CompiledProgram))
			{
				return null;
			}
			Predicate<State> predicate = ((clusterPredicate.CompiledProgram != null) ? ((State state) => clusterPredicate.CompiledProgram(clusterPredicate.ExtractData(state))) : delegate(State state)
			{
				TNode program2 = clusterPredicate.Program;
				return (bool)program2.Node.Invoke(state);
			});
			ExampleSubset exampleSubset = new ExampleSubset(this._examples, this._exampleCounts, predicate);
			Cluster<TNode> cluster = clusterPredicate.ToCluster(generators, exampleSubset.States, exampleSubset.Cardinality);
			this._knownClusters.Add(cluster);
			this._clusterSubsets[cluster] = exampleSubset;
			this._clusterDescriptions[cluster] = clusterPredicate.Description;
			return cluster;
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x0005F0C0 File Offset: 0x0005D2C0
		internal bool Valid(Predicate<object> pred)
		{
			Func<object, bool> <>9__0;
			Func<object, bool> <>9__1;
			foreach (IReadOnlyList<object> readOnlyList in this._clusterConstraints.InSameCluster)
			{
				IEnumerable<object> enumerable = readOnlyList;
				Func<object, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (object i) => pred(i));
				}
				if (enumerable.Any(func))
				{
					IEnumerable<object> enumerable2 = readOnlyList;
					Func<object, bool> func2;
					if ((func2 = <>9__1) == null)
					{
						func2 = (<>9__1 = (object i) => !pred(i));
					}
					if (enumerable2.Any(func2))
					{
						return false;
					}
				}
			}
			Func<object, bool> <>9__2;
			foreach (IEnumerable<object> enumerable3 in this._clusterConstraints.InDifferentCluster)
			{
				Func<object, bool> func3;
				if ((func3 = <>9__2) == null)
				{
					func3 = (<>9__2 = (object i) => pred(i));
				}
				if (enumerable3.Count(func3) > 1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0005F1D4 File Offset: 0x0005D3D4
		internal Cluster<TNode> FindCluster(TNode program)
		{
			return this._knownClusters.Find(delegate(Cluster<TNode> cluster)
			{
				TNode bestProgramNode = cluster.BestProgramNode;
				return bestProgramNode.Equals(program);
			});
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x0005F205 File Offset: 0x0005D405
		internal bool Intersects(Cluster<TNode> cluster1, Cluster<TNode> cluster2)
		{
			return this._intersectsCache.LookupOrCompute(Record.Create<Cluster<TNode>, Cluster<TNode>>(cluster1, cluster2), (Record<Cluster<TNode>, Cluster<TNode>> cs) => this._clusterSubsets[cs.Item1].HasIntersectionWith(this._clusterSubsets[cs.Item2]));
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x0005F228 File Offset: 0x0005D428
		internal bool ArePairwiseDisjoint(IReadOnlyList<Cluster<TNode>> clusters)
		{
			for (int i = 0; i < clusters.Count; i++)
			{
				for (int j = i + 1; j < clusters.Count; j++)
				{
					if (this.Intersects(clusters[i], clusters[j]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x0005F272 File Offset: 0x0005D472
		internal ExampleSubset UnionComplement(IEnumerable<Cluster<TNode>> clusters)
		{
			return ExampleSubset.UnionComplement(this._examples, this._exampleCounts, clusters.Select((Cluster<TNode> c) => this._clusterSubsets[c]));
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x0005F297 File Offset: 0x0005D497
		internal Cluster<TNode> Sample(Random rng)
		{
			return this._knownClusters.RandomElement(rng);
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x0005F2A8 File Offset: 0x0005D4A8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Cluster<TNode> cluster in this._knownClusters.OrderByDescending((Cluster<TNode> c) => c.DataCount))
			{
				stringBuilder.AppendLine(string.Format("{0,7} {1}", cluster.DataCount, this._clusterDescriptions[cluster]));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000FD9 RID: 4057
		private readonly Dictionary<Cluster<TNode>, ExampleSubset> _clusterSubsets;

		// Token: 0x04000FDA RID: 4058
		private readonly IReadOnlyList<uint> _exampleCounts;

		// Token: 0x04000FDB RID: 4059
		private readonly IReadOnlyList<State> _examples;

		// Token: 0x04000FDC RID: 4060
		private readonly UnboundedCache<Record<Cluster<TNode>, Cluster<TNode>>, bool> _intersectsCache = new UnboundedCache<Record<Cluster<TNode>, Cluster<TNode>>, bool>();

		// Token: 0x04000FDD RID: 4061
		private readonly List<Cluster<TNode>> _knownClusters;

		// Token: 0x04000FDE RID: 4062
		private readonly ClusterConstraints _clusterConstraints;

		// Token: 0x04000FDF RID: 4063
		private readonly IDictionary<Cluster<TNode>, string> _clusterDescriptions;
	}
}
