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
	// Token: 0x02000752 RID: 1874
	internal class LiteralLearner_Examples : StdRuleLearner<TerminalRule, DisjunctiveExamplesSpec>
	{
		// Token: 0x0600281D RID: 10269 RVA: 0x00071BAD File Offset: 0x0006FDAD
		public LiteralLearner_Examples(TerminalRule rule)
			: base(rule)
		{
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x00071BB8 File Offset: 0x0006FDB8
		protected override Optional<ProgramSet> LearnRule(SynthesisEngine engine, TerminalRule rule, LearningTask<DisjunctiveExamplesSpec> task, CancellationToken cancel)
		{
			DisjunctiveExamplesSpec spec = task.Spec;
			if (!rule.IsInput)
			{
				HashSet<object> hashSet = spec.DisjunctiveExamples.SelectMany((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Value).ConvertToHashSet(ValueEquality.Comparer);
				if (engine.BindingManager.ExplicitGenerator(rule) != null)
				{
					IEnumerable<object> enumerable = engine.BindingManager.Generator(rule)();
					if (enumerable != null)
					{
						hashSet.IntersectWith(enumerable);
					}
				}
				IEnumerable<ProgramNode> enumerable2 = hashSet.Where(new Func<object, bool>(spec.CorrectOnAllProvided)).Select(new Func<object, ProgramNode>(rule.BuildASTNode));
				return new DirectProgramSet(rule.Head, enumerable2).Some<ProgramSet>();
			}
			if (spec.ProvidedInputs.Any((State input) => !spec.Valid(input, input[rule.Head])))
			{
				return ProgramSet.Empty(rule.Head).Some<ProgramSet>();
			}
			return ProgramSet.List(rule.Head, new ProgramNode[]
			{
				new VariableNode(rule.Head)
			}).Some<ProgramSet>();
		}
	}
}
