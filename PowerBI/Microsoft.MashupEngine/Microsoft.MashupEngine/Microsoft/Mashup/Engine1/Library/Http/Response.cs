using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A91 RID: 2705
	internal abstract class Response : IDisposable
	{
		// Token: 0x06004BBE RID: 19390 RVA: 0x000FA756 File Offset: 0x000F8956
		protected Response(Action<Exception, long> errorLogger)
		{
			this.errorLogger = errorLogger;
			this.innerException = null;
		}

		// Token: 0x06004BBF RID: 19391 RVA: 0x000FA76C File Offset: 0x000F896C
		protected Response(Action<Exception, long> errorLogger, WebException innerException)
		{
			this.errorLogger = errorLogger;
			this.innerException = innerException;
		}

		// Token: 0x06004BC0 RID: 19392 RVA: 0x000FA784 File Offset: 0x000F8984
		public static Stream Serialize(Response response)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(response.ContentLength);
			binaryWriter.WriteNullableString(response.ContentType);
			string[] allKeys = response.Headers.AllKeys;
			binaryWriter.Write(allKeys.Length);
			for (int i = 0; i < allKeys.Length; i++)
			{
				if (response.Headers[allKeys[i]] != null)
				{
					binaryWriter.Write(allKeys[i]);
					binaryWriter.Write(response.Headers[allKeys[i]]);
				}
			}
			binaryWriter.WriteNullableString(response.ResponseUri.AbsoluteUri);
			binaryWriter.Write(response.StatusCode);
			binaryWriter.WriteNullableString(response.StatusDescription);
			binaryWriter.WriteNullableString(response.CharacterSet);
			binaryWriter.WriteNullableString(response.ContentEncoding);
			memoryStream.Position = 0L;
			return memoryStream.Concat(response.GetResponseStream());
		}

		// Token: 0x06004BC1 RID: 19393 RVA: 0x000FA85B File Offset: 0x000F8A5B
		public static Response Deserialize(Stream stream)
		{
			return new Response.DeserializedResponse(stream);
		}

		// Token: 0x170017DD RID: 6109
		// (get) Token: 0x06004BC2 RID: 19394 RVA: 0x000FA863 File Offset: 0x000F8A63
		public WebException InnerException
		{
			get
			{
				return this.innerException;
			}
		}

		// Token: 0x170017DE RID: 6110
		// (get) Token: 0x06004BC3 RID: 19395
		public abstract string CharacterSet { get; }

		// Token: 0x170017DF RID: 6111
		// (get) Token: 0x06004BC4 RID: 19396
		public abstract string ContentEncoding { get; }

		// Token: 0x170017E0 RID: 6112
		// (get) Token: 0x06004BC5 RID: 19397
		public abstract long ContentLength { get; }

		// Token: 0x170017E1 RID: 6113
		// (get) Token: 0x06004BC6 RID: 19398
		public abstract string ContentType { get; }

		// Token: 0x170017E2 RID: 6114
		// (get) Token: 0x06004BC7 RID: 19399
		public abstract WebHeaderCollection Headers { get; }

		// Token: 0x170017E3 RID: 6115
		// (get) Token: 0x06004BC8 RID: 19400
		public abstract Uri ResponseUri { get; }

		// Token: 0x170017E4 RID: 6116
		// (get) Token: 0x06004BC9 RID: 19401
		public abstract int StatusCode { get; }

		// Token: 0x170017E5 RID: 6117
		// (get) Token: 0x06004BCA RID: 19402
		public abstract string StatusDescription { get; }

		// Token: 0x06004BCB RID: 19403
		public abstract void Close();

		// Token: 0x06004BCC RID: 19404 RVA: 0x000FA86B File Offset: 0x000F8A6B
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06004BCD RID: 19405
		public abstract Stream GetResponseStream();

		// Token: 0x06004BCE RID: 19406 RVA: 0x000FA873 File Offset: 0x000F8A73
		public virtual RecordValue GetHeaders()
		{
			return RequestHeaders.GetHeaders(this.Headers);
		}

		// Token: 0x0400282B RID: 10283
		protected readonly Action<Exception, long> errorLogger;

		// Token: 0x0400282C RID: 10284
		protected WebException innerException;

		// Token: 0x02000A92 RID: 2706
		private sealed class RetryingWebResponseStream : ForwardReadOnlyStream
		{
			// Token: 0x06004BCF RID: 19407 RVA: 0x000FA880 File Offset: 0x000F8A80
			public RetryingWebResponseStream(Stream stream, IEngineHost host, Func<Stream> getNewStream)
			{
				this.stream = stream;
				this.host = host;
				this.getNewStream = getNewStream;
				this.hasher = new MurmurHash3_128(0UL);
			}

			// Token: 0x06004BD0 RID: 19408 RVA: 0x000FA8AC File Offset: 0x000F8AAC
			public override int Read(byte[] buffer, int offset, int count)
			{
				int num2;
				try
				{
					int num = this.stream.Read(buffer, offset, count);
					this.position += num;
					if (this.hasher != null)
					{
						this.hasher.AddBytes(buffer, offset, num);
					}
					num2 = num;
				}
				catch (IOException ex)
				{
					if (!this.CanRetry(ex))
					{
						throw;
					}
					num2 = this.Read(buffer, offset, count);
				}
				return num2;
			}

			// Token: 0x06004BD1 RID: 19409 RVA: 0x000FA91C File Offset: 0x000F8B1C
			public override int ReadByte()
			{
				int num2;
				try
				{
					int num = this.stream.ReadByte();
					if (num >= 0)
					{
						this.position++;
						if (this.hasher != null)
						{
							this.hasher.AddByte((byte)num);
						}
					}
					num2 = num;
				}
				catch (IOException ex)
				{
					if (!this.CanRetry(ex))
					{
						throw;
					}
					num2 = this.ReadByte();
				}
				return num2;
			}

			// Token: 0x06004BD2 RID: 19410 RVA: 0x000FA988 File Offset: 0x000F8B88
			public override void Close()
			{
				this.stream.Close();
			}

			// Token: 0x06004BD3 RID: 19411 RVA: 0x000FA995 File Offset: 0x000F8B95
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.stream != null)
				{
					this.stream.Dispose();
					this.stream = null;
				}
			}

			// Token: 0x06004BD4 RID: 19412 RVA: 0x000FA9B4 File Offset: 0x000F8BB4
			private bool CanRetry(Exception exception)
			{
				if (this.isRetry || this.hasher == null || this.position == 0)
				{
					this.TraceRetryCheck(exception, false);
					return false;
				}
				this.isRetry = true;
				bool flag;
				try
				{
					int num = this.position;
					ulong num2;
					ulong num3;
					this.hasher.Finish(out num2, out num3);
					this.stream.Dispose();
					this.stream = this.getNewStream();
					this.position = 0;
					this.hasher = new MurmurHash3_128(0UL);
					byte[] array = new byte[4096];
					int num4;
					do
					{
						num4 = this.Read(array, 0, Math.Min(array.Length, num));
						num -= num4;
					}
					while (num4 > 0 && num > 0);
					if (num != 0)
					{
						this.TraceRetryResult(false, "Different content length", null);
						flag = false;
					}
					else
					{
						ulong num5;
						ulong num6;
						this.hasher.Finish(out num5, out num6);
						if (num2 != num5 || num3 != num6)
						{
							this.TraceRetryResult(false, "Different content hash", null);
							flag = false;
						}
						else
						{
							this.TraceRetryResult(true, null, null);
							this.hasher = null;
							flag = true;
						}
					}
				}
				catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
				{
					this.TraceRetryResult(false, "Exception", ex);
					flag = false;
				}
				return flag;
			}

			// Token: 0x06004BD5 RID: 19413 RVA: 0x000FAAF4 File Offset: 0x000F8CF4
			private void TraceRetryCheck(Exception exception, bool retrying)
			{
				using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/Web/Response/RetryingWebResponseStream/TraceRetryCheck", TraceEventType.Information, null))
				{
					hostTrace.Add(exception, TraceEventType.Warning, true);
					hostTrace.Add("retrying", retrying, false);
					if (!retrying)
					{
						hostTrace.Add("isRetry", this.isRetry, false);
						hostTrace.Add("hasherNull", this.hasher == null, false);
						hostTrace.Add("position", this.position, false);
					}
				}
			}

			// Token: 0x06004BD6 RID: 19414 RVA: 0x000FAB98 File Offset: 0x000F8D98
			private void TraceRetryResult(bool success, string reason = null, Exception exception = null)
			{
				using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/Web/Response/RetryingWebResponseStream/TraceRetryResult", TraceEventType.Information, null))
				{
					hostTrace.Add("success", success, false);
					if (reason != null)
					{
						hostTrace.Add("reason", reason, false);
					}
					if (exception != null)
					{
						hostTrace.Add(exception, TraceEventType.Warning, true);
					}
				}
			}

			// Token: 0x0400282D RID: 10285
			private readonly IEngineHost host;

			// Token: 0x0400282E RID: 10286
			private readonly Func<Stream> getNewStream;

			// Token: 0x0400282F RID: 10287
			private Stream stream;

			// Token: 0x04002830 RID: 10288
			private int position;

			// Token: 0x04002831 RID: 10289
			private MurmurHash3_128 hasher;

			// Token: 0x04002832 RID: 10290
			private bool isRetry;
		}

		// Token: 0x02000A93 RID: 2707
		protected sealed class WebResponseStream : ForwardReadOnlyStream
		{
			// Token: 0x06004BD7 RID: 19415 RVA: 0x000FAC04 File Offset: 0x000F8E04
			public WebResponseStream(WebRequest request, Stream stream, Action<Exception, long> errorLogger, IEngineHost host, Func<global::System.Tuple<WebRequest, Stream>> getNewRequestAndStream = null)
			{
				this.request = request;
				if (getNewRequestAndStream == null)
				{
					this.stream = new ErrorLoggingReadStream(stream, errorLogger);
					return;
				}
				this.getNewRequestAndStream = getNewRequestAndStream;
				this.stream = new ErrorLoggingReadStream(new Response.RetryingWebResponseStream(stream, host, new Func<Stream>(this.GetNewStream)), errorLogger);
			}

			// Token: 0x06004BD8 RID: 19416 RVA: 0x000FAC58 File Offset: 0x000F8E58
			public override int Read(byte[] buffer, int offset, int count)
			{
				return this.stream.Read(buffer, offset, count);
			}

			// Token: 0x06004BD9 RID: 19417 RVA: 0x000FAC68 File Offset: 0x000F8E68
			public override int ReadByte()
			{
				return this.stream.ReadByte();
			}

			// Token: 0x06004BDA RID: 19418 RVA: 0x000FAC75 File Offset: 0x000F8E75
			public override void Close()
			{
				this.stream.Close();
			}

			// Token: 0x06004BDB RID: 19419 RVA: 0x000FAC82 File Offset: 0x000F8E82
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.request.Abort();
					this.stream.Dispose();
				}
				base.Dispose(disposing);
			}

			// Token: 0x06004BDC RID: 19420 RVA: 0x000FACA4 File Offset: 0x000F8EA4
			private Stream GetNewStream()
			{
				global::System.Tuple<WebRequest, Stream> tuple = this.getNewRequestAndStream();
				this.request.Abort();
				this.request = tuple.Item1;
				return tuple.Item2;
			}

			// Token: 0x04002833 RID: 10291
			private readonly Func<global::System.Tuple<WebRequest, Stream>> getNewRequestAndStream;

			// Token: 0x04002834 RID: 10292
			private readonly ErrorLoggingReadStream stream;

			// Token: 0x04002835 RID: 10293
			private WebRequest request;
		}

		// Token: 0x02000A94 RID: 2708
		private class DeserializedResponse : Response
		{
			// Token: 0x06004BDD RID: 19421 RVA: 0x000FACDC File Offset: 0x000F8EDC
			public DeserializedResponse(Stream stream)
				: base(null)
			{
				BinaryReader binaryReader = new BinaryReader(stream);
				this.contentLength = binaryReader.ReadInt64();
				this.contentType = binaryReader.ReadNullableString();
				this.headers = new WebHeaderCollection();
				int num = binaryReader.ReadInt32();
				for (int i = 0; i < num; i++)
				{
					string text = binaryReader.ReadString();
					string text2 = binaryReader.ReadString();
					this.headers.Add(text, text2);
				}
				this.responseUri = new Uri(binaryReader.ReadNullableString());
				this.statusCode = binaryReader.ReadInt32();
				this.statusDescription = binaryReader.ReadNullableString();
				this.characterSet = binaryReader.ReadNullableString();
				this.contentEncoding = binaryReader.ReadNullableString();
				this.stream = stream;
				if (this.stream.CanSeek)
				{
					this.stream = new OffsetStream(this.stream, this.stream.Position);
				}
			}

			// Token: 0x170017E6 RID: 6118
			// (get) Token: 0x06004BDE RID: 19422 RVA: 0x000FADBB File Offset: 0x000F8FBB
			public override string CharacterSet
			{
				get
				{
					return this.characterSet;
				}
			}

			// Token: 0x170017E7 RID: 6119
			// (get) Token: 0x06004BDF RID: 19423 RVA: 0x000FADC3 File Offset: 0x000F8FC3
			public override string ContentEncoding
			{
				get
				{
					return this.contentEncoding;
				}
			}

			// Token: 0x170017E8 RID: 6120
			// (get) Token: 0x06004BE0 RID: 19424 RVA: 0x000FADCB File Offset: 0x000F8FCB
			public override long ContentLength
			{
				get
				{
					return this.contentLength;
				}
			}

			// Token: 0x170017E9 RID: 6121
			// (get) Token: 0x06004BE1 RID: 19425 RVA: 0x000FADD3 File Offset: 0x000F8FD3
			public override string ContentType
			{
				get
				{
					return this.contentType;
				}
			}

			// Token: 0x170017EA RID: 6122
			// (get) Token: 0x06004BE2 RID: 19426 RVA: 0x000FADDB File Offset: 0x000F8FDB
			public override WebHeaderCollection Headers
			{
				get
				{
					return this.headers;
				}
			}

			// Token: 0x170017EB RID: 6123
			// (get) Token: 0x06004BE3 RID: 19427 RVA: 0x000FADE3 File Offset: 0x000F8FE3
			public override Uri ResponseUri
			{
				get
				{
					return this.responseUri;
				}
			}

			// Token: 0x170017EC RID: 6124
			// (get) Token: 0x06004BE4 RID: 19428 RVA: 0x000FADEB File Offset: 0x000F8FEB
			public override int StatusCode
			{
				get
				{
					return this.statusCode;
				}
			}

			// Token: 0x170017ED RID: 6125
			// (get) Token: 0x06004BE5 RID: 19429 RVA: 0x000FADF3 File Offset: 0x000F8FF3
			public override string StatusDescription
			{
				get
				{
					return this.statusDescription;
				}
			}

			// Token: 0x06004BE6 RID: 19430 RVA: 0x000FADFB File Offset: 0x000F8FFB
			public override void Close()
			{
				this.stream.Close();
			}

			// Token: 0x06004BE7 RID: 19431 RVA: 0x000FAE08 File Offset: 0x000F9008
			public override Stream GetResponseStream()
			{
				return this.stream;
			}

			// Token: 0x04002836 RID: 10294
			private readonly long contentLength;

			// Token: 0x04002837 RID: 10295
			private readonly string contentType;

			// Token: 0x04002838 RID: 10296
			private readonly int statusCode;

			// Token: 0x04002839 RID: 10297
			private readonly string statusDescription;

			// Token: 0x0400283A RID: 10298
			private readonly string characterSet;

			// Token: 0x0400283B RID: 10299
			private readonly string contentEncoding;

			// Token: 0x0400283C RID: 10300
			private readonly WebHeaderCollection headers;

			// Token: 0x0400283D RID: 10301
			private readonly Stream stream;

			// Token: 0x0400283E RID: 10302
			private readonly Uri responseUri;
		}
	}
}
