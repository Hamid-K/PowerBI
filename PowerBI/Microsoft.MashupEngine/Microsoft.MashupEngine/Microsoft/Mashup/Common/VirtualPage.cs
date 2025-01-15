using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C36 RID: 7222
	public abstract class VirtualPage : IPage, IDisposable
	{
		// Token: 0x17002D16 RID: 11542
		// (get) Token: 0x0600B44A RID: 46154
		protected abstract IPage Page { get; }

		// Token: 0x17002D17 RID: 11543
		// (get) Token: 0x0600B44B RID: 46155 RVA: 0x002495AE File Offset: 0x002477AE
		public virtual int ColumnCount
		{
			get
			{
				return this.Page.ColumnCount;
			}
		}

		// Token: 0x17002D18 RID: 11544
		// (get) Token: 0x0600B44C RID: 46156 RVA: 0x002495BB File Offset: 0x002477BB
		public virtual int RowCount
		{
			get
			{
				IPage page = this.Page;
				if (page == null)
				{
					return 0;
				}
				return page.RowCount;
			}
		}

		// Token: 0x0600B44D RID: 46157 RVA: 0x002495CE File Offset: 0x002477CE
		public virtual IColumn GetColumn(int ordinal)
		{
			return this.Page.GetColumn(ordinal);
		}

		// Token: 0x17002D19 RID: 11545
		// (get) Token: 0x0600B44E RID: 46158 RVA: 0x002495DC File Offset: 0x002477DC
		public virtual IDictionary<int, IExceptionRow> ExceptionRows
		{
			get
			{
				return this.Page.ExceptionRows;
			}
		}

		// Token: 0x17002D1A RID: 11546
		// (get) Token: 0x0600B44F RID: 46159 RVA: 0x002495E9 File Offset: 0x002477E9
		public virtual ISerializedException PageException
		{
			get
			{
				IPage page = this.Page;
				if (page == null)
				{
					return null;
				}
				return page.PageException;
			}
		}

		// Token: 0x0600B450 RID: 46160 RVA: 0x002495FC File Offset: 0x002477FC
		public virtual void Dispose()
		{
			this.Page.Dispose();
		}
	}
}
