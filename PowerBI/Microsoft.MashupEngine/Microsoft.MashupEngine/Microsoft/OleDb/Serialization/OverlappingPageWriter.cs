using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FBE RID: 8126
	public class OverlappingPageWriter : IDisposable
	{
		// Token: 0x0600C644 RID: 50756 RVA: 0x00278578 File Offset: 0x00276778
		public OverlappingPageWriter(Action<ThreadStart> startThread, OleDbPageWriter writer, IPage page1, IPage page2, Action<Exception> exceptionHandler, Func<ISerializedException, Exception> pageExceptionHandler = null)
		{
			this.writer = writer;
			this.exceptionHandler = exceptionHandler;
			this.pageExceptionHandler = pageExceptionHandler;
			this.freePages = new BlockingQueue<IPage>(2);
			this.pagesToSerialize = new BlockingQueue<IPage>(2);
			this.waitHandle = new ManualResetEvent(false);
			this.writerHandle = new AutoResetEvent(false);
			this.freePages.Enqueue(page1);
			this.freePages.Enqueue(page2);
			this.page = this.freePages.Dequeue();
			startThread(new ThreadStart(this.PageSerializer));
		}

		// Token: 0x17003019 RID: 12313
		// (get) Token: 0x0600C645 RID: 50757 RVA: 0x0027860E File Offset: 0x0027680E
		public IPage Page
		{
			get
			{
				return this.page;
			}
		}

		// Token: 0x0600C646 RID: 50758 RVA: 0x00278616 File Offset: 0x00276816
		public void Write()
		{
			this.pagesToSerialize.Enqueue(this.page);
			this.page = this.freePages.Dequeue();
			this.CheckException();
		}

		// Token: 0x0600C647 RID: 50759 RVA: 0x00278640 File Offset: 0x00276840
		public void Dispose()
		{
			this.pagesToSerialize.Enqueue(null);
			this.waitHandle.WaitOne();
		}

		// Token: 0x0600C648 RID: 50760 RVA: 0x0027865A File Offset: 0x0027685A
		public void Flush()
		{
			this.pagesToSerialize.Enqueue(OverlappingPageWriter.flushPage);
			this.writerHandle.WaitOne();
			this.CheckException();
		}

		// Token: 0x0600C649 RID: 50761 RVA: 0x0027867E File Offset: 0x0027687E
		private void CheckException()
		{
			if (this.exception != null)
			{
				this.exceptionHandler(this.exception);
			}
		}

		// Token: 0x0600C64A RID: 50762 RVA: 0x0027869C File Offset: 0x0027689C
		private void PageSerializer()
		{
			for (;;)
			{
				IPage page = this.pagesToSerialize.Dequeue();
				if (page == null)
				{
					break;
				}
				if (page == OverlappingPageWriter.flushPage)
				{
					if (this.exception == null)
					{
						try
						{
							this.writer.Flush();
						}
						catch (Exception ex)
						{
							this.exception = ex;
						}
					}
					this.writerHandle.Set();
				}
				else
				{
					if (this.exception == null)
					{
						try
						{
							if (page.PageException != null && this.pageExceptionHandler != null)
							{
								throw this.pageExceptionHandler(page.PageException);
							}
							this.writer.Write(page);
						}
						catch (Exception ex2)
						{
							this.exception = ex2;
						}
					}
					this.freePages.Enqueue(page);
				}
			}
			this.waitHandle.Set();
		}

		// Token: 0x04006554 RID: 25940
		private static readonly IPage flushPage = new OverlappingPageWriter.FlushPage();

		// Token: 0x04006555 RID: 25941
		private readonly OleDbPageWriter writer;

		// Token: 0x04006556 RID: 25942
		private readonly Action<Exception> exceptionHandler;

		// Token: 0x04006557 RID: 25943
		private readonly BlockingQueue<IPage> freePages;

		// Token: 0x04006558 RID: 25944
		private readonly BlockingQueue<IPage> pagesToSerialize;

		// Token: 0x04006559 RID: 25945
		private readonly ManualResetEvent waitHandle;

		// Token: 0x0400655A RID: 25946
		private readonly AutoResetEvent writerHandle;

		// Token: 0x0400655B RID: 25947
		private readonly Func<ISerializedException, Exception> pageExceptionHandler;

		// Token: 0x0400655C RID: 25948
		private IPage page;

		// Token: 0x0400655D RID: 25949
		private Exception exception;

		// Token: 0x02001FBF RID: 8127
		private class FlushPage : IPage, IDisposable
		{
			// Token: 0x1700301A RID: 12314
			// (get) Token: 0x0600C64C RID: 50764 RVA: 0x000091AE File Offset: 0x000073AE
			public int ColumnCount
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700301B RID: 12315
			// (get) Token: 0x0600C64D RID: 50765 RVA: 0x000091AE File Offset: 0x000073AE
			public int RowCount
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x0600C64E RID: 50766 RVA: 0x000091AE File Offset: 0x000073AE
			public IColumn GetColumn(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x1700301C RID: 12316
			// (get) Token: 0x0600C64F RID: 50767 RVA: 0x000091AE File Offset: 0x000073AE
			public IDictionary<int, IExceptionRow> ExceptionRows
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700301D RID: 12317
			// (get) Token: 0x0600C650 RID: 50768 RVA: 0x000091AE File Offset: 0x000073AE
			public ISerializedException PageException
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x0600C651 RID: 50769 RVA: 0x000091AE File Offset: 0x000073AE
			public void Dispose()
			{
				throw new NotImplementedException();
			}
		}
	}
}
