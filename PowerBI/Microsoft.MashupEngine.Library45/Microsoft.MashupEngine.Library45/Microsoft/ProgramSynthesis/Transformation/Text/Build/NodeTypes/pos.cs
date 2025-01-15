using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C4D RID: 7245
	public struct pos : IProgramNodeBuilder, IEquatable<pos>
	{
		// Token: 0x170028E3 RID: 10467
		// (get) Token: 0x0600F4F2 RID: 62706 RVA: 0x0034693E File Offset: 0x00344B3E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F4F3 RID: 62707 RVA: 0x00346946 File Offset: 0x00344B46
		private pos(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F4F4 RID: 62708 RVA: 0x0034694F File Offset: 0x00344B4F
		public static pos CreateUnsafe(ProgramNode node)
		{
			return new pos(node);
		}

		// Token: 0x0600F4F5 RID: 62709 RVA: 0x00346958 File Offset: 0x00344B58
		public static pos? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.pos)
			{
				return null;
			}
			return new pos?(pos.CreateUnsafe(node));
		}

		// Token: 0x0600F4F6 RID: 62710 RVA: 0x00346992 File Offset: 0x00344B92
		public static pos CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new pos(new Hole(g.Symbol.pos, holeId));
		}

		// Token: 0x0600F4F7 RID: 62711 RVA: 0x003469AA File Offset: 0x00344BAA
		public bool Is_RelativePosition(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.RelativePosition;
		}

		// Token: 0x0600F4F8 RID: 62712 RVA: 0x003469C4 File Offset: 0x00344BC4
		public bool Is_RelativePosition(GrammarBuilders g, out RelativePosition value)
		{
			if (this.Node.GrammarRule == g.Rule.RelativePosition)
			{
				value = RelativePosition.CreateUnsafe(this.Node);
				return true;
			}
			value = default(RelativePosition);
			return false;
		}

		// Token: 0x0600F4F9 RID: 62713 RVA: 0x003469FC File Offset: 0x00344BFC
		public RelativePosition? As_RelativePosition(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.RelativePosition)
			{
				return null;
			}
			return new RelativePosition?(RelativePosition.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F4FA RID: 62714 RVA: 0x00346A3C File Offset: 0x00344C3C
		public RelativePosition Cast_RelativePosition(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.RelativePosition)
			{
				return RelativePosition.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_RelativePosition is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F4FB RID: 62715 RVA: 0x00346A91 File Offset: 0x00344C91
		public bool Is_RegexPositionRelative(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.RegexPositionRelative;
		}

		// Token: 0x0600F4FC RID: 62716 RVA: 0x00346AAB File Offset: 0x00344CAB
		public bool Is_RegexPositionRelative(GrammarBuilders g, out RegexPositionRelative value)
		{
			if (this.Node.GrammarRule == g.Rule.RegexPositionRelative)
			{
				value = RegexPositionRelative.CreateUnsafe(this.Node);
				return true;
			}
			value = default(RegexPositionRelative);
			return false;
		}

		// Token: 0x0600F4FD RID: 62717 RVA: 0x00346AE0 File Offset: 0x00344CE0
		public RegexPositionRelative? As_RegexPositionRelative(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.RegexPositionRelative)
			{
				return null;
			}
			return new RegexPositionRelative?(RegexPositionRelative.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F4FE RID: 62718 RVA: 0x00346B20 File Offset: 0x00344D20
		public RegexPositionRelative Cast_RegexPositionRelative(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.RegexPositionRelative)
			{
				return RegexPositionRelative.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_RegexPositionRelative is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F4FF RID: 62719 RVA: 0x00346B75 File Offset: 0x00344D75
		public bool Is_AbsolutePosition(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.AbsolutePosition;
		}

		// Token: 0x0600F500 RID: 62720 RVA: 0x00346B8F File Offset: 0x00344D8F
		public bool Is_AbsolutePosition(GrammarBuilders g, out AbsolutePosition value)
		{
			if (this.Node.GrammarRule == g.Rule.AbsolutePosition)
			{
				value = AbsolutePosition.CreateUnsafe(this.Node);
				return true;
			}
			value = default(AbsolutePosition);
			return false;
		}

		// Token: 0x0600F501 RID: 62721 RVA: 0x00346BC4 File Offset: 0x00344DC4
		public AbsolutePosition? As_AbsolutePosition(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.AbsolutePosition)
			{
				return null;
			}
			return new AbsolutePosition?(AbsolutePosition.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F502 RID: 62722 RVA: 0x00346C04 File Offset: 0x00344E04
		public AbsolutePosition Cast_AbsolutePosition(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.AbsolutePosition)
			{
				return AbsolutePosition.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_AbsolutePosition is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F503 RID: 62723 RVA: 0x00346C59 File Offset: 0x00344E59
		public bool Is_RegexPosition(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.RegexPosition;
		}

		// Token: 0x0600F504 RID: 62724 RVA: 0x00346C73 File Offset: 0x00344E73
		public bool Is_RegexPosition(GrammarBuilders g, out RegexPosition value)
		{
			if (this.Node.GrammarRule == g.Rule.RegexPosition)
			{
				value = RegexPosition.CreateUnsafe(this.Node);
				return true;
			}
			value = default(RegexPosition);
			return false;
		}

		// Token: 0x0600F505 RID: 62725 RVA: 0x00346CA8 File Offset: 0x00344EA8
		public RegexPosition? As_RegexPosition(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.RegexPosition)
			{
				return null;
			}
			return new RegexPosition?(RegexPosition.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F506 RID: 62726 RVA: 0x00346CE8 File Offset: 0x00344EE8
		public RegexPosition Cast_RegexPosition(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.RegexPosition)
			{
				return RegexPosition.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_RegexPosition is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F507 RID: 62727 RVA: 0x00346D40 File Offset: 0x00344F40
		public T Switch<T>(GrammarBuilders g, Func<RelativePosition, T> func0, Func<RegexPositionRelative, T> func1, Func<AbsolutePosition, T> func2, Func<RegexPosition, T> func3)
		{
			RelativePosition relativePosition;
			if (this.Is_RelativePosition(g, out relativePosition))
			{
				return func0(relativePosition);
			}
			RegexPositionRelative regexPositionRelative;
			if (this.Is_RegexPositionRelative(g, out regexPositionRelative))
			{
				return func1(regexPositionRelative);
			}
			AbsolutePosition absolutePosition;
			if (this.Is_AbsolutePosition(g, out absolutePosition))
			{
				return func2(absolutePosition);
			}
			RegexPosition regexPosition;
			if (this.Is_RegexPosition(g, out regexPosition))
			{
				return func3(regexPosition);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol pos");
		}

		// Token: 0x0600F508 RID: 62728 RVA: 0x00346DC0 File Offset: 0x00344FC0
		public void Switch(GrammarBuilders g, Action<RelativePosition> func0, Action<RegexPositionRelative> func1, Action<AbsolutePosition> func2, Action<RegexPosition> func3)
		{
			RelativePosition relativePosition;
			if (this.Is_RelativePosition(g, out relativePosition))
			{
				func0(relativePosition);
				return;
			}
			RegexPositionRelative regexPositionRelative;
			if (this.Is_RegexPositionRelative(g, out regexPositionRelative))
			{
				func1(regexPositionRelative);
				return;
			}
			AbsolutePosition absolutePosition;
			if (this.Is_AbsolutePosition(g, out absolutePosition))
			{
				func2(absolutePosition);
				return;
			}
			RegexPosition regexPosition;
			if (this.Is_RegexPosition(g, out regexPosition))
			{
				func3(regexPosition);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol pos");
		}

		// Token: 0x0600F509 RID: 62729 RVA: 0x00346E3F File Offset: 0x0034503F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F50A RID: 62730 RVA: 0x00346E54 File Offset: 0x00345054
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F50B RID: 62731 RVA: 0x00346E7E File Offset: 0x0034507E
		public bool Equals(pos other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B3C RID: 23356
		private ProgramNode _node;
	}
}
