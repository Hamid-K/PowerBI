using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E6C RID: 7788
	public struct DeleteChild : IProgramNodeBuilder, IEquatable<DeleteChild>
	{
		// Token: 0x17002BAE RID: 11182
		// (get) Token: 0x0601069B RID: 67227 RVA: 0x00389F3E File Offset: 0x0038813E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0601069C RID: 67228 RVA: 0x00389F46 File Offset: 0x00388146
		private DeleteChild(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0601069D RID: 67229 RVA: 0x00389F4F File Offset: 0x0038814F
		public static DeleteChild CreateUnsafe(ProgramNode node)
		{
			return new DeleteChild(node);
		}

		// Token: 0x0601069E RID: 67230 RVA: 0x00389F58 File Offset: 0x00388158
		public static DeleteChild? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.DeleteChild)
			{
				return null;
			}
			return new DeleteChild?(DeleteChild.CreateUnsafe(node));
		}

		// Token: 0x0601069F RID: 67231 RVA: 0x00389F8D File Offset: 0x0038818D
		public DeleteChild(GrammarBuilders g, select value0, relChild value1)
		{
			this._node = g.Rule.DeleteChild.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060106A0 RID: 67232 RVA: 0x00389FB3 File Offset: 0x003881B3
		public static implicit operator sequenceChildren(DeleteChild arg)
		{
			return sequenceChildren.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002BAF RID: 11183
		// (get) Token: 0x060106A1 RID: 67233 RVA: 0x00389FC1 File Offset: 0x003881C1
		public select select
		{
			get
			{
				return select.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002BB0 RID: 11184
		// (get) Token: 0x060106A2 RID: 67234 RVA: 0x00389FD5 File Offset: 0x003881D5
		public relChild relChild
		{
			get
			{
				return relChild.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060106A3 RID: 67235 RVA: 0x00389FE9 File Offset: 0x003881E9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060106A4 RID: 67236 RVA: 0x00389FFC File Offset: 0x003881FC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060106A5 RID: 67237 RVA: 0x0038A026 File Offset: 0x00388226
		public bool Equals(DeleteChild other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062AB RID: 25259
		private ProgramNode _node;
	}
}
