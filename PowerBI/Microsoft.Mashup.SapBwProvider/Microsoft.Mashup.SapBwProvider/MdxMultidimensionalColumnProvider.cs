using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000017 RID: 23
	internal sealed class MdxMultidimensionalColumnProvider : MdxColumnProvider
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00004A96 File Offset: 0x00002C96
		public MdxMultidimensionalColumnProvider(SapBwCommand command, MdxCommand mdxCommand, string cubeName)
			: base(command, mdxCommand, cubeName)
		{
			this.propertyKeyToColumnIndex = new Dictionary<int, int>();
			this.propertyNameToColumnIndex = new List<Dictionary<string, KeyValuePair<int, int?>>>();
			this.dimKeyToColumnIndices = new Dictionary<int, List<int>>();
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004AC2 File Offset: 0x00002CC2
		private int TupleCount
		{
			get
			{
				return this.tupleCount;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004ACA File Offset: 0x00002CCA
		private int MeasureCount
		{
			get
			{
				return this.measureCount;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004AD2 File Offset: 0x00002CD2
		public override DbDataReader GetReader()
		{
			return new MdxMultidimensionalColumnProvider.MdxMultidimensionalDataReader(this.command, this.mdxCommand, this, this.firstPageDataProvider);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004AEC File Offset: 0x00002CEC
		public int Add(MdxColumn column, int index = -1)
		{
			int num = base.Add(column);
			MdxColumnType mdxColumnType = column.MdxColumnType;
			List<int> list;
			if (mdxColumnType != MdxColumnType.MandatoryDimensionProperty)
			{
				if (mdxColumnType != MdxColumnType.OptionalDimensionProperty)
				{
					if (index != -1)
					{
						int num2;
						this.propertyNameToColumnIndex[index][column.FieldName] = new KeyValuePair<int, int?>(num, MdxMultidimensionalColumnProvider.measureIndices.TryGetValue(column.FieldName, out num2) ? new int?(num2) : null);
					}
				}
				else
				{
					this.propertyKeyToColumnIndex[column.PropertyKey] = num;
				}
			}
			else if (this.dimKeyToColumnIndices.TryGetValue(column.PropertyKey, out list))
			{
				list.Add(num);
			}
			else
			{
				this.dimKeyToColumnIndices[column.PropertyKey] = new List<int> { num };
			}
			return num;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004BAC File Offset: 0x00002DAC
		public override bool TryRetrieveColumns(string datasetId, ColumnInfos columnInfos)
		{
			this.EnsureInitialized(datasetId);
			IRfcTable table = this.getAxisInfo.GetTable(4);
			for (int i = table.RowCount - 1; i >= 0; i--)
			{
				table.CurrentIndex = i;
				int @int = table.GetInt(0);
				if (@int != 0)
				{
					if (@int != 1)
					{
						if (@int == 255)
						{
							goto IL_0151;
						}
					}
					else
					{
						using (IEnumerator<MdxColumn> enumerator = this.GetDimensions(columnInfos, this.getAxisInfo.GetTable(3), this.getAxisInfo.GetTable(6), this.getAxisInfo.GetTable(5)).GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								MdxColumn mdxColumn = enumerator.Current;
								this.Add(mdxColumn, -1);
							}
							goto IL_0151;
						}
					}
					throw new SapBwException(Resources.ExtraAxesFound(@int));
				}
				int int2 = table.GetInt(2);
				int num = -1;
				foreach (MdxColumn mdxColumn2 in this.GetMeasuresAndCellProperties(columnInfos))
				{
					if (mdxColumn2.MdxColumnType == MdxColumnType.KeyFigureValue)
					{
						num++;
						this.propertyNameToColumnIndex.Add(new Dictionary<string, KeyValuePair<int, int?>>());
					}
					this.Add(mdxColumn2, num);
				}
				if (int2 != num + 1)
				{
					throw new SapBwException(Resources.IncorrectMeasureCount(int2, @int, num));
				}
				IL_0151:;
			}
			return true;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004D34 File Offset: 0x00002F34
		private void EnsureInitialized(string mdxDataSetId)
		{
			if (this.dataSetId == null)
			{
				this.dataSetId = mdxDataSetId;
				this.getAxisInfo = this.connection.GetFunction("BAPI_MDDATASET_GET_AXIS_INFO", true);
				this.getAxisInfo.SetValue(2, this.dataSetId);
				this.connection.InvokeFunction(this.getAxisInfo, true, this.command, true);
				int num = 0;
				this.measureCount = 0;
				this.rowCount = 0;
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				foreach (IRfcStructure rfcStructure in this.getAxisInfo.GetTable(4))
				{
					dictionary[rfcStructure.GetInt(0)] = rfcStructure.GetInt(2);
				}
				if (!dictionary.TryGetValue(1, out this.rowCount) && (dictionary.ContainsKey(0) || dictionary.ContainsKey(255)))
				{
					this.rowCount = 1;
				}
				Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
				foreach (IRfcStructure rfcStructure2 in this.getAxisInfo.GetTable(3))
				{
					int @int = rfcStructure2.GetInt(0);
					if (@int != 1)
					{
						if (@int == 255)
						{
							if (string.Equals(rfcStructure2.GetString(1), "[Measures]", StringComparison.OrdinalIgnoreCase))
							{
								this.measureCount++;
								this.hasMeasureInDefaultAxis = true;
							}
						}
					}
					else
					{
						dictionary2.Add(rfcStructure2.GetInt(2), rfcStructure2.GetInt(3));
					}
				}
				foreach (IGrouping<int, IRfcStructure> grouping in from l in this.getAxisInfo.GetTable(5)
					where l.GetInt(0) == 1
					group l by l.GetInt(1))
				{
					int num2;
					if (dictionary2.TryGetValue(grouping.Key, out num2))
					{
						num += num2 * grouping.Count<IRfcStructure>();
					}
				}
				this.measureCount += this.getAxisInfo.GetTable(7).RowCount;
				num += this.measureCount * 3;
				if (this.command.SapBwConnection.BatchSize < 200000)
				{
					this.tupleCount = this.command.SapBwConnection.BatchSize;
				}
				else if (num > 0)
				{
					this.tupleCount = ((this.command.SapBwConnection.BatchSize * num > 1000000) ? (1000000 / num) : 1000000);
					this.tupleCount = ((this.tupleCount < 5) ? 5 : Math.Min(this.tupleCount, 200000));
				}
				else
				{
					this.tupleCount = 200000;
				}
				this.firstPageDataProvider = MdxMultidimensionalColumnProvider.MdxMultidimensionalDataProvider.New(this.command, this.mdxCommand, 0, 0, this.tupleCount, this.measureCount, new bool?(true));
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005058 File Offset: 0x00003258
		private IEnumerable<MdxColumn> GetDimensions(ColumnInfos columnInfos, IRfcTable axisDimensions, IRfcTable dimensionProperties, IRfcTable axisLevels)
		{
			foreach (IRfcStructure rfcStructure in axisDimensions)
			{
				int @int = rfcStructure.GetInt(0);
				Dictionary<string, int> levelToDimKey;
				Dictionary<string, int> columnNameToPropertyKey;
				if (@int != 0)
				{
					if (@int != 1)
					{
						if (@int == 255)
						{
							if (this.hasMeasureInDefaultAxis)
							{
								string text = this.GetMandatoryPropertiesMemberNames(255).FirstOrDefault<string>() ?? Utils.BuildColumnName("[Measures]", "[Default]");
								foreach (MdxColumn mdxColumn in this.GetMeasure(text, null))
								{
									if (mdxColumn.MdxColumnType == MdxColumnType.KeyFigureValue)
									{
										this.propertyNameToColumnIndex.Add(new Dictionary<string, KeyValuePair<int, int?>>());
									}
									this.Add(mdxColumn, 0);
								}
							}
						}
					}
					else
					{
						levelToDimKey = null;
						Dictionary<int, string> dictionary = null;
						foreach (IRfcStructure rfcStructure2 in this.GetFirstTuple())
						{
							if (levelToDimKey == null)
							{
								levelToDimKey = new Dictionary<string, int>();
								dictionary = new Dictionary<int, string>();
							}
							string @string = rfcStructure2.GetString(4);
							int int2 = rfcStructure2.GetInt(1);
							dictionary[int2] = @string;
							levelToDimKey[@string] = int2;
						}
						columnNameToPropertyKey = null;
						if (dictionary != null && this.firstPageDataProvider.RowsOptionalPropertyKeys != null && this.firstPageDataProvider.RowsOptionalPropertyKeys.RowCount > 0)
						{
							foreach (IRfcStructure rfcStructure3 in this.firstPageDataProvider.RowsOptionalPropertyKeys)
							{
								string string2 = rfcStructure3.GetString(1);
								if (!MdxMultidimensionalColumnProvider.mandatoryDimensionProperties.ContainsKey(string2))
								{
									if (columnNameToPropertyKey == null)
									{
										columnNameToPropertyKey = new Dictionary<string, int>();
									}
									string text2;
									if (dictionary.TryGetValue(rfcStructure3.GetInt(0), out text2))
									{
										columnNameToPropertyKey[Utils.BuildColumnName(text2, string2)] = rfcStructure3.GetInt(2);
									}
								}
							}
						}
						axisLevels.TrySetCurrentIndex(0);
						dimensionProperties.TrySetCurrentIndex(0);
						foreach (IRfcStructure rfcStructure4 in MdxMultidimensionalColumnProvider.SequentialSelect(axisLevels, 1, rfcStructure.GetInt(2)))
						{
							string levelUniqueName = rfcStructure4.GetString(3);
							foreach (IRfcStructure rfcStructure5 in MdxMultidimensionalColumnProvider.SequentialSelect(dimensionProperties, 0, rfcStructure4.GetInt(2)))
							{
								string string3 = rfcStructure5.GetString(1);
								string text3 = Utils.BuildColumnName(levelUniqueName, string3);
								if (columnInfos == null || columnInfos.ColumnNames.Contains(text3))
								{
									int num = -1;
									MdxColumnType mdxColumnType = (MdxMultidimensionalColumnProvider.mandatoryDimensionProperties.ContainsKey(string3) ? MdxColumnType.MandatoryDimensionProperty : MdxColumnType.OptionalDimensionProperty);
									if (mdxColumnType == MdxColumnType.MandatoryDimensionProperty)
									{
										if (levelToDimKey != null && !levelToDimKey.TryGetValue(levelUniqueName, out num))
										{
											continue;
										}
									}
									else if (columnNameToPropertyKey != null && !columnNameToPropertyKey.TryGetValue(text3, out num))
									{
										continue;
									}
									MdxColumnType mdxColumnType2 = mdxColumnType;
									string text4 = text3;
									string string4 = rfcStructure5.GetString(2);
									int int3 = rfcStructure5.GetInt(3);
									string text5 = string3;
									int num2 = num;
									yield return new MdxColumn(mdxColumnType2, text4, string4, int3, null, text5, num2);
								}
							}
							IEnumerator<IRfcStructure> enumerator5 = null;
							levelUniqueName = null;
						}
						IEnumerator<IRfcStructure> enumerator4 = null;
					}
				}
				levelToDimKey = null;
				columnNameToPropertyKey = null;
			}
			IEnumerator<IRfcStructure> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005085 File Offset: 0x00003285
		private IEnumerable<IRfcStructure> GetFirstTuple()
		{
			if (this.firstPageDataProvider.RowsMandatoryProperties != null)
			{
				int? firstTupleOrdinal = null;
				foreach (IRfcStructure rfcStructure in this.firstPageDataProvider.RowsMandatoryProperties)
				{
					int @int = rfcStructure.GetInt(0);
					if (firstTupleOrdinal == null)
					{
						firstTupleOrdinal = new int?(@int);
					}
					else
					{
						int num = @int;
						int? num2 = firstTupleOrdinal;
						if (!((num == num2.GetValueOrDefault()) & (num2 != null)))
						{
							yield break;
						}
					}
					yield return rfcStructure;
				}
				IEnumerator<IRfcStructure> enumerator = null;
				firstTupleOrdinal = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005095 File Offset: 0x00003295
		private IEnumerable<MdxColumn> GetMeasuresAndCellProperties(ColumnInfos columnInfos)
		{
			HashSet<string> cellProperties = null;
			foreach (string text in this.GetMandatoryPropertiesMemberNames(0))
			{
				if (cellProperties == null)
				{
					cellProperties = new HashSet<string>(this.GetCellProperties(text, columnInfos));
					if (this.hasCustomCellProperties == null)
					{
						foreach (string text2 in cellProperties)
						{
							if (!MdxMultidimensionalColumnProvider.measureIndices.ContainsKey(text2) && text2 != "UNIT_OF_MEASURE")
							{
								this.hasCustomCellProperties = new bool?(true);
								break;
							}
						}
					}
					if (this.hasCustomCellProperties == null)
					{
						this.hasCustomCellProperties = new bool?(false);
					}
				}
				foreach (MdxColumn mdxColumn in this.GetMeasure(text, cellProperties))
				{
					yield return mdxColumn;
				}
				IEnumerator<MdxColumn> enumerator3 = null;
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000050AC File Offset: 0x000032AC
		private IEnumerable<MdxColumn> GetMeasure(string memberUniqueName, HashSet<string> cellProperties = null)
		{
			yield return new MdxColumn(MdxColumnType.KeyFigureValue, memberUniqueName, "FLTP", 22, null, "VALUE", -1);
			if (cellProperties != null)
			{
				foreach (string text in cellProperties.Where((string p) => p != "VALUE"))
				{
					string text2 = Utils.BuildColumnName(memberUniqueName, text);
					if (!base.ContainsColumn(text2))
					{
						MdxColumnType mdxColumnType = MdxColumnType.CellProperty;
						string text3 = text2;
						string text4 = "CHAR";
						int num = 0;
						string text5 = text;
						yield return new MdxColumn(mdxColumnType, text3, text4, num, null, text5, -1);
					}
				}
				IEnumerator<string> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000050CA File Offset: 0x000032CA
		private IEnumerable<string> GetMandatoryPropertiesMemberNames(int axis)
		{
			IRfcFunction function = this.connection.GetFunction("BAPI_MDDATASET_GET_AXIS_DATA", true);
			function.SetValue(3, this.mdxCommand.DataSetId);
			function.SetValue(2, axis);
			function.SetParameterActive(7, false);
			function.SetParameterActive(8, false);
			this.connection.InvokeFunction(function, true, this.command, true);
			foreach (IRfcStructure rfcStructure in function.GetTable(6))
			{
				yield return rfcStructure.GetString(2);
			}
			IEnumerator<IRfcStructure> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000050E1 File Offset: 0x000032E1
		private IEnumerable<string> GetCellProperties(string memberUniqueName, ColumnInfos columnInfos)
		{
			foreach (IRfcStructure rfcStructure in this.firstPageDataProvider.CellPropertyNames)
			{
				string @string = rfcStructure.GetString(0);
				if (columnInfos != null)
				{
					if (columnInfos.ContainsCellProperty(memberUniqueName, @string))
					{
						yield return @string;
					}
				}
				else
				{
					yield return @string;
				}
			}
			IEnumerator<IRfcStructure> enumerator = null;
			if (columnInfos != null)
			{
				if (columnInfos.ContainsCellProperty(memberUniqueName, "UNIT_OF_MEASURE"))
				{
					yield return "UNIT_OF_MEASURE";
				}
			}
			else
			{
				foreach (string text in MdxMultidimensionalColumnProvider.nativeQueryCellProperties)
				{
					yield return text;
				}
				HashSet<string>.Enumerator enumerator2 = default(HashSet<string>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000050FF File Offset: 0x000032FF
		private bool TryGetRequestedMeasureProperties(int i, out Dictionary<string, KeyValuePair<int, int?>> mapping)
		{
			if (i >= 0 && i < this.propertyNameToColumnIndex.Count)
			{
				mapping = this.propertyNameToColumnIndex[i];
				return true;
			}
			mapping = null;
			return false;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005127 File Offset: 0x00003327
		private static IEnumerable<IRfcStructure> SequentialSelect(IRfcTable table, int key, int value)
		{
			if (table.RowCount <= 0)
			{
				yield break;
			}
			int num;
			for (int i = table.CurrentIndex; i < table.RowCount; i = num + 1)
			{
				table.CurrentIndex = i;
				int @int = table.GetInt(key);
				if (@int > value)
				{
					yield break;
				}
				if (@int == value)
				{
					yield return table.CurrentRow;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x04000048 RID: 72
		private const int MaxTupleCount = 200000;

		// Token: 0x04000049 RID: 73
		private const int MaxCellCount = 1000000;

		// Token: 0x0400004A RID: 74
		private const int MinTupleCount = 5;

		// Token: 0x0400004B RID: 75
		private const int ColumnsAxis = 0;

		// Token: 0x0400004C RID: 76
		private const int RowsAxis = 1;

		// Token: 0x0400004D RID: 77
		private const int DefaultAxis = 255;

		// Token: 0x0400004E RID: 78
		private const string Measures = "[Measures]";

		// Token: 0x0400004F RID: 79
		private const string DefaultMeasureName = "[Default]";

		// Token: 0x04000050 RID: 80
		private const string MemberCaption = "[MEMBER_CAPTION]";

		// Token: 0x04000051 RID: 81
		private const string MemberUniqueName = "[MEMBER_UNIQUE_NAME]";

		// Token: 0x04000052 RID: 82
		private static readonly Dictionary<string, string> mandatoryDimensionProperties = new Dictionary<string, string>
		{
			{ "[MEMBER_CAPTION]", "MEM_CAP" },
			{ "[MEMBER_UNIQUE_NAME]", "MEM_UNAM" }
		};

		// Token: 0x04000053 RID: 83
		private static readonly Dictionary<string, int> measureIndices = new Dictionary<string, int>
		{
			{ "VALUE", 1 },
			{ "FORMATTED_VALUE", 2 },
			{ "VALUE_TYPE", 3 },
			{ "CURRENCY", 4 },
			{ "UNIT", 5 },
			{ "MWKZ", 6 },
			{ "NUM_SCALE", 7 },
			{ "NUM_PREC", 8 },
			{ "CELL_STATUS", 9 },
			{ "BACK_COLOR", 10 }
		};

		// Token: 0x04000054 RID: 84
		private static readonly HashSet<string> nativeQueryCellProperties = new HashSet<string> { "FORMATTED_VALUE", "UNIT_OF_MEASURE" };

		// Token: 0x04000055 RID: 85
		private readonly Dictionary<int, int> propertyKeyToColumnIndex;

		// Token: 0x04000056 RID: 86
		private readonly List<Dictionary<string, KeyValuePair<int, int?>>> propertyNameToColumnIndex;

		// Token: 0x04000057 RID: 87
		private readonly Dictionary<int, List<int>> dimKeyToColumnIndices;

		// Token: 0x04000058 RID: 88
		private IRfcFunction getAxisInfo;

		// Token: 0x04000059 RID: 89
		private MdxMultidimensionalColumnProvider.MdxMultidimensionalDataProvider firstPageDataProvider;

		// Token: 0x0400005A RID: 90
		private int rowCount;

		// Token: 0x0400005B RID: 91
		private int measureCount;

		// Token: 0x0400005C RID: 92
		private int tupleCount;

		// Token: 0x0400005D RID: 93
		private bool hasMeasureInDefaultAxis;

		// Token: 0x0400005E RID: 94
		private bool? hasCustomCellProperties;

		// Token: 0x0400005F RID: 95
		private string dataSetId;

		// Token: 0x02000053 RID: 83
		private sealed class MdxMultidimensionalDataProvider
		{
			// Token: 0x0600033F RID: 831 RVA: 0x0000D11A File Offset: 0x0000B31A
			private MdxMultidimensionalDataProvider(IRfcFunction getAxisDataFunction, IRfcFunction getCellDataFunction, int endTuple, int endCell)
			{
				this.getAxisDataFunction = getAxisDataFunction;
				this.getCellDataFunction = getCellDataFunction;
				this.endTuple = endTuple;
				this.endCell = endCell;
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06000340 RID: 832 RVA: 0x0000D13F File Offset: 0x0000B33F
			public int EndTuple
			{
				get
				{
					return this.endTuple;
				}
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x06000341 RID: 833 RVA: 0x0000D147 File Offset: 0x0000B347
			public int EndCell
			{
				get
				{
					return this.endCell;
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000342 RID: 834 RVA: 0x0000D14F File Offset: 0x0000B34F
			public IRfcTable RowsMandatoryProperties
			{
				get
				{
					if (this.rowsMandatoryProperties == null)
					{
						this.rowsMandatoryProperties = this.getAxisDataFunction.GetTable(6);
					}
					return this.rowsMandatoryProperties;
				}
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x06000343 RID: 835 RVA: 0x0000D171 File Offset: 0x0000B371
			public IRfcTable RowsOptionalPropertyKeys
			{
				get
				{
					if (this.rowsOptionalPropertyKeys == null)
					{
						this.rowsOptionalPropertyKeys = this.getAxisDataFunction.GetTable(7);
					}
					return this.rowsOptionalPropertyKeys;
				}
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x06000344 RID: 836 RVA: 0x0000D193 File Offset: 0x0000B393
			public IRfcTable RowsOptionalPropertyValues
			{
				get
				{
					if (this.rowsOptionalPropertyValues == null)
					{
						this.rowsOptionalPropertyValues = this.getAxisDataFunction.GetTable(8);
					}
					return this.rowsOptionalPropertyValues;
				}
			}

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x06000345 RID: 837 RVA: 0x0000D1B5 File Offset: 0x0000B3B5
			public IRfcTable CellData
			{
				get
				{
					if (this.cellData == null)
					{
						this.cellData = this.getCellDataFunction.GetTable(5);
					}
					return this.cellData;
				}
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x06000346 RID: 838 RVA: 0x0000D1D7 File Offset: 0x0000B3D7
			public IRfcTable CellPropertyNames
			{
				get
				{
					if (this.cellPropertyNames == null && this.getCellDataFunction.IsParameterActive(7))
					{
						this.cellPropertyNames = this.getCellDataFunction.GetTable(7);
					}
					return this.cellPropertyNames;
				}
			}

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x06000347 RID: 839 RVA: 0x0000D207 File Offset: 0x0000B407
			public IRfcTable CellPropertyValues
			{
				get
				{
					if (this.cellPropertyValues == null && this.getCellDataFunction.IsParameterActive(6))
					{
						this.cellPropertyValues = this.getCellDataFunction.GetTable(6);
					}
					return this.cellPropertyValues;
				}
			}

			// Token: 0x06000348 RID: 840 RVA: 0x0000D238 File Offset: 0x0000B438
			public static MdxMultidimensionalColumnProvider.MdxMultidimensionalDataProvider New(SapBwCommand command, MdxCommand mdxCommand, int startTuple, int startCell, int tupleCount, int measureCount, bool? hasCustomCellProperties)
			{
				int num = startTuple + tupleCount - 1;
				int num2 = startCell + tupleCount * measureCount - 1;
				return new MdxMultidimensionalColumnProvider.MdxMultidimensionalDataProvider(MdxMultidimensionalColumnProvider.MdxMultidimensionalDataProvider.GetRowsAxisData(command, mdxCommand.DataSetId, startTuple, num), MdxMultidimensionalColumnProvider.MdxMultidimensionalDataProvider.GetCellData(command, mdxCommand.DataSetId, startCell, num2, hasCustomCellProperties), num, num2);
			}

			// Token: 0x06000349 RID: 841 RVA: 0x0000D27C File Offset: 0x0000B47C
			private static IRfcFunction GetRowsAxisData(SapBwCommand command, string dataSetId, int startTuple, int endTuple)
			{
				IRfcFunction function = command.SapBwConnection.GetFunction("BAPI_MDDATASET_GET_AXIS_DATA", true);
				function.SetValue(3, dataSetId);
				function.SetValue(2, 1);
				function.SetValue(5, startTuple);
				function.SetValue(4, endTuple);
				command.SapBwConnection.InvokeFunction(function, true, command, true);
				return function;
			}

			// Token: 0x0600034A RID: 842 RVA: 0x0000D2CC File Offset: 0x0000B4CC
			private static IRfcFunction GetCellData(SapBwCommand command, string dataSetId, int startCell, int endCell, bool? hasCustomCellProperties)
			{
				IRfcFunction function = command.SapBwConnection.GetFunction("BAPI_MDDATASET_GET_CELL_DATA", true);
				function.SetValue(2, dataSetId);
				function.SetValue(4, startCell);
				function.SetValue(3, endCell);
				bool? flag = hasCustomCellProperties;
				bool flag2 = false;
				if ((flag.GetValueOrDefault() == flag2) & (flag != null))
				{
					function.SetParameterActive(6, false);
					function.SetParameterActive(7, false);
				}
				command.SapBwConnection.InvokeFunction(function, true, command, true);
				return function;
			}

			// Token: 0x0400023F RID: 575
			private readonly IRfcFunction getAxisDataFunction;

			// Token: 0x04000240 RID: 576
			private readonly IRfcFunction getCellDataFunction;

			// Token: 0x04000241 RID: 577
			private readonly int endTuple;

			// Token: 0x04000242 RID: 578
			private readonly int endCell;

			// Token: 0x04000243 RID: 579
			private IRfcTable rowsMandatoryProperties;

			// Token: 0x04000244 RID: 580
			private IRfcTable rowsOptionalPropertyKeys;

			// Token: 0x04000245 RID: 581
			private IRfcTable rowsOptionalPropertyValues;

			// Token: 0x04000246 RID: 582
			private IRfcTable cellData;

			// Token: 0x04000247 RID: 583
			private IRfcTable cellPropertyNames;

			// Token: 0x04000248 RID: 584
			private IRfcTable cellPropertyValues;
		}

		// Token: 0x02000054 RID: 84
		private sealed class MdxMultidimensionalDataReader : MdxDataReader
		{
			// Token: 0x0600034B RID: 843 RVA: 0x0000D33C File Offset: 0x0000B53C
			public MdxMultidimensionalDataReader(SapBwCommand command, MdxCommand mdxCommand, MdxColumnProvider columnProvider, MdxMultidimensionalColumnProvider.MdxMultidimensionalDataProvider firstDataProvider)
				: base(command, mdxCommand, columnProvider, 0)
			{
				this.multidimColumnProvider = (MdxMultidimensionalColumnProvider)columnProvider;
				this.dataProvider = firstDataProvider;
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x0600034C RID: 844 RVA: 0x0000D35C File Offset: 0x0000B55C
			private MdxMultidimensionalColumnProvider.MdxMultidimensionalDataProvider DataProvider
			{
				get
				{
					if (this.dataProvider == null)
					{
						this.dataProvider = MdxMultidimensionalColumnProvider.MdxMultidimensionalDataProvider.New(this.command, this.mdxCommand, this.startTuple, this.startCell, this.multidimColumnProvider.TupleCount, this.multidimColumnProvider.MeasureCount, this.multidimColumnProvider.hasCustomCellProperties);
					}
					return this.dataProvider;
				}
			}

			// Token: 0x0600034D RID: 845 RVA: 0x0000D3BB File Offset: 0x0000B5BB
			protected override bool TryGetRow(out object[] newRow)
			{
				this.EnsureInitialized();
				if (this.rowEnumerator.MoveNext())
				{
					newRow = this.rowEnumerator.Current;
					return true;
				}
				newRow = null;
				return false;
			}

			// Token: 0x0600034E RID: 846 RVA: 0x0000D3E3 File Offset: 0x0000B5E3
			protected override void EnsureInitialized()
			{
				if (this.rowEnumerator == null && this.columnProvider != null)
				{
					this.rowEnumerator = ((this.multidimColumnProvider.rowCount == 0) ? Enumerable.Empty<object[]>().GetEnumerator() : this.EnumerateFlatData());
				}
			}

			// Token: 0x0600034F RID: 847 RVA: 0x0000D41A File Offset: 0x0000B61A
			private IEnumerator<object[]> EnumerateFlatData()
			{
				long totalRows = 0L;
				int tuple = 0;
				this.cellOrdinal = 0;
				while (totalRows < (long)this.multidimColumnProvider.rowCount)
				{
					this.DataProvider.RowsOptionalPropertyValues.TrySetCurrentIndex(0);
					this.DataProvider.RowsMandatoryProperties.TrySetCurrentIndex(0);
					this.DataProvider.CellData.TrySetCurrentIndex(0);
					if (this.DataProvider.CellPropertyValues != null)
					{
						this.DataProvider.CellPropertyValues.TrySetCurrentIndex(0);
					}
					object[] array;
					while (this.TryBuildRow(tuple, out array))
					{
						long num = totalRows;
						totalRows = num + 1L;
						yield return array;
						int num2 = tuple;
						tuple = num2 + 1;
					}
					this.startTuple = this.dataProvider.EndTuple + 1;
					this.startCell = this.dataProvider.EndCell + 1;
					this.dataProvider = null;
				}
				yield break;
			}

			// Token: 0x06000350 RID: 848 RVA: 0x0000D42C File Offset: 0x0000B62C
			private bool TryBuildRow(int tuple, out object[] row)
			{
				row = null;
				foreach (KeyValuePair<int, string> keyValuePair in this.GetColumns(tuple))
				{
					if (row == null)
					{
						row = this.columnProvider.StartBuildingRecord();
					}
					object obj;
					if (this.columnProvider[keyValuePair.Key].TryExtractValue(keyValuePair.Value, out obj))
					{
						row[keyValuePair.Key] = obj;
					}
				}
				this.columnProvider.FinishBuildingRecord(row);
				return row != null;
			}

			// Token: 0x06000351 RID: 849 RVA: 0x0000D4C8 File Offset: 0x0000B6C8
			private IEnumerable<KeyValuePair<int, string>> GetColumns(int tuple)
			{
				foreach (IRfcStructure mandatoryPropertiesRow in MdxMultidimensionalColumnProvider.SequentialSelect(this.DataProvider.RowsMandatoryProperties, 0, tuple))
				{
					int @int = mandatoryPropertiesRow.GetInt(1);
					foreach (int num in this.multidimColumnProvider.dimKeyToColumnIndices[@int])
					{
						MdxColumn mdxColumn = this.columnProvider[num];
						string text;
						if (MdxMultidimensionalColumnProvider.mandatoryDimensionProperties.TryGetValue(mdxColumn.FieldName, out text))
						{
							yield return new KeyValuePair<int, string>(num, mandatoryPropertiesRow.GetString(text));
						}
					}
					List<int>.Enumerator enumerator2 = default(List<int>.Enumerator);
					mandatoryPropertiesRow = null;
				}
				IEnumerator<IRfcStructure> enumerator = null;
				foreach (IRfcStructure rfcStructure in MdxMultidimensionalColumnProvider.SequentialSelect(this.DataProvider.RowsOptionalPropertyValues, 0, tuple))
				{
					int int2 = rfcStructure.GetInt(1);
					int num2;
					if (this.multidimColumnProvider.propertyKeyToColumnIndex.TryGetValue(int2, out num2))
					{
						yield return new KeyValuePair<int, string>(num2, rfcStructure.GetString(2));
					}
				}
				enumerator = null;
				int num3;
				for (int i = 0; i < this.multidimColumnProvider.measureCount; i = num3 + 1)
				{
					bool hasMeasureProperties = false;
					Dictionary<string, KeyValuePair<int, int?>> dictionary;
					if (this.multidimColumnProvider.TryGetRequestedMeasureProperties(i, out dictionary))
					{
						foreach (KeyValuePair<int, string> keyValuePair in this.GetMeasureProperties(dictionary))
						{
							hasMeasureProperties = true;
							yield return new KeyValuePair<int, string>(keyValuePair.Key, keyValuePair.Value);
						}
						IEnumerator<KeyValuePair<int, string>> enumerator3 = null;
						if (hasMeasureProperties)
						{
							this.cellOrdinal++;
						}
					}
					else
					{
						this.cellOrdinal++;
					}
					num3 = i;
				}
				yield break;
				yield break;
			}

			// Token: 0x06000352 RID: 850 RVA: 0x0000D4DF File Offset: 0x0000B6DF
			private IEnumerable<KeyValuePair<int, string>> GetMeasureProperties(Dictionary<string, KeyValuePair<int, int?>> requestedProperties)
			{
				int? unitOfMeasureColumnIndex = null;
				KeyValuePair<int, int?> keyValuePair;
				if (requestedProperties.TryGetValue("UNIT_OF_MEASURE", out keyValuePair))
				{
					unitOfMeasureColumnIndex = new int?(keyValuePair.Key);
				}
				foreach (IRfcStructure cellRow in MdxMultidimensionalColumnProvider.SequentialSelect(this.DataProvider.CellData, 0, this.cellOrdinal))
				{
					if (unitOfMeasureColumnIndex != null)
					{
						string text = cellRow.GetString(MdxMultidimensionalColumnProvider.measureIndices["CURRENCY"]);
						if (string.IsNullOrEmpty(text))
						{
							text = cellRow.GetString(MdxMultidimensionalColumnProvider.measureIndices["UNIT"]);
						}
						if (!string.IsNullOrEmpty(text))
						{
							yield return new KeyValuePair<int, string>(unitOfMeasureColumnIndex.Value, text);
							unitOfMeasureColumnIndex = null;
						}
					}
					foreach (KeyValuePair<string, KeyValuePair<int, int?>> keyValuePair2 in requestedProperties)
					{
						int key = keyValuePair2.Value.Key;
						int? value = keyValuePair2.Value.Value;
						if (value != null)
						{
							yield return new KeyValuePair<int, string>(key, cellRow.GetString(value.Value));
						}
					}
					Dictionary<string, KeyValuePair<int, int?>>.Enumerator enumerator2 = default(Dictionary<string, KeyValuePair<int, int?>>.Enumerator);
					cellRow = null;
				}
				IEnumerator<IRfcStructure> enumerator = null;
				bool? hasCustomCellProperties = this.multidimColumnProvider.hasCustomCellProperties;
				bool flag = true;
				if ((hasCustomCellProperties.GetValueOrDefault() == flag) & (hasCustomCellProperties != null))
				{
					foreach (IRfcStructure rfcStructure in MdxMultidimensionalColumnProvider.SequentialSelect(this.DataProvider.CellPropertyValues, 1, this.cellOrdinal))
					{
						string @string = rfcStructure.GetString(0);
						if (!MdxMultidimensionalColumnProvider.measureIndices.ContainsKey(@string) && requestedProperties.TryGetValue(@string, out keyValuePair))
						{
							yield return new KeyValuePair<int, string>(keyValuePair.Key, rfcStructure.GetString(2));
						}
					}
					enumerator = null;
				}
				yield break;
				yield break;
			}

			// Token: 0x04000249 RID: 585
			private readonly MdxMultidimensionalColumnProvider multidimColumnProvider;

			// Token: 0x0400024A RID: 586
			private MdxMultidimensionalColumnProvider.MdxMultidimensionalDataProvider dataProvider;

			// Token: 0x0400024B RID: 587
			private int cellOrdinal;

			// Token: 0x0400024C RID: 588
			private int startTuple;

			// Token: 0x0400024D RID: 589
			private int startCell;
		}

		// Token: 0x02000055 RID: 85
		private static class RsrMdxGetAxisInfo
		{
			// Token: 0x0400024E RID: 590
			public const int ReturnS = 0;

			// Token: 0x0400024F RID: 591
			public const int StatisticS = 1;

			// Token: 0x04000250 RID: 592
			public const int DataSetId = 2;

			// Token: 0x04000251 RID: 593
			public const int AxisDimensionsT = 3;

			// Token: 0x04000252 RID: 594
			public const int AxisInfoT = 4;

			// Token: 0x04000253 RID: 595
			public const int AxisLevelsT = 5;

			// Token: 0x04000254 RID: 596
			public const int DimPrptysT = 6;

			// Token: 0x04000255 RID: 597
			public const int FltcolinfoT = 7;

			// Token: 0x02000089 RID: 137
			public static class AxisDimensions
			{
				// Token: 0x04000346 RID: 838
				public const int Axis = 0;

				// Token: 0x04000347 RID: 839
				public const int DimUnam = 1;

				// Token: 0x04000348 RID: 840
				public const int DimKey = 2;

				// Token: 0x04000349 RID: 841
				public const int DimPrptyCount = 3;
			}

			// Token: 0x0200008A RID: 138
			public static class AxisInfo
			{
				// Token: 0x0400034A RID: 842
				public const int Axis = 0;

				// Token: 0x0400034B RID: 843
				public const int Dims = 1;

				// Token: 0x0400034C RID: 844
				public const int Coordinates = 2;
			}

			// Token: 0x0200008B RID: 139
			public static class AxisLevels
			{
				// Token: 0x0400034D RID: 845
				public const int IAxis = 0;

				// Token: 0x0400034E RID: 846
				public const int DimKey = 1;

				// Token: 0x0400034F RID: 847
				public const int LvlKey = 2;

				// Token: 0x04000350 RID: 848
				public const int LvlUnam = 3;
			}

			// Token: 0x0200008C RID: 140
			public static class DimPrptys
			{
				// Token: 0x04000351 RID: 849
				public const int LvlKey = 0;

				// Token: 0x04000352 RID: 850
				public const int PrptyNam = 1;

				// Token: 0x04000353 RID: 851
				public const int DataType = 2;

				// Token: 0x04000354 RID: 852
				public const int ChrMaxLen = 3;
			}

			// Token: 0x0200008D RID: 141
			public static class Fltcolinfo
			{
				// Token: 0x04000355 RID: 853
				public const int ColOrdinal = 0;

				// Token: 0x04000356 RID: 854
				public const int DataType = 1;

				// Token: 0x04000357 RID: 855
				public const int ChrMaxLen = 2;
			}
		}

		// Token: 0x02000056 RID: 86
		private static class RsrMdxGetAxisData
		{
			// Token: 0x04000256 RID: 598
			public const int ReturnS = 0;

			// Token: 0x04000257 RID: 599
			public const int StatisticS = 1;

			// Token: 0x04000258 RID: 600
			public const int Axis = 2;

			// Token: 0x04000259 RID: 601
			public const int DataSetId = 3;

			// Token: 0x0400025A RID: 602
			public const int EndTuple = 4;

			// Token: 0x0400025B RID: 603
			public const int StartTuple = 5;

			// Token: 0x0400025C RID: 604
			public const int MndtryPrptysT = 6;

			// Token: 0x0400025D RID: 605
			public const int OptionPrptysKeysT = 7;

			// Token: 0x0400025E RID: 606
			public const int OptionPrptysValsT = 8;

			// Token: 0x0200008E RID: 142
			public static class MndtryPrptys
			{
				// Token: 0x04000358 RID: 856
				public const int TupleOrdinal = 0;

				// Token: 0x04000359 RID: 857
				public const int DimKey = 1;

				// Token: 0x0400035A RID: 858
				public const int MemUnam = 2;

				// Token: 0x0400035B RID: 859
				public const int MemCap = 3;

				// Token: 0x0400035C RID: 860
				public const int LvlUnam = 4;

				// Token: 0x0400035D RID: 861
				public const int LvlNumber = 5;

				// Token: 0x0400035E RID: 862
				public const int Children = 6;

				// Token: 0x0400035F RID: 863
				public const int DrilledDown = 7;

				// Token: 0x04000360 RID: 864
				public const int ParentSameAsPrev = 8;
			}

			// Token: 0x0200008F RID: 143
			public static class OptionPrptysKeys
			{
				// Token: 0x04000361 RID: 865
				public const int DimKey = 0;

				// Token: 0x04000362 RID: 866
				public const int PrptyNam = 1;

				// Token: 0x04000363 RID: 867
				public const int PrptyKey = 2;
			}

			// Token: 0x02000090 RID: 144
			public static class OptionPrptysVals
			{
				// Token: 0x04000364 RID: 868
				public const int TupleOrdinal = 0;

				// Token: 0x04000365 RID: 869
				public const int PrptyKey = 1;

				// Token: 0x04000366 RID: 870
				public const int PrptyVal = 2;
			}
		}

		// Token: 0x02000057 RID: 87
		private static class RsrMdxGetCellData
		{
			// Token: 0x0400025F RID: 607
			public const int ReturnS = 0;

			// Token: 0x04000260 RID: 608
			public const int StatisticS = 1;

			// Token: 0x04000261 RID: 609
			public const int DataSetId = 2;

			// Token: 0x04000262 RID: 610
			public const int EndCell = 3;

			// Token: 0x04000263 RID: 611
			public const int StartCell = 4;

			// Token: 0x04000264 RID: 612
			public const int CellDataT = 5;

			// Token: 0x04000265 RID: 613
			public const int CellPropsT = 6;

			// Token: 0x04000266 RID: 614
			public const int CellPropNamesT = 7;

			// Token: 0x02000091 RID: 145
			public static class CellData
			{
				// Token: 0x04000367 RID: 871
				public const int CellOrdinal = 0;

				// Token: 0x04000368 RID: 872
				public const int Value = 1;

				// Token: 0x04000369 RID: 873
				public const int FormattedValue = 2;

				// Token: 0x0400036A RID: 874
				public const int ValueType = 3;

				// Token: 0x0400036B RID: 875
				public const int Currency = 4;

				// Token: 0x0400036C RID: 876
				public const int Unit = 5;

				// Token: 0x0400036D RID: 877
				public const int Mwkz = 6;

				// Token: 0x0400036E RID: 878
				public const int NumScale = 7;

				// Token: 0x0400036F RID: 879
				public const int NumPrec = 8;

				// Token: 0x04000370 RID: 880
				public const int CellStatus = 9;

				// Token: 0x04000371 RID: 881
				public const int BackColor = 10;
			}

			// Token: 0x02000092 RID: 146
			public static class CellProps
			{
				// Token: 0x04000372 RID: 882
				public const int PrptyNam = 0;

				// Token: 0x04000373 RID: 883
				public const int CellOrdinal = 1;

				// Token: 0x04000374 RID: 884
				public const int PrptyVal = 2;
			}

			// Token: 0x02000093 RID: 147
			public static class CellPropNames
			{
				// Token: 0x04000375 RID: 885
				public const int PrptyNam = 0;
			}
		}
	}
}
