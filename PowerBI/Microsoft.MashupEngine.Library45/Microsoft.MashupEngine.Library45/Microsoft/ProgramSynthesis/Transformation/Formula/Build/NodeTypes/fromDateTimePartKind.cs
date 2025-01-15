using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015E9 RID: 5609
	public struct fromDateTimePartKind : IProgramNodeBuilder, IEquatable<fromDateTimePartKind>
	{
		// Token: 0x17002029 RID: 8233
		// (get) Token: 0x0600BA3E RID: 47678 RVA: 0x0028253A File Offset: 0x0028073A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BA3F RID: 47679 RVA: 0x00282542 File Offset: 0x00280742
		private fromDateTimePartKind(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BA40 RID: 47680 RVA: 0x0028254B File Offset: 0x0028074B
		public static fromDateTimePartKind CreateUnsafe(ProgramNode node)
		{
			return new fromDateTimePartKind(node);
		}

		// Token: 0x0600BA41 RID: 47681 RVA: 0x00282554 File Offset: 0x00280754
		public static fromDateTimePartKind? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fromDateTimePartKind)
			{
				return null;
			}
			return new fromDateTimePartKind?(fromDateTimePartKind.CreateUnsafe(node));
		}

		// Token: 0x0600BA42 RID: 47682 RVA: 0x0028258E File Offset: 0x0028078E
		public static fromDateTimePartKind CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fromDateTimePartKind(new Hole(g.Symbol.fromDateTimePartKind, holeId));
		}

		// Token: 0x0600BA43 RID: 47683 RVA: 0x002825A6 File Offset: 0x002807A6
		public fromDateTimePartKind(GrammarBuilders g, DateTimePartKind value)
		{
			this = new fromDateTimePartKind(new LiteralNode(g.Symbol.fromDateTimePartKind, value));
		}

		// Token: 0x1700202A RID: 8234
		// (get) Token: 0x0600BA44 RID: 47684 RVA: 0x002825C4 File Offset: 0x002807C4
		public DateTimePartKind Value
		{
			get
			{
				return (DateTimePartKind)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600BA45 RID: 47685 RVA: 0x002825DB File Offset: 0x002807DB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BA46 RID: 47686 RVA: 0x002825F0 File Offset: 0x002807F0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BA47 RID: 47687 RVA: 0x0028261A File Offset: 0x0028081A
		public bool Equals(fromDateTimePartKind other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004697 RID: 18071
		private ProgramNode _node;
	}
}
