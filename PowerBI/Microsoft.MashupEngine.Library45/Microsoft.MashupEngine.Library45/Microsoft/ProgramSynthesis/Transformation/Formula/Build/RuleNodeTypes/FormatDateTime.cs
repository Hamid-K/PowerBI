using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001578 RID: 5496
	public struct FormatDateTime : IProgramNodeBuilder, IEquatable<FormatDateTime>
	{
		// Token: 0x17001F5D RID: 8029
		// (get) Token: 0x0600B3CE RID: 46030 RVA: 0x00273E62 File Offset: 0x00272062
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B3CF RID: 46031 RVA: 0x00273E6A File Offset: 0x0027206A
		private FormatDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B3D0 RID: 46032 RVA: 0x00273E73 File Offset: 0x00272073
		public static FormatDateTime CreateUnsafe(ProgramNode node)
		{
			return new FormatDateTime(node);
		}

		// Token: 0x0600B3D1 RID: 46033 RVA: 0x00273E7C File Offset: 0x0027207C
		public static FormatDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FormatDateTime)
			{
				return null;
			}
			return new FormatDateTime?(FormatDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600B3D2 RID: 46034 RVA: 0x00273EB1 File Offset: 0x002720B1
		public FormatDateTime(GrammarBuilders g, date value0, dateTimeFormatDesc value1)
		{
			this._node = g.Rule.FormatDateTime.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B3D3 RID: 46035 RVA: 0x00273ED7 File Offset: 0x002720D7
		public static implicit operator formatDateTime(FormatDateTime arg)
		{
			return formatDateTime.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F5E RID: 8030
		// (get) Token: 0x0600B3D4 RID: 46036 RVA: 0x00273EE5 File Offset: 0x002720E5
		public date date
		{
			get
			{
				return date.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F5F RID: 8031
		// (get) Token: 0x0600B3D5 RID: 46037 RVA: 0x00273EF9 File Offset: 0x002720F9
		public dateTimeFormatDesc dateTimeFormatDesc
		{
			get
			{
				return dateTimeFormatDesc.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B3D6 RID: 46038 RVA: 0x00273F0D File Offset: 0x0027210D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B3D7 RID: 46039 RVA: 0x00273F20 File Offset: 0x00272120
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B3D8 RID: 46040 RVA: 0x00273F4A File Offset: 0x0027214A
		public bool Equals(FormatDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004626 RID: 17958
		private ProgramNode _node;
	}
}
