using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200102E RID: 4142
	public struct HasAttribute : IProgramNodeBuilder, IEquatable<HasAttribute>
	{
		// Token: 0x170015C3 RID: 5571
		// (get) Token: 0x06007A6C RID: 31340 RVA: 0x001A1D02 File Offset: 0x0019FF02
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007A6D RID: 31341 RVA: 0x001A1D0A File Offset: 0x0019FF0A
		private HasAttribute(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007A6E RID: 31342 RVA: 0x001A1D13 File Offset: 0x0019FF13
		public static HasAttribute CreateUnsafe(ProgramNode node)
		{
			return new HasAttribute(node);
		}

		// Token: 0x06007A6F RID: 31343 RVA: 0x001A1D1C File Offset: 0x0019FF1C
		public static HasAttribute? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.HasAttribute)
			{
				return null;
			}
			return new HasAttribute?(HasAttribute.CreateUnsafe(node));
		}

		// Token: 0x06007A70 RID: 31344 RVA: 0x001A1D51 File Offset: 0x0019FF51
		public HasAttribute(GrammarBuilders g, name value0, value value1, node value2)
		{
			this._node = g.Rule.HasAttribute.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06007A71 RID: 31345 RVA: 0x001A1D7E File Offset: 0x0019FF7E
		public static implicit operator atomExpr(HasAttribute arg)
		{
			return atomExpr.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015C4 RID: 5572
		// (get) Token: 0x06007A72 RID: 31346 RVA: 0x001A1D8C File Offset: 0x0019FF8C
		public name name
		{
			get
			{
				return name.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015C5 RID: 5573
		// (get) Token: 0x06007A73 RID: 31347 RVA: 0x001A1DA0 File Offset: 0x0019FFA0
		public value value
		{
			get
			{
				return value.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170015C6 RID: 5574
		// (get) Token: 0x06007A74 RID: 31348 RVA: 0x001A1DB4 File Offset: 0x0019FFB4
		public node node
		{
			get
			{
				return node.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06007A75 RID: 31349 RVA: 0x001A1DC8 File Offset: 0x0019FFC8
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007A76 RID: 31350 RVA: 0x001A1DDC File Offset: 0x0019FFDC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007A77 RID: 31351 RVA: 0x001A1E06 File Offset: 0x001A0006
		public bool Equals(HasAttribute other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003347 RID: 13127
		private ProgramNode _node;
	}
}
