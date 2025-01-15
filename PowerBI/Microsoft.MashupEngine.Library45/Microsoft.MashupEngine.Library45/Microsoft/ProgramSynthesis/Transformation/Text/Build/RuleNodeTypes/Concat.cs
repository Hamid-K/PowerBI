using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001BFB RID: 7163
	public struct Concat : IProgramNodeBuilder, IEquatable<Concat>
	{
		// Token: 0x1700281A RID: 10266
		// (get) Token: 0x0600F0D1 RID: 61649 RVA: 0x0033EADE File Offset: 0x0033CCDE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F0D2 RID: 61650 RVA: 0x0033EAE6 File Offset: 0x0033CCE6
		private Concat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F0D3 RID: 61651 RVA: 0x0033EAEF File Offset: 0x0033CCEF
		public static Concat CreateUnsafe(ProgramNode node)
		{
			return new Concat(node);
		}

		// Token: 0x0600F0D4 RID: 61652 RVA: 0x0033EAF8 File Offset: 0x0033CCF8
		public static Concat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Concat)
			{
				return null;
			}
			return new Concat?(Concat.CreateUnsafe(node));
		}

		// Token: 0x0600F0D5 RID: 61653 RVA: 0x0033EB2D File Offset: 0x0033CD2D
		public Concat(GrammarBuilders g, f value0, e value1)
		{
			this._node = g.Rule.Concat.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F0D6 RID: 61654 RVA: 0x0033EB53 File Offset: 0x0033CD53
		public static implicit operator e(Concat arg)
		{
			return e.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700281B RID: 10267
		// (get) Token: 0x0600F0D7 RID: 61655 RVA: 0x0033EB61 File Offset: 0x0033CD61
		public f f
		{
			get
			{
				return f.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700281C RID: 10268
		// (get) Token: 0x0600F0D8 RID: 61656 RVA: 0x0033EB75 File Offset: 0x0033CD75
		public e e
		{
			get
			{
				return e.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F0D9 RID: 61657 RVA: 0x0033EB89 File Offset: 0x0033CD89
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F0DA RID: 61658 RVA: 0x0033EB9C File Offset: 0x0033CD9C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F0DB RID: 61659 RVA: 0x0033EBC6 File Offset: 0x0033CDC6
		public bool Equals(Concat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AEA RID: 23274
		private ProgramNode _node;
	}
}
