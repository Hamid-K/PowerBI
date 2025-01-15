using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C2D RID: 7213
	public struct LetX : IProgramNodeBuilder, IEquatable<LetX>
	{
		// Token: 0x170028AF RID: 10415
		// (get) Token: 0x0600F2F6 RID: 62198 RVA: 0x00341CBE File Offset: 0x0033FEBE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F2F7 RID: 62199 RVA: 0x00341CC6 File Offset: 0x0033FEC6
		private LetX(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F2F8 RID: 62200 RVA: 0x00341CCF File Offset: 0x0033FECF
		public static LetX CreateUnsafe(ProgramNode node)
		{
			return new LetX(node);
		}

		// Token: 0x0600F2F9 RID: 62201 RVA: 0x00341CD8 File Offset: 0x0033FED8
		public static LetX? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetX)
			{
				return null;
			}
			return new LetX?(LetX.CreateUnsafe(node));
		}

		// Token: 0x0600F2FA RID: 62202 RVA: 0x00341D0D File Offset: 0x0033FF0D
		public LetX(GrammarBuilders g, v value0, conv value1)
		{
			this._node = new LetNode(g.Rule.LetX, value0.Node, value1.Node);
		}

		// Token: 0x0600F2FB RID: 62203 RVA: 0x00341D33 File Offset: 0x0033FF33
		public static implicit operator letOptions(LetX arg)
		{
			return letOptions.CreateUnsafe(arg.Node);
		}

		// Token: 0x170028B0 RID: 10416
		// (get) Token: 0x0600F2FC RID: 62204 RVA: 0x00341D41 File Offset: 0x0033FF41
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170028B1 RID: 10417
		// (get) Token: 0x0600F2FD RID: 62205 RVA: 0x00341D55 File Offset: 0x0033FF55
		public conv conv
		{
			get
			{
				return conv.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F2FE RID: 62206 RVA: 0x00341D69 File Offset: 0x0033FF69
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F2FF RID: 62207 RVA: 0x00341D7C File Offset: 0x0033FF7C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F300 RID: 62208 RVA: 0x00341DA6 File Offset: 0x0033FFA6
		public bool Equals(LetX other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B1C RID: 23324
		private ProgramNode _node;
	}
}
