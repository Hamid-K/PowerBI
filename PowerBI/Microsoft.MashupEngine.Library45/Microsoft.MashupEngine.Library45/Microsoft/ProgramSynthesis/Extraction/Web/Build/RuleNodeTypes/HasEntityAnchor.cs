using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001030 RID: 4144
	public struct HasEntityAnchor : IProgramNodeBuilder, IEquatable<HasEntityAnchor>
	{
		// Token: 0x170015CB RID: 5579
		// (get) Token: 0x06007A84 RID: 31364 RVA: 0x001A1F32 File Offset: 0x001A0132
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007A85 RID: 31365 RVA: 0x001A1F3A File Offset: 0x001A013A
		private HasEntityAnchor(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007A86 RID: 31366 RVA: 0x001A1F43 File Offset: 0x001A0143
		public static HasEntityAnchor CreateUnsafe(ProgramNode node)
		{
			return new HasEntityAnchor(node);
		}

		// Token: 0x06007A87 RID: 31367 RVA: 0x001A1F4C File Offset: 0x001A014C
		public static HasEntityAnchor? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.HasEntityAnchor)
			{
				return null;
			}
			return new HasEntityAnchor?(HasEntityAnchor.CreateUnsafe(node));
		}

		// Token: 0x06007A88 RID: 31368 RVA: 0x001A1F81 File Offset: 0x001A0181
		public HasEntityAnchor(GrammarBuilders g, entityObjs value0, direction value1, node value2)
		{
			this._node = g.Rule.HasEntityAnchor.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06007A89 RID: 31369 RVA: 0x001A1FAE File Offset: 0x001A01AE
		public static implicit operator atomExpr(HasEntityAnchor arg)
		{
			return atomExpr.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015CC RID: 5580
		// (get) Token: 0x06007A8A RID: 31370 RVA: 0x001A1FBC File Offset: 0x001A01BC
		public entityObjs entityObjs
		{
			get
			{
				return entityObjs.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015CD RID: 5581
		// (get) Token: 0x06007A8B RID: 31371 RVA: 0x001A1FD0 File Offset: 0x001A01D0
		public direction direction
		{
			get
			{
				return direction.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170015CE RID: 5582
		// (get) Token: 0x06007A8C RID: 31372 RVA: 0x001A1FE4 File Offset: 0x001A01E4
		public node node
		{
			get
			{
				return node.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06007A8D RID: 31373 RVA: 0x001A1FF8 File Offset: 0x001A01F8
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007A8E RID: 31374 RVA: 0x001A200C File Offset: 0x001A020C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007A8F RID: 31375 RVA: 0x001A2036 File Offset: 0x001A0236
		public bool Equals(HasEntityAnchor other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003349 RID: 13129
		private ProgramNode _node;
	}
}
