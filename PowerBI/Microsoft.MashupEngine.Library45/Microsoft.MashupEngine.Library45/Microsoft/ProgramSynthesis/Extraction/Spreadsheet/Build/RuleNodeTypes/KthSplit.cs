using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E4C RID: 3660
	public struct KthSplit : IProgramNodeBuilder, IEquatable<KthSplit>
	{
		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x06006247 RID: 25159 RVA: 0x0014055A File Offset: 0x0013E75A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006248 RID: 25160 RVA: 0x00140562 File Offset: 0x0013E762
		private KthSplit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006249 RID: 25161 RVA: 0x0014056B File Offset: 0x0013E76B
		public static KthSplit CreateUnsafe(ProgramNode node)
		{
			return new KthSplit(node);
		}

		// Token: 0x0600624A RID: 25162 RVA: 0x00140574 File Offset: 0x0013E774
		public static KthSplit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.KthSplit)
			{
				return null;
			}
			return new KthSplit?(KthSplit.CreateUnsafe(node));
		}

		// Token: 0x0600624B RID: 25163 RVA: 0x001405A9 File Offset: 0x0013E7A9
		public KthSplit(GrammarBuilders g, sheetSplits value0, k value1)
		{
			this._node = g.Rule.KthSplit.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x0600624C RID: 25164 RVA: 0x001405DB File Offset: 0x0013E7DB
		public static implicit operator uncleanedSheetSection(KthSplit arg)
		{
			return uncleanedSheetSection.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x0600624D RID: 25165 RVA: 0x001405E9 File Offset: 0x0013E7E9
		public sheetSplits sheetSplits
		{
			get
			{
				return sheetSplits.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x0600624E RID: 25166 RVA: 0x001405FD File Offset: 0x0013E7FD
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600624F RID: 25167 RVA: 0x00140611 File Offset: 0x0013E811
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006250 RID: 25168 RVA: 0x00140624 File Offset: 0x0013E824
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006251 RID: 25169 RVA: 0x0014064E File Offset: 0x0013E84E
		public bool Equals(KthSplit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BF6 RID: 11254
		private ProgramNode _node;
	}
}
