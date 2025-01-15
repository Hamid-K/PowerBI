using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes
{
	// Token: 0x02000F32 RID: 3890
	public struct RowMap : IProgramNodeBuilder, IEquatable<RowMap>
	{
		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x06006BC9 RID: 27593 RVA: 0x00161912 File Offset: 0x0015FB12
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006BCA RID: 27594 RVA: 0x0016191A File Offset: 0x0015FB1A
		private RowMap(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006BCB RID: 27595 RVA: 0x00161923 File Offset: 0x0015FB23
		public static RowMap CreateUnsafe(ProgramNode node)
		{
			return new RowMap(node);
		}

		// Token: 0x06006BCC RID: 27596 RVA: 0x0016192C File Offset: 0x0015FB2C
		public static RowMap? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RowMap)
			{
				return null;
			}
			return new RowMap?(RowMap.CreateUnsafe(node));
		}

		// Token: 0x06006BCD RID: 27597 RVA: 0x00161961 File Offset: 0x0015FB61
		public RowMap(GrammarBuilders g, colSplit value0, records value1)
		{
			this._node = g.Rule.RowMap.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x06006BCE RID: 27598 RVA: 0x00161993 File Offset: 0x0015FB93
		public static implicit operator table(RowMap arg)
		{
			return table.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x06006BCF RID: 27599 RVA: 0x001619A1 File Offset: 0x0015FBA1
		public colSplit colSplit
		{
			get
			{
				return colSplit.CreateUnsafe(this.Node.Children[0].Children[0]);
			}
		}

		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x06006BD0 RID: 27600 RVA: 0x001619BC File Offset: 0x0015FBBC
		public records records
		{
			get
			{
				return records.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06006BD1 RID: 27601 RVA: 0x001619D0 File Offset: 0x0015FBD0
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006BD2 RID: 27602 RVA: 0x001619E4 File Offset: 0x0015FBE4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006BD3 RID: 27603 RVA: 0x00161A0E File Offset: 0x0015FC0E
		public bool Equals(RowMap other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F1D RID: 12061
		private ProgramNode _node;
	}
}
