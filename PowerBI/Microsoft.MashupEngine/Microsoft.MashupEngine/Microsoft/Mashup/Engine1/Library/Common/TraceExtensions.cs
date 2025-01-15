using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001149 RID: 4425
	internal static class TraceExtensions
	{
		// Token: 0x060073E8 RID: 29672 RVA: 0x0018E85C File Offset: 0x0018CA5C
		public static void AddArray(this IHostTrace trace, string name, IEnumerable<string> values, bool isPii)
		{
			StringBuilder stringBuilder = new StringBuilder();
			TraceExtensions.AddArray(stringBuilder, values);
			trace.Add(name, stringBuilder.ToString(), isPii);
		}

		// Token: 0x060073E9 RID: 29673 RVA: 0x0018E884 File Offset: 0x0018CA84
		public static void AddDataTable(this IHostTrace trace, string name, DataTable table, bool isPii)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			TraceExtensions.AddArray(stringBuilder, from DataColumn c in table.Columns
				select c.ColumnName);
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				stringBuilder.Append(",");
				TraceExtensions.AddArray(stringBuilder, dataRow.ItemArray.Select(delegate(object i)
				{
					if (i != null)
					{
						return i.ToString();
					}
					return null;
				}));
			}
			stringBuilder.Append("]");
			trace.Add(name, stringBuilder.ToString(), isPii);
		}

		// Token: 0x060073EA RID: 29674 RVA: 0x0018E974 File Offset: 0x0018CB74
		public static void AddSchema(this IHostTrace trace, string name, TableSchema schema, bool isPii)
		{
			trace.AddDataTable(name, schema.ToDataTable(), isPii);
		}

		// Token: 0x060073EB RID: 29675 RVA: 0x0018E984 File Offset: 0x0018CB84
		public static bool AddExceptionInFilter(this IHostTrace trace, Exception e)
		{
			trace.Add(e, true);
			return false;
		}

		// Token: 0x060073EC RID: 29676 RVA: 0x0018E990 File Offset: 0x0018CB90
		private static void AddArray(StringBuilder builder, IEnumerable<string> items)
		{
			builder.Append("[");
			string text = "";
			foreach (string text2 in items)
			{
				builder.Append(text);
				if (text2 == null)
				{
					builder.Append("null");
				}
				else
				{
					builder.Append(text2.ToString());
				}
				text = ",";
			}
			builder.Append("]");
		}
	}
}
