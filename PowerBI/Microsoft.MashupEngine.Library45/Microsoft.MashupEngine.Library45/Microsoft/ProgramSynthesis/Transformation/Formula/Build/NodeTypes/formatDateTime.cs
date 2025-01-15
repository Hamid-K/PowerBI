using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015B6 RID: 5558
	public struct formatDateTime : IProgramNodeBuilder, IEquatable<formatDateTime>
	{
		// Token: 0x17001FDC RID: 8156
		// (get) Token: 0x0600B7AE RID: 47022 RVA: 0x0027D652 File Offset: 0x0027B852
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B7AF RID: 47023 RVA: 0x0027D65A File Offset: 0x0027B85A
		private formatDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B7B0 RID: 47024 RVA: 0x0027D663 File Offset: 0x0027B863
		public static formatDateTime CreateUnsafe(ProgramNode node)
		{
			return new formatDateTime(node);
		}

		// Token: 0x0600B7B1 RID: 47025 RVA: 0x0027D66C File Offset: 0x0027B86C
		public static formatDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.formatDateTime)
			{
				return null;
			}
			return new formatDateTime?(formatDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600B7B2 RID: 47026 RVA: 0x0027D6A6 File Offset: 0x0027B8A6
		public static formatDateTime CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new formatDateTime(new Hole(g.Symbol.formatDateTime, holeId));
		}

		// Token: 0x0600B7B3 RID: 47027 RVA: 0x0027D6BE File Offset: 0x0027B8BE
		public FormatDateTime Cast_FormatDateTime()
		{
			return FormatDateTime.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B7B4 RID: 47028 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_FormatDateTime(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B7B5 RID: 47029 RVA: 0x0027D6CB File Offset: 0x0027B8CB
		public bool Is_FormatDateTime(GrammarBuilders g, out FormatDateTime value)
		{
			value = FormatDateTime.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B7B6 RID: 47030 RVA: 0x0027D6DF File Offset: 0x0027B8DF
		public FormatDateTime? As_FormatDateTime(GrammarBuilders g)
		{
			return new FormatDateTime?(FormatDateTime.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B7B7 RID: 47031 RVA: 0x0027D6F1 File Offset: 0x0027B8F1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B7B8 RID: 47032 RVA: 0x0027D704 File Offset: 0x0027B904
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B7B9 RID: 47033 RVA: 0x0027D72E File Offset: 0x0027B92E
		public bool Equals(formatDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004664 RID: 18020
		private ProgramNode _node;
	}
}
