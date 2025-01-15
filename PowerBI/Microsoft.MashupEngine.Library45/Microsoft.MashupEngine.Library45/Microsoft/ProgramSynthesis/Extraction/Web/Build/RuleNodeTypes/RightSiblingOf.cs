using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200103E RID: 4158
	public struct RightSiblingOf : IProgramNodeBuilder, IEquatable<RightSiblingOf>
	{
		// Token: 0x170015EF RID: 5615
		// (get) Token: 0x06007B18 RID: 31512 RVA: 0x001A2C72 File Offset: 0x001A0E72
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007B19 RID: 31513 RVA: 0x001A2C7A File Offset: 0x001A0E7A
		private RightSiblingOf(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007B1A RID: 31514 RVA: 0x001A2C83 File Offset: 0x001A0E83
		public static RightSiblingOf CreateUnsafe(ProgramNode node)
		{
			return new RightSiblingOf(node);
		}

		// Token: 0x06007B1B RID: 31515 RVA: 0x001A2C8C File Offset: 0x001A0E8C
		public static RightSiblingOf? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RightSiblingOf)
			{
				return null;
			}
			return new RightSiblingOf?(RightSiblingOf.CreateUnsafe(node));
		}

		// Token: 0x06007B1C RID: 31516 RVA: 0x001A2CC1 File Offset: 0x001A0EC1
		public RightSiblingOf(GrammarBuilders g, nodeCollection value0)
		{
			this._node = g.Rule.RightSiblingOf.BuildASTNode(value0.Node);
		}

		// Token: 0x06007B1D RID: 31517 RVA: 0x001A2CE0 File Offset: 0x001A0EE0
		public static implicit operator nodeCollection(RightSiblingOf arg)
		{
			return nodeCollection.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015F0 RID: 5616
		// (get) Token: 0x06007B1E RID: 31518 RVA: 0x001A2CEE File Offset: 0x001A0EEE
		public nodeCollection nodeCollection
		{
			get
			{
				return nodeCollection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06007B1F RID: 31519 RVA: 0x001A2D02 File Offset: 0x001A0F02
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007B20 RID: 31520 RVA: 0x001A2D18 File Offset: 0x001A0F18
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007B21 RID: 31521 RVA: 0x001A2D42 File Offset: 0x001A0F42
		public bool Equals(RightSiblingOf other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003357 RID: 13143
		private ProgramNode _node;
	}
}
