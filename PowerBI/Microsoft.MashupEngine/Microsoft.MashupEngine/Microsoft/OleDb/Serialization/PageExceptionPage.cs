using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FC0 RID: 8128
	internal class PageExceptionPage : IPage, IDisposable
	{
		// Token: 0x0600C653 RID: 50771 RVA: 0x00278778 File Offset: 0x00276978
		public PageExceptionPage(int columnCount, ISerializedException pageException)
		{
			this.columnCount = columnCount;
			Dictionary<int, ISerializedException> dictionary = new Dictionary<int, ISerializedException>(columnCount);
			for (int i = 0; i < columnCount; i++)
			{
				dictionary[i] = pageException;
			}
			this.cells = new Dictionary<int, IExceptionRow> { 
			{
				0,
				new ExceptionRow(dictionary)
			} };
		}

		// Token: 0x1700301E RID: 12318
		// (get) Token: 0x0600C654 RID: 50772 RVA: 0x002787C5 File Offset: 0x002769C5
		public int ColumnCount
		{
			get
			{
				return this.columnCount;
			}
		}

		// Token: 0x1700301F RID: 12319
		// (get) Token: 0x0600C655 RID: 50773 RVA: 0x00002139 File Offset: 0x00000339
		public int RowCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600C656 RID: 50774 RVA: 0x002787CD File Offset: 0x002769CD
		public IColumn GetColumn(int ordinal)
		{
			return PageExceptionPage.nullColumn;
		}

		// Token: 0x17003020 RID: 12320
		// (get) Token: 0x0600C657 RID: 50775 RVA: 0x002787D4 File Offset: 0x002769D4
		public IDictionary<int, IExceptionRow> ExceptionRows
		{
			get
			{
				return this.cells;
			}
		}

		// Token: 0x17003021 RID: 12321
		// (get) Token: 0x0600C658 RID: 50776 RVA: 0x000020FA File Offset: 0x000002FA
		public ISerializedException PageException
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600C659 RID: 50777 RVA: 0x0000336E File Offset: 0x0000156E
		public void Dispose()
		{
		}

		// Token: 0x0400655E RID: 25950
		private static readonly IColumn nullColumn = new NullColumn(0);

		// Token: 0x0400655F RID: 25951
		private readonly int columnCount;

		// Token: 0x04006560 RID: 25952
		private readonly Dictionary<int, IExceptionRow> cells;
	}
}
