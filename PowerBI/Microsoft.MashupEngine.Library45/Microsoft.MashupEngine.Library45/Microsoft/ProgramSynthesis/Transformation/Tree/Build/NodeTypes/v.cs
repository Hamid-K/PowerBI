using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E94 RID: 7828
	public struct v : IProgramNodeBuilder, IEquatable<v>
	{
		// Token: 0x17002BF5 RID: 11253
		// (get) Token: 0x060108A6 RID: 67750 RVA: 0x0038DEB2 File Offset: 0x0038C0B2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060108A7 RID: 67751 RVA: 0x0038DEBA File Offset: 0x0038C0BA
		private v(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060108A8 RID: 67752 RVA: 0x0038DEC3 File Offset: 0x0038C0C3
		public static v CreateUnsafe(ProgramNode node)
		{
			return new v(node);
		}

		// Token: 0x060108A9 RID: 67753 RVA: 0x0038DECC File Offset: 0x0038C0CC
		public static v? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.v)
			{
				return null;
			}
			return new v?(v.CreateUnsafe(node));
		}

		// Token: 0x060108AA RID: 67754 RVA: 0x0038DF06 File Offset: 0x0038C106
		public static v CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new v(new Hole(g.Symbol.v, holeId));
		}

		// Token: 0x060108AB RID: 67755 RVA: 0x0038DF1E File Offset: 0x0038C11E
		public v(GrammarBuilders g)
		{
			this = new v(new VariableNode(g.Symbol.v));
		}

		// Token: 0x17002BF6 RID: 11254
		// (get) Token: 0x060108AC RID: 67756 RVA: 0x0038DF36 File Offset: 0x0038C136
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x060108AD RID: 67757 RVA: 0x0038DF43 File Offset: 0x0038C143
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060108AE RID: 67758 RVA: 0x0038DF58 File Offset: 0x0038C158
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060108AF RID: 67759 RVA: 0x0038DF82 File Offset: 0x0038C182
		public bool Equals(v other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062D3 RID: 25299
		private ProgramNode _node;
	}
}
