using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000E12 RID: 3602
	public struct title_above : IProgramNodeBuilder, IEquatable<title_above>
	{
		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x06005FF4 RID: 24564 RVA: 0x0013CFFA File Offset: 0x0013B1FA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06005FF5 RID: 24565 RVA: 0x0013D002 File Offset: 0x0013B202
		private title_above(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06005FF6 RID: 24566 RVA: 0x0013D00B File Offset: 0x0013B20B
		public static title_above CreateUnsafe(ProgramNode node)
		{
			return new title_above(node);
		}

		// Token: 0x06005FF7 RID: 24567 RVA: 0x0013D014 File Offset: 0x0013B214
		public static title_above? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.title_above)
			{
				return null;
			}
			return new title_above?(title_above.CreateUnsafe(node));
		}

		// Token: 0x06005FF8 RID: 24568 RVA: 0x0013D049 File Offset: 0x0013B249
		public title_above(GrammarBuilders g, above value0)
		{
			this._node = g.UnnamedConversion.title_above.BuildASTNode(value0.Node);
		}

		// Token: 0x06005FF9 RID: 24569 RVA: 0x0013D068 File Offset: 0x0013B268
		public static implicit operator title(title_above arg)
		{
			return title.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x06005FFA RID: 24570 RVA: 0x0013D076 File Offset: 0x0013B276
		public above above
		{
			get
			{
				return above.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06005FFB RID: 24571 RVA: 0x0013D08A File Offset: 0x0013B28A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06005FFC RID: 24572 RVA: 0x0013D0A0 File Offset: 0x0013B2A0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06005FFD RID: 24573 RVA: 0x0013D0CA File Offset: 0x0013B2CA
		public bool Equals(title_above other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BBC RID: 11196
		private ProgramNode _node;
	}
}
