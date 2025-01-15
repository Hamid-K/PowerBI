using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200151C RID: 5404
	public struct outStr1_segmentCase : IProgramNodeBuilder, IEquatable<outStr1_segmentCase>
	{
		// Token: 0x17001E7E RID: 7806
		// (get) Token: 0x0600B00F RID: 45071 RVA: 0x0026E85A File Offset: 0x0026CA5A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B010 RID: 45072 RVA: 0x0026E862 File Offset: 0x0026CA62
		private outStr1_segmentCase(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B011 RID: 45073 RVA: 0x0026E86B File Offset: 0x0026CA6B
		public static outStr1_segmentCase CreateUnsafe(ProgramNode node)
		{
			return new outStr1_segmentCase(node);
		}

		// Token: 0x0600B012 RID: 45074 RVA: 0x0026E874 File Offset: 0x0026CA74
		public static outStr1_segmentCase? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.outStr1_segmentCase)
			{
				return null;
			}
			return new outStr1_segmentCase?(outStr1_segmentCase.CreateUnsafe(node));
		}

		// Token: 0x0600B013 RID: 45075 RVA: 0x0026E8A9 File Offset: 0x0026CAA9
		public outStr1_segmentCase(GrammarBuilders g, segmentCase value0)
		{
			this._node = g.UnnamedConversion.outStr1_segmentCase.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B014 RID: 45076 RVA: 0x0026E8C8 File Offset: 0x0026CAC8
		public static implicit operator outStr1(outStr1_segmentCase arg)
		{
			return outStr1.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E7F RID: 7807
		// (get) Token: 0x0600B015 RID: 45077 RVA: 0x0026E8D6 File Offset: 0x0026CAD6
		public segmentCase segmentCase
		{
			get
			{
				return segmentCase.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B016 RID: 45078 RVA: 0x0026E8EA File Offset: 0x0026CAEA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B017 RID: 45079 RVA: 0x0026E900 File Offset: 0x0026CB00
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B018 RID: 45080 RVA: 0x0026E92A File Offset: 0x0026CB2A
		public bool Equals(outStr1_segmentCase other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045CA RID: 17866
		private ProgramNode _node;
	}
}
