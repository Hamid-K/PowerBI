using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200151B RID: 5403
	public struct outStr_outStr1 : IProgramNodeBuilder, IEquatable<outStr_outStr1>
	{
		// Token: 0x17001E7C RID: 7804
		// (get) Token: 0x0600B005 RID: 45061 RVA: 0x0026E776 File Offset: 0x0026C976
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B006 RID: 45062 RVA: 0x0026E77E File Offset: 0x0026C97E
		private outStr_outStr1(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B007 RID: 45063 RVA: 0x0026E787 File Offset: 0x0026C987
		public static outStr_outStr1 CreateUnsafe(ProgramNode node)
		{
			return new outStr_outStr1(node);
		}

		// Token: 0x0600B008 RID: 45064 RVA: 0x0026E790 File Offset: 0x0026C990
		public static outStr_outStr1? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.outStr_outStr1)
			{
				return null;
			}
			return new outStr_outStr1?(outStr_outStr1.CreateUnsafe(node));
		}

		// Token: 0x0600B009 RID: 45065 RVA: 0x0026E7C5 File Offset: 0x0026C9C5
		public outStr_outStr1(GrammarBuilders g, outStr1 value0)
		{
			this._node = g.UnnamedConversion.outStr_outStr1.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B00A RID: 45066 RVA: 0x0026E7E4 File Offset: 0x0026C9E4
		public static implicit operator outStr(outStr_outStr1 arg)
		{
			return outStr.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E7D RID: 7805
		// (get) Token: 0x0600B00B RID: 45067 RVA: 0x0026E7F2 File Offset: 0x0026C9F2
		public outStr1 outStr1
		{
			get
			{
				return outStr1.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B00C RID: 45068 RVA: 0x0026E806 File Offset: 0x0026CA06
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B00D RID: 45069 RVA: 0x0026E81C File Offset: 0x0026CA1C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B00E RID: 45070 RVA: 0x0026E846 File Offset: 0x0026CA46
		public bool Equals(outStr_outStr1 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045C9 RID: 17865
		private ProgramNode _node;
	}
}
