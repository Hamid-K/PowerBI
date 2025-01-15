using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000A35 RID: 2613
	public struct pred_match : IProgramNodeBuilder, IEquatable<pred_match>
	{
		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06003FFB RID: 16379 RVA: 0x000CA17A File Offset: 0x000C837A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x000CA182 File Offset: 0x000C8382
		private pred_match(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003FFD RID: 16381 RVA: 0x000CA18B File Offset: 0x000C838B
		public static pred_match CreateUnsafe(ProgramNode node)
		{
			return new pred_match(node);
		}

		// Token: 0x06003FFE RID: 16382 RVA: 0x000CA194 File Offset: 0x000C8394
		public static pred_match? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.pred_match)
			{
				return null;
			}
			return new pred_match?(pred_match.CreateUnsafe(node));
		}

		// Token: 0x06003FFF RID: 16383 RVA: 0x000CA1C9 File Offset: 0x000C83C9
		public pred_match(GrammarBuilders g, match value0)
		{
			this._node = g.UnnamedConversion.pred_match.BuildASTNode(value0.Node);
		}

		// Token: 0x06004000 RID: 16384 RVA: 0x000CA1E8 File Offset: 0x000C83E8
		public static implicit operator pred(pred_match arg)
		{
			return pred.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06004001 RID: 16385 RVA: 0x000CA1F6 File Offset: 0x000C83F6
		public match match
		{
			get
			{
				return match.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06004002 RID: 16386 RVA: 0x000CA20A File Offset: 0x000C840A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004003 RID: 16387 RVA: 0x000CA220 File Offset: 0x000C8420
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004004 RID: 16388 RVA: 0x000CA24A File Offset: 0x000C844A
		public bool Equals(pred_match other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D70 RID: 7536
		private ProgramNode _node;
	}
}
