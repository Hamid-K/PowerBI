using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Tree
{
	// Token: 0x02001E33 RID: 7731
	internal sealed class PrettyPrintVisitor : ProgramNodeVisitor<IReadOnlyList<string>>
	{
		// Token: 0x06010212 RID: 66066 RVA: 0x00375C9C File Offset: 0x00373E9C
		public override IReadOnlyList<string> VisitNonterminal(NonterminalNode node)
		{
			string id = node.Rule.Id;
			if (id == "TmpFilter")
			{
				return GuardPrinter.Print(node.Children[0]).Concat(node.Children[1].AcceptVisitor<IReadOnlyList<string>>(this)).ToList<string>();
			}
			if (id == "LeafConstLabelNode")
			{
				LeafConstLabelNode leafConstLabelNode = LeafConstLabelNode.CreateUnsafe(node);
				return new string[] { "LeafNode: " + PrettyPrintVisitor.PrintLabelAttributes(leafConstLabelNode.label.Value, leafConstLabelNode.attributes.Value) };
			}
			if (id == "ConstLabelNode")
			{
				ConstLabelNode constLabelNode = ConstLabelNode.CreateUnsafe(node);
				List<string> list = new List<string>();
				list.Add("Node: " + PrettyPrintVisitor.PrintLabelAttributes(constLabelNode.label.Value, constLabelNode.attributes.Value));
				list.AddRange(this.VisitIndent(node.Children[2]));
				return list;
			}
			if (id == "Select")
			{
				Select select = Select.CreateUnsafe(node);
				List<string> list2 = new List<string> { PrettyPrintVisitor.<VisitNonterminal>g__ToOrdinal|2_0(select.k.Value + 1) + " element of:" };
				list2.AddRange(this.VisitChildren(node, true));
				string text = string.Format("SelectNode #{0}", this._selectAliases.Count);
				this._selectAliases.Add(new KeyValuePair<string, string>(text, string.Join(Environment.NewLine, list2)));
				return new string[] { text };
			}
			if (!(id == "SingleList") && !(id == "Prepend"))
			{
				return this.VisitChildren(node, false);
			}
			return this.VisitChildren(node, false);
		}

		// Token: 0x06010213 RID: 66067 RVA: 0x00375E69 File Offset: 0x00374069
		public override IReadOnlyList<string> VisitLet(LetNode node)
		{
			return this.VisitChildren(node, false);
		}

		// Token: 0x06010214 RID: 66068 RVA: 0x00375E69 File Offset: 0x00374069
		public override IReadOnlyList<string> VisitLambda(LambdaNode node)
		{
			return this.VisitChildren(node, false);
		}

		// Token: 0x06010215 RID: 66069 RVA: 0x00375E73 File Offset: 0x00374073
		public override IReadOnlyList<string> VisitLiteral(LiteralNode node)
		{
			return CollectionUtils.EmptyArray<string>();
		}

		// Token: 0x06010216 RID: 66070 RVA: 0x00375E73 File Offset: 0x00374073
		public override IReadOnlyList<string> VisitVariable(VariableNode node)
		{
			return CollectionUtils.EmptyArray<string>();
		}

		// Token: 0x06010217 RID: 66071 RVA: 0x00375E73 File Offset: 0x00374073
		public override IReadOnlyList<string> VisitHole(Hole node)
		{
			return CollectionUtils.EmptyArray<string>();
		}

		// Token: 0x06010218 RID: 66072 RVA: 0x00375E7C File Offset: 0x0037407C
		private IReadOnlyList<string> VisitChildren(ProgramNode node, bool indent = false)
		{
			return node.Children.SelectMany(delegate(ProgramNode child)
			{
				if (!indent)
				{
					return child.AcceptVisitor<IReadOnlyList<string>>(this);
				}
				return this.VisitIndent(child);
			}).ToList<string>();
		}

		// Token: 0x06010219 RID: 66073 RVA: 0x00375EB9 File Offset: 0x003740B9
		private IReadOnlyList<string> VisitIndent(ProgramNode node)
		{
			return (from line in node.AcceptVisitor<IReadOnlyList<string>>(this)
				select "  " + line).ToList<string>();
		}

		// Token: 0x0601021A RID: 66074 RVA: 0x00375EEC File Offset: 0x003740EC
		private static string PrintLabelAttributes(string label, Dictionary<string, string> dict)
		{
			string text = string.Join(", ", dict.Select2((string k, string v) => k + " = " + v.ToLiteral(null))) ?? "";
			if (dict.Count != 1)
			{
				return label + ", { " + text + " }";
			}
			return label + "." + text;
		}

		// Token: 0x0601021C RID: 66076 RVA: 0x00375F6C File Offset: 0x0037416C
		[CompilerGenerated]
		internal static string <VisitNonterminal>g__ToOrdinal|2_0(int i)
		{
			if (i == 1)
			{
				return "1st";
			}
			if (i == 2)
			{
				return "2nd";
			}
			if (i != 3)
			{
				return string.Format("{0}th", i);
			}
			return "3rd";
		}

		// Token: 0x0400617D RID: 24957
		private const string Indentation = "  ";

		// Token: 0x0400617E RID: 24958
		private readonly List<KeyValuePair<string, string>> _selectAliases = new List<KeyValuePair<string, string>>();
	}
}
