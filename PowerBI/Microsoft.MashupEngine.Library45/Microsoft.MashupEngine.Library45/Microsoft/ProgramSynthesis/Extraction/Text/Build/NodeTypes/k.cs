using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F44 RID: 3908
	public struct k : IProgramNodeBuilder, IEquatable<k>
	{
		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x06006CD1 RID: 27857 RVA: 0x00163C2E File Offset: 0x00161E2E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006CD2 RID: 27858 RVA: 0x00163C36 File Offset: 0x00161E36
		private k(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006CD3 RID: 27859 RVA: 0x00163C3F File Offset: 0x00161E3F
		public static k CreateUnsafe(ProgramNode node)
		{
			return new k(node);
		}

		// Token: 0x06006CD4 RID: 27860 RVA: 0x00163C48 File Offset: 0x00161E48
		public static k? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.k)
			{
				return null;
			}
			return new k?(k.CreateUnsafe(node));
		}

		// Token: 0x06006CD5 RID: 27861 RVA: 0x00163C82 File Offset: 0x00161E82
		public static k CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new k(new Hole(g.Symbol.k, holeId));
		}

		// Token: 0x06006CD6 RID: 27862 RVA: 0x00163C9A File Offset: 0x00161E9A
		public k(GrammarBuilders g, int value)
		{
			this = new k(new LiteralNode(g.Symbol.k, value));
		}

		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x06006CD7 RID: 27863 RVA: 0x00163CB8 File Offset: 0x00161EB8
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06006CD8 RID: 27864 RVA: 0x00163CCF File Offset: 0x00161ECF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006CD9 RID: 27865 RVA: 0x00163CE4 File Offset: 0x00161EE4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006CDA RID: 27866 RVA: 0x00163D0E File Offset: 0x00161F0E
		public bool Equals(k other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F2F RID: 12079
		private ProgramNode _node;
	}
}
