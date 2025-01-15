using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Portal.Interfaces.Exceptions;
using Newtonsoft.Json;

namespace Microsoft.ReportingServices.Portal.Services.Util
{
	// Token: 0x02000026 RID: 38
	public static class DataSetTable
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x0000CFB0 File Offset: 0x0000B1B0
		public static string GetDataSetValuesJson<TRow>(string json, int? sampledRows = null)
		{
			if (typeof(TRow) != typeof(string) && typeof(TRow) != typeof(double?))
			{
				throw new InvalidDataFormatException(SR.Error_InvalidDataFormat);
			}
			DataSetTable.Table<TRow> table;
			try
			{
				table = JsonConvert.DeserializeObject<DataSetTable.Table<TRow>>(json);
			}
			catch (JsonException)
			{
				throw new InvalidDataFormatException(SR.Error_InvalidDataFormat);
			}
			if (sampledRows != null)
			{
				return JsonConvert.SerializeObject(ResamplingUtils.LttbDownsample(table.Rows.Select((TRow[] x) => x[0] as double?).ToArray<double?>(), sampledRows ?? 0));
			}
			return JsonConvert.SerializeObject(table.Rows.Select((TRow[] x) => x[0]).ToArray<TRow>());
		}

		// Token: 0x02000147 RID: 327
		private class Column
		{
			// Token: 0x17000113 RID: 275
			// (get) Token: 0x06000870 RID: 2160 RVA: 0x0002009E File Offset: 0x0001E29E
			// (set) Token: 0x06000871 RID: 2161 RVA: 0x000200A6 File Offset: 0x0001E2A6
			[JsonProperty(PropertyName = "Name")]
			public string Name { get; set; }

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x06000872 RID: 2162 RVA: 0x000200AF File Offset: 0x0001E2AF
			// (set) Token: 0x06000873 RID: 2163 RVA: 0x000200B7 File Offset: 0x0001E2B7
			[JsonProperty(PropertyName = "Type")]
			public string Type { get; set; }
		}

		// Token: 0x02000148 RID: 328
		private class Table<TRow>
		{
			// Token: 0x17000115 RID: 277
			// (get) Token: 0x06000875 RID: 2165 RVA: 0x000200C0 File Offset: 0x0001E2C0
			// (set) Token: 0x06000876 RID: 2166 RVA: 0x000200C8 File Offset: 0x0001E2C8
			[JsonProperty(PropertyName = "Rows")]
			public List<TRow[]> Rows { get; set; }

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x06000877 RID: 2167 RVA: 0x000200D1 File Offset: 0x0001E2D1
			// (set) Token: 0x06000878 RID: 2168 RVA: 0x000200D9 File Offset: 0x0001E2D9
			[JsonProperty(PropertyName = "Columns")]
			public List<DataSetTable.Column> Columns { get; set; }

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x06000879 RID: 2169 RVA: 0x000200E2 File Offset: 0x0001E2E2
			// (set) Token: 0x0600087A RID: 2170 RVA: 0x000200EA File Offset: 0x0001E2EA
			[JsonProperty(PropertyName = "Name")]
			public string Name { get; set; }
		}
	}
}
