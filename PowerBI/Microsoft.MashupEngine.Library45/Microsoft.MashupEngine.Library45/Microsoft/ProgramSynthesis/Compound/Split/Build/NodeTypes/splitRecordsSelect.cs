using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x0200095E RID: 2398
	public struct splitRecordsSelect : IProgramNodeBuilder, IEquatable<splitRecordsSelect>
	{
		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06003850 RID: 14416 RVA: 0x000AF26A File Offset: 0x000AD46A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x000AF272 File Offset: 0x000AD472
		private splitRecordsSelect(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x000AF27B File Offset: 0x000AD47B
		public static splitRecordsSelect CreateUnsafe(ProgramNode node)
		{
			return new splitRecordsSelect(node);
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x000AF284 File Offset: 0x000AD484
		public static splitRecordsSelect? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.splitRecordsSelect)
			{
				return null;
			}
			return new splitRecordsSelect?(splitRecordsSelect.CreateUnsafe(node));
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x000AF2BE File Offset: 0x000AD4BE
		public static splitRecordsSelect CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new splitRecordsSelect(new Hole(g.Symbol.splitRecordsSelect, holeId));
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x000AF2D6 File Offset: 0x000AD4D6
		public bool Is_SelectColumns(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SelectColumns;
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x000AF2F0 File Offset: 0x000AD4F0
		public bool Is_SelectColumns(GrammarBuilders g, out SelectColumns value)
		{
			if (this.Node.GrammarRule == g.Rule.SelectColumns)
			{
				value = SelectColumns.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SelectColumns);
			return false;
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x000AF328 File Offset: 0x000AD528
		public SelectColumns? As_SelectColumns(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SelectColumns)
			{
				return null;
			}
			return new SelectColumns?(SelectColumns.CreateUnsafe(this.Node));
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x000AF368 File Offset: 0x000AD568
		public SelectColumns Cast_SelectColumns(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SelectColumns)
			{
				return SelectColumns.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SelectColumns is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x000AF3BD File Offset: 0x000AD5BD
		public bool Is_splitRecordsSelect_splitRecords(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.splitRecordsSelect_splitRecords;
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x000AF3D7 File Offset: 0x000AD5D7
		public bool Is_splitRecordsSelect_splitRecords(GrammarBuilders g, out splitRecordsSelect_splitRecords value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.splitRecordsSelect_splitRecords)
			{
				value = splitRecordsSelect_splitRecords.CreateUnsafe(this.Node);
				return true;
			}
			value = default(splitRecordsSelect_splitRecords);
			return false;
		}

		// Token: 0x0600385B RID: 14427 RVA: 0x000AF40C File Offset: 0x000AD60C
		public splitRecordsSelect_splitRecords? As_splitRecordsSelect_splitRecords(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.splitRecordsSelect_splitRecords)
			{
				return null;
			}
			return new splitRecordsSelect_splitRecords?(splitRecordsSelect_splitRecords.CreateUnsafe(this.Node));
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x000AF44C File Offset: 0x000AD64C
		public splitRecordsSelect_splitRecords Cast_splitRecordsSelect_splitRecords(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.splitRecordsSelect_splitRecords)
			{
				return splitRecordsSelect_splitRecords.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_splitRecordsSelect_splitRecords is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x000AF4A4 File Offset: 0x000AD6A4
		public T Switch<T>(GrammarBuilders g, Func<SelectColumns, T> func0, Func<splitRecordsSelect_splitRecords, T> func1)
		{
			SelectColumns selectColumns;
			if (this.Is_SelectColumns(g, out selectColumns))
			{
				return func0(selectColumns);
			}
			splitRecordsSelect_splitRecords splitRecordsSelect_splitRecords;
			if (this.Is_splitRecordsSelect_splitRecords(g, out splitRecordsSelect_splitRecords))
			{
				return func1(splitRecordsSelect_splitRecords);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol splitRecordsSelect");
		}

		// Token: 0x0600385E RID: 14430 RVA: 0x000AF4FC File Offset: 0x000AD6FC
		public void Switch(GrammarBuilders g, Action<SelectColumns> func0, Action<splitRecordsSelect_splitRecords> func1)
		{
			SelectColumns selectColumns;
			if (this.Is_SelectColumns(g, out selectColumns))
			{
				func0(selectColumns);
				return;
			}
			splitRecordsSelect_splitRecords splitRecordsSelect_splitRecords;
			if (this.Is_splitRecordsSelect_splitRecords(g, out splitRecordsSelect_splitRecords))
			{
				func1(splitRecordsSelect_splitRecords);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol splitRecordsSelect");
		}

		// Token: 0x0600385F RID: 14431 RVA: 0x000AF553 File Offset: 0x000AD753
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003860 RID: 14432 RVA: 0x000AF568 File Offset: 0x000AD768
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003861 RID: 14433 RVA: 0x000AF592 File Offset: 0x000AD792
		public bool Equals(splitRecordsSelect other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A7E RID: 6782
		private ProgramNode _node;
	}
}
