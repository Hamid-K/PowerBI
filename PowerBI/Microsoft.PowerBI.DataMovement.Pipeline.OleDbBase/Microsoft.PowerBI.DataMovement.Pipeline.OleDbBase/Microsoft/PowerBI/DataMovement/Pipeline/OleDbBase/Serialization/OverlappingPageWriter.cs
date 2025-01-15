using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000E0 RID: 224
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class OverlappingPageWriter : IDisposable
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x0000C934 File Offset: 0x0000AB34
		public OverlappingPageWriter(OleDbPageWriter writer, IPage page1, IPage page2, Action<Exception> exceptionHandler)
		{
			this.writer = writer;
			this.exceptionHandler = exceptionHandler;
			this.freePages = new BlockingQueue<IPage>(2);
			this.pagesToSerialize = new BlockingQueue<IPage>(2);
			this.waitHandle = new ManualResetEvent(false);
			this.writerHandle = new AutoResetEvent(false);
			this.freePages.Enqueue(page1);
			this.freePages.Enqueue(page2);
			this.page = this.freePages.Dequeue();
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.PageSerializer));
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0000C9C1 File Offset: 0x0000ABC1
		public IPage Page
		{
			get
			{
				return this.page;
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000C9C9 File Offset: 0x0000ABC9
		public void Write()
		{
			this.pagesToSerialize.Enqueue(this.page);
			this.page = this.freePages.Dequeue();
			this.CheckException();
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000C9F3 File Offset: 0x0000ABF3
		public void Dispose()
		{
			this.pagesToSerialize.Enqueue(null);
			this.waitHandle.WaitOne();
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000CA0D File Offset: 0x0000AC0D
		public void Flush()
		{
			this.pagesToSerialize.Enqueue(OverlappingPageWriter.flushPage);
			this.writerHandle.WaitOne();
			this.CheckException();
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000CA31 File Offset: 0x0000AC31
		private void CheckException()
		{
			if (this.exception != null)
			{
				this.exceptionHandler(this.exception);
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000CA4C File Offset: 0x0000AC4C
		private void PageSerializer(object obj)
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

		// Token: 0x040003DB RID: 987
		private static readonly IPage flushPage = new OverlappingPageWriter.FlushPage();

		// Token: 0x040003DC RID: 988
		private readonly OleDbPageWriter writer;

		// Token: 0x040003DD RID: 989
		private readonly Action<Exception> exceptionHandler;

		// Token: 0x040003DE RID: 990
		private readonly BlockingQueue<IPage> freePages;

		// Token: 0x040003DF RID: 991
		private readonly BlockingQueue<IPage> pagesToSerialize;

		// Token: 0x040003E0 RID: 992
		private readonly ManualResetEvent waitHandle;

		// Token: 0x040003E1 RID: 993
		private readonly AutoResetEvent writerHandle;

		// Token: 0x040003E2 RID: 994
		private IPage page;

		// Token: 0x040003E3 RID: 995
		private Exception exception;

		// Token: 0x020000FB RID: 251
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal class FlushPage : IPage, IDisposable
		{
			// Token: 0x1700012C RID: 300
			// (get) Token: 0x06000513 RID: 1299 RVA: 0x0000F791 File Offset: 0x0000D991
			public int ColumnCount
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x06000514 RID: 1300 RVA: 0x0000F798 File Offset: 0x0000D998
			public int RowCount
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000F79F File Offset: 0x0000D99F
			[global::System.Runtime.CompilerServices.Nullable(1)]
			public IDictionary<int, IExceptionRow> ExceptionRows
			{
				[global::System.Runtime.CompilerServices.NullableContext(1)]
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x06000516 RID: 1302 RVA: 0x0000F7A6 File Offset: 0x0000D9A6
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			public IColumn GetColumn(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000517 RID: 1303 RVA: 0x0000F7AD File Offset: 0x0000D9AD
			public void Dispose()
			{
				throw new NotImplementedException();
			}
		}
	}
}
