using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001519 RID: 5401
	public struct outDate_date : IProgramNodeBuilder, IEquatable<outDate_date>
	{
		// Token: 0x17001E78 RID: 7800
		// (get) Token: 0x0600AFF1 RID: 45041 RVA: 0x0026E5AE File Offset: 0x0026C7AE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600AFF2 RID: 45042 RVA: 0x0026E5B6 File Offset: 0x0026C7B6
		private outDate_date(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600AFF3 RID: 45043 RVA: 0x0026E5BF File Offset: 0x0026C7BF
		public static outDate_date CreateUnsafe(ProgramNode node)
		{
			return new outDate_date(node);
		}

		// Token: 0x0600AFF4 RID: 45044 RVA: 0x0026E5C8 File Offset: 0x0026C7C8
		public static outDate_date? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.outDate_date)
			{
				return null;
			}
			return new outDate_date?(outDate_date.CreateUnsafe(node));
		}

		// Token: 0x0600AFF5 RID: 45045 RVA: 0x0026E5FD File Offset: 0x0026C7FD
		public outDate_date(GrammarBuilders g, date value0)
		{
			this._node = g.UnnamedConversion.outDate_date.BuildASTNode(value0.Node);
		}

		// Token: 0x0600AFF6 RID: 45046 RVA: 0x0026E61C File Offset: 0x0026C81C
		public static implicit operator outDate(outDate_date arg)
		{
			return outDate.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E79 RID: 7801
		// (get) Token: 0x0600AFF7 RID: 45047 RVA: 0x0026E62A File Offset: 0x0026C82A
		public date date
		{
			get
			{
				return date.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600AFF8 RID: 45048 RVA: 0x0026E63E File Offset: 0x0026C83E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600AFF9 RID: 45049 RVA: 0x0026E654 File Offset: 0x0026C854
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600AFFA RID: 45050 RVA: 0x0026E67E File Offset: 0x0026C87E
		public bool Equals(outDate_date other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045C7 RID: 17863
		private ProgramNode _node;
	}
}
