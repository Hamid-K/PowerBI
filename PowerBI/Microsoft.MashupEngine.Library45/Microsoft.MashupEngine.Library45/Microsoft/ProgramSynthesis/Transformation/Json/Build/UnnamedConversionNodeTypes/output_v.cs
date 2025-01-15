using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001A1B RID: 6683
	public struct output_v : IProgramNodeBuilder, IEquatable<output_v>
	{
		// Token: 0x170024BB RID: 9403
		// (get) Token: 0x0600DB78 RID: 56184 RVA: 0x002ED89F File Offset: 0x002EBA9F
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DB79 RID: 56185 RVA: 0x002ED8A7 File Offset: 0x002EBAA7
		private output_v(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DB7A RID: 56186 RVA: 0x002ED8B0 File Offset: 0x002EBAB0
		public static output_v CreateUnsafe(ProgramNode node)
		{
			return new output_v(node);
		}

		// Token: 0x0600DB7B RID: 56187 RVA: 0x002ED8B8 File Offset: 0x002EBAB8
		public static output_v? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.output_v)
			{
				return null;
			}
			return new output_v?(output_v.CreateUnsafe(node));
		}

		// Token: 0x0600DB7C RID: 56188 RVA: 0x002ED8ED File Offset: 0x002EBAED
		public output_v(GrammarBuilders g, v value0)
		{
			this._node = g.UnnamedConversion.output_v.BuildASTNode(value0.Node);
		}

		// Token: 0x0600DB7D RID: 56189 RVA: 0x002ED90C File Offset: 0x002EBB0C
		public static implicit operator output(output_v arg)
		{
			return output.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024BC RID: 9404
		// (get) Token: 0x0600DB7E RID: 56190 RVA: 0x002ED91A File Offset: 0x002EBB1A
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600DB7F RID: 56191 RVA: 0x002ED92E File Offset: 0x002EBB2E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DB80 RID: 56192 RVA: 0x002ED944 File Offset: 0x002EBB44
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DB81 RID: 56193 RVA: 0x002ED96E File Offset: 0x002EBB6E
		public bool Equals(output_v other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400540C RID: 21516
		private ProgramNode _node;
	}
}
