using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x0200134F RID: 4943
	public struct Empty : IProgramNodeBuilder, IEquatable<Empty>
	{
		// Token: 0x17001A3A RID: 6714
		// (get) Token: 0x06009875 RID: 39029 RVA: 0x00206CCA File Offset: 0x00204ECA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009876 RID: 39030 RVA: 0x00206CD2 File Offset: 0x00204ED2
		private Empty(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009877 RID: 39031 RVA: 0x00206CDB File Offset: 0x00204EDB
		public static Empty CreateUnsafe(ProgramNode node)
		{
			return new Empty(node);
		}

		// Token: 0x06009878 RID: 39032 RVA: 0x00206CE4 File Offset: 0x00204EE4
		public static Empty? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Empty)
			{
				return null;
			}
			return new Empty?(Empty.CreateUnsafe(node));
		}

		// Token: 0x06009879 RID: 39033 RVA: 0x00206D19 File Offset: 0x00204F19
		public Empty(GrammarBuilders g, v value0)
		{
			this._node = g.Rule.Empty.BuildASTNode(value0.Node);
		}

		// Token: 0x0600987A RID: 39034 RVA: 0x00206D38 File Offset: 0x00204F38
		public static implicit operator r(Empty arg)
		{
			return r.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A3B RID: 6715
		// (get) Token: 0x0600987B RID: 39035 RVA: 0x00206D46 File Offset: 0x00204F46
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600987C RID: 39036 RVA: 0x00206D5A File Offset: 0x00204F5A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600987D RID: 39037 RVA: 0x00206D70 File Offset: 0x00204F70
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600987E RID: 39038 RVA: 0x00206D9A File Offset: 0x00204F9A
		public bool Equals(Empty other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DC6 RID: 15814
		private ProgramNode _node;
	}
}
