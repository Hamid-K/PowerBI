using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200103A RID: 4154
	public struct SingleColumn : IProgramNodeBuilder, IEquatable<SingleColumn>
	{
		// Token: 0x170015E6 RID: 5606
		// (get) Token: 0x06007AEF RID: 31471 RVA: 0x001A28CA File Offset: 0x001A0ACA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007AF0 RID: 31472 RVA: 0x001A28D2 File Offset: 0x001A0AD2
		private SingleColumn(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007AF1 RID: 31473 RVA: 0x001A28DB File Offset: 0x001A0ADB
		public static SingleColumn CreateUnsafe(ProgramNode node)
		{
			return new SingleColumn(node);
		}

		// Token: 0x06007AF2 RID: 31474 RVA: 0x001A28E4 File Offset: 0x001A0AE4
		public static SingleColumn? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SingleColumn)
			{
				return null;
			}
			return new SingleColumn?(SingleColumn.CreateUnsafe(node));
		}

		// Token: 0x06007AF3 RID: 31475 RVA: 0x001A2919 File Offset: 0x001A0B19
		public SingleColumn(GrammarBuilders g, resultSequence value0)
		{
			this._node = g.Rule.SingleColumn.BuildASTNode(value0.Node);
		}

		// Token: 0x06007AF4 RID: 31476 RVA: 0x001A2938 File Offset: 0x001A0B38
		public static implicit operator columnSelectors(SingleColumn arg)
		{
			return columnSelectors.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015E7 RID: 5607
		// (get) Token: 0x06007AF5 RID: 31477 RVA: 0x001A2946 File Offset: 0x001A0B46
		public resultSequence resultSequence
		{
			get
			{
				return resultSequence.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06007AF6 RID: 31478 RVA: 0x001A295A File Offset: 0x001A0B5A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007AF7 RID: 31479 RVA: 0x001A2970 File Offset: 0x001A0B70
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007AF8 RID: 31480 RVA: 0x001A299A File Offset: 0x001A0B9A
		public bool Equals(SingleColumn other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003353 RID: 13139
		private ProgramNode _node;
	}
}
