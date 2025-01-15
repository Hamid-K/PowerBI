using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using Microsoft.SqlServer.XEvent.Linq.Internal;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000BE RID: 190
	internal class XEventFileProvider<TEvent> : IEventBufferStore, IEventProvider<TEvent>, IEnumerable<TEvent>, IEnumerable, IQueryProvider, IDisposable where TEvent : PublishedEvent
	{
		// Token: 0x06000227 RID: 551 RVA: 0x0001A6D8 File Offset: 0x0001A6D8
		internal XEventFileProvider(QueryableXEventData queryable, string[] fileList, string[] metadataFiles)
		{
			this.m_queryable = queryable;
			this.m_fileReader = new XEventInteropFileReader(fileList, metadataFiles);
			this.m_buffers = new Dictionary<BufferLocator, XEventCachedBuffer>();
			BufferLocator bufferLocator;
			byte[] firstBuf = this.m_fileReader.GetFirstBuf(out bufferLocator);
			if (firstBuf != null)
			{
				this.m_firstBuffer = new XEventCachedBuffer(bufferLocator, firstBuf, this);
				this.m_buffers[bufferLocator] = this.m_firstBuffer;
				this.m_readAheadQueue = new ConcurrentQueue<BufferLocator>();
				this.m_readAheadQueue.Enqueue(bufferLocator);
				this.m_readAheadEvent = new AutoResetEvent(true);
				this.m_readAheadThread = new Thread(new ThreadStart(this.ExecuteReadAhead));
				this.m_readAheadThread.IsBackground = true;
				this.m_readAheadThread.Start();
				return;
			}
			this.m_firstBuffer = null;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0001A79C File Offset: 0x0001A79C
		public IQueryable<TData> CreateQuery<TData>(Expression expression)
		{
			QueryBuilder.CheckExpressionTypeSupported(expression);
			this.m_queryable.Expression = expression;
			return (IQueryable<TData>)this.m_queryable;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0001A7C8 File Offset: 0x0001A7C8
		public IQueryable CreateQuery(Expression expression)
		{
			IQueryable queryable;
			try
			{
				QueryBuilder.CheckExpressionTypeSupported(expression);
				this.m_queryable.Expression = expression;
				queryable = this.m_queryable;
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
			return queryable;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0001A818 File Offset: 0x0001A818
		public TResult Execute<TResult>(Expression expression)
		{
			return (TResult)((object)this.ExecuteInternal(expression));
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0001A834 File Offset: 0x0001A834
		public object Execute(Expression expression)
		{
			return this.ExecuteInternal(expression);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0001A848 File Offset: 0x0001A848
		public IEnumerator<TEvent> GetEnumerator()
		{
			return (IEnumerator<TEvent>)new XEventEnumerator(this, this.m_fileReader.InteropMetadata);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0001A86C File Offset: 0x0001A86C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new XEventEnumerator(this, this.m_fileReader.InteropMetadata);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0001A88C File Offset: 0x0001A88C
		public void Stop()
		{
			this.m_stopped = true;
			if (this.m_readAheadEvent != null)
			{
				this.m_readAheadEvent.Set();
				if (this.m_readAheadThread != null)
				{
					this.m_readAheadThread.Join();
				}
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0001A8CC File Offset: 0x0001A8CC
		public TEvent RetrieveEvent(EventLocator eventLocation)
		{
			using (XEventInteropBufferProcessor xeventInteropBufferProcessor = new XEventInteropBufferProcessor(this.m_fileReader.InteropMetadata))
			{
				BufferLocator bufferLocator = new BufferLocator();
				BufferLocatorHelper.InitBufLocator(bufferLocator, eventLocation);
				byte[] array;
				if (this.GetBuffer(bufferLocator, out array) && xeventInteropBufferProcessor.Reset(bufferLocator, array, BufferLocatorHelper.GetEventOffset(eventLocation)))
				{
					return (TEvent)((object)xeventInteropBufferProcessor.Current);
				}
			}
			throw new EventLocationException();
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0001A950 File Offset: 0x0001A950
		public ReadOnlyCollection<IMetadataGeneration> MetadataGenerations
		{
			get
			{
				return this.m_fileReader.MetadataGenerations;
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0001A968 File Offset: 0x0001A968
		public void SerializeEvent(IEventSerializer serializationContext, TEvent serializableEvent)
		{
			using (XEventInteropBufferProcessor xeventInteropBufferProcessor = new XEventInteropBufferProcessor(this.m_fileReader.InteropMetadata))
			{
				BufferLocator bufferLocator = new BufferLocator();
				BufferLocatorHelper.InitBufLocator(bufferLocator, serializableEvent.Location);
				byte[] array;
				if (this.GetBuffer(bufferLocator, out array) && xeventInteropBufferProcessor.Reset(bufferLocator, array, BufferLocatorHelper.GetEventOffset(serializableEvent.Location)))
				{
					xeventInteropBufferProcessor.SerializeEvent(serializationContext, serializableEvent.Package.Generation);
				}
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0001AA04 File Offset: 0x0001AA04
		private bool InsertCachedBuffer(XEventCachedBuffer prevBuffer, XEventCachedBuffer newBuffer)
		{
			bool flag = false;
			Dictionary<BufferLocator, XEventCachedBuffer> buffers = this.m_buffers;
			lock (buffers)
			{
				if (prevBuffer.Next == null && !this.m_buffers.ContainsKey(newBuffer.BufferId))
				{
					this.m_buffers[newBuffer.BufferId] = newBuffer;
					prevBuffer.Next = newBuffer;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0001AA84 File Offset: 0x0001AA84
		private void EnqueueReadAhead(BufferLocator bufferId)
		{
			if (!this.m_stopped)
			{
				this.m_readAheadQueue.Enqueue(bufferId);
				this.m_readAheadEvent.Set();
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0001AAB4 File Offset: 0x0001AAB4
		private void ExecuteReadAhead()
		{
			try
			{
				int num = 0;
				while (!this.m_stopped)
				{
					this.m_readAheadEvent.WaitOne(10000);
					if (this.m_stopped || ++num > 3)
					{
						this.m_stopped = true;
						break;
					}
					while (!this.m_stopped && this.m_readAheadQueue.Count != 0)
					{
						num = 0;
						BufferLocator bufferLocator;
						if (this.m_readAheadQueue.TryDequeue(out bufferLocator))
						{
							XEventCachedBuffer xeventCachedBuffer;
							this.m_buffers.TryGetValue(bufferLocator, out xeventCachedBuffer);
							int num2 = 0;
							while (!this.m_stopped && num2 < this.m_readAheadLimit && xeventCachedBuffer != null && xeventCachedBuffer.Next == null)
							{
								BufferLocator bufferLocator2;
								byte[] nextBuffer = this.m_fileReader.GetNextBuffer(bufferLocator, out bufferLocator2);
								if (nextBuffer == null)
								{
									this.m_stopped = true;
									break;
								}
								XEventCachedBuffer xeventCachedBuffer2 = new XEventCachedBuffer(bufferLocator2, nextBuffer, this);
								if (!this.InsertCachedBuffer(xeventCachedBuffer, xeventCachedBuffer2))
								{
									break;
								}
								bufferLocator = bufferLocator2;
								xeventCachedBuffer = xeventCachedBuffer2;
								num2++;
							}
						}
					}
				}
				goto IL_0102;
			}
			catch (Exception)
			{
				this.m_stopped = true;
				goto IL_0102;
			}
			IL_00F4:
			BufferLocator bufferLocator3;
			this.m_readAheadQueue.TryDequeue(out bufferLocator3);
			IL_0102:
			if (this.m_readAheadQueue.Count <= 0)
			{
				return;
			}
			goto IL_00F4;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0001ABF0 File Offset: 0x0001ABF0
		public bool GetNextBuffer(BufferLocator prevBufferId, out BufferLocator bufferId, out byte[] buffer)
		{
			bufferId = null;
			buffer = null;
			XEventCachedBuffer xeventCachedBuffer;
			if (prevBufferId == null)
			{
				if (this.m_firstBuffer != null)
				{
					bufferId = this.m_firstBuffer.BufferId;
					buffer = this.m_firstBuffer.GetBuffer();
				}
			}
			else if (this.m_buffers.TryGetValue(prevBufferId, out xeventCachedBuffer))
			{
				if (xeventCachedBuffer.Next != null)
				{
					bufferId = xeventCachedBuffer.Next.BufferId;
					buffer = xeventCachedBuffer.Next.GetBuffer();
					if (xeventCachedBuffer.Next.Next == null)
					{
						this.EnqueueReadAhead(xeventCachedBuffer.Next.BufferId);
					}
				}
				else
				{
					buffer = this.m_fileReader.GetNextBuffer(prevBufferId, out bufferId);
					if (buffer != null)
					{
						XEventCachedBuffer xeventCachedBuffer2 = new XEventCachedBuffer(bufferId, buffer, this);
						this.InsertCachedBuffer(xeventCachedBuffer, xeventCachedBuffer2);
						this.EnqueueReadAhead(bufferId);
					}
				}
			}
			return buffer != null;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0001ACBC File Offset: 0x0001ACBC
		public bool GetBuffer(BufferLocator bufferId, out byte[] buffer)
		{
			buffer = null;
			XEventCachedBuffer xeventCachedBuffer;
			if (this.m_buffers.TryGetValue(bufferId, out xeventCachedBuffer))
			{
				buffer = xeventCachedBuffer.GetBuffer();
			}
			return buffer != null;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0001ACEC File Offset: 0x0001ACEC
		public bool GetBufferDirect(BufferLocator bufferId, int bufferSize, out byte[] buffer)
		{
			buffer = this.m_fileReader.GetBuffer(bufferId);
			return buffer != null;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0001AD0C File Offset: 0x0001AD0C
		public Exception LastException
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0001AD1C File Offset: 0x0001AD1C
		public void Dispose()
		{
			this.Stop();
			this.m_fileReader.Dispose();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0001AD40 File Offset: 0x0001AD40
		internal object ExecuteInternal(Expression expression)
		{
			QueryBuilder.CheckExpressionTypeSupported(expression);
			return this;
		}

		// Token: 0x0400024F RID: 591
		private QueryableXEventData m_queryable;

		// Token: 0x04000250 RID: 592
		private XEventInteropFileReader m_fileReader;

		// Token: 0x04000251 RID: 593
		private Dictionary<BufferLocator, XEventCachedBuffer> m_buffers;

		// Token: 0x04000252 RID: 594
		private XEventCachedBuffer m_firstBuffer;

		// Token: 0x04000253 RID: 595
		private ConcurrentQueue<BufferLocator> m_readAheadQueue;

		// Token: 0x04000254 RID: 596
		private Thread m_readAheadThread;

		// Token: 0x04000255 RID: 597
		private AutoResetEvent m_readAheadEvent;

		// Token: 0x04000256 RID: 598
		private volatile bool m_stopped;

		// Token: 0x04000257 RID: 599
		private int m_readAheadLimit = 5;

		// Token: 0x04000258 RID: 600
		private const int m_readAheadIdleTimeout = 10000;

		// Token: 0x04000259 RID: 601
		private const int m_readAheadIdleLoops = 3;
	}
}
