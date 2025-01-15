using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015D4 RID: 5588
	public struct containsCount : IProgramNodeBuilder, IEquatable<containsCount>
	{
		// Token: 0x17001FFF RID: 8191
		// (get) Token: 0x0600B96C RID: 47468 RVA: 0x0028116A File Offset: 0x0027F36A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B96D RID: 47469 RVA: 0x00281172 File Offset: 0x0027F372
		private containsCount(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B96E RID: 47470 RVA: 0x0028117B File Offset: 0x0027F37B
		public static containsCount CreateUnsafe(ProgramNode node)
		{
			return new containsCount(node);
		}

		// Token: 0x0600B96F RID: 47471 RVA: 0x00281184 File Offset: 0x0027F384
		public static containsCount? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.containsCount)
			{
				return null;
			}
			return new containsCount?(containsCount.CreateUnsafe(node));
		}

		// Token: 0x0600B970 RID: 47472 RVA: 0x002811BE File Offset: 0x0027F3BE
		public static containsCount CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new containsCount(new Hole(g.Symbol.containsCount, holeId));
		}

		// Token: 0x0600B971 RID: 47473 RVA: 0x002811D6 File Offset: 0x0027F3D6
		public containsCount(GrammarBuilders g, int value)
		{
			this = new containsCount(new LiteralNode(g.Symbol.containsCount, value));
		}

		// Token: 0x17002000 RID: 8192
		// (get) Token: 0x0600B972 RID: 47474 RVA: 0x002811F4 File Offset: 0x0027F3F4
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600B973 RID: 47475 RVA: 0x0028120B File Offset: 0x0027F40B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B974 RID: 47476 RVA: 0x00281220 File Offset: 0x0027F420
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B975 RID: 47477 RVA: 0x0028124A File Offset: 0x0027F44A
		public bool Equals(containsCount other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004682 RID: 18050
		private ProgramNode _node;
	}
}
