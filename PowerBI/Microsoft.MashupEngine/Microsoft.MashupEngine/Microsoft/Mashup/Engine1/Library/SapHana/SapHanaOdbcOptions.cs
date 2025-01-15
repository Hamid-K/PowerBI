using System;
using System.Data;
using System.Globalization;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000466 RID: 1126
	internal static class SapHanaOdbcOptions
	{
		// Token: 0x060025A0 RID: 9632 RVA: 0x0006CA04 File Offset: 0x0006AC04
		private static RecordValue GetOptionsRecord(bool prepareStatements, bool bindColumns = false)
		{
			return RecordValue.New(Keys.New("SqlCapabilities", "ClientConnectionPooling", "SQLGetTypeInfo", "SQLGetFunctions"), new Value[]
			{
				RecordValue.New(Keys.New(new string[] { "SupportedPredicates", "Sql92Conformance", "SupportedAggregateFunctions", "SupportsTop", "SupportedSql92Joins", "PrepareStatements", "LimitClauseLocation", "SupportsNumericLiterals" }), new Value[]
				{
					NumberValue.New(65535),
					NumberValue.New(8),
					NumberValue.New(255),
					LogicalValue.True,
					NumberValue.New(344),
					LogicalValue.New(prepareStatements),
					TextValue.New("AfterSelectBeforeModifiers"),
					LogicalValue.True
				}),
				LogicalValue.True,
				SapHanaOdbcOptions.AugmentTypeInfoFunctionValue.Instance,
				RecordValue.New(Keys.New("SQL_API_SQLBINDCOL"), new Value[] { LogicalValue.New(bindColumns) })
			});
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x0006CB15 File Offset: 0x0006AD15
		public static OdbcOptions Get(bool prepareStatements, bool bindColumns, RecordValue odbcOptions)
		{
			if (bindColumns || !odbcOptions.IsEmpty)
			{
				return OdbcOptions.CreateDataSourceOptionsFromValue(SapHanaOdbcOptions.GetOptionsRecord(prepareStatements, bindColumns).Concatenate(odbcOptions), true);
			}
			if (!prepareStatements)
			{
				return SapHanaOdbcOptions.Instance;
			}
			return SapHanaOdbcOptions.InstanceWithPrepare;
		}

		// Token: 0x04000F91 RID: 3985
		public static OdbcOptions Instance = OdbcOptions.CreateDataSourceOptionsFromValue(SapHanaOdbcOptions.GetOptionsRecord(false, false), true);

		// Token: 0x04000F92 RID: 3986
		public static OdbcOptions InstanceWithPrepare = OdbcOptions.CreateDataSourceOptionsFromValue(SapHanaOdbcOptions.GetOptionsRecord(true, false), true);

		// Token: 0x02000467 RID: 1127
		private sealed class AugmentTypeInfoFunctionValue : NativeFunctionValue1
		{
			// Token: 0x060025A3 RID: 9635 RVA: 0x0006CB6C File Offset: 0x0006AD6C
			public override Value Invoke(Value typeInfo)
			{
				DataTable dataTable = new DataTable();
				dataTable.Locale = CultureInfo.InvariantCulture;
				dataTable.Load(new PageReaderDataReader(typeInfo.AsTable.GetReader()));
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow[0].Equals("BOOLEAN"))
					{
						dataRow[1] = Odbc32.SQL_TYPE.BIT;
					}
				}
				return DataReaderTableValue.New(dataTable);
			}

			// Token: 0x04000F93 RID: 3987
			private const string BooleanTypeName = "BOOLEAN";

			// Token: 0x04000F94 RID: 3988
			public static readonly FunctionValue Instance = new SapHanaOdbcOptions.AugmentTypeInfoFunctionValue();
		}
	}
}
