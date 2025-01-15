using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes
{
	// Token: 0x02000F31 RID: 3889
	public struct SplitLines : IProgramNodeBuilder, IEquatable<SplitLines>
	{
		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x06006BBF RID: 27583 RVA: 0x0016182E File Offset: 0x0015FA2E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006BC0 RID: 27584 RVA: 0x00161836 File Offset: 0x0015FA36
		private SplitLines(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006BC1 RID: 27585 RVA: 0x0016183F File Offset: 0x0015FA3F
		public static SplitLines CreateUnsafe(ProgramNode node)
		{
			return new SplitLines(node);
		}

		// Token: 0x06006BC2 RID: 27586 RVA: 0x00161848 File Offset: 0x0015FA48
		public static SplitLines? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SplitLines)
			{
				return null;
			}
			return new SplitLines?(SplitLines.CreateUnsafe(node));
		}

		// Token: 0x06006BC3 RID: 27587 RVA: 0x0016187D File Offset: 0x0015FA7D
		public SplitLines(GrammarBuilders g, v value0)
		{
			this._node = g.Rule.SplitLines.BuildASTNode(value0.Node);
		}

		// Token: 0x06006BC4 RID: 27588 RVA: 0x0016189C File Offset: 0x0015FA9C
		public static implicit operator lines(SplitLines arg)
		{
			return lines.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x06006BC5 RID: 27589 RVA: 0x001618AA File Offset: 0x0015FAAA
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006BC6 RID: 27590 RVA: 0x001618BE File Offset: 0x0015FABE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006BC7 RID: 27591 RVA: 0x001618D4 File Offset: 0x0015FAD4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006BC8 RID: 27592 RVA: 0x001618FE File Offset: 0x0015FAFE
		public bool Equals(SplitLines other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F1C RID: 12060
		private ProgramNode _node;
	}
}
