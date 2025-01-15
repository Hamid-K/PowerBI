using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001086 RID: 4230
	internal class DbExceptionHandlingPageReader : IPageReader, IDisposable
	{
		// Token: 0x06006EC3 RID: 28355 RVA: 0x0017E63E File Offset: 0x0017C83E
		public DbExceptionHandlingPageReader(DbExceptionHandler exceptionHandler, IPageReader pageReader)
		{
			this.exceptionHandler = exceptionHandler;
			this.pageReader = pageReader;
		}

		// Token: 0x17001F3B RID: 7995
		// (get) Token: 0x06006EC4 RID: 28356 RVA: 0x0017E654 File Offset: 0x0017C854
		public TableSchema Schema
		{
			get
			{
				return this.exceptionHandler.InvokeWithoutRetry<TableSchema>(() => this.pageReader.Schema);
			}
		}

		// Token: 0x17001F3C RID: 7996
		// (get) Token: 0x06006EC5 RID: 28357 RVA: 0x0017E66D File Offset: 0x0017C86D
		public IProgress Progress
		{
			get
			{
				return this.pageReader.Progress;
			}
		}

		// Token: 0x17001F3D RID: 7997
		// (get) Token: 0x06006EC6 RID: 28358 RVA: 0x0017E67A File Offset: 0x0017C87A
		public int MaxPageRowCount
		{
			get
			{
				return this.pageReader.MaxPageRowCount;
			}
		}

		// Token: 0x06006EC7 RID: 28359 RVA: 0x0017E687 File Offset: 0x0017C887
		public IPage CreatePage()
		{
			return new DbExceptionHandlingPageReader.DbExceptionHandlingPage(this.exceptionHandler, this.pageReader.CreatePage());
		}

		// Token: 0x06006EC8 RID: 28360 RVA: 0x0017E6A0 File Offset: 0x0017C8A0
		public void Read(IPage page)
		{
			DbExceptionHandlingPageReader.DbExceptionHandlingPage dbExceptionHandlingPage = (DbExceptionHandlingPageReader.DbExceptionHandlingPage)page;
			this.exceptionHandler.InvokeWithoutRetry(delegate
			{
				this.pageReader.Read(dbExceptionHandlingPage.InnerPage);
			});
		}

		// Token: 0x06006EC9 RID: 28361 RVA: 0x0017E6E0 File Offset: 0x0017C8E0
		public IPageReader NextResult()
		{
			IPageReader pageReader = this.pageReader.NextResult();
			if (pageReader != null)
			{
				pageReader = new DbExceptionHandlingPageReader(this.exceptionHandler, pageReader);
			}
			return pageReader;
		}

		// Token: 0x06006ECA RID: 28362 RVA: 0x0017E70A File Offset: 0x0017C90A
		public void Dispose()
		{
			if (this.pageReader != null)
			{
				this.pageReader.Dispose();
				this.pageReader = null;
			}
		}

		// Token: 0x04003D72 RID: 15730
		private readonly DbExceptionHandler exceptionHandler;

		// Token: 0x04003D73 RID: 15731
		private IPageReader pageReader;

		// Token: 0x02001087 RID: 4231
		private class DbExceptionHandlingPage : IPage, IDisposable
		{
			// Token: 0x06006ECC RID: 28364 RVA: 0x0017E733 File Offset: 0x0017C933
			public DbExceptionHandlingPage(DbExceptionHandler exceptionHandler, IPage page)
			{
				this.exceptionHandler = exceptionHandler;
				this.page = page;
			}

			// Token: 0x17001F3E RID: 7998
			// (get) Token: 0x06006ECD RID: 28365 RVA: 0x0017E749 File Offset: 0x0017C949
			public IPage InnerPage
			{
				get
				{
					return this.page;
				}
			}

			// Token: 0x17001F3F RID: 7999
			// (get) Token: 0x06006ECE RID: 28366 RVA: 0x0017E751 File Offset: 0x0017C951
			public int ColumnCount
			{
				get
				{
					return this.page.ColumnCount;
				}
			}

			// Token: 0x17001F40 RID: 8000
			// (get) Token: 0x06006ECF RID: 28367 RVA: 0x0017E75E File Offset: 0x0017C95E
			public int RowCount
			{
				get
				{
					return this.page.RowCount;
				}
			}

			// Token: 0x17001F41 RID: 8001
			// (get) Token: 0x06006ED0 RID: 28368 RVA: 0x0017E76B File Offset: 0x0017C96B
			public IDictionary<int, IExceptionRow> ExceptionRows
			{
				get
				{
					return this.page.ExceptionRows;
				}
			}

			// Token: 0x17001F42 RID: 8002
			// (get) Token: 0x06006ED1 RID: 28369 RVA: 0x0017E778 File Offset: 0x0017C978
			public ISerializedException PageException
			{
				get
				{
					return this.page.PageException;
				}
			}

			// Token: 0x06006ED2 RID: 28370 RVA: 0x0017E788 File Offset: 0x0017C988
			public IColumn GetColumn(int ordinal)
			{
				return this.exceptionHandler.InvokeWithoutRetry<IColumn>(() => this.page.GetColumn(ordinal));
			}

			// Token: 0x06006ED3 RID: 28371 RVA: 0x0017E7C0 File Offset: 0x0017C9C0
			public void Dispose()
			{
				if (this.page != null)
				{
					this.page.Dispose();
					this.page = null;
				}
			}

			// Token: 0x04003D74 RID: 15732
			private readonly DbExceptionHandler exceptionHandler;

			// Token: 0x04003D75 RID: 15733
			private IPage page;
		}
	}
}
