using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline
{
	// Token: 0x02000008 RID: 8
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class DataStreamWriter : IDisposable
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002270 File Offset: 0x00000470
		protected DataStreamWriter(Stream outputStream)
			: this(new List<Stream> { outputStream })
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002284 File Offset: 0x00000484
		protected DataStreamWriter(List<Stream> outputStreams)
		{
			if (outputStreams == null)
			{
				throw new ArgumentNullException("outputStreams");
			}
			if (outputStreams.Count == 0)
			{
				throw new ArgumentException("Empty list", "outputStreams");
			}
			if (outputStreams.Any((Stream outputStream) => outputStream == null))
			{
				throw new ArgumentException("A list with a null item", "outputStreams");
			}
			this.m_outputStreamWriters = outputStreams.SelectWithSafeDispose((Stream outputStream) => new StreamWriter(outputStream, DataStreamWriter.DataStreamWriterUTF8Encoding, 4096, true));
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000231F File Offset: 0x0000051F
		public long RowsProcessed
		{
			get
			{
				return this.m_rowsProcessed;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002327 File Offset: 0x00000527
		public long PagesProcessed
		{
			get
			{
				return this.m_pagesProcessed;
			}
		}

		// Token: 0x06000013 RID: 19
		internal abstract Task WritePageData(List<IColumn> columns, int rowsCount, StreamWriter outputStreamWriter);

		// Token: 0x06000014 RID: 20 RVA: 0x0000232F File Offset: 0x0000052F
		internal virtual Task WriteHeader(DataTable schemaTable, StreamWriter outputStreamWriter)
		{
			return Task.FromResult<int>(0);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002337 File Offset: 0x00000537
		internal virtual Task WriteFooter(DataTable schemaTable, StreamWriter outputStreamWriter)
		{
			return Task.FromResult<int>(0);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002340 File Offset: 0x00000540
		internal virtual async Task WriteTableAsync(IRawDataPageReader pageReader)
		{
			try
			{
				List<Tuple<StreamWriter, DataTable>> streamWritersWithSchemaTable = this.m_outputStreamWriters.JoinByOrder(pageReader.SchemaTables).ToList<Tuple<StreamWriter, DataTable>>();
				foreach (Tuple<StreamWriter, DataTable> tuple in streamWritersWithSchemaTable)
				{
					await this.WriteHeader(tuple.Item2, tuple.Item1);
				}
				List<Tuple<StreamWriter, DataTable>>.Enumerator enumerator = default(List<Tuple<StreamWriter, DataTable>>.Enumerator);
				using (IPage page = pageReader.CreatePage())
				{
					int rowCount = -1;
					while (rowCount != 0)
					{
						pageReader.Read(page);
						rowCount = page.RowCount;
						if (rowCount > 0)
						{
							foreach (Tuple<StreamWriter, DataTable> tuple2 in streamWritersWithSchemaTable)
							{
								await this.WritePageData(this.GetPageColumns(tuple2.Item2, page), page.RowCount, tuple2.Item1);
							}
							enumerator = default(List<Tuple<StreamWriter, DataTable>>.Enumerator);
							this.m_rowsProcessed += (long)rowCount;
						}
						this.m_pagesProcessed += 1L;
					}
				}
				IPage page = null;
				foreach (Tuple<StreamWriter, DataTable> tuple3 in streamWritersWithSchemaTable)
				{
					await this.WriteFooter(tuple3.Item2, tuple3.Item1);
				}
				enumerator = default(List<Tuple<StreamWriter, DataTable>>.Enumerator);
				streamWritersWithSchemaTable = null;
			}
			catch (Exception ex)
			{
				TraceSourceBase<PowerBIRawDataTraceSource>.Tracer.TraceError("WriteTableAsync failed with exception: {0}", new object[] { ex });
				List<StreamWriter> outputStreamWriters = this.m_outputStreamWriters;
				Action<StreamWriter> action;
				if ((action = DataStreamWriter.<>O.<0>__CleanupIncompleteOutputStream) == null)
				{
					action = (DataStreamWriter.<>O.<0>__CleanupIncompleteOutputStream = new Action<StreamWriter>(DataStreamWriter.CleanupIncompleteOutputStream));
				}
				outputStreamWriters.ForEach(action);
				throw;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000238C File Offset: 0x0000058C
		protected List<IColumn> GetPageColumns(DataTable schemaTable, IPage page)
		{
			return Enumerable.Range(0, schemaTable.Rows.Count).Select(delegate(int column)
			{
				int num = (int)schemaTable.Rows[column]["ColumnOrdinal"];
				return page.GetColumn(num - 1);
			}).ToList<IColumn>();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023DC File Offset: 0x000005DC
		protected static object ParseOleDbType(object value)
		{
			if (value is Currency)
			{
				value = ((Currency)value).Value;
			}
			else if (value is Date)
			{
				value = ((Date)value).DateTime;
			}
			else if (value is Time)
			{
				value = ((Time)value).TimeSpan;
			}
			else if (value is Number)
			{
				value = ((Number)value).ToDecimal();
			}
			return value;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002464 File Offset: 0x00000664
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002473 File Offset: 0x00000673
		protected virtual void Dispose(bool disposing)
		{
			if (this.m_disposed)
			{
				return;
			}
			if (disposing && this.m_outputStreamWriters != null)
			{
				this.m_outputStreamWriters.ForEachDispose<StreamWriter>();
				this.m_outputStreamWriters.Clear();
			}
			this.m_disposed = true;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024A8 File Offset: 0x000006A8
		protected static void CleanupIncompleteOutputStream(StreamWriter streamWriter)
		{
			try
			{
				Stream baseStream = streamWriter.BaseStream;
				if (baseStream.CanSeek)
				{
					if (baseStream.Position > 0L)
					{
						baseStream.Position = 0L;
					}
					if (baseStream.Length > 0L)
					{
						baseStream.SetLength(0L);
					}
				}
			}
			catch (Exception ex)
			{
				TraceSourceBase<PowerBIRawDataTraceSource>.Tracer.TraceError("A WriteTable failure requires base stream cleanup. The cleanup failed: {0}", new object[] { ex });
			}
		}

		// Token: 0x04000014 RID: 20
		private const int BufferSize = 4096;

		// Token: 0x04000015 RID: 21
		private static readonly UTF8Encoding DataStreamWriterUTF8Encoding = new UTF8Encoding(false);

		// Token: 0x04000016 RID: 22
		private readonly List<StreamWriter> m_outputStreamWriters;

		// Token: 0x04000017 RID: 23
		private long m_rowsProcessed;

		// Token: 0x04000018 RID: 24
		private long m_pagesProcessed;

		// Token: 0x04000019 RID: 25
		private bool m_disposed;

		// Token: 0x02000024 RID: 36
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400008D RID: 141
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			public static Action<StreamWriter> <0>__CleanupIncompleteOutputStream;
		}
	}
}
