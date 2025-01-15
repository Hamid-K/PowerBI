using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A2B RID: 6699
	public struct ConstKey : IProgramNodeBuilder, IEquatable<ConstKey>
	{
		// Token: 0x170024E0 RID: 9440
		// (get) Token: 0x0600DC1D RID: 56349 RVA: 0x002EE756 File Offset: 0x002EC956
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DC1E RID: 56350 RVA: 0x002EE75E File Offset: 0x002EC95E
		private ConstKey(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DC1F RID: 56351 RVA: 0x002EE767 File Offset: 0x002EC967
		public static ConstKey CreateUnsafe(ProgramNode node)
		{
			return new ConstKey(node);
		}

		// Token: 0x0600DC20 RID: 56352 RVA: 0x002EE770 File Offset: 0x002EC970
		public static ConstKey? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ConstKey)
			{
				return null;
			}
			return new ConstKey?(ConstKey.CreateUnsafe(node));
		}

		// Token: 0x0600DC21 RID: 56353 RVA: 0x002EE7A5 File Offset: 0x002EC9A5
		public ConstKey(GrammarBuilders g, str value0)
		{
			this._node = g.Rule.ConstKey.BuildASTNode(value0.Node);
		}

		// Token: 0x0600DC22 RID: 56354 RVA: 0x002EE7C4 File Offset: 0x002EC9C4
		public static implicit operator key(ConstKey arg)
		{
			return key.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024E1 RID: 9441
		// (get) Token: 0x0600DC23 RID: 56355 RVA: 0x002EE7D2 File Offset: 0x002EC9D2
		public str str
		{
			get
			{
				return str.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600DC24 RID: 56356 RVA: 0x002EE7E6 File Offset: 0x002EC9E6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DC25 RID: 56357 RVA: 0x002EE7FC File Offset: 0x002EC9FC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DC26 RID: 56358 RVA: 0x002EE826 File Offset: 0x002ECA26
		public bool Equals(ConstKey other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400541C RID: 21532
		private ProgramNode _node;
	}
}
