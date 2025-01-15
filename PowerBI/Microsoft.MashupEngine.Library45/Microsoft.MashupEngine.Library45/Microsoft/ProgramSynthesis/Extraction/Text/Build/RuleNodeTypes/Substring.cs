using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes
{
	// Token: 0x02000F29 RID: 3881
	public struct Substring : IProgramNodeBuilder, IEquatable<Substring>
	{
		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x06006B64 RID: 27492 RVA: 0x00160FFA File Offset: 0x0015F1FA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006B65 RID: 27493 RVA: 0x00161002 File Offset: 0x0015F202
		private Substring(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006B66 RID: 27494 RVA: 0x0016100B File Offset: 0x0015F20B
		public static Substring CreateUnsafe(ProgramNode node)
		{
			return new Substring(node);
		}

		// Token: 0x06006B67 RID: 27495 RVA: 0x00161014 File Offset: 0x0015F214
		public static Substring? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Substring)
			{
				return null;
			}
			return new Substring?(Substring.CreateUnsafe(node));
		}

		// Token: 0x06006B68 RID: 27496 RVA: 0x00161049 File Offset: 0x0015F249
		public Substring(GrammarBuilders g, row value0, k value1, k value2)
		{
			this._node = g.Rule.Substring.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06006B69 RID: 27497 RVA: 0x00161076 File Offset: 0x0015F276
		public static implicit operator extract(Substring arg)
		{
			return extract.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x06006B6A RID: 27498 RVA: 0x00161084 File Offset: 0x0015F284
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x06006B6B RID: 27499 RVA: 0x00161098 File Offset: 0x0015F298
		public k k1
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x06006B6C RID: 27500 RVA: 0x001610AC File Offset: 0x0015F2AC
		public k k2
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06006B6D RID: 27501 RVA: 0x001610C0 File Offset: 0x0015F2C0
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006B6E RID: 27502 RVA: 0x001610D4 File Offset: 0x0015F2D4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006B6F RID: 27503 RVA: 0x001610FE File Offset: 0x0015F2FE
		public bool Equals(Substring other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F14 RID: 12052
		private ProgramNode _node;
	}
}
