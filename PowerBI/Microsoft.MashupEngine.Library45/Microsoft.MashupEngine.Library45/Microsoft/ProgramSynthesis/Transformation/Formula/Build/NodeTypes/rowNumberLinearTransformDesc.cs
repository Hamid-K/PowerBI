using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015EB RID: 5611
	public struct rowNumberLinearTransformDesc : IProgramNodeBuilder, IEquatable<rowNumberLinearTransformDesc>
	{
		// Token: 0x1700202D RID: 8237
		// (get) Token: 0x0600BA52 RID: 47698 RVA: 0x00282722 File Offset: 0x00280922
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BA53 RID: 47699 RVA: 0x0028272A File Offset: 0x0028092A
		private rowNumberLinearTransformDesc(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BA54 RID: 47700 RVA: 0x00282733 File Offset: 0x00280933
		public static rowNumberLinearTransformDesc CreateUnsafe(ProgramNode node)
		{
			return new rowNumberLinearTransformDesc(node);
		}

		// Token: 0x0600BA55 RID: 47701 RVA: 0x0028273C File Offset: 0x0028093C
		public static rowNumberLinearTransformDesc? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.rowNumberLinearTransformDesc)
			{
				return null;
			}
			return new rowNumberLinearTransformDesc?(rowNumberLinearTransformDesc.CreateUnsafe(node));
		}

		// Token: 0x0600BA56 RID: 47702 RVA: 0x00282776 File Offset: 0x00280976
		public static rowNumberLinearTransformDesc CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new rowNumberLinearTransformDesc(new Hole(g.Symbol.rowNumberLinearTransformDesc, holeId));
		}

		// Token: 0x0600BA57 RID: 47703 RVA: 0x0028278E File Offset: 0x0028098E
		public rowNumberLinearTransformDesc(GrammarBuilders g, RowNumberLinearTransformDescriptor value)
		{
			this = new rowNumberLinearTransformDesc(new LiteralNode(g.Symbol.rowNumberLinearTransformDesc, value));
		}

		// Token: 0x1700202E RID: 8238
		// (get) Token: 0x0600BA58 RID: 47704 RVA: 0x002827A7 File Offset: 0x002809A7
		public RowNumberLinearTransformDescriptor Value
		{
			get
			{
				return (RowNumberLinearTransformDescriptor)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600BA59 RID: 47705 RVA: 0x002827BE File Offset: 0x002809BE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BA5A RID: 47706 RVA: 0x002827D4 File Offset: 0x002809D4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BA5B RID: 47707 RVA: 0x002827FE File Offset: 0x002809FE
		public bool Equals(rowNumberLinearTransformDesc other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004699 RID: 18073
		private ProgramNode _node;
	}
}
