using System;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000C08 RID: 3080
	internal class ExchangeValueBuilder
	{
		// Token: 0x060053C9 RID: 21449 RVA: 0x0011C7DC File Offset: 0x0011A9DC
		public static Value CreateColumnValue(ExchangeVersion exchangeVersion, IExchangeService service, ExchangeSearchResult result, ExchangeColumnInfo columnInfo, HashSet<PropertyDefinitionBase> additionalPropertiesLoaded)
		{
			switch (columnInfo.ColumnCategory)
			{
			case ColumnCategory.PrimitiveColumn:
			case ColumnCategory.FolderPath:
				return ExchangeValueBuilder.CreateColumnValueFromResult(columnInfo, result);
			case ColumnCategory.AttachmentTableColumn:
				return new ExchangeAttachmentTableValue(exchangeVersion, service, result, columnInfo, additionalPropertiesLoaded);
			case ColumnCategory.TableColumn:
				return new ExchangeCollectionTableValue(service, result, columnInfo, additionalPropertiesLoaded);
			case ColumnCategory.ListColumn:
				return new ExchangeCollectionTableValue(service, result, columnInfo, additionalPropertiesLoaded)[columnInfo.SubColumns[0].DisplayName];
			case ColumnCategory.RecordColumn:
				return ExchangeValueBuilder.CreateColumnRecordValueFromResult(exchangeVersion, service, columnInfo, result, additionalPropertiesLoaded);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060053CA RID: 21450 RVA: 0x0011C860 File Offset: 0x0011AA60
		private static ListValue GetListValueFromPrimitiveColumn(ExchangeColumnInfo columnInfo, object selectedValue)
		{
			object[] array = (object[])selectedValue;
			Value[] array2 = new Value[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				object[] array3 = (object[])array[i];
				array2[i] = columnInfo.Marshal(array3[0]);
			}
			return ListValue.New(array2);
		}

		// Token: 0x060053CB RID: 21451 RVA: 0x0011C8AC File Offset: 0x0011AAAC
		private static ListValue GetListValueFromRecordColumn(ExchangeColumnInfo columnInfo, object selectedValue)
		{
			object[] array = (object[])selectedValue;
			Value[] array2 = new Value[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				object[] array3 = (object[])array[i];
				Value[] array4 = new Value[array3.Length];
				for (int j = 0; j < array4.Length; j++)
				{
					array4[j] = columnInfo.SubColumns[j].Marshal(array3[j]);
				}
				array2[i] = RecordValue.New(columnInfo.SubColumns.GetKeys(), array4);
			}
			if (array2.Length == 0)
			{
				return ListValue.New(new Value[] { RecordValue.Empty });
			}
			return ListValue.New(array2);
		}

		// Token: 0x060053CC RID: 21452 RVA: 0x0011C94C File Offset: 0x0011AB4C
		private static Value CreateColumnRecordValueFromResult(ExchangeVersion exchangeVersion, IExchangeService service, ExchangeColumnInfo columnInfo, ExchangeSearchResult result, HashSet<PropertyDefinitionBase> additionalPropertiesLoaded)
		{
			if (columnInfo.IsExpandedFromList)
			{
				object obj;
				if (ExchangeValueBuilder.TryGetColumnValue(columnInfo, result, out obj))
				{
					return ExchangeValueBuilder.GetListValueFromRecordColumn(columnInfo, obj);
				}
				return ListValue.New(new Value[] { RecordValue.Empty });
			}
			else
			{
				if (columnInfo.Property == null || !additionalPropertiesLoaded.Contains(columnInfo.Property))
				{
					return RecordValue.New(columnInfo.SubColumns.GetKeys(), delegate(int i)
					{
						ExchangeSearchResult exchangeSearchResult = result;
						if ((columnInfo.Property != null || !additionalPropertiesLoaded.Contains(columnInfo.SubColumns[i].Property)) && !result.IsAttachment)
						{
							exchangeSearchResult = service.GetItem(result.Id, result.FolderPath, columnInfo.SubColumns.GetPropertiesToFetch(null), columnInfo.SubColumns);
						}
						return ExchangeValueBuilder.CreateColumnValue(exchangeVersion, service, exchangeSearchResult, columnInfo.SubColumns[i], additionalPropertiesLoaded);
					});
				}
				object[] array = (object[])((object[])result.GetColumnValue(columnInfo))[0];
				Value[] array2 = new Value[columnInfo.SubColumns.Length];
				for (int j = 0; j < array2.Length; j++)
				{
					if (array[j] == null)
					{
						array2[j] = Value.Null;
					}
					else
					{
						array2[j] = columnInfo.SubColumns[j].Marshal(array[j]);
					}
				}
				return RecordValue.New(columnInfo.SubColumns.GetKeys(), array2);
			}
		}

		// Token: 0x060053CD RID: 21453 RVA: 0x0011CA98 File Offset: 0x0011AC98
		private static Value CreateColumnValueFromResult(ExchangeColumnInfo columnInfo, ExchangeSearchResult result)
		{
			object obj;
			if (!ExchangeValueBuilder.TryGetColumnValue(columnInfo, result, out obj))
			{
				return Value.Null;
			}
			if (columnInfo.IsExpandedFromList)
			{
				return ExchangeValueBuilder.GetListValueFromPrimitiveColumn(columnInfo, obj);
			}
			return columnInfo.Marshal(obj);
		}

		// Token: 0x060053CE RID: 21454 RVA: 0x0011CAD2 File Offset: 0x0011ACD2
		private static bool TryGetColumnValue(ExchangeColumnInfo columnInfo, ExchangeSearchResult result, out object selectedValue)
		{
			selectedValue = result.GetColumnValue(columnInfo);
			return selectedValue != null;
		}

		// Token: 0x060053CF RID: 21455 RVA: 0x0011CAE4 File Offset: 0x0011ACE4
		public static Value[] CreateRowValue(ExchangeVersion exchangeVersion, IExchangeService service, ExchangeSearchResult result, ExchangeColumnInfo[] columnInfos, HashSet<PropertyDefinitionBase> additionalPropertiesLoaded)
		{
			Value[] array = new Value[columnInfos.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = ExchangeValueBuilder.CreateColumnValue(exchangeVersion, service, result, columnInfos[i], additionalPropertiesLoaded);
			}
			TableValue tableValue = ListValue.New(new Value[] { RecordValue.New(columnInfos.GetKeys(), array) }).ToTable();
			for (int j = 0; j < columnInfos.Length; j++)
			{
				if (columnInfos[j].IsExpandedFromList)
				{
					tableValue = tableValue.ExpandListColumn(j, false);
				}
			}
			return tableValue.ToRecords().ToArray();
		}
	}
}
