using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x0200095F RID: 2399
	public struct splitRecords : IProgramNodeBuilder, IEquatable<splitRecords>
	{
		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06003862 RID: 14434 RVA: 0x000AF5A6 File Offset: 0x000AD7A6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x000AF5AE File Offset: 0x000AD7AE
		private splitRecords(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x000AF5B7 File Offset: 0x000AD7B7
		public static splitRecords CreateUnsafe(ProgramNode node)
		{
			return new splitRecords(node);
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x000AF5C0 File Offset: 0x000AD7C0
		public static splitRecords? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.splitRecords)
			{
				return null;
			}
			return new splitRecords?(splitRecords.CreateUnsafe(node));
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x000AF5FA File Offset: 0x000AD7FA
		public static splitRecords CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new splitRecords(new Hole(g.Symbol.splitRecords, holeId));
		}

		// Token: 0x06003867 RID: 14439 RVA: 0x000AF612 File Offset: 0x000AD812
		public bool Is_NoSplit(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.NoSplit;
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x000AF62C File Offset: 0x000AD82C
		public bool Is_NoSplit(GrammarBuilders g, out NoSplit value)
		{
			if (this.Node.GrammarRule == g.Rule.NoSplit)
			{
				value = NoSplit.CreateUnsafe(this.Node);
				return true;
			}
			value = default(NoSplit);
			return false;
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x000AF664 File Offset: 0x000AD864
		public NoSplit? As_NoSplit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.NoSplit)
			{
				return null;
			}
			return new NoSplit?(NoSplit.CreateUnsafe(this.Node));
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x000AF6A4 File Offset: 0x000AD8A4
		public NoSplit Cast_NoSplit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.NoSplit)
			{
				return NoSplit.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_NoSplit is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x000AF6F9 File Offset: 0x000AD8F9
		public bool Is_TableFromCells(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TableFromCells;
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x000AF713 File Offset: 0x000AD913
		public bool Is_TableFromCells(GrammarBuilders g, out TableFromCells value)
		{
			if (this.Node.GrammarRule == g.Rule.TableFromCells)
			{
				value = TableFromCells.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TableFromCells);
			return false;
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x000AF748 File Offset: 0x000AD948
		public TableFromCells? As_TableFromCells(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TableFromCells)
			{
				return null;
			}
			return new TableFromCells?(TableFromCells.CreateUnsafe(this.Node));
		}

		// Token: 0x0600386E RID: 14446 RVA: 0x000AF788 File Offset: 0x000AD988
		public TableFromCells Cast_TableFromCells(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TableFromCells)
			{
				return TableFromCells.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TableFromCells is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x000AF7DD File Offset: 0x000AD9DD
		public bool Is_MultiRecordSplit(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.MultiRecordSplit;
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x000AF7F7 File Offset: 0x000AD9F7
		public bool Is_MultiRecordSplit(GrammarBuilders g, out MultiRecordSplit value)
		{
			if (this.Node.GrammarRule == g.Rule.MultiRecordSplit)
			{
				value = MultiRecordSplit.CreateUnsafe(this.Node);
				return true;
			}
			value = default(MultiRecordSplit);
			return false;
		}

		// Token: 0x06003871 RID: 14449 RVA: 0x000AF82C File Offset: 0x000ADA2C
		public MultiRecordSplit? As_MultiRecordSplit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.MultiRecordSplit)
			{
				return null;
			}
			return new MultiRecordSplit?(MultiRecordSplit.CreateUnsafe(this.Node));
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x000AF86C File Offset: 0x000ADA6C
		public MultiRecordSplit Cast_MultiRecordSplit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.MultiRecordSplit)
			{
				return MultiRecordSplit.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_MultiRecordSplit is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06003873 RID: 14451 RVA: 0x000AF8C4 File Offset: 0x000ADAC4
		public T Switch<T>(GrammarBuilders g, Func<NoSplit, T> func0, Func<TableFromCells, T> func1, Func<MultiRecordSplit, T> func2)
		{
			NoSplit noSplit;
			if (this.Is_NoSplit(g, out noSplit))
			{
				return func0(noSplit);
			}
			TableFromCells tableFromCells;
			if (this.Is_TableFromCells(g, out tableFromCells))
			{
				return func1(tableFromCells);
			}
			MultiRecordSplit multiRecordSplit;
			if (this.Is_MultiRecordSplit(g, out multiRecordSplit))
			{
				return func2(multiRecordSplit);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol splitRecords");
		}

		// Token: 0x06003874 RID: 14452 RVA: 0x000AF930 File Offset: 0x000ADB30
		public void Switch(GrammarBuilders g, Action<NoSplit> func0, Action<TableFromCells> func1, Action<MultiRecordSplit> func2)
		{
			NoSplit noSplit;
			if (this.Is_NoSplit(g, out noSplit))
			{
				func0(noSplit);
				return;
			}
			TableFromCells tableFromCells;
			if (this.Is_TableFromCells(g, out tableFromCells))
			{
				func1(tableFromCells);
				return;
			}
			MultiRecordSplit multiRecordSplit;
			if (this.Is_MultiRecordSplit(g, out multiRecordSplit))
			{
				func2(multiRecordSplit);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol splitRecords");
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x000AF99B File Offset: 0x000ADB9B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x000AF9B0 File Offset: 0x000ADBB0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x000AF9DA File Offset: 0x000ADBDA
		public bool Equals(splitRecords other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A7F RID: 6783
		private ProgramNode _node;
	}
}
