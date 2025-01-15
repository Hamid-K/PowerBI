using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200044C RID: 1100
	internal sealed class SapHanaMeasureCollection1 : SapHanaMeasureCollection
	{
		// Token: 0x06002530 RID: 9520 RVA: 0x0006A594 File Offset: 0x00068794
		public SapHanaMeasureCollection1(SapHanaOdbcDataSource dataSource, SapHanaCubeBase cube)
			: base(dataSource, cube)
		{
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x0006A5A0 File Offset: 0x000687A0
		protected override Dictionary<string, SapHanaMeasure> GetMeasures()
		{
			Dictionary<string, SapHanaMeasure> dictionary = new Dictionary<string, SapHanaMeasure>();
			using (IDataReader dataReader = SapHanaMeasureCollection1.measuresQuery.Execute(this.dataSource, this.cube))
			{
				while (dataReader.Read())
				{
					string text = (string)dataReader[0];
					SapHanaAggregationType aggregateFunction = base.GetAggregateFunction(dataReader.GetInt32(1));
					string text2 = (string)dataReader[2];
					bool flag = dataReader.GetInt32(3) != 0;
					OdbcColumnInfo odbcColumnInfo = this.cube.Columns[text];
					dictionary.Add(text, new SapHanaMeasure(text, text2, aggregateFunction, flag, this.GetMeasureTypeValue(odbcColumnInfo, flag, aggregateFunction))
					{
						Column = new SapHanaColumn(odbcColumnInfo)
					});
				}
			}
			return dictionary;
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x0006A668 File Offset: 0x00068868
		protected override TypeValue GetMeasureTypeValue(OdbcColumnInfo columnInfo, bool isAggregatable, SapHanaAggregationType aggregationType)
		{
			if (isAggregatable)
			{
				if (aggregationType == SapHanaAggregationType.Sum)
				{
					TypeValue typeValue = base.GetTypeValue(columnInfo, "DOUBLE", TypeValue.Double, OdbcTypeValue.Double);
					if (columnInfo.IsNullable)
					{
						typeValue = typeValue.Nullable;
					}
					return typeValue;
				}
				if (aggregationType == SapHanaAggregationType.Count)
				{
					return base.GetTypeValue(columnInfo, "BIGINT", TypeValue.Int64, OdbcTypeValue.SignedBigInt);
				}
			}
			return columnInfo.TypeValue;
		}

		// Token: 0x04000F20 RID: 3872
		private static readonly MetadataQuery measuresQuery = new MetadataQuery
		{
			ColumnNames = new string[] { "MEASURE_NAME", "MEASURE_AGGREGATOR", "MEASURE_CAPTION", "MEASURE_AGGREGATABLE" },
			Query = "select {columns}\r\nfrom _SYS_BI.BIMC_MEASURES\r\nwhere CATALOG_NAME = {catalog} and CUBE_NAME = {cube}"
		};
	}
}
