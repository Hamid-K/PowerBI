using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E75 RID: 7797
	public struct InOrderAllNodes : IProgramNodeBuilder, IEquatable<InOrderAllNodes>
	{
		// Token: 0x17002BC5 RID: 11205
		// (get) Token: 0x060106FA RID: 67322 RVA: 0x0038A7BE File Offset: 0x003889BE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060106FB RID: 67323 RVA: 0x0038A7C6 File Offset: 0x003889C6
		private InOrderAllNodes(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060106FC RID: 67324 RVA: 0x0038A7CF File Offset: 0x003889CF
		public static InOrderAllNodes CreateUnsafe(ProgramNode node)
		{
			return new InOrderAllNodes(node);
		}

		// Token: 0x060106FD RID: 67325 RVA: 0x0038A7D8 File Offset: 0x003889D8
		public static InOrderAllNodes? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.InOrderAllNodes)
			{
				return null;
			}
			return new InOrderAllNodes?(InOrderAllNodes.CreateUnsafe(node));
		}

		// Token: 0x060106FE RID: 67326 RVA: 0x0038A80D File Offset: 0x00388A0D
		public InOrderAllNodes(GrammarBuilders g, selectedNode value0)
		{
			this._node = g.Rule.InOrderAllNodes.BuildASTNode(value0.Node);
		}

		// Token: 0x060106FF RID: 67327 RVA: 0x0038A82C File Offset: 0x00388A2C
		public static implicit operator inorderAllNodes(InOrderAllNodes arg)
		{
			return inorderAllNodes.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002BC6 RID: 11206
		// (get) Token: 0x06010700 RID: 67328 RVA: 0x0038A83A File Offset: 0x00388A3A
		public selectedNode selectedNode
		{
			get
			{
				return selectedNode.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06010701 RID: 67329 RVA: 0x0038A84E File Offset: 0x00388A4E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010702 RID: 67330 RVA: 0x0038A864 File Offset: 0x00388A64
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010703 RID: 67331 RVA: 0x0038A88E File Offset: 0x00388A8E
		public bool Equals(InOrderAllNodes other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062B4 RID: 25268
		private ProgramNode _node;
	}
}
