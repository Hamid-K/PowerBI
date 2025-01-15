using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Tree;
using Microsoft.ProgramSynthesis.Wrangling.Tree.TreePathStep;

namespace Microsoft.ProgramSynthesis.Transformation.Tree
{
	// Token: 0x02001E36 RID: 7734
	internal sealed class GuardPrinter : ProgramNodeVisitor<IReadOnlyList<KeyValuePair<List<string>, string>>>
	{
		// Token: 0x06010223 RID: 66083 RVA: 0x00375FEC File Offset: 0x003741EC
		public static IReadOnlyList<string> Print(ProgramNode matchNode)
		{
			return GuardPrinter.PrintPredicates(matchNode.AcceptVisitor<IReadOnlyList<KeyValuePair<List<string>, string>>>(new GuardPrinter()));
		}

		// Token: 0x06010224 RID: 66084 RVA: 0x00376000 File Offset: 0x00374200
		public override IReadOnlyList<KeyValuePair<List<string>, string>> VisitNonterminal(NonterminalNode node)
		{
			IReadOnlyList<KeyValuePair<List<string>, string>> readOnlyList;
			if (pred.CreateSafe(Language.Build, node) == null)
			{
				readOnlyList = null;
			}
			else
			{
				Func<ProgramNode, IEnumerable<KeyValuePair<List<string>, string>>> <>9__6;
				pred? pred;
				readOnlyList = pred.GetValueOrDefault().Switch<IReadOnlyList<KeyValuePair<List<string>, string>>>(Language.Build, delegate(IsKind isKind)
				{
					List<string> list = this.ConvertPath(isKind.path.Value);
					string text = ((list.Count > 0 && isKind.kind.Value == list.Last<string>()) ? string.Empty : ("kind = " + isKind.kind.Value.ToLiteral(null)));
					return new KeyValuePair<List<string>, string>[]
					{
						new KeyValuePair<List<string>, string>(list, text)
					};
				}, (IsAttributePresent isAttributePresent) => new KeyValuePair<List<string>, string>[]
				{
					new KeyValuePair<List<string>, string>(this.ConvertPath(isAttributePresent.path.Value), isAttributePresent.name.Value + " = " + isAttributePresent.value.Value.ToLiteral(null))
				}, (IsNthChild isNthChild) => new KeyValuePair<List<string>, string>[]
				{
					new KeyValuePair<List<string>, string>(new List<string>(), string.Format("{0}({1})", "IsNthChild", isNthChild.k.Value))
				}, (HasNChildren hasNChildren) => new KeyValuePair<List<string>, string>[]
				{
					new KeyValuePair<List<string>, string>(this.ConvertPath(hasNChildren.path.Value), string.Format("{0}({1})", "HasNChildren", hasNChildren.k.Value))
				}, delegate(Not not)
				{
					IEnumerable<ProgramNode> children = node.Children;
					Func<ProgramNode, IEnumerable<KeyValuePair<List<string>, string>>> func;
					if ((func = <>9__6) == null)
					{
						func = (<>9__6 = (ProgramNode child) => child.AcceptVisitor<IReadOnlyList<KeyValuePair<List<string>, string>>>(this));
					}
					return (from kvp in children.SelectMany(func)
						select new KeyValuePair<List<string>, string>(kvp.Key, "Not(" + kvp.Value + ")")).ToList<KeyValuePair<List<string>, string>>();
				});
			}
			return readOnlyList ?? node.Children.SelectMany((ProgramNode child) => child.AcceptVisitor<IReadOnlyList<KeyValuePair<List<string>, string>>>(this)).ToList<KeyValuePair<List<string>, string>>();
		}

		// Token: 0x06010225 RID: 66085 RVA: 0x003760C6 File Offset: 0x003742C6
		public override IReadOnlyList<KeyValuePair<List<string>, string>> VisitLet(LetNode node)
		{
			return node.Children.SelectMany((ProgramNode child) => child.AcceptVisitor<IReadOnlyList<KeyValuePair<List<string>, string>>>(this)).ToList<KeyValuePair<List<string>, string>>();
		}

		// Token: 0x06010226 RID: 66086 RVA: 0x003760E4 File Offset: 0x003742E4
		public override IReadOnlyList<KeyValuePair<List<string>, string>> VisitLambda(LambdaNode node)
		{
			return node.Children.SelectMany((ProgramNode child) => child.AcceptVisitor<IReadOnlyList<KeyValuePair<List<string>, string>>>(this)).ToList<KeyValuePair<List<string>, string>>();
		}

		// Token: 0x06010227 RID: 66087 RVA: 0x00376102 File Offset: 0x00374302
		public override IReadOnlyList<KeyValuePair<List<string>, string>> VisitLiteral(LiteralNode node)
		{
			return CollectionUtils.EmptyArray<KeyValuePair<List<string>, string>>();
		}

		// Token: 0x06010228 RID: 66088 RVA: 0x00376102 File Offset: 0x00374302
		public override IReadOnlyList<KeyValuePair<List<string>, string>> VisitVariable(VariableNode node)
		{
			return CollectionUtils.EmptyArray<KeyValuePair<List<string>, string>>();
		}

		// Token: 0x06010229 RID: 66089 RVA: 0x00376102 File Offset: 0x00374302
		public override IReadOnlyList<KeyValuePair<List<string>, string>> VisitHole(Hole node)
		{
			return CollectionUtils.EmptyArray<KeyValuePair<List<string>, string>>();
		}

		// Token: 0x0601022A RID: 66090 RVA: 0x00376109 File Offset: 0x00374309
		private List<string> ConvertPath(TreePath path)
		{
			return path.Steps.Select((TreePathStep step) => step.ToString().TrimStart(new char[] { '[' }).TrimEnd(new char[] { ',', '1', ']' })).ToList<string>();
		}

		// Token: 0x0601022B RID: 66091 RVA: 0x0037613C File Offset: 0x0037433C
		private static List<string> PrintPredicates(IEnumerable<KeyValuePair<List<string>, string>> predicateByPaths)
		{
			List<string> list = new List<string>();
			foreach (IGrouping<Optional<string>, KeyValuePair<List<string>, string>> grouping in (from predicate in predicateByPaths
				group predicate by predicate.Key.MaybeFirst<string>()).OrderBy(delegate(IGrouping<Optional<string>, KeyValuePair<List<string>, string>> g)
			{
				if (!(g.Key == Optional<string>.Nothing))
				{
					return g.Max((KeyValuePair<List<string>, string> kvp) => kvp.Key.Count);
				}
				return -1;
			}))
			{
				if (grouping.Key == Optional<string>.Nothing)
				{
					List<string> list2 = (from kvp in grouping
						where !string.IsNullOrWhiteSpace(kvp.Value)
						select kvp.Value).ToList<string>();
					list.Add((list2.Count == 0) ? string.Empty : ("=> " + string.Join(" && ", list2)));
				}
				else
				{
					List<string> list3 = GuardPrinter.PrintPredicates(grouping.Select2((List<string> k, string v) => new KeyValuePair<List<string>, string>(k.Skip(1).ToList<string>(), v)).ToList<KeyValuePair<List<string>, string>>());
					list3[0] = grouping.Key.Value + "  " + list3[0];
					for (int i = 1; i < list3.Count; i++)
					{
						list3[i] = (list3[i].Contains("|---") ? "    " : "|---") + list3[i];
					}
					list.AddRange(list3);
				}
			}
			return list;
		}

		// Token: 0x04006184 RID: 24964
		private const string BranchIndent = "|---";

		// Token: 0x04006185 RID: 24965
		private const string SpaceIndent = "    ";
	}
}
