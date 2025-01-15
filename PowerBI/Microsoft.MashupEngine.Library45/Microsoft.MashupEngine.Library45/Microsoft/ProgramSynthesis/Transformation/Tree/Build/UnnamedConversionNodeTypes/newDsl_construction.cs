using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001E5A RID: 7770
	public struct newDsl_construction : IProgramNodeBuilder, IEquatable<newDsl_construction>
	{
		// Token: 0x17002B74 RID: 11124
		// (get) Token: 0x060105D1 RID: 67025 RVA: 0x00388CBE File Offset: 0x00386EBE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060105D2 RID: 67026 RVA: 0x00388CC6 File Offset: 0x00386EC6
		private newDsl_construction(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060105D3 RID: 67027 RVA: 0x00388CCF File Offset: 0x00386ECF
		public static newDsl_construction CreateUnsafe(ProgramNode node)
		{
			return new newDsl_construction(node);
		}

		// Token: 0x060105D4 RID: 67028 RVA: 0x00388CD8 File Offset: 0x00386ED8
		public static newDsl_construction? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.newDsl_construction)
			{
				return null;
			}
			return new newDsl_construction?(newDsl_construction.CreateUnsafe(node));
		}

		// Token: 0x060105D5 RID: 67029 RVA: 0x00388D0D File Offset: 0x00386F0D
		public newDsl_construction(GrammarBuilders g, construction value0)
		{
			this._node = g.UnnamedConversion.newDsl_construction.BuildASTNode(value0.Node);
		}

		// Token: 0x060105D6 RID: 67030 RVA: 0x00388D2C File Offset: 0x00386F2C
		public static implicit operator newDsl(newDsl_construction arg)
		{
			return newDsl.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002B75 RID: 11125
		// (get) Token: 0x060105D7 RID: 67031 RVA: 0x00388D3A File Offset: 0x00386F3A
		public construction construction
		{
			get
			{
				return construction.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060105D8 RID: 67032 RVA: 0x00388D4E File Offset: 0x00386F4E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060105D9 RID: 67033 RVA: 0x00388D64 File Offset: 0x00386F64
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060105DA RID: 67034 RVA: 0x00388D8E File Offset: 0x00386F8E
		public bool Equals(newDsl_construction other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04006299 RID: 25241
		private ProgramNode _node;
	}
}
