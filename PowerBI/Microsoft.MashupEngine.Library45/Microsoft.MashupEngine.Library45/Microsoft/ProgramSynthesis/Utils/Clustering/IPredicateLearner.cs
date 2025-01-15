using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Utils.Clustering
{
	// Token: 0x02000621 RID: 1569
	public interface IPredicateLearner<TNode> where TNode : IProgramNodeBuilder
	{
		// Token: 0x060021FF RID: 8703
		ClusterPredicate<TNode>? Learn(IEnumerable<State> samples, CancellationToken cancel);
	}
}
