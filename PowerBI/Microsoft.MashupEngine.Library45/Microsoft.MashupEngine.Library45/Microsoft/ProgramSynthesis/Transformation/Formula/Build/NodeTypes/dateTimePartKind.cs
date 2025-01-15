using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015E8 RID: 5608
	public struct dateTimePartKind : IProgramNodeBuilder, IEquatable<dateTimePartKind>
	{
		// Token: 0x17002027 RID: 8231
		// (get) Token: 0x0600BA34 RID: 47668 RVA: 0x00282446 File Offset: 0x00280646
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BA35 RID: 47669 RVA: 0x0028244E File Offset: 0x0028064E
		private dateTimePartKind(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BA36 RID: 47670 RVA: 0x00282457 File Offset: 0x00280657
		public static dateTimePartKind CreateUnsafe(ProgramNode node)
		{
			return new dateTimePartKind(node);
		}

		// Token: 0x0600BA37 RID: 47671 RVA: 0x00282460 File Offset: 0x00280660
		public static dateTimePartKind? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.dateTimePartKind)
			{
				return null;
			}
			return new dateTimePartKind?(dateTimePartKind.CreateUnsafe(node));
		}

		// Token: 0x0600BA38 RID: 47672 RVA: 0x0028249A File Offset: 0x0028069A
		public static dateTimePartKind CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new dateTimePartKind(new Hole(g.Symbol.dateTimePartKind, holeId));
		}

		// Token: 0x0600BA39 RID: 47673 RVA: 0x002824B2 File Offset: 0x002806B2
		public dateTimePartKind(GrammarBuilders g, DateTimePartKind value)
		{
			this = new dateTimePartKind(new LiteralNode(g.Symbol.dateTimePartKind, value));
		}

		// Token: 0x17002028 RID: 8232
		// (get) Token: 0x0600BA3A RID: 47674 RVA: 0x002824D0 File Offset: 0x002806D0
		public DateTimePartKind Value
		{
			get
			{
				return (DateTimePartKind)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600BA3B RID: 47675 RVA: 0x002824E7 File Offset: 0x002806E7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BA3C RID: 47676 RVA: 0x002824FC File Offset: 0x002806FC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BA3D RID: 47677 RVA: 0x00282526 File Offset: 0x00280726
		public bool Equals(dateTimePartKind other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004696 RID: 18070
		private ProgramNode _node;
	}
}
