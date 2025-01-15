using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.Mashup.Engine1.Library.ExcelTableInference
{
	// Token: 0x0200001D RID: 29
	public class ProgramSynthesisSpreadsheetAdapter : AbstractSpreadsheet
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004577 File Offset: 0x00002777
		public ITableValue TableValue { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000457F File Offset: 0x0000277F
		public Vector<TableUnit> Offset { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004587 File Offset: 0x00002787
		public override Vector<TableUnit> FreezePaneSize { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000458F File Offset: 0x0000278F
		public override IReadOnlyList<DefinedRange> DefinedRanges { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004597 File Offset: 0x00002797
		public override Vector<TableUnit> Size { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000459F File Offset: 0x0000279F
		public RectangularArray<ISpreadsheetCell> Cells { get; }

		// Token: 0x17000030 RID: 48
		public override ISpreadsheetCell this[int x, int y]
		{
			get
			{
				return this.Cells[x, y];
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000045C8 File Offset: 0x000027C8
		public ProgramSynthesisSpreadsheetAdapter(ITableValue tableValue, IReadOnlyCollection<DefinedRange> definedRanges, bool loadFormatting)
		{
			this.TableValue = tableValue;
			this.Size = new Vector<TableUnit>((tableValue.RowCount == 0L) ? 0 : tableValue[0].AsRecord.Keys.Length, (int)tableValue.RowCount);
			List<DefinedRange> list = new List<DefinedRange>(definedRanges.Count);
			HashSet<string> hashSet = new HashSet<string>();
			int rangeIndex;
			if (loadFormatting && ProgramSynthesisSpreadsheetAdapter.HasRange.GetOrAdd(tableValue.MetaValue.Keys, (IKeys keys) => keys.TryGetIndex("Range", out rangeIndex)))
			{
				IRecordValue asRecord = tableValue.MetaValue["Range"].AsRecord;
				this.Offset = new Vector<TableUnit>(asRecord["SkippedColumnCount"].AsNumber.AsInteger32, asRecord["SkippedRowCount"].AsNumber.AsInteger32);
				Func<IValue, int?> func = delegate(IValue value)
				{
					if (!value.IsNumber)
					{
						return null;
					}
					return new int?(value.AsNumber.AsInteger32);
				};
				int? num = func(tableValue.MetaValue["FrozenRowCount"]);
				int? num2 = func(tableValue.MetaValue["FrozenColumnCount"]);
				if (num != null || num2 != null)
				{
					this.FreezePaneSize = new Vector<TableUnit>(num2.GetValueOrDefault(), num.GetValueOrDefault());
				}
				else
				{
					this.FreezePaneSize = null;
				}
				hashSet.UnionWith(from v in tableValue.MetaValue["HiddenColumns"].AsList.GetEnumerable()
					select v.Value.AsString);
				int num3;
				if (tableValue.MetaValue.Keys.TryGetIndex("AutoFilterRange", out num3))
				{
					IValue value2 = tableValue.MetaValue[num3];
					if (value2.IsRecord)
					{
						IRecordValue asRecord2 = value2.AsRecord;
						Vector<TableUnit> vector = new Vector<TableUnit>(asRecord2["SkippedColumnCount"].AsNumber.AsInteger32, asRecord2["SkippedRowCount"].AsNumber.AsInteger32);
						Vector<TableUnit> vector2 = new Vector<TableUnit>(asRecord2["ColumnCount"].AsNumber.AsInteger32 - 1, asRecord2["RowCount"].AsNumber.AsInteger32 - 1);
						Vector<TableUnit> vector3 = vector - this.Offset;
						Bounds<TableUnit> bounds = new Bounds<TableUnit>(vector3, vector3 + vector2);
						list.Add(new DefinedRange(bounds, "AutoFilter", true, "!autoFilter", true));
					}
				}
			}
			else
			{
				this.Offset = new Vector<TableUnit>(0, 0);
				this.FreezePaneSize = null;
			}
			list.AddRange(definedRanges.Select((DefinedRange dr) => new DefinedRange(dr.Span - this.Offset, dr.Kind, dr.Hidden, dr.Name, dr.InternalName)));
			this.DefinedRanges = list;
			this.Cells = this.BuildCells(hashSet, loadFormatting);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004890 File Offset: 0x00002A90
		private RectangularArray<ISpreadsheetCell> BuildCells(HashSet<string> hiddenColumns, bool loadFormatting)
		{
			RectangularArray<ISpreadsheetCell> rectangularArray = new RectangularArray<ISpreadsheetCell>(this.Size);
			int num = 0;
			Dictionary<Vector<TableUnit>, ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell> dictionary = new Dictionary<Vector<TableUnit>, ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell>();
			foreach (IValueReference2 valueReference in this.TableValue)
			{
				IRecordValue asRecord = valueReference.Value.AsRecord;
				IValue value;
				bool flag = loadFormatting && asRecord.TryGetMetaField("Hidden", out value) && value.AsBoolean;
				int num2 = 0;
				ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell spreadsheetCell = null;
				foreach (string text in asRecord.Keys)
				{
					bool flag2 = hiddenColumns.Contains(text);
					Vector<TableUnit> vector = new Vector<TableUnit>(num2, num);
					IValue value2 = null;
					bool flag3 = false;
					try
					{
						value2 = asRecord[text];
					}
					catch (ValueException)
					{
						flag3 = true;
					}
					ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell spreadsheetCell2 = new ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell(num2, num, value2, flag3, flag, flag2, loadFormatting);
					ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell spreadsheetCell3;
					if (dictionary.TryGetValue(vector, out spreadsheetCell3))
					{
						rectangularArray[vector] = new MergedSpreadsheetCellProxy(spreadsheetCell3, spreadsheetCell2);
						dictionary.Remove(vector);
					}
					else
					{
						rectangularArray[vector] = spreadsheetCell2;
						if (spreadsheetCell2.Span.Area() > 1)
						{
							foreach (Vector<TableUnit> vector2 in spreadsheetCell2.Span.AsEnumerable(Axis.Vertical, Derivative.Increasing))
							{
								if (!(vector == vector2))
								{
									dictionary.Add(vector2, spreadsheetCell2);
								}
							}
						}
						if (spreadsheetCell2.IsCenterContinuous)
						{
							if (string.IsNullOrWhiteSpace(spreadsheetCell2.AsString))
							{
								if (spreadsheetCell != null)
								{
									rectangularArray[vector] = new MergedSpreadsheetCellProxy(spreadsheetCell, spreadsheetCell2);
									spreadsheetCell.Span = spreadsheetCell.Span.With(Direction.Right, num2);
								}
							}
							else
							{
								spreadsheetCell = spreadsheetCell2;
							}
						}
						else
						{
							spreadsheetCell = null;
						}
					}
					num2++;
				}
				num++;
			}
			return rectangularArray;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004AE4 File Offset: 0x00002CE4
		public static IReadOnlyList<KeyValuePair<string, ProgramSynthesisSpreadsheetAdapter.Pair>> ReadExcelWorkbook(ITableValue excelWorkbookValue, out List<string> tableNames)
		{
			tableNames = new List<string>();
			List<KeyValuePair<string, ProgramSynthesisSpreadsheetAdapter.Pair>> list = new List<KeyValuePair<string, ProgramSynthesisSpreadsheetAdapter.Pair>>();
			Record<int?, int?, int?, int?> orAdd = ProgramSynthesisSpreadsheetAdapter.WorkbookKeyIndexes.GetOrAdd(excelWorkbookValue[0].AsRecord.Keys, delegate(IKeys keys)
			{
				int? num3 = null;
				int? num4 = null;
				int? num5 = null;
				int? num6 = null;
				foreach (Record<int, string> record in keys.Enumerate<string>())
				{
					int item5 = record.Item1;
					string item6 = record.Item2;
					if (!(item6 == "Kind"))
					{
						if (!(item6 == "Name"))
						{
							if (!(item6 == "Data"))
							{
								if (item6 == "Hidden")
								{
									num6 = new int?(item5);
								}
							}
							else
							{
								num5 = new int?(item5);
							}
						}
						else
						{
							num4 = new int?(item5);
						}
					}
					else
					{
						num3 = new int?(item5);
					}
				}
				return new Record<int?, int?, int?, int?>(num3, num4, num5, num6);
			});
			int? item = orAdd.Item1;
			int? item2 = orAdd.Item2;
			int? item3 = orAdd.Item3;
			int? item4 = orAdd.Item4;
			if (item2 == null || item3 == null)
			{
				return list;
			}
			Dictionary<string, ITableValue> dictionary = new Dictionary<string, ITableValue>();
			MultiValueDictionary<string, DefinedRange> multiValueDictionary = new MultiValueDictionary<string, DefinedRange>();
			int num = 0;
			while ((long)num < excelWorkbookValue.RowCount)
			{
				IRecordValue asRecord = excelWorkbookValue[num].AsRecord;
				ITableValue asTable = asRecord[item3.Value].AsTable;
				if (asTable.RowCount > 0L)
				{
					string text = ((item != null) ? asRecord[item.Value].AsString : null);
					if (text != "Sheet")
					{
						string asString = asRecord[item2.Value].AsString;
						bool flag = item4 == null || asRecord[item4.Value].AsBoolean;
						if (!flag)
						{
							tableNames.Add(asString);
						}
						IRecordValue metaValue = asTable.MetaValue;
						string asString2 = metaValue["SheetName"].AsString;
						IRecordValue asRecord2 = metaValue["Range"].AsRecord;
						int asInteger = asRecord2["SkippedRowCount"].AsNumber.AsInteger32;
						int asInteger2 = asRecord2["SkippedColumnCount"].AsNumber.AsInteger32;
						int asInteger3 = asRecord2["RowCount"].AsNumber.AsInteger32;
						int asInteger4 = asRecord2["ColumnCount"].AsNumber.AsInteger32;
						MultiValueDictionary<string, DefinedRange> multiValueDictionary2 = multiValueDictionary;
						string text2 = asString2;
						int num2 = asInteger;
						multiValueDictionary2.Add(text2, new DefinedRange(new Bounds<TableUnit>(asInteger2, asInteger2 + asInteger4 - 1, num2, asInteger + asInteger3 - 1), text, flag, asString, false));
					}
					else
					{
						string asString3 = asRecord[item2.Value].AsString;
						dictionary[asString3] = asTable;
					}
				}
				num++;
			}
			foreach (KeyValuePair<string, ITableValue> keyValuePair in dictionary)
			{
				string key = keyValuePair.Key;
				ITableValue value = keyValuePair.Value;
				IReadOnlyCollection<DefinedRange> orEmpty = multiValueDictionary.GetOrEmpty(key);
				ProgramSynthesisSpreadsheetAdapter.Pair pair = new ProgramSynthesisSpreadsheetAdapter.Pair(value, orEmpty);
				list.Add(new KeyValuePair<string, ProgramSynthesisSpreadsheetAdapter.Pair>(key, pair));
			}
			return list;
		}

		// Token: 0x0400007B RID: 123
		private static readonly Dictionary<IKeys, bool> HasRange = new Dictionary<IKeys, bool>(IdentityEquality.Comparer);

		// Token: 0x0400007C RID: 124
		private static readonly Dictionary<IKeys, Record<int?, int?, int?, int?>> WorkbookKeyIndexes = new Dictionary<IKeys, Record<int?, int?, int?, int?>>();

		// Token: 0x0200001E RID: 30
		private class SpreadsheetCell : ISpreadsheetCell, ICellStyleInfo
		{
			// Token: 0x17000031 RID: 49
			// (get) Token: 0x0600009B RID: 155 RVA: 0x00004DCB File Offset: 0x00002FCB
			public string Formula { get; }

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x0600009C RID: 156 RVA: 0x00004DD3 File Offset: 0x00002FD3
			public string FormulaSharedId { get; }

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x0600009D RID: 157 RVA: 0x00004DDB File Offset: 0x00002FDB
			public string NumberFormat { get; }

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x0600009E RID: 158 RVA: 0x00004DE3 File Offset: 0x00002FE3
			public bool IsError { get; }

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x0600009F RID: 159 RVA: 0x00004DEB File Offset: 0x00002FEB
			// (set) Token: 0x060000A0 RID: 160 RVA: 0x00004DF3 File Offset: 0x00002FF3
			public Bounds<TableUnit> Span { get; set; }

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004DFC File Offset: 0x00002FFC
			public bool RowHidden { get; }

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004E04 File Offset: 0x00003004
			public bool ColumnHidden { get; }

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004E0C File Offset: 0x0000300C
			public AxisAligned<string> Alignment { get; }

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x060000A4 RID: 164 RVA: 0x00004E14 File Offset: 0x00003014
			private ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.CellMetaKeysIndexes.CellMetaReader MetaReader { get; }

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060000A5 RID: 165 RVA: 0x00004E1C File Offset: 0x0000301C
			private ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.FontMetaKeysIndexes.FontReader FontReader { get; }

			// Token: 0x060000A6 RID: 166 RVA: 0x00004E24 File Offset: 0x00003024
			public SpreadsheetCell(int x, int y, IValue value, bool isError, bool rowHidden, bool columnHidden, bool loadFormatting)
			{
				this.value = value;
				this.MetaReader = ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.CellMetaKeysIndexes.CellMetaReader.Create(this.value);
				this.IsError = isError || this.MetaReader.IsError;
				if (!loadFormatting)
				{
					this.MetaReader = ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.CellMetaKeysIndexes.CellMetaReader.Null;
				}
				this.RowHidden = rowHidden;
				this.ColumnHidden = columnHidden;
				if (this.MetaReader.RowSpan != null)
				{
					this.Span = new Bounds<TableUnit>(x, x + this.MetaReader.ColumnSpan.Value - 1, y, y + this.MetaReader.RowSpan.Value - 1);
				}
				else
				{
					this.Span = new Bounds<TableUnit>(x, x, y, y);
				}
				string horizontalAlignment = this.MetaReader.HorizontalAlignment;
				string verticalAlignment = this.MetaReader.VerticalAlignment;
				if (horizontalAlignment != null || verticalAlignment != null)
				{
					this.Alignment = new AxisAligned<string>(horizontalAlignment, verticalAlignment);
				}
				this.Formula = this.MetaReader.Formula;
				this.FormulaSharedId = this.MetaReader.FormulaSharedId;
				this.NumberFormat = this.MetaReader.NumberFormat;
				this.FontReader = this.MetaReader.Font;
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060000A7 RID: 167 RVA: 0x00004F56 File Offset: 0x00003156
			public bool IsCenterContinuous
			{
				get
				{
					AxisAligned<string> alignment = this.Alignment;
					return ((alignment != null) ? alignment.Horizontal : null) == "centerContinuous";
				}
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004F74 File Offset: 0x00003174
			public string AsString
			{
				get
				{
					if (this.IsError)
					{
						return "ERROR";
					}
					if (this.value == null || this.value.IsNull)
					{
						return null;
					}
					return this.value.ToString();
				}
			}

			// Token: 0x060000A9 RID: 169 RVA: 0x00004FA6 File Offset: 0x000031A6
			public override string ToString()
			{
				return this.AsString;
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060000AA RID: 170 RVA: 0x00004FAE File Offset: 0x000031AE
			public ICellStyleInfo StyleInfo
			{
				get
				{
					return this;
				}
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x060000AB RID: 171 RVA: 0x00004FB1 File Offset: 0x000031B1
			public string FontName
			{
				get
				{
					return this.FontReader.Name;
				}
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060000AC RID: 172 RVA: 0x00004FBE File Offset: 0x000031BE
			public int? FontSize
			{
				get
				{
					return this.FontReader.Size;
				}
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060000AD RID: 173 RVA: 0x00004FCC File Offset: 0x000031CC
			public bool Bold
			{
				get
				{
					return this.FontReader.Bold.GetValueOrDefault();
				}
			}

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x060000AE RID: 174 RVA: 0x00004FEC File Offset: 0x000031EC
			public bool Italic
			{
				get
				{
					return this.FontReader.Italic.GetValueOrDefault();
				}
			}

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x060000AF RID: 175 RVA: 0x0000500C File Offset: 0x0000320C
			public bool Underline
			{
				get
				{
					return this.FontReader.Underline != null;
				}
			}

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000501C File Offset: 0x0000321C
			public bool Strikethrough
			{
				get
				{
					return this.FontReader.Strikethrough.GetValueOrDefault();
				}
			}

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000503C File Offset: 0x0000323C
			public Directed<BorderInfo> Borders
			{
				get
				{
					return this.MetaReader.Borders;
				}
			}

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x060000B2 RID: 178 RVA: 0x00005049 File Offset: 0x00003249
			public FillInfo Fill
			{
				get
				{
					return this.MetaReader.Fill;
				}
			}

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x060000B3 RID: 179 RVA: 0x00005056 File Offset: 0x00003256
			public ColorInfo Color
			{
				get
				{
					return this.FontReader.Color;
				}
			}

			// Token: 0x0400007E RID: 126
			private readonly IValue value;

			// Token: 0x04000089 RID: 137
			private const string CenterContinuous = "centerContinuous";

			// Token: 0x0200001F RID: 31
			private class CellMetaKeysIndexes
			{
				// Token: 0x060000B4 RID: 180 RVA: 0x00005064 File Offset: 0x00003264
				private CellMetaKeysIndexes(IKeys keys)
				{
					foreach (Record<int, string> record in keys.Enumerate<string>())
					{
						int item = record.Item1;
						string item2 = record.Item2;
						if (item2 != null)
						{
							switch (item2.Length)
							{
							case 4:
							{
								char c = item2[1];
								if (c != 'i')
								{
									if (c == 'o')
									{
										if (item2 == "Font")
										{
											this.font = new int?(item);
										}
									}
								}
								else if (item2 == "Fill")
								{
									this.fill = new int?(item);
								}
								break;
							}
							case 5:
								if (item2 == "Error")
								{
									this.error = new int?(item);
								}
								break;
							case 6:
								if (item2 == "Border")
								{
									this.border = new int?(item);
								}
								break;
							case 7:
							{
								char c = item2[0];
								if (c != 'F')
								{
									if (c == 'R')
									{
										if (item2 == "RowSpan")
										{
											this.rowSpan = new int?(item);
										}
									}
								}
								else if (item2 == "Formula")
								{
									this.formula = new int?(item);
								}
								break;
							}
							case 10:
								if (item2 == "ColumnSpan")
								{
									this.columnSpan = new int?(item);
								}
								break;
							case 12:
								if (item2 == "NumberFormat")
								{
									this.numberFormat = new int?(item);
								}
								break;
							case 15:
								if (item2 == "FormulaSharedId")
								{
									this.formulaSharedId = new int?(item);
								}
								break;
							case 17:
								if (item2 == "VerticalAlignment")
								{
									this.verticalAlignment = new int?(item);
								}
								break;
							case 19:
								if (item2 == "HorizontalAlignment")
								{
									this.horizontalAlignment = new int?(item);
								}
								break;
							}
						}
					}
				}

				// Token: 0x0400008A RID: 138
				private readonly int? formula;

				// Token: 0x0400008B RID: 139
				private readonly int? formulaSharedId;

				// Token: 0x0400008C RID: 140
				private readonly int? rowSpan;

				// Token: 0x0400008D RID: 141
				private readonly int? columnSpan;

				// Token: 0x0400008E RID: 142
				private readonly int? font;

				// Token: 0x0400008F RID: 143
				private readonly int? fill;

				// Token: 0x04000090 RID: 144
				private readonly int? border;

				// Token: 0x04000091 RID: 145
				private readonly int? horizontalAlignment;

				// Token: 0x04000092 RID: 146
				private readonly int? verticalAlignment;

				// Token: 0x04000093 RID: 147
				private readonly int? error;

				// Token: 0x04000094 RID: 148
				private readonly int? numberFormat;

				// Token: 0x04000095 RID: 149
				private static readonly Dictionary<IKeys, ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.CellMetaKeysIndexes> CellMetaKeysIndexesLookup = new Dictionary<IKeys, ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.CellMetaKeysIndexes>();

				// Token: 0x02000020 RID: 32
				public class CellMetaReader
				{
					// Token: 0x17000047 RID: 71
					// (get) Token: 0x060000B6 RID: 182 RVA: 0x000052EC File Offset: 0x000034EC
					private IRecordValue CellMetaValue { get; }

					// Token: 0x17000048 RID: 72
					// (get) Token: 0x060000B7 RID: 183 RVA: 0x000052F4 File Offset: 0x000034F4
					private ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.CellMetaKeysIndexes Indexes { get; }

					// Token: 0x060000B8 RID: 184 RVA: 0x000052FC File Offset: 0x000034FC
					private CellMetaReader(IRecordValue cellMetaValue, ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.CellMetaKeysIndexes indexes)
					{
						this.Indexes = indexes;
						this.CellMetaValue = cellMetaValue;
					}

					// Token: 0x17000049 RID: 73
					// (get) Token: 0x060000B9 RID: 185 RVA: 0x00005312 File Offset: 0x00003512
					public static ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.CellMetaKeysIndexes.CellMetaReader Null { get; } = new ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.CellMetaKeysIndexes.CellMetaReader(null, null);

					// Token: 0x060000BA RID: 186 RVA: 0x0000531C File Offset: 0x0000351C
					public static ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.CellMetaKeysIndexes.CellMetaReader Create(IValue cell)
					{
						IRecordValue recordValue = ((cell != null) ? cell.MetaValue : null);
						if (recordValue == null)
						{
							return ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.CellMetaKeysIndexes.CellMetaReader.Null;
						}
						ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.CellMetaKeysIndexes orAdd = ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.CellMetaKeysIndexes.CellMetaKeysIndexesLookup.GetOrAdd(recordValue.Keys, (IKeys keys) => new ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.CellMetaKeysIndexes(keys));
						return new ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.CellMetaKeysIndexes.CellMetaReader(recordValue, orAdd);
					}

					// Token: 0x1700004A RID: 74
					// (get) Token: 0x060000BB RID: 187 RVA: 0x00005376 File Offset: 0x00003576
					public string Formula
					{
						get
						{
							if (this.CellMetaValue == null || this.Indexes.formula == null)
							{
								return null;
							}
							return this.CellMetaValue[this.Indexes.formula.Value].AsString;
						}
					}

					// Token: 0x1700004B RID: 75
					// (get) Token: 0x060000BC RID: 188 RVA: 0x000053B4 File Offset: 0x000035B4
					public string FormulaSharedId
					{
						get
						{
							if (this.CellMetaValue == null || this.Indexes.formulaSharedId == null)
							{
								return null;
							}
							return this.CellMetaValue[this.Indexes.formulaSharedId.Value].AsString;
						}
					}

					// Token: 0x1700004C RID: 76
					// (get) Token: 0x060000BD RID: 189 RVA: 0x000053F4 File Offset: 0x000035F4
					public int? RowSpan
					{
						get
						{
							if (this.CellMetaValue == null || this.Indexes.rowSpan == null)
							{
								return null;
							}
							return new int?(this.CellMetaValue[this.Indexes.rowSpan.Value].AsNumber.AsInteger32);
						}
					}

					// Token: 0x1700004D RID: 77
					// (get) Token: 0x060000BE RID: 190 RVA: 0x00005450 File Offset: 0x00003650
					public int? ColumnSpan
					{
						get
						{
							if (this.CellMetaValue == null || this.Indexes.columnSpan == null)
							{
								return null;
							}
							return new int?(this.CellMetaValue[this.Indexes.columnSpan.Value].AsNumber.AsInteger32);
						}
					}

					// Token: 0x1700004E RID: 78
					// (get) Token: 0x060000BF RID: 191 RVA: 0x000054AB File Offset: 0x000036AB
					public string HorizontalAlignment
					{
						get
						{
							if (this.CellMetaValue == null || this.Indexes.horizontalAlignment == null)
							{
								return null;
							}
							return this.CellMetaValue[this.Indexes.horizontalAlignment.Value].AsString;
						}
					}

					// Token: 0x1700004F RID: 79
					// (get) Token: 0x060000C0 RID: 192 RVA: 0x000054E9 File Offset: 0x000036E9
					public string VerticalAlignment
					{
						get
						{
							if (this.CellMetaValue == null || this.Indexes.verticalAlignment == null)
							{
								return null;
							}
							return this.CellMetaValue[this.Indexes.verticalAlignment.Value].AsString;
						}
					}

					// Token: 0x17000050 RID: 80
					// (get) Token: 0x060000C1 RID: 193 RVA: 0x00005527 File Offset: 0x00003727
					public string NumberFormat
					{
						get
						{
							if (this.CellMetaValue == null || this.Indexes.numberFormat == null)
							{
								return null;
							}
							return this.CellMetaValue[this.Indexes.numberFormat.Value].AsString;
						}
					}

					// Token: 0x17000051 RID: 81
					// (get) Token: 0x060000C2 RID: 194 RVA: 0x00005568 File Offset: 0x00003768
					public ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.FontMetaKeysIndexes.FontReader Font
					{
						get
						{
							return ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.FontMetaKeysIndexes.FontReader.Create((this.CellMetaValue != null && this.Indexes.font != null) ? this.CellMetaValue[this.Indexes.font.Value].AsRecord : null);
						}
					}

					// Token: 0x17000052 RID: 82
					// (get) Token: 0x060000C3 RID: 195 RVA: 0x000055B8 File Offset: 0x000037B8
					public bool IsError
					{
						get
						{
							return this.CellMetaValue != null && this.Indexes.error != null && !this.CellMetaValue[this.Indexes.error.Value].IsNull;
						}
					}

					// Token: 0x060000C4 RID: 196 RVA: 0x00005604 File Offset: 0x00003804
					public static ColorInfo ReadColorInfo(IValue colorValue)
					{
						if (colorValue == null || colorValue.IsNull || !colorValue.IsRecord)
						{
							return null;
						}
						IRecordValue asRecord = colorValue.AsRecord;
						bool? flag = null;
						IValue value = asRecord["Auto"];
						if (!value.IsNull)
						{
							flag = new bool?(value.AsBoolean);
						}
						int? num = null;
						IValue value2 = asRecord["Indexed"];
						if (!value2.IsNull)
						{
							num = new int?(value2.AsNumber.AsInteger32);
						}
						string text = null;
						IValue value3 = asRecord["RGB"];
						if (!value3.IsNull)
						{
							text = value3.AsString;
						}
						int? num2 = null;
						IValue value4 = asRecord["Theme"];
						if (!value4.IsNull)
						{
							num2 = new int?(value4.AsNumber.AsInteger32);
						}
						double? num3 = null;
						IValue value5 = asRecord["Tint"];
						if (!value5.IsNull)
						{
							num3 = new double?(value5.AsNumber.AsDouble);
						}
						return new ColorInfo(flag, num, text, num2, num3);
					}

					// Token: 0x17000053 RID: 83
					// (get) Token: 0x060000C5 RID: 197 RVA: 0x00005714 File Offset: 0x00003914
					public Directed<BorderInfo> Borders
					{
						get
						{
							if (this.CellMetaValue == null)
							{
								return null;
							}
							if (this.Indexes.border == null)
							{
								return null;
							}
							int value = this.Indexes.border.Value;
							IRecordValue borders = this.CellMetaValue[value].AsRecord;
							return new Directed<BorderInfo>(delegate(Direction direction)
							{
								IValue value2 = borders[ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.CellMetaKeysIndexes.CellMetaReader.BorderDirectionNames[direction]];
								if (value2.IsNull || !value2.IsRecord)
								{
									return default(BorderInfo);
								}
								IRecordValue asRecord = value2.AsRecord;
								return new BorderInfo(asRecord["Style"].AsString, ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.CellMetaKeysIndexes.CellMetaReader.ReadColorInfo(asRecord["Color"]));
							});
						}
					}

					// Token: 0x17000054 RID: 84
					// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005780 File Offset: 0x00003980
					public FillInfo Fill
					{
						get
						{
							if (this.CellMetaValue == null)
							{
								return default(FillInfo);
							}
							if (this.Indexes.fill == null)
							{
								return default(FillInfo);
							}
							int value = this.Indexes.fill.Value;
							IRecordValue asRecord = this.CellMetaValue[value].AsRecord;
							return new FillInfo(asRecord["PatternType"].AsString, ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.CellMetaKeysIndexes.CellMetaReader.ReadColorInfo(asRecord["FgColor"]), ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.CellMetaKeysIndexes.CellMetaReader.ReadColorInfo(asRecord["BgColor"]));
						}
					}

					// Token: 0x04000099 RID: 153
					private static readonly Directed<string> BorderDirectionNames = new Directed<string>("Left", "Right", "Top", "Bottom");
				}
			}

			// Token: 0x02000023 RID: 35
			private class FontMetaKeysIndexes
			{
				// Token: 0x060000CD RID: 205 RVA: 0x000058C0 File Offset: 0x00003AC0
				private FontMetaKeysIndexes(IKeys keys)
				{
					foreach (Record<int, string> record in keys.Enumerate<string>())
					{
						int item = record.Item1;
						string item2 = record.Item2;
						if (item2 != null)
						{
							int length = item2.Length;
							switch (length)
							{
							case 4:
							{
								char c = item2[0];
								if (c != 'B')
								{
									if (c != 'N')
									{
										if (c == 'S')
										{
											if (item2 == "Size")
											{
												this.size = new int?(item);
											}
										}
									}
									else if (item2 == "Name")
									{
										this.name = new int?(item);
									}
								}
								else if (item2 == "Bold")
								{
									this.bold = new int?(item);
								}
								break;
							}
							case 5:
								if (item2 == "Color")
								{
									this.color = new int?(item);
								}
								break;
							case 6:
								if (item2 == "Italic")
								{
									this.italic = new int?(item);
								}
								break;
							case 7:
							case 8:
								break;
							case 9:
								if (item2 == "Underline")
								{
									this.underline = new int?(item);
								}
								break;
							default:
								if (length == 13)
								{
									if (item2 == "Strikethrough")
									{
										this.strikethrough = new int?(item);
									}
								}
								break;
							}
						}
					}
				}

				// Token: 0x0400009D RID: 157
				private readonly int? name;

				// Token: 0x0400009E RID: 158
				private readonly int? bold;

				// Token: 0x0400009F RID: 159
				private readonly int? italic;

				// Token: 0x040000A0 RID: 160
				private readonly int? underline;

				// Token: 0x040000A1 RID: 161
				private readonly int? strikethrough;

				// Token: 0x040000A2 RID: 162
				private readonly int? size;

				// Token: 0x040000A3 RID: 163
				private readonly int? color;

				// Token: 0x040000A4 RID: 164
				private static readonly Dictionary<IKeys, ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.FontMetaKeysIndexes> FontMetaKeysIndexesLookup = new Dictionary<IKeys, ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.FontMetaKeysIndexes>();

				// Token: 0x02000024 RID: 36
				public class FontReader
				{
					// Token: 0x17000055 RID: 85
					// (get) Token: 0x060000CF RID: 207 RVA: 0x00005A68 File Offset: 0x00003C68
					private IRecordValue FontValue { get; }

					// Token: 0x17000056 RID: 86
					// (get) Token: 0x060000D0 RID: 208 RVA: 0x00005A70 File Offset: 0x00003C70
					private ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.FontMetaKeysIndexes Indexes { get; }

					// Token: 0x060000D1 RID: 209 RVA: 0x00005A78 File Offset: 0x00003C78
					private FontReader(IRecordValue cellMetaValue, ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.FontMetaKeysIndexes indexes)
					{
						this.Indexes = indexes;
						this.FontValue = cellMetaValue;
					}

					// Token: 0x060000D2 RID: 210 RVA: 0x00005A90 File Offset: 0x00003C90
					public static ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.FontMetaKeysIndexes.FontReader Create(IRecordValue font)
					{
						if (font == null)
						{
							return new ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.FontMetaKeysIndexes.FontReader(null, null);
						}
						ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.FontMetaKeysIndexes orAdd = ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.FontMetaKeysIndexes.FontMetaKeysIndexesLookup.GetOrAdd(font.Keys, (IKeys keys) => new ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.FontMetaKeysIndexes(keys));
						return new ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.FontMetaKeysIndexes.FontReader(font, orAdd);
					}

					// Token: 0x17000057 RID: 87
					// (get) Token: 0x060000D3 RID: 211 RVA: 0x00005AE0 File Offset: 0x00003CE0
					public string Name
					{
						get
						{
							if (this.FontValue != null && this.Indexes.name != null)
							{
								IValue value = this.FontValue[this.Indexes.name.Value];
								if (!value.IsNull)
								{
									return value.AsString;
								}
							}
							return null;
						}
					}

					// Token: 0x17000058 RID: 88
					// (get) Token: 0x060000D4 RID: 212 RVA: 0x00005B34 File Offset: 0x00003D34
					public bool? Bold
					{
						get
						{
							if (this.FontValue == null || this.Indexes.bold == null)
							{
								return null;
							}
							return new bool?(this.FontValue[this.Indexes.bold.Value].AsBoolean);
						}
					}

					// Token: 0x17000059 RID: 89
					// (get) Token: 0x060000D5 RID: 213 RVA: 0x00005B8C File Offset: 0x00003D8C
					public bool? Italic
					{
						get
						{
							if (this.FontValue == null || this.Indexes.italic == null)
							{
								return null;
							}
							return new bool?(this.FontValue[this.Indexes.italic.Value].AsBoolean);
						}
					}

					// Token: 0x1700005A RID: 90
					// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005BE4 File Offset: 0x00003DE4
					public string Underline
					{
						get
						{
							if (this.FontValue != null && this.Indexes.underline != null)
							{
								IValue value = this.FontValue[this.Indexes.underline.Value];
								if (!value.IsNull)
								{
									return value.AsString;
								}
							}
							return null;
						}
					}

					// Token: 0x1700005B RID: 91
					// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005C38 File Offset: 0x00003E38
					public bool? Strikethrough
					{
						get
						{
							if (this.FontValue == null || this.Indexes.strikethrough == null)
							{
								return null;
							}
							return new bool?(this.FontValue[this.Indexes.strikethrough.Value].AsBoolean);
						}
					}

					// Token: 0x1700005C RID: 92
					// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005C90 File Offset: 0x00003E90
					public int? Size
					{
						get
						{
							if (this.FontValue != null && this.Indexes.size != null)
							{
								IValue value = this.FontValue[this.Indexes.size.Value];
								if (value.IsNumber)
								{
									return new int?(value.AsNumber.AsInteger32);
								}
							}
							return null;
						}
					}

					// Token: 0x1700005D RID: 93
					// (get) Token: 0x060000D9 RID: 217 RVA: 0x00005CF8 File Offset: 0x00003EF8
					public ColorInfo Color
					{
						get
						{
							if (this.FontValue == null)
							{
								return null;
							}
							if (this.Indexes.color == null)
							{
								return null;
							}
							int value = this.Indexes.color.Value;
							IValue value2 = this.FontValue[value];
							if (value2 == null || value2.IsNull)
							{
								return null;
							}
							return ProgramSynthesisSpreadsheetAdapter.SpreadsheetCell.CellMetaKeysIndexes.CellMetaReader.ReadColorInfo(value2);
						}
					}
				}
			}
		}

		// Token: 0x02000026 RID: 38
		public class Pair : ISpreadsheetPair
		{
			// Token: 0x060000DD RID: 221 RVA: 0x00005D68 File Offset: 0x00003F68
			public Pair(ITableValue value, IReadOnlyCollection<DefinedRange> definedRanges)
			{
				this.WithFormatting = new ProgramSynthesisSpreadsheetAdapter(value, definedRanges, true);
				this.WithoutFormatting = new ProgramSynthesisSpreadsheetAdapter(value, new DefinedRange[0], false);
			}

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x060000DE RID: 222 RVA: 0x00005D91 File Offset: 0x00003F91
			public ISpreadsheet WithFormatting { get; }

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x060000DF RID: 223 RVA: 0x00005D99 File Offset: 0x00003F99
			public ISpreadsheet WithoutFormatting { get; }
		}
	}
}
