using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000F21 RID: 3873
	public struct skip_lines : IProgramNodeBuilder, IEquatable<skip_lines>
	{
		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x06006B10 RID: 27408 RVA: 0x00160876 File Offset: 0x0015EA76
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006B11 RID: 27409 RVA: 0x0016087E File Offset: 0x0015EA7E
		private skip_lines(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006B12 RID: 27410 RVA: 0x00160887 File Offset: 0x0015EA87
		public static skip_lines CreateUnsafe(ProgramNode node)
		{
			return new skip_lines(node);
		}

		// Token: 0x06006B13 RID: 27411 RVA: 0x00160890 File Offset: 0x0015EA90
		public static skip_lines? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.skip_lines)
			{
				return null;
			}
			return new skip_lines?(skip_lines.CreateUnsafe(node));
		}

		// Token: 0x06006B14 RID: 27412 RVA: 0x001608C5 File Offset: 0x0015EAC5
		public skip_lines(GrammarBuilders g, lines value0)
		{
			this._node = g.UnnamedConversion.skip_lines.BuildASTNode(value0.Node);
		}

		// Token: 0x06006B15 RID: 27413 RVA: 0x001608E4 File Offset: 0x0015EAE4
		public static implicit operator skip(skip_lines arg)
		{
			return skip.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x06006B16 RID: 27414 RVA: 0x001608F2 File Offset: 0x0015EAF2
		public lines lines
		{
			get
			{
				return lines.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006B17 RID: 27415 RVA: 0x00160906 File Offset: 0x0015EB06
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006B18 RID: 27416 RVA: 0x0016091C File Offset: 0x0015EB1C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006B19 RID: 27417 RVA: 0x00160946 File Offset: 0x0015EB46
		public bool Equals(skip_lines other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F0C RID: 12044
		private ProgramNode _node;
	}
}
