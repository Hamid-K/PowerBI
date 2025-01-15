using System;
using Microsoft.Data.Serialization;

namespace Microsoft.Internal
{
	// Token: 0x02000188 RID: 392
	internal class ContextAwarePageReader<T, U> : IPageReader, IDisposable where T : struct, IContext<U> where U : struct, IDisposable
	{
		// Token: 0x0600078A RID: 1930 RVA: 0x0000D96E File Offset: 0x0000BB6E
		public ContextAwarePageReader(T context, IPageReader reader)
		{
			this.context = context;
			this.reader = reader;
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x0000D984 File Offset: 0x0000BB84
		public TableSchema Schema
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				TableSchema schema;
				try
				{
					schema = this.reader.Schema;
				}
				finally
				{
					u.Dispose();
				}
				return schema;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x0000D9D4 File Offset: 0x0000BBD4
		public IProgress Progress
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				IProgress progress;
				try
				{
					progress = this.context.Marshal(this.reader.Progress);
				}
				finally
				{
					u.Dispose();
				}
				return progress;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x0000DA30 File Offset: 0x0000BC30
		public int MaxPageRowCount
		{
			get
			{
				T t = this.context;
				U u = t.Enter();
				int maxPageRowCount;
				try
				{
					maxPageRowCount = this.MaxPageRowCount;
				}
				finally
				{
					u.Dispose();
				}
				return maxPageRowCount;
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0000DA7C File Offset: 0x0000BC7C
		public IPage CreatePage()
		{
			T t = this.context;
			U u = t.Enter();
			IPage page;
			try
			{
				page = this.context.Marshal(this.reader.CreatePage());
			}
			finally
			{
				u.Dispose();
			}
			return page;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0000DAD8 File Offset: 0x0000BCD8
		public void Read(IPage page)
		{
			T t = this.context;
			U u = t.Enter();
			try
			{
				ContextAwarePage<T, U> contextAwarePage = page as ContextAwarePage<T, U>;
				if (contextAwarePage != null)
				{
					this.reader.Read(contextAwarePage.Page);
				}
				else
				{
					this.reader.Read(this.context.ReverseMarshal(page));
				}
			}
			finally
			{
				u.Dispose();
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0000DB50 File Offset: 0x0000BD50
		public IPageReader NextResult()
		{
			T t = this.context;
			U u = t.Enter();
			IPageReader pageReader2;
			try
			{
				IPageReader pageReader = this.reader.NextResult();
				pageReader2 = ((pageReader != null) ? this.context.Marshal(pageReader) : null);
			}
			finally
			{
				u.Dispose();
			}
			return pageReader2;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0000DBB4 File Offset: 0x0000BDB4
		public void Dispose()
		{
			T t = this.context;
			U u = t.Enter();
			try
			{
				this.reader.Dispose();
			}
			finally
			{
				u.Dispose();
			}
		}

		// Token: 0x04000491 RID: 1169
		protected readonly T context;

		// Token: 0x04000492 RID: 1170
		protected readonly IPageReader reader;
	}
}
