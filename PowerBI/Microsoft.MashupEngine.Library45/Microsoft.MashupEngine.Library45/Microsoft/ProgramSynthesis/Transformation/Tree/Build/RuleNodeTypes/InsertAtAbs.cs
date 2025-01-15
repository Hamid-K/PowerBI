using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E6A RID: 7786
	public struct InsertAtAbs : IProgramNodeBuilder, IEquatable<InsertAtAbs>
	{
		// Token: 0x17002BA6 RID: 11174
		// (get) Token: 0x06010683 RID: 67203 RVA: 0x00389D0E File Offset: 0x00387F0E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06010684 RID: 67204 RVA: 0x00389D16 File Offset: 0x00387F16
		private InsertAtAbs(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010685 RID: 67205 RVA: 0x00389D1F File Offset: 0x00387F1F
		public static InsertAtAbs CreateUnsafe(ProgramNode node)
		{
			return new InsertAtAbs(node);
		}

		// Token: 0x06010686 RID: 67206 RVA: 0x00389D28 File Offset: 0x00387F28
		public static InsertAtAbs? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.InsertAtAbs)
			{
				return null;
			}
			return new InsertAtAbs?(InsertAtAbs.CreateUnsafe(node));
		}

		// Token: 0x06010687 RID: 67207 RVA: 0x00389D5D File Offset: 0x00387F5D
		public InsertAtAbs(GrammarBuilders g, select value0, pos value1, newDsl value2)
		{
			this._node = g.Rule.InsertAtAbs.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06010688 RID: 67208 RVA: 0x00389D8A File Offset: 0x00387F8A
		public static implicit operator sequenceChildren(InsertAtAbs arg)
		{
			return sequenceChildren.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002BA7 RID: 11175
		// (get) Token: 0x06010689 RID: 67209 RVA: 0x00389D98 File Offset: 0x00387F98
		public select select
		{
			get
			{
				return select.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002BA8 RID: 11176
		// (get) Token: 0x0601068A RID: 67210 RVA: 0x00389DAC File Offset: 0x00387FAC
		public pos pos
		{
			get
			{
				return pos.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17002BA9 RID: 11177
		// (get) Token: 0x0601068B RID: 67211 RVA: 0x00389DC0 File Offset: 0x00387FC0
		public newDsl newDsl
		{
			get
			{
				return newDsl.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0601068C RID: 67212 RVA: 0x00389DD4 File Offset: 0x00387FD4
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0601068D RID: 67213 RVA: 0x00389DE8 File Offset: 0x00387FE8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0601068E RID: 67214 RVA: 0x00389E12 File Offset: 0x00388012
		public bool Equals(InsertAtAbs other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062A9 RID: 25257
		private ProgramNode _node;
	}
}
