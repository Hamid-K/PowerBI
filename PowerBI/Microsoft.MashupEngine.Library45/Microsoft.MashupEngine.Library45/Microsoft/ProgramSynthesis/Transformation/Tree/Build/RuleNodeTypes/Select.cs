using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E76 RID: 7798
	public struct Select : IProgramNodeBuilder, IEquatable<Select>
	{
		// Token: 0x17002BC7 RID: 11207
		// (get) Token: 0x06010704 RID: 67332 RVA: 0x0038A8A2 File Offset: 0x00388AA2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06010705 RID: 67333 RVA: 0x0038A8AA File Offset: 0x00388AAA
		private Select(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010706 RID: 67334 RVA: 0x0038A8B3 File Offset: 0x00388AB3
		public static Select CreateUnsafe(ProgramNode node)
		{
			return new Select(node);
		}

		// Token: 0x06010707 RID: 67335 RVA: 0x0038A8BC File Offset: 0x00388ABC
		public static Select? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Select)
			{
				return null;
			}
			return new Select?(Select.CreateUnsafe(node));
		}

		// Token: 0x06010708 RID: 67336 RVA: 0x0038A8F1 File Offset: 0x00388AF1
		public Select(GrammarBuilders g, tmpFilter value0, k value1)
		{
			this._node = g.Rule.Select.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x06010709 RID: 67337 RVA: 0x0038A923 File Offset: 0x00388B23
		public static implicit operator select(Select arg)
		{
			return select.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002BC8 RID: 11208
		// (get) Token: 0x0601070A RID: 67338 RVA: 0x0038A931 File Offset: 0x00388B31
		public tmpFilter tmpFilter
		{
			get
			{
				return tmpFilter.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002BC9 RID: 11209
		// (get) Token: 0x0601070B RID: 67339 RVA: 0x0038A945 File Offset: 0x00388B45
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0601070C RID: 67340 RVA: 0x0038A959 File Offset: 0x00388B59
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0601070D RID: 67341 RVA: 0x0038A96C File Offset: 0x00388B6C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0601070E RID: 67342 RVA: 0x0038A996 File Offset: 0x00388B96
		public bool Equals(Select other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062B5 RID: 25269
		private ProgramNode _node;
	}
}
