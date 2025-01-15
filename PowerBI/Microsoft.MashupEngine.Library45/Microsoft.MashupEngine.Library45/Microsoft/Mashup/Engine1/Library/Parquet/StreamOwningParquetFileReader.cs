using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Parquet;
using ParquetSharp;
using ParquetSharp.IO;

namespace Microsoft.Mashup.Engine1.Library.Parquet
{
	// Token: 0x02001F79 RID: 8057
	internal class StreamOwningParquetFileReader : IDisposable
	{
		// Token: 0x06010E72 RID: 69234 RVA: 0x003A3FE7 File Offset: 0x003A21E7
		public StreamOwningParquetFileReader(Stream stream, RandomAccessFile randomAccessFile, ParquetFileReader parquetFileReader)
		{
			this.stream = stream;
			this.randomAccessFile = randomAccessFile;
			this.parquetFileReader = parquetFileReader;
		}

		// Token: 0x06010E73 RID: 69235 RVA: 0x003A4004 File Offset: 0x003A2204
		public StreamOwningParquetFileReader(StreamOwningParquetFileReader other)
		{
			this.Swap(other);
		}

		// Token: 0x06010E74 RID: 69236 RVA: 0x00002130 File Offset: 0x00000330
		public StreamOwningParquetFileReader()
		{
		}

		// Token: 0x17002CC5 RID: 11461
		// (get) Token: 0x06010E75 RID: 69237 RVA: 0x003A4013 File Offset: 0x003A2213
		public ParquetFileReader ParquetFileReader
		{
			get
			{
				return this.parquetFileReader;
			}
		}

		// Token: 0x06010E76 RID: 69238 RVA: 0x003A401C File Offset: 0x003A221C
		public static StreamOwningParquetFileReader Open(BinaryValue binaryValue)
		{
			Stream stream = null;
			RandomAccessFile randomAccessFile = null;
			ParquetFileReader parquetFileReader = null;
			StreamOwningParquetFileReader streamOwningParquetFileReader2;
			try
			{
				stream = binaryValue.Open(true);
				if (!stream.CanSeek)
				{
					throw ValueException.NewParameterError<Message0>(Resources.SeekableStreamRequired, binaryValue);
				}
				randomAccessFile = new ManagedRandomAccessFile(stream);
				using (ReaderProperties defaultReaderProperties = ReaderProperties.GetDefaultReaderProperties())
				{
					defaultReaderProperties.EnableBufferedStream();
					parquetFileReader = new ParquetFileReader(randomAccessFile, defaultReaderProperties);
				}
				StreamOwningParquetFileReader streamOwningParquetFileReader = new StreamOwningParquetFileReader(stream, randomAccessFile, parquetFileReader);
				stream = null;
				randomAccessFile = null;
				parquetFileReader = null;
				streamOwningParquetFileReader2 = streamOwningParquetFileReader;
			}
			catch (ParquetException ex)
			{
				throw ParquetExceptionHandler.GetParquetValueException(ex);
			}
			finally
			{
				if (parquetFileReader != null)
				{
					parquetFileReader.Dispose();
				}
				if (randomAccessFile != null)
				{
					randomAccessFile.Dispose();
				}
				if (stream != null)
				{
					stream.Dispose();
				}
			}
			return streamOwningParquetFileReader2;
		}

		// Token: 0x06010E77 RID: 69239 RVA: 0x003A40D0 File Offset: 0x003A22D0
		public void Swap(StreamOwningParquetFileReader other)
		{
			Stream stream = this.stream;
			RandomAccessFile randomAccessFile = this.randomAccessFile;
			ParquetFileReader parquetFileReader = this.parquetFileReader;
			ILifetimeService lifetimeService = this.lifetimeService;
			this.stream = other.stream;
			this.randomAccessFile = other.randomAccessFile;
			this.parquetFileReader = other.parquetFileReader;
			this.lifetimeService = other.lifetimeService;
			if (lifetimeService == null && other.lifetimeService != null)
			{
				other.lifetimeService.Unregister(other);
				other.lifetimeService.Register(this);
			}
			if (lifetimeService != null && other.lifetimeService == null)
			{
				lifetimeService.Unregister(this);
				lifetimeService.Register(other);
			}
			other.stream = stream;
			other.randomAccessFile = randomAccessFile;
			other.parquetFileReader = parquetFileReader;
			other.lifetimeService = lifetimeService;
		}

		// Token: 0x06010E78 RID: 69240 RVA: 0x003A4181 File Offset: 0x003A2381
		public void RegisterForCleanup(IEngineHost engineHost)
		{
			if (this.lifetimeService == null && this.parquetFileReader != null)
			{
				this.lifetimeService = engineHost.QueryService<ILifetimeService>();
				ILifetimeService lifetimeService = this.lifetimeService;
				if (lifetimeService == null)
				{
					return;
				}
				lifetimeService.Register(this);
			}
		}

		// Token: 0x06010E79 RID: 69241 RVA: 0x003A41B0 File Offset: 0x003A23B0
		public void Dispose()
		{
			ParquetFileReader parquetFileReader = this.parquetFileReader;
			if (parquetFileReader != null)
			{
				parquetFileReader.Dispose();
			}
			RandomAccessFile randomAccessFile = this.randomAccessFile;
			if (randomAccessFile != null)
			{
				randomAccessFile.Dispose();
			}
			Stream stream = this.stream;
			if (stream != null)
			{
				stream.Dispose();
			}
			ILifetimeService lifetimeService = this.lifetimeService;
			if (lifetimeService != null)
			{
				lifetimeService.Unregister(this);
			}
			this.parquetFileReader = null;
			this.randomAccessFile = null;
			this.stream = null;
			this.lifetimeService = null;
		}

		// Token: 0x040065C9 RID: 26057
		private Stream stream;

		// Token: 0x040065CA RID: 26058
		private RandomAccessFile randomAccessFile;

		// Token: 0x040065CB RID: 26059
		private ParquetFileReader parquetFileReader;

		// Token: 0x040065CC RID: 26060
		private ILifetimeService lifetimeService;
	}
}
