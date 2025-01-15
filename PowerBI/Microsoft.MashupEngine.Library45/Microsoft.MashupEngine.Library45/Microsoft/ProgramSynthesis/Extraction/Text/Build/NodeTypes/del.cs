using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F46 RID: 3910
	public struct del : IProgramNodeBuilder, IEquatable<del>
	{
		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x06006CE5 RID: 27877 RVA: 0x00163E12 File Offset: 0x00162012
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006CE6 RID: 27878 RVA: 0x00163E1A File Offset: 0x0016201A
		private del(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006CE7 RID: 27879 RVA: 0x00163E23 File Offset: 0x00162023
		public static del CreateUnsafe(ProgramNode node)
		{
			return new del(node);
		}

		// Token: 0x06006CE8 RID: 27880 RVA: 0x00163E2C File Offset: 0x0016202C
		public static del? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.del)
			{
				return null;
			}
			return new del?(del.CreateUnsafe(node));
		}

		// Token: 0x06006CE9 RID: 27881 RVA: 0x00163E66 File Offset: 0x00162066
		public static del CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new del(new Hole(g.Symbol.del, holeId));
		}

		// Token: 0x06006CEA RID: 27882 RVA: 0x00163E7E File Offset: 0x0016207E
		public del(GrammarBuilders g, Optional<string> value)
		{
			this = new del(new LiteralNode(g.Symbol.del, value));
		}

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x06006CEB RID: 27883 RVA: 0x00163E9C File Offset: 0x0016209C
		public Optional<string> Value
		{
			get
			{
				return (Optional<string>)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06006CEC RID: 27884 RVA: 0x00163EB3 File Offset: 0x001620B3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006CED RID: 27885 RVA: 0x00163EC8 File Offset: 0x001620C8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006CEE RID: 27886 RVA: 0x00163EF2 File Offset: 0x001620F2
		public bool Equals(del other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F31 RID: 12081
		private ProgramNode _node;
	}
}
