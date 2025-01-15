using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001338 RID: 4920
	public struct splitMatches_multipleMatches : IProgramNodeBuilder, IEquatable<splitMatches_multipleMatches>
	{
		// Token: 0x170019F5 RID: 6645
		// (get) Token: 0x06009778 RID: 38776 RVA: 0x002055D3 File Offset: 0x002037D3
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009779 RID: 38777 RVA: 0x002055DB File Offset: 0x002037DB
		private splitMatches_multipleMatches(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600977A RID: 38778 RVA: 0x002055E4 File Offset: 0x002037E4
		public static splitMatches_multipleMatches CreateUnsafe(ProgramNode node)
		{
			return new splitMatches_multipleMatches(node);
		}

		// Token: 0x0600977B RID: 38779 RVA: 0x002055EC File Offset: 0x002037EC
		public static splitMatches_multipleMatches? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.splitMatches_multipleMatches)
			{
				return null;
			}
			return new splitMatches_multipleMatches?(splitMatches_multipleMatches.CreateUnsafe(node));
		}

		// Token: 0x0600977C RID: 38780 RVA: 0x00205621 File Offset: 0x00203821
		public splitMatches_multipleMatches(GrammarBuilders g, multipleMatches value0)
		{
			this._node = g.UnnamedConversion.splitMatches_multipleMatches.BuildASTNode(value0.Node);
		}

		// Token: 0x0600977D RID: 38781 RVA: 0x00205640 File Offset: 0x00203840
		public static implicit operator splitMatches(splitMatches_multipleMatches arg)
		{
			return splitMatches.CreateUnsafe(arg.Node);
		}

		// Token: 0x170019F6 RID: 6646
		// (get) Token: 0x0600977E RID: 38782 RVA: 0x0020564E File Offset: 0x0020384E
		public multipleMatches multipleMatches
		{
			get
			{
				return multipleMatches.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600977F RID: 38783 RVA: 0x00205662 File Offset: 0x00203862
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009780 RID: 38784 RVA: 0x00205678 File Offset: 0x00203878
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009781 RID: 38785 RVA: 0x002056A2 File Offset: 0x002038A2
		public bool Equals(splitMatches_multipleMatches other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DAF RID: 15791
		private ProgramNode _node;
	}
}
