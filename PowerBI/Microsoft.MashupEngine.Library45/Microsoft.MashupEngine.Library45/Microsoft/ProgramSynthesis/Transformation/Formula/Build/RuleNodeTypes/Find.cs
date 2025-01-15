using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001586 RID: 5510
	public struct Find : IProgramNodeBuilder, IEquatable<Find>
	{
		// Token: 0x17001F87 RID: 8071
		// (get) Token: 0x0600B468 RID: 46184 RVA: 0x00274C3A File Offset: 0x00272E3A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B469 RID: 46185 RVA: 0x00274C42 File Offset: 0x00272E42
		private Find(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B46A RID: 46186 RVA: 0x00274C4B File Offset: 0x00272E4B
		public static Find CreateUnsafe(ProgramNode node)
		{
			return new Find(node);
		}

		// Token: 0x0600B46B RID: 46187 RVA: 0x00274C54 File Offset: 0x00272E54
		public static Find? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Find)
			{
				return null;
			}
			return new Find?(Find.CreateUnsafe(node));
		}

		// Token: 0x0600B46C RID: 46188 RVA: 0x00274C8C File Offset: 0x00272E8C
		public Find(GrammarBuilders g, x value0, findDelimiter value1, findInstance value2, findOffset value3)
		{
			this._node = g.Rule.Find.BuildASTNode(new ProgramNode[] { value0.Node, value1.Node, value2.Node, value3.Node });
		}

		// Token: 0x0600B46D RID: 46189 RVA: 0x00274CDD File Offset: 0x00272EDD
		public static implicit operator pos(Find arg)
		{
			return pos.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F88 RID: 8072
		// (get) Token: 0x0600B46E RID: 46190 RVA: 0x00274CEB File Offset: 0x00272EEB
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F89 RID: 8073
		// (get) Token: 0x0600B46F RID: 46191 RVA: 0x00274CFF File Offset: 0x00272EFF
		public findDelimiter findDelimiter
		{
			get
			{
				return findDelimiter.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001F8A RID: 8074
		// (get) Token: 0x0600B470 RID: 46192 RVA: 0x00274D13 File Offset: 0x00272F13
		public findInstance findInstance
		{
			get
			{
				return findInstance.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x17001F8B RID: 8075
		// (get) Token: 0x0600B471 RID: 46193 RVA: 0x00274D27 File Offset: 0x00272F27
		public findOffset findOffset
		{
			get
			{
				return findOffset.CreateUnsafe(this.Node.Children[3]);
			}
		}

		// Token: 0x0600B472 RID: 46194 RVA: 0x00274D3B File Offset: 0x00272F3B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B473 RID: 46195 RVA: 0x00274D50 File Offset: 0x00272F50
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B474 RID: 46196 RVA: 0x00274D7A File Offset: 0x00272F7A
		public bool Equals(Find other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004634 RID: 17972
		private ProgramNode _node;
	}
}
