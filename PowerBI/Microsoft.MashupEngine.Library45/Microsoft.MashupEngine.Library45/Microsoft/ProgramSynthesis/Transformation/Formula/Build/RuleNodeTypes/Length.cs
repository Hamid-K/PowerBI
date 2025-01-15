using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001567 RID: 5479
	public struct Length : IProgramNodeBuilder, IEquatable<Length>
	{
		// Token: 0x17001F32 RID: 7986
		// (get) Token: 0x0600B31B RID: 45851 RVA: 0x00272E66 File Offset: 0x00271066
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B31C RID: 45852 RVA: 0x00272E6E File Offset: 0x0027106E
		private Length(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B31D RID: 45853 RVA: 0x00272E77 File Offset: 0x00271077
		public static Length CreateUnsafe(ProgramNode node)
		{
			return new Length(node);
		}

		// Token: 0x0600B31E RID: 45854 RVA: 0x00272E80 File Offset: 0x00271080
		public static Length? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Length)
			{
				return null;
			}
			return new Length?(Length.CreateUnsafe(node));
		}

		// Token: 0x0600B31F RID: 45855 RVA: 0x00272EB5 File Offset: 0x002710B5
		public Length(GrammarBuilders g, fromStr value0)
		{
			this._node = g.Rule.Length.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B320 RID: 45856 RVA: 0x00272ED4 File Offset: 0x002710D4
		public static implicit operator number(Length arg)
		{
			return number.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F33 RID: 7987
		// (get) Token: 0x0600B321 RID: 45857 RVA: 0x00272EE2 File Offset: 0x002710E2
		public fromStr fromStr
		{
			get
			{
				return fromStr.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B322 RID: 45858 RVA: 0x00272EF6 File Offset: 0x002710F6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B323 RID: 45859 RVA: 0x00272F0C File Offset: 0x0027110C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B324 RID: 45860 RVA: 0x00272F36 File Offset: 0x00271136
		public bool Equals(Length other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004615 RID: 17941
		private ProgramNode _node;
	}
}
