using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000A34 RID: 2612
	public struct baseConjunct_pred : IProgramNodeBuilder, IEquatable<baseConjunct_pred>
	{
		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06003FF1 RID: 16369 RVA: 0x000CA095 File Offset: 0x000C8295
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x000CA09D File Offset: 0x000C829D
		private baseConjunct_pred(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x000CA0A6 File Offset: 0x000C82A6
		public static baseConjunct_pred CreateUnsafe(ProgramNode node)
		{
			return new baseConjunct_pred(node);
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x000CA0B0 File Offset: 0x000C82B0
		public static baseConjunct_pred? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.baseConjunct_pred)
			{
				return null;
			}
			return new baseConjunct_pred?(baseConjunct_pred.CreateUnsafe(node));
		}

		// Token: 0x06003FF5 RID: 16373 RVA: 0x000CA0E5 File Offset: 0x000C82E5
		public baseConjunct_pred(GrammarBuilders g, pred value0)
		{
			this._node = g.UnnamedConversion.baseConjunct_pred.BuildASTNode(value0.Node);
		}

		// Token: 0x06003FF6 RID: 16374 RVA: 0x000CA104 File Offset: 0x000C8304
		public static implicit operator baseConjunct(baseConjunct_pred arg)
		{
			return baseConjunct.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06003FF7 RID: 16375 RVA: 0x000CA112 File Offset: 0x000C8312
		public pred pred
		{
			get
			{
				return pred.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x000CA126 File Offset: 0x000C8326
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003FF9 RID: 16377 RVA: 0x000CA13C File Offset: 0x000C833C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003FFA RID: 16378 RVA: 0x000CA166 File Offset: 0x000C8366
		public bool Equals(baseConjunct_pred other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D6F RID: 7535
		private ProgramNode _node;
	}
}
