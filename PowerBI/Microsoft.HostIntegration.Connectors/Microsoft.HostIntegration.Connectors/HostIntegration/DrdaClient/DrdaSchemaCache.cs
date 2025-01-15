using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A09 RID: 2569
	internal class DrdaSchemaCache
	{
		// Token: 0x060050E5 RID: 20709 RVA: 0x00143D65 File Offset: 0x00141F65
		public DrdaSchemaCache()
		{
			this._schemaCache = new Dictionary<string, Dictionary<string, DataTable>>(10);
		}

		// Token: 0x060050E6 RID: 20710 RVA: 0x00143D7C File Offset: 0x00141F7C
		public DataTable GetSchema(string schema, string[] restrictions, string options)
		{
			Dictionary<string, DataTable> cache = this.GetCache(schema);
			DataTable dataTable = null;
			string text = this.FormatRestrictionKey(restrictions, options);
			if (!cache.TryGetValue(text, out dataTable))
			{
				dataTable = null;
			}
			if (dataTable != null)
			{
				return dataTable.Copy();
			}
			return null;
		}

		// Token: 0x060050E7 RID: 20711 RVA: 0x00143DB4 File Offset: 0x00141FB4
		public void AddSchema(string schema, string[] restrictions, string options, DataTable schemaTable)
		{
			Dictionary<string, DataTable> cache = this.GetCache(schema);
			string text = this.FormatRestrictionKey(restrictions, options);
			cache.Add(text, schemaTable);
		}

		// Token: 0x060050E8 RID: 20712 RVA: 0x00143DD9 File Offset: 0x00141FD9
		public void Clear()
		{
			this._schemaCache = new Dictionary<string, Dictionary<string, DataTable>>(10);
		}

		// Token: 0x060050E9 RID: 20713 RVA: 0x00143DE8 File Offset: 0x00141FE8
		private string FormatRestrictionKey(string[] restrictions, string options)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (options == null)
			{
				stringBuilder.Append("null");
			}
			else
			{
				stringBuilder.Append(options.ToUpper(CultureInfo.InvariantCulture));
			}
			if (restrictions != null)
			{
				for (int i = 0; i < restrictions.Length; i++)
				{
					stringBuilder.Append(".");
					if (restrictions[i] == null)
					{
						stringBuilder.Append("null");
					}
					else
					{
						stringBuilder.Append(restrictions[i].ToUpper(CultureInfo.InvariantCulture));
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060050EA RID: 20714 RVA: 0x00143E68 File Offset: 0x00142068
		private Dictionary<string, DataTable> GetCache(string schema)
		{
			Dictionary<string, DataTable> dictionary = null;
			if (!this._schemaCache.TryGetValue(schema, out dictionary))
			{
				dictionary = new Dictionary<string, DataTable>(20);
				this._schemaCache.Add(schema, dictionary);
			}
			return dictionary;
		}

		// Token: 0x04003F82 RID: 16258
		private Dictionary<string, Dictionary<string, DataTable>> _schemaCache;
	}
}
