using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C01 RID: 7169
	public struct ToUppercase : IProgramNodeBuilder, IEquatable<ToUppercase>
	{
		// Token: 0x1700282A RID: 10282
		// (get) Token: 0x0600F111 RID: 61713 RVA: 0x0033F096 File Offset: 0x0033D296
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F112 RID: 61714 RVA: 0x0033F09E File Offset: 0x0033D29E
		private ToUppercase(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F113 RID: 61715 RVA: 0x0033F0A7 File Offset: 0x0033D2A7
		public static ToUppercase CreateUnsafe(ProgramNode node)
		{
			return new ToUppercase(node);
		}

		// Token: 0x0600F114 RID: 61716 RVA: 0x0033F0B0 File Offset: 0x0033D2B0
		public static ToUppercase? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ToUppercase)
			{
				return null;
			}
			return new ToUppercase?(ToUppercase.CreateUnsafe(node));
		}

		// Token: 0x0600F115 RID: 61717 RVA: 0x0033F0E5 File Offset: 0x0033D2E5
		public ToUppercase(GrammarBuilders g, SS value0)
		{
			this._node = g.Rule.ToUppercase.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F116 RID: 61718 RVA: 0x0033F104 File Offset: 0x0033D304
		public static implicit operator conv(ToUppercase arg)
		{
			return conv.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700282B RID: 10283
		// (get) Token: 0x0600F117 RID: 61719 RVA: 0x0033F112 File Offset: 0x0033D312
		public SS SS
		{
			get
			{
				return SS.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F118 RID: 61720 RVA: 0x0033F126 File Offset: 0x0033D326
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F119 RID: 61721 RVA: 0x0033F13C File Offset: 0x0033D33C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F11A RID: 61722 RVA: 0x0033F166 File Offset: 0x0033D366
		public bool Equals(ToUppercase other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AF0 RID: 23280
		private ProgramNode _node;
	}
}
