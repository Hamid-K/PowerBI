using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes
{
	// Token: 0x02000B70 RID: 2928
	public struct x : IProgramNodeBuilder, IEquatable<x>
	{
		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06004A55 RID: 19029 RVA: 0x000E9942 File Offset: 0x000E7B42
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004A56 RID: 19030 RVA: 0x000E994A File Offset: 0x000E7B4A
		private x(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004A57 RID: 19031 RVA: 0x000E9953 File Offset: 0x000E7B53
		public static x CreateUnsafe(ProgramNode node)
		{
			return new x(node);
		}

		// Token: 0x06004A58 RID: 19032 RVA: 0x000E995C File Offset: 0x000E7B5C
		public static x? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.x)
			{
				return null;
			}
			return new x?(x.CreateUnsafe(node));
		}

		// Token: 0x06004A59 RID: 19033 RVA: 0x000E9996 File Offset: 0x000E7B96
		public static x CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new x(new Hole(g.Symbol.x, holeId));
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x000E99AE File Offset: 0x000E7BAE
		public x(GrammarBuilders g)
		{
			this = new x(new VariableNode(g.Symbol.x));
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06004A5B RID: 19035 RVA: 0x000E99C6 File Offset: 0x000E7BC6
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06004A5C RID: 19036 RVA: 0x000E99D3 File Offset: 0x000E7BD3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004A5D RID: 19037 RVA: 0x000E99E8 File Offset: 0x000E7BE8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004A5E RID: 19038 RVA: 0x000E9A12 File Offset: 0x000E7C12
		public bool Equals(x other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400216B RID: 8555
		private ProgramNode _node;
	}
}
