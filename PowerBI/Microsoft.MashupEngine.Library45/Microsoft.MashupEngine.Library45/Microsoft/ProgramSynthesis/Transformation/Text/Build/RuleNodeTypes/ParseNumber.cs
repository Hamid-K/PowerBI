using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C1E RID: 7198
	public struct ParseNumber : IProgramNodeBuilder, IEquatable<ParseNumber>
	{
		// Token: 0x17002885 RID: 10373
		// (get) Token: 0x0600F254 RID: 62036 RVA: 0x00340DEA File Offset: 0x0033EFEA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F255 RID: 62037 RVA: 0x00340DF2 File Offset: 0x0033EFF2
		private ParseNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F256 RID: 62038 RVA: 0x00340DFB File Offset: 0x0033EFFB
		public static ParseNumber CreateUnsafe(ProgramNode node)
		{
			return new ParseNumber(node);
		}

		// Token: 0x0600F257 RID: 62039 RVA: 0x00340E04 File Offset: 0x0033F004
		public static ParseNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ParseNumber)
			{
				return null;
			}
			return new ParseNumber?(ParseNumber.CreateUnsafe(node));
		}

		// Token: 0x0600F258 RID: 62040 RVA: 0x00340E39 File Offset: 0x0033F039
		public ParseNumber(GrammarBuilders g, SS value0, numberFormatDetails value1)
		{
			this._node = g.Rule.ParseNumber.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F259 RID: 62041 RVA: 0x00340E5F File Offset: 0x0033F05F
		public static implicit operator parsedNumber(ParseNumber arg)
		{
			return parsedNumber.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002886 RID: 10374
		// (get) Token: 0x0600F25A RID: 62042 RVA: 0x00340E6D File Offset: 0x0033F06D
		public SS SS
		{
			get
			{
				return SS.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002887 RID: 10375
		// (get) Token: 0x0600F25B RID: 62043 RVA: 0x00340E81 File Offset: 0x0033F081
		public numberFormatDetails numberFormatDetails
		{
			get
			{
				return numberFormatDetails.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F25C RID: 62044 RVA: 0x00340E95 File Offset: 0x0033F095
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F25D RID: 62045 RVA: 0x00340EA8 File Offset: 0x0033F0A8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F25E RID: 62046 RVA: 0x00340ED2 File Offset: 0x0033F0D2
		public bool Equals(ParseNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B0D RID: 23309
		private ProgramNode _node;
	}
}
