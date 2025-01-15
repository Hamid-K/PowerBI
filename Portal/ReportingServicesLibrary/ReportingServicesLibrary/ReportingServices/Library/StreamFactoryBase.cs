using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Exceptions;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000242 RID: 578
	internal abstract class StreamFactoryBase : IDisposable
	{
		// Token: 0x06001515 RID: 5397 RVA: 0x00053DA8 File Offset: 0x00051FA8
		protected StreamFactoryBase()
		{
			this.m_factoryName = "StreamFactory:" + Guid.NewGuid().ToString();
			if (StreamFactoryBase.m_tracer.TraceVerbose)
			{
				StreamFactoryBase.m_tracer.Trace(TraceLevel.Verbose, "Constructed Stream Factory '{0}' of type '{1}'", new object[]
				{
					this.m_factoryName,
					base.GetType().FullName
				});
			}
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x00053E24 File Offset: 0x00052024
		public Stream CreateStream(bool primaryOutputStream, bool willSeek, bool needCacheableStream)
		{
			object sync = this.m_sync;
			Stream stream;
			lock (sync)
			{
				if (this.m_isDisposed)
				{
					throw new ObjectDisposedException("StreamFactory");
				}
				if (StreamFactoryBase.m_tracer.TraceVerbose)
				{
					StreamFactoryBase.m_tracer.Trace(TraceLevel.Verbose, "{0} constructing stream (primary={1}, seek={2}, cache={3})", new object[] { this.m_factoryName, primaryOutputStream, willSeek, needCacheableStream });
				}
				stream = this.InternalCreateStream(primaryOutputStream, willSeek, needCacheableStream);
			}
			return stream;
		}

		// Token: 0x06001517 RID: 5399
		protected abstract Stream InternalCreateStream(bool primaryOutputStream, bool willSeek, bool needCacheableStream);

		// Token: 0x06001518 RID: 5400 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public virtual void SetResult(RSStream rsStream)
		{
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x00053EC4 File Offset: 0x000520C4
		public void Dispose()
		{
			try
			{
				if (StreamFactoryBase.m_tracer.TraceVerbose)
				{
					StreamFactoryBase.m_tracer.Trace(TraceLevel.Verbose, "Disposing {0}", new object[] { this.m_factoryName });
				}
				this.CloseRegisteredStreams();
			}
			finally
			{
				this.m_isDisposed = true;
			}
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x00053F1C File Offset: 0x0005211C
		public static void CloseAllStreams(IEnumerable<Stream> streams)
		{
			if (streams == null)
			{
				return;
			}
			Exception ex = null;
			foreach (Stream stream in streams)
			{
				try
				{
					stream.Close();
				}
				catch (Exception ex2)
				{
					if (RSTrace.BufferedResponseTracer.TraceError)
					{
						RSTrace.BufferedResponseTracer.Trace(TraceLevel.Error, "Exception thrown when closing result buffered stream: {0}", new object[] { ex2 });
					}
					ex = ex2;
				}
			}
			if (ex == null)
			{
				return;
			}
			if (ExceptionUtils.ContainsException(ex, (Exception e) => e is RSException || e is TimeoutException || e is SocketException))
			{
				throw new ClosingRegisteredStreamException(ex);
			}
			throw ex;
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00053FD8 File Offset: 0x000521D8
		protected void RegisterStreamForClosing(Stream stream)
		{
			object sync = this.m_sync;
			lock (sync)
			{
				if (this.m_streamsForClosing == null)
				{
					this.m_streamsForClosing = new List<Stream>(1);
				}
				this.m_streamsForClosing.Add(stream);
			}
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00054034 File Offset: 0x00052234
		public void UnregisterStream(Stream stream)
		{
			object sync = this.m_sync;
			lock (sync)
			{
				if (this.m_streamsForClosing != null)
				{
					bool flag2 = this.m_streamsForClosing.Remove(stream);
					RSTrace.CatalogTrace.Assert(flag2, "stream removed");
				}
			}
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00054094 File Offset: 0x00052294
		private void CloseRegisteredStreams()
		{
			object sync = this.m_sync;
			lock (sync)
			{
				if (this.m_streamsForClosing != null)
				{
					StreamFactoryBase.CloseAllStreams(this.m_streamsForClosing);
				}
			}
		}

		// Token: 0x040007B0 RID: 1968
		private bool m_isDisposed;

		// Token: 0x040007B1 RID: 1969
		private List<Stream> m_streamsForClosing;

		// Token: 0x040007B2 RID: 1970
		private readonly object m_sync = new object();

		// Token: 0x040007B3 RID: 1971
		private readonly string m_factoryName;

		// Token: 0x040007B4 RID: 1972
		private static readonly RSTrace m_tracer = RSTrace.CatalogTrace;
	}
}
