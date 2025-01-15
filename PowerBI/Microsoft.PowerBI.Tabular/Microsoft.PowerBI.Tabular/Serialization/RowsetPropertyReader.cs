using System;
using System.Data;
using System.Xml;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x0200018C RID: 396
	internal class RowsetPropertyReader : IPropertyReader
	{
		// Token: 0x0600184E RID: 6222 RVA: 0x000A3782 File Offset: 0x000A1982
		public void SetDataRow(DataRow row)
		{
			this.dataRow = row;
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x000A378B File Offset: 0x000A198B
		public DataRow Row
		{
			get
			{
				return this.dataRow;
			}
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x000A3794 File Offset: 0x000A1994
		bool IPropertyReader.TryReadProperty<T>(string propName, out T propValue)
		{
			Utils.Verify(this.dataRow != null, "RowsetPropertyReader has not been initialized before calling TryReadProperty");
			propValue = default(T);
			if (!this.dataRow.Table.Columns.Contains(propName))
			{
				return false;
			}
			DataColumn dataColumn = this.dataRow.Table.Columns[propName];
			object obj = this.dataRow.ItemArray[dataColumn.Ordinal];
			if (obj is DBNull)
			{
				return false;
			}
			if (typeof(T) == typeof(ObjectId))
			{
				propValue = (T)((object)ObjectId.FromUInt64((ulong)obj));
			}
			else if (typeof(T).IsEnum)
			{
				propValue = ConvertHelper.ParseRawEnumValue<T>(obj, false, false);
			}
			else if (typeof(T) == typeof(string) && obj is XmlReader)
			{
				propValue = (T)((object)obj.ToString());
			}
			else
			{
				propValue = (T)((object)obj);
			}
			return true;
		}

		// Token: 0x04000495 RID: 1173
		private DataRow dataRow;
	}
}
