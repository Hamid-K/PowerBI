using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.AST.Extensions
{
	// Token: 0x020008F1 RID: 2289
	public static class ProgramNodeExtensions
	{
		// Token: 0x06003180 RID: 12672 RVA: 0x00091EA4 File Offset: 0x000900A4
		public static ProgramNode PreTraverse(this ProgramNode program, Action<ProgramNode> action)
		{
			action(program);
			ProgramNode[] children = program.Children;
			for (int i = 0; i < children.Length; i++)
			{
				children[i].PreTraverse(action);
			}
			return program;
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x00091ED8 File Offset: 0x000900D8
		public static ProgramNode PreMap(this ProgramNode program, Func<ProgramNode, int, ProgramNode, ProgramNode> transformation, ProgramNode parent = null, int indexInParent = -1)
		{
			ProgramNode programNode = transformation(parent, indexInParent, program) ?? program.Clone();
			int num = 0;
			ProgramNode[] children = programNode.Children;
			for (int i = 0; i < children.Length; i++)
			{
				ProgramNode programNode2 = children[i].PreMap(transformation, programNode, num);
				if (programNode2 != null)
				{
					programNode.Children[num] = programNode2;
				}
				num++;
			}
			return programNode;
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x00091F38 File Offset: 0x00090138
		public static void PostTraverse(this ProgramNode program, Action<ProgramNode> action)
		{
			ProgramNode[] children = program.Children;
			for (int i = 0; i < children.Length; i++)
			{
				children[i].PostTraverse(action);
			}
			action(program);
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x00091F6A File Offset: 0x0009016A
		public static ProgramNode AddConversionRules(this ProgramNode program, IEnumerable<ConversionRule> conversions)
		{
			return conversions.Aggregate(program, (ProgramNode current, ConversionRule conversionRule) => conversionRule.BuildASTNode(current));
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x00091F94 File Offset: 0x00090194
		public static ProgramNode AddConversionRules(this ProgramNode program, Symbol symbol)
		{
			IImmutableList<ConversionRule> immutableList = ((program != null) ? program.Symbol.ConversionRulesTo(symbol) : null);
			if (immutableList != null)
			{
				return program.AddConversionRules(immutableList);
			}
			return null;
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x00091FC0 File Offset: 0x000901C0
		public static IEnumerable<Symbol> GetFreeVariables(this ProgramNode node)
		{
			return node.AcceptVisitor<IImmutableSet<Symbol>>(ProgramNodeExtensions.FreeVariableVisitor.Instance);
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x00091FCD File Offset: 0x000901CD
		public static IEnumerable<ProgramNode> EnumerateDescendants(this ProgramNode node)
		{
			IEnumerable<ProgramNode> children = node.Children;
			Func<ProgramNode, IEnumerable<ProgramNode>> func;
			if ((func = ProgramNodeExtensions.<>O.<0>__EnumerateDescendants) == null)
			{
				func = (ProgramNodeExtensions.<>O.<0>__EnumerateDescendants = new Func<ProgramNode, IEnumerable<ProgramNode>>(ProgramNodeExtensions.EnumerateDescendants));
			}
			return children.SelectMany(func).PrependItem(node);
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x00091FFB File Offset: 0x000901FB
		public static void ClearCaches(this ProgramNode node)
		{
			node.PreTraverse(delegate(ProgramNode node)
			{
				node.ClearFeatureCache();
			});
		}

		// Token: 0x020008F2 RID: 2290
		private class FreeVariableVisitor : ProgramNodeVisitor<IImmutableSet<Symbol>>
		{
			// Token: 0x170008B1 RID: 2225
			// (get) Token: 0x06003188 RID: 12680 RVA: 0x00092023 File Offset: 0x00090223
			public static ProgramNodeExtensions.FreeVariableVisitor Instance { get; } = new ProgramNodeExtensions.FreeVariableVisitor();

			// Token: 0x06003189 RID: 12681 RVA: 0x0009202A File Offset: 0x0009022A
			public override IImmutableSet<Symbol> VisitNonterminal(NonterminalNode node)
			{
				return node.Children.Select((ProgramNode child) => child.AcceptVisitor<IImmutableSet<Symbol>>(this)).Aggregate((IImmutableSet<Symbol> a, IImmutableSet<Symbol> b) => a.Union(b));
			}

			// Token: 0x0600318A RID: 12682 RVA: 0x00092068 File Offset: 0x00090268
			public override IImmutableSet<Symbol> VisitLet(LetNode node)
			{
				IImmutableSet<Symbol> immutableSet = node.ValueNode.AcceptVisitor<IImmutableSet<Symbol>>(this);
				IImmutableSet<Symbol> immutableSet2 = node.BodyNode.AcceptVisitor<IImmutableSet<Symbol>>(this).Remove(node.LetRule.Variable);
				return immutableSet.Union(immutableSet2);
			}

			// Token: 0x0600318B RID: 12683 RVA: 0x000920A4 File Offset: 0x000902A4
			public override IImmutableSet<Symbol> VisitLambda(LambdaNode node)
			{
				return node.BodyNode.AcceptVisitor<IImmutableSet<Symbol>>(this).Remove(((LambdaRule)node.Rule).Variable);
			}

			// Token: 0x0600318C RID: 12684 RVA: 0x000920C7 File Offset: 0x000902C7
			public override IImmutableSet<Symbol> VisitLiteral(LiteralNode node)
			{
				return ImmutableHashSet<Symbol>.Empty;
			}

			// Token: 0x0600318D RID: 12685 RVA: 0x000920CE File Offset: 0x000902CE
			public override IImmutableSet<Symbol> VisitVariable(VariableNode node)
			{
				return ImmutableHashSet.Create<Symbol>(node.Symbol);
			}

			// Token: 0x0600318E RID: 12686 RVA: 0x000920C7 File Offset: 0x000902C7
			public override IImmutableSet<Symbol> VisitHole(Hole node)
			{
				return ImmutableHashSet<Symbol>.Empty;
			}
		}

		// Token: 0x020008F4 RID: 2292
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040018A1 RID: 6305
			public static Func<ProgramNode, IEnumerable<ProgramNode>> <0>__EnumerateDescendants;
		}
	}
}
