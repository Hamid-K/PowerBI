using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200105A RID: 4186
	public struct LetSubstring : IProgramNodeBuilder, IEquatable<LetSubstring>
	{
		// Token: 0x17001641 RID: 5697
		// (get) Token: 0x06007C4A RID: 31818 RVA: 0x001A48B6 File Offset: 0x001A2AB6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007C4B RID: 31819 RVA: 0x001A48BE File Offset: 0x001A2ABE
		private LetSubstring(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007C4C RID: 31820 RVA: 0x001A48C7 File Offset: 0x001A2AC7
		public static LetSubstring CreateUnsafe(ProgramNode node)
		{
			return new LetSubstring(node);
		}

		// Token: 0x06007C4D RID: 31821 RVA: 0x001A48D0 File Offset: 0x001A2AD0
		public static LetSubstring? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetSubstring)
			{
				return null;
			}
			return new LetSubstring?(LetSubstring.CreateUnsafe(node));
		}

		// Token: 0x06007C4E RID: 31822 RVA: 0x001A4905 File Offset: 0x001A2B05
		public LetSubstring(GrammarBuilders g, y value0, selectSubstring value1)
		{
			this._node = new LetNode(g.Rule.LetSubstring, value0.Node, value1.Node);
		}

		// Token: 0x06007C4F RID: 31823 RVA: 0x001A492B File Offset: 0x001A2B2B
		public static implicit operator fieldSubstring(LetSubstring arg)
		{
			return fieldSubstring.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001642 RID: 5698
		// (get) Token: 0x06007C50 RID: 31824 RVA: 0x001A4939 File Offset: 0x001A2B39
		public y y
		{
			get
			{
				return y.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001643 RID: 5699
		// (get) Token: 0x06007C51 RID: 31825 RVA: 0x001A494D File Offset: 0x001A2B4D
		public selectSubstring selectSubstring
		{
			get
			{
				return selectSubstring.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007C52 RID: 31826 RVA: 0x001A4961 File Offset: 0x001A2B61
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007C53 RID: 31827 RVA: 0x001A4974 File Offset: 0x001A2B74
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007C54 RID: 31828 RVA: 0x001A499E File Offset: 0x001A2B9E
		public bool Equals(LetSubstring other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003373 RID: 13171
		private ProgramNode _node;
	}
}
