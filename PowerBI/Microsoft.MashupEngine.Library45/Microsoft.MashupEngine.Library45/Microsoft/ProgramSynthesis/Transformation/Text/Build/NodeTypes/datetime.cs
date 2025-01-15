using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C48 RID: 7240
	public struct datetime : IProgramNodeBuilder, IEquatable<datetime>
	{
		// Token: 0x170028DE RID: 10462
		// (get) Token: 0x0600F496 RID: 62614 RVA: 0x00345946 File Offset: 0x00343B46
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F497 RID: 62615 RVA: 0x0034594E File Offset: 0x00343B4E
		private datetime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F498 RID: 62616 RVA: 0x00345957 File Offset: 0x00343B57
		public static datetime CreateUnsafe(ProgramNode node)
		{
			return new datetime(node);
		}

		// Token: 0x0600F499 RID: 62617 RVA: 0x00345960 File Offset: 0x00343B60
		public static datetime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.datetime)
			{
				return null;
			}
			return new datetime?(datetime.CreateUnsafe(node));
		}

		// Token: 0x0600F49A RID: 62618 RVA: 0x0034599A File Offset: 0x00343B9A
		public static datetime CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new datetime(new Hole(g.Symbol.datetime, holeId));
		}

		// Token: 0x0600F49B RID: 62619 RVA: 0x003459B2 File Offset: 0x00343BB2
		public bool Is_datetime_inputDateTime(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.datetime_inputDateTime;
		}

		// Token: 0x0600F49C RID: 62620 RVA: 0x003459CC File Offset: 0x00343BCC
		public bool Is_datetime_inputDateTime(GrammarBuilders g, out datetime_inputDateTime value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.datetime_inputDateTime)
			{
				value = datetime_inputDateTime.CreateUnsafe(this.Node);
				return true;
			}
			value = default(datetime_inputDateTime);
			return false;
		}

		// Token: 0x0600F49D RID: 62621 RVA: 0x00345A04 File Offset: 0x00343C04
		public datetime_inputDateTime? As_datetime_inputDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.datetime_inputDateTime)
			{
				return null;
			}
			return new datetime_inputDateTime?(datetime_inputDateTime.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F49E RID: 62622 RVA: 0x00345A44 File Offset: 0x00343C44
		public datetime_inputDateTime Cast_datetime_inputDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.datetime_inputDateTime)
			{
				return datetime_inputDateTime.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_datetime_inputDateTime is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F49F RID: 62623 RVA: 0x00345A99 File Offset: 0x00343C99
		public bool Is_RoundPartialDateTime(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.RoundPartialDateTime;
		}

		// Token: 0x0600F4A0 RID: 62624 RVA: 0x00345AB3 File Offset: 0x00343CB3
		public bool Is_RoundPartialDateTime(GrammarBuilders g, out RoundPartialDateTime value)
		{
			if (this.Node.GrammarRule == g.Rule.RoundPartialDateTime)
			{
				value = RoundPartialDateTime.CreateUnsafe(this.Node);
				return true;
			}
			value = default(RoundPartialDateTime);
			return false;
		}

		// Token: 0x0600F4A1 RID: 62625 RVA: 0x00345AE8 File Offset: 0x00343CE8
		public RoundPartialDateTime? As_RoundPartialDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.RoundPartialDateTime)
			{
				return null;
			}
			return new RoundPartialDateTime?(RoundPartialDateTime.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F4A2 RID: 62626 RVA: 0x00345B28 File Offset: 0x00343D28
		public RoundPartialDateTime Cast_RoundPartialDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.RoundPartialDateTime)
			{
				return RoundPartialDateTime.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_RoundPartialDateTime is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F4A3 RID: 62627 RVA: 0x00345B80 File Offset: 0x00343D80
		public T Switch<T>(GrammarBuilders g, Func<datetime_inputDateTime, T> func0, Func<RoundPartialDateTime, T> func1)
		{
			datetime_inputDateTime datetime_inputDateTime;
			if (this.Is_datetime_inputDateTime(g, out datetime_inputDateTime))
			{
				return func0(datetime_inputDateTime);
			}
			RoundPartialDateTime roundPartialDateTime;
			if (this.Is_RoundPartialDateTime(g, out roundPartialDateTime))
			{
				return func1(roundPartialDateTime);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol datetime");
		}

		// Token: 0x0600F4A4 RID: 62628 RVA: 0x00345BD8 File Offset: 0x00343DD8
		public void Switch(GrammarBuilders g, Action<datetime_inputDateTime> func0, Action<RoundPartialDateTime> func1)
		{
			datetime_inputDateTime datetime_inputDateTime;
			if (this.Is_datetime_inputDateTime(g, out datetime_inputDateTime))
			{
				func0(datetime_inputDateTime);
				return;
			}
			RoundPartialDateTime roundPartialDateTime;
			if (this.Is_RoundPartialDateTime(g, out roundPartialDateTime))
			{
				func1(roundPartialDateTime);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol datetime");
		}

		// Token: 0x0600F4A5 RID: 62629 RVA: 0x00345C2F File Offset: 0x00343E2F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F4A6 RID: 62630 RVA: 0x00345C44 File Offset: 0x00343E44
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F4A7 RID: 62631 RVA: 0x00345C6E File Offset: 0x00343E6E
		public bool Equals(datetime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B37 RID: 23351
		private ProgramNode _node;
	}
}
