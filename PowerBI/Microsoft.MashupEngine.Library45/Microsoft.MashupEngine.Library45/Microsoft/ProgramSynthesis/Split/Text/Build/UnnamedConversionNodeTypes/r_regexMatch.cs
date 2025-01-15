using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200133D RID: 4925
	public struct r_regexMatch : IProgramNodeBuilder, IEquatable<r_regexMatch>
	{
		// Token: 0x170019FF RID: 6655
		// (get) Token: 0x060097AA RID: 38826 RVA: 0x00205A46 File Offset: 0x00203C46
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060097AB RID: 38827 RVA: 0x00205A4E File Offset: 0x00203C4E
		private r_regexMatch(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060097AC RID: 38828 RVA: 0x00205A57 File Offset: 0x00203C57
		public static r_regexMatch CreateUnsafe(ProgramNode node)
		{
			return new r_regexMatch(node);
		}

		// Token: 0x060097AD RID: 38829 RVA: 0x00205A60 File Offset: 0x00203C60
		public static r_regexMatch? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.r_regexMatch)
			{
				return null;
			}
			return new r_regexMatch?(r_regexMatch.CreateUnsafe(node));
		}

		// Token: 0x060097AE RID: 38830 RVA: 0x00205A95 File Offset: 0x00203C95
		public r_regexMatch(GrammarBuilders g, regexMatch value0)
		{
			this._node = g.UnnamedConversion.r_regexMatch.BuildASTNode(value0.Node);
		}

		// Token: 0x060097AF RID: 38831 RVA: 0x00205AB4 File Offset: 0x00203CB4
		public static implicit operator r(r_regexMatch arg)
		{
			return r.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A00 RID: 6656
		// (get) Token: 0x060097B0 RID: 38832 RVA: 0x00205AC2 File Offset: 0x00203CC2
		public regexMatch regexMatch
		{
			get
			{
				return regexMatch.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060097B1 RID: 38833 RVA: 0x00205AD6 File Offset: 0x00203CD6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060097B2 RID: 38834 RVA: 0x00205AEC File Offset: 0x00203CEC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060097B3 RID: 38835 RVA: 0x00205B16 File Offset: 0x00203D16
		public bool Equals(r_regexMatch other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DB4 RID: 15796
		private ProgramNode _node;
	}
}
