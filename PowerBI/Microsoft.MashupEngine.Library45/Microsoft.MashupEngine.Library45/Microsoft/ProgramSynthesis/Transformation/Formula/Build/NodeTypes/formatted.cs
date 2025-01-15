using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015A1 RID: 5537
	public struct formatted : IProgramNodeBuilder, IEquatable<formatted>
	{
		// Token: 0x17001FC7 RID: 8135
		// (get) Token: 0x0600B5E8 RID: 46568 RVA: 0x0027814A File Offset: 0x0027634A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B5E9 RID: 46569 RVA: 0x00278152 File Offset: 0x00276352
		private formatted(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B5EA RID: 46570 RVA: 0x0027815B File Offset: 0x0027635B
		public static formatted CreateUnsafe(ProgramNode node)
		{
			return new formatted(node);
		}

		// Token: 0x0600B5EB RID: 46571 RVA: 0x00278164 File Offset: 0x00276364
		public static formatted? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.formatted)
			{
				return null;
			}
			return new formatted?(formatted.CreateUnsafe(node));
		}

		// Token: 0x0600B5EC RID: 46572 RVA: 0x0027819E File Offset: 0x0027639E
		public static formatted CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new formatted(new Hole(g.Symbol.formatted, holeId));
		}

		// Token: 0x0600B5ED RID: 46573 RVA: 0x002781B6 File Offset: 0x002763B6
		public bool Is_formatted_formatNumber(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.formatted_formatNumber;
		}

		// Token: 0x0600B5EE RID: 46574 RVA: 0x002781D0 File Offset: 0x002763D0
		public bool Is_formatted_formatNumber(GrammarBuilders g, out formatted_formatNumber value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.formatted_formatNumber)
			{
				value = formatted_formatNumber.CreateUnsafe(this.Node);
				return true;
			}
			value = default(formatted_formatNumber);
			return false;
		}

		// Token: 0x0600B5EF RID: 46575 RVA: 0x00278208 File Offset: 0x00276408
		public formatted_formatNumber? As_formatted_formatNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.formatted_formatNumber)
			{
				return null;
			}
			return new formatted_formatNumber?(formatted_formatNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B5F0 RID: 46576 RVA: 0x00278248 File Offset: 0x00276448
		public formatted_formatNumber Cast_formatted_formatNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.formatted_formatNumber)
			{
				return formatted_formatNumber.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_formatted_formatNumber is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B5F1 RID: 46577 RVA: 0x0027829D File Offset: 0x0027649D
		public bool Is_formatted_formatDateTime(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.formatted_formatDateTime;
		}

		// Token: 0x0600B5F2 RID: 46578 RVA: 0x002782B7 File Offset: 0x002764B7
		public bool Is_formatted_formatDateTime(GrammarBuilders g, out formatted_formatDateTime value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.formatted_formatDateTime)
			{
				value = formatted_formatDateTime.CreateUnsafe(this.Node);
				return true;
			}
			value = default(formatted_formatDateTime);
			return false;
		}

		// Token: 0x0600B5F3 RID: 46579 RVA: 0x002782EC File Offset: 0x002764EC
		public formatted_formatDateTime? As_formatted_formatDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.formatted_formatDateTime)
			{
				return null;
			}
			return new formatted_formatDateTime?(formatted_formatDateTime.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B5F4 RID: 46580 RVA: 0x0027832C File Offset: 0x0027652C
		public formatted_formatDateTime Cast_formatted_formatDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.formatted_formatDateTime)
			{
				return formatted_formatDateTime.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_formatted_formatDateTime is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B5F5 RID: 46581 RVA: 0x00278384 File Offset: 0x00276584
		public T Switch<T>(GrammarBuilders g, Func<formatted_formatNumber, T> func0, Func<formatted_formatDateTime, T> func1)
		{
			formatted_formatNumber formatted_formatNumber;
			if (this.Is_formatted_formatNumber(g, out formatted_formatNumber))
			{
				return func0(formatted_formatNumber);
			}
			formatted_formatDateTime formatted_formatDateTime;
			if (this.Is_formatted_formatDateTime(g, out formatted_formatDateTime))
			{
				return func1(formatted_formatDateTime);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol formatted");
		}

		// Token: 0x0600B5F6 RID: 46582 RVA: 0x002783DC File Offset: 0x002765DC
		public void Switch(GrammarBuilders g, Action<formatted_formatNumber> func0, Action<formatted_formatDateTime> func1)
		{
			formatted_formatNumber formatted_formatNumber;
			if (this.Is_formatted_formatNumber(g, out formatted_formatNumber))
			{
				func0(formatted_formatNumber);
				return;
			}
			formatted_formatDateTime formatted_formatDateTime;
			if (this.Is_formatted_formatDateTime(g, out formatted_formatDateTime))
			{
				func1(formatted_formatDateTime);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol formatted");
		}

		// Token: 0x0600B5F7 RID: 46583 RVA: 0x00278433 File Offset: 0x00276633
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B5F8 RID: 46584 RVA: 0x00278448 File Offset: 0x00276648
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B5F9 RID: 46585 RVA: 0x00278472 File Offset: 0x00276672
		public bool Equals(formatted other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400464F RID: 17999
		private ProgramNode _node;
	}
}
