using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200157D RID: 5501
	public struct SliceSuffix : IProgramNodeBuilder, IEquatable<SliceSuffix>
	{
		// Token: 0x17001F6C RID: 8044
		// (get) Token: 0x0600B405 RID: 46085 RVA: 0x0027434E File Offset: 0x0027254E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B406 RID: 46086 RVA: 0x00274356 File Offset: 0x00272556
		private SliceSuffix(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B407 RID: 46087 RVA: 0x0027435F File Offset: 0x0027255F
		public static SliceSuffix CreateUnsafe(ProgramNode node)
		{
			return new SliceSuffix(node);
		}

		// Token: 0x0600B408 RID: 46088 RVA: 0x00274368 File Offset: 0x00272568
		public static SliceSuffix? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SliceSuffix)
			{
				return null;
			}
			return new SliceSuffix?(SliceSuffix.CreateUnsafe(node));
		}

		// Token: 0x0600B409 RID: 46089 RVA: 0x0027439D File Offset: 0x0027259D
		public SliceSuffix(GrammarBuilders g, x value0, pos value1)
		{
			this._node = g.Rule.SliceSuffix.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B40A RID: 46090 RVA: 0x002743C3 File Offset: 0x002725C3
		public static implicit operator substring(SliceSuffix arg)
		{
			return substring.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F6D RID: 8045
		// (get) Token: 0x0600B40B RID: 46091 RVA: 0x002743D1 File Offset: 0x002725D1
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F6E RID: 8046
		// (get) Token: 0x0600B40C RID: 46092 RVA: 0x002743E5 File Offset: 0x002725E5
		public pos pos
		{
			get
			{
				return pos.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B40D RID: 46093 RVA: 0x002743F9 File Offset: 0x002725F9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B40E RID: 46094 RVA: 0x0027440C File Offset: 0x0027260C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B40F RID: 46095 RVA: 0x00274436 File Offset: 0x00272636
		public bool Equals(SliceSuffix other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400462B RID: 17963
		private ProgramNode _node;
	}
}
