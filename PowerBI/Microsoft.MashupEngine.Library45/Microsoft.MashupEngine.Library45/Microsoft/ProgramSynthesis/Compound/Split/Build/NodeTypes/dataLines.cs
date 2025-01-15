using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000969 RID: 2409
	public struct dataLines : IProgramNodeBuilder, IEquatable<dataLines>
	{
		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x0600390A RID: 14602 RVA: 0x000B0E7E File Offset: 0x000AF07E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x000B0E86 File Offset: 0x000AF086
		private dataLines(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x000B0E8F File Offset: 0x000AF08F
		public static dataLines CreateUnsafe(ProgramNode node)
		{
			return new dataLines(node);
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x000B0E98 File Offset: 0x000AF098
		public static dataLines? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.dataLines)
			{
				return null;
			}
			return new dataLines?(dataLines.CreateUnsafe(node));
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x000B0ED2 File Offset: 0x000AF0D2
		public static dataLines CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new dataLines(new Hole(g.Symbol.dataLines, holeId));
		}

		// Token: 0x0600390F RID: 14607 RVA: 0x000B0EEA File Offset: 0x000AF0EA
		public bool Is_FilterHeader(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.FilterHeader;
		}

		// Token: 0x06003910 RID: 14608 RVA: 0x000B0F04 File Offset: 0x000AF104
		public bool Is_FilterHeader(GrammarBuilders g, out FilterHeader value)
		{
			if (this.Node.GrammarRule == g.Rule.FilterHeader)
			{
				value = FilterHeader.CreateUnsafe(this.Node);
				return true;
			}
			value = default(FilterHeader);
			return false;
		}

		// Token: 0x06003911 RID: 14609 RVA: 0x000B0F3C File Offset: 0x000AF13C
		public FilterHeader? As_FilterHeader(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.FilterHeader)
			{
				return null;
			}
			return new FilterHeader?(FilterHeader.CreateUnsafe(this.Node));
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x000B0F7C File Offset: 0x000AF17C
		public FilterHeader Cast_FilterHeader(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.FilterHeader)
			{
				return FilterHeader.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_FilterHeader is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x000B0FD1 File Offset: 0x000AF1D1
		public bool Is_SelectData(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SelectData;
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x000B0FEB File Offset: 0x000AF1EB
		public bool Is_SelectData(GrammarBuilders g, out SelectData value)
		{
			if (this.Node.GrammarRule == g.Rule.SelectData)
			{
				value = SelectData.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SelectData);
			return false;
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x000B1020 File Offset: 0x000AF220
		public SelectData? As_SelectData(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SelectData)
			{
				return null;
			}
			return new SelectData?(SelectData.CreateUnsafe(this.Node));
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x000B1060 File Offset: 0x000AF260
		public SelectData Cast_SelectData(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SelectData)
			{
				return SelectData.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SelectData is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x000B10B5 File Offset: 0x000AF2B5
		public bool Is_FilterRecords(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.FilterRecords;
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x000B10CF File Offset: 0x000AF2CF
		public bool Is_FilterRecords(GrammarBuilders g, out FilterRecords value)
		{
			if (this.Node.GrammarRule == g.Rule.FilterRecords)
			{
				value = FilterRecords.CreateUnsafe(this.Node);
				return true;
			}
			value = default(FilterRecords);
			return false;
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x000B1104 File Offset: 0x000AF304
		public FilterRecords? As_FilterRecords(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.FilterRecords)
			{
				return null;
			}
			return new FilterRecords?(FilterRecords.CreateUnsafe(this.Node));
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x000B1144 File Offset: 0x000AF344
		public FilterRecords Cast_FilterRecords(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.FilterRecords)
			{
				return FilterRecords.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_FilterRecords is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x000B1199 File Offset: 0x000AF399
		public bool Is_dataLines_skippedRecords(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.dataLines_skippedRecords;
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x000B11B3 File Offset: 0x000AF3B3
		public bool Is_dataLines_skippedRecords(GrammarBuilders g, out dataLines_skippedRecords value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.dataLines_skippedRecords)
			{
				value = dataLines_skippedRecords.CreateUnsafe(this.Node);
				return true;
			}
			value = default(dataLines_skippedRecords);
			return false;
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x000B11E8 File Offset: 0x000AF3E8
		public dataLines_skippedRecords? As_dataLines_skippedRecords(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.dataLines_skippedRecords)
			{
				return null;
			}
			return new dataLines_skippedRecords?(dataLines_skippedRecords.CreateUnsafe(this.Node));
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x000B1228 File Offset: 0x000AF428
		public dataLines_skippedRecords Cast_dataLines_skippedRecords(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.dataLines_skippedRecords)
			{
				return dataLines_skippedRecords.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_dataLines_skippedRecords is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x000B1280 File Offset: 0x000AF480
		public T Switch<T>(GrammarBuilders g, Func<FilterHeader, T> func0, Func<SelectData, T> func1, Func<FilterRecords, T> func2, Func<dataLines_skippedRecords, T> func3)
		{
			FilterHeader filterHeader;
			if (this.Is_FilterHeader(g, out filterHeader))
			{
				return func0(filterHeader);
			}
			SelectData selectData;
			if (this.Is_SelectData(g, out selectData))
			{
				return func1(selectData);
			}
			FilterRecords filterRecords;
			if (this.Is_FilterRecords(g, out filterRecords))
			{
				return func2(filterRecords);
			}
			dataLines_skippedRecords dataLines_skippedRecords;
			if (this.Is_dataLines_skippedRecords(g, out dataLines_skippedRecords))
			{
				return func3(dataLines_skippedRecords);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol dataLines");
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x000B1300 File Offset: 0x000AF500
		public void Switch(GrammarBuilders g, Action<FilterHeader> func0, Action<SelectData> func1, Action<FilterRecords> func2, Action<dataLines_skippedRecords> func3)
		{
			FilterHeader filterHeader;
			if (this.Is_FilterHeader(g, out filterHeader))
			{
				func0(filterHeader);
				return;
			}
			SelectData selectData;
			if (this.Is_SelectData(g, out selectData))
			{
				func1(selectData);
				return;
			}
			FilterRecords filterRecords;
			if (this.Is_FilterRecords(g, out filterRecords))
			{
				func2(filterRecords);
				return;
			}
			dataLines_skippedRecords dataLines_skippedRecords;
			if (this.Is_dataLines_skippedRecords(g, out dataLines_skippedRecords))
			{
				func3(dataLines_skippedRecords);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol dataLines");
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x000B137F File Offset: 0x000AF57F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x000B1394 File Offset: 0x000AF594
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003923 RID: 14627 RVA: 0x000B13BE File Offset: 0x000AF5BE
		public bool Equals(dataLines other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A89 RID: 6793
		private ProgramNode _node;
	}
}
