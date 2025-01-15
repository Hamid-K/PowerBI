using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200044D RID: 1101
	internal sealed class SapHanaMeasureCollection2 : SapHanaMeasureCollection
	{
		// Token: 0x06002534 RID: 9524 RVA: 0x0006A594 File Offset: 0x00068794
		public SapHanaMeasureCollection2(SapHanaOdbcDataSource dataSource, SapHanaCubeBase cube)
			: base(dataSource, cube)
		{
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x0006A718 File Offset: 0x00068918
		protected override TypeValue GetMeasureTypeValue(OdbcColumnInfo columnInfo, bool isAggregatable, SapHanaAggregationType aggregationType)
		{
			if (isAggregatable && aggregationType == SapHanaAggregationType.Count)
			{
				return base.GetTypeValue(columnInfo, "BIGINT", TypeValue.Int64, OdbcTypeValue.SignedBigInt);
			}
			return columnInfo.TypeValue;
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x0006A740 File Offset: 0x00068940
		protected override Dictionary<string, SapHanaMeasure> GetMeasures()
		{
			Dictionary<string, SapHanaMeasure> dictionary = new Dictionary<string, SapHanaMeasure>();
			using (IDataReader dataReader = SapHanaMeasureCollection2.MeasuresQuery.Execute(this.dataSource, this.cube))
			{
				while (dataReader.Read())
				{
					string @string = dataReader.GetString(0);
					string string2 = dataReader.GetString(1);
					string string3 = dataReader.GetString(2);
					SapHanaAggregationType aggregateFunction = base.GetAggregateFunction(dataReader.GetInt32(3));
					bool flag = dataReader.GetInt32(4) != 0;
					OdbcColumnInfo odbcColumnInfo = this.cube.Columns[string2];
					dictionary.Add(@string, new SapHanaMeasure(@string, string3, aggregateFunction, flag, this.GetMeasureTypeValue(odbcColumnInfo, flag, aggregateFunction))
					{
						Column = new SapHanaColumn(odbcColumnInfo)
					});
				}
			}
			return dictionary;
		}

		// Token: 0x04000F21 RID: 3873
		internal static readonly MdxMetadataQuery MeasuresQuery = new MdxMetadataQuery
		{
			ColumnNames = new string[] { "MEASURE_UNIQUE_NAME", "MEASURE_NAME", "MEASURE_CAPTION", "MEASURE_AGGREGATOR", "MEASURE_AGGREGATABLE" },
			TableName = "BIMC_MEASURES",
			WhereClause = "CATALOG_NAME = {catalog} and CUBE_NAME = {cube}"
		};
	}
}
