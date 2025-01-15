using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C03 RID: 7171
	public struct FormatPartialDateTime : IProgramNodeBuilder, IEquatable<FormatPartialDateTime>
	{
		// Token: 0x1700282E RID: 10286
		// (get) Token: 0x0600F125 RID: 61733 RVA: 0x0033F25E File Offset: 0x0033D45E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F126 RID: 61734 RVA: 0x0033F266 File Offset: 0x0033D466
		private FormatPartialDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F127 RID: 61735 RVA: 0x0033F26F File Offset: 0x0033D46F
		public static FormatPartialDateTime CreateUnsafe(ProgramNode node)
		{
			return new FormatPartialDateTime(node);
		}

		// Token: 0x0600F128 RID: 61736 RVA: 0x0033F278 File Offset: 0x0033D478
		public static FormatPartialDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FormatPartialDateTime)
			{
				return null;
			}
			return new FormatPartialDateTime?(FormatPartialDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600F129 RID: 61737 RVA: 0x0033F2AD File Offset: 0x0033D4AD
		public FormatPartialDateTime(GrammarBuilders g, datetime value0, outputDtFormat value1)
		{
			this._node = g.Rule.FormatPartialDateTime.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F12A RID: 61738 RVA: 0x0033F2D3 File Offset: 0x0033D4D3
		public static implicit operator conv(FormatPartialDateTime arg)
		{
			return conv.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700282F RID: 10287
		// (get) Token: 0x0600F12B RID: 61739 RVA: 0x0033F2E1 File Offset: 0x0033D4E1
		public datetime datetime
		{
			get
			{
				return datetime.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002830 RID: 10288
		// (get) Token: 0x0600F12C RID: 61740 RVA: 0x0033F2F5 File Offset: 0x0033D4F5
		public outputDtFormat outputDtFormat
		{
			get
			{
				return outputDtFormat.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F12D RID: 61741 RVA: 0x0033F309 File Offset: 0x0033D509
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F12E RID: 61742 RVA: 0x0033F31C File Offset: 0x0033D51C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F12F RID: 61743 RVA: 0x0033F346 File Offset: 0x0033D546
		public bool Equals(FormatPartialDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AF2 RID: 23282
		private ProgramNode _node;
	}
}
