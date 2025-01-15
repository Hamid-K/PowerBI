using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E53 RID: 3667
	public struct area : IProgramNodeBuilder, IEquatable<area>
	{
		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x06006299 RID: 25241 RVA: 0x00140E42 File Offset: 0x0013F042
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600629A RID: 25242 RVA: 0x00140E4A File Offset: 0x0013F04A
		private area(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600629B RID: 25243 RVA: 0x00140E53 File Offset: 0x0013F053
		public static area CreateUnsafe(ProgramNode node)
		{
			return new area(node);
		}

		// Token: 0x0600629C RID: 25244 RVA: 0x00140E5C File Offset: 0x0013F05C
		public static area? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.area)
			{
				return null;
			}
			return new area?(area.CreateUnsafe(node));
		}

		// Token: 0x0600629D RID: 25245 RVA: 0x00140E96 File Offset: 0x0013F096
		public static area CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new area(new Hole(g.Symbol.area, holeId));
		}

		// Token: 0x0600629E RID: 25246 RVA: 0x00140EAE File Offset: 0x0013F0AE
		public bool Is_area_trimLeft(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.area_trimLeft;
		}

		// Token: 0x0600629F RID: 25247 RVA: 0x00140EC8 File Offset: 0x0013F0C8
		public bool Is_area_trimLeft(GrammarBuilders g, out area_trimLeft value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.area_trimLeft)
			{
				value = area_trimLeft.CreateUnsafe(this.Node);
				return true;
			}
			value = default(area_trimLeft);
			return false;
		}

		// Token: 0x060062A0 RID: 25248 RVA: 0x00140F00 File Offset: 0x0013F100
		public area_trimLeft? As_area_trimLeft(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.area_trimLeft)
			{
				return null;
			}
			return new area_trimLeft?(area_trimLeft.CreateUnsafe(this.Node));
		}

		// Token: 0x060062A1 RID: 25249 RVA: 0x00140F40 File Offset: 0x0013F140
		public area_trimLeft Cast_area_trimLeft(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.area_trimLeft)
			{
				return area_trimLeft.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_area_trimLeft is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060062A2 RID: 25250 RVA: 0x00140F95 File Offset: 0x0013F195
		public bool Is_DefinedRange(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.DefinedRange;
		}

		// Token: 0x060062A3 RID: 25251 RVA: 0x00140FAF File Offset: 0x0013F1AF
		public bool Is_DefinedRange(GrammarBuilders g, out DefinedRange value)
		{
			if (this.Node.GrammarRule == g.Rule.DefinedRange)
			{
				value = DefinedRange.CreateUnsafe(this.Node);
				return true;
			}
			value = default(DefinedRange);
			return false;
		}

		// Token: 0x060062A4 RID: 25252 RVA: 0x00140FE4 File Offset: 0x0013F1E4
		public DefinedRange? As_DefinedRange(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.DefinedRange)
			{
				return null;
			}
			return new DefinedRange?(DefinedRange.CreateUnsafe(this.Node));
		}

		// Token: 0x060062A5 RID: 25253 RVA: 0x00141024 File Offset: 0x0013F224
		public DefinedRange Cast_DefinedRange(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.DefinedRange)
			{
				return DefinedRange.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_DefinedRange is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060062A6 RID: 25254 RVA: 0x0014107C File Offset: 0x0013F27C
		public T Switch<T>(GrammarBuilders g, Func<area_trimLeft, T> func0, Func<DefinedRange, T> func1)
		{
			area_trimLeft area_trimLeft;
			if (this.Is_area_trimLeft(g, out area_trimLeft))
			{
				return func0(area_trimLeft);
			}
			DefinedRange definedRange;
			if (this.Is_DefinedRange(g, out definedRange))
			{
				return func1(definedRange);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol area");
		}

		// Token: 0x060062A7 RID: 25255 RVA: 0x001410D4 File Offset: 0x0013F2D4
		public void Switch(GrammarBuilders g, Action<area_trimLeft> func0, Action<DefinedRange> func1)
		{
			area_trimLeft area_trimLeft;
			if (this.Is_area_trimLeft(g, out area_trimLeft))
			{
				func0(area_trimLeft);
				return;
			}
			DefinedRange definedRange;
			if (this.Is_DefinedRange(g, out definedRange))
			{
				func1(definedRange);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol area");
		}

		// Token: 0x060062A8 RID: 25256 RVA: 0x0014112B File Offset: 0x0013F32B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060062A9 RID: 25257 RVA: 0x00141140 File Offset: 0x0013F340
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060062AA RID: 25258 RVA: 0x0014116A File Offset: 0x0013F36A
		public bool Equals(area other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BFD RID: 11261
		private ProgramNode _node;
	}
}
