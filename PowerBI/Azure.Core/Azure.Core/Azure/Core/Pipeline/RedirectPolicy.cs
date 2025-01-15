using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Azure.Core.Diagnostics;

namespace Azure.Core.Pipeline
{
	// Token: 0x020000A0 RID: 160
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RedirectPolicy : HttpPipelinePolicy
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0000F6B7 File Offset: 0x0000D8B7
		internal static RedirectPolicy Shared { get; } = new RedirectPolicy(false);

		// Token: 0x06000504 RID: 1284 RVA: 0x0000F6BE File Offset: 0x0000D8BE
		internal RedirectPolicy(bool allowAutoRedirect)
		{
			this._allowAutoRedirects = allowAutoRedirect;
			this._maxAutomaticRedirections = 50;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000F6D5 File Offset: 0x0000D8D5
		public static void SetAllowAutoRedirect(HttpMessage message, bool allowAutoRedirect)
		{
			message.SetProperty(typeof(RedirectPolicy.AllowRedirectsValueKey), allowAutoRedirect);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000F6F0 File Offset: 0x0000D8F0
		internal async ValueTask ProcessAsync(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline, bool async)
		{
			if (async)
			{
				await HttpPipelinePolicy.ProcessNextAsync(message, pipeline).ConfigureAwait(false);
			}
			else
			{
				HttpPipelinePolicy.ProcessNext(message, pipeline);
			}
			uint redirectCount = 0U;
			Request request = message.Request;
			Response response = message.Response;
			if (this.AllowAutoRedirect(message))
			{
				Uri uriForRedirect;
				while ((uriForRedirect = RedirectPolicy.GetUriForRedirect(request, message.Response)) != null)
				{
					redirectCount += 1U;
					if ((ulong)redirectCount > (ulong)((long)this._maxAutomaticRedirections))
					{
						if (AzureCoreEventSource.Singleton.IsEnabled())
						{
							AzureCoreEventSource.Singleton.RequestRedirectCountExceeded(request.ClientRequestId, request.Uri.ToString(), uriForRedirect.ToString());
							break;
						}
						break;
					}
					else
					{
						response.Dispose();
						request.Headers.Remove(HttpHeader.Names.Authorization);
						AzureCoreEventSource.Singleton.RequestRedirect(request, uriForRedirect, response);
						request.Uri.Reset(uriForRedirect);
						if (RedirectPolicy.RequestRequiresForceGet(response.Status, request.Method))
						{
							request.Method = RequestMethod.Get;
							request.Content = null;
						}
						if (async)
						{
							await HttpPipelinePolicy.ProcessNextAsync(message, pipeline).ConfigureAwait(false);
						}
						else
						{
							HttpPipelinePolicy.ProcessNext(message, pipeline);
						}
						response = message.Response;
					}
				}
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000F74C File Offset: 0x0000D94C
		[return: Nullable(2)]
		private static Uri GetUriForRedirect(Request request, Response response)
		{
			int status = response.Status;
			if (status - 300 > 3 && status - 307 > 1)
			{
				return null;
			}
			string text;
			if (!response.Headers.TryGetValue("Location", out text))
			{
				return null;
			}
			Uri uri;
			if (!RedirectPolicy.TryParseValue(text, out uri))
			{
				return null;
			}
			Uri uri2 = request.Uri.ToUri();
			if (!uri.IsAbsoluteUri)
			{
				uri = new Uri(uri2, uri);
			}
			string fragment = uri2.Fragment;
			if (!string.IsNullOrEmpty(fragment) && string.IsNullOrEmpty(uri.Fragment))
			{
				uri = new UriBuilder(uri)
				{
					Fragment = fragment
				}.Uri;
			}
			if (RedirectPolicy.IsSupportedSecureScheme(uri2.Scheme) && !RedirectPolicy.IsSupportedSecureScheme(uri.Scheme))
			{
				if (AzureCoreEventSource.Singleton.IsEnabled())
				{
					AzureCoreEventSource.Singleton.RequestRedirectBlocked(request.ClientRequestId, uri2.ToString(), uri.ToString());
				}
				return null;
			}
			return uri;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000F82F File Offset: 0x0000DA2F
		private static bool RequestRequiresForceGet(int statusCode, RequestMethod requestMethod)
		{
			if (statusCode - 300 > 2)
			{
				return statusCode == 303 && requestMethod != RequestMethod.Get && requestMethod != RequestMethod.Head;
			}
			return requestMethod == RequestMethod.Post;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000F86D File Offset: 0x0000DA6D
		internal static bool IsSupportedSecureScheme(string scheme)
		{
			return string.Equals(scheme, "https", StringComparison.OrdinalIgnoreCase) || RedirectPolicy.IsSecureWebSocketScheme(scheme);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000F885 File Offset: 0x0000DA85
		internal static bool IsSecureWebSocketScheme(string scheme)
		{
			return string.Equals(scheme, "wss", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000F893 File Offset: 0x0000DA93
		public override ValueTask ProcessAsync(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			return this.ProcessAsync(message, pipeline, true);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000F89E File Offset: 0x0000DA9E
		public override void Process(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			this.ProcessAsync(message, pipeline, false).EnsureCompleted();
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000F8B0 File Offset: 0x0000DAB0
		[NullableContext(2)]
		private static bool TryParseValue([NotNullWhen(true)] string value, [NotNullWhen(true)] out Uri parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(value))
			{
				return false;
			}
			Uri uri;
			if (!Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out uri))
			{
				string text = RedirectPolicy.DecodeUtf8FromString(value);
				if (!Uri.TryCreate(text, UriKind.RelativeOrAbsolute, out uri))
				{
					return false;
				}
			}
			parsedValue = uri;
			return true;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000F8F0 File Offset: 0x0000DAF0
		private static string DecodeUtf8FromString(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				return input;
			}
			bool flag = false;
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] > 'ÿ')
				{
					return input;
				}
				if (input[i] > '\u007f')
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				byte[] array = new byte[input.Length];
				for (int j = 0; j < input.Length; j++)
				{
					if (input[j] > 'ÿ')
					{
						return input;
					}
					array[j] = (byte)input[j];
				}
				try
				{
					return Encoding.GetEncoding("utf-8", EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback).GetString(array, 0, array.Length);
				}
				catch (ArgumentException)
				{
				}
				return input;
			}
			return input;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000F9B0 File Offset: 0x0000DBB0
		private bool AllowAutoRedirect(HttpMessage message)
		{
			object obj;
			if (message.TryGetProperty(typeof(RedirectPolicy.AllowRedirectsValueKey), out obj))
			{
				return (bool)obj;
			}
			return this._allowAutoRedirects;
		}

		// Token: 0x04000211 RID: 529
		private readonly int _maxAutomaticRedirections;

		// Token: 0x04000212 RID: 530
		private readonly bool _allowAutoRedirects;

		// Token: 0x0200012F RID: 303
		[NullableContext(0)]
		private class AllowRedirectsValueKey
		{
		}
	}
}
