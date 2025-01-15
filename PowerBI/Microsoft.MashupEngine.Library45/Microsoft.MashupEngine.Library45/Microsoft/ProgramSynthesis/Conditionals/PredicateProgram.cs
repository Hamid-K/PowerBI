using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;
using Microsoft.ProgramSynthesis.DslLibrary;

namespace Microsoft.ProgramSynthesis.Conditionals
{
	// Token: 0x02000A0F RID: 2575
	public class PredicateProgram : Program<string, bool>
	{
		// Token: 0x06003E15 RID: 15893 RVA: 0x000C150C File Offset: 0x000BF70C
		public PredicateProgram(conjunct node)
			: base(node.Node, node.Node.GetFeatureValue<double>(Learner.Instance.ScoreFeature, null), null)
		{
			this._description = new Lazy<string>(() => PredicateProgram.DescribeConjunct(node));
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x000C156A File Offset: 0x000BF76A
		public override string Describe(CultureInfo cultureInfo = null)
		{
			return this._description.Value;
		}

		// Token: 0x06003E17 RID: 15895 RVA: 0x000C1577 File Offset: 0x000BF777
		public static string DescribeConjunct(conjunct node)
		{
			string text = " and ";
			IEnumerable<pred> enumerable = node.Node.AcceptVisitor<IEnumerable<pred>>(new ConjunctCollector());
			Func<pred, string> func;
			if ((func = PredicateProgram.<>O.<0>__DescribePredicate) == null)
			{
				func = (PredicateProgram.<>O.<0>__DescribePredicate = new Func<pred, string>(PredicateProgram.DescribePredicate));
			}
			return string.Join(text, enumerable.Select(func));
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x000C15B4 File Offset: 0x000BF7B4
		public static string DescribePredicate(pred predicate)
		{
			string text = predicate.Node.PrintAST(ASTSerializationFormat.HumanReadable);
			string text2 = text.Replace("\\\"", string.Empty);
			if (text2.Length == text.Length)
			{
				text2 = text2.Replace("\"", string.Empty);
			}
			return Regex.Replace(text2, "\\(s(, )?", "(");
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x000C1610 File Offset: 0x000BF810
		public override bool Run(string input)
		{
			State state = State.CreateForExecution(Language.Grammar.InputSymbol, (input == null) ? null : new StringRegion(new StringLearningCache(input, null)));
			object obj = base.ProgramNode.Invoke(state);
			return obj != null && (bool)obj;
		}

		// Token: 0x04001CDE RID: 7390
		private readonly Lazy<string> _description;

		// Token: 0x02000A10 RID: 2576
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001CDF RID: 7391
			public static Func<pred, string> <0>__DescribePredicate;
		}
	}
}
