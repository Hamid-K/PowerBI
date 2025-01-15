using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E20 RID: 3616
	public struct FreezePaneToBlanks : IProgramNodeBuilder, IEquatable<FreezePaneToBlanks>
	{
		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x06006081 RID: 24705 RVA: 0x0013DC8A File Offset: 0x0013BE8A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006082 RID: 24706 RVA: 0x0013DC92 File Offset: 0x0013BE92
		private FreezePaneToBlanks(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006083 RID: 24707 RVA: 0x0013DC9B File Offset: 0x0013BE9B
		public static FreezePaneToBlanks CreateUnsafe(ProgramNode node)
		{
			return new FreezePaneToBlanks(node);
		}

		// Token: 0x06006084 RID: 24708 RVA: 0x0013DCA4 File Offset: 0x0013BEA4
		public static FreezePaneToBlanks? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FreezePaneToBlanks)
			{
				return null;
			}
			return new FreezePaneToBlanks?(FreezePaneToBlanks.CreateUnsafe(node));
		}

		// Token: 0x06006085 RID: 24709 RVA: 0x0013DCD9 File Offset: 0x0013BED9
		public FreezePaneToBlanks(GrammarBuilders g, sheet value0)
		{
			this._node = g.Rule.FreezePaneToBlanks.BuildASTNode(value0.Node);
		}

		// Token: 0x06006086 RID: 24710 RVA: 0x0013DCF8 File Offset: 0x0013BEF8
		public static implicit operator trimTop(FreezePaneToBlanks arg)
		{
			return trimTop.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x06006087 RID: 24711 RVA: 0x0013DD06 File Offset: 0x0013BF06
		public sheet sheet
		{
			get
			{
				return sheet.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006088 RID: 24712 RVA: 0x0013DD1A File Offset: 0x0013BF1A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006089 RID: 24713 RVA: 0x0013DD30 File Offset: 0x0013BF30
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600608A RID: 24714 RVA: 0x0013DD5A File Offset: 0x0013BF5A
		public bool Equals(FreezePaneToBlanks other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BCA RID: 11210
		private ProgramNode _node;
	}
}
