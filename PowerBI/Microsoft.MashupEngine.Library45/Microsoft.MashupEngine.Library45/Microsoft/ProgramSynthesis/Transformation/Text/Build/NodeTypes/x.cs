using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C74 RID: 7284
	public struct x : IProgramNodeBuilder, IEquatable<x>
	{
		// Token: 0x17002920 RID: 10528
		// (get) Token: 0x0600F6C0 RID: 63168 RVA: 0x00349B6A File Offset: 0x00347D6A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F6C1 RID: 63169 RVA: 0x00349B72 File Offset: 0x00347D72
		private x(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F6C2 RID: 63170 RVA: 0x00349B7B File Offset: 0x00347D7B
		public static x CreateUnsafe(ProgramNode node)
		{
			return new x(node);
		}

		// Token: 0x0600F6C3 RID: 63171 RVA: 0x00349B84 File Offset: 0x00347D84
		public static x? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.x)
			{
				return null;
			}
			return new x?(x.CreateUnsafe(node));
		}

		// Token: 0x0600F6C4 RID: 63172 RVA: 0x00349BBE File Offset: 0x00347DBE
		public static x CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new x(new Hole(g.Symbol.x, holeId));
		}

		// Token: 0x0600F6C5 RID: 63173 RVA: 0x00349BD6 File Offset: 0x00347DD6
		public x(GrammarBuilders g)
		{
			this = new x(new VariableNode(g.Symbol.x));
		}

		// Token: 0x17002921 RID: 10529
		// (get) Token: 0x0600F6C6 RID: 63174 RVA: 0x00349BEE File Offset: 0x00347DEE
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600F6C7 RID: 63175 RVA: 0x00349BFB File Offset: 0x00347DFB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F6C8 RID: 63176 RVA: 0x00349C10 File Offset: 0x00347E10
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F6C9 RID: 63177 RVA: 0x00349C3A File Offset: 0x00347E3A
		public bool Equals(x other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B63 RID: 23395
		private ProgramNode _node;
	}
}
