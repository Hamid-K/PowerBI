using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;

namespace Microsoft.Internal
{
	// Token: 0x0200018A RID: 394
	internal class ContextAwarePage<T, U> : IPage, IDisposable where T : struct, IContext<U> where U : struct, IDisposable
	{
		// Token: 0x06000795 RID: 1941 RVA: 0x0000DCBC File Offset: 0x0000BEBC
		public ContextAwarePage(T context, IPage page)
		{
			this.context = context;
			this.page = page;
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0000DCD2 File Offset: 0x0000BED2
		public IPage Page
		{
			get
			{
				return this.page;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x0000DCDC File Offset: 0x0000BEDC
		public int ColumnCount
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				int columnCount;
				try
				{
					columnCount = this.page.ColumnCount;
				}
				finally
				{
					u.Dispose();
				}
				return columnCount;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0000DD2C File Offset: 0x0000BF2C
		public int RowCount
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				int rowCount;
				try
				{
					rowCount = this.page.RowCount;
				}
				finally
				{
					u.Dispose();
				}
				return rowCount;
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0000DD7C File Offset: 0x0000BF7C
		public IColumn GetColumn(int ordinal)
		{
			T t = this.context;
			U u = t.Enter();
			IColumn column;
			try
			{
				column = this.page.GetColumn(ordinal);
			}
			finally
			{
				u.Dispose();
			}
			return column;
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x0000DDCC File Offset: 0x0000BFCC
		public IDictionary<int, IExceptionRow> ExceptionRows
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				IDictionary<int, IExceptionRow> exceptionRows;
				try
				{
					exceptionRows = this.page.ExceptionRows;
				}
				finally
				{
					u.Dispose();
				}
				return exceptionRows;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x0000DE1C File Offset: 0x0000C01C
		public ISerializedException PageException
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				ISerializedException pageException;
				try
				{
					pageException = this.page.PageException;
				}
				finally
				{
					u.Dispose();
				}
				return pageException;
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0000DE6C File Offset: 0x0000C06C
		public void Dispose()
		{
			T t = this.context;
			U u = t.Enter();
			try
			{
				this.page.Dispose();
			}
			finally
			{
				u.Dispose();
			}
		}

		// Token: 0x04000495 RID: 1173
		private const int rowCountNotSet = -1;

		// Token: 0x04000496 RID: 1174
		protected readonly T context;

		// Token: 0x04000497 RID: 1175
		protected readonly IPage page;
	}
}
