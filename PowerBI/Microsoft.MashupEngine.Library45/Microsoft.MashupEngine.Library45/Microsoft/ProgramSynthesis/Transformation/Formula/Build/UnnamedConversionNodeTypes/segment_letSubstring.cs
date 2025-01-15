using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001522 RID: 5410
	public struct segment_letSubstring : IProgramNodeBuilder, IEquatable<segment_letSubstring>
	{
		// Token: 0x17001E8A RID: 7818
		// (get) Token: 0x0600B04B RID: 45131 RVA: 0x0026EDB2 File Offset: 0x0026CFB2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B04C RID: 45132 RVA: 0x0026EDBA File Offset: 0x0026CFBA
		private segment_letSubstring(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B04D RID: 45133 RVA: 0x0026EDC3 File Offset: 0x0026CFC3
		public static segment_letSubstring CreateUnsafe(ProgramNode node)
		{
			return new segment_letSubstring(node);
		}

		// Token: 0x0600B04E RID: 45134 RVA: 0x0026EDCC File Offset: 0x0026CFCC
		public static segment_letSubstring? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.segment_letSubstring)
			{
				return null;
			}
			return new segment_letSubstring?(segment_letSubstring.CreateUnsafe(node));
		}

		// Token: 0x0600B04F RID: 45135 RVA: 0x0026EE01 File Offset: 0x0026D001
		public segment_letSubstring(GrammarBuilders g, letSubstring value0)
		{
			this._node = g.UnnamedConversion.segment_letSubstring.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B050 RID: 45136 RVA: 0x0026EE20 File Offset: 0x0026D020
		public static implicit operator segment(segment_letSubstring arg)
		{
			return segment.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E8B RID: 7819
		// (get) Token: 0x0600B051 RID: 45137 RVA: 0x0026EE2E File Offset: 0x0026D02E
		public letSubstring letSubstring
		{
			get
			{
				return letSubstring.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B052 RID: 45138 RVA: 0x0026EE42 File Offset: 0x0026D042
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B053 RID: 45139 RVA: 0x0026EE58 File Offset: 0x0026D058
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B054 RID: 45140 RVA: 0x0026EE82 File Offset: 0x0026D082
		public bool Equals(segment_letSubstring other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045D0 RID: 17872
		private ProgramNode _node;
	}
}
