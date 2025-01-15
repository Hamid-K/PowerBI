using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Utils.Clustering
{
	// Token: 0x02000620 RID: 1568
	public struct ClusterPredicate<TNode> where TNode : IProgramNodeBuilder
	{
		// Token: 0x060021FD RID: 8701 RVA: 0x00060CE1 File Offset: 0x0005EEE1
		internal ClusterPredicate(TNode program, double programScore, string description, Predicate<object> compiledProgram, Func<State, object> extractData)
		{
			this.Program = program;
			this.Score = programScore;
			this.CompiledProgram = compiledProgram;
			this.ExtractData = extractData;
			this.Description = description;
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x00060D08 File Offset: 0x0005EF08
		public Cluster<TNode> ToCluster(IReadOnlyList<State> generators, IReadOnlyList<State> allMatchedStates, uint cardinality)
		{
			return new Cluster<TNode>(ProgramSetBuilder.List<TNode>(new TNode[] { this.Program }), this.Program, this.Score, generators, allMatchedStates, cardinality, this.Description);
		}

		// Token: 0x0400103F RID: 4159
		internal readonly TNode Program;

		// Token: 0x04001040 RID: 4160
		internal readonly double Score;

		// Token: 0x04001041 RID: 4161
		internal readonly string Description;

		// Token: 0x04001042 RID: 4162
		internal readonly Predicate<object> CompiledProgram;

		// Token: 0x04001043 RID: 4163
		internal readonly Func<State, object> ExtractData;
	}
}
