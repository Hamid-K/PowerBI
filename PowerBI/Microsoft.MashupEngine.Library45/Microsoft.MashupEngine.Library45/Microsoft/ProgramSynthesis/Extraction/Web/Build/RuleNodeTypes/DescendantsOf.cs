using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200103D RID: 4157
	public struct DescendantsOf : IProgramNodeBuilder, IEquatable<DescendantsOf>
	{
		// Token: 0x170015ED RID: 5613
		// (get) Token: 0x06007B0E RID: 31502 RVA: 0x001A2B8E File Offset: 0x001A0D8E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007B0F RID: 31503 RVA: 0x001A2B96 File Offset: 0x001A0D96
		private DescendantsOf(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007B10 RID: 31504 RVA: 0x001A2B9F File Offset: 0x001A0D9F
		public static DescendantsOf CreateUnsafe(ProgramNode node)
		{
			return new DescendantsOf(node);
		}

		// Token: 0x06007B11 RID: 31505 RVA: 0x001A2BA8 File Offset: 0x001A0DA8
		public static DescendantsOf? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.DescendantsOf)
			{
				return null;
			}
			return new DescendantsOf?(DescendantsOf.CreateUnsafe(node));
		}

		// Token: 0x06007B12 RID: 31506 RVA: 0x001A2BDD File Offset: 0x001A0DDD
		public DescendantsOf(GrammarBuilders g, nodeCollection value0)
		{
			this._node = g.Rule.DescendantsOf.BuildASTNode(value0.Node);
		}

		// Token: 0x06007B13 RID: 31507 RVA: 0x001A2BFC File Offset: 0x001A0DFC
		public static implicit operator nodeCollection(DescendantsOf arg)
		{
			return nodeCollection.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015EE RID: 5614
		// (get) Token: 0x06007B14 RID: 31508 RVA: 0x001A2C0A File Offset: 0x001A0E0A
		public nodeCollection nodeCollection
		{
			get
			{
				return nodeCollection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06007B15 RID: 31509 RVA: 0x001A2C1E File Offset: 0x001A0E1E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007B16 RID: 31510 RVA: 0x001A2C34 File Offset: 0x001A0E34
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007B17 RID: 31511 RVA: 0x001A2C5E File Offset: 0x001A0E5E
		public bool Equals(DescendantsOf other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003356 RID: 13142
		private ProgramNode _node;
	}
}
