using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E95 RID: 7829
	public struct x : IProgramNodeBuilder, IEquatable<x>
	{
		// Token: 0x17002BF7 RID: 11255
		// (get) Token: 0x060108B0 RID: 67760 RVA: 0x0038DF96 File Offset: 0x0038C196
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060108B1 RID: 67761 RVA: 0x0038DF9E File Offset: 0x0038C19E
		private x(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060108B2 RID: 67762 RVA: 0x0038DFA7 File Offset: 0x0038C1A7
		public static x CreateUnsafe(ProgramNode node)
		{
			return new x(node);
		}

		// Token: 0x060108B3 RID: 67763 RVA: 0x0038DFB0 File Offset: 0x0038C1B0
		public static x? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.x)
			{
				return null;
			}
			return new x?(x.CreateUnsafe(node));
		}

		// Token: 0x060108B4 RID: 67764 RVA: 0x0038DFEA File Offset: 0x0038C1EA
		public static x CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new x(new Hole(g.Symbol.x, holeId));
		}

		// Token: 0x060108B5 RID: 67765 RVA: 0x0038E002 File Offset: 0x0038C202
		public x(GrammarBuilders g)
		{
			this = new x(new VariableNode(g.Symbol.x));
		}

		// Token: 0x17002BF8 RID: 11256
		// (get) Token: 0x060108B6 RID: 67766 RVA: 0x0038E01A File Offset: 0x0038C21A
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x060108B7 RID: 67767 RVA: 0x0038E027 File Offset: 0x0038C227
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060108B8 RID: 67768 RVA: 0x0038E03C File Offset: 0x0038C23C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060108B9 RID: 67769 RVA: 0x0038E066 File Offset: 0x0038C266
		public bool Equals(x other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062D4 RID: 25300
		private ProgramNode _node;
	}
}
