using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes
{
	// Token: 0x02000F27 RID: 3879
	public struct Trim : IProgramNodeBuilder, IEquatable<Trim>
	{
		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x06006B4E RID: 27470 RVA: 0x00160DFE File Offset: 0x0015EFFE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006B4F RID: 27471 RVA: 0x00160E06 File Offset: 0x0015F006
		private Trim(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006B50 RID: 27472 RVA: 0x00160E0F File Offset: 0x0015F00F
		public static Trim CreateUnsafe(ProgramNode node)
		{
			return new Trim(node);
		}

		// Token: 0x06006B51 RID: 27473 RVA: 0x00160E18 File Offset: 0x0015F018
		public static Trim? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Trim)
			{
				return null;
			}
			return new Trim?(Trim.CreateUnsafe(node));
		}

		// Token: 0x06006B52 RID: 27474 RVA: 0x00160E4D File Offset: 0x0015F04D
		public Trim(GrammarBuilders g, extract value0)
		{
			this._node = g.Rule.Trim.BuildASTNode(value0.Node);
		}

		// Token: 0x06006B53 RID: 27475 RVA: 0x00160E6C File Offset: 0x0015F06C
		public static implicit operator trimExtract(Trim arg)
		{
			return trimExtract.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x06006B54 RID: 27476 RVA: 0x00160E7A File Offset: 0x0015F07A
		public extract extract
		{
			get
			{
				return extract.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006B55 RID: 27477 RVA: 0x00160E8E File Offset: 0x0015F08E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006B56 RID: 27478 RVA: 0x00160EA4 File Offset: 0x0015F0A4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006B57 RID: 27479 RVA: 0x00160ECE File Offset: 0x0015F0CE
		public bool Equals(Trim other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F12 RID: 12050
		private ProgramNode _node;
	}
}
