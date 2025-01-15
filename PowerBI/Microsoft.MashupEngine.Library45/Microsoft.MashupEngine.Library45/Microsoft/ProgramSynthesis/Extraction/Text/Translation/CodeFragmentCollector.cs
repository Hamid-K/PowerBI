using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Translation
{
	// Token: 0x02000F9A RID: 3994
	internal sealed class CodeFragmentCollector : ProgramNodeVisitor<object>
	{
		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x06006E65 RID: 28261 RVA: 0x00168A03 File Offset: 0x00166C03
		// (set) Token: 0x06006E66 RID: 28262 RVA: 0x00168A0B File Offset: 0x00166C0B
		internal IReadOnlyList<string> ColumnNames { get; private set; }

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x06006E67 RID: 28263 RVA: 0x00168A14 File Offset: 0x00166C14
		internal IReadOnlyList<trimExtract> Extracts
		{
			get
			{
				return this._extracts;
			}
		}

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x06006E68 RID: 28264 RVA: 0x00168A1C File Offset: 0x00166C1C
		// (set) Token: 0x06006E69 RID: 28265 RVA: 0x00168A24 File Offset: 0x00166C24
		internal records Group { get; private set; }

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x06006E6A RID: 28266 RVA: 0x00168A2D File Offset: 0x00166C2D
		// (set) Token: 0x06006E6B RID: 28267 RVA: 0x00168A35 File Offset: 0x00166C35
		internal int SkipLines { get; private set; }

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x06006E6C RID: 28268 RVA: 0x00168A3E File Offset: 0x00166C3E
		internal IReadOnlyList<split> Splits
		{
			get
			{
				return this._splits;
			}
		}

		// Token: 0x06006E6D RID: 28269 RVA: 0x00168A48 File Offset: 0x00166C48
		public override object VisitNonterminal(NonterminalNode node)
		{
			List list;
			Skip skip;
			records_skip records_skip;
			Select select;
			Group group;
			MergeEvery mergeEvery;
			Table table;
			if (Language.Build.Node.IsRule.List(node, out list))
			{
				this._extracts.Add(list.trimExtract);
			}
			else if (Language.Build.Node.IsRule.Skip(node, out skip))
			{
				this.SkipLines = skip.k.Value;
			}
			else if (Language.Build.Node.IsRule.records_skip(node, out records_skip))
			{
				this.Group = records_skip;
			}
			else if (Language.Build.Node.IsRule.Select(node, out select))
			{
				this.Group = select;
			}
			else if (Language.Build.Node.IsRule.Group(node, out group))
			{
				this.Group = group;
			}
			else if (Language.Build.Node.IsRule.MergeEvery(node, out mergeEvery))
			{
				this.Group = mergeEvery;
			}
			else if (Language.Build.Node.IsRule.Table(node, out table))
			{
				this.ColumnNames = table.columnNames.Value;
			}
			ProgramNode[] children = node.Children;
			for (int i = 0; i < children.Length; i++)
			{
				children[i].AcceptVisitor<object>(this);
			}
			return null;
		}

		// Token: 0x06006E6E RID: 28270 RVA: 0x00168BB4 File Offset: 0x00166DB4
		public override object VisitLet(LetNode node)
		{
			LetSplit letSplit;
			if (Language.Build.Node.IsRule.LetSplit(node, out letSplit))
			{
				this._splits.Add(letSplit.split);
			}
			LetExtractTup letExtractTup;
			if (Language.Build.Node.IsRule.LetExtractTup(node, out letExtractTup))
			{
				this._extracts.Add(letExtractTup.trimExtract);
			}
			ProgramNode[] children = node.Children;
			for (int i = 0; i < children.Length; i++)
			{
				children[i].AcceptVisitor<object>(this);
			}
			return null;
		}

		// Token: 0x06006E6F RID: 28271 RVA: 0x00168C38 File Offset: 0x00166E38
		public override object VisitLambda(LambdaNode node)
		{
			ProgramNode[] children = node.Children;
			for (int i = 0; i < children.Length; i++)
			{
				children[i].AcceptVisitor<object>(this);
			}
			return null;
		}

		// Token: 0x06006E70 RID: 28272 RVA: 0x00002188 File Offset: 0x00000388
		public override object VisitLiteral(LiteralNode node)
		{
			return null;
		}

		// Token: 0x06006E71 RID: 28273 RVA: 0x00002188 File Offset: 0x00000388
		public override object VisitVariable(VariableNode node)
		{
			return null;
		}

		// Token: 0x06006E72 RID: 28274 RVA: 0x00002188 File Offset: 0x00000388
		public override object VisitHole(Hole node)
		{
			return null;
		}

		// Token: 0x04003016 RID: 12310
		private readonly List<trimExtract> _extracts = new List<trimExtract>();

		// Token: 0x04003017 RID: 12311
		private readonly List<split> _splits = new List<split>();
	}
}
