using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Web
{
	// Token: 0x0200029D RID: 669
	internal sealed class SodaTableValue : TableValue
	{
		// Token: 0x06001AEF RID: 6895 RVA: 0x000371E3 File Offset: 0x000353E3
		public SodaTableValue(Uri destinationUri, Func<string, TableValue> tableExtractor, TypeValue type, RowRange range)
		{
			this.destinationUri = destinationUri;
			this.tableExtractor = tableExtractor;
			this.type = type;
			this.range = range;
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x00037208 File Offset: 0x00035408
		public override TypeValue Type
		{
			get
			{
				if (this.type == null)
				{
					Value value = ListValue.Empty;
					using (IEnumerator<IValueReference> enumerator = this.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							value = Library.Record.FieldNames.Invoke(enumerator.Current.Value);
						}
					}
					this.type = TableTypeValue.FromValue(value, DataSource.NullableSerializedTextType);
				}
				return this.type;
			}
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x0003727C File Offset: 0x0003547C
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return this.Rows.GetEnumerator();
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x00037289 File Offset: 0x00035489
		private ListValue Rows
		{
			get
			{
				if (this.rows == null)
				{
					this.rows = ListValue.Combine(ListValue.New(this.RetrieveRecords()));
				}
				return this.rows;
			}
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x000372B0 File Offset: 0x000354B0
		private TableValue RetrieveTablePage(RowCount skip, RowCount take)
		{
			if (skip.IsInfinite)
			{
				return TableValue.Empty;
			}
			if (take.IsZero)
			{
				return TableValue.Empty;
			}
			UriBuilder uriBuilder = new UriBuilder(this.destinationUri);
			if (!skip.IsZero)
			{
				string text = "$offset=" + skip.Value.ToString(CultureInfo.InvariantCulture);
				uriBuilder.Query = text;
			}
			if (!take.IsInfinite)
			{
				string text2 = "$limit=" + take.Value.ToString(CultureInfo.InvariantCulture);
				uriBuilder.Query = (string.IsNullOrEmpty(uriBuilder.Query) ? text2 : (uriBuilder.Query.Substring(1) + "&" + text2));
			}
			return this.tableExtractor(uriBuilder.ToString());
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x0003737C File Offset: 0x0003557C
		private IEnumerable<IValueReference> RetrieveRecords()
		{
			RowCount page = new RowCount(1000L);
			RowRange baseRange = this.range;
			for (;;)
			{
				RowRange rowRange = baseRange.Take(page);
				if (rowRange.IsNone)
				{
					break;
				}
				TableValue tableValue = this.RetrieveTablePage(rowRange.SkipCount, rowRange.TakeCount);
				Value value;
				if (!tableValue.TryGetValue(NumberValue.Zero, out value))
				{
					break;
				}
				baseRange = baseRange.Skip(page);
				yield return tableValue.ToRecords();
			}
			yield break;
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0003738C File Offset: 0x0003558C
		public override TableValue Skip(RowCount count)
		{
			return new SodaTableValue(this.destinationUri, this.tableExtractor, this.Type, this.range.Skip(count));
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x000373C0 File Offset: 0x000355C0
		public override TableValue Take(RowCount count)
		{
			return new SodaTableValue(this.destinationUri, this.tableExtractor, this.Type, this.range.Take(count));
		}

		// Token: 0x04000821 RID: 2081
		private const string TakeToken = "$limit";

		// Token: 0x04000822 RID: 2082
		private const string SkipToken = "$offset";

		// Token: 0x04000823 RID: 2083
		private const int PageSize = 1000;

		// Token: 0x04000824 RID: 2084
		private readonly Uri destinationUri;

		// Token: 0x04000825 RID: 2085
		private readonly Func<string, TableValue> tableExtractor;

		// Token: 0x04000826 RID: 2086
		private readonly RowRange range;

		// Token: 0x04000827 RID: 2087
		private TypeValue type;

		// Token: 0x04000828 RID: 2088
		private ListValue rows;
	}
}
