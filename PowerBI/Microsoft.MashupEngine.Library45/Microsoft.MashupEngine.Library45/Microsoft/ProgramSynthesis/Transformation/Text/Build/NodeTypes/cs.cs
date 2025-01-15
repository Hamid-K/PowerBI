using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C7C RID: 7292
	public struct cs : IProgramNodeBuilder, IEquatable<cs>
	{
		// Token: 0x17002930 RID: 10544
		// (get) Token: 0x0600F710 RID: 63248 RVA: 0x0034A28A File Offset: 0x0034848A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F711 RID: 63249 RVA: 0x0034A292 File Offset: 0x00348492
		private cs(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F712 RID: 63250 RVA: 0x0034A29B File Offset: 0x0034849B
		public static cs CreateUnsafe(ProgramNode node)
		{
			return new cs(node);
		}

		// Token: 0x0600F713 RID: 63251 RVA: 0x0034A2A4 File Offset: 0x003484A4
		public static cs? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.cs)
			{
				return null;
			}
			return new cs?(cs.CreateUnsafe(node));
		}

		// Token: 0x0600F714 RID: 63252 RVA: 0x0034A2DE File Offset: 0x003484DE
		public static cs CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new cs(new Hole(g.Symbol.cs, holeId));
		}

		// Token: 0x0600F715 RID: 63253 RVA: 0x0034A2F6 File Offset: 0x003484F6
		public cs(GrammarBuilders g)
		{
			this = new cs(new VariableNode(g.Symbol.cs));
		}

		// Token: 0x17002931 RID: 10545
		// (get) Token: 0x0600F716 RID: 63254 RVA: 0x0034A30E File Offset: 0x0034850E
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600F717 RID: 63255 RVA: 0x0034A31B File Offset: 0x0034851B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F718 RID: 63256 RVA: 0x0034A330 File Offset: 0x00348530
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F719 RID: 63257 RVA: 0x0034A35A File Offset: 0x0034855A
		public bool Equals(cs other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B6B RID: 23403
		private ProgramNode _node;
	}
}
