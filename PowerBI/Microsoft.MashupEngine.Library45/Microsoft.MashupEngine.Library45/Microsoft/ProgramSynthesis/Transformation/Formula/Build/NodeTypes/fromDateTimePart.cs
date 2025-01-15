using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015CA RID: 5578
	public struct fromDateTimePart : IProgramNodeBuilder, IEquatable<fromDateTimePart>
	{
		// Token: 0x17001FF0 RID: 8176
		// (get) Token: 0x0600B8FE RID: 47358 RVA: 0x0028080A File Offset: 0x0027EA0A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B8FF RID: 47359 RVA: 0x00280812 File Offset: 0x0027EA12
		private fromDateTimePart(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B900 RID: 47360 RVA: 0x0028081B File Offset: 0x0027EA1B
		public static fromDateTimePart CreateUnsafe(ProgramNode node)
		{
			return new fromDateTimePart(node);
		}

		// Token: 0x0600B901 RID: 47361 RVA: 0x00280824 File Offset: 0x0027EA24
		public static fromDateTimePart? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fromDateTimePart)
			{
				return null;
			}
			return new fromDateTimePart?(fromDateTimePart.CreateUnsafe(node));
		}

		// Token: 0x0600B902 RID: 47362 RVA: 0x0028085E File Offset: 0x0027EA5E
		public static fromDateTimePart CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fromDateTimePart(new Hole(g.Symbol.fromDateTimePart, holeId));
		}

		// Token: 0x0600B903 RID: 47363 RVA: 0x00280876 File Offset: 0x0027EA76
		public FromDateTimePart Cast_FromDateTimePart()
		{
			return FromDateTimePart.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B904 RID: 47364 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_FromDateTimePart(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B905 RID: 47365 RVA: 0x00280883 File Offset: 0x0027EA83
		public bool Is_FromDateTimePart(GrammarBuilders g, out FromDateTimePart value)
		{
			value = FromDateTimePart.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B906 RID: 47366 RVA: 0x00280897 File Offset: 0x0027EA97
		public FromDateTimePart? As_FromDateTimePart(GrammarBuilders g)
		{
			return new FromDateTimePart?(FromDateTimePart.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B907 RID: 47367 RVA: 0x002808A9 File Offset: 0x0027EAA9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B908 RID: 47368 RVA: 0x002808BC File Offset: 0x0027EABC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B909 RID: 47369 RVA: 0x002808E6 File Offset: 0x0027EAE6
		public bool Equals(fromDateTimePart other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004678 RID: 18040
		private ProgramNode _node;
	}
}
