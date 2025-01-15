using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners
{
	// Token: 0x02000750 RID: 1872
	internal class LiteralLearner : StdRuleLearner<TerminalRule, Spec>
	{
		// Token: 0x06002819 RID: 10265 RVA: 0x00071A54 File Offset: 0x0006FC54
		public LiteralLearner(TerminalRule rule)
			: base(rule)
		{
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x00071A60 File Offset: 0x0006FC60
		protected override Optional<ProgramSet> LearnRule(SynthesisEngine engine, TerminalRule rule, LearningTask<Spec> task, CancellationToken cancel)
		{
			Spec spec = task.Spec;
			if (rule.IsInput)
			{
				if (spec.ProvidedInputs.Any((State input) => !spec.Valid(input, input[rule.Head])))
				{
					return ProgramSet.Empty(rule.Head).Some<ProgramSet>();
				}
				return ProgramSet.List(rule.Head, new ProgramNode[]
				{
					new VariableNode(rule.Head)
				}).Some<ProgramSet>();
			}
			else
			{
				LiteralGenerator literalGenerator = engine.BindingManager.Generator(rule);
				if (literalGenerator == null)
				{
					return ProgramSet.Empty(rule.Head).Some<ProgramSet>();
				}
				IEnumerable<object> enumerable = literalGenerator();
				if (enumerable == null)
				{
					return ProgramSet.Empty(rule.Head).Some<ProgramSet>();
				}
				IReadOnlyList<ProgramNode> readOnlyList = enumerable.Where(new Func<object, bool>(spec.CorrectOnAllProvided)).Select(new Func<object, ProgramNode>(rule.BuildASTNode)).ToList<ProgramNode>();
				return new DirectProgramSet(rule.Head, readOnlyList).Some<ProgramSet>();
			}
		}
	}
}
