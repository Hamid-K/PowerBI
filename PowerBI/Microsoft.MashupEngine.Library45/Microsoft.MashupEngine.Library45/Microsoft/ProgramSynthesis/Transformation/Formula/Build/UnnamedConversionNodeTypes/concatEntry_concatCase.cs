using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001525 RID: 5413
	public struct concatEntry_concatCase : IProgramNodeBuilder, IEquatable<concatEntry_concatCase>
	{
		// Token: 0x17001E90 RID: 7824
		// (get) Token: 0x0600B069 RID: 45161 RVA: 0x0026F05E File Offset: 0x0026D25E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B06A RID: 45162 RVA: 0x0026F066 File Offset: 0x0026D266
		private concatEntry_concatCase(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B06B RID: 45163 RVA: 0x0026F06F File Offset: 0x0026D26F
		public static concatEntry_concatCase CreateUnsafe(ProgramNode node)
		{
			return new concatEntry_concatCase(node);
		}

		// Token: 0x0600B06C RID: 45164 RVA: 0x0026F078 File Offset: 0x0026D278
		public static concatEntry_concatCase? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.concatEntry_concatCase)
			{
				return null;
			}
			return new concatEntry_concatCase?(concatEntry_concatCase.CreateUnsafe(node));
		}

		// Token: 0x0600B06D RID: 45165 RVA: 0x0026F0AD File Offset: 0x0026D2AD
		public concatEntry_concatCase(GrammarBuilders g, concatCase value0)
		{
			this._node = g.UnnamedConversion.concatEntry_concatCase.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B06E RID: 45166 RVA: 0x0026F0CC File Offset: 0x0026D2CC
		public static implicit operator concatEntry(concatEntry_concatCase arg)
		{
			return concatEntry.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E91 RID: 7825
		// (get) Token: 0x0600B06F RID: 45167 RVA: 0x0026F0DA File Offset: 0x0026D2DA
		public concatCase concatCase
		{
			get
			{
				return concatCase.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B070 RID: 45168 RVA: 0x0026F0EE File Offset: 0x0026D2EE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B071 RID: 45169 RVA: 0x0026F104 File Offset: 0x0026D304
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B072 RID: 45170 RVA: 0x0026F12E File Offset: 0x0026D32E
		public bool Equals(concatEntry_concatCase other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045D3 RID: 17875
		private ProgramNode _node;
	}
}
