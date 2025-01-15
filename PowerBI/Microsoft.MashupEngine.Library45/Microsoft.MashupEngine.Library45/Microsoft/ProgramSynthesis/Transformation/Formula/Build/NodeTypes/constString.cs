using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015CC RID: 5580
	public struct constString : IProgramNodeBuilder, IEquatable<constString>
	{
		// Token: 0x17001FF2 RID: 8178
		// (get) Token: 0x0600B916 RID: 47382 RVA: 0x002809EA File Offset: 0x0027EBEA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B917 RID: 47383 RVA: 0x002809F2 File Offset: 0x0027EBF2
		private constString(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B918 RID: 47384 RVA: 0x002809FB File Offset: 0x0027EBFB
		public static constString CreateUnsafe(ProgramNode node)
		{
			return new constString(node);
		}

		// Token: 0x0600B919 RID: 47385 RVA: 0x00280A04 File Offset: 0x0027EC04
		public static constString? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.constString)
			{
				return null;
			}
			return new constString?(constString.CreateUnsafe(node));
		}

		// Token: 0x0600B91A RID: 47386 RVA: 0x00280A3E File Offset: 0x0027EC3E
		public static constString CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new constString(new Hole(g.Symbol.constString, holeId));
		}

		// Token: 0x0600B91B RID: 47387 RVA: 0x00280A56 File Offset: 0x0027EC56
		public Str Cast_Str()
		{
			return Str.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B91C RID: 47388 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Str(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B91D RID: 47389 RVA: 0x00280A63 File Offset: 0x0027EC63
		public bool Is_Str(GrammarBuilders g, out Str value)
		{
			value = Str.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B91E RID: 47390 RVA: 0x00280A77 File Offset: 0x0027EC77
		public Str? As_Str(GrammarBuilders g)
		{
			return new Str?(Str.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B91F RID: 47391 RVA: 0x00280A89 File Offset: 0x0027EC89
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B920 RID: 47392 RVA: 0x00280A9C File Offset: 0x0027EC9C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B921 RID: 47393 RVA: 0x00280AC6 File Offset: 0x0027ECC6
		public bool Equals(constString other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400467A RID: 18042
		private ProgramNode _node;
	}
}
