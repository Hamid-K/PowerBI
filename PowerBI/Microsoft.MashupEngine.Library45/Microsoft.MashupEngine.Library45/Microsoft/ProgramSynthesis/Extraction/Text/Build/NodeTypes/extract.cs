using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F3B RID: 3899
	public struct extract : IProgramNodeBuilder, IEquatable<extract>
	{
		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x06006C3D RID: 27709 RVA: 0x0016265E File Offset: 0x0016085E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006C3E RID: 27710 RVA: 0x00162666 File Offset: 0x00160866
		private extract(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006C3F RID: 27711 RVA: 0x0016266F File Offset: 0x0016086F
		public static extract CreateUnsafe(ProgramNode node)
		{
			return new extract(node);
		}

		// Token: 0x06006C40 RID: 27712 RVA: 0x00162678 File Offset: 0x00160878
		public static extract? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.extract)
			{
				return null;
			}
			return new extract?(extract.CreateUnsafe(node));
		}

		// Token: 0x06006C41 RID: 27713 RVA: 0x001626B2 File Offset: 0x001608B2
		public static extract CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new extract(new Hole(g.Symbol.extract, holeId));
		}

		// Token: 0x06006C42 RID: 27714 RVA: 0x001626CA File Offset: 0x001608CA
		public bool Is_extract_row(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.extract_row;
		}

		// Token: 0x06006C43 RID: 27715 RVA: 0x001626E4 File Offset: 0x001608E4
		public bool Is_extract_row(GrammarBuilders g, out extract_row value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.extract_row)
			{
				value = extract_row.CreateUnsafe(this.Node);
				return true;
			}
			value = default(extract_row);
			return false;
		}

		// Token: 0x06006C44 RID: 27716 RVA: 0x0016271C File Offset: 0x0016091C
		public extract_row? As_extract_row(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.extract_row)
			{
				return null;
			}
			return new extract_row?(extract_row.CreateUnsafe(this.Node));
		}

		// Token: 0x06006C45 RID: 27717 RVA: 0x0016275C File Offset: 0x0016095C
		public extract_row Cast_extract_row(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.extract_row)
			{
				return extract_row.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_extract_row is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006C46 RID: 27718 RVA: 0x001627B1 File Offset: 0x001609B1
		public bool Is_BetweenDelimiters(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.BetweenDelimiters;
		}

		// Token: 0x06006C47 RID: 27719 RVA: 0x001627CB File Offset: 0x001609CB
		public bool Is_BetweenDelimiters(GrammarBuilders g, out BetweenDelimiters value)
		{
			if (this.Node.GrammarRule == g.Rule.BetweenDelimiters)
			{
				value = BetweenDelimiters.CreateUnsafe(this.Node);
				return true;
			}
			value = default(BetweenDelimiters);
			return false;
		}

		// Token: 0x06006C48 RID: 27720 RVA: 0x00162800 File Offset: 0x00160A00
		public BetweenDelimiters? As_BetweenDelimiters(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.BetweenDelimiters)
			{
				return null;
			}
			return new BetweenDelimiters?(BetweenDelimiters.CreateUnsafe(this.Node));
		}

		// Token: 0x06006C49 RID: 27721 RVA: 0x00162840 File Offset: 0x00160A40
		public BetweenDelimiters Cast_BetweenDelimiters(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.BetweenDelimiters)
			{
				return BetweenDelimiters.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_BetweenDelimiters is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006C4A RID: 27722 RVA: 0x00162895 File Offset: 0x00160A95
		public bool Is_Substring(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Substring;
		}

		// Token: 0x06006C4B RID: 27723 RVA: 0x001628AF File Offset: 0x00160AAF
		public bool Is_Substring(GrammarBuilders g, out Substring value)
		{
			if (this.Node.GrammarRule == g.Rule.Substring)
			{
				value = Substring.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Substring);
			return false;
		}

		// Token: 0x06006C4C RID: 27724 RVA: 0x001628E4 File Offset: 0x00160AE4
		public Substring? As_Substring(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Substring)
			{
				return null;
			}
			return new Substring?(Substring.CreateUnsafe(this.Node));
		}

		// Token: 0x06006C4D RID: 27725 RVA: 0x00162924 File Offset: 0x00160B24
		public Substring Cast_Substring(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Substring)
			{
				return Substring.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Substring is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006C4E RID: 27726 RVA: 0x00162979 File Offset: 0x00160B79
		public bool Is_Slice(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Slice;
		}

		// Token: 0x06006C4F RID: 27727 RVA: 0x00162993 File Offset: 0x00160B93
		public bool Is_Slice(GrammarBuilders g, out Slice value)
		{
			if (this.Node.GrammarRule == g.Rule.Slice)
			{
				value = Slice.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Slice);
			return false;
		}

		// Token: 0x06006C50 RID: 27728 RVA: 0x001629C8 File Offset: 0x00160BC8
		public Slice? As_Slice(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Slice)
			{
				return null;
			}
			return new Slice?(Slice.CreateUnsafe(this.Node));
		}

		// Token: 0x06006C51 RID: 27729 RVA: 0x00162A08 File Offset: 0x00160C08
		public Slice Cast_Slice(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Slice)
			{
				return Slice.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Slice is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006C52 RID: 27730 RVA: 0x00162A60 File Offset: 0x00160C60
		public T Switch<T>(GrammarBuilders g, Func<extract_row, T> func0, Func<BetweenDelimiters, T> func1, Func<Substring, T> func2, Func<Slice, T> func3)
		{
			extract_row extract_row;
			if (this.Is_extract_row(g, out extract_row))
			{
				return func0(extract_row);
			}
			BetweenDelimiters betweenDelimiters;
			if (this.Is_BetweenDelimiters(g, out betweenDelimiters))
			{
				return func1(betweenDelimiters);
			}
			Substring substring;
			if (this.Is_Substring(g, out substring))
			{
				return func2(substring);
			}
			Slice slice;
			if (this.Is_Slice(g, out slice))
			{
				return func3(slice);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol extract");
		}

		// Token: 0x06006C53 RID: 27731 RVA: 0x00162AE0 File Offset: 0x00160CE0
		public void Switch(GrammarBuilders g, Action<extract_row> func0, Action<BetweenDelimiters> func1, Action<Substring> func2, Action<Slice> func3)
		{
			extract_row extract_row;
			if (this.Is_extract_row(g, out extract_row))
			{
				func0(extract_row);
				return;
			}
			BetweenDelimiters betweenDelimiters;
			if (this.Is_BetweenDelimiters(g, out betweenDelimiters))
			{
				func1(betweenDelimiters);
				return;
			}
			Substring substring;
			if (this.Is_Substring(g, out substring))
			{
				func2(substring);
				return;
			}
			Slice slice;
			if (this.Is_Slice(g, out slice))
			{
				func3(slice);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol extract");
		}

		// Token: 0x06006C54 RID: 27732 RVA: 0x00162B5F File Offset: 0x00160D5F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006C55 RID: 27733 RVA: 0x00162B74 File Offset: 0x00160D74
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006C56 RID: 27734 RVA: 0x00162B9E File Offset: 0x00160D9E
		public bool Equals(extract other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F26 RID: 12070
		private ProgramNode _node;
	}
}
