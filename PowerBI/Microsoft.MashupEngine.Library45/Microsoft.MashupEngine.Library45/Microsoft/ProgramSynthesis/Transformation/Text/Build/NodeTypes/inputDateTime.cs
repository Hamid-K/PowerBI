using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C49 RID: 7241
	public struct inputDateTime : IProgramNodeBuilder, IEquatable<inputDateTime>
	{
		// Token: 0x170028DF RID: 10463
		// (get) Token: 0x0600F4A8 RID: 62632 RVA: 0x00345C82 File Offset: 0x00343E82
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F4A9 RID: 62633 RVA: 0x00345C8A File Offset: 0x00343E8A
		private inputDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F4AA RID: 62634 RVA: 0x00345C93 File Offset: 0x00343E93
		public static inputDateTime CreateUnsafe(ProgramNode node)
		{
			return new inputDateTime(node);
		}

		// Token: 0x0600F4AB RID: 62635 RVA: 0x00345C9C File Offset: 0x00343E9C
		public static inputDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.inputDateTime)
			{
				return null;
			}
			return new inputDateTime?(inputDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600F4AC RID: 62636 RVA: 0x00345CD6 File Offset: 0x00343ED6
		public static inputDateTime CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new inputDateTime(new Hole(g.Symbol.inputDateTime, holeId));
		}

		// Token: 0x0600F4AD RID: 62637 RVA: 0x00345CEE File Offset: 0x00343EEE
		public bool Is_AsPartialDateTime(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.AsPartialDateTime;
		}

		// Token: 0x0600F4AE RID: 62638 RVA: 0x00345D08 File Offset: 0x00343F08
		public bool Is_AsPartialDateTime(GrammarBuilders g, out AsPartialDateTime value)
		{
			if (this.Node.GrammarRule == g.Rule.AsPartialDateTime)
			{
				value = AsPartialDateTime.CreateUnsafe(this.Node);
				return true;
			}
			value = default(AsPartialDateTime);
			return false;
		}

		// Token: 0x0600F4AF RID: 62639 RVA: 0x00345D40 File Offset: 0x00343F40
		public AsPartialDateTime? As_AsPartialDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.AsPartialDateTime)
			{
				return null;
			}
			return new AsPartialDateTime?(AsPartialDateTime.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F4B0 RID: 62640 RVA: 0x00345D80 File Offset: 0x00343F80
		public AsPartialDateTime Cast_AsPartialDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.AsPartialDateTime)
			{
				return AsPartialDateTime.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_AsPartialDateTime is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F4B1 RID: 62641 RVA: 0x00345DD5 File Offset: 0x00343FD5
		public bool Is_inputDateTime_parsedDateTime(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.inputDateTime_parsedDateTime;
		}

		// Token: 0x0600F4B2 RID: 62642 RVA: 0x00345DEF File Offset: 0x00343FEF
		public bool Is_inputDateTime_parsedDateTime(GrammarBuilders g, out inputDateTime_parsedDateTime value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.inputDateTime_parsedDateTime)
			{
				value = inputDateTime_parsedDateTime.CreateUnsafe(this.Node);
				return true;
			}
			value = default(inputDateTime_parsedDateTime);
			return false;
		}

		// Token: 0x0600F4B3 RID: 62643 RVA: 0x00345E24 File Offset: 0x00344024
		public inputDateTime_parsedDateTime? As_inputDateTime_parsedDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.inputDateTime_parsedDateTime)
			{
				return null;
			}
			return new inputDateTime_parsedDateTime?(inputDateTime_parsedDateTime.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F4B4 RID: 62644 RVA: 0x00345E64 File Offset: 0x00344064
		public inputDateTime_parsedDateTime Cast_inputDateTime_parsedDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.inputDateTime_parsedDateTime)
			{
				return inputDateTime_parsedDateTime.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_inputDateTime_parsedDateTime is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F4B5 RID: 62645 RVA: 0x00345EBC File Offset: 0x003440BC
		public T Switch<T>(GrammarBuilders g, Func<AsPartialDateTime, T> func0, Func<inputDateTime_parsedDateTime, T> func1)
		{
			AsPartialDateTime asPartialDateTime;
			if (this.Is_AsPartialDateTime(g, out asPartialDateTime))
			{
				return func0(asPartialDateTime);
			}
			inputDateTime_parsedDateTime inputDateTime_parsedDateTime;
			if (this.Is_inputDateTime_parsedDateTime(g, out inputDateTime_parsedDateTime))
			{
				return func1(inputDateTime_parsedDateTime);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol inputDateTime");
		}

		// Token: 0x0600F4B6 RID: 62646 RVA: 0x00345F14 File Offset: 0x00344114
		public void Switch(GrammarBuilders g, Action<AsPartialDateTime> func0, Action<inputDateTime_parsedDateTime> func1)
		{
			AsPartialDateTime asPartialDateTime;
			if (this.Is_AsPartialDateTime(g, out asPartialDateTime))
			{
				func0(asPartialDateTime);
				return;
			}
			inputDateTime_parsedDateTime inputDateTime_parsedDateTime;
			if (this.Is_inputDateTime_parsedDateTime(g, out inputDateTime_parsedDateTime))
			{
				func1(inputDateTime_parsedDateTime);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol inputDateTime");
		}

		// Token: 0x0600F4B7 RID: 62647 RVA: 0x00345F6B File Offset: 0x0034416B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F4B8 RID: 62648 RVA: 0x00345F80 File Offset: 0x00344180
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F4B9 RID: 62649 RVA: 0x00345FAA File Offset: 0x003441AA
		public bool Equals(inputDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B38 RID: 23352
		private ProgramNode _node;
	}
}
