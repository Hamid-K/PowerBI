using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000FFF RID: 4095
	public struct resultRegion_subNode : IProgramNodeBuilder, IEquatable<resultRegion_subNode>
	{
		// Token: 0x17001554 RID: 5460
		// (get) Token: 0x06007885 RID: 30853 RVA: 0x0019F18E File Offset: 0x0019D38E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007886 RID: 30854 RVA: 0x0019F196 File Offset: 0x0019D396
		private resultRegion_subNode(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007887 RID: 30855 RVA: 0x0019F19F File Offset: 0x0019D39F
		public static resultRegion_subNode CreateUnsafe(ProgramNode node)
		{
			return new resultRegion_subNode(node);
		}

		// Token: 0x06007888 RID: 30856 RVA: 0x0019F1A8 File Offset: 0x0019D3A8
		public static resultRegion_subNode? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.resultRegion_subNode)
			{
				return null;
			}
			return new resultRegion_subNode?(resultRegion_subNode.CreateUnsafe(node));
		}

		// Token: 0x06007889 RID: 30857 RVA: 0x0019F1DD File Offset: 0x0019D3DD
		public resultRegion_subNode(GrammarBuilders g, subNode value0)
		{
			this._node = g.UnnamedConversion.resultRegion_subNode.BuildASTNode(value0.Node);
		}

		// Token: 0x0600788A RID: 30858 RVA: 0x0019F1FC File Offset: 0x0019D3FC
		public static implicit operator resultRegion(resultRegion_subNode arg)
		{
			return resultRegion.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001555 RID: 5461
		// (get) Token: 0x0600788B RID: 30859 RVA: 0x0019F20A File Offset: 0x0019D40A
		public subNode subNode
		{
			get
			{
				return subNode.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600788C RID: 30860 RVA: 0x0019F21E File Offset: 0x0019D41E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600788D RID: 30861 RVA: 0x0019F234 File Offset: 0x0019D434
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600788E RID: 30862 RVA: 0x0019F25E File Offset: 0x0019D45E
		public bool Equals(resultRegion_subNode other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003318 RID: 13080
		private ProgramNode _node;
	}
}
