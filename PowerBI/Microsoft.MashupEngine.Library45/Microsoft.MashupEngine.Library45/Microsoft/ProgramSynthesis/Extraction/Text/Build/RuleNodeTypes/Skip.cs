using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes
{
	// Token: 0x02000F30 RID: 3888
	public struct Skip : IProgramNodeBuilder, IEquatable<Skip>
	{
		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x06006BB4 RID: 27572 RVA: 0x00161732 File Offset: 0x0015F932
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006BB5 RID: 27573 RVA: 0x0016173A File Offset: 0x0015F93A
		private Skip(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006BB6 RID: 27574 RVA: 0x00161743 File Offset: 0x0015F943
		public static Skip CreateUnsafe(ProgramNode node)
		{
			return new Skip(node);
		}

		// Token: 0x06006BB7 RID: 27575 RVA: 0x0016174C File Offset: 0x0015F94C
		public static Skip? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Skip)
			{
				return null;
			}
			return new Skip?(Skip.CreateUnsafe(node));
		}

		// Token: 0x06006BB8 RID: 27576 RVA: 0x00161781 File Offset: 0x0015F981
		public Skip(GrammarBuilders g, k value0, lines value1)
		{
			this._node = g.Rule.Skip.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06006BB9 RID: 27577 RVA: 0x001617A7 File Offset: 0x0015F9A7
		public static implicit operator skip(Skip arg)
		{
			return skip.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x06006BBA RID: 27578 RVA: 0x001617B5 File Offset: 0x0015F9B5
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x06006BBB RID: 27579 RVA: 0x001617C9 File Offset: 0x0015F9C9
		public lines lines
		{
			get
			{
				return lines.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06006BBC RID: 27580 RVA: 0x001617DD File Offset: 0x0015F9DD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006BBD RID: 27581 RVA: 0x001617F0 File Offset: 0x0015F9F0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006BBE RID: 27582 RVA: 0x0016181A File Offset: 0x0015FA1A
		public bool Equals(Skip other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F1B RID: 12059
		private ProgramNode _node;
	}
}
