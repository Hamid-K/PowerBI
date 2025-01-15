using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E68 RID: 3688
	public struct aboveOrOutput : IProgramNodeBuilder, IEquatable<aboveOrOutput>
	{
		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x0600644B RID: 25675 RVA: 0x00145C8E File Offset: 0x00143E8E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600644C RID: 25676 RVA: 0x00145C96 File Offset: 0x00143E96
		private aboveOrOutput(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600644D RID: 25677 RVA: 0x00145C9F File Offset: 0x00143E9F
		public static aboveOrOutput CreateUnsafe(ProgramNode node)
		{
			return new aboveOrOutput(node);
		}

		// Token: 0x0600644E RID: 25678 RVA: 0x00145CA8 File Offset: 0x00143EA8
		public static aboveOrOutput? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.aboveOrOutput)
			{
				return null;
			}
			return new aboveOrOutput?(aboveOrOutput.CreateUnsafe(node));
		}

		// Token: 0x0600644F RID: 25679 RVA: 0x00145CE2 File Offset: 0x00143EE2
		public static aboveOrOutput CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new aboveOrOutput(new Hole(g.Symbol.aboveOrOutput, holeId));
		}

		// Token: 0x06006450 RID: 25680 RVA: 0x00145CFA File Offset: 0x00143EFA
		public bool Is_aboveOrOutput_aboveOrHeader(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.aboveOrOutput_aboveOrHeader;
		}

		// Token: 0x06006451 RID: 25681 RVA: 0x00145D14 File Offset: 0x00143F14
		public bool Is_aboveOrOutput_aboveOrHeader(GrammarBuilders g, out aboveOrOutput_aboveOrHeader value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.aboveOrOutput_aboveOrHeader)
			{
				value = aboveOrOutput_aboveOrHeader.CreateUnsafe(this.Node);
				return true;
			}
			value = default(aboveOrOutput_aboveOrHeader);
			return false;
		}

		// Token: 0x06006452 RID: 25682 RVA: 0x00145D4C File Offset: 0x00143F4C
		public aboveOrOutput_aboveOrHeader? As_aboveOrOutput_aboveOrHeader(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.aboveOrOutput_aboveOrHeader)
			{
				return null;
			}
			return new aboveOrOutput_aboveOrHeader?(aboveOrOutput_aboveOrHeader.CreateUnsafe(this.Node));
		}

		// Token: 0x06006453 RID: 25683 RVA: 0x00145D8C File Offset: 0x00143F8C
		public aboveOrOutput_aboveOrHeader Cast_aboveOrOutput_aboveOrHeader(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.aboveOrOutput_aboveOrHeader)
			{
				return aboveOrOutput_aboveOrHeader.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_aboveOrOutput_aboveOrHeader is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006454 RID: 25684 RVA: 0x00145DE1 File Offset: 0x00143FE1
		public bool Is_aboveOrOutput_titleOf(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.aboveOrOutput_titleOf;
		}

		// Token: 0x06006455 RID: 25685 RVA: 0x00145DFB File Offset: 0x00143FFB
		public bool Is_aboveOrOutput_titleOf(GrammarBuilders g, out aboveOrOutput_titleOf value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.aboveOrOutput_titleOf)
			{
				value = aboveOrOutput_titleOf.CreateUnsafe(this.Node);
				return true;
			}
			value = default(aboveOrOutput_titleOf);
			return false;
		}

		// Token: 0x06006456 RID: 25686 RVA: 0x00145E30 File Offset: 0x00144030
		public aboveOrOutput_titleOf? As_aboveOrOutput_titleOf(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.aboveOrOutput_titleOf)
			{
				return null;
			}
			return new aboveOrOutput_titleOf?(aboveOrOutput_titleOf.CreateUnsafe(this.Node));
		}

		// Token: 0x06006457 RID: 25687 RVA: 0x00145E70 File Offset: 0x00144070
		public aboveOrOutput_titleOf Cast_aboveOrOutput_titleOf(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.aboveOrOutput_titleOf)
			{
				return aboveOrOutput_titleOf.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_aboveOrOutput_titleOf is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006458 RID: 25688 RVA: 0x00145EC8 File Offset: 0x001440C8
		public T Switch<T>(GrammarBuilders g, Func<aboveOrOutput_aboveOrHeader, T> func0, Func<aboveOrOutput_titleOf, T> func1)
		{
			aboveOrOutput_aboveOrHeader aboveOrOutput_aboveOrHeader;
			if (this.Is_aboveOrOutput_aboveOrHeader(g, out aboveOrOutput_aboveOrHeader))
			{
				return func0(aboveOrOutput_aboveOrHeader);
			}
			aboveOrOutput_titleOf aboveOrOutput_titleOf;
			if (this.Is_aboveOrOutput_titleOf(g, out aboveOrOutput_titleOf))
			{
				return func1(aboveOrOutput_titleOf);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol aboveOrOutput");
		}

		// Token: 0x06006459 RID: 25689 RVA: 0x00145F20 File Offset: 0x00144120
		public void Switch(GrammarBuilders g, Action<aboveOrOutput_aboveOrHeader> func0, Action<aboveOrOutput_titleOf> func1)
		{
			aboveOrOutput_aboveOrHeader aboveOrOutput_aboveOrHeader;
			if (this.Is_aboveOrOutput_aboveOrHeader(g, out aboveOrOutput_aboveOrHeader))
			{
				func0(aboveOrOutput_aboveOrHeader);
				return;
			}
			aboveOrOutput_titleOf aboveOrOutput_titleOf;
			if (this.Is_aboveOrOutput_titleOf(g, out aboveOrOutput_titleOf))
			{
				func1(aboveOrOutput_titleOf);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol aboveOrOutput");
		}

		// Token: 0x0600645A RID: 25690 RVA: 0x00145F77 File Offset: 0x00144177
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600645B RID: 25691 RVA: 0x00145F8C File Offset: 0x0014418C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600645C RID: 25692 RVA: 0x00145FB6 File Offset: 0x001441B6
		public bool Equals(aboveOrOutput other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C12 RID: 11282
		private ProgramNode _node;
	}
}
