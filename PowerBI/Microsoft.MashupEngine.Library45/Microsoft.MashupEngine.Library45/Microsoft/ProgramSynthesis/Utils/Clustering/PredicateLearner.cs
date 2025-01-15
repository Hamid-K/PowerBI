using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Utils.Clustering
{
	// Token: 0x02000622 RID: 1570
	public class PredicateLearner<TNode> : IPredicateLearner<TNode> where TNode : struct, IProgramNodeBuilder
	{
		// Token: 0x06002200 RID: 8704 RVA: 0x00060D3C File Offset: 0x0005EF3C
		public PredicateLearner(LearningTask task, Feature<double> score, SynthesisEngine synthesisEngine, Func<ProgramNode, TNode> cast, Func<State, object> extract, Func<TNode, Predicate<object>> compile, Func<TNode, string> describe)
		{
			this._task = task;
			this._score = score;
			this._synthesisEngine = synthesisEngine;
			this._cast = cast;
			this._extract = extract;
			this._compile = compile;
			this._describe = describe;
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x00060D7C File Offset: 0x0005EF7C
		public ClusterPredicate<TNode>? Learn(IEnumerable<State> samples, CancellationToken cancel)
		{
			ExampleSpec exampleSpec = new ExampleSpec(samples.ToDictionary((State state) => state, (State state) => true));
			LearningTask learningTask = this._task.Clone(null, exampleSpec);
			ProgramSet programSet = this._synthesisEngine.Learn(learningTask, cancel);
			TNode? tnode = ((programSet != null) ? programSet.TopK(this._score, 1, null, null).Select(this._cast).FirstOrNull<TNode>() : null);
			if (tnode == null)
			{
				return null;
			}
			TNode value = tnode.Value;
			double featureValue = value.Node.GetFeatureValue<double>(this._score, null);
			Func<TNode, Predicate<object>> compile = this._compile;
			Predicate<object> predicate = ((compile != null) ? compile(tnode.Value) : null);
			string text = this._describe(tnode.Value);
			return new ClusterPredicate<TNode>?(new ClusterPredicate<TNode>(tnode.Value, featureValue, text, predicate, this._extract));
		}

		// Token: 0x04001044 RID: 4164
		private readonly SynthesisEngine _synthesisEngine;

		// Token: 0x04001045 RID: 4165
		private readonly LearningTask _task;

		// Token: 0x04001046 RID: 4166
		private readonly Feature<double> _score;

		// Token: 0x04001047 RID: 4167
		private readonly Func<ProgramNode, TNode> _cast;

		// Token: 0x04001048 RID: 4168
		private readonly Func<State, object> _extract;

		// Token: 0x04001049 RID: 4169
		private readonly Func<TNode, Predicate<object>> _compile;

		// Token: 0x0400104A RID: 4170
		private readonly Func<TNode, string> _describe;
	}
}
