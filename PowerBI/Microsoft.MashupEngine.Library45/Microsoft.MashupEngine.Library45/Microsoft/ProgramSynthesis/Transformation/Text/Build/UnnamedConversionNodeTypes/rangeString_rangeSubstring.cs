using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001BF2 RID: 7154
	public struct rangeString_rangeSubstring : IProgramNodeBuilder, IEquatable<rangeString_rangeSubstring>
	{
		// Token: 0x17002806 RID: 10246
		// (get) Token: 0x0600F075 RID: 61557 RVA: 0x0033E2A6 File Offset: 0x0033C4A6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F076 RID: 61558 RVA: 0x0033E2AE File Offset: 0x0033C4AE
		private rangeString_rangeSubstring(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F077 RID: 61559 RVA: 0x0033E2B7 File Offset: 0x0033C4B7
		public static rangeString_rangeSubstring CreateUnsafe(ProgramNode node)
		{
			return new rangeString_rangeSubstring(node);
		}

		// Token: 0x0600F078 RID: 61560 RVA: 0x0033E2C0 File Offset: 0x0033C4C0
		public static rangeString_rangeSubstring? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.rangeString_rangeSubstring)
			{
				return null;
			}
			return new rangeString_rangeSubstring?(rangeString_rangeSubstring.CreateUnsafe(node));
		}

		// Token: 0x0600F079 RID: 61561 RVA: 0x0033E2F5 File Offset: 0x0033C4F5
		public rangeString_rangeSubstring(GrammarBuilders g, rangeSubstring value0)
		{
			this._node = g.UnnamedConversion.rangeString_rangeSubstring.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F07A RID: 61562 RVA: 0x0033E314 File Offset: 0x0033C514
		public static implicit operator rangeString(rangeString_rangeSubstring arg)
		{
			return rangeString.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002807 RID: 10247
		// (get) Token: 0x0600F07B RID: 61563 RVA: 0x0033E322 File Offset: 0x0033C522
		public rangeSubstring rangeSubstring
		{
			get
			{
				return rangeSubstring.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F07C RID: 61564 RVA: 0x0033E336 File Offset: 0x0033C536
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F07D RID: 61565 RVA: 0x0033E34C File Offset: 0x0033C54C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F07E RID: 61566 RVA: 0x0033E376 File Offset: 0x0033C576
		public bool Equals(rangeString_rangeSubstring other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AE1 RID: 23265
		private ProgramNode _node;
	}
}
