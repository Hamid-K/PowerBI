using System;
using System.Globalization;
using System.IO;
using System.Net;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A6C RID: 2668
	internal abstract class PagedHttpBinaryValue : HttpBinaryValue
	{
		// Token: 0x06004AAB RID: 19115 RVA: 0x000F8098 File Offset: 0x000F6298
		protected PagedHttpBinaryValue(IEngineHost host, IResource resource, TextValue blobUrl, OptionsRecord options, long? rawLength = null, string etag = null, string contentEncoding = null)
		{
			this.syncRoot = new object();
			this.host = host;
			this.resource = resource;
			this.blobUrl = blobUrl;
			this.options = options;
			this.cache = this.host.GetPersistentCache();
			this.concurrentRequests = this.GetPositiveInt32(this.options, "ConcurrentRequests");
			this.pageSize = this.GetPositiveInt32(this.options, "RequestSize");
			this.blockSize = this.GetPositiveInt32(this.options, "BlockSize");
			this.rawLength = rawLength;
			this.etag = etag;
			this.contentEncoding = contentEncoding;
			this.fullBlobRequest = this.CreateRequest(0L, long.MaxValue, false);
			this.previewBlobRequest = new Lazy<Request>(() => this.CreateRequest(0L, 65536L, this.IsNotEmptyBlob()));
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x000F8170 File Offset: 0x000F6370
		public override bool TryGetLength(out long length)
		{
			if (this.decompressedLength == null)
			{
				bool flag;
				long num = this.GetRawLength(out flag);
				if (flag)
				{
					length = 0L;
					return false;
				}
				this.decompressedLength = new long?(num);
			}
			length = this.decompressedLength.Value;
			return true;
		}

		// Token: 0x1700178D RID: 6029
		// (get) Token: 0x06004AAD RID: 19117 RVA: 0x000F81B8 File Offset: 0x000F63B8
		public override long Length
		{
			get
			{
				long length;
				if (!this.TryGetLength(out length))
				{
					length = base.Length;
					this.decompressedLength = new long?(length);
				}
				return length;
			}
		}

		// Token: 0x1700178E RID: 6030
		// (get) Token: 0x06004AAE RID: 19118
		protected abstract PersistentCacheKey CacheKey { get; }

		// Token: 0x1700178F RID: 6031
		// (get) Token: 0x06004AAF RID: 19119 RVA: 0x000F81E3 File Offset: 0x000F63E3
		protected virtual string RangeKey
		{
			get
			{
				return "Range";
			}
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x000F81EC File Offset: 0x000F63EC
		private long GetRawLength(out bool isCompressed)
		{
			if (this.rawLength == null)
			{
				string hash = HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.host, this.resource, null).GetHash();
				this.GetBlobProperties(hash);
			}
			isCompressed = RequestHeaders.IsGzip(this.contentEncoding) || RequestHeaders.IsDeflate(this.contentEncoding);
			return this.rawLength.Value;
		}

		// Token: 0x06004AB1 RID: 19121 RVA: 0x000F824D File Offset: 0x000F644D
		public override Stream Open()
		{
			return this.Open(false);
		}

		// Token: 0x06004AB2 RID: 19122 RVA: 0x000F8258 File Offset: 0x000F6458
		public override Stream Open(bool preferCanSeek)
		{
			string hash = HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.host, this.resource, null).GetHash();
			if (this.rawLength == null || string.IsNullOrEmpty(this.etag) || this.contentEncoding == null)
			{
				this.GetBlobProperties(hash);
			}
			int num = (int)PageHelpers.PageCount(this.rawLength.Value, (long)this.pageSize);
			string text = this.CacheKey.Qualify(hash, this.fullBlobRequest.Uri.AbsoluteUri, this.etag ?? string.Empty, "GET");
			IPagedStorage storage = this.cache.OpenStorage(text, this.pageSize, num);
			Func<int, Stream> func = (int pageIndex) => this.GetPage(storage, this.etag, pageIndex);
			Stream stream;
			if (preferCanSeek)
			{
				stream = new PagedSeekableStream(this.rawLength.Value, this.pageSize, func);
			}
			else
			{
				stream = ParallelReadAheadStream.NewNonBuffering(this.host, this.resource, this.concurrentRequests, func);
			}
			stream = stream.CreateDecompressStream(this.contentEncoding);
			return stream.AfterDispose(new Action(storage.Dispose));
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x000F8394 File Offset: 0x000F6594
		private void GetBlobProperties(string credentialsHash)
		{
			string text = this.CacheKey.Qualify(credentialsHash, this.fullBlobRequest.Uri.AbsoluteUri, "HEAD", "PagedHeaders");
			Stream stream;
			if (!this.cache.TryGetValue(text, out stream))
			{
				Request request = this.CreateRequest(null, null, null);
				request.Method = "HEAD";
				request.UseCache = false;
				request.KeepCompressed = true;
				using (Response response = this.GetResponse(request))
				{
					stream = this.cache.BeginAdd();
					using (Stream stream2 = Response.Serialize(response))
					{
						stream2.CopyTo(stream);
					}
					stream = this.cache.EndAdd(text, stream);
				}
			}
			Response response2 = Response.Deserialize(stream);
			base.SetResponseHeaders(response2.GetHeaders(), response2.StatusCode);
			this.etag = response2.Headers["ETag"];
			this.rawLength = new long?(response2.ContentLength);
			this.contentEncoding = response2.Headers["Content-Encoding"];
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x000F84D0 File Offset: 0x000F66D0
		private Stream GetPage(IPagedStorage storage, string etag, int pageIndex)
		{
			int num = (int)(this.rawLength.Value / (long)this.pageSize);
			if (pageIndex <= num)
			{
				int num2 = ((pageIndex == num) ? ((int)(this.rawLength.Value % (long)this.pageSize)) : this.pageSize);
				if (num2 > 0)
				{
					bool flag;
					Stream stream = storage.OpenPage(pageIndex, out flag);
					if (flag)
					{
						try
						{
							using (Stream streamWithRetry = this.GetStreamWithRetry(etag, (long)pageIndex * (long)this.pageSize, (long)num2))
							{
								streamWithRetry.CopyTo(stream);
								storage.CommitPage(stream);
								stream.Position = 0L;
							}
						}
						catch
						{
							stream.Dispose();
							throw;
						}
					}
					if (num2 != this.pageSize)
					{
						stream = stream.Take((long)num2);
					}
					return stream;
				}
			}
			return null;
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x000F85A4 File Offset: 0x000F67A4
		private Stream GetStreamWithRetry(string etag, long offset, long length = 9223372036854775807L)
		{
			return new RetryStream(this.host, this.resource, PagedHttpBinaryValue.retryPolicy, offset, delegate(long streamOffset)
			{
				Request request = this.CreateRequest(offset + streamOffset, length - streamOffset, true);
				Response response = this.GetResponse(request, PagedHttpBinaryValue.ignoredStatusCodes);
				if (response.StatusCode == 416)
				{
					response.Dispose();
					return new MemoryStream();
				}
				if (response.Headers["ETag"] != etag)
				{
					response.Dispose();
					throw HttpServices.NewDataSourceError<Message1>(this.host, Strings.DataSourceChanged(this.resource.Kind), this.resource, this.blobUrl);
				}
				return response.GetResponseStream();
			}).TranslateErrors(new Func<Exception, Exception>(this.TranslateException));
		}

		// Token: 0x06004AB6 RID: 19126
		protected abstract Request CreateRequest(Value query = null, Value headers = null, Value content = null);

		// Token: 0x06004AB7 RID: 19127 RVA: 0x000F860C File Offset: 0x000F680C
		private Request CreateRequest(long offset, long length, bool useRange = true)
		{
			Value value = Value.Null;
			if (useRange)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "bytes={0}-{1}", offset, offset + length - 1L);
				value = RecordValue.New(Keys.New(this.RangeKey), new Value[] { TextValue.New(text) });
			}
			Request request = this.CreateRequest(null, value, null);
			request.UseCache = false;
			request.KeepCompressed = true;
			return request;
		}

		// Token: 0x06004AB8 RID: 19128 RVA: 0x000F867C File Offset: 0x000F687C
		private bool IsNotEmptyBlob()
		{
			bool flag;
			return this.GetRawLength(out flag) != 0L;
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x000F8698 File Offset: 0x000F6898
		private Exception TranslateException(Exception e)
		{
			if (e is RuntimeException)
			{
				return e;
			}
			Message2 message = Strings.WebRequestFailed(this.resource.Kind, e.Message);
			return HttpServices.NewDataSourceError<Message2>(this.host, message, this.resource, this.blobUrl);
		}

		// Token: 0x06004ABA RID: 19130
		protected abstract Response GetResponse(Request request, int[] ignoredStatusCodes);

		// Token: 0x06004ABB RID: 19131 RVA: 0x000F86DE File Offset: 0x000F68DE
		protected sealed override Response GetResponse(Request request)
		{
			return this.GetResponse(request, null);
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x000F86E8 File Offset: 0x000F68E8
		private static RetryHandlerResult RetryOnStreamError(Exception e)
		{
			while (e != null)
			{
				if (e is WebException || e is IOException)
				{
					return RetryHandlerResult.RetryAfterDefaultDelay;
				}
				e = e.InnerException;
			}
			return RetryHandlerResult.FailWithOriginalException;
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x000F8714 File Offset: 0x000F6914
		private int GetPositiveInt32(OptionsRecord options, string name)
		{
			int @int = options.GetInt32(name);
			if (@int <= 0)
			{
				Message1 message = Strings.InvalidOptionValue(name);
				throw HttpServices.NewDataSourceError<Message1>(this.host, message, this.resource, this.blobUrl);
			}
			return @int;
		}

		// Token: 0x0400278D RID: 10125
		private static readonly int[] ignoredStatusCodes = new int[] { 416 };

		// Token: 0x0400278E RID: 10126
		private static readonly RetryPolicy retryPolicy = new RetryPolicy(3, new Func<Exception, RetryHandlerResult>(PagedHttpBinaryValue.RetryOnStreamError));

		// Token: 0x0400278F RID: 10127
		protected readonly object syncRoot;

		// Token: 0x04002790 RID: 10128
		protected readonly IEngineHost host;

		// Token: 0x04002791 RID: 10129
		protected readonly IPersistentCache cache;

		// Token: 0x04002792 RID: 10130
		protected readonly Request fullBlobRequest;

		// Token: 0x04002793 RID: 10131
		protected readonly Lazy<Request> previewBlobRequest;

		// Token: 0x04002794 RID: 10132
		protected readonly OptionsRecord options;

		// Token: 0x04002795 RID: 10133
		protected readonly IResource resource;

		// Token: 0x04002796 RID: 10134
		protected readonly TextValue blobUrl;

		// Token: 0x04002797 RID: 10135
		protected readonly int concurrentRequests;

		// Token: 0x04002798 RID: 10136
		protected readonly int pageSize;

		// Token: 0x04002799 RID: 10137
		protected readonly int blockSize;

		// Token: 0x0400279A RID: 10138
		protected long? rawLength;

		// Token: 0x0400279B RID: 10139
		protected long? decompressedLength;

		// Token: 0x0400279C RID: 10140
		protected string etag;

		// Token: 0x0400279D RID: 10141
		protected string contentEncoding;

		// Token: 0x02000A6D RID: 2669
		protected class PutBlockBinaryValue : StreamedBinaryValue
		{
			// Token: 0x06004AC0 RID: 19136 RVA: 0x000F878E File Offset: 0x000F698E
			public PutBlockBinaryValue(Stream stream)
			{
				this.stream = stream;
			}

			// Token: 0x06004AC1 RID: 19137 RVA: 0x000F879D File Offset: 0x000F699D
			public override Stream Open()
			{
				this.stream.Position = 0L;
				return this.stream.NonDisposable();
			}

			// Token: 0x0400279E RID: 10142
			private readonly Stream stream;
		}
	}
}
