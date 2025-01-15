using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Internal;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Web
{
	// Token: 0x0200029F RID: 671
	internal sealed class WebContentsBinaryValue : HttpBinaryValue
	{
		// Token: 0x06001AFF RID: 6911 RVA: 0x00037524 File Offset: 0x00035724
		public static BinaryValue New(Request request, ResourceCredentialCollection credentials, Value webContentsOptions, bool privilegedMode = false, string filePath = null)
		{
			HashSet<string> hashSet;
			WebOptionsHelper.TryGetHiddenQueryKeys(webContentsOptions, out hashSet);
			WebContentsBinaryValue webContentsBinaryValue = new WebContentsBinaryValue(request, credentials, privilegedMode, hashSet);
			return webContentsBinaryValue.NewMeta(DataSource.CreateDataSourceRecordValue(webContentsBinaryValue.GetContentType(request, filePath), webContentsBinaryValue.GetHeaders(request), request, webContentsOptions, webContentsBinaryValue.GetCacheKey(), webContentsBinaryValue.GetStatusCode(request))).AsBinary.WithExpressionFromValue(webContentsBinaryValue);
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x0003757C File Offset: 0x0003577C
		public static BinaryValue New(Lazy<Request> request, ResourceCredentialCollection credentials, Value webContentsOptions, bool privilegedMode = false, string filePath = null)
		{
			HashSet<string> hashSet;
			WebOptionsHelper.TryGetHiddenQueryKeys(webContentsOptions, out hashSet);
			WebContentsBinaryValue webContents = new WebContentsBinaryValue(request, credentials, privilegedMode, hashSet);
			return webContents.NewMeta(DataSource.CreateDataSourceRecordValue(webContents.GetContentType(request, filePath), webContents.GetHeaders(request), request, webContentsOptions, new Lazy<string>(() => webContents.GetCacheKey()), webContents.GetStatusCode(request))).AsBinary;
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x000375F5 File Offset: 0x000357F5
		private WebContentsBinaryValue(Lazy<Request> request, ResourceCredentialCollection credentials, bool privilegedMode, HashSet<string> hiddenQueryKeys)
			: base(privilegedMode)
		{
			this.lazyRequest = request;
			this.credentials = credentials;
			this.hiddenQueryKeys = hiddenQueryKeys;
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x00037614 File Offset: 0x00035814
		private WebContentsBinaryValue(Request request, ResourceCredentialCollection credentials, bool privilegedMode, HashSet<string> hiddenQueryKeys)
			: base(privilegedMode)
		{
			this.request = request;
			this.credentials = credentials;
			this.hiddenQueryKeys = hiddenQueryKeys;
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x00037634 File Offset: 0x00035834
		public override byte[] AsBytes
		{
			get
			{
				Stream stream = this.OpenStream();
				byte[] bytes;
				using (EngineContext.Leave())
				{
					bytes = StreamedBinaryValue.GetBytes(stream);
				}
				return bytes;
			}
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x00037678 File Offset: 0x00035878
		public override bool TryGetLength(out long length)
		{
			using (Response response = this.GetResponse(this.request ?? this.lazyRequest.Value))
			{
				Value value;
				if (response.GetHeaders().TryGetValue("Content-Length", out value) && value.IsText)
				{
					try
					{
						length = Convert.ToInt64(value.AsString, CultureInfo.InvariantCulture);
						if (length >= 0L)
						{
							return true;
						}
					}
					catch (FormatException)
					{
					}
					catch (OverflowException)
					{
					}
				}
			}
			length = -1L;
			return false;
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x0003771C File Offset: 0x0003591C
		protected override Response GetResponse(Request request)
		{
			Response response2;
			try
			{
				Response response = request.GetResponse(this.credentials, null, false);
				base.SetResponseHeaders(response.GetHeaders(), response.StatusCode);
				if (request.IsFailedStatusCode(response))
				{
					Uri uri = UriHelper.RemoveQueryKeys(response.ResponseUri, this.hiddenQueryKeys);
					Message3 message = Strings.WebContentsFailed((uri.AbsoluteUri.Length > 1000) ? uri.AbsoluteUri.Substring(0, 1000) : uri.AbsoluteUri, PiiFree.New(response.StatusCode), response.StatusDescription);
					throw DataSourceException.NewDataSourceError<Message3>(request.Host, message, request.RequestResource, "Url", TextValue.New(uri.AbsoluteUri), TypeValue.Text, response.InnerException);
				}
				response2 = response;
			}
			catch (ResponseException ex)
			{
				throw FileErrors.HandleException(ex.InnerException, request.InitialUri);
			}
			return response2;
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x00037800 File Offset: 0x00035A00
		public override Stream Open()
		{
			return new OutsideOfEngineContextStream(this.OpenStream());
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00037810 File Offset: 0x00035A10
		private Stream OpenStream()
		{
			Stream openStream = base.GetOpenStream();
			if (openStream != null)
			{
				return openStream;
			}
			Request request = this.request ?? this.lazyRequest.Value;
			Stream responseStream;
			using (EngineContext.Leave())
			{
				responseStream = this.GetResponse(request).GetResponseStream();
			}
			return responseStream;
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00037874 File Offset: 0x00035A74
		public string GetCacheKey()
		{
			return (this.request ?? this.lazyRequest.Value).GetCacheKey(this.credentials);
		}

		// Token: 0x0400082F RID: 2095
		private const int maxUriLengthInMessage = 1000;

		// Token: 0x04000830 RID: 2096
		private readonly ResourceCredentialCollection credentials;

		// Token: 0x04000831 RID: 2097
		private readonly Request request;

		// Token: 0x04000832 RID: 2098
		private readonly Lazy<Request> lazyRequest;

		// Token: 0x04000833 RID: 2099
		private readonly HashSet<string> hiddenQueryKeys;
	}
}
