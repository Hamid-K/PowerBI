using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001576 RID: 5494
	public struct ParseNumber : IProgramNodeBuilder, IEquatable<ParseNumber>
	{
		// Token: 0x17001F57 RID: 8023
		// (get) Token: 0x0600B3B8 RID: 46008 RVA: 0x00273C6A File Offset: 0x00271E6A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B3B9 RID: 46009 RVA: 0x00273C72 File Offset: 0x00271E72
		private ParseNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B3BA RID: 46010 RVA: 0x00273C7B File Offset: 0x00271E7B
		public static ParseNumber CreateUnsafe(ProgramNode node)
		{
			return new ParseNumber(node);
		}

		// Token: 0x0600B3BB RID: 46011 RVA: 0x00273C84 File Offset: 0x00271E84
		public static ParseNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ParseNumber)
			{
				return null;
			}
			return new ParseNumber?(ParseNumber.CreateUnsafe(node));
		}

		// Token: 0x0600B3BC RID: 46012 RVA: 0x00273CB9 File Offset: 0x00271EB9
		public ParseNumber(GrammarBuilders g, parseSubject value0, locale value1)
		{
			this._node = g.Rule.ParseNumber.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B3BD RID: 46013 RVA: 0x00273CDF File Offset: 0x00271EDF
		public static implicit operator inumber(ParseNumber arg)
		{
			return inumber.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F58 RID: 8024
		// (get) Token: 0x0600B3BE RID: 46014 RVA: 0x00273CED File Offset: 0x00271EED
		public parseSubject parseSubject
		{
			get
			{
				return parseSubject.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F59 RID: 8025
		// (get) Token: 0x0600B3BF RID: 46015 RVA: 0x00273D01 File Offset: 0x00271F01
		public locale locale
		{
			get
			{
				return locale.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B3C0 RID: 46016 RVA: 0x00273D15 File Offset: 0x00271F15
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B3C1 RID: 46017 RVA: 0x00273D28 File Offset: 0x00271F28
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B3C2 RID: 46018 RVA: 0x00273D52 File Offset: 0x00271F52
		public bool Equals(ParseNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004624 RID: 17956
		private ProgramNode _node;
	}
}
