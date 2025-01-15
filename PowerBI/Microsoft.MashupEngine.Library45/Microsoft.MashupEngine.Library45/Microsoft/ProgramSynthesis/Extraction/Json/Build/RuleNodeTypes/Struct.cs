using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes
{
	// Token: 0x02000B5A RID: 2906
	public struct Struct : IProgramNodeBuilder, IEquatable<Struct>
	{
		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06004945 RID: 18757 RVA: 0x000E7A1E File Offset: 0x000E5C1E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004946 RID: 18758 RVA: 0x000E7A26 File Offset: 0x000E5C26
		private Struct(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004947 RID: 18759 RVA: 0x000E7A2F File Offset: 0x000E5C2F
		public static Struct CreateUnsafe(ProgramNode node)
		{
			return new Struct(node);
		}

		// Token: 0x06004948 RID: 18760 RVA: 0x000E7A38 File Offset: 0x000E5C38
		public static Struct? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Struct)
			{
				return null;
			}
			return new Struct?(Struct.CreateUnsafe(node));
		}

		// Token: 0x06004949 RID: 18761 RVA: 0x000E7A6D File Offset: 0x000E5C6D
		public Struct(GrammarBuilders g, v value0, structBodyRec value1)
		{
			this._node = g.Rule.Struct.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600494A RID: 18762 RVA: 0x000E7A93 File Offset: 0x000E5C93
		public static implicit operator @struct(Struct arg)
		{
			return @struct.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x0600494B RID: 18763 RVA: 0x000E7AA1 File Offset: 0x000E5CA1
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x0600494C RID: 18764 RVA: 0x000E7AB5 File Offset: 0x000E5CB5
		public structBodyRec structBodyRec
		{
			get
			{
				return structBodyRec.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600494D RID: 18765 RVA: 0x000E7AC9 File Offset: 0x000E5CC9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600494E RID: 18766 RVA: 0x000E7ADC File Offset: 0x000E5CDC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600494F RID: 18767 RVA: 0x000E7B06 File Offset: 0x000E5D06
		public bool Equals(Struct other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002155 RID: 8533
		private ProgramNode _node;
	}
}
