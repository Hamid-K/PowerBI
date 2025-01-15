using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E50 RID: 3664
	public struct WrapOutputForTitle : IProgramNodeBuilder, IEquatable<WrapOutputForTitle>
	{
		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x06006271 RID: 25201 RVA: 0x00140932 File Offset: 0x0013EB32
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006272 RID: 25202 RVA: 0x0014093A File Offset: 0x0013EB3A
		private WrapOutputForTitle(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006273 RID: 25203 RVA: 0x00140943 File Offset: 0x0013EB43
		public static WrapOutputForTitle CreateUnsafe(ProgramNode node)
		{
			return new WrapOutputForTitle(node);
		}

		// Token: 0x06006274 RID: 25204 RVA: 0x0014094C File Offset: 0x0013EB4C
		public static WrapOutputForTitle? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.WrapOutputForTitle)
			{
				return null;
			}
			return new WrapOutputForTitle?(WrapOutputForTitle.CreateUnsafe(node));
		}

		// Token: 0x06006275 RID: 25205 RVA: 0x00140981 File Offset: 0x0013EB81
		public WrapOutputForTitle(GrammarBuilders g, output value0)
		{
			this._node = g.Rule.WrapOutputForTitle.BuildASTNode(value0.Node);
		}

		// Token: 0x06006276 RID: 25206 RVA: 0x001409A0 File Offset: 0x0013EBA0
		public static implicit operator titleOf(WrapOutputForTitle arg)
		{
			return titleOf.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x06006277 RID: 25207 RVA: 0x001409AE File Offset: 0x0013EBAE
		public output output
		{
			get
			{
				return output.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006278 RID: 25208 RVA: 0x001409C2 File Offset: 0x0013EBC2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006279 RID: 25209 RVA: 0x001409D8 File Offset: 0x0013EBD8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600627A RID: 25210 RVA: 0x00140A02 File Offset: 0x0013EC02
		public bool Equals(WrapOutputForTitle other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BFA RID: 11258
		private ProgramNode _node;
	}
}
