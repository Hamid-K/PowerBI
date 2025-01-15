using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals
{
	// Token: 0x02000A12 RID: 2578
	public class Program : Program<IEnumerable<string>, IEnumerable<IEnumerable<string>>>
	{
		// Token: 0x06003E1C RID: 15900 RVA: 0x000C1664 File Offset: 0x000BF864
		internal Program(output node)
			: base(node.Node, node.Node.GetFeatureValue<double>(Learner.Instance.ScoreFeature, null), null)
		{
			this._predicatePrograms = new Lazy<IReadOnlyList<PredicateProgram>>(() => (from n in base.ProgramNode.AcceptVisitor<IEnumerable<conjunct>>(new DisjunctCollector())
				select new PredicateProgram(n)).ToList<PredicateProgram>());
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06003E1D RID: 15901 RVA: 0x000C16A2 File Offset: 0x000BF8A2
		public IReadOnlyList<PredicateProgram> PredicatePrograms
		{
			get
			{
				return this._predicatePrograms.Value;
			}
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x000C16B0 File Offset: 0x000BF8B0
		public override IEnumerable<IEnumerable<string>> Run(IEnumerable<string> inputs)
		{
			List<string>[] array = new List<string>[this.PredicatePrograms.Count];
			for (int i = 0; i < this.PredicatePrograms.Count; i++)
			{
				array[i] = new List<string>();
			}
			foreach (string text in inputs)
			{
				for (int j = 0; j < this.PredicatePrograms.Count; j++)
				{
					if (this.PredicatePrograms[j].Run(text))
					{
						array[j].Add(text);
						break;
					}
				}
			}
			return array;
		}

		// Token: 0x04001CE1 RID: 7393
		private readonly Lazy<IReadOnlyList<PredicateProgram>> _predicatePrograms;
	}
}
