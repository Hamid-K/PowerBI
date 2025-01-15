using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015F7 RID: 5623
	public struct x : IProgramNodeBuilder, IEquatable<x>
	{
		// Token: 0x17002045 RID: 8261
		// (get) Token: 0x0600BACA RID: 47818 RVA: 0x00283272 File Offset: 0x00281472
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BACB RID: 47819 RVA: 0x0028327A File Offset: 0x0028147A
		private x(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BACC RID: 47820 RVA: 0x00283283 File Offset: 0x00281483
		public static x CreateUnsafe(ProgramNode node)
		{
			return new x(node);
		}

		// Token: 0x0600BACD RID: 47821 RVA: 0x0028328C File Offset: 0x0028148C
		public static x? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.x)
			{
				return null;
			}
			return new x?(x.CreateUnsafe(node));
		}

		// Token: 0x0600BACE RID: 47822 RVA: 0x002832C6 File Offset: 0x002814C6
		public static x CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new x(new Hole(g.Symbol.x, holeId));
		}

		// Token: 0x0600BACF RID: 47823 RVA: 0x002832DE File Offset: 0x002814DE
		public x(GrammarBuilders g)
		{
			this = new x(new VariableNode(g.Symbol.x));
		}

		// Token: 0x17002046 RID: 8262
		// (get) Token: 0x0600BAD0 RID: 47824 RVA: 0x002832F6 File Offset: 0x002814F6
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600BAD1 RID: 47825 RVA: 0x00283303 File Offset: 0x00281503
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BAD2 RID: 47826 RVA: 0x00283318 File Offset: 0x00281518
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BAD3 RID: 47827 RVA: 0x00283342 File Offset: 0x00281542
		public bool Equals(x other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040046A5 RID: 18085
		private ProgramNode _node;
	}
}
