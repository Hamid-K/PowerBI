using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001058 RID: 4184
	public struct Substring : IProgramNodeBuilder, IEquatable<Substring>
	{
		// Token: 0x1700163C RID: 5692
		// (get) Token: 0x06007C35 RID: 31797 RVA: 0x001A46D6 File Offset: 0x001A28D6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007C36 RID: 31798 RVA: 0x001A46DE File Offset: 0x001A28DE
		private Substring(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007C37 RID: 31799 RVA: 0x001A46E7 File Offset: 0x001A28E7
		public static Substring CreateUnsafe(ProgramNode node)
		{
			return new Substring(node);
		}

		// Token: 0x06007C38 RID: 31800 RVA: 0x001A46F0 File Offset: 0x001A28F0
		public static Substring? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Substring)
			{
				return null;
			}
			return new Substring?(Substring.CreateUnsafe(node));
		}

		// Token: 0x06007C39 RID: 31801 RVA: 0x001A4725 File Offset: 0x001A2925
		public Substring(GrammarBuilders g, SS value0)
		{
			this._node = g.Rule.Substring.BuildASTNode(value0.Node);
		}

		// Token: 0x06007C3A RID: 31802 RVA: 0x001A4744 File Offset: 0x001A2944
		public static implicit operator substring(Substring arg)
		{
			return substring.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700163D RID: 5693
		// (get) Token: 0x06007C3B RID: 31803 RVA: 0x001A4752 File Offset: 0x001A2952
		public SS SS
		{
			get
			{
				return SS.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06007C3C RID: 31804 RVA: 0x001A4766 File Offset: 0x001A2966
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007C3D RID: 31805 RVA: 0x001A477C File Offset: 0x001A297C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007C3E RID: 31806 RVA: 0x001A47A6 File Offset: 0x001A29A6
		public bool Equals(Substring other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003371 RID: 13169
		private ProgramNode _node;
	}
}
