using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015E6 RID: 5606
	public struct dateTimeFormatDesc : IProgramNodeBuilder, IEquatable<dateTimeFormatDesc>
	{
		// Token: 0x17002023 RID: 8227
		// (get) Token: 0x0600BA20 RID: 47648 RVA: 0x00282266 File Offset: 0x00280466
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BA21 RID: 47649 RVA: 0x0028226E File Offset: 0x0028046E
		private dateTimeFormatDesc(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BA22 RID: 47650 RVA: 0x00282277 File Offset: 0x00280477
		public static dateTimeFormatDesc CreateUnsafe(ProgramNode node)
		{
			return new dateTimeFormatDesc(node);
		}

		// Token: 0x0600BA23 RID: 47651 RVA: 0x00282280 File Offset: 0x00280480
		public static dateTimeFormatDesc? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.dateTimeFormatDesc)
			{
				return null;
			}
			return new dateTimeFormatDesc?(dateTimeFormatDesc.CreateUnsafe(node));
		}

		// Token: 0x0600BA24 RID: 47652 RVA: 0x002822BA File Offset: 0x002804BA
		public static dateTimeFormatDesc CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new dateTimeFormatDesc(new Hole(g.Symbol.dateTimeFormatDesc, holeId));
		}

		// Token: 0x0600BA25 RID: 47653 RVA: 0x002822D2 File Offset: 0x002804D2
		public dateTimeFormatDesc(GrammarBuilders g, DateTimeDescriptor value)
		{
			this = new dateTimeFormatDesc(new LiteralNode(g.Symbol.dateTimeFormatDesc, value));
		}

		// Token: 0x17002024 RID: 8228
		// (get) Token: 0x0600BA26 RID: 47654 RVA: 0x002822EB File Offset: 0x002804EB
		public DateTimeDescriptor Value
		{
			get
			{
				return (DateTimeDescriptor)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600BA27 RID: 47655 RVA: 0x00282302 File Offset: 0x00280502
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BA28 RID: 47656 RVA: 0x00282318 File Offset: 0x00280518
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BA29 RID: 47657 RVA: 0x00282342 File Offset: 0x00280542
		public bool Equals(dateTimeFormatDesc other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004694 RID: 18068
		private ProgramNode _node;
	}
}
