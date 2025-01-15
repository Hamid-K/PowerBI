using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001E59 RID: 7769
	public struct newDsl_select : IProgramNodeBuilder, IEquatable<newDsl_select>
	{
		// Token: 0x17002B72 RID: 11122
		// (get) Token: 0x060105C7 RID: 67015 RVA: 0x00388BDA File Offset: 0x00386DDA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060105C8 RID: 67016 RVA: 0x00388BE2 File Offset: 0x00386DE2
		private newDsl_select(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060105C9 RID: 67017 RVA: 0x00388BEB File Offset: 0x00386DEB
		public static newDsl_select CreateUnsafe(ProgramNode node)
		{
			return new newDsl_select(node);
		}

		// Token: 0x060105CA RID: 67018 RVA: 0x00388BF4 File Offset: 0x00386DF4
		public static newDsl_select? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.newDsl_select)
			{
				return null;
			}
			return new newDsl_select?(newDsl_select.CreateUnsafe(node));
		}

		// Token: 0x060105CB RID: 67019 RVA: 0x00388C29 File Offset: 0x00386E29
		public newDsl_select(GrammarBuilders g, select value0)
		{
			this._node = g.UnnamedConversion.newDsl_select.BuildASTNode(value0.Node);
		}

		// Token: 0x060105CC RID: 67020 RVA: 0x00388C48 File Offset: 0x00386E48
		public static implicit operator newDsl(newDsl_select arg)
		{
			return newDsl.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002B73 RID: 11123
		// (get) Token: 0x060105CD RID: 67021 RVA: 0x00388C56 File Offset: 0x00386E56
		public select select
		{
			get
			{
				return select.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060105CE RID: 67022 RVA: 0x00388C6A File Offset: 0x00386E6A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060105CF RID: 67023 RVA: 0x00388C80 File Offset: 0x00386E80
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060105D0 RID: 67024 RVA: 0x00388CAA File Offset: 0x00386EAA
		public bool Equals(newDsl_select other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04006298 RID: 25240
		private ProgramNode _node;
	}
}
