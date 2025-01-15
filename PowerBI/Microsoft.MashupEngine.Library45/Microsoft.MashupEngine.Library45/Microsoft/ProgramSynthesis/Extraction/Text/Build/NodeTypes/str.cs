using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F45 RID: 3909
	public struct str : IProgramNodeBuilder, IEquatable<str>
	{
		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x06006CDB RID: 27867 RVA: 0x00163D22 File Offset: 0x00161F22
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006CDC RID: 27868 RVA: 0x00163D2A File Offset: 0x00161F2A
		private str(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006CDD RID: 27869 RVA: 0x00163D33 File Offset: 0x00161F33
		public static str CreateUnsafe(ProgramNode node)
		{
			return new str(node);
		}

		// Token: 0x06006CDE RID: 27870 RVA: 0x00163D3C File Offset: 0x00161F3C
		public static str? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.str)
			{
				return null;
			}
			return new str?(str.CreateUnsafe(node));
		}

		// Token: 0x06006CDF RID: 27871 RVA: 0x00163D76 File Offset: 0x00161F76
		public static str CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new str(new Hole(g.Symbol.str, holeId));
		}

		// Token: 0x06006CE0 RID: 27872 RVA: 0x00163D8E File Offset: 0x00161F8E
		public str(GrammarBuilders g, string value)
		{
			this = new str(new LiteralNode(g.Symbol.str, value));
		}

		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x06006CE1 RID: 27873 RVA: 0x00163DA7 File Offset: 0x00161FA7
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06006CE2 RID: 27874 RVA: 0x00163DBE File Offset: 0x00161FBE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006CE3 RID: 27875 RVA: 0x00163DD4 File Offset: 0x00161FD4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006CE4 RID: 27876 RVA: 0x00163DFE File Offset: 0x00161FFE
		public bool Equals(str other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F30 RID: 12080
		private ProgramNode _node;
	}
}
