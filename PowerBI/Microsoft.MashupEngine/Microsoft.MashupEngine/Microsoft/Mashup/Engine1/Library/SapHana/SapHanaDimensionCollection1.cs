using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Mashup.Engine1.Library.Odbc;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200043F RID: 1087
	internal sealed class SapHanaDimensionCollection1 : SapHanaDimensionCollection
	{
		// Token: 0x060024F4 RID: 9460 RVA: 0x000696EA File Offset: 0x000678EA
		public SapHanaDimensionCollection1(SapHanaOdbcDataSource dataSource, SapHanaCubeBase cube)
			: base(dataSource, cube)
		{
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x000696F4 File Offset: 0x000678F4
		protected override Dictionary<string, SapHanaDimension> GetDimensions()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			using (IDataReader dataReader = SapHanaDimensionCollection1.columnsQuery.Execute(this.dataSource, this.cube))
			{
				while (dataReader.Read())
				{
					string text = (string)dataReader[0];
					string text2 = (string)dataReader[1];
					dictionary[text] = text2;
				}
			}
			Dictionary<string, SapHanaDimension> dictionary2 = new Dictionary<string, SapHanaDimension>();
			foreach (OdbcColumnInfo odbcColumnInfo in this.cube.Columns)
			{
				SapHanaMeasure sapHanaMeasure;
				if (!this.cube.Measures.TryGetMeasureByColumn(odbcColumnInfo.Name, out sapHanaMeasure) && odbcColumnInfo.Name != "row.count")
				{
					string name = odbcColumnInfo.Name;
					string text3;
					dictionary.TryGetValue(name, out text3);
					SapHanaDimension sapHanaDimension = new SapHanaDimension(name, text3);
					SapHanaColumn sapHanaColumn = new SapHanaColumn(odbcColumnInfo);
					ColumnSapHanaDimensionAttribute columnSapHanaDimensionAttribute = new ColumnSapHanaDimensionAttribute(sapHanaDimension, name, text3, sapHanaColumn, sapHanaColumn);
					sapHanaDimension.Attributes.Add(columnSapHanaDimensionAttribute.Name, columnSapHanaDimensionAttribute);
					dictionary2.Add(name, sapHanaDimension);
				}
			}
			return dictionary2;
		}

		// Token: 0x04000EEC RID: 3820
		private static readonly MetadataQuery columnsQuery = new MetadataQuery
		{
			ColumnNames = new string[] { "ALTERNATIVE_PROPERTY_NAME", "DESCRIPTION" },
			Query = "select {columns}\r\nFROM _SYS_BI.BIMC_PROPERTIES\r\nwhere CATALOG_NAME = {catalog} and CUBE_NAME = {cube}\r\nand ALTERNATIVE_PROPERTY_NAME is not null\r\nand DIMENSION_UNIQUE_NAME <> '[Measures]'"
		};
	}
}
