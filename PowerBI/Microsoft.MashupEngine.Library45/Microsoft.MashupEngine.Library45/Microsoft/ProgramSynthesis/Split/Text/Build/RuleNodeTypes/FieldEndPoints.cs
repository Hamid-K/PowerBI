using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x02001348 RID: 4936
	public struct FieldEndPoints : IProgramNodeBuilder, IEquatable<FieldEndPoints>
	{
		// Token: 0x17001A24 RID: 6692
		// (get) Token: 0x06009827 RID: 38951 RVA: 0x002065C6 File Offset: 0x002047C6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009828 RID: 38952 RVA: 0x002065CE File Offset: 0x002047CE
		private FieldEndPoints(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009829 RID: 38953 RVA: 0x002065D7 File Offset: 0x002047D7
		public static FieldEndPoints CreateUnsafe(ProgramNode node)
		{
			return new FieldEndPoints(node);
		}

		// Token: 0x0600982A RID: 38954 RVA: 0x002065E0 File Offset: 0x002047E0
		public static FieldEndPoints? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FieldEndPoints)
			{
				return null;
			}
			return new FieldEndPoints?(FieldEndPoints.CreateUnsafe(node));
		}

		// Token: 0x0600982B RID: 38955 RVA: 0x00206615 File Offset: 0x00204815
		public FieldEndPoints(GrammarBuilders g, fieldMatch value0)
		{
			this._node = g.Rule.FieldEndPoints.BuildASTNode(value0.Node);
		}

		// Token: 0x0600982C RID: 38956 RVA: 0x00206634 File Offset: 0x00204834
		public static implicit operator d(FieldEndPoints arg)
		{
			return d.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A25 RID: 6693
		// (get) Token: 0x0600982D RID: 38957 RVA: 0x00206642 File Offset: 0x00204842
		public fieldMatch fieldMatch
		{
			get
			{
				return fieldMatch.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600982E RID: 38958 RVA: 0x00206656 File Offset: 0x00204856
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600982F RID: 38959 RVA: 0x0020666C File Offset: 0x0020486C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009830 RID: 38960 RVA: 0x00206696 File Offset: 0x00204896
		public bool Equals(FieldEndPoints other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DBF RID: 15807
		private ProgramNode _node;
	}
}
