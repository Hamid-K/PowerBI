using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.AST.Extensions;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.VersionSpace
{
	// Token: 0x02000296 RID: 662
	public static class ProgramSetRewriter
	{
		// Token: 0x06000E61 RID: 3681 RVA: 0x00029D9D File Offset: 0x00027F9D
		private static ProgramSet PromoteToSet(this ProgramNode node)
		{
			return ProgramSet.List(node.Symbol, new ProgramNode[] { node });
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00029DB4 File Offset: 0x00027FB4
		private static ProgramSetRewriter.NodeOrSet ToNodeOrSet(this ProgramNode node)
		{
			return new ProgramSetRewriter.NodeOrSet(node);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00029DBC File Offset: 0x00027FBC
		private static ProgramSetRewriter.NodeOrSet ToNodeOrSet(this ProgramSet set)
		{
			return new ProgramSetRewriter.NodeOrSet(set);
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00029DC4 File Offset: 0x00027FC4
		private static ProgramSet NormalizedUnion(this ProgramSetRewriter.NodeOrSet[] programs)
		{
			if (programs == null || programs.IsEmpty<ProgramSetRewriter.NodeOrSet>())
			{
				return null;
			}
			if (programs.All((ProgramSetRewriter.NodeOrSet p) => p.IsNode))
			{
				return ProgramSet.List(programs[0].Node.Symbol, programs.Select((ProgramSetRewriter.NodeOrSet c) => c.Node));
			}
			return programs.Select((ProgramSetRewriter.NodeOrSet c) => c.Promote()).NormalizedUnion();
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00029E6C File Offset: 0x0002806C
		private static ProgramNode Rebuild(this NonterminalNode node, params ProgramNode[] arguments)
		{
			IEnumerable<ProgramNode> children = node.Children;
			Func<ProgramNode, ProgramNode, bool> func;
			if ((func = ProgramSetRewriter.<>O.<0>__ReferenceEquals) == null)
			{
				func = (ProgramSetRewriter.<>O.<0>__ReferenceEquals = new Func<ProgramNode, ProgramNode, bool>(object.ReferenceEquals));
			}
			if (children.Zip(arguments, func).All((bool b) => b))
			{
				return node;
			}
			return node.Rule.BuildASTNode(arguments);
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00029ED4 File Offset: 0x000280D4
		private static ProgramSetRewriter.NodeOrSet Rebuild(this NonterminalNode node, params ProgramSetRewriter.NodeOrSet[] arguments)
		{
			if (arguments.All((ProgramSetRewriter.NodeOrSet a) => a.IsNode))
			{
				return node.Rebuild(arguments.Select((ProgramSetRewriter.NodeOrSet a) => a.Node).ToArray<ProgramNode>()).ToNodeOrSet();
			}
			return ProgramSetRewriter.NodeOrSet.Apply(node.Rule, arguments);
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00029F4C File Offset: 0x0002814C
		private static ProgramSet Rebuild(this JoinProgramSet set, params ProgramSet[] argumentSpaces)
		{
			IEnumerable<ProgramSet> parameterSpaces = set.ParameterSpaces;
			Func<ProgramSet, ProgramSet, bool> func;
			if ((func = ProgramSetRewriter.<>O.<1>__ReferenceEquals) == null)
			{
				func = (ProgramSetRewriter.<>O.<1>__ReferenceEquals = new Func<ProgramSet, ProgramSet, bool>(object.ReferenceEquals));
			}
			if (parameterSpaces.Zip(argumentSpaces, func).All((bool b) => b))
			{
				return set;
			}
			return ProgramSet.Join(set.Rule, argumentSpaces);
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00029FB4 File Offset: 0x000281B4
		private static ProgramSet Rebuild(this DirectProgramSet set, params ProgramNode[] programs)
		{
			IEnumerable<ProgramNode> realizedPrograms = set.RealizedPrograms;
			Func<ProgramNode, ProgramNode, bool> func;
			if ((func = ProgramSetRewriter.<>O.<0>__ReferenceEquals) == null)
			{
				func = (ProgramSetRewriter.<>O.<0>__ReferenceEquals = new Func<ProgramNode, ProgramNode, bool>(object.ReferenceEquals));
			}
			if (realizedPrograms.Zip(programs, func).All((bool b) => b))
			{
				return set;
			}
			return ProgramSet.List(set.Symbol, programs);
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0002A01C File Offset: 0x0002821C
		private static ProgramSet Rebuild(this DirectProgramSet set, params ProgramSetRewriter.NodeOrSet[] programs)
		{
			if (programs.All((ProgramSetRewriter.NodeOrSet p) => p.IsNode))
			{
				IEnumerable<ProgramNode> realizedPrograms = set.RealizedPrograms;
				IEnumerable<ProgramNode> enumerable = programs.Select((ProgramSetRewriter.NodeOrSet a) => a.Node);
				Func<ProgramNode, ProgramNode, bool> func;
				if ((func = ProgramSetRewriter.<>O.<0>__ReferenceEquals) == null)
				{
					func = (ProgramSetRewriter.<>O.<0>__ReferenceEquals = new Func<ProgramNode, ProgramNode, bool>(object.ReferenceEquals));
				}
				if (realizedPrograms.Zip(enumerable, func).All((bool b) => b))
				{
					return set;
				}
			}
			return programs.NormalizedUnion();
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0002A0CC File Offset: 0x000282CC
		private static ProgramSet Rebuild(this UnionProgramSet set, params ProgramSet[] unionSpaces)
		{
			IEnumerable<ProgramSet> unionSpaces2 = set.UnionSpaces;
			Func<ProgramSet, ProgramSet, bool> func;
			if ((func = ProgramSetRewriter.<>O.<1>__ReferenceEquals) == null)
			{
				func = (ProgramSetRewriter.<>O.<1>__ReferenceEquals = new Func<ProgramSet, ProgramSet, bool>(object.ReferenceEquals));
			}
			if (unionSpaces2.Zip(unionSpaces, func).All((bool b) => b))
			{
				return set;
			}
			return unionSpaces.NormalizedUnion();
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0002A12E File Offset: 0x0002832E
		public static ProgramSet Rewrite(ProgramSet input, RewriteRule rewriteRule)
		{
			return input.AcceptVisitor<ProgramSet>(new ProgramSetRewriter.RewriteSetVisitor(rewriteRule.Source, rewriteRule.Target));
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0002A149 File Offset: 0x00028349
		public static ProgramSet Rewrite(ProgramSet input, ProgramNode source, ProgramSet target)
		{
			return input.AcceptVisitor<ProgramSet>(new ProgramSetRewriter.RewriteSetToSetVisitor(source, target));
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0002A158 File Offset: 0x00028358
		public static ProgramNode Rewrite(ProgramNode input, RewriteRule rewriteRule)
		{
			return ProgramSetRewriter.Rewrite(input, rewriteRule.Source, rewriteRule.Target);
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0002A170 File Offset: 0x00028370
		private static ProgramNode Rewrite(ProgramNode input, ProgramNode source, ProgramNode target)
		{
			if (ProgramSetRewriter.Matches(input, source))
			{
				return ProgramSetRewriter.TransformNode(input, source, target);
			}
			NonterminalNode nonterminalNode = input as NonterminalNode;
			if (nonterminalNode == null)
			{
				return input;
			}
			return nonterminalNode.Rebuild(nonterminalNode.Children.Select((ProgramNode p) => ProgramSetRewriter.Rewrite(p, source, target)).ToArray<ProgramNode>());
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0002A1E8 File Offset: 0x000283E8
		private static ProgramSet Rewrite(ProgramNode input, ProgramNode source, ProgramSet target)
		{
			if (ProgramSetRewriter.Matches(input, source))
			{
				return ProgramSetRewriter.TransformNode(input, source, target);
			}
			NonterminalNode nonterminalNode = input as NonterminalNode;
			if (nonterminalNode == null)
			{
				return input.PromoteToSet();
			}
			return ProgramSet.Join(nonterminalNode.Rule, nonterminalNode.Children.Select((ProgramNode p) => ProgramSetRewriter.Rewrite(p, source, target)).ToArray<ProgramSet>());
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0002A268 File Offset: 0x00028468
		public static ProgramNode Rewrite(ProgramNode input, ProgramNode source, Func<ProgramNode, IReadOnlyDictionary<Hole, ProgramNode>, ProgramNode> computeTarget)
		{
			if (ProgramSetRewriter.Matches(input, source))
			{
				return computeTarget(input, ProgramSetRewriter.ExtractMappings(input, source));
			}
			NonterminalNode nonterminalNode = input as NonterminalNode;
			if (nonterminalNode == null)
			{
				return input;
			}
			return nonterminalNode.Rebuild(nonterminalNode.Children.Select((ProgramNode p) => ProgramSetRewriter.Rewrite(p, source, computeTarget)).ToArray<ProgramNode>());
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0002A2E4 File Offset: 0x000284E4
		public static ProgramNode Rewrite(ProgramNode input, IFuncRewriteRule rewriteRule)
		{
			return ProgramSetRewriter.Rewrite(input, rewriteRule.Source, rewriteRule.Target);
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0002A2F8 File Offset: 0x000284F8
		public static ProgramNode TopRewrite(ProgramNode input, RewriteRule rewriteRule)
		{
			if (ProgramSetRewriter.Matches(input, rewriteRule.Source))
			{
				return ProgramSetRewriter.TransformNode(input, rewriteRule.Source, rewriteRule.Target);
			}
			return null;
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0002A320 File Offset: 0x00028520
		public static ProgramNode FoldConstants(ProgramNode input)
		{
			if (input == null)
			{
				return null;
			}
			ProgramNode programNode = input;
			using (IEnumerator<TerminalRule> enumerator = (from rule in input.Grammar.Rules.OfType<TerminalRule>()
				where !rule.IsVariable && !rule.IsInput && !rule.Deprecated
				select rule).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TerminalRule literalRule = enumerator.Current;
					IEnumerable<ConversionRule> enumerable = input.Grammar.Rules.OfType<ConversionRule>();
					Func<ConversionRule, bool> func;
					Func<ConversionRule, bool> <>9__1;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (ConversionRule conversion) => !conversion.Deprecated && conversion.Body[0] == literalRule.Head);
					}
					using (IEnumerator<ConversionRule> enumerator2 = enumerable.Where(func).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ConversionRule conversionToLiteral = enumerator2.Current;
							programNode = ProgramSetRewriter.Rewrite(programNode, new Hole(conversionToLiteral.Head, null), delegate(ProgramNode match, IReadOnlyDictionary<Hole, ProgramNode> _)
							{
								if (match.GrammarRule == conversionToLiteral || match is LambdaNode || match.GetFreeVariables().Any<Symbol>())
								{
									return match;
								}
								object obj = match.Invoke(null);
								return conversionToLiteral.BuildASTNode(literalRule.BuildASTNode(obj));
							});
						}
					}
				}
			}
			return programNode;
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0002A454 File Offset: 0x00028654
		public static void Extract(ProgramSet input, ProgramNode pattern, out ProgramSet matched, out ProgramSet unmatched)
		{
			Symbol symbol = input.Symbol;
			if (pattern is Hole && symbol == pattern.Symbol)
			{
				matched = input;
				unmatched = ProgramSet.Empty(symbol);
				return;
			}
			DirectProgramSet directProgramSet = input as DirectProgramSet;
			if (directProgramSet != null)
			{
				IEnumerable<IGrouping<bool, ProgramNode>> enumerable = from inputProgram in directProgramSet.RealizedPrograms
					group inputProgram by ProgramSetRewriter.Matches(inputProgram, pattern);
				IEnumerable<ProgramNode> enumerable2 = null;
				IEnumerable<ProgramNode> enumerable3 = null;
				foreach (IGrouping<bool, ProgramNode> grouping in enumerable)
				{
					if (grouping.Key)
					{
						enumerable2 = grouping;
					}
					else
					{
						enumerable3 = grouping;
					}
				}
				if (enumerable2 == null)
				{
					matched = ProgramSet.Empty(symbol);
					unmatched = input;
					return;
				}
				if (enumerable3 == null)
				{
					matched = input;
					unmatched = ProgramSet.Empty(symbol);
					return;
				}
				matched = new DirectProgramSet(symbol, enumerable2);
				unmatched = new DirectProgramSet(symbol, enumerable3);
				return;
			}
			else
			{
				UnionProgramSet unionProgramSet = input as UnionProgramSet;
				if (unionProgramSet != null)
				{
					List<ProgramSet> list = new List<ProgramSet>();
					List<ProgramSet> list2 = new List<ProgramSet>();
					ProgramSet[] unionSpaces = unionProgramSet.UnionSpaces;
					for (int i = 0; i < unionSpaces.Length; i++)
					{
						ProgramSet programSet;
						ProgramSet programSet2;
						ProgramSetRewriter.Extract(unionSpaces[i], pattern, out programSet, out programSet2);
						if (!programSet.IsEmpty)
						{
							list.Add(programSet);
						}
						if (!programSet2.IsEmpty)
						{
							list2.Add(programSet2);
						}
					}
					if (list.IsEmpty<ProgramSet>())
					{
						matched = ProgramSet.Empty(symbol);
						unmatched = input;
						return;
					}
					if (list2.IsEmpty<ProgramSet>())
					{
						matched = input;
						unmatched = ProgramSet.Empty(symbol);
						return;
					}
					matched = list.NormalizedUnion();
					unmatched = list2.NormalizedUnion();
					return;
				}
				else
				{
					JoinProgramSet joinProgramSet = (JoinProgramSet)input;
					NonterminalNode nonterminalNode = pattern as NonterminalNode;
					if (nonterminalNode == null || joinProgramSet.Rule != nonterminalNode.Rule)
					{
						matched = ProgramSet.Empty(symbol);
						unmatched = input;
						return;
					}
					int num = joinProgramSet.ParameterSpaces.Length;
					ProgramSet[] array = new ProgramSet[num];
					ProgramSet[] array2 = new ProgramSet[num];
					for (int j = 0; j < num; j++)
					{
						ProgramSetRewriter.Extract(joinProgramSet.ParameterSpaces[j], nonterminalNode.Children[j], out array[j], out array2[j]);
					}
					if (array.Any((ProgramSet s) => s.IsEmpty))
					{
						matched = ProgramSet.Empty(symbol);
						unmatched = input;
						return;
					}
					unmatched = ProgramSetRewriter.CreateUnmatchedCartesianProduct(joinProgramSet.Rule, joinProgramSet.ParameterSpaces, array, array2);
					matched = (unmatched.IsEmpty ? input : ProgramSet.Join(joinProgramSet.Rule, array));
					return;
				}
			}
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0002A6F0 File Offset: 0x000288F0
		public static bool Matches(ProgramSet input, ProgramNode pattern)
		{
			if (pattern is Hole && input.Symbol == pattern.Symbol)
			{
				return true;
			}
			DirectProgramSet directProgramSet = input as DirectProgramSet;
			if (directProgramSet != null)
			{
				using (IEnumerator<ProgramNode> enumerator = directProgramSet.RealizedPrograms.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (ProgramSetRewriter.Matches(enumerator.Current, pattern))
						{
							return true;
						}
					}
				}
				return false;
			}
			UnionProgramSet unionProgramSet = input as UnionProgramSet;
			if (unionProgramSet != null)
			{
				ProgramSet[] unionSpaces = unionProgramSet.UnionSpaces;
				for (int i = 0; i < unionSpaces.Length; i++)
				{
					if (ProgramSetRewriter.Matches(unionSpaces[i], pattern))
					{
						return true;
					}
				}
				return false;
			}
			JoinProgramSet joinProgramSet = input as JoinProgramSet;
			if (joinProgramSet == null)
			{
				string text = "Unknown ProgramSet subtype: ";
				Type type = input.GetType();
				throw new NotImplementedException(text + ((type != null) ? type.ToString() : null));
			}
			NonterminalNode nonterminalNode = pattern as NonterminalNode;
			if (nonterminalNode == null || joinProgramSet.Rule != nonterminalNode.Rule)
			{
				return false;
			}
			int num = joinProgramSet.ParameterSpaces.Length;
			for (int j = 0; j < num; j++)
			{
				if (!ProgramSetRewriter.Matches(joinProgramSet.ParameterSpaces[j], nonterminalNode.Children[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0002A834 File Offset: 0x00028A34
		public static bool ContainsSubProgram(this ProgramSet input, ProgramNode needle)
		{
			return input.AcceptVisitor<bool>(new ProgramSetRewriter.SetContainsSubProgramVisitor(needle));
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0002A842 File Offset: 0x00028A42
		public static bool ContainsSubProgram(this ProgramNode input, ProgramNode needle)
		{
			return input.AcceptVisitor<bool>(new ProgramSetRewriter.NodeContainsSubProgramVisitor(needle, null));
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0002A854 File Offset: 0x00028A54
		internal static ProgramSet CreateUnmatchedCartesianProduct(NonterminalRule rule, ProgramSet[] originalParameters, ProgramSet[] matchedParameters, ProgramSet[] unmatchedParameters)
		{
			if (unmatchedParameters.All((ProgramSet s) => s.IsEmpty))
			{
				return ProgramSet.Empty(rule.Head);
			}
			Func<int, IEnumerable<ProgramSet>> setsWithFirstUnmatchedAtI = (int i) => matchedParameters.Take(i).AppendItem(unmatchedParameters[i]).Concat(originalParameters.Skip(i + 1));
			Func<ProgramSet, int, ProgramSet> func = delegate(ProgramSet unmatched, int i)
			{
				if (!unmatched.IsEmpty)
				{
					return ProgramSet.Join(rule, setsWithFirstUnmatchedAtI(i));
				}
				return null;
			};
			return unmatchedParameters.Select(func).NormalizedUnion();
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0002A8F0 File Offset: 0x00028AF0
		internal static ProgramSet TransformNode(ProgramNode input, ProgramNode source, ProgramSet target)
		{
			Hole hole = source as Hole;
			if (hole != null)
			{
				return target.AcceptVisitor<ProgramSet>(new ProgramSetRewriter.NodeInSetHoleFiller(hole, input));
			}
			if (source.Holes.Any<Record<ProgramNode, int, Hole>>())
			{
				for (int i = 0; i < source.Children.Length; i++)
				{
					target = ProgramSetRewriter.TransformNode(input.Children[i], source.Children[i], target);
				}
			}
			return target;
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0002A954 File Offset: 0x00028B54
		internal static ProgramNode TransformNode(ProgramNode input, ProgramNode source, ProgramNode target)
		{
			Hole hole = source as Hole;
			if (hole != null)
			{
				return target.AcceptVisitor<ProgramNode>(new ProgramSetRewriter.NodeInNodeHoleFiller(hole, input));
			}
			if (source.Holes.Any<Record<ProgramNode, int, Hole>>())
			{
				for (int i = 0; i < source.Children.Length; i++)
				{
					target = ProgramSetRewriter.TransformNode(input.Children[i], source.Children[i], target);
				}
			}
			return target;
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0002A9B8 File Offset: 0x00028BB8
		internal static ProgramSet TransformSet(ProgramSet input, ProgramNode source, ProgramNode target)
		{
			return ProgramSetRewriter.TransformSet(input, source, target.PromoteToSet());
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0002A9C8 File Offset: 0x00028BC8
		private static ProgramSet TransformSet(ProgramSet input, ProgramNode source, ProgramSet target)
		{
			DirectProgramSet directProgramSet = input as DirectProgramSet;
			if (directProgramSet != null)
			{
				BigInteger size = directProgramSet.Size;
				if (size == 0L)
				{
					return input;
				}
				if (size == 1L)
				{
					return ProgramSetRewriter.TransformNode(directProgramSet.RealizedPrograms.First<ProgramNode>(), source, target);
				}
			}
			Hole hole = source as Hole;
			if (hole != null)
			{
				return target.AcceptVisitor<ProgramSet>(new ProgramSetRewriter.SetInSetHoleFiller(hole, input));
			}
			if (directProgramSet != null || input is UnionProgramSet)
			{
				IImmutableSet<Hole> immutableSet = ProgramSetRewriter.UniqueHolesInNode(source, null);
				Func<ProgramSet, ProgramNode, ProgramSet, ProgramSet> func;
				if ((func = ProgramSetRewriter.<>O.<2>__TransformSet) == null)
				{
					func = (ProgramSetRewriter.<>O.<2>__TransformSet = new Func<ProgramSet, ProgramNode, ProgramSet, ProgramSet>(ProgramSetRewriter.TransformSet));
				}
				Func<ProgramNode, ProgramNode, ProgramSet, ProgramSet> func2;
				if ((func2 = ProgramSetRewriter.<>O.<3>__TransformNode) == null)
				{
					func2 = (ProgramSetRewriter.<>O.<3>__TransformNode = new Func<ProgramNode, ProgramNode, ProgramSet, ProgramSet>(ProgramSetRewriter.TransformNode));
				}
				ProgramSetRewriter.TransformMinimalProgramSet transformMinimalProgramSet = new ProgramSetRewriter.TransformMinimalProgramSet(immutableSet, func, func2, input, source);
				return target.AcceptVisitor<ProgramSet>(transformMinimalProgramSet);
			}
			JoinProgramSet joinProgramSet = (JoinProgramSet)input;
			NonterminalNode nonterminalNode = (NonterminalNode)source;
			for (int i = 0; i < joinProgramSet.ParameterSpaces.Length; i++)
			{
				target = ProgramSetRewriter.TransformSet(joinProgramSet.ParameterSpaces[i], nonterminalNode.Children[i], target);
			}
			return target;
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0002AACC File Offset: 0x00028CCC
		private static IEnumerable<Hole> HolesInNode(ProgramNode node, IImmutableSet<Hole> filter)
		{
			Hole hole = node as Hole;
			IEnumerable<Hole> enumerable;
			if (!(hole != null))
			{
				enumerable = node.Holes.Select((Record<ProgramNode, int, Hole> record) => record.Item3);
			}
			else
			{
				enumerable = hole.Yield<Hole>();
			}
			IEnumerable<Hole> enumerable2 = enumerable;
			if (filter != null)
			{
				return enumerable2.Where(new Func<Hole, bool>(filter.Contains));
			}
			return enumerable2;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0002AB34 File Offset: 0x00028D34
		internal static IImmutableSet<Hole> UniqueHolesInNode(ProgramNode node, IImmutableSet<Hole> filter)
		{
			return ImmutableHashSet.CreateRange<Hole>(ProgramSetRewriter.HolesInNode(node, filter));
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0002AB44 File Offset: 0x00028D44
		internal static bool Matches(ProgramNode input, ProgramNode pattern)
		{
			if (pattern is Hole)
			{
				return input.Symbol == pattern.Symbol;
			}
			if (input.GrammarRule != pattern.GrammarRule)
			{
				return false;
			}
			if (!input.Children.Any<ProgramNode>())
			{
				return input.Equals(pattern);
			}
			IEnumerable<ProgramNode> children = input.Children;
			IEnumerable<ProgramNode> children2 = pattern.Children;
			Func<ProgramNode, ProgramNode, bool> func;
			if ((func = ProgramSetRewriter.<>O.<4>__Matches) == null)
			{
				func = (ProgramSetRewriter.<>O.<4>__Matches = new Func<ProgramNode, ProgramNode, bool>(ProgramSetRewriter.Matches));
			}
			return children.Zip(children2, func).All((bool b) => b);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0002ABE0 File Offset: 0x00028DE0
		public static IReadOnlyDictionary<Hole, ProgramNode> ExtractMappings(ProgramNode input, ProgramNode pattern)
		{
			Hole hole = pattern as Hole;
			if (hole != null)
			{
				if (!(input.Symbol == pattern.Symbol))
				{
					return null;
				}
				return ProgramSetRewriter.EmptyHoleDictionary.Add(hole, input);
			}
			else
			{
				if (input.GrammarRule != pattern.GrammarRule)
				{
					return null;
				}
				if (input.Children.Any<ProgramNode>())
				{
					ImmutableDictionary<Hole, ProgramNode> res = ProgramSetRewriter.EmptyHoleDictionary;
					Func<KeyValuePair<Hole, ProgramNode>, bool> <>9__0;
					foreach (Record<ProgramNode, ProgramNode> record in input.Children.ZipWith(pattern.Children))
					{
						IReadOnlyDictionary<Hole, ProgramNode> readOnlyDictionary = ProgramSetRewriter.ExtractMappings(record.Item1, record.Item2);
						if (readOnlyDictionary == null)
						{
							return null;
						}
						IEnumerable<KeyValuePair<Hole, ProgramNode>> enumerable = readOnlyDictionary;
						Func<KeyValuePair<Hole, ProgramNode>, bool> func;
						if ((func = <>9__0) == null)
						{
							func = (<>9__0 = delegate(KeyValuePair<Hole, ProgramNode> kv)
							{
								ProgramNode programNode;
								return res.TryGetValue(kv.Key, out programNode) && !programNode.Equals(kv.Value);
							});
						}
						if (enumerable.Any(func))
						{
							return null;
						}
						res = res.AddRange(readOnlyDictionary);
					}
					return res;
				}
				if (!input.Equals(pattern))
				{
					return null;
				}
				return ProgramSetRewriter.EmptyHoleDictionary;
			}
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0002AD04 File Offset: 0x00028F04
		private static HashSet<Symbol> SymbolsWithPathsTo(Symbol s)
		{
			HashSet<Symbol> res = new HashSet<Symbol> { s };
			Grammar grammar = s.Grammar;
			Func<GrammarRule, bool> <>9__0;
			for (;;)
			{
				IEnumerable<GrammarRule> rules = grammar.Rules;
				Func<GrammarRule, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (GrammarRule r) => r.Body.Intersect(res).Any<Symbol>());
				}
				HashSet<Symbol> hashSet = (from r in rules.Where(func)
					select r.Head).Distinct<Symbol>().Except(res).ConvertToHashSet<Symbol>();
				if (!hashSet.Any<Symbol>())
				{
					break;
				}
				res.UnionWith(hashSet);
			}
			return res;
		}

		// Token: 0x040006F0 RID: 1776
		private static readonly ImmutableDictionary<Hole, ProgramNode> EmptyHoleDictionary = ImmutableDictionary<Hole, ProgramNode>.Empty;

		// Token: 0x02000297 RID: 663
		internal class NodeInSetHoleFiller : ProgramSetVisitor<ProgramSet>
		{
			// Token: 0x06000E83 RID: 3715 RVA: 0x0002ADBD File Offset: 0x00028FBD
			public NodeInSetHoleFiller(Hole hole, ProgramNode value)
			{
				this._nodeFiller = new ProgramSetRewriter.NodeInNodeHoleFiller(hole, value);
			}

			// Token: 0x06000E84 RID: 3716 RVA: 0x0002ADD2 File Offset: 0x00028FD2
			public override ProgramSet VisitJoin(JoinProgramSet set)
			{
				return set.Rebuild(set.ParameterSpaces.Select((ProgramSet space) => space.AcceptVisitor<ProgramSet>(this)).ToArray<ProgramSet>());
			}

			// Token: 0x06000E85 RID: 3717 RVA: 0x0002ADF6 File Offset: 0x00028FF6
			public override ProgramSet VisitDirect(DirectProgramSet set)
			{
				return set.Rebuild(set.RealizedPrograms.Select((ProgramNode direct) => direct.AcceptVisitor<ProgramNode>(this._nodeFiller)).ToArray<ProgramNode>());
			}

			// Token: 0x06000E86 RID: 3718 RVA: 0x0002AE1A File Offset: 0x0002901A
			public override ProgramSet VisitUnion(UnionProgramSet set)
			{
				return set.Rebuild(set.UnionSpaces.Select((ProgramSet space) => space.AcceptVisitor<ProgramSet>(this)).ToArray<ProgramSet>());
			}

			// Token: 0x040006F1 RID: 1777
			private readonly ProgramSetRewriter.NodeInNodeHoleFiller _nodeFiller;
		}

		// Token: 0x02000298 RID: 664
		internal class NodeInNodeHoleFiller : ProgramNodeVisitor<ProgramNode>
		{
			// Token: 0x06000E8A RID: 3722 RVA: 0x0002AE55 File Offset: 0x00029055
			public NodeInNodeHoleFiller(Hole hole, ProgramNode value)
			{
				this._hole = hole;
				this._value = value;
			}

			// Token: 0x06000E8B RID: 3723 RVA: 0x0002AE6B File Offset: 0x0002906B
			public override ProgramNode VisitNonterminal(NonterminalNode node)
			{
				return node.Rebuild(node.Children.Select((ProgramNode child) => child.AcceptVisitor<ProgramNode>(this)).ToArray<ProgramNode>());
			}

			// Token: 0x06000E8C RID: 3724 RVA: 0x0002AE8F File Offset: 0x0002908F
			public override ProgramNode VisitLet(LetNode node)
			{
				return this.VisitNonterminal(node);
			}

			// Token: 0x06000E8D RID: 3725 RVA: 0x0002AE8F File Offset: 0x0002908F
			public override ProgramNode VisitLambda(LambdaNode node)
			{
				return this.VisitNonterminal(node);
			}

			// Token: 0x06000E8E RID: 3726 RVA: 0x0000E945 File Offset: 0x0000CB45
			public override ProgramNode VisitLiteral(LiteralNode node)
			{
				return node;
			}

			// Token: 0x06000E8F RID: 3727 RVA: 0x0000E945 File Offset: 0x0000CB45
			public override ProgramNode VisitVariable(VariableNode node)
			{
				return node;
			}

			// Token: 0x06000E90 RID: 3728 RVA: 0x0002AE98 File Offset: 0x00029098
			public override ProgramNode VisitHole(Hole node)
			{
				if (!(node == this._hole))
				{
					return node;
				}
				return this._value;
			}

			// Token: 0x040006F2 RID: 1778
			private readonly Hole _hole;

			// Token: 0x040006F3 RID: 1779
			private readonly ProgramNode _value;
		}

		// Token: 0x02000299 RID: 665
		internal class SetInSetHoleFiller : ProgramSetVisitor<ProgramSet>
		{
			// Token: 0x06000E92 RID: 3730 RVA: 0x0002AEB9 File Offset: 0x000290B9
			public SetInSetHoleFiller(Hole hole, ProgramSet value)
			{
				this._nodeFiller = new ProgramSetRewriter.SetInNodeHoleFiller(hole, value);
			}

			// Token: 0x06000E93 RID: 3731 RVA: 0x0002AECE File Offset: 0x000290CE
			public override ProgramSet VisitJoin(JoinProgramSet set)
			{
				return set.Rebuild(set.ParameterSpaces.Select((ProgramSet space) => space.AcceptVisitor<ProgramSet>(this)).ToArray<ProgramSet>());
			}

			// Token: 0x06000E94 RID: 3732 RVA: 0x0002AEF2 File Offset: 0x000290F2
			public override ProgramSet VisitDirect(DirectProgramSet set)
			{
				return set.Rebuild(set.RealizedPrograms.Select((ProgramNode direct) => direct.AcceptVisitor<ProgramSetRewriter.NodeOrSet>(this._nodeFiller)).ToArray<ProgramSetRewriter.NodeOrSet>());
			}

			// Token: 0x06000E95 RID: 3733 RVA: 0x0002AF16 File Offset: 0x00029116
			public override ProgramSet VisitUnion(UnionProgramSet set)
			{
				return set.Rebuild(set.UnionSpaces.Select((ProgramSet space) => space.AcceptVisitor<ProgramSet>(this)).ToArray<ProgramSet>());
			}

			// Token: 0x040006F4 RID: 1780
			private readonly ProgramSetRewriter.SetInNodeHoleFiller _nodeFiller;
		}

		// Token: 0x0200029A RID: 666
		internal class SetInNodeHoleFiller : ProgramNodeVisitor<ProgramSetRewriter.NodeOrSet>
		{
			// Token: 0x06000E99 RID: 3737 RVA: 0x0002AF48 File Offset: 0x00029148
			public SetInNodeHoleFiller(Hole hole, ProgramSet value)
			{
				this._hole = hole;
				this._value = value;
			}

			// Token: 0x06000E9A RID: 3738 RVA: 0x0002AF5E File Offset: 0x0002915E
			public override ProgramSetRewriter.NodeOrSet VisitNonterminal(NonterminalNode node)
			{
				return node.Rebuild(node.Children.Select((ProgramNode child) => child.AcceptVisitor<ProgramSetRewriter.NodeOrSet>(this)).ToArray<ProgramSetRewriter.NodeOrSet>());
			}

			// Token: 0x06000E9B RID: 3739 RVA: 0x0002AF82 File Offset: 0x00029182
			public override ProgramSetRewriter.NodeOrSet VisitLet(LetNode node)
			{
				return this.VisitNonterminal(node);
			}

			// Token: 0x06000E9C RID: 3740 RVA: 0x0002AF82 File Offset: 0x00029182
			public override ProgramSetRewriter.NodeOrSet VisitLambda(LambdaNode node)
			{
				return this.VisitNonterminal(node);
			}

			// Token: 0x06000E9D RID: 3741 RVA: 0x0002AF8B File Offset: 0x0002918B
			public override ProgramSetRewriter.NodeOrSet VisitLiteral(LiteralNode node)
			{
				return node.ToNodeOrSet();
			}

			// Token: 0x06000E9E RID: 3742 RVA: 0x0002AF8B File Offset: 0x0002918B
			public override ProgramSetRewriter.NodeOrSet VisitVariable(VariableNode node)
			{
				return node.ToNodeOrSet();
			}

			// Token: 0x06000E9F RID: 3743 RVA: 0x0002AF93 File Offset: 0x00029193
			public override ProgramSetRewriter.NodeOrSet VisitHole(Hole node)
			{
				if (!(node == this._hole))
				{
					return node.ToNodeOrSet();
				}
				return this._value.ToNodeOrSet();
			}

			// Token: 0x040006F5 RID: 1781
			private readonly Hole _hole;

			// Token: 0x040006F6 RID: 1782
			private readonly ProgramSet _value;
		}

		// Token: 0x0200029B RID: 667
		internal class TransformMinimalProgramSet : ProgramSetVisitor<ProgramSet>
		{
			// Token: 0x06000EA1 RID: 3745 RVA: 0x0002AFC0 File Offset: 0x000291C0
			public TransformMinimalProgramSet(IImmutableSet<Hole> filter, Func<ProgramSet, ProgramNode, ProgramSet, ProgramSet> transformSet, Func<ProgramNode, ProgramNode, ProgramSet, ProgramSet> transformNode, ProgramSet input, ProgramNode source)
			{
				this._uniqueHoleCollector = new ProgramSetRewriter.UniqueHoleCollector(filter);
				this._transformSet = transformSet;
				this._transformNode = transformNode;
				this._input = input;
				this._source = source;
				this._transformMinimalNode = new ProgramSetRewriter.TransformMinimalProgramNode(filter, transformSet, transformNode, input, source);
			}

			// Token: 0x06000EA2 RID: 3746 RVA: 0x0002B010 File Offset: 0x00029210
			internal static ProgramSet TransformMinimalHelper(Func<ProgramSet, ProgramNode, ProgramSet, ProgramSet> transformSet, Func<ProgramNode, ProgramNode, ProgramSet, ProgramSet> transformNode, ProgramSet input, ProgramNode source, ProgramSet set)
			{
				UnionProgramSet unionProgramSet = input as UnionProgramSet;
				if (unionProgramSet != null)
				{
					return unionProgramSet.UnionSpaces.Select((ProgramSet space) => transformSet(space, source, set)).NormalizedUnion();
				}
				ProgramSet[] array = ((DirectProgramSet)input).RealizedPrograms.Select((ProgramNode program) => transformNode(program, source, set)).ToArray<ProgramSet>();
				if (array.All((ProgramSet program) => program is DirectProgramSet))
				{
					return ProgramSet.List(set.Symbol, array.SelectMany((ProgramSet y) => ((DirectProgramSet)y).RealizedPrograms));
				}
				return array.NormalizedUnion();
			}

			// Token: 0x06000EA3 RID: 3747 RVA: 0x0002B0EC File Offset: 0x000292EC
			public override ProgramSet VisitJoin(JoinProgramSet set)
			{
				if (set.AcceptVisitor<ImmutableHashSet<Hole>>(this._uniqueHoleCollector).Count == 0)
				{
					return set;
				}
				if ((from space in set.ParameterSpaces
					select space.AcceptVisitor<ImmutableHashSet<Hole>>(this._uniqueHoleCollector) into e
					where e.Any<Hole>()
					select e).HasAtLeast(2))
				{
					return ProgramSetRewriter.TransformMinimalProgramSet.TransformMinimalHelper(this._transformSet, this._transformNode, this._input, this._source, set);
				}
				return ProgramSet.Join(set.Rule, set.ParameterSpaces.Select((ProgramSet space) => space.AcceptVisitor<ProgramSet>(this)).ToArray<ProgramSet>());
			}

			// Token: 0x06000EA4 RID: 3748 RVA: 0x0002B197 File Offset: 0x00029397
			public override ProgramSet VisitDirect(DirectProgramSet set)
			{
				return set.RealizedPrograms.Select((ProgramNode direct) => direct.AcceptVisitor<ProgramSetRewriter.NodeOrSet>(this._transformMinimalNode).Promote()).NormalizedUnion();
			}

			// Token: 0x06000EA5 RID: 3749 RVA: 0x0002B1B5 File Offset: 0x000293B5
			public override ProgramSet VisitUnion(UnionProgramSet set)
			{
				return set.UnionSpaces.Select((ProgramSet space) => space.AcceptVisitor<ProgramSet>(this)).NormalizedUnion();
			}

			// Token: 0x040006F7 RID: 1783
			private readonly ProgramSetRewriter.UniqueHoleCollector _uniqueHoleCollector;

			// Token: 0x040006F8 RID: 1784
			private readonly Func<ProgramSet, ProgramNode, ProgramSet, ProgramSet> _transformSet;

			// Token: 0x040006F9 RID: 1785
			private readonly Func<ProgramNode, ProgramNode, ProgramSet, ProgramSet> _transformNode;

			// Token: 0x040006FA RID: 1786
			private readonly ProgramSet _input;

			// Token: 0x040006FB RID: 1787
			private readonly ProgramNode _source;

			// Token: 0x040006FC RID: 1788
			private readonly ProgramSetRewriter.TransformMinimalProgramNode _transformMinimalNode;
		}

		// Token: 0x0200029E RID: 670
		internal class UniqueHoleCollector : ProgramSetVisitor<ImmutableHashSet<Hole>>
		{
			// Token: 0x06000EB2 RID: 3762 RVA: 0x0002B265 File Offset: 0x00029465
			public UniqueHoleCollector(IImmutableSet<Hole> filter)
			{
				this._filter = filter;
			}

			// Token: 0x06000EB3 RID: 3763 RVA: 0x0002B274 File Offset: 0x00029474
			public override ImmutableHashSet<Hole> VisitJoin(JoinProgramSet set)
			{
				return set.ParameterSpaces.Select((ProgramSet value) => value.AcceptVisitor<ImmutableHashSet<Hole>>(this)).Aggregate((ImmutableHashSet<Hole> x, ImmutableHashSet<Hole> y) => x.Union(y));
			}

			// Token: 0x06000EB4 RID: 3764 RVA: 0x0002B2B4 File Offset: 0x000294B4
			public override ImmutableHashSet<Hole> VisitDirect(DirectProgramSet set)
			{
				return ImmutableHashSet.CreateRange<Hole>(set.RealizedPrograms.Select((ProgramNode value) => ProgramSetRewriter.HolesInNode(value, this._filter)).SelectMany((IEnumerable<Hole> x) => x));
			}

			// Token: 0x06000EB5 RID: 3765 RVA: 0x0002B301 File Offset: 0x00029501
			public override ImmutableHashSet<Hole> VisitUnion(UnionProgramSet set)
			{
				return set.UnionSpaces.Select((ProgramSet value) => value.AcceptVisitor<ImmutableHashSet<Hole>>(this)).Aggregate((ImmutableHashSet<Hole> x, ImmutableHashSet<Hole> y) => x.Union(y));
			}

			// Token: 0x04000705 RID: 1797
			private readonly IImmutableSet<Hole> _filter;
		}

		// Token: 0x020002A0 RID: 672
		internal class TransformMinimalProgramNode : ProgramNodeVisitor<ProgramSetRewriter.NodeOrSet>
		{
			// Token: 0x06000EBE RID: 3774 RVA: 0x0002B36A File Offset: 0x0002956A
			public TransformMinimalProgramNode(IImmutableSet<Hole> filter, Func<ProgramSet, ProgramNode, ProgramSet, ProgramSet> transformSet, Func<ProgramNode, ProgramNode, ProgramSet, ProgramSet> transformNode, ProgramSet input, ProgramNode source)
			{
				this._filter = filter;
				this._transformSet = transformSet;
				this._transformNode = transformNode;
				this._input = input;
				this._source = source;
			}

			// Token: 0x06000EBF RID: 3775 RVA: 0x0002B398 File Offset: 0x00029598
			public override ProgramSetRewriter.NodeOrSet VisitNonterminal(NonterminalNode node)
			{
				if (ProgramSetRewriter.HolesInNode(node, this._filter).IsEmpty<Hole>())
				{
					return node.ToNodeOrSet();
				}
				if ((from child in node.Children
					select ProgramSetRewriter.UniqueHolesInNode(child, this._filter) into e
					where e.Any<Hole>()
					select e).HasAtLeast(2))
				{
					return ProgramSetRewriter.TransformMinimalProgramSet.TransformMinimalHelper(this._transformSet, this._transformNode, this._input, this._source, node.PromoteToSet()).ToNodeOrSet();
				}
				return node.Rebuild(node.Children.Select((ProgramNode value) => value.AcceptVisitor<ProgramSetRewriter.NodeOrSet>(this)).ToArray<ProgramSetRewriter.NodeOrSet>());
			}

			// Token: 0x06000EC0 RID: 3776 RVA: 0x0002AF82 File Offset: 0x00029182
			public override ProgramSetRewriter.NodeOrSet VisitLet(LetNode node)
			{
				return this.VisitNonterminal(node);
			}

			// Token: 0x06000EC1 RID: 3777 RVA: 0x0002AF82 File Offset: 0x00029182
			public override ProgramSetRewriter.NodeOrSet VisitLambda(LambdaNode node)
			{
				return this.VisitNonterminal(node);
			}

			// Token: 0x06000EC2 RID: 3778 RVA: 0x0002AF8B File Offset: 0x0002918B
			public override ProgramSetRewriter.NodeOrSet VisitLiteral(LiteralNode node)
			{
				return node.ToNodeOrSet();
			}

			// Token: 0x06000EC3 RID: 3779 RVA: 0x0002AF8B File Offset: 0x0002918B
			public override ProgramSetRewriter.NodeOrSet VisitVariable(VariableNode node)
			{
				return node.ToNodeOrSet();
			}

			// Token: 0x06000EC4 RID: 3780 RVA: 0x0002B44D File Offset: 0x0002964D
			public override ProgramSetRewriter.NodeOrSet VisitHole(Hole node)
			{
				return ProgramSetRewriter.TransformMinimalProgramSet.TransformMinimalHelper(this._transformSet, this._transformNode, this._input, this._source, node.PromoteToSet()).ToNodeOrSet();
			}

			// Token: 0x0400070A RID: 1802
			private readonly IImmutableSet<Hole> _filter;

			// Token: 0x0400070B RID: 1803
			private readonly Func<ProgramSet, ProgramNode, ProgramSet, ProgramSet> _transformSet;

			// Token: 0x0400070C RID: 1804
			private readonly Func<ProgramNode, ProgramNode, ProgramSet, ProgramSet> _transformNode;

			// Token: 0x0400070D RID: 1805
			private readonly ProgramSet _input;

			// Token: 0x0400070E RID: 1806
			private readonly ProgramNode _source;
		}

		// Token: 0x020002A2 RID: 674
		internal struct NodeOrSet
		{
			// Token: 0x06000ECA RID: 3786 RVA: 0x0002B491 File Offset: 0x00029691
			public NodeOrSet(ProgramNode node)
			{
				this._node = node;
				this._set = null;
			}

			// Token: 0x06000ECB RID: 3787 RVA: 0x0002B4A4 File Offset: 0x000296A4
			public NodeOrSet(ProgramSet set)
			{
				DirectProgramSet directProgramSet = set as DirectProgramSet;
				if (directProgramSet != null && !directProgramSet.RealizedPrograms.HasAtLeast(2))
				{
					this._node = directProgramSet.RealizedPrograms.FirstOrDefault<ProgramNode>();
					this._set = null;
					return;
				}
				this._node = null;
				this._set = set;
			}

			// Token: 0x06000ECC RID: 3788 RVA: 0x0002B4F0 File Offset: 0x000296F0
			public static ProgramSetRewriter.NodeOrSet Apply(NonterminalRule rule, ProgramSetRewriter.NodeOrSet[] children)
			{
				if (children.All((ProgramSetRewriter.NodeOrSet c) => c.IsNode))
				{
					return rule.BuildASTNode(children.Select((ProgramSetRewriter.NodeOrSet c) => c.Node).ToArray<ProgramNode>()).ToNodeOrSet();
				}
				return ProgramSet.Join(rule, children.Select((ProgramSetRewriter.NodeOrSet c) => c.Promote())).ToNodeOrSet();
			}

			// Token: 0x06000ECD RID: 3789 RVA: 0x0002B58A File Offset: 0x0002978A
			public ProgramSet Promote()
			{
				return this._set ?? this._node.PromoteToSet();
			}

			// Token: 0x1700034F RID: 847
			// (get) Token: 0x06000ECE RID: 3790 RVA: 0x0002B5A1 File Offset: 0x000297A1
			public bool IsNode
			{
				get
				{
					return this._node != null;
				}
			}

			// Token: 0x17000350 RID: 848
			// (get) Token: 0x06000ECF RID: 3791 RVA: 0x0002B5AF File Offset: 0x000297AF
			public bool IsSet
			{
				get
				{
					return this._set != null;
				}
			}

			// Token: 0x17000351 RID: 849
			// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x0002B5BA File Offset: 0x000297BA
			public ProgramNode Node
			{
				get
				{
					return this._node;
				}
			}

			// Token: 0x17000352 RID: 850
			// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x0002B5C2 File Offset: 0x000297C2
			public ProgramSet Set
			{
				get
				{
					return this._set;
				}
			}

			// Token: 0x04000711 RID: 1809
			private readonly ProgramNode _node;

			// Token: 0x04000712 RID: 1810
			private readonly ProgramSet _set;
		}

		// Token: 0x020002A4 RID: 676
		private class RewriteSetVisitor : ProgramSetVisitor<ProgramSet>
		{
			// Token: 0x06000ED7 RID: 3799 RVA: 0x0002B5F1 File Offset: 0x000297F1
			public RewriteSetVisitor(ProgramNode source, ProgramNode target)
			{
				this._source = source;
				this._target = target;
				this._accessibleSymbols = ProgramSetRewriter.SymbolsWithPathsTo(source.Symbol);
			}

			// Token: 0x06000ED8 RID: 3800 RVA: 0x0002B628 File Offset: 0x00029828
			public override ProgramSet VisitDirect(DirectProgramSet set)
			{
				if (!this._accessibleSymbols.Contains(set.Symbol))
				{
					return set;
				}
				ProgramSet programSet;
				if (this._cache.TryGetValue(set, out programSet))
				{
					return programSet;
				}
				programSet = set.Rebuild(set.RealizedPrograms.Select((ProgramNode p) => ProgramSetRewriter.Rewrite(p, this._source, this._target)).ToArray<ProgramNode>());
				this._cache[set] = programSet;
				return programSet;
			}

			// Token: 0x06000ED9 RID: 3801 RVA: 0x0002B690 File Offset: 0x00029890
			public override ProgramSet VisitJoin(JoinProgramSet set)
			{
				if (!this._accessibleSymbols.Contains(set.Symbol))
				{
					return set;
				}
				ProgramSet programSet;
				if (this._cache.TryGetValue(set, out programSet))
				{
					return programSet;
				}
				ProgramSet programSet2;
				ProgramSet programSet3;
				ProgramSetRewriter.Extract(set, this._source, out programSet2, out programSet3);
				if (programSet2.IsEmpty)
				{
					programSet = set.Rebuild(set.ParameterSpaces.Select((ProgramSet s) => s.AcceptVisitor<ProgramSet>(this)).ToArray<ProgramSet>());
				}
				else
				{
					ProgramSet programSet4 = ProgramSetRewriter.TransformSet(programSet2, this._source, this._target);
					programSet4 = programSet4.AcceptVisitor<ProgramSet>(this);
					ProgramSet programSet5 = programSet3.AcceptVisitor<ProgramSet>(this);
					programSet = Seq.Of<ProgramSet>(new ProgramSet[] { programSet4, programSet5 }).NormalizedUnion();
				}
				this._cache[set] = programSet;
				return programSet;
			}

			// Token: 0x06000EDA RID: 3802 RVA: 0x0002B750 File Offset: 0x00029950
			public override ProgramSet VisitUnion(UnionProgramSet set)
			{
				if (!this._accessibleSymbols.Contains(set.Symbol))
				{
					return set;
				}
				ProgramSet programSet;
				if (this._cache.TryGetValue(set, out programSet))
				{
					return programSet;
				}
				programSet = set.Rebuild(set.UnionSpaces.Select((ProgramSet s) => s.AcceptVisitor<ProgramSet>(this)).ToArray<ProgramSet>());
				this._cache[set] = programSet;
				return programSet;
			}

			// Token: 0x04000717 RID: 1815
			private readonly ProgramNode _source;

			// Token: 0x04000718 RID: 1816
			private readonly ProgramNode _target;

			// Token: 0x04000719 RID: 1817
			private readonly HashSet<Symbol> _accessibleSymbols;

			// Token: 0x0400071A RID: 1818
			private readonly Dictionary<ProgramSet, ProgramSet> _cache = new Dictionary<ProgramSet, ProgramSet>(IdentityEquality.Comparer);
		}

		// Token: 0x020002A5 RID: 677
		private class RewriteSetToSetVisitor : ProgramSetVisitor<ProgramSet>
		{
			// Token: 0x06000EDE RID: 3806 RVA: 0x0002B7C9 File Offset: 0x000299C9
			public RewriteSetToSetVisitor(ProgramNode source, ProgramSet target)
			{
				this._source = source;
				this._target = target;
				this._accessibleSymbols = ProgramSetRewriter.SymbolsWithPathsTo(source.Symbol);
			}

			// Token: 0x06000EDF RID: 3807 RVA: 0x0002B800 File Offset: 0x00029A00
			public override ProgramSet VisitDirect(DirectProgramSet set)
			{
				if (!this._accessibleSymbols.Contains(set.Symbol))
				{
					return set;
				}
				ProgramSet programSet;
				if (this._cache.TryGetValue(set, out programSet))
				{
					return programSet;
				}
				programSet = set.Rebuild(set.RealizedPrograms.Select((ProgramNode p) => new ProgramSetRewriter.NodeOrSet(ProgramSetRewriter.Rewrite(p, this._source, this._target))).ToArray<ProgramSetRewriter.NodeOrSet>());
				this._cache[set] = programSet;
				return programSet;
			}

			// Token: 0x06000EE0 RID: 3808 RVA: 0x0002B868 File Offset: 0x00029A68
			public override ProgramSet VisitJoin(JoinProgramSet set)
			{
				if (!this._accessibleSymbols.Contains(set.Symbol))
				{
					return set;
				}
				ProgramSet programSet;
				if (this._cache.TryGetValue(set, out programSet))
				{
					return programSet;
				}
				ProgramSet programSet2;
				ProgramSet programSet3;
				ProgramSetRewriter.Extract(set, this._source, out programSet2, out programSet3);
				if (programSet2.IsEmpty)
				{
					programSet = set.Rebuild(set.ParameterSpaces.Select((ProgramSet s) => s.AcceptVisitor<ProgramSet>(this)).ToArray<ProgramSet>());
				}
				else
				{
					ProgramSet programSet4 = ProgramSetRewriter.TransformSet(programSet2, this._source, this._target);
					programSet4 = programSet4.AcceptVisitor<ProgramSet>(this);
					ProgramSet programSet5 = programSet3.AcceptVisitor<ProgramSet>(this);
					programSet = Seq.Of<ProgramSet>(new ProgramSet[] { programSet4, programSet5 }).NormalizedUnion();
				}
				this._cache[set] = programSet;
				return programSet;
			}

			// Token: 0x06000EE1 RID: 3809 RVA: 0x0002B928 File Offset: 0x00029B28
			public override ProgramSet VisitUnion(UnionProgramSet set)
			{
				if (!this._accessibleSymbols.Contains(set.Symbol))
				{
					return set;
				}
				ProgramSet programSet;
				if (this._cache.TryGetValue(set, out programSet))
				{
					return programSet;
				}
				programSet = set.Rebuild(set.UnionSpaces.Select((ProgramSet s) => s.AcceptVisitor<ProgramSet>(this)).ToArray<ProgramSet>());
				this._cache[set] = programSet;
				return programSet;
			}

			// Token: 0x0400071B RID: 1819
			private readonly ProgramNode _source;

			// Token: 0x0400071C RID: 1820
			private readonly ProgramSet _target;

			// Token: 0x0400071D RID: 1821
			private readonly HashSet<Symbol> _accessibleSymbols;

			// Token: 0x0400071E RID: 1822
			private readonly Dictionary<ProgramSet, ProgramSet> _cache = new Dictionary<ProgramSet, ProgramSet>(IdentityEquality.Comparer);
		}

		// Token: 0x020002A6 RID: 678
		private class NodeContainsSubProgramVisitor : ProgramNodeVisitor<bool>
		{
			// Token: 0x06000EE5 RID: 3813 RVA: 0x0002B9A6 File Offset: 0x00029BA6
			public NodeContainsSubProgramVisitor(ProgramNode needle, HashSet<Symbol> accessibleSymbols = null)
			{
				this._needle = needle;
				this._accessibleSymbols = accessibleSymbols ?? ProgramSetRewriter.SymbolsWithPathsTo(needle.Symbol);
			}

			// Token: 0x06000EE6 RID: 3814 RVA: 0x0002B9CB File Offset: 0x00029BCB
			public override bool VisitNonterminal(NonterminalNode node)
			{
				return this._accessibleSymbols.Contains(node.Symbol) && (node.Equals(this._needle) || node.Children.Any((ProgramNode child) => child.AcceptVisitor<bool>(this)));
			}

			// Token: 0x06000EE7 RID: 3815 RVA: 0x0002BA09 File Offset: 0x00029C09
			public override bool VisitLet(LetNode node)
			{
				return this.VisitNonterminal(node);
			}

			// Token: 0x06000EE8 RID: 3816 RVA: 0x0002BA09 File Offset: 0x00029C09
			public override bool VisitLambda(LambdaNode node)
			{
				return this.VisitNonterminal(node);
			}

			// Token: 0x06000EE9 RID: 3817 RVA: 0x0002BA12 File Offset: 0x00029C12
			public override bool VisitLiteral(LiteralNode node)
			{
				return node.Equals(this._needle);
			}

			// Token: 0x06000EEA RID: 3818 RVA: 0x0002BA12 File Offset: 0x00029C12
			public override bool VisitVariable(VariableNode node)
			{
				return node.Equals(this._needle);
			}

			// Token: 0x06000EEB RID: 3819 RVA: 0x0002BA12 File Offset: 0x00029C12
			public override bool VisitHole(Hole node)
			{
				return node.Equals(this._needle);
			}

			// Token: 0x0400071F RID: 1823
			private readonly ProgramNode _needle;

			// Token: 0x04000720 RID: 1824
			private readonly HashSet<Symbol> _accessibleSymbols;
		}

		// Token: 0x020002A7 RID: 679
		private class SetContainsSubProgramVisitor : ProgramSetVisitor<bool>
		{
			// Token: 0x06000EED RID: 3821 RVA: 0x0002BA2C File Offset: 0x00029C2C
			public SetContainsSubProgramVisitor(ProgramNode needle)
			{
				this._needle = needle;
				this._accessibleSymbols = ProgramSetRewriter.SymbolsWithPathsTo(needle.Symbol);
				this._nodeVisitor = new ProgramSetRewriter.NodeContainsSubProgramVisitor(this._needle, this._accessibleSymbols);
			}

			// Token: 0x06000EEE RID: 3822 RVA: 0x0002BA80 File Offset: 0x00029C80
			public override bool VisitDirect(DirectProgramSet set)
			{
				Func<ProgramNode, bool> <>9__1;
				return this._accessibleSymbols.Contains(set.Symbol) && this._cache.GetOrAdd(set, delegate(ProgramSet _)
				{
					IEnumerable<ProgramNode> realizedPrograms = set.RealizedPrograms;
					Func<ProgramNode, bool> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (ProgramNode node) => node.AcceptVisitor<bool>(this._nodeVisitor));
					}
					return realizedPrograms.Any(func);
				});
			}

			// Token: 0x06000EEF RID: 3823 RVA: 0x0002BAD8 File Offset: 0x00029CD8
			public override bool VisitJoin(JoinProgramSet set)
			{
				Func<ProgramSet, bool> <>9__1;
				return this._accessibleSymbols.Contains(set.Symbol) && this._cache.GetOrAdd(set, delegate(ProgramSet _)
				{
					if (!(set.Symbol == this._needle.Symbol) || !set.Contains(this._needle))
					{
						IEnumerable<ProgramSet> parameterSpaces = set.ParameterSpaces;
						Func<ProgramSet, bool> func;
						if ((func = <>9__1) == null)
						{
							func = (<>9__1 = (ProgramSet space) => space.AcceptVisitor<bool>(this));
						}
						return parameterSpaces.Any(func);
					}
					return true;
				});
			}

			// Token: 0x06000EF0 RID: 3824 RVA: 0x0002BB30 File Offset: 0x00029D30
			public override bool VisitUnion(UnionProgramSet set)
			{
				Func<ProgramSet, bool> <>9__1;
				return this._accessibleSymbols.Contains(set.Symbol) && this._cache.GetOrAdd(set, delegate(ProgramSet _)
				{
					IEnumerable<ProgramSet> unionSpaces = set.UnionSpaces;
					Func<ProgramSet, bool> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (ProgramSet space) => space.AcceptVisitor<bool>(this));
					}
					return unionSpaces.Any(func);
				});
			}

			// Token: 0x04000721 RID: 1825
			private readonly ProgramNode _needle;

			// Token: 0x04000722 RID: 1826
			private readonly HashSet<Symbol> _accessibleSymbols;

			// Token: 0x04000723 RID: 1827
			private readonly UnboundedCache<ProgramSet, bool> _cache = new UnboundedCache<ProgramSet, bool>(IdentityEquality.Comparer, null, null);

			// Token: 0x04000724 RID: 1828
			private readonly ProgramSetRewriter.NodeContainsSubProgramVisitor _nodeVisitor;
		}

		// Token: 0x020002AB RID: 683
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400072E RID: 1838
			public static Func<ProgramNode, ProgramNode, bool> <0>__ReferenceEquals;

			// Token: 0x0400072F RID: 1839
			public static Func<ProgramSet, ProgramSet, bool> <1>__ReferenceEquals;

			// Token: 0x04000730 RID: 1840
			public static Func<ProgramSet, ProgramNode, ProgramSet, ProgramSet> <2>__TransformSet;

			// Token: 0x04000731 RID: 1841
			public static Func<ProgramNode, ProgramNode, ProgramSet, ProgramSet> <3>__TransformNode;

			// Token: 0x04000732 RID: 1842
			public static Func<ProgramNode, ProgramNode, bool> <4>__Matches;
		}
	}
}
