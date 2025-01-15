using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200100B RID: 4107
	public struct resultFields_singletonField : IProgramNodeBuilder, IEquatable<resultFields_singletonField>
	{
		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x060078FD RID: 30973 RVA: 0x0019FC3E File Offset: 0x0019DE3E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060078FE RID: 30974 RVA: 0x0019FC46 File Offset: 0x0019DE46
		private resultFields_singletonField(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060078FF RID: 30975 RVA: 0x0019FC4F File Offset: 0x0019DE4F
		public static resultFields_singletonField CreateUnsafe(ProgramNode node)
		{
			return new resultFields_singletonField(node);
		}

		// Token: 0x06007900 RID: 30976 RVA: 0x0019FC58 File Offset: 0x0019DE58
		public static resultFields_singletonField? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.resultFields_singletonField)
			{
				return null;
			}
			return new resultFields_singletonField?(resultFields_singletonField.CreateUnsafe(node));
		}

		// Token: 0x06007901 RID: 30977 RVA: 0x0019FC8D File Offset: 0x0019DE8D
		public resultFields_singletonField(GrammarBuilders g, singletonField value0)
		{
			this._node = g.UnnamedConversion.resultFields_singletonField.BuildASTNode(value0.Node);
		}

		// Token: 0x06007902 RID: 30978 RVA: 0x0019FCAC File Offset: 0x0019DEAC
		public static implicit operator resultFields(resultFields_singletonField arg)
		{
			return resultFields.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x06007903 RID: 30979 RVA: 0x0019FCBA File Offset: 0x0019DEBA
		public singletonField singletonField
		{
			get
			{
				return singletonField.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06007904 RID: 30980 RVA: 0x0019FCCE File Offset: 0x0019DECE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007905 RID: 30981 RVA: 0x0019FCE4 File Offset: 0x0019DEE4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007906 RID: 30982 RVA: 0x0019FD0E File Offset: 0x0019DF0E
		public bool Equals(resultFields_singletonField other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003324 RID: 13092
		private ProgramNode _node;
	}
}
