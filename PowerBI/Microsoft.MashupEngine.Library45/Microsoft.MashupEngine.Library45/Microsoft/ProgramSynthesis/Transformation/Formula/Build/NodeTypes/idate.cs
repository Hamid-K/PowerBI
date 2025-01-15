using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015B8 RID: 5560
	public struct idate : IProgramNodeBuilder, IEquatable<idate>
	{
		// Token: 0x17001FDE RID: 8158
		// (get) Token: 0x0600B7CC RID: 47052 RVA: 0x0027DA7E File Offset: 0x0027BC7E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B7CD RID: 47053 RVA: 0x0027DA86 File Offset: 0x0027BC86
		private idate(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B7CE RID: 47054 RVA: 0x0027DA8F File Offset: 0x0027BC8F
		public static idate CreateUnsafe(ProgramNode node)
		{
			return new idate(node);
		}

		// Token: 0x0600B7CF RID: 47055 RVA: 0x0027DA98 File Offset: 0x0027BC98
		public static idate? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.idate)
			{
				return null;
			}
			return new idate?(idate.CreateUnsafe(node));
		}

		// Token: 0x0600B7D0 RID: 47056 RVA: 0x0027DAD2 File Offset: 0x0027BCD2
		public static idate CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new idate(new Hole(g.Symbol.idate, holeId));
		}

		// Token: 0x0600B7D1 RID: 47057 RVA: 0x0027DAEA File Offset: 0x0027BCEA
		public bool Is_idate_fromDateTime(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.idate_fromDateTime;
		}

		// Token: 0x0600B7D2 RID: 47058 RVA: 0x0027DB04 File Offset: 0x0027BD04
		public bool Is_idate_fromDateTime(GrammarBuilders g, out idate_fromDateTime value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.idate_fromDateTime)
			{
				value = idate_fromDateTime.CreateUnsafe(this.Node);
				return true;
			}
			value = default(idate_fromDateTime);
			return false;
		}

		// Token: 0x0600B7D3 RID: 47059 RVA: 0x0027DB3C File Offset: 0x0027BD3C
		public idate_fromDateTime? As_idate_fromDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.idate_fromDateTime)
			{
				return null;
			}
			return new idate_fromDateTime?(idate_fromDateTime.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B7D4 RID: 47060 RVA: 0x0027DB7C File Offset: 0x0027BD7C
		public idate_fromDateTime Cast_idate_fromDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.idate_fromDateTime)
			{
				return idate_fromDateTime.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_idate_fromDateTime is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B7D5 RID: 47061 RVA: 0x0027DBD1 File Offset: 0x0027BDD1
		public bool Is_idate_fromDateTimePart(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.idate_fromDateTimePart;
		}

		// Token: 0x0600B7D6 RID: 47062 RVA: 0x0027DBEB File Offset: 0x0027BDEB
		public bool Is_idate_fromDateTimePart(GrammarBuilders g, out idate_fromDateTimePart value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.idate_fromDateTimePart)
			{
				value = idate_fromDateTimePart.CreateUnsafe(this.Node);
				return true;
			}
			value = default(idate_fromDateTimePart);
			return false;
		}

		// Token: 0x0600B7D7 RID: 47063 RVA: 0x0027DC20 File Offset: 0x0027BE20
		public idate_fromDateTimePart? As_idate_fromDateTimePart(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.idate_fromDateTimePart)
			{
				return null;
			}
			return new idate_fromDateTimePart?(idate_fromDateTimePart.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B7D8 RID: 47064 RVA: 0x0027DC60 File Offset: 0x0027BE60
		public idate_fromDateTimePart Cast_idate_fromDateTimePart(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.idate_fromDateTimePart)
			{
				return idate_fromDateTimePart.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_idate_fromDateTimePart is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B7D9 RID: 47065 RVA: 0x0027DCB5 File Offset: 0x0027BEB5
		public bool Is_ParseDateTime(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ParseDateTime;
		}

		// Token: 0x0600B7DA RID: 47066 RVA: 0x0027DCCF File Offset: 0x0027BECF
		public bool Is_ParseDateTime(GrammarBuilders g, out ParseDateTime value)
		{
			if (this.Node.GrammarRule == g.Rule.ParseDateTime)
			{
				value = ParseDateTime.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ParseDateTime);
			return false;
		}

		// Token: 0x0600B7DB RID: 47067 RVA: 0x0027DD04 File Offset: 0x0027BF04
		public ParseDateTime? As_ParseDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ParseDateTime)
			{
				return null;
			}
			return new ParseDateTime?(ParseDateTime.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B7DC RID: 47068 RVA: 0x0027DD44 File Offset: 0x0027BF44
		public ParseDateTime Cast_ParseDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ParseDateTime)
			{
				return ParseDateTime.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ParseDateTime is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B7DD RID: 47069 RVA: 0x0027DD9C File Offset: 0x0027BF9C
		public T Switch<T>(GrammarBuilders g, Func<idate_fromDateTime, T> func0, Func<idate_fromDateTimePart, T> func1, Func<ParseDateTime, T> func2)
		{
			idate_fromDateTime idate_fromDateTime;
			if (this.Is_idate_fromDateTime(g, out idate_fromDateTime))
			{
				return func0(idate_fromDateTime);
			}
			idate_fromDateTimePart idate_fromDateTimePart;
			if (this.Is_idate_fromDateTimePart(g, out idate_fromDateTimePart))
			{
				return func1(idate_fromDateTimePart);
			}
			ParseDateTime parseDateTime;
			if (this.Is_ParseDateTime(g, out parseDateTime))
			{
				return func2(parseDateTime);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol idate");
		}

		// Token: 0x0600B7DE RID: 47070 RVA: 0x0027DE08 File Offset: 0x0027C008
		public void Switch(GrammarBuilders g, Action<idate_fromDateTime> func0, Action<idate_fromDateTimePart> func1, Action<ParseDateTime> func2)
		{
			idate_fromDateTime idate_fromDateTime;
			if (this.Is_idate_fromDateTime(g, out idate_fromDateTime))
			{
				func0(idate_fromDateTime);
				return;
			}
			idate_fromDateTimePart idate_fromDateTimePart;
			if (this.Is_idate_fromDateTimePart(g, out idate_fromDateTimePart))
			{
				func1(idate_fromDateTimePart);
				return;
			}
			ParseDateTime parseDateTime;
			if (this.Is_ParseDateTime(g, out parseDateTime))
			{
				func2(parseDateTime);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol idate");
		}

		// Token: 0x0600B7DF RID: 47071 RVA: 0x0027DE73 File Offset: 0x0027C073
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B7E0 RID: 47072 RVA: 0x0027DE88 File Offset: 0x0027C088
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B7E1 RID: 47073 RVA: 0x0027DEB2 File Offset: 0x0027C0B2
		public bool Equals(idate other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004666 RID: 18022
		private ProgramNode _node;
	}
}
