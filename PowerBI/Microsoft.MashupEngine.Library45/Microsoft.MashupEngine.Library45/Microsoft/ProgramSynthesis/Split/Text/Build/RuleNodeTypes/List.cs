using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x0200135B RID: 4955
	public struct List : IProgramNodeBuilder, IEquatable<List>
	{
		// Token: 0x17001A5E RID: 6750
		// (get) Token: 0x060098F9 RID: 39161 RVA: 0x002078A2 File Offset: 0x00205AA2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060098FA RID: 39162 RVA: 0x002078AA File Offset: 0x00205AAA
		private List(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060098FB RID: 39163 RVA: 0x002078B3 File Offset: 0x00205AB3
		public static List CreateUnsafe(ProgramNode node)
		{
			return new List(node);
		}

		// Token: 0x060098FC RID: 39164 RVA: 0x002078BC File Offset: 0x00205ABC
		public static List? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.List)
			{
				return null;
			}
			return new List?(List.CreateUnsafe(node));
		}

		// Token: 0x060098FD RID: 39165 RVA: 0x002078F1 File Offset: 0x00205AF1
		public List(GrammarBuilders g, v value0)
		{
			this._node = g.Rule.List.BuildASTNode(value0.Node);
		}

		// Token: 0x060098FE RID: 39166 RVA: 0x00207910 File Offset: 0x00205B10
		public static implicit operator output(List arg)
		{
			return output.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A5F RID: 6751
		// (get) Token: 0x060098FF RID: 39167 RVA: 0x0020791E File Offset: 0x00205B1E
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06009900 RID: 39168 RVA: 0x00207932 File Offset: 0x00205B32
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009901 RID: 39169 RVA: 0x00207948 File Offset: 0x00205B48
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009902 RID: 39170 RVA: 0x00207972 File Offset: 0x00205B72
		public bool Equals(List other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DD2 RID: 15826
		private ProgramNode _node;
	}
}
