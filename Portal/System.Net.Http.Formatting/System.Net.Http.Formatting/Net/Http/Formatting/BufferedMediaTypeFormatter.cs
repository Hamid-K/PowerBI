using System;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Internal;
using System.Net.Http.Properties;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200003D RID: 61
	public abstract class BufferedMediaTypeFormatter : MediaTypeFormatter
	{
		// Token: 0x06000241 RID: 577 RVA: 0x00007E7F File Offset: 0x0000607F
		protected BufferedMediaTypeFormatter()
		{
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00007E92 File Offset: 0x00006092
		protected BufferedMediaTypeFormatter(BufferedMediaTypeFormatter formatter)
			: base(formatter)
		{
			this.BufferSize = formatter.BufferSize;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00007EB2 File Offset: 0x000060B2
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00007EBA File Offset: 0x000060BA
		public int BufferSize
		{
			get
			{
				return this._bufferSizeInBytes;
			}
			set
			{
				if (value < 0)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 0);
				}
				this._bufferSizeInBytes = value;
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00007EDE File Offset: 0x000060DE
		public virtual void WriteToStream(Type type, object value, Stream writeStream, HttpContent content, CancellationToken cancellationToken)
		{
			this.WriteToStream(type, value, writeStream, content);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007EEB File Offset: 0x000060EB
		public virtual void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
		{
			throw Error.NotSupported(Resources.MediaTypeFormatterCannotWriteSync, new object[] { base.GetType().Name });
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00007F0B File Offset: 0x0000610B
		public virtual object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return this.ReadFromStream(type, readStream, content, formatterLogger);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00007F18 File Offset: 0x00006118
		public virtual object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			throw Error.NotSupported(Resources.MediaTypeFormatterCannotReadSync, new object[] { base.GetType().Name });
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00007F38 File Offset: 0x00006138
		public sealed override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
		{
			return this.WriteToStreamAsync(type, value, writeStream, content, transportContext, CancellationToken.None);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00007F4C File Offset: 0x0000614C
		public sealed override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (writeStream == null)
			{
				throw Error.ArgumentNull("writeStream");
			}
			Task task;
			try
			{
				this.WriteToStreamSync(type, value, writeStream, content, cancellationToken);
				task = TaskHelpers.Completed();
			}
			catch (Exception ex)
			{
				task = TaskHelpers.FromError(ex);
			}
			return task;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00007FAC File Offset: 0x000061AC
		private void WriteToStreamSync(Type type, object value, Stream writeStream, HttpContent content, CancellationToken cancellationToken)
		{
			using (Stream bufferStream = BufferedMediaTypeFormatter.GetBufferStream(writeStream, this._bufferSizeInBytes))
			{
				this.WriteToStream(type, value, bufferStream, content, cancellationToken);
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00007FF0 File Offset: 0x000061F0
		public sealed override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			return this.ReadFromStreamAsync(type, readStream, content, formatterLogger, CancellationToken.None);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00008004 File Offset: 0x00006204
		public sealed override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (readStream == null)
			{
				throw Error.ArgumentNull("readStream");
			}
			Task<object> task;
			try
			{
				task = Task.FromResult<object>(this.ReadFromStreamSync(type, readStream, content, formatterLogger, cancellationToken));
			}
			catch (Exception ex)
			{
				task = TaskHelpers.FromError<object>(ex);
			}
			return task;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00008064 File Offset: 0x00006264
		private object ReadFromStreamSync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			HttpContentHeaders httpContentHeaders = ((content == null) ? null : content.Headers);
			if (httpContentHeaders != null)
			{
				long? contentLength = httpContentHeaders.ContentLength;
				long num = 0L;
				if ((contentLength.GetValueOrDefault() == num) & (contentLength != null))
				{
					return MediaTypeFormatter.GetDefaultValueForType(type);
				}
			}
			object obj;
			using (Stream bufferStream = BufferedMediaTypeFormatter.GetBufferStream(readStream, this._bufferSizeInBytes))
			{
				obj = this.ReadFromStream(type, bufferStream, content, formatterLogger, cancellationToken);
			}
			return obj;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x000080E4 File Offset: 0x000062E4
		private static Stream GetBufferStream(Stream innerStream, int bufferSize)
		{
			return new BufferedStream(new NonClosingDelegatingStream(innerStream), bufferSize);
		}

		// Token: 0x040000A7 RID: 167
		private const int MinBufferSize = 0;

		// Token: 0x040000A8 RID: 168
		private const int DefaultBufferSize = 16384;

		// Token: 0x040000A9 RID: 169
		private int _bufferSizeInBytes = 16384;
	}
}
