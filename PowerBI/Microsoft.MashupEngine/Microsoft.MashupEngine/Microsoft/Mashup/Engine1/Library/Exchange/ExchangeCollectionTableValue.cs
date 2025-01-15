using System;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BD5 RID: 3029
	internal class ExchangeCollectionTableValue : TableValue
	{
		// Token: 0x06005298 RID: 21144 RVA: 0x00117577 File Offset: 0x00115777
		public ExchangeCollectionTableValue(IExchangeService service, ExchangeSearchResult result, ExchangeColumnInfo columnInfo, HashSet<PropertyDefinitionBase> additionalPropertiesLoaded)
		{
			this.service = service;
			this.result = result;
			this.columnInfo = columnInfo;
			this.additionalPropertiesLoaded = additionalPropertiesLoaded;
		}

		// Token: 0x17001976 RID: 6518
		// (get) Token: 0x06005299 RID: 21145 RVA: 0x0011759C File Offset: 0x0011579C
		public override TypeValue Type
		{
			get
			{
				if (this.type == null)
				{
					if (this.columnInfo.ColumnCategory == ColumnCategory.TableColumn)
					{
						this.type = this.columnInfo.SubColumns[0].SubColumns.CreateTableTypeValue();
					}
					else
					{
						this.type = this.columnInfo.SubColumns.CreateTableTypeValue();
					}
				}
				return this.type;
			}
		}

		// Token: 0x0600529A RID: 21146 RVA: 0x001175FA File Offset: 0x001157FA
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			ExchangeSearchResult item = this.result;
			if (!this.additionalPropertiesLoaded.Contains(this.columnInfo.Property) && !this.result.IsAttachment)
			{
				item = this.service.GetItem(this.result.Id, this.result.FolderPath, new PropertyDefinitionBase[] { this.columnInfo.Property }, new ExchangeColumnInfo[] { this.columnInfo });
			}
			ExchangeColumnInfo[] subColumns = this.columnInfo.SubColumns;
			if (this.columnInfo.ColumnCategory == ColumnCategory.TableColumn)
			{
				subColumns = subColumns[0].SubColumns;
			}
			object[] collection = (object[])item.GetColumnValue(this.columnInfo);
			int num;
			for (int rowIndex = 0; rowIndex < collection.Length; rowIndex = num + 1)
			{
				object[] array = (object[])collection[rowIndex];
				IValueReference[] array2 = new IValueReference[array.Length];
				for (int i = 0; i < subColumns.Length; i++)
				{
					string displayName = subColumns[i].DisplayName;
					Value value = ((array[i] == null) ? Value.Null : subColumns[i].Marshal(array[i]));
					array2[i] = value;
				}
				yield return RecordValue.New(this.Columns, array2);
				num = rowIndex;
			}
			yield break;
		}

		// Token: 0x04002D8B RID: 11659
		private readonly IExchangeService service;

		// Token: 0x04002D8C RID: 11660
		private readonly ExchangeSearchResult result;

		// Token: 0x04002D8D RID: 11661
		private readonly ExchangeColumnInfo columnInfo;

		// Token: 0x04002D8E RID: 11662
		private readonly HashSet<PropertyDefinitionBase> additionalPropertiesLoaded;

		// Token: 0x04002D8F RID: 11663
		private TypeValue type;
	}
}
