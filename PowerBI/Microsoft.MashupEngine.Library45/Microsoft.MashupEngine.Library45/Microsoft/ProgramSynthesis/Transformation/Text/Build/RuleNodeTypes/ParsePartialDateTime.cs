using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C12 RID: 7186
	public struct ParsePartialDateTime : IProgramNodeBuilder, IEquatable<ParsePartialDateTime>
	{
		// Token: 0x1700285E RID: 10334
		// (get) Token: 0x0600F1CD RID: 61901 RVA: 0x003401C2 File Offset: 0x0033E3C2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F1CE RID: 61902 RVA: 0x003401CA File Offset: 0x0033E3CA
		private ParsePartialDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F1CF RID: 61903 RVA: 0x003401D3 File Offset: 0x0033E3D3
		public static ParsePartialDateTime CreateUnsafe(ProgramNode node)
		{
			return new ParsePartialDateTime(node);
		}

		// Token: 0x0600F1D0 RID: 61904 RVA: 0x003401DC File Offset: 0x0033E3DC
		public static ParsePartialDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ParsePartialDateTime)
			{
				return null;
			}
			return new ParsePartialDateTime?(ParsePartialDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600F1D1 RID: 61905 RVA: 0x00340211 File Offset: 0x0033E411
		public ParsePartialDateTime(GrammarBuilders g, SS value0, inputDtFormats value1)
		{
			this._node = g.Rule.ParsePartialDateTime.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F1D2 RID: 61906 RVA: 0x00340237 File Offset: 0x0033E437
		public static implicit operator parsedDateTime(ParsePartialDateTime arg)
		{
			return parsedDateTime.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700285F RID: 10335
		// (get) Token: 0x0600F1D3 RID: 61907 RVA: 0x00340245 File Offset: 0x0033E445
		public SS SS
		{
			get
			{
				return SS.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002860 RID: 10336
		// (get) Token: 0x0600F1D4 RID: 61908 RVA: 0x00340259 File Offset: 0x0033E459
		public inputDtFormats inputDtFormats
		{
			get
			{
				return inputDtFormats.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F1D5 RID: 61909 RVA: 0x0034026D File Offset: 0x0033E46D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F1D6 RID: 61910 RVA: 0x00340280 File Offset: 0x0033E480
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F1D7 RID: 61911 RVA: 0x003402AA File Offset: 0x0033E4AA
		public bool Equals(ParsePartialDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B01 RID: 23297
		private ProgramNode _node;
	}
}
