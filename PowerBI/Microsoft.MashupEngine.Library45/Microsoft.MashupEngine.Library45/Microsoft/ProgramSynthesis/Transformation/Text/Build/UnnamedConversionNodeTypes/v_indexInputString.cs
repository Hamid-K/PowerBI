using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001BF0 RID: 7152
	public struct v_indexInputString : IProgramNodeBuilder, IEquatable<v_indexInputString>
	{
		// Token: 0x17002802 RID: 10242
		// (get) Token: 0x0600F061 RID: 61537 RVA: 0x0033E0DE File Offset: 0x0033C2DE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F062 RID: 61538 RVA: 0x0033E0E6 File Offset: 0x0033C2E6
		private v_indexInputString(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F063 RID: 61539 RVA: 0x0033E0EF File Offset: 0x0033C2EF
		public static v_indexInputString CreateUnsafe(ProgramNode node)
		{
			return new v_indexInputString(node);
		}

		// Token: 0x0600F064 RID: 61540 RVA: 0x0033E0F8 File Offset: 0x0033C2F8
		public static v_indexInputString? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.v_indexInputString)
			{
				return null;
			}
			return new v_indexInputString?(v_indexInputString.CreateUnsafe(node));
		}

		// Token: 0x0600F065 RID: 61541 RVA: 0x0033E12D File Offset: 0x0033C32D
		public v_indexInputString(GrammarBuilders g, indexInputString value0)
		{
			this._node = g.UnnamedConversion.v_indexInputString.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F066 RID: 61542 RVA: 0x0033E14C File Offset: 0x0033C34C
		public static implicit operator v(v_indexInputString arg)
		{
			return v.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002803 RID: 10243
		// (get) Token: 0x0600F067 RID: 61543 RVA: 0x0033E15A File Offset: 0x0033C35A
		public indexInputString indexInputString
		{
			get
			{
				return indexInputString.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F068 RID: 61544 RVA: 0x0033E16E File Offset: 0x0033C36E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F069 RID: 61545 RVA: 0x0033E184 File Offset: 0x0033C384
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F06A RID: 61546 RVA: 0x0033E1AE File Offset: 0x0033C3AE
		public bool Equals(v_indexInputString other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005ADF RID: 23263
		private ProgramNode _node;
	}
}
