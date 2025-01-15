using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000E13 RID: 3603
	public struct aboveOrLeftmost_above : IProgramNodeBuilder, IEquatable<aboveOrLeftmost_above>
	{
		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x06005FFE RID: 24574 RVA: 0x0013D0DE File Offset: 0x0013B2DE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06005FFF RID: 24575 RVA: 0x0013D0E6 File Offset: 0x0013B2E6
		private aboveOrLeftmost_above(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006000 RID: 24576 RVA: 0x0013D0EF File Offset: 0x0013B2EF
		public static aboveOrLeftmost_above CreateUnsafe(ProgramNode node)
		{
			return new aboveOrLeftmost_above(node);
		}

		// Token: 0x06006001 RID: 24577 RVA: 0x0013D0F8 File Offset: 0x0013B2F8
		public static aboveOrLeftmost_above? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.aboveOrLeftmost_above)
			{
				return null;
			}
			return new aboveOrLeftmost_above?(aboveOrLeftmost_above.CreateUnsafe(node));
		}

		// Token: 0x06006002 RID: 24578 RVA: 0x0013D12D File Offset: 0x0013B32D
		public aboveOrLeftmost_above(GrammarBuilders g, above value0)
		{
			this._node = g.UnnamedConversion.aboveOrLeftmost_above.BuildASTNode(value0.Node);
		}

		// Token: 0x06006003 RID: 24579 RVA: 0x0013D14C File Offset: 0x0013B34C
		public static implicit operator aboveOrLeftmost(aboveOrLeftmost_above arg)
		{
			return aboveOrLeftmost.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x06006004 RID: 24580 RVA: 0x0013D15A File Offset: 0x0013B35A
		public above above
		{
			get
			{
				return above.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006005 RID: 24581 RVA: 0x0013D16E File Offset: 0x0013B36E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006006 RID: 24582 RVA: 0x0013D184 File Offset: 0x0013B384
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006007 RID: 24583 RVA: 0x0013D1AE File Offset: 0x0013B3AE
		public bool Equals(aboveOrLeftmost_above other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BBD RID: 11197
		private ProgramNode _node;
	}
}
