using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes
{
	// Token: 0x0200127F RID: 4735
	public struct readFlatFile : IProgramNodeBuilder, IEquatable<readFlatFile>
	{
		// Token: 0x170018A4 RID: 6308
		// (get) Token: 0x06008F1A RID: 36634 RVA: 0x001E2062 File Offset: 0x001E0262
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008F1B RID: 36635 RVA: 0x001E206A File Offset: 0x001E026A
		private readFlatFile(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008F1C RID: 36636 RVA: 0x001E2073 File Offset: 0x001E0273
		public static readFlatFile CreateUnsafe(ProgramNode node)
		{
			return new readFlatFile(node);
		}

		// Token: 0x06008F1D RID: 36637 RVA: 0x001E207C File Offset: 0x001E027C
		public static readFlatFile? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.readFlatFile)
			{
				return null;
			}
			return new readFlatFile?(readFlatFile.CreateUnsafe(node));
		}

		// Token: 0x06008F1E RID: 36638 RVA: 0x001E20B6 File Offset: 0x001E02B6
		public static readFlatFile CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new readFlatFile(new Hole(g.Symbol.readFlatFile, holeId));
		}

		// Token: 0x06008F1F RID: 36639 RVA: 0x001E20CE File Offset: 0x001E02CE
		public bool Is_Csv(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Csv;
		}

		// Token: 0x06008F20 RID: 36640 RVA: 0x001E20E8 File Offset: 0x001E02E8
		public bool Is_Csv(GrammarBuilders g, out Csv value)
		{
			if (this.Node.GrammarRule == g.Rule.Csv)
			{
				value = Csv.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Csv);
			return false;
		}

		// Token: 0x06008F21 RID: 36641 RVA: 0x001E2120 File Offset: 0x001E0320
		public Csv? As_Csv(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Csv)
			{
				return null;
			}
			return new Csv?(Csv.CreateUnsafe(this.Node));
		}

		// Token: 0x06008F22 RID: 36642 RVA: 0x001E2160 File Offset: 0x001E0360
		public Csv Cast_Csv(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Csv)
			{
				return Csv.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Csv is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06008F23 RID: 36643 RVA: 0x001E21B5 File Offset: 0x001E03B5
		public bool Is_Fw(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Fw;
		}

		// Token: 0x06008F24 RID: 36644 RVA: 0x001E21CF File Offset: 0x001E03CF
		public bool Is_Fw(GrammarBuilders g, out Fw value)
		{
			if (this.Node.GrammarRule == g.Rule.Fw)
			{
				value = Fw.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Fw);
			return false;
		}

		// Token: 0x06008F25 RID: 36645 RVA: 0x001E2204 File Offset: 0x001E0404
		public Fw? As_Fw(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Fw)
			{
				return null;
			}
			return new Fw?(Fw.CreateUnsafe(this.Node));
		}

		// Token: 0x06008F26 RID: 36646 RVA: 0x001E2244 File Offset: 0x001E0444
		public Fw Cast_Fw(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Fw)
			{
				return Fw.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Fw is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06008F27 RID: 36647 RVA: 0x001E2299 File Offset: 0x001E0499
		public bool Is_StringRegionToStringTable(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.StringRegionToStringTable;
		}

		// Token: 0x06008F28 RID: 36648 RVA: 0x001E22B3 File Offset: 0x001E04B3
		public bool Is_StringRegionToStringTable(GrammarBuilders g, out StringRegionToStringTable value)
		{
			if (this.Node.GrammarRule == g.Rule.StringRegionToStringTable)
			{
				value = StringRegionToStringTable.CreateUnsafe(this.Node);
				return true;
			}
			value = default(StringRegionToStringTable);
			return false;
		}

		// Token: 0x06008F29 RID: 36649 RVA: 0x001E22E8 File Offset: 0x001E04E8
		public StringRegionToStringTable? As_StringRegionToStringTable(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.StringRegionToStringTable)
			{
				return null;
			}
			return new StringRegionToStringTable?(StringRegionToStringTable.CreateUnsafe(this.Node));
		}

		// Token: 0x06008F2A RID: 36650 RVA: 0x001E2328 File Offset: 0x001E0528
		public StringRegionToStringTable Cast_StringRegionToStringTable(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.StringRegionToStringTable)
			{
				return StringRegionToStringTable.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_StringRegionToStringTable is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06008F2B RID: 36651 RVA: 0x001E2380 File Offset: 0x001E0580
		public T Switch<T>(GrammarBuilders g, Func<Csv, T> func0, Func<Fw, T> func1, Func<StringRegionToStringTable, T> func2)
		{
			Csv csv;
			if (this.Is_Csv(g, out csv))
			{
				return func0(csv);
			}
			Fw fw;
			if (this.Is_Fw(g, out fw))
			{
				return func1(fw);
			}
			StringRegionToStringTable stringRegionToStringTable;
			if (this.Is_StringRegionToStringTable(g, out stringRegionToStringTable))
			{
				return func2(stringRegionToStringTable);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol readFlatFile");
		}

		// Token: 0x06008F2C RID: 36652 RVA: 0x001E23EC File Offset: 0x001E05EC
		public void Switch(GrammarBuilders g, Action<Csv> func0, Action<Fw> func1, Action<StringRegionToStringTable> func2)
		{
			Csv csv;
			if (this.Is_Csv(g, out csv))
			{
				func0(csv);
				return;
			}
			Fw fw;
			if (this.Is_Fw(g, out fw))
			{
				func1(fw);
				return;
			}
			StringRegionToStringTable stringRegionToStringTable;
			if (this.Is_StringRegionToStringTable(g, out stringRegionToStringTable))
			{
				func2(stringRegionToStringTable);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol readFlatFile");
		}

		// Token: 0x06008F2D RID: 36653 RVA: 0x001E2457 File Offset: 0x001E0657
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008F2E RID: 36654 RVA: 0x001E246C File Offset: 0x001E066C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008F2F RID: 36655 RVA: 0x001E2496 File Offset: 0x001E0696
		public bool Equals(readFlatFile other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A70 RID: 14960
		private ProgramNode _node;
	}
}
