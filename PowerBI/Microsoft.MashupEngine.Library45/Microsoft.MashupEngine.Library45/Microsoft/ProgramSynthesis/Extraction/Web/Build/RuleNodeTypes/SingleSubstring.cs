using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001036 RID: 4150
	public struct SingleSubstring : IProgramNodeBuilder, IEquatable<SingleSubstring>
	{
		// Token: 0x170015DC RID: 5596
		// (get) Token: 0x06007AC5 RID: 31429 RVA: 0x001A250A File Offset: 0x001A070A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007AC6 RID: 31430 RVA: 0x001A2512 File Offset: 0x001A0712
		private SingleSubstring(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007AC7 RID: 31431 RVA: 0x001A251B File Offset: 0x001A071B
		public static SingleSubstring CreateUnsafe(ProgramNode node)
		{
			return new SingleSubstring(node);
		}

		// Token: 0x06007AC8 RID: 31432 RVA: 0x001A2524 File Offset: 0x001A0724
		public static SingleSubstring? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SingleSubstring)
			{
				return null;
			}
			return new SingleSubstring?(SingleSubstring.CreateUnsafe(node));
		}

		// Token: 0x06007AC9 RID: 31433 RVA: 0x001A2559 File Offset: 0x001A0759
		public SingleSubstring(GrammarBuilders g, substring value0)
		{
			this._node = g.Rule.SingleSubstring.BuildASTNode(value0.Node);
		}

		// Token: 0x06007ACA RID: 31434 RVA: 0x001A2578 File Offset: 0x001A0778
		public static implicit operator substringDisj(SingleSubstring arg)
		{
			return substringDisj.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015DD RID: 5597
		// (get) Token: 0x06007ACB RID: 31435 RVA: 0x001A2586 File Offset: 0x001A0786
		public substring substring
		{
			get
			{
				return substring.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06007ACC RID: 31436 RVA: 0x001A259A File Offset: 0x001A079A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007ACD RID: 31437 RVA: 0x001A25B0 File Offset: 0x001A07B0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007ACE RID: 31438 RVA: 0x001A25DA File Offset: 0x001A07DA
		public bool Equals(SingleSubstring other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400334F RID: 13135
		private ProgramNode _node;
	}
}
