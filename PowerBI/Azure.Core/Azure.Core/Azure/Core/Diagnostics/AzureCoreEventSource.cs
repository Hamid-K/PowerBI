using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Text;

namespace Azure.Core.Diagnostics
{
	// Token: 0x020000C8 RID: 200
	[NullableContext(1)]
	[Nullable(0)]
	[EventSource(Name = "Azure-Core")]
	internal sealed class AzureCoreEventSource : AzureEventSource
	{
		// Token: 0x060006A1 RID: 1697 RVA: 0x00016960 File Offset: 0x00014B60
		private AzureCoreEventSource()
			: base("Azure-Core")
		{
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0001696D File Offset: 0x00014B6D
		public static AzureCoreEventSource Singleton { get; } = new AzureCoreEventSource();

		// Token: 0x060006A3 RID: 1699 RVA: 0x00016974 File Offset: 0x00014B74
		[Event(19, Level = EventLevel.Informational, Message = "Background token refresh [{0}] failed with exception {1}")]
		public void BackgroundRefreshFailed(string requestId, string exception)
		{
			base.WriteEvent(19, requestId, exception);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00016980 File Offset: 0x00014B80
		[NonEvent]
		public void Request(Request request, [Nullable(2)] string assemblyName, HttpMessageSanitizer sanitizer)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.None))
			{
				this.Request(request.ClientRequestId, request.Method.ToString(), sanitizer.SanitizeUrl(request.Uri.ToString()), AzureCoreEventSource.FormatHeaders(request.Headers, sanitizer), assemblyName);
			}
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x000169DB File Offset: 0x00014BDB
		[Event(1, Level = EventLevel.Informational, Message = "Request [{0}] {1} {2}\r\n{3}client assembly: {4}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with primitive types.")]
		public void Request(string requestId, string method, string uri, string headers, [Nullable(2)] string clientAssembly)
		{
			base.WriteEvent(1, new object[] { requestId, method, uri, headers, clientAssembly });
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00016A00 File Offset: 0x00014C00
		[NonEvent]
		public void RequestContent(string requestId, byte[] content, [Nullable(2)] Encoding textEncoding)
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.None))
			{
				if (textEncoding != null)
				{
					this.RequestContentText(requestId, textEncoding.GetString(content));
					return;
				}
				this.RequestContent(requestId, content);
			}
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00016A27 File Offset: 0x00014C27
		[Event(2, Level = EventLevel.Verbose, Message = "Request [{0}] content: {1}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with an array with primitive type elements.")]
		public void RequestContent(string requestId, byte[] content)
		{
			base.WriteEvent(2, new object[] { requestId, content });
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00016A3E File Offset: 0x00014C3E
		[Event(17, Level = EventLevel.Verbose, Message = "Request [{0}] content: {1}")]
		public void RequestContentText(string requestId, string content)
		{
			base.WriteEvent(17, requestId, content);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00016A4A File Offset: 0x00014C4A
		[NonEvent]
		public void Response(Response response, HttpMessageSanitizer sanitizer, double elapsed)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.None))
			{
				this.Response(response.ClientRequestId, response.Status, response.ReasonPhrase, AzureCoreEventSource.FormatHeaders(response.Headers, sanitizer), elapsed);
			}
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00016A81 File Offset: 0x00014C81
		[Event(5, Level = EventLevel.Informational, Message = "Response [{0}] {1} {2} ({4:00.0}s)\r\n{3}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with primitive types.")]
		public void Response(string requestId, int status, string reasonPhrase, string headers, double seconds)
		{
			base.WriteEvent(5, new object[] { requestId, status, reasonPhrase, headers, seconds });
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00016AB0 File Offset: 0x00014CB0
		[NonEvent]
		public void ResponseContent(string requestId, byte[] content, [Nullable(2)] Encoding textEncoding)
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.None))
			{
				if (textEncoding != null)
				{
					this.ResponseContentText(requestId, textEncoding.GetString(content));
					return;
				}
				this.ResponseContent(requestId, content);
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00016AD7 File Offset: 0x00014CD7
		[Event(6, Level = EventLevel.Verbose, Message = "Response [{0}] content: {1}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with an array with primitive type elements.")]
		public void ResponseContent(string requestId, byte[] content)
		{
			base.WriteEvent(6, new object[] { requestId, content });
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00016AEE File Offset: 0x00014CEE
		[Event(13, Level = EventLevel.Verbose, Message = "Response [{0}] content: {1}")]
		public void ResponseContentText(string requestId, string content)
		{
			base.WriteEvent(13, requestId, content);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00016AFA File Offset: 0x00014CFA
		[NonEvent]
		public void ResponseContentBlock(string requestId, int blockNumber, byte[] content, [Nullable(2)] Encoding textEncoding)
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.None))
			{
				if (textEncoding != null)
				{
					this.ResponseContentTextBlock(requestId, blockNumber, textEncoding.GetString(content));
					return;
				}
				this.ResponseContentBlock(requestId, blockNumber, content);
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00016B25 File Offset: 0x00014D25
		[Event(11, Level = EventLevel.Verbose, Message = "Response [{0}] content block {1}: {2}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with an array with primitive type elements.")]
		public void ResponseContentBlock(string requestId, int blockNumber, byte[] content)
		{
			base.WriteEvent(11, new object[] { requestId, blockNumber, content });
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00016B46 File Offset: 0x00014D46
		[Event(15, Level = EventLevel.Verbose, Message = "Response [{0}] content block {1}: {2}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with primitive types.")]
		public void ResponseContentTextBlock(string requestId, int blockNumber, string content)
		{
			base.WriteEvent(15, new object[] { requestId, blockNumber, content });
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00016B67 File Offset: 0x00014D67
		[NonEvent]
		public void ErrorResponse(Response response, HttpMessageSanitizer sanitizer, double elapsed)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.None))
			{
				this.ErrorResponse(response.ClientRequestId, response.Status, response.ReasonPhrase, AzureCoreEventSource.FormatHeaders(response.Headers, sanitizer), elapsed);
			}
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00016B9E File Offset: 0x00014D9E
		[Event(8, Level = EventLevel.Warning, Message = "Error response [{0}] {1} {2} ({4:00.0}s)\r\n{3}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with primitive types.")]
		public void ErrorResponse(string requestId, int status, string reasonPhrase, string headers, double seconds)
		{
			base.WriteEvent(8, new object[] { requestId, status, reasonPhrase, headers, seconds });
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00016BCD File Offset: 0x00014DCD
		[NonEvent]
		public void ErrorResponseContent(string requestId, byte[] content, [Nullable(2)] Encoding textEncoding)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.None))
			{
				if (textEncoding != null)
				{
					this.ErrorResponseContentText(requestId, textEncoding.GetString(content));
					return;
				}
				this.ErrorResponseContent(requestId, content);
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00016BF4 File Offset: 0x00014DF4
		[Event(9, Level = EventLevel.Informational, Message = "Error response [{0}] content: {1}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with an array with primitive type elements.")]
		public void ErrorResponseContent(string requestId, byte[] content)
		{
			base.WriteEvent(9, new object[] { requestId, content });
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00016C0C File Offset: 0x00014E0C
		[Event(14, Level = EventLevel.Informational, Message = "Error response [{0}] content: {1}")]
		public void ErrorResponseContentText(string requestId, string content)
		{
			base.WriteEvent(14, requestId, content);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00016C18 File Offset: 0x00014E18
		[NonEvent]
		public void ErrorResponseContentBlock(string requestId, int blockNumber, byte[] content, [Nullable(2)] Encoding textEncoding)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.None))
			{
				if (textEncoding != null)
				{
					this.ErrorResponseContentTextBlock(requestId, blockNumber, textEncoding.GetString(content));
					return;
				}
				this.ErrorResponseContentBlock(requestId, blockNumber, content);
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00016C43 File Offset: 0x00014E43
		[Event(12, Level = EventLevel.Informational, Message = "Error response [{0}] content block {1}: {2}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with an array with primitive type elements.")]
		public void ErrorResponseContentBlock(string requestId, int blockNumber, byte[] content)
		{
			base.WriteEvent(12, new object[] { requestId, blockNumber, content });
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00016C64 File Offset: 0x00014E64
		[Event(16, Level = EventLevel.Informational, Message = "Error response [{0}] content block {1}: {2}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with primitive types.")]
		public void ErrorResponseContentTextBlock(string requestId, int blockNumber, string content)
		{
			base.WriteEvent(16, new object[] { requestId, blockNumber, content });
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00016C85 File Offset: 0x00014E85
		[Event(10, Level = EventLevel.Informational, Message = "Request [{0}] attempt number {1} took {2:00.0}s")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with primitive types.")]
		public void RequestRetrying(string requestId, int retryNumber, double seconds)
		{
			base.WriteEvent(10, new object[] { requestId, retryNumber, seconds });
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00016CAB File Offset: 0x00014EAB
		[Event(7, Level = EventLevel.Warning, Message = "Response [{0}] took {1:00.0}s")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with primitive types.")]
		public void ResponseDelay(string requestId, double seconds)
		{
			base.WriteEvent(7, new object[] { requestId, seconds });
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00016CC7 File Offset: 0x00014EC7
		[Event(18, Level = EventLevel.Informational, Message = "Request [{0}] exception {1}")]
		public void ExceptionResponse(string requestId, string exception)
		{
			base.WriteEvent(18, requestId, exception);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00016CD3 File Offset: 0x00014ED3
		[NonEvent]
		public void RequestRedirect(Request request, Uri redirectUri, Response response)
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.None))
			{
				this.RequestRedirect(request.ClientRequestId, request.Uri.ToString(), redirectUri.ToString(), response.Status);
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00016D03 File Offset: 0x00014F03
		[Event(20, Level = EventLevel.Verbose, Message = "Request [{0}] Redirecting from {1} to {2} in response to status code {3}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with primitive types.")]
		public void RequestRedirect(string requestId, string from, string to, int status)
		{
			base.WriteEvent(20, new object[] { requestId, from, to, status });
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00016D29 File Offset: 0x00014F29
		[Event(21, Level = EventLevel.Warning, Message = "Request [{0}] Insecure HTTPS to HTTP redirect from {1} to {2} was blocked.")]
		public void RequestRedirectBlocked(string requestId, string from, string to)
		{
			base.WriteEvent(21, requestId, from, to);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00016D36 File Offset: 0x00014F36
		[Event(22, Level = EventLevel.Warning, Message = "Request [{0}] Exceeded max number of redirects. Redirect from {1} to {2} blocked.")]
		public void RequestRedirectCountExceeded(string requestId, string from, string to)
		{
			base.WriteEvent(22, requestId, from, to);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00016D43 File Offset: 0x00014F43
		[NonEvent]
		public void PipelineTransportOptionsNotApplied(Type optionsType)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.None))
			{
				this.PipelineTransportOptionsNotApplied(optionsType.FullName ?? string.Empty);
			}
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00016D65 File Offset: 0x00014F65
		[Event(23, Level = EventLevel.Informational, Message = "The client requires transport configuration but it was not applied because custom transport was provided. Type: {0}")]
		public void PipelineTransportOptionsNotApplied(string optionsType)
		{
			base.WriteEvent(23, optionsType);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00016D70 File Offset: 0x00014F70
		[NonEvent]
		private static string FormatHeaders(IEnumerable<HttpHeader> headers, HttpMessageSanitizer sanitizer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (HttpHeader httpHeader in headers)
			{
				stringBuilder.Append(httpHeader.Name);
				stringBuilder.Append(':');
				stringBuilder.Append(sanitizer.SanitizeHeader(httpHeader.Name, httpHeader.Value));
				stringBuilder.Append(Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400028C RID: 652
		private const string EventSourceName = "Azure-Core";

		// Token: 0x0400028D RID: 653
		private const int RequestEvent = 1;

		// Token: 0x0400028E RID: 654
		private const int RequestContentEvent = 2;

		// Token: 0x0400028F RID: 655
		private const int ResponseEvent = 5;

		// Token: 0x04000290 RID: 656
		private const int ResponseContentEvent = 6;

		// Token: 0x04000291 RID: 657
		private const int ResponseDelayEvent = 7;

		// Token: 0x04000292 RID: 658
		private const int ErrorResponseEvent = 8;

		// Token: 0x04000293 RID: 659
		private const int ErrorResponseContentEvent = 9;

		// Token: 0x04000294 RID: 660
		private const int RequestRetryingEvent = 10;

		// Token: 0x04000295 RID: 661
		private const int ResponseContentBlockEvent = 11;

		// Token: 0x04000296 RID: 662
		private const int ErrorResponseContentBlockEvent = 12;

		// Token: 0x04000297 RID: 663
		private const int ResponseContentTextEvent = 13;

		// Token: 0x04000298 RID: 664
		private const int ErrorResponseContentTextEvent = 14;

		// Token: 0x04000299 RID: 665
		private const int ResponseContentTextBlockEvent = 15;

		// Token: 0x0400029A RID: 666
		private const int ErrorResponseContentTextBlockEvent = 16;

		// Token: 0x0400029B RID: 667
		private const int RequestContentTextEvent = 17;

		// Token: 0x0400029C RID: 668
		private const int ExceptionResponseEvent = 18;

		// Token: 0x0400029D RID: 669
		private const int BackgroundRefreshFailedEvent = 19;

		// Token: 0x0400029E RID: 670
		private const int RequestRedirectEvent = 20;

		// Token: 0x0400029F RID: 671
		private const int RequestRedirectBlockedEvent = 21;

		// Token: 0x040002A0 RID: 672
		private const int RequestRedirectCountExceededEvent = 22;

		// Token: 0x040002A1 RID: 673
		private const int PipelineTransportOptionsNotAppliedEvent = 23;
	}
}
