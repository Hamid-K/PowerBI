using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes
{
	// Token: 0x02000B5B RID: 2907
	public struct Field : IProgramNodeBuilder, IEquatable<Field>
	{
		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06004950 RID: 18768 RVA: 0x000E7B1A File Offset: 0x000E5D1A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004951 RID: 18769 RVA: 0x000E7B22 File Offset: 0x000E5D22
		private Field(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004952 RID: 18770 RVA: 0x000E7B2B File Offset: 0x000E5D2B
		public static Field CreateUnsafe(ProgramNode node)
		{
			return new Field(node);
		}

		// Token: 0x06004953 RID: 18771 RVA: 0x000E7B34 File Offset: 0x000E5D34
		public static Field? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Field)
			{
				return null;
			}
			return new Field?(Field.CreateUnsafe(node));
		}

		// Token: 0x06004954 RID: 18772 RVA: 0x000E7B69 File Offset: 0x000E5D69
		public Field(GrammarBuilders g, v value0, id value1, selectRegion value2)
		{
			this._node = g.Rule.Field.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06004955 RID: 18773 RVA: 0x000E7B96 File Offset: 0x000E5D96
		public static implicit operator @struct(Field arg)
		{
			return @struct.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06004956 RID: 18774 RVA: 0x000E7BA4 File Offset: 0x000E5DA4
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06004957 RID: 18775 RVA: 0x000E7BB8 File Offset: 0x000E5DB8
		public id id
		{
			get
			{
				return id.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06004958 RID: 18776 RVA: 0x000E7BCC File Offset: 0x000E5DCC
		public selectRegion selectRegion
		{
			get
			{
				return selectRegion.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06004959 RID: 18777 RVA: 0x000E7BE0 File Offset: 0x000E5DE0
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600495A RID: 18778 RVA: 0x000E7BF4 File Offset: 0x000E5DF4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600495B RID: 18779 RVA: 0x000E7C1E File Offset: 0x000E5E1E
		public bool Equals(Field other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002156 RID: 8534
		private ProgramNode _node;
	}
}
