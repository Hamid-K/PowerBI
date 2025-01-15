using System;
using System.IO;
using System.Threading;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Content;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A54 RID: 2644
	internal abstract class HttpBinaryValue : StreamedBinaryValue
	{
		// Token: 0x060049E0 RID: 18912 RVA: 0x000F625A File Offset: 0x000F445A
		protected HttpBinaryValue()
			: this(false)
		{
		}

		// Token: 0x060049E1 RID: 18913 RVA: 0x000F6263 File Offset: 0x000F4463
		protected HttpBinaryValue(bool privilegedMode)
		{
			this.privilegedMode = privilegedMode;
		}

		// Token: 0x060049E2 RID: 18914 RVA: 0x000F6274 File Offset: 0x000F4474
		protected Stream GetOpenStream()
		{
			Stream stream;
			if (this.openResponse != null && this.openResponse.TryGetStreamAndDispose(out stream))
			{
				this.openResponse = null;
				return stream;
			}
			return null;
		}

		// Token: 0x060049E3 RID: 18915 RVA: 0x000F62A4 File Offset: 0x000F44A4
		private bool TrySetOpenStream(Request request, Response response)
		{
			ILifetimeService lifetimeService = request.Host.QueryService<ILifetimeService>();
			if (lifetimeService != null)
			{
				HttpBinaryValue.OpenResponse openResponse = this.openResponse;
				if (openResponse != null)
				{
					openResponse.Dispose();
				}
				this.openResponse = new HttpBinaryValue.OpenResponse(lifetimeService, response);
				return true;
			}
			return false;
		}

		// Token: 0x060049E4 RID: 18916 RVA: 0x000F62E4 File Offset: 0x000F44E4
		private bool MakeRequest(Request request, Action<Response> action)
		{
			Response response = null;
			try
			{
				using (EngineContext.Leave())
				{
					response = this.GetResponse(request);
				}
				this.SetResponseHeaders(response.GetHeaders(), response.StatusCode);
				if (action != null)
				{
					action(response);
				}
				else if (this.TrySetOpenStream(request, response))
				{
					response = null;
				}
				return true;
			}
			catch (ResourceAccessAuthorizationException)
			{
			}
			catch (ResourceAccessForbiddenException)
			{
			}
			finally
			{
				if (response != null)
				{
					response.Dispose();
				}
			}
			return false;
		}

		// Token: 0x060049E5 RID: 18917 RVA: 0x000F6388 File Offset: 0x000F4588
		private TextValue GetContentTypeCore(Request request, string filePath)
		{
			string text;
			try
			{
				text = ((filePath == null) ? Path.GetExtension(request.Uri.GetLeftPart(UriPartial.Path)) : Path.GetExtension(filePath));
			}
			catch (ArgumentException)
			{
				text = string.Empty;
			}
			string contentTypeForExtension = ContentHelper.GetContentTypeForExtension(text);
			string bestMatch;
			string determinedContentType = this.DetermineContentType(contentTypeForExtension, out bestMatch);
			if (determinedContentType == null && !this.MakeRequest(request, delegate(Response webResponse)
			{
				determinedContentType = this.DetermineContentType(contentTypeForExtension, out bestMatch);
				if (determinedContentType == null)
				{
					using (Stream responseStream = webResponse.GetResponseStream())
					{
						byte[] array = new byte[4096];
						int num = responseStream.Read(array, 0, array.Length);
						if (num > 0)
						{
							determinedContentType = ContentHelper.GetContentType(array, num, bestMatch);
						}
					}
				}
			}))
			{
				determinedContentType = bestMatch;
			}
			if (determinedContentType == null)
			{
				return HttpBinaryValue.ApplicationOctetStream;
			}
			return TextValue.New(determinedContentType);
		}

		// Token: 0x060049E6 RID: 18918 RVA: 0x000F6440 File Offset: 0x000F4640
		private string DetermineContentType(string contentTypeForExtension, out string bestMatch)
		{
			bestMatch = contentTypeForExtension;
			if (!ContentHelper.IsAmbiguousContentType(contentTypeForExtension))
			{
				return contentTypeForExtension;
			}
			if (this.responseHeaders == null)
			{
				return null;
			}
			string text = this.ResponseContentType.AsString;
			if (!string.IsNullOrEmpty(text))
			{
				int num = text.IndexOf(';');
				if (num > 0)
				{
					text = text.Substring(0, num);
				}
				text = ContentHelper.GetBestContentType(text, contentTypeForExtension);
			}
			else
			{
				text = null;
			}
			if (text == null || ContentHelper.IsAmbiguousContentType(text))
			{
				bestMatch = text ?? contentTypeForExtension;
				return null;
			}
			return text;
		}

		// Token: 0x060049E7 RID: 18919 RVA: 0x000F64B0 File Offset: 0x000F46B0
		private RecordValue GetHeadersCore(Request request)
		{
			if (this.responseHeaders == null)
			{
				this.MakeRequest(request, null);
			}
			if (this.privilegedMode)
			{
				return this.responseHeaders ?? RecordValue.Empty;
			}
			Value value = Library.Record.SelectFields.Invoke(this.responseHeaders ?? RecordValue.Empty, HttpBinaryValue.sometimesVisibleHeaders, Library.MissingField.Ignore);
			if (request.Method == "HEAD")
			{
				return value.AsRecord;
			}
			return RecordValue.New(HttpBinaryValue.alwaysVisibleHeaders, delegate(int i)
			{
				Value value2;
				if (this.responseHeaders != null && this.responseHeaders.TryGetValue(HttpBinaryValue.alwaysVisibleHeaders[i], out value2))
				{
					return value2;
				}
				return Value.Null;
			}).Concatenate(value).AsRecord;
		}

		// Token: 0x060049E8 RID: 18920 RVA: 0x000F6544 File Offset: 0x000F4744
		private Value GetStatusCodeCore(Request request)
		{
			if (this.statusCode == null)
			{
				this.MakeRequest(request, null);
			}
			if (this.statusCode == null)
			{
				return Value.Null;
			}
			return NumberValue.New(this.statusCode.Value);
		}

		// Token: 0x1700175D RID: 5981
		// (get) Token: 0x060049E9 RID: 18921 RVA: 0x000F6580 File Offset: 0x000F4780
		private TextValue ResponseContentType
		{
			get
			{
				Value value;
				if (this.responseHeaders.TryGetValue("Content-Type", out value) && value.IsText)
				{
					return value.AsText;
				}
				return TextValue.Empty;
			}
		}

		// Token: 0x060049EA RID: 18922
		protected abstract Response GetResponse(Request request);

		// Token: 0x060049EB RID: 18923 RVA: 0x000F65B5 File Offset: 0x000F47B5
		public IValueReference GetContentType(Request request, string filePath = null)
		{
			return new DelayedValue(() => this.GetContentTypeCore(request, filePath));
		}

		// Token: 0x060049EC RID: 18924 RVA: 0x000F65E1 File Offset: 0x000F47E1
		public IValueReference GetHeaders(Request request)
		{
			return new DelayedValue(() => this.GetHeadersCore(request));
		}

		// Token: 0x060049ED RID: 18925 RVA: 0x000F6606 File Offset: 0x000F4806
		public IValueReference GetStatusCode(Request request)
		{
			return new DelayedValue(() => this.GetStatusCodeCore(request));
		}

		// Token: 0x060049EE RID: 18926 RVA: 0x000F662B File Offset: 0x000F482B
		public IValueReference GetContentType(Lazy<Request> request, string filePath = null)
		{
			return new DelayedValue(() => this.GetContentTypeCore(request.Value, filePath));
		}

		// Token: 0x060049EF RID: 18927 RVA: 0x000F6657 File Offset: 0x000F4857
		public IValueReference GetHeaders(Lazy<Request> request)
		{
			return new DelayedValue(() => this.GetHeadersCore(request.Value));
		}

		// Token: 0x060049F0 RID: 18928 RVA: 0x000F667C File Offset: 0x000F487C
		public IValueReference GetStatusCode(Lazy<Request> request)
		{
			return new DelayedValue(() => this.GetStatusCodeCore(request.Value));
		}

		// Token: 0x060049F1 RID: 18929 RVA: 0x000F66A1 File Offset: 0x000F48A1
		protected void SetResponseHeaders(RecordValue headers, int statusCode)
		{
			this.responseHeaders = this.responseHeaders ?? headers;
			this.statusCode = new int?(this.statusCode.GetValueOrDefault(statusCode));
		}

		// Token: 0x04002750 RID: 10064
		private static readonly TextValue ApplicationOctetStream = TextValue.New("application/octet-stream");

		// Token: 0x04002751 RID: 10065
		private static readonly TimeSpan maximumOpenDuration = TimeSpan.FromSeconds(10.0);

		// Token: 0x04002752 RID: 10066
		private static readonly Keys alwaysVisibleHeaders = Keys.New("Content-Type");

		// Token: 0x04002753 RID: 10067
		private static readonly Value sometimesVisibleHeaders = ListValue.New(new string[]
		{
			"Accept-Ranges", "Age", "Allow", "Cache-Control", "Content-Encoding", "Content-Language", "Content-Length", "Content-Location", "Content-Range", "Content-Type",
			"Date", "ETag", "Expires", "LastModified", "Location", "MicrosoftSharePointTeamServices", "Proxy-Authenticate", "Server", "Vary", "Warning",
			"WWW-Authenticate"
		});

		// Token: 0x04002754 RID: 10068
		private readonly bool privilegedMode;

		// Token: 0x04002755 RID: 10069
		private RecordValue responseHeaders;

		// Token: 0x04002756 RID: 10070
		private int? statusCode;

		// Token: 0x04002757 RID: 10071
		private HttpBinaryValue.OpenResponse openResponse;

		// Token: 0x02000A55 RID: 2645
		private sealed class OpenResponse : IDisposable
		{
			// Token: 0x060049F4 RID: 18932 RVA: 0x000F6808 File Offset: 0x000F4A08
			public OpenResponse(ILifetimeService lifetimeService, Response response)
			{
				this.syncRoot = new object();
				this.lifetimeService = lifetimeService;
				this.timer = new Timer(new TimerCallback(this.OnTimer), null, (int)HttpBinaryValue.maximumOpenDuration.TotalMilliseconds, -1);
				this.response = response;
				lifetimeService.Register(this);
			}

			// Token: 0x060049F5 RID: 18933 RVA: 0x000F6864 File Offset: 0x000F4A64
			public bool TryGetStreamAndDispose(out Stream stream)
			{
				Response localResponse = this.TakeResponse();
				if (localResponse == null)
				{
					stream = null;
					return false;
				}
				this.UnregisterAndDispose();
				stream = localResponse.GetResponseStream().AfterDispose(delegate
				{
					localResponse.Dispose();
				});
				return true;
			}

			// Token: 0x060049F6 RID: 18934 RVA: 0x000F68B5 File Offset: 0x000F4AB5
			public void Dispose()
			{
				Response response = this.TakeResponse();
				if (response != null)
				{
					response.Dispose();
				}
				this.timer.Dispose();
			}

			// Token: 0x060049F7 RID: 18935 RVA: 0x000F68D4 File Offset: 0x000F4AD4
			private Response TakeResponse()
			{
				object obj = this.syncRoot;
				Response response;
				lock (obj)
				{
					response = this.response;
					this.response = null;
				}
				return response;
			}

			// Token: 0x060049F8 RID: 18936 RVA: 0x000F6920 File Offset: 0x000F4B20
			private void UnregisterAndDispose()
			{
				this.lifetimeService.Unregister(this);
				this.Dispose();
			}

			// Token: 0x060049F9 RID: 18937 RVA: 0x000F6934 File Offset: 0x000F4B34
			private void OnTimer(object obj)
			{
				this.UnregisterAndDispose();
			}

			// Token: 0x04002758 RID: 10072
			private readonly object syncRoot;

			// Token: 0x04002759 RID: 10073
			private readonly ILifetimeService lifetimeService;

			// Token: 0x0400275A RID: 10074
			private readonly Timer timer;

			// Token: 0x0400275B RID: 10075
			private Response response;
		}
	}
}
