using System;
using System.Collections.Generic;
using System.Linq;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x0200001C RID: 28
	internal class TableQuery
	{
		// Token: 0x0600015D RID: 349 RVA: 0x00005996 File Offset: 0x00003B96
		public TableQuery(SapBwConnection connection)
		{
			this.connection = connection;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600015E RID: 350 RVA: 0x000059A5 File Offset: 0x00003BA5
		public ColumnProvider ColumnProvider
		{
			get
			{
				if (this.columnProvider == null)
				{
					this.GetReadTableFunction();
				}
				return this.columnProvider;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000059BC File Offset: 0x00003BBC
		// (set) Token: 0x06000160 RID: 352 RVA: 0x000059C4 File Offset: 0x00003BC4
		public string Table { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000059CD File Offset: 0x00003BCD
		// (set) Token: 0x06000162 RID: 354 RVA: 0x000059D5 File Offset: 0x00003BD5
		public int? RowSkips { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000059DE File Offset: 0x00003BDE
		// (set) Token: 0x06000164 RID: 356 RVA: 0x000059E6 File Offset: 0x00003BE6
		public int? RowCount { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000059EF File Offset: 0x00003BEF
		// (set) Token: 0x06000166 RID: 358 RVA: 0x000059F7 File Offset: 0x00003BF7
		public string Where { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00005A00 File Offset: 0x00003C00
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00005A1C File Offset: 0x00003C1C
		public string Fields
		{
			get
			{
				if (this.fieldArray == null)
				{
					return null;
				}
				return string.Join(",", this.fieldArray);
			}
			set
			{
				string[] array;
				if (value != null)
				{
					array = (from x in value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
						select x.Trim()).ToArray<string>();
				}
				else
				{
					array = null;
				}
				this.fieldArray = array;
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005A70 File Offset: 0x00003C70
		private IRfcFunction GetReadTableFunction()
		{
			if (this.readTableFunction == null)
			{
				if (string.IsNullOrEmpty(this.Table))
				{
					throw this.connection.Helper.NewDataSourceError(Resources.TableParameterMissing);
				}
				if (this.fieldArray == null || string.IsNullOrEmpty(this.Fields))
				{
					throw this.connection.Helper.NewDataSourceError(Resources.FieldsParameterMissing);
				}
				this.columnProvider = new ColumnProvider();
				IRfcFunction function = this.connection.GetFunction("RFC_READ_TABLE", true);
				function.SetValue("DELIMITER", '|');
				function.SetValue("QUERY_TABLE", this.Table);
				this.columnProvider.Table = this.Table;
				IRfcTable table = function.GetTable("FIELDS");
				table.Append(this.fieldArray.Length);
				for (int i = 0; i < table.RowCount; i++)
				{
					table.CurrentIndex = i;
					table.CurrentRow["FIELDNAME"].SetValue(this.fieldArray[i]);
					this.columnProvider.Add(new MdxColumn(MdxColumnType.Other, this.fieldArray[i], "CHAR", 0, null, null, -1));
				}
				if (this.columnProvider.ColumnCount == 0)
				{
					throw this.connection.Helper.NewDataSourceError(Resources.FieldsParameterMissing);
				}
				if (!string.IsNullOrEmpty(this.Where))
				{
					IRfcTable table2 = function.GetTable("OPTIONS");
					string[] array = Utils.SplitByLengthConsideringSpaces(this.Where, 72).ToArray<string>();
					table2.Append(array.Length);
					for (int j = 0; j < table2.RowCount; j++)
					{
						table2.CurrentIndex = j;
						table2.SetValue(0, array[j]);
					}
				}
				if (this.RowSkips != null)
				{
					function.SetValue("ROWSKIPS", this.RowSkips);
				}
				if (this.RowCount != null)
				{
					function.SetValue("ROWCOUNT", this.RowCount);
				}
				this.readTableFunction = function;
			}
			return this.readTableFunction;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005C8C File Offset: 0x00003E8C
		public IRfcTable ExecuteQuery(SapBwCommand command)
		{
			IRfcFunction rfcFunction = this.GetReadTableFunction();
			this.connection.InvokeFunction(rfcFunction, false, command, false);
			return rfcFunction.GetTable("DATA");
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005CBC File Offset: 0x00003EBC
		public bool TryExecuteQuery(out IRfcTable data)
		{
			IRfcFunction rfcFunction = this.GetReadTableFunction();
			if (this.connection.TryInvokeStatelessFunction(rfcFunction, false, string.Join("|", new string[] { this.Table, this.Where })))
			{
				data = rfcFunction.GetTable("DATA");
				return true;
			}
			data = null;
			return false;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005D14 File Offset: 0x00003F14
		public bool TryExtractSingleRowUsingCache(bool serverLevelCache, out object[] row)
		{
			string cacheKey = this.connection.GetCacheKey("RFC_READ_TABLE", this.Table, this.Where, serverLevelCache);
			IRfcStructure rfcStructure;
			if (!this.connection.Provider.Structures.TryGetValue(cacheKey, out rfcStructure))
			{
				IRfcTable rfcTable;
				if (this.TryExecuteQuery(out rfcTable) && rfcTable != null && rfcTable.RowCount >= 1)
				{
					rfcStructure = rfcTable[0];
				}
				this.connection.Provider.Structures[cacheKey] = rfcStructure;
			}
			if (rfcStructure != null)
			{
				row = TableQuery.ParseSingleRow(rfcStructure, this.fieldArray.Length);
				if (row != null && row.Length == this.fieldArray.Length)
				{
					return true;
				}
			}
			row = null;
			return false;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005DB9 File Offset: 0x00003FB9
		public static IEnumerable<object[]> EnumerateTable(IRfcTable data, int columnCount)
		{
			if (data == null || data.RowCount == 0)
			{
				yield break;
			}
			foreach (IRfcStructure rfcStructure in data)
			{
				object[] array = TableQuery.ParseSingleRow(rfcStructure, columnCount);
				if (rfcStructure != null)
				{
					yield return array;
				}
			}
			IEnumerator<IRfcStructure> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005DD0 File Offset: 0x00003FD0
		public IEnumerable<object[]> EnumerateAndCacheTable(bool serverLevelCache)
		{
			IEnumerable<IRfcStructure> enumerable = null;
			string cacheKey = this.connection.GetCacheKey("RFC_READ_TABLE", this.Table, this.Where, serverLevelCache);
			IRfcStructure rfcStructure;
			if (this.connection.Provider.Structures.TryGetValue(cacheKey, out rfcStructure))
			{
				MultiRowStructure multiRowStructure = rfcStructure as MultiRowStructure;
				if (multiRowStructure != null && multiRowStructure.Values.Any<IRfcStructure>())
				{
					enumerable = multiRowStructure.Values;
				}
			}
			else
			{
				MultiRowStructure multiRowStructure = new MultiRowStructure();
				IRfcTable rfcTable;
				if (this.TryExecuteQuery(out rfcTable))
				{
					int num = 0;
					foreach (IRfcStructure rfcStructure2 in rfcTable)
					{
						multiRowStructure.SetValue(num, rfcStructure2);
						num++;
					}
					if (rfcTable.RowCount > 0)
					{
						enumerable = rfcTable;
					}
				}
				this.connection.Provider.Structures[cacheKey] = multiRowStructure;
			}
			if (enumerable == null)
			{
				yield break;
			}
			foreach (IRfcStructure rfcStructure3 in enumerable)
			{
				object[] array = TableQuery.ParseSingleRow(rfcStructure3, this.fieldArray.Length);
				if (rfcStructure3 != null)
				{
					yield return array;
				}
			}
			IEnumerator<IRfcStructure> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005DE8 File Offset: 0x00003FE8
		public static object[] ParseSingleRow(IRfcStructure row, int columnCount)
		{
			string @string = row[0].GetString();
			if (!string.IsNullOrEmpty(@string))
			{
				return (from x in @string.Split(new char[] { '|' })
					select x.Trim()).ToArray(columnCount);
			}
			return null;
		}

		// Token: 0x04000071 RID: 113
		private const char Delimiter = '|';

		// Token: 0x04000072 RID: 114
		private readonly SapBwConnection connection;

		// Token: 0x04000073 RID: 115
		private IRfcFunction readTableFunction;

		// Token: 0x04000074 RID: 116
		private ColumnProvider columnProvider;

		// Token: 0x04000075 RID: 117
		private string[] fieldArray;
	}
}
