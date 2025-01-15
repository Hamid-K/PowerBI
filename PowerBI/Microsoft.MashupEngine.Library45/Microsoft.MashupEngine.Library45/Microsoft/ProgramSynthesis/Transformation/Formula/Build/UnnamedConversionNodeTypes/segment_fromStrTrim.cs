using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001521 RID: 5409
	public struct segment_fromStrTrim : IProgramNodeBuilder, IEquatable<segment_fromStrTrim>
	{
		// Token: 0x17001E88 RID: 7816
		// (get) Token: 0x0600B041 RID: 45121 RVA: 0x0026ECCE File Offset: 0x0026CECE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B042 RID: 45122 RVA: 0x0026ECD6 File Offset: 0x0026CED6
		private segment_fromStrTrim(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B043 RID: 45123 RVA: 0x0026ECDF File Offset: 0x0026CEDF
		public static segment_fromStrTrim CreateUnsafe(ProgramNode node)
		{
			return new segment_fromStrTrim(node);
		}

		// Token: 0x0600B044 RID: 45124 RVA: 0x0026ECE8 File Offset: 0x0026CEE8
		public static segment_fromStrTrim? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.segment_fromStrTrim)
			{
				return null;
			}
			return new segment_fromStrTrim?(segment_fromStrTrim.CreateUnsafe(node));
		}

		// Token: 0x0600B045 RID: 45125 RVA: 0x0026ED1D File Offset: 0x0026CF1D
		public segment_fromStrTrim(GrammarBuilders g, fromStrTrim value0)
		{
			this._node = g.UnnamedConversion.segment_fromStrTrim.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B046 RID: 45126 RVA: 0x0026ED3C File Offset: 0x0026CF3C
		public static implicit operator segment(segment_fromStrTrim arg)
		{
			return segment.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E89 RID: 7817
		// (get) Token: 0x0600B047 RID: 45127 RVA: 0x0026ED4A File Offset: 0x0026CF4A
		public fromStrTrim fromStrTrim
		{
			get
			{
				return fromStrTrim.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B048 RID: 45128 RVA: 0x0026ED5E File Offset: 0x0026CF5E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B049 RID: 45129 RVA: 0x0026ED74 File Offset: 0x0026CF74
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B04A RID: 45130 RVA: 0x0026ED9E File Offset: 0x0026CF9E
		public bool Equals(segment_fromStrTrim other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045CF RID: 17871
		private ProgramNode _node;
	}
}
