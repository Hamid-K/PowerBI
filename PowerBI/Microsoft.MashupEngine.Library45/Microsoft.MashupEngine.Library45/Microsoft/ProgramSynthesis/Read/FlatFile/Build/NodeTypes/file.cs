using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes
{
	// Token: 0x0200128D RID: 4749
	public struct file : IProgramNodeBuilder, IEquatable<file>
	{
		// Token: 0x170018BC RID: 6332
		// (get) Token: 0x06008FB8 RID: 36792 RVA: 0x001E30F6 File Offset: 0x001E12F6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008FB9 RID: 36793 RVA: 0x001E30FE File Offset: 0x001E12FE
		private file(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008FBA RID: 36794 RVA: 0x001E3107 File Offset: 0x001E1307
		public static file CreateUnsafe(ProgramNode node)
		{
			return new file(node);
		}

		// Token: 0x06008FBB RID: 36795 RVA: 0x001E3110 File Offset: 0x001E1310
		public static file? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.file)
			{
				return null;
			}
			return new file?(file.CreateUnsafe(node));
		}

		// Token: 0x06008FBC RID: 36796 RVA: 0x001E314A File Offset: 0x001E134A
		public static file CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new file(new Hole(g.Symbol.file, holeId));
		}

		// Token: 0x06008FBD RID: 36797 RVA: 0x001E3162 File Offset: 0x001E1362
		public file(GrammarBuilders g)
		{
			this = new file(new VariableNode(g.Symbol.file));
		}

		// Token: 0x170018BD RID: 6333
		// (get) Token: 0x06008FBE RID: 36798 RVA: 0x001E317A File Offset: 0x001E137A
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06008FBF RID: 36799 RVA: 0x001E3187 File Offset: 0x001E1387
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008FC0 RID: 36800 RVA: 0x001E319C File Offset: 0x001E139C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008FC1 RID: 36801 RVA: 0x001E31C6 File Offset: 0x001E13C6
		public bool Equals(file other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A7E RID: 14974
		private ProgramNode _node;
	}
}
