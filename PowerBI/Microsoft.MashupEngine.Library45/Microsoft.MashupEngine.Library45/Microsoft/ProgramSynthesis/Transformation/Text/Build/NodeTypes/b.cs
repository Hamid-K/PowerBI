using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C53 RID: 7251
	public struct b : IProgramNodeBuilder, IEquatable<b>
	{
		// Token: 0x170028E9 RID: 10473
		// (get) Token: 0x0600F554 RID: 62804 RVA: 0x003477DA File Offset: 0x003459DA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F555 RID: 62805 RVA: 0x003477E2 File Offset: 0x003459E2
		private b(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F556 RID: 62806 RVA: 0x003477EB File Offset: 0x003459EB
		public static b CreateUnsafe(ProgramNode node)
		{
			return new b(node);
		}

		// Token: 0x0600F557 RID: 62807 RVA: 0x003477F4 File Offset: 0x003459F4
		public static b? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.b)
			{
				return null;
			}
			return new b?(b.CreateUnsafe(node));
		}

		// Token: 0x0600F558 RID: 62808 RVA: 0x0034782E File Offset: 0x00345A2E
		public static b CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new b(new Hole(g.Symbol.b, holeId));
		}

		// Token: 0x0600F559 RID: 62809 RVA: 0x00347846 File Offset: 0x00345A46
		public LetPredicate Cast_LetPredicate()
		{
			return LetPredicate.CreateUnsafe(this.Node);
		}

		// Token: 0x0600F55A RID: 62810 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LetPredicate(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600F55B RID: 62811 RVA: 0x00347853 File Offset: 0x00345A53
		public bool Is_LetPredicate(GrammarBuilders g, out LetPredicate value)
		{
			value = LetPredicate.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600F55C RID: 62812 RVA: 0x00347867 File Offset: 0x00345A67
		public LetPredicate? As_LetPredicate(GrammarBuilders g)
		{
			return new LetPredicate?(LetPredicate.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F55D RID: 62813 RVA: 0x00347879 File Offset: 0x00345A79
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F55E RID: 62814 RVA: 0x0034788C File Offset: 0x00345A8C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F55F RID: 62815 RVA: 0x003478B6 File Offset: 0x00345AB6
		public bool Equals(b other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B42 RID: 23362
		private ProgramNode _node;
	}
}
