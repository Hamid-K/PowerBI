using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015AB RID: 5547
	public struct formatNumber : IProgramNodeBuilder, IEquatable<formatNumber>
	{
		// Token: 0x17001FD1 RID: 8145
		// (get) Token: 0x0600B6CA RID: 46794 RVA: 0x0027AB96 File Offset: 0x00278D96
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B6CB RID: 46795 RVA: 0x0027AB9E File Offset: 0x00278D9E
		private formatNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B6CC RID: 46796 RVA: 0x0027ABA7 File Offset: 0x00278DA7
		public static formatNumber CreateUnsafe(ProgramNode node)
		{
			return new formatNumber(node);
		}

		// Token: 0x0600B6CD RID: 46797 RVA: 0x0027ABB0 File Offset: 0x00278DB0
		public static formatNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.formatNumber)
			{
				return null;
			}
			return new formatNumber?(formatNumber.CreateUnsafe(node));
		}

		// Token: 0x0600B6CE RID: 46798 RVA: 0x0027ABEA File Offset: 0x00278DEA
		public static formatNumber CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new formatNumber(new Hole(g.Symbol.formatNumber, holeId));
		}

		// Token: 0x0600B6CF RID: 46799 RVA: 0x0027AC02 File Offset: 0x00278E02
		public FormatNumber Cast_FormatNumber()
		{
			return FormatNumber.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B6D0 RID: 46800 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_FormatNumber(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B6D1 RID: 46801 RVA: 0x0027AC0F File Offset: 0x00278E0F
		public bool Is_FormatNumber(GrammarBuilders g, out FormatNumber value)
		{
			value = FormatNumber.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B6D2 RID: 46802 RVA: 0x0027AC23 File Offset: 0x00278E23
		public FormatNumber? As_FormatNumber(GrammarBuilders g)
		{
			return new FormatNumber?(FormatNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B6D3 RID: 46803 RVA: 0x0027AC35 File Offset: 0x00278E35
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B6D4 RID: 46804 RVA: 0x0027AC48 File Offset: 0x00278E48
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B6D5 RID: 46805 RVA: 0x0027AC72 File Offset: 0x00278E72
		public bool Equals(formatNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004659 RID: 18009
		private ProgramNode _node;
	}
}
