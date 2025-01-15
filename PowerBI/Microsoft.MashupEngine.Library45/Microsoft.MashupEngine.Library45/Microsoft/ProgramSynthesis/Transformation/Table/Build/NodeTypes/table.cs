using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes
{
	// Token: 0x02001AB6 RID: 6838
	public struct table : IProgramNodeBuilder, IEquatable<table>
	{
		// Token: 0x170025D5 RID: 9685
		// (get) Token: 0x0600E1F5 RID: 57845 RVA: 0x003008D2 File Offset: 0x002FEAD2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E1F6 RID: 57846 RVA: 0x003008DA File Offset: 0x002FEADA
		private table(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E1F7 RID: 57847 RVA: 0x003008E3 File Offset: 0x002FEAE3
		public static table CreateUnsafe(ProgramNode node)
		{
			return new table(node);
		}

		// Token: 0x0600E1F8 RID: 57848 RVA: 0x003008EC File Offset: 0x002FEAEC
		public static table? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.table)
			{
				return null;
			}
			return new table?(table.CreateUnsafe(node));
		}

		// Token: 0x0600E1F9 RID: 57849 RVA: 0x00300926 File Offset: 0x002FEB26
		public static table CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new table(new Hole(g.Symbol.table, holeId));
		}

		// Token: 0x0600E1FA RID: 57850 RVA: 0x0030093E File Offset: 0x002FEB3E
		public bool Is_table_inputTable(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.table_inputTable;
		}

		// Token: 0x0600E1FB RID: 57851 RVA: 0x00300958 File Offset: 0x002FEB58
		public bool Is_table_inputTable(GrammarBuilders g, out table_inputTable value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.table_inputTable)
			{
				value = table_inputTable.CreateUnsafe(this.Node);
				return true;
			}
			value = default(table_inputTable);
			return false;
		}

		// Token: 0x0600E1FC RID: 57852 RVA: 0x00300990 File Offset: 0x002FEB90
		public table_inputTable? As_table_inputTable(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.table_inputTable)
			{
				return null;
			}
			return new table_inputTable?(table_inputTable.CreateUnsafe(this.Node));
		}

		// Token: 0x0600E1FD RID: 57853 RVA: 0x003009D0 File Offset: 0x002FEBD0
		public table_inputTable Cast_table_inputTable(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.table_inputTable)
			{
				return table_inputTable.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_table_inputTable is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600E1FE RID: 57854 RVA: 0x00300A25 File Offset: 0x002FEC25
		public bool Is_LabelEncode(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LabelEncode;
		}

		// Token: 0x0600E1FF RID: 57855 RVA: 0x00300A3F File Offset: 0x002FEC3F
		public bool Is_LabelEncode(GrammarBuilders g, out LabelEncode value)
		{
			if (this.Node.GrammarRule == g.Rule.LabelEncode)
			{
				value = LabelEncode.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LabelEncode);
			return false;
		}

		// Token: 0x0600E200 RID: 57856 RVA: 0x00300A74 File Offset: 0x002FEC74
		public LabelEncode? As_LabelEncode(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LabelEncode)
			{
				return null;
			}
			return new LabelEncode?(LabelEncode.CreateUnsafe(this.Node));
		}

		// Token: 0x0600E201 RID: 57857 RVA: 0x00300AB4 File Offset: 0x002FECB4
		public LabelEncode Cast_LabelEncode(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LabelEncode)
			{
				return LabelEncode.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LabelEncode is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600E202 RID: 57858 RVA: 0x00300B09 File Offset: 0x002FED09
		public bool Is_OneHotEncode(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.OneHotEncode;
		}

		// Token: 0x0600E203 RID: 57859 RVA: 0x00300B23 File Offset: 0x002FED23
		public bool Is_OneHotEncode(GrammarBuilders g, out OneHotEncode value)
		{
			if (this.Node.GrammarRule == g.Rule.OneHotEncode)
			{
				value = OneHotEncode.CreateUnsafe(this.Node);
				return true;
			}
			value = default(OneHotEncode);
			return false;
		}

		// Token: 0x0600E204 RID: 57860 RVA: 0x00300B58 File Offset: 0x002FED58
		public OneHotEncode? As_OneHotEncode(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.OneHotEncode)
			{
				return null;
			}
			return new OneHotEncode?(OneHotEncode.CreateUnsafe(this.Node));
		}

		// Token: 0x0600E205 RID: 57861 RVA: 0x00300B98 File Offset: 0x002FED98
		public OneHotEncode Cast_OneHotEncode(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.OneHotEncode)
			{
				return OneHotEncode.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_OneHotEncode is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600E206 RID: 57862 RVA: 0x00300BED File Offset: 0x002FEDED
		public bool Is_MultiLabelBinarizer(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.MultiLabelBinarizer;
		}

		// Token: 0x0600E207 RID: 57863 RVA: 0x00300C07 File Offset: 0x002FEE07
		public bool Is_MultiLabelBinarizer(GrammarBuilders g, out MultiLabelBinarizer value)
		{
			if (this.Node.GrammarRule == g.Rule.MultiLabelBinarizer)
			{
				value = MultiLabelBinarizer.CreateUnsafe(this.Node);
				return true;
			}
			value = default(MultiLabelBinarizer);
			return false;
		}

		// Token: 0x0600E208 RID: 57864 RVA: 0x00300C3C File Offset: 0x002FEE3C
		public MultiLabelBinarizer? As_MultiLabelBinarizer(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.MultiLabelBinarizer)
			{
				return null;
			}
			return new MultiLabelBinarizer?(MultiLabelBinarizer.CreateUnsafe(this.Node));
		}

		// Token: 0x0600E209 RID: 57865 RVA: 0x00300C7C File Offset: 0x002FEE7C
		public MultiLabelBinarizer Cast_MultiLabelBinarizer(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.MultiLabelBinarizer)
			{
				return MultiLabelBinarizer.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_MultiLabelBinarizer is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600E20A RID: 57866 RVA: 0x00300CD1 File Offset: 0x002FEED1
		public bool Is_CastColumn(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.CastColumn;
		}

		// Token: 0x0600E20B RID: 57867 RVA: 0x00300CEB File Offset: 0x002FEEEB
		public bool Is_CastColumn(GrammarBuilders g, out CastColumn value)
		{
			if (this.Node.GrammarRule == g.Rule.CastColumn)
			{
				value = CastColumn.CreateUnsafe(this.Node);
				return true;
			}
			value = default(CastColumn);
			return false;
		}

		// Token: 0x0600E20C RID: 57868 RVA: 0x00300D20 File Offset: 0x002FEF20
		public CastColumn? As_CastColumn(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.CastColumn)
			{
				return null;
			}
			return new CastColumn?(CastColumn.CreateUnsafe(this.Node));
		}

		// Token: 0x0600E20D RID: 57869 RVA: 0x00300D60 File Offset: 0x002FEF60
		public CastColumn Cast_CastColumn(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.CastColumn)
			{
				return CastColumn.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_CastColumn is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600E20E RID: 57870 RVA: 0x00300DB5 File Offset: 0x002FEFB5
		public bool Is_FillMissingValues(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.FillMissingValues;
		}

		// Token: 0x0600E20F RID: 57871 RVA: 0x00300DCF File Offset: 0x002FEFCF
		public bool Is_FillMissingValues(GrammarBuilders g, out FillMissingValues value)
		{
			if (this.Node.GrammarRule == g.Rule.FillMissingValues)
			{
				value = FillMissingValues.CreateUnsafe(this.Node);
				return true;
			}
			value = default(FillMissingValues);
			return false;
		}

		// Token: 0x0600E210 RID: 57872 RVA: 0x00300E04 File Offset: 0x002FF004
		public FillMissingValues? As_FillMissingValues(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.FillMissingValues)
			{
				return null;
			}
			return new FillMissingValues?(FillMissingValues.CreateUnsafe(this.Node));
		}

		// Token: 0x0600E211 RID: 57873 RVA: 0x00300E44 File Offset: 0x002FF044
		public FillMissingValues Cast_FillMissingValues(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.FillMissingValues)
			{
				return FillMissingValues.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_FillMissingValues is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600E212 RID: 57874 RVA: 0x00300E99 File Offset: 0x002FF099
		public bool Is_DropColumn(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.DropColumn;
		}

		// Token: 0x0600E213 RID: 57875 RVA: 0x00300EB3 File Offset: 0x002FF0B3
		public bool Is_DropColumn(GrammarBuilders g, out DropColumn value)
		{
			if (this.Node.GrammarRule == g.Rule.DropColumn)
			{
				value = DropColumn.CreateUnsafe(this.Node);
				return true;
			}
			value = default(DropColumn);
			return false;
		}

		// Token: 0x0600E214 RID: 57876 RVA: 0x00300EE8 File Offset: 0x002FF0E8
		public DropColumn? As_DropColumn(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.DropColumn)
			{
				return null;
			}
			return new DropColumn?(DropColumn.CreateUnsafe(this.Node));
		}

		// Token: 0x0600E215 RID: 57877 RVA: 0x00300F28 File Offset: 0x002FF128
		public DropColumn Cast_DropColumn(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.DropColumn)
			{
				return DropColumn.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_DropColumn is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600E216 RID: 57878 RVA: 0x00300F7D File Offset: 0x002FF17D
		public bool Is_DropRows(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.DropRows;
		}

		// Token: 0x0600E217 RID: 57879 RVA: 0x00300F97 File Offset: 0x002FF197
		public bool Is_DropRows(GrammarBuilders g, out DropRows value)
		{
			if (this.Node.GrammarRule == g.Rule.DropRows)
			{
				value = DropRows.CreateUnsafe(this.Node);
				return true;
			}
			value = default(DropRows);
			return false;
		}

		// Token: 0x0600E218 RID: 57880 RVA: 0x00300FCC File Offset: 0x002FF1CC
		public DropRows? As_DropRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.DropRows)
			{
				return null;
			}
			return new DropRows?(DropRows.CreateUnsafe(this.Node));
		}

		// Token: 0x0600E219 RID: 57881 RVA: 0x0030100C File Offset: 0x002FF20C
		public DropRows Cast_DropRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.DropRows)
			{
				return DropRows.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_DropRows is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600E21A RID: 57882 RVA: 0x00301061 File Offset: 0x002FF261
		public bool Is_AddSplitColumns(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.AddSplitColumns;
		}

		// Token: 0x0600E21B RID: 57883 RVA: 0x0030107B File Offset: 0x002FF27B
		public bool Is_AddSplitColumns(GrammarBuilders g, out AddSplitColumns value)
		{
			if (this.Node.GrammarRule == g.Rule.AddSplitColumns)
			{
				value = AddSplitColumns.CreateUnsafe(this.Node);
				return true;
			}
			value = default(AddSplitColumns);
			return false;
		}

		// Token: 0x0600E21C RID: 57884 RVA: 0x003010B0 File Offset: 0x002FF2B0
		public AddSplitColumns? As_AddSplitColumns(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.AddSplitColumns)
			{
				return null;
			}
			return new AddSplitColumns?(AddSplitColumns.CreateUnsafe(this.Node));
		}

		// Token: 0x0600E21D RID: 57885 RVA: 0x003010F0 File Offset: 0x002FF2F0
		public AddSplitColumns Cast_AddSplitColumns(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.AddSplitColumns)
			{
				return AddSplitColumns.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_AddSplitColumns is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600E21E RID: 57886 RVA: 0x00301145 File Offset: 0x002FF345
		public bool Is_AddColumnsFromJson(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.AddColumnsFromJson;
		}

		// Token: 0x0600E21F RID: 57887 RVA: 0x0030115F File Offset: 0x002FF35F
		public bool Is_AddColumnsFromJson(GrammarBuilders g, out AddColumnsFromJson value)
		{
			if (this.Node.GrammarRule == g.Rule.AddColumnsFromJson)
			{
				value = AddColumnsFromJson.CreateUnsafe(this.Node);
				return true;
			}
			value = default(AddColumnsFromJson);
			return false;
		}

		// Token: 0x0600E220 RID: 57888 RVA: 0x00301194 File Offset: 0x002FF394
		public AddColumnsFromJson? As_AddColumnsFromJson(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.AddColumnsFromJson)
			{
				return null;
			}
			return new AddColumnsFromJson?(AddColumnsFromJson.CreateUnsafe(this.Node));
		}

		// Token: 0x0600E221 RID: 57889 RVA: 0x003011D4 File Offset: 0x002FF3D4
		public AddColumnsFromJson Cast_AddColumnsFromJson(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.AddColumnsFromJson)
			{
				return AddColumnsFromJson.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_AddColumnsFromJson is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600E222 RID: 57890 RVA: 0x0030122C File Offset: 0x002FF42C
		public T Switch<T>(GrammarBuilders g, Func<table_inputTable, T> func0, Func<LabelEncode, T> func1, Func<OneHotEncode, T> func2, Func<MultiLabelBinarizer, T> func3, Func<CastColumn, T> func4, Func<FillMissingValues, T> func5, Func<DropColumn, T> func6, Func<DropRows, T> func7, Func<AddSplitColumns, T> func8, Func<AddColumnsFromJson, T> func9)
		{
			table_inputTable table_inputTable;
			if (this.Is_table_inputTable(g, out table_inputTable))
			{
				return func0(table_inputTable);
			}
			LabelEncode labelEncode;
			if (this.Is_LabelEncode(g, out labelEncode))
			{
				return func1(labelEncode);
			}
			OneHotEncode oneHotEncode;
			if (this.Is_OneHotEncode(g, out oneHotEncode))
			{
				return func2(oneHotEncode);
			}
			MultiLabelBinarizer multiLabelBinarizer;
			if (this.Is_MultiLabelBinarizer(g, out multiLabelBinarizer))
			{
				return func3(multiLabelBinarizer);
			}
			CastColumn castColumn;
			if (this.Is_CastColumn(g, out castColumn))
			{
				return func4(castColumn);
			}
			FillMissingValues fillMissingValues;
			if (this.Is_FillMissingValues(g, out fillMissingValues))
			{
				return func5(fillMissingValues);
			}
			DropColumn dropColumn;
			if (this.Is_DropColumn(g, out dropColumn))
			{
				return func6(dropColumn);
			}
			DropRows dropRows;
			if (this.Is_DropRows(g, out dropRows))
			{
				return func7(dropRows);
			}
			AddSplitColumns addSplitColumns;
			if (this.Is_AddSplitColumns(g, out addSplitColumns))
			{
				return func8(addSplitColumns);
			}
			AddColumnsFromJson addColumnsFromJson;
			if (this.Is_AddColumnsFromJson(g, out addColumnsFromJson))
			{
				return func9(addColumnsFromJson);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol table");
		}

		// Token: 0x0600E223 RID: 57891 RVA: 0x0030132C File Offset: 0x002FF52C
		public void Switch(GrammarBuilders g, Action<table_inputTable> func0, Action<LabelEncode> func1, Action<OneHotEncode> func2, Action<MultiLabelBinarizer> func3, Action<CastColumn> func4, Action<FillMissingValues> func5, Action<DropColumn> func6, Action<DropRows> func7, Action<AddSplitColumns> func8, Action<AddColumnsFromJson> func9)
		{
			table_inputTable table_inputTable;
			if (this.Is_table_inputTable(g, out table_inputTable))
			{
				func0(table_inputTable);
				return;
			}
			LabelEncode labelEncode;
			if (this.Is_LabelEncode(g, out labelEncode))
			{
				func1(labelEncode);
				return;
			}
			OneHotEncode oneHotEncode;
			if (this.Is_OneHotEncode(g, out oneHotEncode))
			{
				func2(oneHotEncode);
				return;
			}
			MultiLabelBinarizer multiLabelBinarizer;
			if (this.Is_MultiLabelBinarizer(g, out multiLabelBinarizer))
			{
				func3(multiLabelBinarizer);
				return;
			}
			CastColumn castColumn;
			if (this.Is_CastColumn(g, out castColumn))
			{
				func4(castColumn);
				return;
			}
			FillMissingValues fillMissingValues;
			if (this.Is_FillMissingValues(g, out fillMissingValues))
			{
				func5(fillMissingValues);
				return;
			}
			DropColumn dropColumn;
			if (this.Is_DropColumn(g, out dropColumn))
			{
				func6(dropColumn);
				return;
			}
			DropRows dropRows;
			if (this.Is_DropRows(g, out dropRows))
			{
				func7(dropRows);
				return;
			}
			AddSplitColumns addSplitColumns;
			if (this.Is_AddSplitColumns(g, out addSplitColumns))
			{
				func8(addSplitColumns);
				return;
			}
			AddColumnsFromJson addColumnsFromJson;
			if (this.Is_AddColumnsFromJson(g, out addColumnsFromJson))
			{
				func9(addColumnsFromJson);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol table");
		}

		// Token: 0x0600E224 RID: 57892 RVA: 0x00301429 File Offset: 0x002FF629
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E225 RID: 57893 RVA: 0x0030143C File Offset: 0x002FF63C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E226 RID: 57894 RVA: 0x00301466 File Offset: 0x002FF666
		public bool Equals(table other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005575 RID: 21877
		private ProgramNode _node;
	}
}
