using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200157A RID: 5498
	public struct ParseDateTime : IProgramNodeBuilder, IEquatable<ParseDateTime>
	{
		// Token: 0x17001F63 RID: 8035
		// (get) Token: 0x0600B3E4 RID: 46052 RVA: 0x0027405A File Offset: 0x0027225A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B3E5 RID: 46053 RVA: 0x00274062 File Offset: 0x00272262
		private ParseDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B3E6 RID: 46054 RVA: 0x0027406B File Offset: 0x0027226B
		public static ParseDateTime CreateUnsafe(ProgramNode node)
		{
			return new ParseDateTime(node);
		}

		// Token: 0x0600B3E7 RID: 46055 RVA: 0x00274074 File Offset: 0x00272274
		public static ParseDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ParseDateTime)
			{
				return null;
			}
			return new ParseDateTime?(ParseDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600B3E8 RID: 46056 RVA: 0x002740A9 File Offset: 0x002722A9
		public ParseDateTime(GrammarBuilders g, parseSubject value0, dateTimeParseDesc value1)
		{
			this._node = g.Rule.ParseDateTime.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B3E9 RID: 46057 RVA: 0x002740CF File Offset: 0x002722CF
		public static implicit operator idate(ParseDateTime arg)
		{
			return idate.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F64 RID: 8036
		// (get) Token: 0x0600B3EA RID: 46058 RVA: 0x002740DD File Offset: 0x002722DD
		public parseSubject parseSubject
		{
			get
			{
				return parseSubject.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F65 RID: 8037
		// (get) Token: 0x0600B3EB RID: 46059 RVA: 0x002740F1 File Offset: 0x002722F1
		public dateTimeParseDesc dateTimeParseDesc
		{
			get
			{
				return dateTimeParseDesc.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B3EC RID: 46060 RVA: 0x00274105 File Offset: 0x00272305
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B3ED RID: 46061 RVA: 0x00274118 File Offset: 0x00272318
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B3EE RID: 46062 RVA: 0x00274142 File Offset: 0x00272342
		public bool Equals(ParseDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004628 RID: 17960
		private ProgramNode _node;
	}
}
