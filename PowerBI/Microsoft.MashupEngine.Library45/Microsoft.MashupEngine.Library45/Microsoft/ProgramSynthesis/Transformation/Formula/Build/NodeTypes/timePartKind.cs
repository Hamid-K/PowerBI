using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015EA RID: 5610
	public struct timePartKind : IProgramNodeBuilder, IEquatable<timePartKind>
	{
		// Token: 0x1700202B RID: 8235
		// (get) Token: 0x0600BA48 RID: 47688 RVA: 0x0028262E File Offset: 0x0028082E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BA49 RID: 47689 RVA: 0x00282636 File Offset: 0x00280836
		private timePartKind(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BA4A RID: 47690 RVA: 0x0028263F File Offset: 0x0028083F
		public static timePartKind CreateUnsafe(ProgramNode node)
		{
			return new timePartKind(node);
		}

		// Token: 0x0600BA4B RID: 47691 RVA: 0x00282648 File Offset: 0x00280848
		public static timePartKind? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.timePartKind)
			{
				return null;
			}
			return new timePartKind?(timePartKind.CreateUnsafe(node));
		}

		// Token: 0x0600BA4C RID: 47692 RVA: 0x00282682 File Offset: 0x00280882
		public static timePartKind CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new timePartKind(new Hole(g.Symbol.timePartKind, holeId));
		}

		// Token: 0x0600BA4D RID: 47693 RVA: 0x0028269A File Offset: 0x0028089A
		public timePartKind(GrammarBuilders g, TimePartKind value)
		{
			this = new timePartKind(new LiteralNode(g.Symbol.timePartKind, value));
		}

		// Token: 0x1700202C RID: 8236
		// (get) Token: 0x0600BA4E RID: 47694 RVA: 0x002826B8 File Offset: 0x002808B8
		public TimePartKind Value
		{
			get
			{
				return (TimePartKind)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600BA4F RID: 47695 RVA: 0x002826CF File Offset: 0x002808CF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BA50 RID: 47696 RVA: 0x002826E4 File Offset: 0x002808E4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BA51 RID: 47697 RVA: 0x0028270E File Offset: 0x0028090E
		public bool Equals(timePartKind other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004698 RID: 18072
		private ProgramNode _node;
	}
}
