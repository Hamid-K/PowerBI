using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015E7 RID: 5607
	public struct dateTimeParseDesc : IProgramNodeBuilder, IEquatable<dateTimeParseDesc>
	{
		// Token: 0x17002025 RID: 8229
		// (get) Token: 0x0600BA2A RID: 47658 RVA: 0x00282356 File Offset: 0x00280556
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BA2B RID: 47659 RVA: 0x0028235E File Offset: 0x0028055E
		private dateTimeParseDesc(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BA2C RID: 47660 RVA: 0x00282367 File Offset: 0x00280567
		public static dateTimeParseDesc CreateUnsafe(ProgramNode node)
		{
			return new dateTimeParseDesc(node);
		}

		// Token: 0x0600BA2D RID: 47661 RVA: 0x00282370 File Offset: 0x00280570
		public static dateTimeParseDesc? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.dateTimeParseDesc)
			{
				return null;
			}
			return new dateTimeParseDesc?(dateTimeParseDesc.CreateUnsafe(node));
		}

		// Token: 0x0600BA2E RID: 47662 RVA: 0x002823AA File Offset: 0x002805AA
		public static dateTimeParseDesc CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new dateTimeParseDesc(new Hole(g.Symbol.dateTimeParseDesc, holeId));
		}

		// Token: 0x0600BA2F RID: 47663 RVA: 0x002823C2 File Offset: 0x002805C2
		public dateTimeParseDesc(GrammarBuilders g, DateTimeDescriptor value)
		{
			this = new dateTimeParseDesc(new LiteralNode(g.Symbol.dateTimeParseDesc, value));
		}

		// Token: 0x17002026 RID: 8230
		// (get) Token: 0x0600BA30 RID: 47664 RVA: 0x002823DB File Offset: 0x002805DB
		public DateTimeDescriptor Value
		{
			get
			{
				return (DateTimeDescriptor)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600BA31 RID: 47665 RVA: 0x002823F2 File Offset: 0x002805F2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BA32 RID: 47666 RVA: 0x00282408 File Offset: 0x00280608
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BA33 RID: 47667 RVA: 0x00282432 File Offset: 0x00280632
		public bool Equals(dateTimeParseDesc other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004695 RID: 18069
		private ProgramNode _node;
	}
}
