using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes
{
	// Token: 0x02000F23 RID: 3875
	public struct Second : IProgramNodeBuilder, IEquatable<Second>
	{
		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x06006B25 RID: 27429 RVA: 0x00160A56 File Offset: 0x0015EC56
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006B26 RID: 27430 RVA: 0x00160A5E File Offset: 0x0015EC5E
		private Second(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006B27 RID: 27431 RVA: 0x00160A67 File Offset: 0x0015EC67
		public static Second CreateUnsafe(ProgramNode node)
		{
			return new Second(node);
		}

		// Token: 0x06006B28 RID: 27432 RVA: 0x00160A70 File Offset: 0x0015EC70
		public static Second? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Second)
			{
				return null;
			}
			return new Second?(Second.CreateUnsafe(node));
		}

		// Token: 0x06006B29 RID: 27433 RVA: 0x00160AA5 File Offset: 0x0015ECA5
		public Second(GrammarBuilders g, tup value0)
		{
			this._node = g.Rule.Second.BuildASTNode(value0.Node);
		}

		// Token: 0x06006B2A RID: 27434 RVA: 0x00160AC4 File Offset: 0x0015ECC4
		public static implicit operator _LetB0(Second arg)
		{
			return _LetB0.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x06006B2B RID: 27435 RVA: 0x00160AD2 File Offset: 0x0015ECD2
		public tup tup
		{
			get
			{
				return tup.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006B2C RID: 27436 RVA: 0x00160AE6 File Offset: 0x0015ECE6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006B2D RID: 27437 RVA: 0x00160AFC File Offset: 0x0015ECFC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006B2E RID: 27438 RVA: 0x00160B26 File Offset: 0x0015ED26
		public bool Equals(Second other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F0E RID: 12046
		private ProgramNode _node;
	}
}
