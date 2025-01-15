using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C55 RID: 7253
	public struct numberFormat : IProgramNodeBuilder, IEquatable<numberFormat>
	{
		// Token: 0x170028EB RID: 10475
		// (get) Token: 0x0600F572 RID: 62834 RVA: 0x00347C06 File Offset: 0x00345E06
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F573 RID: 62835 RVA: 0x00347C0E File Offset: 0x00345E0E
		private numberFormat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F574 RID: 62836 RVA: 0x00347C17 File Offset: 0x00345E17
		public static numberFormat CreateUnsafe(ProgramNode node)
		{
			return new numberFormat(node);
		}

		// Token: 0x0600F575 RID: 62837 RVA: 0x00347C20 File Offset: 0x00345E20
		public static numberFormat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.numberFormat)
			{
				return null;
			}
			return new numberFormat?(numberFormat.CreateUnsafe(node));
		}

		// Token: 0x0600F576 RID: 62838 RVA: 0x00347C5A File Offset: 0x00345E5A
		public static numberFormat CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new numberFormat(new Hole(g.Symbol.numberFormat, holeId));
		}

		// Token: 0x0600F577 RID: 62839 RVA: 0x00347C72 File Offset: 0x00345E72
		public bool Is_BuildNumberFormat(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.BuildNumberFormat;
		}

		// Token: 0x0600F578 RID: 62840 RVA: 0x00347C8C File Offset: 0x00345E8C
		public bool Is_BuildNumberFormat(GrammarBuilders g, out BuildNumberFormat value)
		{
			if (this.Node.GrammarRule == g.Rule.BuildNumberFormat)
			{
				value = BuildNumberFormat.CreateUnsafe(this.Node);
				return true;
			}
			value = default(BuildNumberFormat);
			return false;
		}

		// Token: 0x0600F579 RID: 62841 RVA: 0x00347CC4 File Offset: 0x00345EC4
		public BuildNumberFormat? As_BuildNumberFormat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.BuildNumberFormat)
			{
				return null;
			}
			return new BuildNumberFormat?(BuildNumberFormat.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F57A RID: 62842 RVA: 0x00347D04 File Offset: 0x00345F04
		public BuildNumberFormat Cast_BuildNumberFormat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.BuildNumberFormat)
			{
				return BuildNumberFormat.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_BuildNumberFormat is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F57B RID: 62843 RVA: 0x00347D59 File Offset: 0x00345F59
		public bool Is_numberFormat_numberFormatLiteral(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.numberFormat_numberFormatLiteral;
		}

		// Token: 0x0600F57C RID: 62844 RVA: 0x00347D73 File Offset: 0x00345F73
		public bool Is_numberFormat_numberFormatLiteral(GrammarBuilders g, out numberFormat_numberFormatLiteral value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.numberFormat_numberFormatLiteral)
			{
				value = numberFormat_numberFormatLiteral.CreateUnsafe(this.Node);
				return true;
			}
			value = default(numberFormat_numberFormatLiteral);
			return false;
		}

		// Token: 0x0600F57D RID: 62845 RVA: 0x00347DA8 File Offset: 0x00345FA8
		public numberFormat_numberFormatLiteral? As_numberFormat_numberFormatLiteral(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.numberFormat_numberFormatLiteral)
			{
				return null;
			}
			return new numberFormat_numberFormatLiteral?(numberFormat_numberFormatLiteral.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F57E RID: 62846 RVA: 0x00347DE8 File Offset: 0x00345FE8
		public numberFormat_numberFormatLiteral Cast_numberFormat_numberFormatLiteral(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.numberFormat_numberFormatLiteral)
			{
				return numberFormat_numberFormatLiteral.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_numberFormat_numberFormatLiteral is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F57F RID: 62847 RVA: 0x00347E40 File Offset: 0x00346040
		public T Switch<T>(GrammarBuilders g, Func<BuildNumberFormat, T> func0, Func<numberFormat_numberFormatLiteral, T> func1)
		{
			BuildNumberFormat buildNumberFormat;
			if (this.Is_BuildNumberFormat(g, out buildNumberFormat))
			{
				return func0(buildNumberFormat);
			}
			numberFormat_numberFormatLiteral numberFormat_numberFormatLiteral;
			if (this.Is_numberFormat_numberFormatLiteral(g, out numberFormat_numberFormatLiteral))
			{
				return func1(numberFormat_numberFormatLiteral);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol numberFormat");
		}

		// Token: 0x0600F580 RID: 62848 RVA: 0x00347E98 File Offset: 0x00346098
		public void Switch(GrammarBuilders g, Action<BuildNumberFormat> func0, Action<numberFormat_numberFormatLiteral> func1)
		{
			BuildNumberFormat buildNumberFormat;
			if (this.Is_BuildNumberFormat(g, out buildNumberFormat))
			{
				func0(buildNumberFormat);
				return;
			}
			numberFormat_numberFormatLiteral numberFormat_numberFormatLiteral;
			if (this.Is_numberFormat_numberFormatLiteral(g, out numberFormat_numberFormatLiteral))
			{
				func1(numberFormat_numberFormatLiteral);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol numberFormat");
		}

		// Token: 0x0600F581 RID: 62849 RVA: 0x00347EEF File Offset: 0x003460EF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F582 RID: 62850 RVA: 0x00347F04 File Offset: 0x00346104
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F583 RID: 62851 RVA: 0x00347F2E File Offset: 0x0034612E
		public bool Equals(numberFormat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B44 RID: 23364
		private ProgramNode _node;
	}
}
