using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015F0 RID: 5616
	public struct findDelimiter : IProgramNodeBuilder, IEquatable<findDelimiter>
	{
		// Token: 0x17002037 RID: 8247
		// (get) Token: 0x0600BA84 RID: 47748 RVA: 0x00282BDA File Offset: 0x00280DDA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BA85 RID: 47749 RVA: 0x00282BE2 File Offset: 0x00280DE2
		private findDelimiter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BA86 RID: 47750 RVA: 0x00282BEB File Offset: 0x00280DEB
		public static findDelimiter CreateUnsafe(ProgramNode node)
		{
			return new findDelimiter(node);
		}

		// Token: 0x0600BA87 RID: 47751 RVA: 0x00282BF4 File Offset: 0x00280DF4
		public static findDelimiter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.findDelimiter)
			{
				return null;
			}
			return new findDelimiter?(findDelimiter.CreateUnsafe(node));
		}

		// Token: 0x0600BA88 RID: 47752 RVA: 0x00282C2E File Offset: 0x00280E2E
		public static findDelimiter CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new findDelimiter(new Hole(g.Symbol.findDelimiter, holeId));
		}

		// Token: 0x0600BA89 RID: 47753 RVA: 0x00282C46 File Offset: 0x00280E46
		public findDelimiter(GrammarBuilders g, string value)
		{
			this = new findDelimiter(new LiteralNode(g.Symbol.findDelimiter, value));
		}

		// Token: 0x17002038 RID: 8248
		// (get) Token: 0x0600BA8A RID: 47754 RVA: 0x00282C5F File Offset: 0x00280E5F
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600BA8B RID: 47755 RVA: 0x00282C76 File Offset: 0x00280E76
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BA8C RID: 47756 RVA: 0x00282C8C File Offset: 0x00280E8C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BA8D RID: 47757 RVA: 0x00282CB6 File Offset: 0x00280EB6
		public bool Equals(findDelimiter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400469E RID: 18078
		private ProgramNode _node;
	}
}
