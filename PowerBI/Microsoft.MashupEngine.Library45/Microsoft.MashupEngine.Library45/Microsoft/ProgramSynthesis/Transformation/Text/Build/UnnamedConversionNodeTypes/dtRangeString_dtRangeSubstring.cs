using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001BF3 RID: 7155
	public struct dtRangeString_dtRangeSubstring : IProgramNodeBuilder, IEquatable<dtRangeString_dtRangeSubstring>
	{
		// Token: 0x17002808 RID: 10248
		// (get) Token: 0x0600F07F RID: 61567 RVA: 0x0033E38A File Offset: 0x0033C58A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F080 RID: 61568 RVA: 0x0033E392 File Offset: 0x0033C592
		private dtRangeString_dtRangeSubstring(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F081 RID: 61569 RVA: 0x0033E39B File Offset: 0x0033C59B
		public static dtRangeString_dtRangeSubstring CreateUnsafe(ProgramNode node)
		{
			return new dtRangeString_dtRangeSubstring(node);
		}

		// Token: 0x0600F082 RID: 61570 RVA: 0x0033E3A4 File Offset: 0x0033C5A4
		public static dtRangeString_dtRangeSubstring? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.dtRangeString_dtRangeSubstring)
			{
				return null;
			}
			return new dtRangeString_dtRangeSubstring?(dtRangeString_dtRangeSubstring.CreateUnsafe(node));
		}

		// Token: 0x0600F083 RID: 61571 RVA: 0x0033E3D9 File Offset: 0x0033C5D9
		public dtRangeString_dtRangeSubstring(GrammarBuilders g, dtRangeSubstring value0)
		{
			this._node = g.UnnamedConversion.dtRangeString_dtRangeSubstring.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F084 RID: 61572 RVA: 0x0033E3F8 File Offset: 0x0033C5F8
		public static implicit operator dtRangeString(dtRangeString_dtRangeSubstring arg)
		{
			return dtRangeString.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002809 RID: 10249
		// (get) Token: 0x0600F085 RID: 61573 RVA: 0x0033E406 File Offset: 0x0033C606
		public dtRangeSubstring dtRangeSubstring
		{
			get
			{
				return dtRangeSubstring.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F086 RID: 61574 RVA: 0x0033E41A File Offset: 0x0033C61A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F087 RID: 61575 RVA: 0x0033E430 File Offset: 0x0033C630
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F088 RID: 61576 RVA: 0x0033E45A File Offset: 0x0033C65A
		public bool Equals(dtRangeString_dtRangeSubstring other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AE2 RID: 23266
		private ProgramNode _node;
	}
}
