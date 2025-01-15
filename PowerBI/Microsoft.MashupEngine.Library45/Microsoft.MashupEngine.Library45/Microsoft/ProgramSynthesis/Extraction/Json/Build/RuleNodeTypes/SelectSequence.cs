using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes
{
	// Token: 0x02000B61 RID: 2913
	public struct SelectSequence : IProgramNodeBuilder, IEquatable<SelectSequence>
	{
		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x0600498F RID: 18831 RVA: 0x000E80BE File Offset: 0x000E62BE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004990 RID: 18832 RVA: 0x000E80C6 File Offset: 0x000E62C6
		private SelectSequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004991 RID: 18833 RVA: 0x000E80CF File Offset: 0x000E62CF
		public static SelectSequence CreateUnsafe(ProgramNode node)
		{
			return new SelectSequence(node);
		}

		// Token: 0x06004992 RID: 18834 RVA: 0x000E80D8 File Offset: 0x000E62D8
		public static SelectSequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SelectSequence)
			{
				return null;
			}
			return new SelectSequence?(SelectSequence.CreateUnsafe(node));
		}

		// Token: 0x06004993 RID: 18835 RVA: 0x000E810D File Offset: 0x000E630D
		public SelectSequence(GrammarBuilders g, v value0, path value1)
		{
			this._node = g.Rule.SelectSequence.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06004994 RID: 18836 RVA: 0x000E8133 File Offset: 0x000E6333
		public static implicit operator selectSequence(SelectSequence arg)
		{
			return selectSequence.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06004995 RID: 18837 RVA: 0x000E8141 File Offset: 0x000E6341
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06004996 RID: 18838 RVA: 0x000E8155 File Offset: 0x000E6355
		public path path
		{
			get
			{
				return path.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06004997 RID: 18839 RVA: 0x000E8169 File Offset: 0x000E6369
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004998 RID: 18840 RVA: 0x000E817C File Offset: 0x000E637C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004999 RID: 18841 RVA: 0x000E81A6 File Offset: 0x000E63A6
		public bool Equals(SelectSequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400215C RID: 8540
		private ProgramNode _node;
	}
}
