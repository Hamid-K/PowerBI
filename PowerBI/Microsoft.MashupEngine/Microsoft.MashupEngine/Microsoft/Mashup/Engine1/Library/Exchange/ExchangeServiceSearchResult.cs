using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BF9 RID: 3065
	internal class ExchangeServiceSearchResult : ExchangeSearchResult
	{
		// Token: 0x0600535E RID: 21342 RVA: 0x0011A778 File Offset: 0x00118978
		public ExchangeServiceSearchResult(ServiceObject serviceObject, string id, string folderPath)
			: base(id, folderPath)
		{
			this.serviceObject = serviceObject;
		}

		// Token: 0x0600535F RID: 21343 RVA: 0x0011A789 File Offset: 0x00118989
		public ExchangeServiceSearchResult(ServiceObject serviceObject, string id, string folderPath, bool isAttachment)
			: base(id, folderPath, isAttachment)
		{
			this.serviceObject = serviceObject;
		}

		// Token: 0x06005360 RID: 21344 RVA: 0x0011A79C File Offset: 0x0011899C
		public override object GetColumnValue(ExchangeColumnInfo columnInfo)
		{
			switch (columnInfo.ColumnCategory)
			{
			case ColumnCategory.PrimitiveColumn:
			{
				if (columnInfo.UniqueName == "Id")
				{
					return base.Id;
				}
				object obj = this.GetPropertyValue(columnInfo);
				if (columnInfo.IsExpandedFromList)
				{
					return this.GetCollectionResult(new ExchangeColumnInfo[] { columnInfo }, obj);
				}
				if (columnInfo.FieldSelector != null && obj != null)
				{
					obj = columnInfo.FieldSelector(obj);
				}
				return obj;
			}
			case ColumnCategory.FolderPath:
				return base.FolderPath;
			case ColumnCategory.AttachmentTableColumn:
				return this.GetPropertyValue(columnInfo);
			case ColumnCategory.TableColumn:
			{
				object obj = this.GetPropertyValue(columnInfo);
				return this.GetCollectionResult(columnInfo.SubColumns[0].SubColumns, obj);
			}
			case ColumnCategory.ListColumn:
			{
				object obj = this.GetPropertyValue(columnInfo);
				return this.GetCollectionResult(columnInfo.SubColumns, obj);
			}
			case ColumnCategory.RecordColumn:
			{
				object obj = this.GetPropertyValue(columnInfo);
				if (columnInfo.IsExpandedFromList)
				{
					return this.GetCollectionResult(columnInfo.SubColumns, obj);
				}
				return this.GetRecordResult(columnInfo.SubColumns, obj);
			}
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06005361 RID: 21345 RVA: 0x0011A8A0 File Offset: 0x00118AA0
		private object GetRecordResult(ExchangeColumnInfo[] subColumns, object recordObject)
		{
			object[] array = new object[subColumns.Length];
			if (recordObject != null)
			{
				for (int i = 0; i < subColumns.Length; i++)
				{
					array[i] = subColumns[i].FieldSelector(recordObject);
				}
			}
			return new object[] { array };
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x0011A8E4 File Offset: 0x00118AE4
		private object GetCollectionResult(ExchangeColumnInfo[] columnInfos, object collection)
		{
			List<object[]> list = new List<object[]>();
			if (collection is IEnumerable)
			{
				foreach (object obj in (collection as IEnumerable))
				{
					object[] array = new object[columnInfos.Length];
					for (int i = 0; i < columnInfos.Length; i++)
					{
						ExchangeColumnInfo exchangeColumnInfo = columnInfos[i];
						if (exchangeColumnInfo.FieldSelector != null)
						{
							array[i] = exchangeColumnInfo.FieldSelector(obj);
						}
						else
						{
							array[i] = obj;
						}
					}
					list.Add(array);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06005363 RID: 21347 RVA: 0x0011A994 File Offset: 0x00118B94
		private object GetPropertyValue(ExchangeColumnInfo columnInfo)
		{
			object obj = null;
			if (!this.serviceObject.TryGetProperty(columnInfo.Property, out obj))
			{
				return null;
			}
			return obj;
		}

		// Token: 0x04002E18 RID: 11800
		private ServiceObject serviceObject;
	}
}
