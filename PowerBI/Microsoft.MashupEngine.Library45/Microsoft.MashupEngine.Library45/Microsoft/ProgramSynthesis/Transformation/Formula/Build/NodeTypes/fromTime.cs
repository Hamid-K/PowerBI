using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015CB RID: 5579
	public struct fromTime : IProgramNodeBuilder, IEquatable<fromTime>
	{
		// Token: 0x17001FF1 RID: 8177
		// (get) Token: 0x0600B90A RID: 47370 RVA: 0x002808FA File Offset: 0x0027EAFA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B90B RID: 47371 RVA: 0x00280902 File Offset: 0x0027EB02
		private fromTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B90C RID: 47372 RVA: 0x0028090B File Offset: 0x0027EB0B
		public static fromTime CreateUnsafe(ProgramNode node)
		{
			return new fromTime(node);
		}

		// Token: 0x0600B90D RID: 47373 RVA: 0x00280914 File Offset: 0x0027EB14
		public static fromTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fromTime)
			{
				return null;
			}
			return new fromTime?(fromTime.CreateUnsafe(node));
		}

		// Token: 0x0600B90E RID: 47374 RVA: 0x0028094E File Offset: 0x0027EB4E
		public static fromTime CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fromTime(new Hole(g.Symbol.fromTime, holeId));
		}

		// Token: 0x0600B90F RID: 47375 RVA: 0x00280966 File Offset: 0x0027EB66
		public FromTime Cast_FromTime()
		{
			return FromTime.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B910 RID: 47376 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_FromTime(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B911 RID: 47377 RVA: 0x00280973 File Offset: 0x0027EB73
		public bool Is_FromTime(GrammarBuilders g, out FromTime value)
		{
			value = FromTime.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B912 RID: 47378 RVA: 0x00280987 File Offset: 0x0027EB87
		public FromTime? As_FromTime(GrammarBuilders g)
		{
			return new FromTime?(FromTime.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B913 RID: 47379 RVA: 0x00280999 File Offset: 0x0027EB99
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B914 RID: 47380 RVA: 0x002809AC File Offset: 0x0027EBAC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B915 RID: 47381 RVA: 0x002809D6 File Offset: 0x0027EBD6
		public bool Equals(fromTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004679 RID: 18041
		private ProgramNode _node;
	}
}
