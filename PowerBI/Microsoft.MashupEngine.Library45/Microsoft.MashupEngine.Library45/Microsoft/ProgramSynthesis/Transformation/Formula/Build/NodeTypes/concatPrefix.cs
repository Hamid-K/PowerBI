using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015A5 RID: 5541
	public struct concatPrefix : IProgramNodeBuilder, IEquatable<concatPrefix>
	{
		// Token: 0x17001FCB RID: 8139
		// (get) Token: 0x0600B632 RID: 46642 RVA: 0x00278E06 File Offset: 0x00277006
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B633 RID: 46643 RVA: 0x00278E0E File Offset: 0x0027700E
		private concatPrefix(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B634 RID: 46644 RVA: 0x00278E17 File Offset: 0x00277017
		public static concatPrefix CreateUnsafe(ProgramNode node)
		{
			return new concatPrefix(node);
		}

		// Token: 0x0600B635 RID: 46645 RVA: 0x00278E20 File Offset: 0x00277020
		public static concatPrefix? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.concatPrefix)
			{
				return null;
			}
			return new concatPrefix?(concatPrefix.CreateUnsafe(node));
		}

		// Token: 0x0600B636 RID: 46646 RVA: 0x00278E5A File Offset: 0x0027705A
		public static concatPrefix CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new concatPrefix(new Hole(g.Symbol.concatPrefix, holeId));
		}

		// Token: 0x0600B637 RID: 46647 RVA: 0x00278E72 File Offset: 0x00277072
		public bool Is_concatPrefix_concatSegment(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.concatPrefix_concatSegment;
		}

		// Token: 0x0600B638 RID: 46648 RVA: 0x00278E8C File Offset: 0x0027708C
		public bool Is_concatPrefix_concatSegment(GrammarBuilders g, out concatPrefix_concatSegment value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.concatPrefix_concatSegment)
			{
				value = concatPrefix_concatSegment.CreateUnsafe(this.Node);
				return true;
			}
			value = default(concatPrefix_concatSegment);
			return false;
		}

		// Token: 0x0600B639 RID: 46649 RVA: 0x00278EC4 File Offset: 0x002770C4
		public concatPrefix_concatSegment? As_concatPrefix_concatSegment(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.concatPrefix_concatSegment)
			{
				return null;
			}
			return new concatPrefix_concatSegment?(concatPrefix_concatSegment.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B63A RID: 46650 RVA: 0x00278F04 File Offset: 0x00277104
		public concatPrefix_concatSegment Cast_concatPrefix_concatSegment(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.concatPrefix_concatSegment)
			{
				return concatPrefix_concatSegment.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_concatPrefix_concatSegment is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B63B RID: 46651 RVA: 0x00278F59 File Offset: 0x00277159
		public bool Is_concatPrefix_formatted(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.concatPrefix_formatted;
		}

		// Token: 0x0600B63C RID: 46652 RVA: 0x00278F73 File Offset: 0x00277173
		public bool Is_concatPrefix_formatted(GrammarBuilders g, out concatPrefix_formatted value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.concatPrefix_formatted)
			{
				value = concatPrefix_formatted.CreateUnsafe(this.Node);
				return true;
			}
			value = default(concatPrefix_formatted);
			return false;
		}

		// Token: 0x0600B63D RID: 46653 RVA: 0x00278FA8 File Offset: 0x002771A8
		public concatPrefix_formatted? As_concatPrefix_formatted(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.concatPrefix_formatted)
			{
				return null;
			}
			return new concatPrefix_formatted?(concatPrefix_formatted.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B63E RID: 46654 RVA: 0x00278FE8 File Offset: 0x002771E8
		public concatPrefix_formatted Cast_concatPrefix_formatted(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.concatPrefix_formatted)
			{
				return concatPrefix_formatted.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_concatPrefix_formatted is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B63F RID: 46655 RVA: 0x0027903D File Offset: 0x0027723D
		public bool Is_concatPrefix_constString(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.concatPrefix_constString;
		}

		// Token: 0x0600B640 RID: 46656 RVA: 0x00279057 File Offset: 0x00277257
		public bool Is_concatPrefix_constString(GrammarBuilders g, out concatPrefix_constString value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.concatPrefix_constString)
			{
				value = concatPrefix_constString.CreateUnsafe(this.Node);
				return true;
			}
			value = default(concatPrefix_constString);
			return false;
		}

		// Token: 0x0600B641 RID: 46657 RVA: 0x0027908C File Offset: 0x0027728C
		public concatPrefix_constString? As_concatPrefix_constString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.concatPrefix_constString)
			{
				return null;
			}
			return new concatPrefix_constString?(concatPrefix_constString.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B642 RID: 46658 RVA: 0x002790CC File Offset: 0x002772CC
		public concatPrefix_constString Cast_concatPrefix_constString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.concatPrefix_constString)
			{
				return concatPrefix_constString.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_concatPrefix_constString is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B643 RID: 46659 RVA: 0x00279124 File Offset: 0x00277324
		public T Switch<T>(GrammarBuilders g, Func<concatPrefix_concatSegment, T> func0, Func<concatPrefix_formatted, T> func1, Func<concatPrefix_constString, T> func2)
		{
			concatPrefix_concatSegment concatPrefix_concatSegment;
			if (this.Is_concatPrefix_concatSegment(g, out concatPrefix_concatSegment))
			{
				return func0(concatPrefix_concatSegment);
			}
			concatPrefix_formatted concatPrefix_formatted;
			if (this.Is_concatPrefix_formatted(g, out concatPrefix_formatted))
			{
				return func1(concatPrefix_formatted);
			}
			concatPrefix_constString concatPrefix_constString;
			if (this.Is_concatPrefix_constString(g, out concatPrefix_constString))
			{
				return func2(concatPrefix_constString);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol concatPrefix");
		}

		// Token: 0x0600B644 RID: 46660 RVA: 0x00279190 File Offset: 0x00277390
		public void Switch(GrammarBuilders g, Action<concatPrefix_concatSegment> func0, Action<concatPrefix_formatted> func1, Action<concatPrefix_constString> func2)
		{
			concatPrefix_concatSegment concatPrefix_concatSegment;
			if (this.Is_concatPrefix_concatSegment(g, out concatPrefix_concatSegment))
			{
				func0(concatPrefix_concatSegment);
				return;
			}
			concatPrefix_formatted concatPrefix_formatted;
			if (this.Is_concatPrefix_formatted(g, out concatPrefix_formatted))
			{
				func1(concatPrefix_formatted);
				return;
			}
			concatPrefix_constString concatPrefix_constString;
			if (this.Is_concatPrefix_constString(g, out concatPrefix_constString))
			{
				func2(concatPrefix_constString);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol concatPrefix");
		}

		// Token: 0x0600B645 RID: 46661 RVA: 0x002791FB File Offset: 0x002773FB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B646 RID: 46662 RVA: 0x00279210 File Offset: 0x00277410
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B647 RID: 46663 RVA: 0x0027923A File Offset: 0x0027743A
		public bool Equals(concatPrefix other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004653 RID: 18003
		private ProgramNode _node;
	}
}
