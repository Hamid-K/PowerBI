using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes
{
	// Token: 0x02000F2D RID: 3885
	public struct Select : IProgramNodeBuilder, IEquatable<Select>
	{
		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x06006B93 RID: 27539 RVA: 0x0016143E File Offset: 0x0015F63E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006B94 RID: 27540 RVA: 0x00161446 File Offset: 0x0015F646
		private Select(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006B95 RID: 27541 RVA: 0x0016144F File Offset: 0x0015F64F
		public static Select CreateUnsafe(ProgramNode node)
		{
			return new Select(node);
		}

		// Token: 0x06006B96 RID: 27542 RVA: 0x00161458 File Offset: 0x0015F658
		public static Select? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Select)
			{
				return null;
			}
			return new Select?(Select.CreateUnsafe(node));
		}

		// Token: 0x06006B97 RID: 27543 RVA: 0x0016148D File Offset: 0x0015F68D
		public Select(GrammarBuilders g, re value0, skip value1)
		{
			this._node = g.Rule.Select.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06006B98 RID: 27544 RVA: 0x001614B3 File Offset: 0x0015F6B3
		public static implicit operator records(Select arg)
		{
			return records.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x06006B99 RID: 27545 RVA: 0x001614C1 File Offset: 0x0015F6C1
		public re re
		{
			get
			{
				return re.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x06006B9A RID: 27546 RVA: 0x001614D5 File Offset: 0x0015F6D5
		public skip skip
		{
			get
			{
				return skip.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06006B9B RID: 27547 RVA: 0x001614E9 File Offset: 0x0015F6E9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006B9C RID: 27548 RVA: 0x001614FC File Offset: 0x0015F6FC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006B9D RID: 27549 RVA: 0x00161526 File Offset: 0x0015F726
		public bool Equals(Select other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F18 RID: 12056
		private ProgramNode _node;
	}
}
