using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015C4 RID: 5572
	public struct fromNumberStr : IProgramNodeBuilder, IEquatable<fromNumberStr>
	{
		// Token: 0x17001FEA RID: 8170
		// (get) Token: 0x0600B8B6 RID: 47286 RVA: 0x0028026A File Offset: 0x0027E46A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B8B7 RID: 47287 RVA: 0x00280272 File Offset: 0x0027E472
		private fromNumberStr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B8B8 RID: 47288 RVA: 0x0028027B File Offset: 0x0027E47B
		public static fromNumberStr CreateUnsafe(ProgramNode node)
		{
			return new fromNumberStr(node);
		}

		// Token: 0x0600B8B9 RID: 47289 RVA: 0x00280284 File Offset: 0x0027E484
		public static fromNumberStr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fromNumberStr)
			{
				return null;
			}
			return new fromNumberStr?(fromNumberStr.CreateUnsafe(node));
		}

		// Token: 0x0600B8BA RID: 47290 RVA: 0x002802BE File Offset: 0x0027E4BE
		public static fromNumberStr CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fromNumberStr(new Hole(g.Symbol.fromNumberStr, holeId));
		}

		// Token: 0x0600B8BB RID: 47291 RVA: 0x002802D6 File Offset: 0x0027E4D6
		public FromNumberStr Cast_FromNumberStr()
		{
			return FromNumberStr.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B8BC RID: 47292 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_FromNumberStr(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B8BD RID: 47293 RVA: 0x002802E3 File Offset: 0x0027E4E3
		public bool Is_FromNumberStr(GrammarBuilders g, out FromNumberStr value)
		{
			value = FromNumberStr.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B8BE RID: 47294 RVA: 0x002802F7 File Offset: 0x0027E4F7
		public FromNumberStr? As_FromNumberStr(GrammarBuilders g)
		{
			return new FromNumberStr?(FromNumberStr.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B8BF RID: 47295 RVA: 0x00280309 File Offset: 0x0027E509
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B8C0 RID: 47296 RVA: 0x0028031C File Offset: 0x0027E51C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B8C1 RID: 47297 RVA: 0x00280346 File Offset: 0x0027E546
		public bool Equals(fromNumberStr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004672 RID: 18034
		private ProgramNode _node;
	}
}
