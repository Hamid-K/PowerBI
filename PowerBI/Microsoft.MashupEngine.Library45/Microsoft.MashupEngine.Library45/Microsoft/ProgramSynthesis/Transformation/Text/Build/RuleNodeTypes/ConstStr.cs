using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001BFC RID: 7164
	public struct ConstStr : IProgramNodeBuilder, IEquatable<ConstStr>
	{
		// Token: 0x1700281D RID: 10269
		// (get) Token: 0x0600F0DC RID: 61660 RVA: 0x0033EBDA File Offset: 0x0033CDDA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F0DD RID: 61661 RVA: 0x0033EBE2 File Offset: 0x0033CDE2
		private ConstStr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F0DE RID: 61662 RVA: 0x0033EBEB File Offset: 0x0033CDEB
		public static ConstStr CreateUnsafe(ProgramNode node)
		{
			return new ConstStr(node);
		}

		// Token: 0x0600F0DF RID: 61663 RVA: 0x0033EBF4 File Offset: 0x0033CDF4
		public static ConstStr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ConstStr)
			{
				return null;
			}
			return new ConstStr?(ConstStr.CreateUnsafe(node));
		}

		// Token: 0x0600F0E0 RID: 61664 RVA: 0x0033EC29 File Offset: 0x0033CE29
		public ConstStr(GrammarBuilders g, s value0)
		{
			this._node = g.Rule.ConstStr.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F0E1 RID: 61665 RVA: 0x0033EC48 File Offset: 0x0033CE48
		public static implicit operator f(ConstStr arg)
		{
			return f.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700281E RID: 10270
		// (get) Token: 0x0600F0E2 RID: 61666 RVA: 0x0033EC56 File Offset: 0x0033CE56
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F0E3 RID: 61667 RVA: 0x0033EC6A File Offset: 0x0033CE6A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F0E4 RID: 61668 RVA: 0x0033EC80 File Offset: 0x0033CE80
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F0E5 RID: 61669 RVA: 0x0033ECAA File Offset: 0x0033CEAA
		public bool Equals(ConstStr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AEB RID: 23275
		private ProgramNode _node;
	}
}
