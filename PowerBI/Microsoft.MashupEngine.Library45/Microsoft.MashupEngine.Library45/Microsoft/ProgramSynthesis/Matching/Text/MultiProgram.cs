using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Semantics;

namespace Microsoft.ProgramSynthesis.Matching.Text
{
	// Token: 0x020011B8 RID: 4536
	public class MultiProgram : Program<IEnumerable<string>, IEnumerable<bool>>
	{
		// Token: 0x06008705 RID: 34565 RVA: 0x001C57BB File Offset: 0x001C39BB
		internal MultiProgram(ProgramNode node)
			: base(node, 0.0, null)
		{
			this.LabeledProgramNode = base.ProgramNode.GetFeatureValue<ProgramNode>(Learner.Instance.LabelFeature, null);
		}

		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x06008706 RID: 34566 RVA: 0x001C57EA File Offset: 0x001C39EA
		public ProgramNode LabeledProgramNode { get; }

		// Token: 0x06008707 RID: 34567 RVA: 0x001C57F4 File Offset: 0x001C39F4
		public override IEnumerable<bool> Run(IEnumerable<string> input)
		{
			return base.ProgramNode.Invoke(State.CreateForExecution(Language.Build.Symbol.inputSRegions, input.Select((string s) => new SuffixRegion(s, 0U)).ToArray<SuffixRegion>())) as IEnumerable<bool>;
		}

		// Token: 0x06008708 RID: 34568 RVA: 0x001C5850 File Offset: 0x001C3A50
		public IImmutableList<MatchingLabel> GetLabels(IEnumerable<string> input)
		{
			return this.LabeledProgramNode.Invoke(State.CreateForExecution(Language.Build.Symbol.inputSRegions, input.Select((string s) => new SuffixRegion(s, 0U)).ToArray<SuffixRegion>())) as IImmutableList<MatchingLabel>;
		}
	}
}
