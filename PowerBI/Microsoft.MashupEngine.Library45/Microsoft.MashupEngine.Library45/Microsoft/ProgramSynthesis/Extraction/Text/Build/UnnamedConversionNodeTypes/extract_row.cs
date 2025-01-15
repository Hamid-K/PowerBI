using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000F1F RID: 3871
	public struct extract_row : IProgramNodeBuilder, IEquatable<extract_row>
	{
		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x06006AFC RID: 27388 RVA: 0x001606AE File Offset: 0x0015E8AE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006AFD RID: 27389 RVA: 0x001606B6 File Offset: 0x0015E8B6
		private extract_row(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006AFE RID: 27390 RVA: 0x001606BF File Offset: 0x0015E8BF
		public static extract_row CreateUnsafe(ProgramNode node)
		{
			return new extract_row(node);
		}

		// Token: 0x06006AFF RID: 27391 RVA: 0x001606C8 File Offset: 0x0015E8C8
		public static extract_row? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.extract_row)
			{
				return null;
			}
			return new extract_row?(extract_row.CreateUnsafe(node));
		}

		// Token: 0x06006B00 RID: 27392 RVA: 0x001606FD File Offset: 0x0015E8FD
		public extract_row(GrammarBuilders g, row value0)
		{
			this._node = g.UnnamedConversion.extract_row.BuildASTNode(value0.Node);
		}

		// Token: 0x06006B01 RID: 27393 RVA: 0x0016071C File Offset: 0x0015E91C
		public static implicit operator extract(extract_row arg)
		{
			return extract.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x06006B02 RID: 27394 RVA: 0x0016072A File Offset: 0x0015E92A
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006B03 RID: 27395 RVA: 0x0016073E File Offset: 0x0015E93E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006B04 RID: 27396 RVA: 0x00160754 File Offset: 0x0015E954
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006B05 RID: 27397 RVA: 0x0016077E File Offset: 0x0015E97E
		public bool Equals(extract_row other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F0A RID: 12042
		private ProgramNode _node;
	}
}
