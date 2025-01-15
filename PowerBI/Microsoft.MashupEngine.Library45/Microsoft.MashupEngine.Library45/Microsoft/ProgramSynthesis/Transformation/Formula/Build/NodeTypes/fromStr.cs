using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015C3 RID: 5571
	public struct fromStr : IProgramNodeBuilder, IEquatable<fromStr>
	{
		// Token: 0x17001FE9 RID: 8169
		// (get) Token: 0x0600B8AA RID: 47274 RVA: 0x0028017A File Offset: 0x0027E37A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B8AB RID: 47275 RVA: 0x00280182 File Offset: 0x0027E382
		private fromStr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B8AC RID: 47276 RVA: 0x0028018B File Offset: 0x0027E38B
		public static fromStr CreateUnsafe(ProgramNode node)
		{
			return new fromStr(node);
		}

		// Token: 0x0600B8AD RID: 47277 RVA: 0x00280194 File Offset: 0x0027E394
		public static fromStr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fromStr)
			{
				return null;
			}
			return new fromStr?(fromStr.CreateUnsafe(node));
		}

		// Token: 0x0600B8AE RID: 47278 RVA: 0x002801CE File Offset: 0x0027E3CE
		public static fromStr CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fromStr(new Hole(g.Symbol.fromStr, holeId));
		}

		// Token: 0x0600B8AF RID: 47279 RVA: 0x002801E6 File Offset: 0x0027E3E6
		public FromStr Cast_FromStr()
		{
			return FromStr.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B8B0 RID: 47280 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_FromStr(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B8B1 RID: 47281 RVA: 0x002801F3 File Offset: 0x0027E3F3
		public bool Is_FromStr(GrammarBuilders g, out FromStr value)
		{
			value = FromStr.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B8B2 RID: 47282 RVA: 0x00280207 File Offset: 0x0027E407
		public FromStr? As_FromStr(GrammarBuilders g)
		{
			return new FromStr?(FromStr.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B8B3 RID: 47283 RVA: 0x00280219 File Offset: 0x0027E419
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B8B4 RID: 47284 RVA: 0x0028022C File Offset: 0x0027E42C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B8B5 RID: 47285 RVA: 0x00280256 File Offset: 0x0027E456
		public bool Equals(fromStr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004671 RID: 18033
		private ProgramNode _node;
	}
}
