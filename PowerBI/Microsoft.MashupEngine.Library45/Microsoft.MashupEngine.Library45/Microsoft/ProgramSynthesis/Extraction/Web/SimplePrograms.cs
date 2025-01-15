using System;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.AST.Extensions;
using Microsoft.ProgramSynthesis.Extraction.Web.Learning;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FCD RID: 4045
	public class SimplePrograms<TInput, TOutput> : Constraint<TInput, TOutput>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x06006F98 RID: 28568 RVA: 0x0016C77B File Offset: 0x0016A97B
		public override bool Valid(Program<TInput, TOutput> program)
		{
			return program.ProgramNode.EnumerateDescendants().All((ProgramNode node) => !SimplePrograms<TInput, TOutput>.NonSimpleRules.Contains(node.GrammarRule));
		}

		// Token: 0x06006F99 RID: 28569 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<TInput, TOutput> other)
		{
			return false;
		}

		// Token: 0x06006F9A RID: 28570 RVA: 0x0016C7AC File Offset: 0x0016A9AC
		public void SetOptions(Witnesses.Options options)
		{
			options.LearnSimplePrograms = true;
		}

		// Token: 0x06006F9B RID: 28571 RVA: 0x0016C7B5 File Offset: 0x0016A9B5
		public bool Equals(SimplePrograms<TInput, TOutput> other)
		{
			return other != null;
		}

		// Token: 0x06006F9C RID: 28572 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool Equals(Constraint<TInput, TOutput> other)
		{
			return this.Equals(other);
		}

		// Token: 0x06006F9D RID: 28573 RVA: 0x0016C7C1 File Offset: 0x0016A9C1
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((SimplePrograms<TInput, TOutput>)obj)));
		}

		// Token: 0x06006F9E RID: 28574 RVA: 0x0016C7EF File Offset: 0x0016A9EF
		public override int GetHashCode()
		{
			return 491;
		}

		// Token: 0x0400308C RID: 12428
		private static readonly BlackBoxRule[] NonSimpleRules = new BlackBoxRule[]
		{
			Language.Build.Rule.ContainsDate,
			Language.Build.Rule.ContainsNum,
			Language.Build.Rule.ContainsLeafNodes,
			Language.Build.Rule.ChildrenCount,
			Language.Build.Rule.HasEntityAnchor
		};
	}
}
