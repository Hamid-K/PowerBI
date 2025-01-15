using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes
{
	// Token: 0x0200127B RID: 4731
	public struct StringRegionToStringTable : IProgramNodeBuilder, IEquatable<StringRegionToStringTable>
	{
		// Token: 0x1700189B RID: 6299
		// (get) Token: 0x06008EF1 RID: 36593 RVA: 0x001E1CBA File Offset: 0x001DFEBA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008EF2 RID: 36594 RVA: 0x001E1CC2 File Offset: 0x001DFEC2
		private StringRegionToStringTable(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008EF3 RID: 36595 RVA: 0x001E1CCB File Offset: 0x001DFECB
		public static StringRegionToStringTable CreateUnsafe(ProgramNode node)
		{
			return new StringRegionToStringTable(node);
		}

		// Token: 0x06008EF4 RID: 36596 RVA: 0x001E1CD4 File Offset: 0x001DFED4
		public static StringRegionToStringTable? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.StringRegionToStringTable)
			{
				return null;
			}
			return new StringRegionToStringTable?(StringRegionToStringTable.CreateUnsafe(node));
		}

		// Token: 0x06008EF5 RID: 36597 RVA: 0x001E1D09 File Offset: 0x001DFF09
		public StringRegionToStringTable(GrammarBuilders g, eText value0)
		{
			this._node = g.Rule.StringRegionToStringTable.BuildASTNode(value0.Node);
		}

		// Token: 0x06008EF6 RID: 36598 RVA: 0x001E1D28 File Offset: 0x001DFF28
		public static implicit operator readFlatFile(StringRegionToStringTable arg)
		{
			return readFlatFile.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700189C RID: 6300
		// (get) Token: 0x06008EF7 RID: 36599 RVA: 0x001E1D36 File Offset: 0x001DFF36
		public eText eText
		{
			get
			{
				return eText.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06008EF8 RID: 36600 RVA: 0x001E1D4A File Offset: 0x001DFF4A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008EF9 RID: 36601 RVA: 0x001E1D60 File Offset: 0x001DFF60
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008EFA RID: 36602 RVA: 0x001E1D8A File Offset: 0x001DFF8A
		public bool Equals(StringRegionToStringTable other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A6C RID: 14956
		private ProgramNode _node;
	}
}
