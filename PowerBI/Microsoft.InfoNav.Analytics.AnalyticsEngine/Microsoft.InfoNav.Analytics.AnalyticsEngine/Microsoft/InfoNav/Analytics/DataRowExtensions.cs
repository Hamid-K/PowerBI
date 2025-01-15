using System;
using System.Globalization;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x02000007 RID: 7
	internal static class DataRowExtensions
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000211C File Offset: 0x0000031C
		internal static IDataRow AddColumn(this IDataRow dataRow, object value = null)
		{
			return dataRow.AddColumns(value.ArrayWrap<object>());
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000212C File Offset: 0x0000032C
		internal static double? CoerceIntoDouble(this IDataRow dataRow, DataType dataType, int columnIndex)
		{
			switch (dataType)
			{
			case DataType.DateTime:
			{
				object @object = dataRow.GetObject(columnIndex);
				if (@object == null || !(@object is DateTime))
				{
					return null;
				}
				return new double?(((DateTime)@object).ToDouble());
			}
			case DataType.Decimal:
			case DataType.Double:
			{
				double? asDouble = dataRow.GetAsDouble(columnIndex);
				if (asDouble == null)
				{
					return null;
				}
				return new double?(asDouble.Value);
			}
			case DataType.Int64:
			{
				long? asInt = dataRow.GetAsInt64(columnIndex);
				if (asInt == null)
				{
					return null;
				}
				return new double?((double)asInt.Value);
			}
			default:
				throw new TransformException("Unexpected column data type");
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021DC File Offset: 0x000003DC
		internal static DateTime CoerceIntoDateTime(this IDataRow dataRow, int columnIndex)
		{
			object @object = dataRow.GetObject(columnIndex);
			DateTime dateTime;
			if (@object == null)
			{
				dateTime = default(DateTime);
				return dateTime;
			}
			try
			{
				dateTime = (DateTime)@object;
			}
			catch (Exception ex)
			{
				throw new TransformException("cannot cast value to DateTime", ex);
			}
			return dateTime;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002228 File Offset: 0x00000428
		internal static double CoerceIntoDouble(this IDataRow dataRow, int columnIndex)
		{
			object @object = dataRow.GetObject(columnIndex);
			if (@object == null)
			{
				return 0.0;
			}
			double num;
			try
			{
				num = Convert.ToDouble(@object, CultureInfo.InvariantCulture);
			}
			catch (Exception ex)
			{
				throw new TransformException("cannot cast value to double", ex);
			}
			return num;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002278 File Offset: 0x00000478
		internal static bool TryGetAsDouble(this IDataRow row, int columnIndex, out double result)
		{
			result = 0.0;
			bool flag;
			try
			{
				double? asDouble = row.GetAsDouble(columnIndex);
				if (asDouble == null)
				{
					flag = false;
				}
				else
				{
					result = asDouble.Value;
					flag = true;
				}
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022C8 File Offset: 0x000004C8
		internal static bool TryGetAsDouble(this IDataRow dataRow, int[] columnIndexes, out double[] point)
		{
			int num = columnIndexes.Length;
			point = new double[num];
			for (int i = 0; i < num; i++)
			{
				double? asDouble = dataRow.GetAsDouble(columnIndexes[i]);
				if (asDouble == null)
				{
					point = null;
					return false;
				}
				point[i] = asDouble.Value;
			}
			return true;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002314 File Offset: 0x00000514
		internal static bool AreAllBlanks(this IDataRow dataRow, int[] columnIndexes)
		{
			foreach (int num in columnIndexes)
			{
				if (dataRow.GetObject(num) != null)
				{
					return false;
				}
			}
			return true;
		}
	}
}
