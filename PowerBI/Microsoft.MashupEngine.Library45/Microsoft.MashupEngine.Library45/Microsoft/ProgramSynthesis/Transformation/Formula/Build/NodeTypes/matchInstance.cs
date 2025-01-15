using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015ED RID: 5613
	public struct matchInstance : IProgramNodeBuilder, IEquatable<matchInstance>
	{
		// Token: 0x17002031 RID: 8241
		// (get) Token: 0x0600BA66 RID: 47718 RVA: 0x00282902 File Offset: 0x00280B02
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BA67 RID: 47719 RVA: 0x0028290A File Offset: 0x00280B0A
		private matchInstance(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BA68 RID: 47720 RVA: 0x00282913 File Offset: 0x00280B13
		public static matchInstance CreateUnsafe(ProgramNode node)
		{
			return new matchInstance(node);
		}

		// Token: 0x0600BA69 RID: 47721 RVA: 0x0028291C File Offset: 0x00280B1C
		public static matchInstance? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.matchInstance)
			{
				return null;
			}
			return new matchInstance?(matchInstance.CreateUnsafe(node));
		}

		// Token: 0x0600BA6A RID: 47722 RVA: 0x00282956 File Offset: 0x00280B56
		public static matchInstance CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new matchInstance(new Hole(g.Symbol.matchInstance, holeId));
		}

		// Token: 0x0600BA6B RID: 47723 RVA: 0x0028296E File Offset: 0x00280B6E
		public matchInstance(GrammarBuilders g, int value)
		{
			this = new matchInstance(new LiteralNode(g.Symbol.matchInstance, value));
		}

		// Token: 0x17002032 RID: 8242
		// (get) Token: 0x0600BA6C RID: 47724 RVA: 0x0028298C File Offset: 0x00280B8C
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600BA6D RID: 47725 RVA: 0x002829A3 File Offset: 0x00280BA3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BA6E RID: 47726 RVA: 0x002829B8 File Offset: 0x00280BB8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BA6F RID: 47727 RVA: 0x002829E2 File Offset: 0x00280BE2
		public bool Equals(matchInstance other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400469B RID: 18075
		private ProgramNode _node;
	}
}
