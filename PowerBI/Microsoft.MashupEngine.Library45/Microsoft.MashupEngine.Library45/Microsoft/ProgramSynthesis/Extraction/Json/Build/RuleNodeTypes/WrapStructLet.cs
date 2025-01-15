using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes
{
	// Token: 0x02000B64 RID: 2916
	public struct WrapStructLet : IProgramNodeBuilder, IEquatable<WrapStructLet>
	{
		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x060049B0 RID: 18864 RVA: 0x000E83C6 File Offset: 0x000E65C6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x000E83CE File Offset: 0x000E65CE
		private WrapStructLet(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060049B2 RID: 18866 RVA: 0x000E83D7 File Offset: 0x000E65D7
		public static WrapStructLet CreateUnsafe(ProgramNode node)
		{
			return new WrapStructLet(node);
		}

		// Token: 0x060049B3 RID: 18867 RVA: 0x000E83E0 File Offset: 0x000E65E0
		public static WrapStructLet? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.WrapStructLet)
			{
				return null;
			}
			return new WrapStructLet?(WrapStructLet.CreateUnsafe(node));
		}

		// Token: 0x060049B4 RID: 18868 RVA: 0x000E8415 File Offset: 0x000E6615
		public WrapStructLet(GrammarBuilders g, x value0, output value1)
		{
			this._node = new LetNode(g.Rule.WrapStructLet, value0.Node, value1.Node);
		}

		// Token: 0x060049B5 RID: 18869 RVA: 0x000E843B File Offset: 0x000E663B
		public static implicit operator wrapStruct(WrapStructLet arg)
		{
			return wrapStruct.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x060049B6 RID: 18870 RVA: 0x000E8449 File Offset: 0x000E6649
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x060049B7 RID: 18871 RVA: 0x000E845D File Offset: 0x000E665D
		public output output
		{
			get
			{
				return output.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060049B8 RID: 18872 RVA: 0x000E8471 File Offset: 0x000E6671
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060049B9 RID: 18873 RVA: 0x000E8484 File Offset: 0x000E6684
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060049BA RID: 18874 RVA: 0x000E84AE File Offset: 0x000E66AE
		public bool Equals(WrapStructLet other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400215F RID: 8543
		private ProgramNode _node;
	}
}
