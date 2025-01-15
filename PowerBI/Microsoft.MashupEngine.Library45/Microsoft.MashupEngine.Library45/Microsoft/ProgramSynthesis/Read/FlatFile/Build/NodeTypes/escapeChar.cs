using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes
{
	// Token: 0x0200128B RID: 4747
	public struct escapeChar : IProgramNodeBuilder, IEquatable<escapeChar>
	{
		// Token: 0x170018B8 RID: 6328
		// (get) Token: 0x06008FA4 RID: 36772 RVA: 0x001E2F0E File Offset: 0x001E110E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008FA5 RID: 36773 RVA: 0x001E2F16 File Offset: 0x001E1116
		private escapeChar(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008FA6 RID: 36774 RVA: 0x001E2F1F File Offset: 0x001E111F
		public static escapeChar CreateUnsafe(ProgramNode node)
		{
			return new escapeChar(node);
		}

		// Token: 0x06008FA7 RID: 36775 RVA: 0x001E2F28 File Offset: 0x001E1128
		public static escapeChar? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.escapeChar)
			{
				return null;
			}
			return new escapeChar?(escapeChar.CreateUnsafe(node));
		}

		// Token: 0x06008FA8 RID: 36776 RVA: 0x001E2F62 File Offset: 0x001E1162
		public static escapeChar CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new escapeChar(new Hole(g.Symbol.escapeChar, holeId));
		}

		// Token: 0x06008FA9 RID: 36777 RVA: 0x001E2F7A File Offset: 0x001E117A
		public escapeChar(GrammarBuilders g, Optional<char> value)
		{
			this = new escapeChar(new LiteralNode(g.Symbol.escapeChar, value));
		}

		// Token: 0x170018B9 RID: 6329
		// (get) Token: 0x06008FAA RID: 36778 RVA: 0x001E2F98 File Offset: 0x001E1198
		public Optional<char> Value
		{
			get
			{
				return (Optional<char>)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06008FAB RID: 36779 RVA: 0x001E2FAF File Offset: 0x001E11AF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008FAC RID: 36780 RVA: 0x001E2FC4 File Offset: 0x001E11C4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008FAD RID: 36781 RVA: 0x001E2FEE File Offset: 0x001E11EE
		public bool Equals(escapeChar other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A7C RID: 14972
		private ProgramNode _node;
	}
}
