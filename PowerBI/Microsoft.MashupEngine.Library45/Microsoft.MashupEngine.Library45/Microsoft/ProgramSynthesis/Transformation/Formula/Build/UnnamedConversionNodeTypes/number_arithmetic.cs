using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001531 RID: 5425
	public struct number_arithmetic : IProgramNodeBuilder, IEquatable<number_arithmetic>
	{
		// Token: 0x17001EA8 RID: 7848
		// (get) Token: 0x0600B0E1 RID: 45281 RVA: 0x0026FB0E File Offset: 0x0026DD0E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B0E2 RID: 45282 RVA: 0x0026FB16 File Offset: 0x0026DD16
		private number_arithmetic(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B0E3 RID: 45283 RVA: 0x0026FB1F File Offset: 0x0026DD1F
		public static number_arithmetic CreateUnsafe(ProgramNode node)
		{
			return new number_arithmetic(node);
		}

		// Token: 0x0600B0E4 RID: 45284 RVA: 0x0026FB28 File Offset: 0x0026DD28
		public static number_arithmetic? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.number_arithmetic)
			{
				return null;
			}
			return new number_arithmetic?(number_arithmetic.CreateUnsafe(node));
		}

		// Token: 0x0600B0E5 RID: 45285 RVA: 0x0026FB5D File Offset: 0x0026DD5D
		public number_arithmetic(GrammarBuilders g, arithmetic value0)
		{
			this._node = g.UnnamedConversion.number_arithmetic.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B0E6 RID: 45286 RVA: 0x0026FB7C File Offset: 0x0026DD7C
		public static implicit operator number(number_arithmetic arg)
		{
			return number.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EA9 RID: 7849
		// (get) Token: 0x0600B0E7 RID: 45287 RVA: 0x0026FB8A File Offset: 0x0026DD8A
		public arithmetic arithmetic
		{
			get
			{
				return arithmetic.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B0E8 RID: 45288 RVA: 0x0026FB9E File Offset: 0x0026DD9E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B0E9 RID: 45289 RVA: 0x0026FBB4 File Offset: 0x0026DDB4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B0EA RID: 45290 RVA: 0x0026FBDE File Offset: 0x0026DDDE
		public bool Equals(number_arithmetic other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045DF RID: 17887
		private ProgramNode _node;
	}
}
