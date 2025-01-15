using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001526 RID: 5414
	public struct concatEntry_constString : IProgramNodeBuilder, IEquatable<concatEntry_constString>
	{
		// Token: 0x17001E92 RID: 7826
		// (get) Token: 0x0600B073 RID: 45171 RVA: 0x0026F142 File Offset: 0x0026D342
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B074 RID: 45172 RVA: 0x0026F14A File Offset: 0x0026D34A
		private concatEntry_constString(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B075 RID: 45173 RVA: 0x0026F153 File Offset: 0x0026D353
		public static concatEntry_constString CreateUnsafe(ProgramNode node)
		{
			return new concatEntry_constString(node);
		}

		// Token: 0x0600B076 RID: 45174 RVA: 0x0026F15C File Offset: 0x0026D35C
		public static concatEntry_constString? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.concatEntry_constString)
			{
				return null;
			}
			return new concatEntry_constString?(concatEntry_constString.CreateUnsafe(node));
		}

		// Token: 0x0600B077 RID: 45175 RVA: 0x0026F191 File Offset: 0x0026D391
		public concatEntry_constString(GrammarBuilders g, constString value0)
		{
			this._node = g.UnnamedConversion.concatEntry_constString.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B078 RID: 45176 RVA: 0x0026F1B0 File Offset: 0x0026D3B0
		public static implicit operator concatEntry(concatEntry_constString arg)
		{
			return concatEntry.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E93 RID: 7827
		// (get) Token: 0x0600B079 RID: 45177 RVA: 0x0026F1BE File Offset: 0x0026D3BE
		public constString constString
		{
			get
			{
				return constString.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B07A RID: 45178 RVA: 0x0026F1D2 File Offset: 0x0026D3D2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B07B RID: 45179 RVA: 0x0026F1E8 File Offset: 0x0026D3E8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B07C RID: 45180 RVA: 0x0026F212 File Offset: 0x0026D412
		public bool Equals(concatEntry_constString other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045D4 RID: 17876
		private ProgramNode _node;
	}
}
