using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001579 RID: 5497
	public struct RoundDateTime : IProgramNodeBuilder, IEquatable<RoundDateTime>
	{
		// Token: 0x17001F60 RID: 8032
		// (get) Token: 0x0600B3D9 RID: 46041 RVA: 0x00273F5E File Offset: 0x0027215E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B3DA RID: 46042 RVA: 0x00273F66 File Offset: 0x00272166
		private RoundDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B3DB RID: 46043 RVA: 0x00273F6F File Offset: 0x0027216F
		public static RoundDateTime CreateUnsafe(ProgramNode node)
		{
			return new RoundDateTime(node);
		}

		// Token: 0x0600B3DC RID: 46044 RVA: 0x00273F78 File Offset: 0x00272178
		public static RoundDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RoundDateTime)
			{
				return null;
			}
			return new RoundDateTime?(RoundDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600B3DD RID: 46045 RVA: 0x00273FAD File Offset: 0x002721AD
		public RoundDateTime(GrammarBuilders g, idate value0, dateTimeRoundDesc value1)
		{
			this._node = g.Rule.RoundDateTime.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B3DE RID: 46046 RVA: 0x00273FD3 File Offset: 0x002721D3
		public static implicit operator date(RoundDateTime arg)
		{
			return date.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F61 RID: 8033
		// (get) Token: 0x0600B3DF RID: 46047 RVA: 0x00273FE1 File Offset: 0x002721E1
		public idate idate
		{
			get
			{
				return idate.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F62 RID: 8034
		// (get) Token: 0x0600B3E0 RID: 46048 RVA: 0x00273FF5 File Offset: 0x002721F5
		public dateTimeRoundDesc dateTimeRoundDesc
		{
			get
			{
				return dateTimeRoundDesc.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B3E1 RID: 46049 RVA: 0x00274009 File Offset: 0x00272209
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B3E2 RID: 46050 RVA: 0x0027401C File Offset: 0x0027221C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B3E3 RID: 46051 RVA: 0x00274046 File Offset: 0x00272246
		public bool Equals(RoundDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004627 RID: 17959
		private ProgramNode _node;
	}
}
