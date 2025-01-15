using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C10 RID: 7184
	public struct RoundPartialDateTime : IProgramNodeBuilder, IEquatable<RoundPartialDateTime>
	{
		// Token: 0x17002859 RID: 10329
		// (get) Token: 0x0600F1B8 RID: 61880 RVA: 0x0033FFE2 File Offset: 0x0033E1E2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F1B9 RID: 61881 RVA: 0x0033FFEA File Offset: 0x0033E1EA
		private RoundPartialDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F1BA RID: 61882 RVA: 0x0033FFF3 File Offset: 0x0033E1F3
		public static RoundPartialDateTime CreateUnsafe(ProgramNode node)
		{
			return new RoundPartialDateTime(node);
		}

		// Token: 0x0600F1BB RID: 61883 RVA: 0x0033FFFC File Offset: 0x0033E1FC
		public static RoundPartialDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RoundPartialDateTime)
			{
				return null;
			}
			return new RoundPartialDateTime?(RoundPartialDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600F1BC RID: 61884 RVA: 0x00340031 File Offset: 0x0033E231
		public RoundPartialDateTime(GrammarBuilders g, inputDateTime value0, dtRoundingSpec value1)
		{
			this._node = g.Rule.RoundPartialDateTime.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F1BD RID: 61885 RVA: 0x00340057 File Offset: 0x0033E257
		public static implicit operator datetime(RoundPartialDateTime arg)
		{
			return datetime.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700285A RID: 10330
		// (get) Token: 0x0600F1BE RID: 61886 RVA: 0x00340065 File Offset: 0x0033E265
		public inputDateTime inputDateTime
		{
			get
			{
				return inputDateTime.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700285B RID: 10331
		// (get) Token: 0x0600F1BF RID: 61887 RVA: 0x00340079 File Offset: 0x0033E279
		public dtRoundingSpec dtRoundingSpec
		{
			get
			{
				return dtRoundingSpec.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F1C0 RID: 61888 RVA: 0x0034008D File Offset: 0x0033E28D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F1C1 RID: 61889 RVA: 0x003400A0 File Offset: 0x0033E2A0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F1C2 RID: 61890 RVA: 0x003400CA File Offset: 0x0033E2CA
		public bool Equals(RoundPartialDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AFF RID: 23295
		private ProgramNode _node;
	}
}
