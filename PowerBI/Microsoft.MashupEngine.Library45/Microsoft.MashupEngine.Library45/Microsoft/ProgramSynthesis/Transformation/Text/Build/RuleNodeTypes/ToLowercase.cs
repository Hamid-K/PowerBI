using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C00 RID: 7168
	public struct ToLowercase : IProgramNodeBuilder, IEquatable<ToLowercase>
	{
		// Token: 0x17002828 RID: 10280
		// (get) Token: 0x0600F107 RID: 61703 RVA: 0x0033EFB2 File Offset: 0x0033D1B2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F108 RID: 61704 RVA: 0x0033EFBA File Offset: 0x0033D1BA
		private ToLowercase(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F109 RID: 61705 RVA: 0x0033EFC3 File Offset: 0x0033D1C3
		public static ToLowercase CreateUnsafe(ProgramNode node)
		{
			return new ToLowercase(node);
		}

		// Token: 0x0600F10A RID: 61706 RVA: 0x0033EFCC File Offset: 0x0033D1CC
		public static ToLowercase? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ToLowercase)
			{
				return null;
			}
			return new ToLowercase?(ToLowercase.CreateUnsafe(node));
		}

		// Token: 0x0600F10B RID: 61707 RVA: 0x0033F001 File Offset: 0x0033D201
		public ToLowercase(GrammarBuilders g, SS value0)
		{
			this._node = g.Rule.ToLowercase.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F10C RID: 61708 RVA: 0x0033F020 File Offset: 0x0033D220
		public static implicit operator conv(ToLowercase arg)
		{
			return conv.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002829 RID: 10281
		// (get) Token: 0x0600F10D RID: 61709 RVA: 0x0033F02E File Offset: 0x0033D22E
		public SS SS
		{
			get
			{
				return SS.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F10E RID: 61710 RVA: 0x0033F042 File Offset: 0x0033D242
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F10F RID: 61711 RVA: 0x0033F058 File Offset: 0x0033D258
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F110 RID: 61712 RVA: 0x0033F082 File Offset: 0x0033D282
		public bool Equals(ToLowercase other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AEF RID: 23279
		private ProgramNode _node;
	}
}
