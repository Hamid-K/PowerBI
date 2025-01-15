using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200158F RID: 5519
	public struct FromNumberCoalesced : IProgramNodeBuilder, IEquatable<FromNumberCoalesced>
	{
		// Token: 0x17001FA4 RID: 8100
		// (get) Token: 0x0600B4CD RID: 46285 RVA: 0x00275576 File Offset: 0x00273776
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B4CE RID: 46286 RVA: 0x0027557E File Offset: 0x0027377E
		private FromNumberCoalesced(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B4CF RID: 46287 RVA: 0x00275587 File Offset: 0x00273787
		public static FromNumberCoalesced CreateUnsafe(ProgramNode node)
		{
			return new FromNumberCoalesced(node);
		}

		// Token: 0x0600B4D0 RID: 46288 RVA: 0x00275590 File Offset: 0x00273790
		public static FromNumberCoalesced? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FromNumberCoalesced)
			{
				return null;
			}
			return new FromNumberCoalesced?(FromNumberCoalesced.CreateUnsafe(node));
		}

		// Token: 0x0600B4D1 RID: 46289 RVA: 0x002755C5 File Offset: 0x002737C5
		public FromNumberCoalesced(GrammarBuilders g, row value0, columnName value1)
		{
			this._node = g.Rule.FromNumberCoalesced.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B4D2 RID: 46290 RVA: 0x002755EB File Offset: 0x002737EB
		public static implicit operator fromNumberCoalesced(FromNumberCoalesced arg)
		{
			return fromNumberCoalesced.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001FA5 RID: 8101
		// (get) Token: 0x0600B4D3 RID: 46291 RVA: 0x002755F9 File Offset: 0x002737F9
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001FA6 RID: 8102
		// (get) Token: 0x0600B4D4 RID: 46292 RVA: 0x0027560D File Offset: 0x0027380D
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B4D5 RID: 46293 RVA: 0x00275621 File Offset: 0x00273821
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B4D6 RID: 46294 RVA: 0x00275634 File Offset: 0x00273834
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B4D7 RID: 46295 RVA: 0x0027565E File Offset: 0x0027385E
		public bool Equals(FromNumberCoalesced other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400463D RID: 17981
		private ProgramNode _node;
	}
}
