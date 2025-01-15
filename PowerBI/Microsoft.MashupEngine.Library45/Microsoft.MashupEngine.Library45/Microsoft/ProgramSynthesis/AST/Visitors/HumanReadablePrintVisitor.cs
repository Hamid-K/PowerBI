using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.AST.Visitors
{
	// Token: 0x020008EA RID: 2282
	public class HumanReadablePrintVisitor : ProgramNodeVisitor<CodeBuilder>
	{
		// Token: 0x0600314F RID: 12623 RVA: 0x000917AC File Offset: 0x0008F9AC
		public HumanReadablePrintVisitor(ASTSerializationSettings settings)
		{
			this._settings = settings;
			if (settings.HasOmitLiterals)
			{
				this._uniqueLiterals = new List<object>();
			}
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x000917D0 File Offset: 0x0008F9D0
		public override CodeBuilder VisitNonterminal(NonterminalNode node)
		{
			CodeBuilder codeBuilder = this.StartWithPrefix(node);
			ProgramNode programNode = node;
			ConceptRule conceptRule = node.Rule as ConceptRule;
			if (conceptRule != null)
			{
				programNode = conceptRule.BuildDslASTFromConceptAST(node);
			}
			codeBuilder.Append(node.Rule.FormatAST(programNode.Children.Select((ProgramNode n) => n.AcceptVisitor<CodeBuilder>(this)), this._settings));
			return codeBuilder;
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x0009182B File Offset: 0x0008FA2B
		public override CodeBuilder VisitLet(LetNode node)
		{
			return this.VisitNonterminal(node);
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x0009182B File Offset: 0x0008FA2B
		public override CodeBuilder VisitLambda(LambdaNode node)
		{
			return this.VisitNonterminal(node);
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x00091834 File Offset: 0x0008FA34
		public override CodeBuilder VisitLiteral(LiteralNode node)
		{
			CodeBuilder codeBuilder = this.StartWithPrefix(node);
			if (this._settings.HasOmitLiterals)
			{
				codeBuilder.Append(node.Symbol.Name);
				int num = 0;
				while (num < this._uniqueLiterals.Count && !ValueEquality.Comparer.Equals(node.Value, this._uniqueLiterals[num]))
				{
					num++;
				}
				if (num == this._uniqueLiterals.Count)
				{
					this._uniqueLiterals.Add(node.Value);
				}
				codeBuilder.Append("#");
				codeBuilder.Append(num.ToString());
			}
			else
			{
				IRenderableLiteral renderableLiteral = node.Value as IRenderableLiteral;
				string text = ((renderableLiteral != null) ? renderableLiteral.RenderHumanReadable() : node.Value.ToLiteral(null));
				codeBuilder.Append(text);
			}
			return codeBuilder;
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x00091906 File Offset: 0x0008FB06
		public override CodeBuilder VisitVariable(VariableNode node)
		{
			CodeBuilder codeBuilder = this.StartWithPrefix(node);
			codeBuilder.Append(node.Symbol.Name);
			return codeBuilder;
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x00091920 File Offset: 0x0008FB20
		public override CodeBuilder VisitHole(Hole node)
		{
			CodeBuilder codeBuilder = this.StartWithPrefix(node);
			codeBuilder.Append("?");
			codeBuilder.Append(node.Symbol.Name);
			if (node.HoleId != null)
			{
				codeBuilder.Append("?");
				codeBuilder.Append(node.HoleId);
			}
			return codeBuilder;
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x00091974 File Offset: 0x0008FB74
		private CodeBuilder StartWithPrefix(ProgramNode node)
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			if (this._settings.HasIds)
			{
				codeBuilder.Append(FormattableString.Invariant(FormattableStringFactory.Create("{{{0}}}", new object[] { node.Id })));
			}
			return codeBuilder;
		}

		// Token: 0x04001893 RID: 6291
		private readonly ASTSerializationSettings _settings;

		// Token: 0x04001894 RID: 6292
		private readonly List<object> _uniqueLiterals;
	}
}
