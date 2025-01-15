using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Matching.Text.Build;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Clustering;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Matching.Text.Learning
{
	// Token: 0x0200120B RID: 4619
	internal class SortedProgramSetIntersections : IEnumerable<ClusterIntersection>, IEnumerable
	{
		// Token: 0x06008B37 RID: 35639 RVA: 0x001D27C8 File Offset: 0x001D09C8
		public SortedProgramSetIntersections(SynthesisEngine engine, IReadOnlyDictionary<State, uint> examplesWithCounts, LearningTask taskWithoutK, OperatorRule rule, Feature<double> score)
		{
			this._engine = engine;
			this._examplesWithCounts = examplesWithCounts;
			this._build = GrammarBuilders.Instance(this._engine.Grammar);
			this._rule = rule;
			this._score = score;
			this._task = taskWithoutK;
			this._topTask = this._task.WithTopKRequest(1, this._score, null);
			this._sortedList = new SortedList<double, List<ClusterIntersection>>();
		}

		// Token: 0x170017DB RID: 6107
		// (get) Token: 0x06008B38 RID: 35640 RVA: 0x001D283A File Offset: 0x001D0A3A
		public KeyValuePair<double, List<ClusterIntersection>> Best
		{
			get
			{
				return this._sortedList.LastOrDefault<KeyValuePair<double, List<ClusterIntersection>>>();
			}
		}

		// Token: 0x06008B39 RID: 35641 RVA: 0x001D2848 File Offset: 0x001D0A48
		public void FilterOn(IEnumerable<Cluster<match>> range)
		{
			HashSet<Cluster<match>> vsaSet = new HashSet<Cluster<match>>(range);
			Predicate<ClusterIntersection> <>9__0;
			foreach (KeyValuePair<double, List<ClusterIntersection>> keyValuePair in this._sortedList.ToList<KeyValuePair<double, List<ClusterIntersection>>>())
			{
				List<ClusterIntersection> value = keyValuePair.Value;
				Predicate<ClusterIntersection> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = (ClusterIntersection intersection) => !vsaSet.Contains(intersection.LeftCluster) || !vsaSet.Contains(intersection.RightCluster));
				}
				value.RemoveAll(predicate);
				if (!keyValuePair.Value.Any<ClusterIntersection>())
				{
					this._sortedList.Remove(keyValuePair.Key);
				}
			}
		}

		// Token: 0x06008B3A RID: 35642 RVA: 0x001D28F8 File Offset: 0x001D0AF8
		public void AddPair(Cluster<match> cluster1, Cluster<match> cluster2, CancellationToken cancel = default(CancellationToken))
		{
			List<State> list = cluster1.Data.Concat(cluster2.Data).ToList<State>();
			ExampleSpec exampleSpec = new ExampleSpec(list.ToDictionary((State state) => state, (State state) => true));
			LearningTask learningTask = this._task.MakeSubtask(this._rule, 0, exampleSpec);
			ProgramSetBuilder<match> programSetBuilder = this._build.Set.Cast.match(this._engine.Learn(learningTask, cancel));
			if (ProgramSetBuilder.IsNullOrEmpty<match>(programSetBuilder))
			{
				return;
			}
			match match = this._build.Node.Cast.match(this._engine.Learn(this._topTask.MakeSubtask(this._rule, 0, exampleSpec), default(CancellationToken)).RealizedPrograms.First<ProgramNode>());
			List<State> list2 = cluster1.AllMatchingData.Concat(cluster2.AllMatchingData).Distinct<State>().ToList<State>();
			Cluster<match> cluster3 = new Cluster<match>(programSetBuilder, match, match.Node.GetFeatureValue<double>(this._score, new LearningInfo(FeatureCalculationContext.Create(list, null, null), match.Node)), exampleSpec.ProvidedInputs.ToList<State>(), list2, (uint)list2.Sum((State ex) => (long)((ulong)this._examplesWithCounts[ex])), null);
			this._sortedList.GetOrCreateValue(cluster3.BestProgramScore).Add(new ClusterIntersection(cluster1, cluster2, cluster3));
		}

		// Token: 0x06008B3B RID: 35643 RVA: 0x001D2A80 File Offset: 0x001D0C80
		public void AddAllPairsWith(Cluster<match> newCluster, IEnumerable<Cluster<match>> clusters, CancellationToken cancel = default(CancellationToken))
		{
			foreach (Cluster<match> cluster in clusters)
			{
				this.AddPair(cluster, newCluster, cancel);
			}
		}

		// Token: 0x06008B3C RID: 35644 RVA: 0x001D2ACC File Offset: 0x001D0CCC
		public IEnumerator<ClusterIntersection> GetEnumerator()
		{
			foreach (KeyValuePair<double, List<ClusterIntersection>> keyValuePair in this._sortedList)
			{
				foreach (ClusterIntersection clusterIntersection in keyValuePair.Value)
				{
					yield return clusterIntersection;
				}
				List<ClusterIntersection>.Enumerator enumerator2 = default(List<ClusterIntersection>.Enumerator);
			}
			IEnumerator<KeyValuePair<double, List<ClusterIntersection>>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06008B3D RID: 35645 RVA: 0x001D2ADB File Offset: 0x001D0CDB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040038C5 RID: 14533
		private readonly SynthesisEngine _engine;

		// Token: 0x040038C6 RID: 14534
		private readonly GrammarBuilders _build;

		// Token: 0x040038C7 RID: 14535
		private readonly OperatorRule _rule;

		// Token: 0x040038C8 RID: 14536
		private readonly Feature<double> _score;

		// Token: 0x040038C9 RID: 14537
		private readonly SortedList<double, List<ClusterIntersection>> _sortedList;

		// Token: 0x040038CA RID: 14538
		private readonly LearningTask _task;

		// Token: 0x040038CB RID: 14539
		private readonly LearningTask _topTask;

		// Token: 0x040038CC RID: 14540
		private readonly IReadOnlyDictionary<State, uint> _examplesWithCounts;
	}
}
