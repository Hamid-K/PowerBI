using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles.Infrastructure;

namespace Microsoft.Owin.StaticFiles
{
	// Token: 0x02000011 RID: 17
	internal struct StaticFileContext
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002A0C File Offset: 0x00000C0C
		public StaticFileContext(IOwinContext context, StaticFileOptions options, PathString matchUrl)
		{
			this._context = context;
			this._options = options;
			this._matchUrl = matchUrl;
			this._request = context.Request;
			this._response = context.Response;
			this._method = null;
			this._isGet = false;
			this._isHead = false;
			this._subPath = PathString.Empty;
			this._contentType = null;
			this._fileInfo = null;
			this._length = 0L;
			this._lastModified = default(DateTime);
			this._etag = null;
			this._etagQuoted = null;
			this._lastModifiedString = null;
			this._ifMatchState = StaticFileContext.PreconditionState.Unspecified;
			this._ifNoneMatchState = StaticFileContext.PreconditionState.Unspecified;
			this._ifModifiedSinceState = StaticFileContext.PreconditionState.Unspecified;
			this._ifUnmodifiedSinceState = StaticFileContext.PreconditionState.Unspecified;
			this._ranges = null;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002AC0 File Offset: 0x00000CC0
		public bool IsHeadMethod
		{
			get
			{
				return this._isHead;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public bool IsRangeRequest
		{
			get
			{
				return this._ranges != null;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public bool ValidateMethod()
		{
			this._method = this._request.Method;
			this._isGet = Helpers.IsGetMethod(this._method);
			this._isHead = Helpers.IsHeadMethod(this._method);
			return this._isGet || this._isHead;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B24 File Offset: 0x00000D24
		public bool ValidatePath()
		{
			return Helpers.TryMatchPath(this._context, this._matchUrl, false, out this._subPath);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B40 File Offset: 0x00000D40
		public bool LookupContentType()
		{
			if (this._options.ContentTypeProvider.TryGetContentType(this._subPath.Value, out this._contentType))
			{
				return true;
			}
			if (this._options.ServeUnknownFileTypes)
			{
				this._contentType = this._options.DefaultContentType;
				return true;
			}
			return false;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B94 File Offset: 0x00000D94
		public bool LookupFileInfo()
		{
			bool found = this._options.FileSystem.TryGetFileInfo(this._subPath.Value, out this._fileInfo);
			if (found)
			{
				this._length = this._fileInfo.Length;
				DateTime last = this._fileInfo.LastModified;
				this._lastModified = new DateTime(last.Year, last.Month, last.Day, last.Hour, last.Minute, last.Second, last.Kind);
				this._lastModifiedString = this._lastModified.ToString("r", CultureInfo.InvariantCulture);
				long etagHash = this._lastModified.ToFileTimeUtc() ^ this._length;
				this._etag = Convert.ToString(etagHash, 16);
				this._etagQuoted = "\"" + this._etag + "\"";
			}
			return found;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C7A File Offset: 0x00000E7A
		public void ComprehendRequestHeaders()
		{
			this.ComputeIfMatch();
			this.ComputeIfModifiedSince();
			this.ComputeRange();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C90 File Offset: 0x00000E90
		private void ComputeIfMatch()
		{
			IList<string> ifMatch = this._request.Headers.GetCommaSeparatedValues("If-Match");
			if (ifMatch != null)
			{
				this._ifMatchState = StaticFileContext.PreconditionState.PreconditionFailed;
				foreach (string segment in ifMatch)
				{
					if (segment.Equals("*", StringComparison.Ordinal) || segment.Equals(this._etag, StringComparison.Ordinal))
					{
						this._ifMatchState = StaticFileContext.PreconditionState.ShouldProcess;
						break;
					}
				}
			}
			IList<string> ifNoneMatch = this._request.Headers.GetCommaSeparatedValues("If-None-Match");
			if (ifNoneMatch != null)
			{
				this._ifNoneMatchState = StaticFileContext.PreconditionState.ShouldProcess;
				foreach (string segment2 in ifNoneMatch)
				{
					if (segment2.Equals("*", StringComparison.Ordinal) || segment2.Equals(this._etag, StringComparison.Ordinal))
					{
						this._ifNoneMatchState = StaticFileContext.PreconditionState.NotModified;
						break;
					}
				}
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002D98 File Offset: 0x00000F98
		private void ComputeIfModifiedSince()
		{
			string ifModifiedSinceString = this._request.Headers.Get("If-Modified-Since");
			DateTime ifModifiedSince;
			if (Helpers.TryParseHttpDate(ifModifiedSinceString, out ifModifiedSince))
			{
				this._ifModifiedSinceState = ((ifModifiedSince < this._lastModified) ? StaticFileContext.PreconditionState.ShouldProcess : StaticFileContext.PreconditionState.NotModified);
			}
			string ifUnmodifiedSinceString = this._request.Headers.Get("If-Unmodified-Since");
			DateTime ifUnmodifiedSince;
			if (Helpers.TryParseHttpDate(ifUnmodifiedSinceString, out ifUnmodifiedSince))
			{
				this._ifUnmodifiedSinceState = ((ifUnmodifiedSince >= this._lastModified) ? StaticFileContext.PreconditionState.ShouldProcess : StaticFileContext.PreconditionState.PreconditionFailed);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E20 File Offset: 0x00001020
		private void ComputeRange()
		{
			if (!this._isGet)
			{
				return;
			}
			string rangeHeader = this._request.Headers.Get("Range");
			IList<Tuple<long?, long?>> ranges;
			if (!RangeHelpers.TryParseRanges(rangeHeader, out ranges))
			{
				return;
			}
			if (ranges.Count > 1)
			{
				return;
			}
			string ifRangeHeader = this._request.Headers.Get("If-Range");
			if (!string.IsNullOrWhiteSpace(ifRangeHeader))
			{
				bool ignoreRangeHeader = false;
				DateTime ifRangeLastModified;
				if (Helpers.TryParseHttpDate(ifRangeHeader, out ifRangeLastModified))
				{
					if (this._lastModified > ifRangeLastModified)
					{
						ignoreRangeHeader = true;
					}
				}
				else if (!this._etagQuoted.Equals(ifRangeHeader))
				{
					ignoreRangeHeader = true;
				}
				if (ignoreRangeHeader)
				{
					return;
				}
			}
			this._ranges = RangeHelpers.NormalizeRanges(ranges, this._length);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002EC8 File Offset: 0x000010C8
		public void ApplyResponseHeaders(int statusCode)
		{
			this._response.StatusCode = statusCode;
			if (statusCode < 400)
			{
				if (!string.IsNullOrEmpty(this._contentType))
				{
					this._response.ContentType = this._contentType;
				}
				this._response.Headers.Set("Last-Modified", this._lastModifiedString);
				this._response.ETag = this._etagQuoted;
			}
			if (statusCode == 200)
			{
				this._response.ContentLength = new long?(this._length);
			}
			this._options.OnPrepareResponse(new StaticFileResponseContext
			{
				OwinContext = this._context,
				File = this._fileInfo
			});
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002F7E File Offset: 0x0000117E
		public StaticFileContext.PreconditionState GetPreconditionState()
		{
			return StaticFileContext.GetMaxPreconditionState(new StaticFileContext.PreconditionState[] { this._ifMatchState, this._ifNoneMatchState, this._ifModifiedSinceState, this._ifUnmodifiedSinceState });
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002FB0 File Offset: 0x000011B0
		private static StaticFileContext.PreconditionState GetMaxPreconditionState(params StaticFileContext.PreconditionState[] states)
		{
			StaticFileContext.PreconditionState max = StaticFileContext.PreconditionState.Unspecified;
			for (int i = 0; i < states.Length; i++)
			{
				if (states[i] > max)
				{
					max = states[i];
				}
			}
			return max;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002FD8 File Offset: 0x000011D8
		public Task SendStatusAsync(int statusCode)
		{
			this.ApplyResponseHeaders(statusCode);
			return Constants.CompletedTask;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002FE8 File Offset: 0x000011E8
		public Task SendAsync()
		{
			this.ApplyResponseHeaders(200);
			string physicalPath = this._fileInfo.PhysicalPath;
			Func<string, long, long?, CancellationToken, Task> sendFile = this._response.Get<Func<string, long, long?, CancellationToken, Task>>("sendfile.SendAsync");
			if (sendFile != null && !string.IsNullOrEmpty(physicalPath))
			{
				return sendFile(physicalPath, 0L, new long?(this._length), this._request.CallCancelled);
			}
			Stream readStream = this._fileInfo.CreateReadStream();
			StreamCopyOperation copyOperation = new StreamCopyOperation(readStream, this._response.Body, new long?(this._length), this._request.CallCancelled);
			Task task = copyOperation.Start();
			task.ContinueWith(delegate(Task resultTask)
			{
				readStream.Close();
			}, TaskContinuationOptions.ExecuteSynchronously);
			return task;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000030B0 File Offset: 0x000012B0
		internal Task SendRangeAsync()
		{
			bool rangeNotSatisfiable = false;
			if (this._ranges.Count == 0)
			{
				rangeNotSatisfiable = true;
			}
			if (rangeNotSatisfiable)
			{
				this._response.Headers["Content-Range"] = "bytes */" + this._length.ToString(CultureInfo.InvariantCulture);
				this.ApplyResponseHeaders(416);
				return Constants.CompletedTask;
			}
			long start;
			long length;
			this._response.Headers["Content-Range"] = this.ComputeContentRange(this._ranges[0], out start, out length);
			this._response.ContentLength = new long?(length);
			this.ApplyResponseHeaders(206);
			string physicalPath = this._fileInfo.PhysicalPath;
			Func<string, long, long?, CancellationToken, Task> sendFile = this._response.Get<Func<string, long, long?, CancellationToken, Task>>("sendfile.SendAsync");
			if (sendFile != null && !string.IsNullOrEmpty(physicalPath))
			{
				return sendFile(physicalPath, start, new long?(length), this._request.CallCancelled);
			}
			Stream readStream = this._fileInfo.CreateReadStream();
			readStream.Seek(start, SeekOrigin.Begin);
			StreamCopyOperation copyOperation = new StreamCopyOperation(readStream, this._response.Body, new long?(length), this._request.CallCancelled);
			Task task = copyOperation.Start();
			task.ContinueWith(delegate(Task resultTask)
			{
				readStream.Close();
			}, TaskContinuationOptions.ExecuteSynchronously);
			return task;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003214 File Offset: 0x00001414
		private string ComputeContentRange(Tuple<long, long> range, out long start, out long length)
		{
			start = range.Item1;
			long end = range.Item2;
			length = end - start + 1L;
			return string.Format(CultureInfo.InvariantCulture, "bytes {0}-{1}/{2}", new object[] { start, end, this._length });
		}

		// Token: 0x04000026 RID: 38
		private readonly IOwinContext _context;

		// Token: 0x04000027 RID: 39
		private readonly StaticFileOptions _options;

		// Token: 0x04000028 RID: 40
		private readonly PathString _matchUrl;

		// Token: 0x04000029 RID: 41
		private readonly IOwinRequest _request;

		// Token: 0x0400002A RID: 42
		private readonly IOwinResponse _response;

		// Token: 0x0400002B RID: 43
		private string _method;

		// Token: 0x0400002C RID: 44
		private bool _isGet;

		// Token: 0x0400002D RID: 45
		private bool _isHead;

		// Token: 0x0400002E RID: 46
		private PathString _subPath;

		// Token: 0x0400002F RID: 47
		private string _contentType;

		// Token: 0x04000030 RID: 48
		private IFileInfo _fileInfo;

		// Token: 0x04000031 RID: 49
		private long _length;

		// Token: 0x04000032 RID: 50
		private DateTime _lastModified;

		// Token: 0x04000033 RID: 51
		private string _lastModifiedString;

		// Token: 0x04000034 RID: 52
		private string _etag;

		// Token: 0x04000035 RID: 53
		private string _etagQuoted;

		// Token: 0x04000036 RID: 54
		private StaticFileContext.PreconditionState _ifMatchState;

		// Token: 0x04000037 RID: 55
		private StaticFileContext.PreconditionState _ifNoneMatchState;

		// Token: 0x04000038 RID: 56
		private StaticFileContext.PreconditionState _ifModifiedSinceState;

		// Token: 0x04000039 RID: 57
		private StaticFileContext.PreconditionState _ifUnmodifiedSinceState;

		// Token: 0x0400003A RID: 58
		private IList<Tuple<long, long>> _ranges;

		// Token: 0x0200001E RID: 30
		internal enum PreconditionState
		{
			// Token: 0x04000053 RID: 83
			Unspecified,
			// Token: 0x04000054 RID: 84
			NotModified,
			// Token: 0x04000055 RID: 85
			ShouldProcess,
			// Token: 0x04000056 RID: 86
			PreconditionFailed
		}
	}
}
