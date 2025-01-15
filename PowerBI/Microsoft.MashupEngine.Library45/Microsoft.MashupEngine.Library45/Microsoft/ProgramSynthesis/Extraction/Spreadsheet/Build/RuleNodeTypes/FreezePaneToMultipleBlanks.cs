using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E21 RID: 3617
	public struct FreezePaneToMultipleBlanks : IProgramNodeBuilder, IEquatable<FreezePaneToMultipleBlanks>
	{
		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x0600608B RID: 24715 RVA: 0x0013DD6E File Offset: 0x0013BF6E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600608C RID: 24716 RVA: 0x0013DD76 File Offset: 0x0013BF76
		private FreezePaneToMultipleBlanks(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600608D RID: 24717 RVA: 0x0013DD7F File Offset: 0x0013BF7F
		public static FreezePaneToMultipleBlanks CreateUnsafe(ProgramNode node)
		{
			return new FreezePaneToMultipleBlanks(node);
		}

		// Token: 0x0600608E RID: 24718 RVA: 0x0013DD88 File Offset: 0x0013BF88
		public static FreezePaneToMultipleBlanks? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FreezePaneToMultipleBlanks)
			{
				return null;
			}
			return new FreezePaneToMultipleBlanks?(FreezePaneToMultipleBlanks.CreateUnsafe(node));
		}

		// Token: 0x0600608F RID: 24719 RVA: 0x0013DDBD File Offset: 0x0013BFBD
		public FreezePaneToMultipleBlanks(GrammarBuilders g, sheet value0)
		{
			this._node = g.Rule.FreezePaneToMultipleBlanks.BuildASTNode(value0.Node);
		}

		// Token: 0x06006090 RID: 24720 RVA: 0x0013DDDC File Offset: 0x0013BFDC
		public static implicit operator trimTop(FreezePaneToMultipleBlanks arg)
		{
			return trimTop.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x06006091 RID: 24721 RVA: 0x0013DDEA File Offset: 0x0013BFEA
		public sheet sheet
		{
			get
			{
				return sheet.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006092 RID: 24722 RVA: 0x0013DDFE File Offset: 0x0013BFFE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006093 RID: 24723 RVA: 0x0013DE14 File Offset: 0x0013C014
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006094 RID: 24724 RVA: 0x0013DE3E File Offset: 0x0013C03E
		public bool Equals(FreezePaneToMultipleBlanks other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BCB RID: 11211
		private ProgramNode _node;
	}
}
