using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001082 RID: 4226
	public struct resultTable : IProgramNodeBuilder, IEquatable<resultTable>
	{
		// Token: 0x1700166B RID: 5739
		// (get) Token: 0x06007ED9 RID: 32473 RVA: 0x001AA97A File Offset: 0x001A8B7A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007EDA RID: 32474 RVA: 0x001AA982 File Offset: 0x001A8B82
		private resultTable(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007EDB RID: 32475 RVA: 0x001AA98B File Offset: 0x001A8B8B
		public static resultTable CreateUnsafe(ProgramNode node)
		{
			return new resultTable(node);
		}

		// Token: 0x06007EDC RID: 32476 RVA: 0x001AA994 File Offset: 0x001A8B94
		public static resultTable? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.resultTable)
			{
				return null;
			}
			return new resultTable?(resultTable.CreateUnsafe(node));
		}

		// Token: 0x06007EDD RID: 32477 RVA: 0x001AA9CE File Offset: 0x001A8BCE
		public static resultTable CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new resultTable(new Hole(g.Symbol.resultTable, holeId));
		}

		// Token: 0x06007EDE RID: 32478 RVA: 0x001AA9E6 File Offset: 0x001A8BE6
		public bool Is_ExtractTable(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ExtractTable;
		}

		// Token: 0x06007EDF RID: 32479 RVA: 0x001AAA00 File Offset: 0x001A8C00
		public bool Is_ExtractTable(GrammarBuilders g, out ExtractTable value)
		{
			if (this.Node.GrammarRule == g.Rule.ExtractTable)
			{
				value = ExtractTable.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ExtractTable);
			return false;
		}

		// Token: 0x06007EE0 RID: 32480 RVA: 0x001AAA38 File Offset: 0x001A8C38
		public ExtractTable? As_ExtractTable(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ExtractTable)
			{
				return null;
			}
			return new ExtractTable?(ExtractTable.CreateUnsafe(this.Node));
		}

		// Token: 0x06007EE1 RID: 32481 RVA: 0x001AAA78 File Offset: 0x001A8C78
		public ExtractTable Cast_ExtractTable(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ExtractTable)
			{
				return ExtractTable.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ExtractTable is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007EE2 RID: 32482 RVA: 0x001AAACD File Offset: 0x001A8CCD
		public bool Is_ExtractRowBasedTable(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ExtractRowBasedTable;
		}

		// Token: 0x06007EE3 RID: 32483 RVA: 0x001AAAE7 File Offset: 0x001A8CE7
		public bool Is_ExtractRowBasedTable(GrammarBuilders g, out ExtractRowBasedTable value)
		{
			if (this.Node.GrammarRule == g.Rule.ExtractRowBasedTable)
			{
				value = ExtractRowBasedTable.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ExtractRowBasedTable);
			return false;
		}

		// Token: 0x06007EE4 RID: 32484 RVA: 0x001AAB1C File Offset: 0x001A8D1C
		public ExtractRowBasedTable? As_ExtractRowBasedTable(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ExtractRowBasedTable)
			{
				return null;
			}
			return new ExtractRowBasedTable?(ExtractRowBasedTable.CreateUnsafe(this.Node));
		}

		// Token: 0x06007EE5 RID: 32485 RVA: 0x001AAB5C File Offset: 0x001A8D5C
		public ExtractRowBasedTable Cast_ExtractRowBasedTable(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ExtractRowBasedTable)
			{
				return ExtractRowBasedTable.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ExtractRowBasedTable is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007EE6 RID: 32486 RVA: 0x001AABB4 File Offset: 0x001A8DB4
		public T Switch<T>(GrammarBuilders g, Func<ExtractTable, T> func0, Func<ExtractRowBasedTable, T> func1)
		{
			ExtractTable extractTable;
			if (this.Is_ExtractTable(g, out extractTable))
			{
				return func0(extractTable);
			}
			ExtractRowBasedTable extractRowBasedTable;
			if (this.Is_ExtractRowBasedTable(g, out extractRowBasedTable))
			{
				return func1(extractRowBasedTable);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol resultTable");
		}

		// Token: 0x06007EE7 RID: 32487 RVA: 0x001AAC0C File Offset: 0x001A8E0C
		public void Switch(GrammarBuilders g, Action<ExtractTable> func0, Action<ExtractRowBasedTable> func1)
		{
			ExtractTable extractTable;
			if (this.Is_ExtractTable(g, out extractTable))
			{
				func0(extractTable);
				return;
			}
			ExtractRowBasedTable extractRowBasedTable;
			if (this.Is_ExtractRowBasedTable(g, out extractRowBasedTable))
			{
				func1(extractRowBasedTable);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol resultTable");
		}

		// Token: 0x06007EE8 RID: 32488 RVA: 0x001AAC63 File Offset: 0x001A8E63
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007EE9 RID: 32489 RVA: 0x001AAC78 File Offset: 0x001A8E78
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007EEA RID: 32490 RVA: 0x001AACA2 File Offset: 0x001A8EA2
		public bool Equals(resultTable other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400339B RID: 13211
		private ProgramNode _node;
	}
}
