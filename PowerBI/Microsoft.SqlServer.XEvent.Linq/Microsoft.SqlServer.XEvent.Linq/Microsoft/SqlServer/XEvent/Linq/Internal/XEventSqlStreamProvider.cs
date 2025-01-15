using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Xml;

namespace Microsoft.SqlServer.XEvent.Linq.Internal
{
	// Token: 0x020000C4 RID: 196
	internal class XEventSqlStreamProvider<TEvent> : IEventBufferStore, IEventProvider<TEvent>, IEnumerable<TEvent>, IEnumerable, IQueryProvider, IDisposable where TEvent : PublishedEvent
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0001B1AC File Offset: 0x0001B1AC
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0001B1C4 File Offset: 0x0001B1C4
		public bool Stopped
		{
			get
			{
				return this.m_stopped;
			}
			set
			{
				this.m_stopped = value;
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0001B1DC File Offset: 0x0001B1DC
		public XEventSqlStreamProvider(QueryableXEventData queryable, string connectionString, string source, EventStreamSourceOptions streamSource, EventStreamCacheOptions cacheOption)
		{
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
			if (string.Compare(sqlConnectionStringBuilder.ApplicationName, ".NET SqlClient Data Provider", true) == 0)
			{
				if (streamSource == EventStreamSourceOptions.EventStream)
				{
					sqlConnectionStringBuilder.ApplicationName = XEventSqlStreamProvider<TEvent>.sm_streamAppName + source;
				}
				else
				{
					sqlConnectionStringBuilder.ApplicationName = XEventSqlStreamProvider<TEvent>.sm_fileStreamAppName;
				}
			}
			sqlConnectionStringBuilder.AsynchronousProcessing = true;
			this.m_sqlConnection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
			this.CommonInitialization(queryable, streamSource, cacheOption, source);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0001B264 File Offset: 0x0001B264
		public XEventSqlStreamProvider(QueryableXEventData queryable, XmlReader inputReader, EventStreamSourceOptions streamSource, EventStreamCacheOptions cacheOption)
		{
			this.m_IsSqlDataSource = false;
			this.m_xmlInputReader = inputReader;
			this.m_xeasReader = new XEASXmlReader(this.m_xmlInputReader);
			this.CommonInitialization(queryable, streamSource, cacheOption, "");
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0001B2BC File Offset: 0x0001B2BC
		private void CommonInitialization(QueryableXEventData queryable, EventStreamSourceOptions streamSource, EventStreamCacheOptions cacheOption, string sqlSource = "")
		{
			this.m_queryable = queryable;
			this.m_streamSource = streamSource;
			this.m_cacheOption = cacheOption;
			IDataReader dataReader = null;
			IAsyncResult asyncResult = null;
			if (this.m_IsSqlDataSource)
			{
				this.m_sqlConnection.Open();
				this.m_sqlCommand = new SqlCommand(XEventSqlStreamProvider<TEvent>.sm_queryString, this.m_sqlConnection);
				this.m_sqlCommand.CommandTimeout = 0;
				this.m_sqlCommand.Parameters.Add("@source", SqlDbType.NVarChar, 256).Value = sqlSource;
				this.m_sqlCommand.Parameters.Add("@sourceopt", SqlDbType.Int).Value = (int)this.m_streamSource;
				asyncResult = this.m_sqlCommand.BeginExecuteReader(CommandBehavior.SequentialAccess);
			}
			else
			{
				dataReader = this.m_xeasReader;
			}
			this.m_bufWait = new ManualResetEvent(false);
			this.m_buffers = new Dictionary<BufferLocator, XEventCachedBuffer>();
			this.m_lastBuffer = null;
			this.m_metadataReadyEvent = new ManualResetEvent(false);
			this.m_bufferLock = new Mutex();
			this.m_metadata = new XEventInteropMetadataManager();
			if (this.m_cacheOption == EventStreamCacheOptions.CacheToDisk)
			{
				this.m_dataStore = new FileStream[32767];
				this.CreateNextStoreFile();
			}
			this.m_partialFirstRow = false;
			if (this.m_IsSqlDataSource)
			{
				asyncResult.AsyncWaitHandle.WaitOne();
				this.m_reader = this.m_sqlCommand.EndExecuteReader(asyncResult);
				dataReader = this.m_reader;
			}
			if (dataReader.Read())
			{
				int @int = dataReader.GetInt32(0);
				if (@int == 2)
				{
					long bytes = dataReader.GetBytes(1, 0L, null, 0, 0);
					byte[] array = new byte[bytes];
					dataReader.GetBytes(1, 0L, array, 0, (int)bytes);
					if (!XEventFileHeader.IsVersionCompatible(array))
					{
						throw new EventStreamVersionException(string.Format(Resources.GetString("IncompatibleStreamVersion"), Array.Empty<object>()));
					}
					dataReader.Read();
				}
				else
				{
					this.m_partialFirstRow = true;
					this.m_firstRowType = @int;
				}
				this.m_readerThread = new Thread(new ThreadStart(this.ExecuteReader));
				this.m_readerThread.IsBackground = true;
				this.m_readerThread.Start();
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0001B4B4 File Offset: 0x0001B4B4
		public IQueryable<TData> CreateQuery<TData>(Expression expression)
		{
			QueryBuilder.CheckExpressionTypeSupported(expression);
			this.m_queryable.Expression = expression;
			return (IQueryable<TData>)this.m_queryable;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0001B4E0 File Offset: 0x0001B4E0
		public IQueryable CreateQuery(Expression expression)
		{
			QueryBuilder.CheckExpressionTypeSupported(expression);
			IQueryable queryable;
			try
			{
				this.m_queryable.Expression = expression;
				queryable = this.m_queryable;
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
			return queryable;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0001B530 File Offset: 0x0001B530
		public TResult Execute<TResult>(Expression expression)
		{
			return (TResult)((object)this.ExecuteInternal(expression));
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0001B54C File Offset: 0x0001B54C
		public object Execute(Expression expression)
		{
			return this.ExecuteInternal(expression);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0001B560 File Offset: 0x0001B560
		public IEnumerator<TEvent> GetEnumerator()
		{
			this.m_metadataReadyEvent.WaitOne();
			return (IEnumerator<TEvent>)new XEventEnumerator(this, this.m_metadata);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0001B58C File Offset: 0x0001B58C
		IEnumerator IEnumerable.GetEnumerator()
		{
			this.m_metadataReadyEvent.WaitOne();
			return new XEventEnumerator(this, this.m_metadata);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0001B5B4 File Offset: 0x0001B5B4
		public void Stop()
		{
			if (!this.m_stopped)
			{
				this.m_stopped = true;
				this.m_sqlCommand.Cancel();
				if (this.m_readerThread != null)
				{
					if (!this.m_readerThread.Join(250))
					{
						this.m_readerThread.Abort();
					}
					this.m_readerThread = null;
				}
				this.m_metadataReadyEvent.Set();
				this.m_bufWait.Set();
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0001B624 File Offset: 0x0001B624
		public TEvent RetrieveEvent(EventLocator eventLocation)
		{
			if (this.m_cacheOption == EventStreamCacheOptions.DoNotCache)
			{
				throw new NotSupportedException();
			}
			using (XEventInteropBufferProcessor xeventInteropBufferProcessor = new XEventInteropBufferProcessor(this.m_metadata))
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

		// Token: 0x06000259 RID: 601 RVA: 0x0001B6B0 File Offset: 0x0001B6B0
		public void SerializeEvent(IEventSerializer serializationContext, TEvent serializeEvent)
		{
			using (XEventInteropBufferProcessor xeventInteropBufferProcessor = new XEventInteropBufferProcessor(this.m_metadata))
			{
				BufferLocator bufferLocator = new BufferLocator();
				BufferLocatorHelper.InitBufLocator(bufferLocator, serializeEvent.Location);
				byte[] array;
				if (this.GetBuffer(bufferLocator, out array) && xeventInteropBufferProcessor.Reset(bufferLocator, array, BufferLocatorHelper.GetEventOffset(serializeEvent.Location)))
				{
					xeventInteropBufferProcessor.SerializeEvent(serializationContext, serializeEvent.Package.Generation);
				}
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0001B744 File Offset: 0x0001B744
		public ReadOnlyCollection<IMetadataGeneration> MetadataGenerations
		{
			get
			{
				this.m_metadataReadyEvent.WaitOne();
				return this.m_metadata.MetadataGenerations;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0001B768 File Offset: 0x0001B768
		public bool GetNextBuffer(BufferLocator prevBufferId, out BufferLocator bufferId, out byte[] buffer)
		{
			bufferId = null;
			buffer = null;
			for (;;)
			{
				if ((this.m_lastBuffer == null || this.m_lastBuffer.BufferId == prevBufferId) && !this.m_stopped)
				{
					try
					{
						this.m_bufferLock.WaitOne();
						if (this.m_lastBuffer != null && this.m_lastBuffer.BufferId != prevBufferId)
						{
							goto IL_00D2;
						}
						Interlocked.Increment(ref this.m_waitersCount);
					}
					finally
					{
						this.m_bufferLock.ReleaseMutex();
					}
					this.m_bufWait.WaitOne();
					if (this.m_stopped && (this.m_lastBuffer == null || this.m_lastBuffer.BufferId == prevBufferId))
					{
						break;
					}
					if (Interlocked.Decrement(ref this.m_waitersCount) == 0)
					{
						this.m_bufWait.Reset();
						continue;
					}
					continue;
				}
				IL_00D2:
				if (prevBufferId == null)
				{
					goto Block_6;
				}
				XEventCachedBuffer xeventCachedBuffer;
				if (this.m_buffers.TryGetValue(prevBufferId, out xeventCachedBuffer) && xeventCachedBuffer.Next != null)
				{
					bufferId = xeventCachedBuffer.Next.BufferId;
					buffer = xeventCachedBuffer.Next.GetBuffer();
				}
				if (this.m_cacheOption != EventStreamCacheOptions.DoNotCache)
				{
					goto IL_01C1;
				}
				this.RemoveExpiredBufferDescriptors();
				if (buffer != null || !(prevBufferId != this.m_lastBuffer.BufferId))
				{
					goto IL_01C1;
				}
				XEventCachedBuffer xeventCachedBuffer2 = this.m_firstBuffer;
				while (xeventCachedBuffer2.BufferId <= prevBufferId && xeventCachedBuffer2.BufferId != this.m_lastBuffer.BufferId)
				{
					try
					{
						this.m_bufferLock.WaitOne();
						xeventCachedBuffer2 = xeventCachedBuffer2.Next;
					}
					finally
					{
						this.m_bufferLock.ReleaseMutex();
					}
				}
				prevBufferId = xeventCachedBuffer2.BufferId;
			}
			return false;
			Block_6:
			if (this.m_firstBuffer != null)
			{
				bufferId = this.m_firstBuffer.BufferId;
				buffer = this.m_firstBuffer.GetBuffer();
			}
			IL_01C1:
			return buffer != null;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0001B970 File Offset: 0x0001B970
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

		// Token: 0x0600025D RID: 605 RVA: 0x0001B9A0 File Offset: 0x0001B9A0
		public bool GetBufferDirect(BufferLocator bufferId, int bufferSize, out byte[] buffer)
		{
			buffer = null;
			if (this.m_cacheOption == EventStreamCacheOptions.CacheToDisk)
			{
				FileStream fileStream = this.m_dataStore[(int)bufferId.m_fileId];
				ulong bufferOffset = BufferLocatorHelper.GetBufferOffset(bufferId);
				byte[] array = new byte[bufferSize];
				FileStream fileStream2 = fileStream;
				lock (fileStream2)
				{
					fileStream.Position = (long)bufferOffset;
					fileStream.Read(array, 0, bufferSize);
				}
				buffer = this.DecodeBuffer(array);
			}
			return buffer != null;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0001BA2C File Offset: 0x0001BA2C
		public Exception LastException
		{
			get
			{
				return this.m_lastAsyncException;
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0001BA40 File Offset: 0x0001BA40
		private byte[] DecodeBuffer(byte[] encodedBuffer)
		{
			return encodedBuffer;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0001BA50 File Offset: 0x0001BA50
		private void CreateNextStoreFile()
		{
			if (this.m_fileStoreIndex > 32766)
			{
				throw new OutOfMemoryException();
			}
			this.m_fileStoreIndex += 1;
			this.m_dataStore[(int)this.m_fileStoreIndex] = new FileStream(Path.GetTempFileName() + ".txl", FileMode.Create, FileAccess.ReadWrite, FileShare.Read, 512, FileOptions.Asynchronous | FileOptions.RandomAccess | FileOptions.DeleteOnClose);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0001BAB0 File Offset: 0x0001BAB0
		private void RemoveExpiredBufferDescriptors()
		{
			XEventCachedBuffer xeventCachedBuffer = this.m_firstBuffer;
			while (xeventCachedBuffer.BufferId != this.m_lastBuffer.BufferId)
			{
				try
				{
					this.m_bufferLock.WaitOne();
					while (!xeventCachedBuffer.Next.IsAlive && xeventCachedBuffer.Next.BufferId != this.m_lastBuffer.BufferId)
					{
						this.m_buffers.Remove(xeventCachedBuffer.Next.BufferId);
						xeventCachedBuffer.Next = xeventCachedBuffer.Next.Next;
					}
					xeventCachedBuffer = xeventCachedBuffer.Next;
				}
				finally
				{
					this.m_bufferLock.ReleaseMutex();
				}
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0001BB7C File Offset: 0x0001BB7C
		public void Dispose()
		{
			this.Stop();
			if (this.m_sqlConnection != null && this.m_sqlConnection.State != ConnectionState.Closed)
			{
				this.m_sqlConnection.Close();
			}
			else if (this.m_xeasReader != null && !this.m_xeasReader.IsClosed)
			{
				this.m_xeasReader.Close();
			}
			if (this.m_dataStore != null)
			{
				for (int i = 0; i < (int)this.m_fileStoreIndex; i++)
				{
					this.m_dataStore[(int)this.m_fileStoreIndex].Close();
					this.m_dataStore[(int)this.m_fileStoreIndex].Dispose();
				}
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0001BC14 File Offset: 0x0001BC14
		private void ExecuteReader()
		{
			IDataReader dataReader2;
			if (!this.m_IsSqlDataSource)
			{
				IDataReader dataReader = this.m_xeasReader;
				dataReader2 = dataReader;
			}
			else
			{
				IDataReader dataReader = this.m_reader;
				dataReader2 = dataReader;
			}
			IDataReader dataReader3 = dataReader2;
			try
			{
				ulong num = 0UL;
				bool flag = true;
				bool flag2 = this.m_cacheOption == EventStreamCacheOptions.CacheToDisk;
				for (;;)
				{
					int num2;
					if (this.m_partialFirstRow)
					{
						num2 = this.m_firstRowType;
						this.m_partialFirstRow = false;
					}
					else
					{
						num2 = dataReader3.GetInt32(0);
					}
					long bytes = dataReader3.GetBytes(1, 0L, null, 0, 0);
					byte[] array = new byte[bytes];
					dataReader3.GetBytes(1, 0L, array, 0, (int)bytes);
					if (num2 == 2 && !XEventFileHeader.IsVersionCompatible(array))
					{
						break;
					}
					if (num2 == 1)
					{
						if (!this.m_metadata.DeserializeMetadataBuffer(array))
						{
							goto Block_9;
						}
						this.m_metadataReadyEvent.Set();
					}
					if (num2 == 0)
					{
						BufferLocator bufferLocator = new BufferLocator();
						IAsyncResult asyncResult = null;
						if (flag2)
						{
							FileStream fileStream = this.m_dataStore[(int)this.m_fileStoreIndex];
							FileStream fileStream2 = fileStream;
							lock (fileStream2)
							{
								fileStream.Position = (long)num;
								asyncResult = fileStream.BeginWrite(array, 0, (int)bytes, null, null);
							}
							bufferLocator.m_fileId = (ushort)this.m_fileStoreIndex;
							BufferLocatorHelper.SetBufferOffset(bufferLocator, num);
						}
						else
						{
							bufferLocator.m_bufferNum = (ulong)((uint)num);
							num += 1UL;
							if ((num & 127UL) == 127UL)
							{
								this.RemoveExpiredBufferDescriptors();
							}
						}
						byte[] array2 = this.DecodeBuffer(array);
						XEventCachedBuffer xeventCachedBuffer = new XEventCachedBuffer(bufferLocator, array2, this);
						xeventCachedBuffer.BufferSize = (int)bytes;
						try
						{
							this.m_bufferLock.WaitOne();
							this.m_buffers[bufferLocator] = xeventCachedBuffer;
							if (flag)
							{
								this.m_firstBuffer = xeventCachedBuffer;
								this.m_lastBuffer = xeventCachedBuffer;
								flag = false;
							}
							else
							{
								this.m_lastBuffer.Next = xeventCachedBuffer;
								this.m_lastBuffer = xeventCachedBuffer;
							}
							this.m_bufWait.Set();
						}
						finally
						{
							this.m_bufferLock.ReleaseMutex();
						}
						if (flag2)
						{
							this.m_dataStore[(int)this.m_fileStoreIndex].EndWrite(asyncResult);
							num += (ulong)bytes;
							num = (num + 512UL - 1UL) / 512UL * 512UL;
							if (num > 281474976710655UL)
							{
								this.CreateNextStoreFile();
								num = 0UL;
							}
						}
					}
					if (!dataReader3.Read() || this.m_stopped)
					{
						goto IL_024A;
					}
				}
				throw new EventStreamVersionException(string.Format(Resources.GetString("IncompatibleStreamVersion"), Array.Empty<object>()));
				Block_9:
				throw new EventStreamException(string.Format(Resources.GetString("MetadataDeserializeStreamExceptionString"), Array.Empty<object>()));
				IL_024A:;
			}
			catch (Exception ex)
			{
				if (!this.m_stopped)
				{
					this.m_lastAsyncException = ex;
				}
			}
			finally
			{
				try
				{
					this.m_sqlCommand.Cancel();
					dataReader3.Close();
				}
				catch (Exception ex2)
				{
					if (!this.m_stopped && this.m_lastAsyncException == null)
					{
						this.m_lastAsyncException = ex2;
					}
				}
				this.m_stopped = true;
				this.m_metadataReadyEvent.Set();
				this.m_bufWait.Set();
				try
				{
					if (this.m_IsSqlDataSource)
					{
						this.m_sqlConnection.Close();
					}
					else
					{
						this.m_xmlInputReader.Close();
					}
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0001BF9C File Offset: 0x0001BF9C
		internal object ExecuteInternal(Expression expression)
		{
			QueryBuilder.CheckExpressionTypeSupported(expression);
			return this;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0001BFB0 File Offset: 0x0001BFB0
		[Conditional("DEBUG")]
		public static void SaveMetadata(byte[] mdbuf, int cbBuf)
		{
			using (FileStream fileStream = new FileStream(Path.GetTempFileName(), FileMode.Create, FileAccess.ReadWrite, FileShare.Read, 512, FileOptions.None))
			{
				fileStream.Write(mdbuf, 0, cbBuf);
			}
		}

		// Token: 0x04000262 RID: 610
		private static string sm_queryString = string.Concat(new string[]
		{
			"SELECT ",
			XEventSqlStreamProvider<TEvent>.EventStreamColumns.type.ToString(),
			", ",
			XEventSqlStreamProvider<TEvent>.EventStreamColumns.data.ToString(),
			" FROM sys.fn_MSxe_read_event_stream (@source, @sourceopt)"
		});

		// Token: 0x04000263 RID: 611
		private static string sm_streamAppName = "XEventSqlStreamProvider Event Session: ";

		// Token: 0x04000264 RID: 612
		private static string sm_fileStreamAppName = "XEventSqlStreamingFileProvider";

		// Token: 0x04000265 RID: 613
		private const string sm_defaultAppName = ".NET SqlClient Data Provider";

		// Token: 0x04000266 RID: 614
		private QueryableXEventData m_queryable;

		// Token: 0x04000267 RID: 615
		private SqlDataReader m_reader;

		// Token: 0x04000268 RID: 616
		private SqlConnection m_sqlConnection;

		// Token: 0x04000269 RID: 617
		private SqlCommand m_sqlCommand;

		// Token: 0x0400026A RID: 618
		private XmlReader m_xmlInputReader;

		// Token: 0x0400026B RID: 619
		private XEASXmlReader m_xeasReader;

		// Token: 0x0400026C RID: 620
		private bool m_IsSqlDataSource = true;

		// Token: 0x0400026D RID: 621
		private Thread m_readerThread;

		// Token: 0x0400026E RID: 622
		private ManualResetEvent m_metadataReadyEvent;

		// Token: 0x0400026F RID: 623
		private ManualResetEvent m_bufWait;

		// Token: 0x04000270 RID: 624
		private int m_waitersCount;

		// Token: 0x04000271 RID: 625
		private volatile bool m_stopped;

		// Token: 0x04000272 RID: 626
		private EventStreamSourceOptions m_streamSource;

		// Token: 0x04000273 RID: 627
		private EventStreamCacheOptions m_cacheOption;

		// Token: 0x04000274 RID: 628
		private FileStream[] m_dataStore;

		// Token: 0x04000275 RID: 629
		private short m_fileStoreIndex = -1;

		// Token: 0x04000276 RID: 630
		private Exception m_lastAsyncException;

		// Token: 0x04000277 RID: 631
		private bool m_partialFirstRow;

		// Token: 0x04000278 RID: 632
		private int m_firstRowType = -1;

		// Token: 0x04000279 RID: 633
		private XEventInteropMetadataManager m_metadata;

		// Token: 0x0400027A RID: 634
		private Dictionary<BufferLocator, XEventCachedBuffer> m_buffers;

		// Token: 0x0400027B RID: 635
		private XEventCachedBuffer m_firstBuffer;

		// Token: 0x0400027C RID: 636
		private volatile XEventCachedBuffer m_lastBuffer;

		// Token: 0x0400027D RID: 637
		private Mutex m_bufferLock;

		// Token: 0x020000C6 RID: 198
		internal enum EventStreamColumns
		{
			// Token: 0x04000286 RID: 646
			type,
			// Token: 0x04000287 RID: 647
			data
		}
	}
}
