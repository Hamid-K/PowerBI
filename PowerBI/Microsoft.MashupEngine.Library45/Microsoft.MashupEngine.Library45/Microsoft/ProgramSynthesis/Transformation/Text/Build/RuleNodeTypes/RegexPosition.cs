using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C1B RID: 7195
	public struct RegexPosition : IProgramNodeBuilder, IEquatable<RegexPosition>
	{
		// Token: 0x1700287C RID: 10364
		// (get) Token: 0x0600F233 RID: 62003 RVA: 0x00340AF2 File Offset: 0x0033ECF2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F234 RID: 62004 RVA: 0x00340AFA File Offset: 0x0033ECFA
		private RegexPosition(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F235 RID: 62005 RVA: 0x00340B03 File Offset: 0x0033ED03
		[Obsolete("The RegexPosition rule is marked as @deprecated in the DSL grammar.")]
		public static RegexPosition CreateUnsafe(ProgramNode node)
		{
			return new RegexPosition(node);
		}

		// Token: 0x0600F236 RID: 62006 RVA: 0x00340B0C File Offset: 0x0033ED0C
		[Obsolete("The RegexPosition rule is marked as @deprecated in the DSL grammar.")]
		public static RegexPosition? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RegexPosition)
			{
				return null;
			}
			return new RegexPosition?(RegexPosition.CreateUnsafe(node));
		}

		// Token: 0x0600F237 RID: 62007 RVA: 0x00340B41 File Offset: 0x0033ED41
		[Obsolete("The RegexPosition rule is marked as @deprecated in the DSL grammar.")]
		public RegexPosition(GrammarBuilders g, x value0, regexPair value1, k value2)
		{
			this._node = g.Rule.RegexPosition.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600F238 RID: 62008 RVA: 0x00340B6E File Offset: 0x0033ED6E
		public static implicit operator pos(RegexPosition arg)
		{
			return pos.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700287D RID: 10365
		// (get) Token: 0x0600F239 RID: 62009 RVA: 0x00340B7C File Offset: 0x0033ED7C
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700287E RID: 10366
		// (get) Token: 0x0600F23A RID: 62010 RVA: 0x00340B90 File Offset: 0x0033ED90
		public regexPair regexPair
		{
			get
			{
				return regexPair.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x1700287F RID: 10367
		// (get) Token: 0x0600F23B RID: 62011 RVA: 0x00340BA4 File Offset: 0x0033EDA4
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600F23C RID: 62012 RVA: 0x00340BB8 File Offset: 0x0033EDB8
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F23D RID: 62013 RVA: 0x00340BCC File Offset: 0x0033EDCC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F23E RID: 62014 RVA: 0x00340BF6 File Offset: 0x0033EDF6
		public bool Equals(RegexPosition other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B0A RID: 23306
		private ProgramNode _node;
	}
}
