using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E6B RID: 7787
	public struct InsertAtRel : IProgramNodeBuilder, IEquatable<InsertAtRel>
	{
		// Token: 0x17002BAA RID: 11178
		// (get) Token: 0x0601068F RID: 67215 RVA: 0x00389E26 File Offset: 0x00388026
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06010690 RID: 67216 RVA: 0x00389E2E File Offset: 0x0038802E
		private InsertAtRel(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010691 RID: 67217 RVA: 0x00389E37 File Offset: 0x00388037
		public static InsertAtRel CreateUnsafe(ProgramNode node)
		{
			return new InsertAtRel(node);
		}

		// Token: 0x06010692 RID: 67218 RVA: 0x00389E40 File Offset: 0x00388040
		public static InsertAtRel? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.InsertAtRel)
			{
				return null;
			}
			return new InsertAtRel?(InsertAtRel.CreateUnsafe(node));
		}

		// Token: 0x06010693 RID: 67219 RVA: 0x00389E75 File Offset: 0x00388075
		public InsertAtRel(GrammarBuilders g, select value0, relChild value1, newDsl value2)
		{
			this._node = g.Rule.InsertAtRel.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06010694 RID: 67220 RVA: 0x00389EA2 File Offset: 0x003880A2
		public static implicit operator sequenceChildren(InsertAtRel arg)
		{
			return sequenceChildren.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002BAB RID: 11179
		// (get) Token: 0x06010695 RID: 67221 RVA: 0x00389EB0 File Offset: 0x003880B0
		public select select
		{
			get
			{
				return select.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002BAC RID: 11180
		// (get) Token: 0x06010696 RID: 67222 RVA: 0x00389EC4 File Offset: 0x003880C4
		public relChild relChild
		{
			get
			{
				return relChild.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17002BAD RID: 11181
		// (get) Token: 0x06010697 RID: 67223 RVA: 0x00389ED8 File Offset: 0x003880D8
		public newDsl newDsl
		{
			get
			{
				return newDsl.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06010698 RID: 67224 RVA: 0x00389EEC File Offset: 0x003880EC
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010699 RID: 67225 RVA: 0x00389F00 File Offset: 0x00388100
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0601069A RID: 67226 RVA: 0x00389F2A File Offset: 0x0038812A
		public bool Equals(InsertAtRel other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062AA RID: 25258
		private ProgramNode _node;
	}
}
