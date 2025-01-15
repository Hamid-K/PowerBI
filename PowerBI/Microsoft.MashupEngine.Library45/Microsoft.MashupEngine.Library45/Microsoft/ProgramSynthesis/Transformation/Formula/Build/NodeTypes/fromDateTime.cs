using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015C9 RID: 5577
	public struct fromDateTime : IProgramNodeBuilder, IEquatable<fromDateTime>
	{
		// Token: 0x17001FEF RID: 8175
		// (get) Token: 0x0600B8F2 RID: 47346 RVA: 0x0028071A File Offset: 0x0027E91A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B8F3 RID: 47347 RVA: 0x00280722 File Offset: 0x0027E922
		private fromDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B8F4 RID: 47348 RVA: 0x0028072B File Offset: 0x0027E92B
		public static fromDateTime CreateUnsafe(ProgramNode node)
		{
			return new fromDateTime(node);
		}

		// Token: 0x0600B8F5 RID: 47349 RVA: 0x00280734 File Offset: 0x0027E934
		public static fromDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fromDateTime)
			{
				return null;
			}
			return new fromDateTime?(fromDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600B8F6 RID: 47350 RVA: 0x0028076E File Offset: 0x0027E96E
		public static fromDateTime CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fromDateTime(new Hole(g.Symbol.fromDateTime, holeId));
		}

		// Token: 0x0600B8F7 RID: 47351 RVA: 0x00280786 File Offset: 0x0027E986
		public FromDateTime Cast_FromDateTime()
		{
			return FromDateTime.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B8F8 RID: 47352 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_FromDateTime(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B8F9 RID: 47353 RVA: 0x00280793 File Offset: 0x0027E993
		public bool Is_FromDateTime(GrammarBuilders g, out FromDateTime value)
		{
			value = FromDateTime.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B8FA RID: 47354 RVA: 0x002807A7 File Offset: 0x0027E9A7
		public FromDateTime? As_FromDateTime(GrammarBuilders g)
		{
			return new FromDateTime?(FromDateTime.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B8FB RID: 47355 RVA: 0x002807B9 File Offset: 0x0027E9B9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B8FC RID: 47356 RVA: 0x002807CC File Offset: 0x0027E9CC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B8FD RID: 47357 RVA: 0x002807F6 File Offset: 0x0027E9F6
		public bool Equals(fromDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004677 RID: 18039
		private ProgramNode _node;
	}
}
