using System;
using System.Globalization;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x0200003D RID: 61
	internal static class ODataBatchWriterUtils
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x0000843C File Offset: 0x0000663C
		internal static string CreateBatchBoundary(bool isResponse)
		{
			string text = (isResponse ? "batchresponse_{0}" : "batch_{0}");
			return string.Format(CultureInfo.InvariantCulture, text, new object[] { Guid.NewGuid().ToString() });
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00008480 File Offset: 0x00006680
		internal static string CreateChangeSetBoundary(bool isResponse)
		{
			string text = (isResponse ? "changesetresponse_{0}" : "changeset_{0}");
			return string.Format(CultureInfo.InvariantCulture, text, new object[] { Guid.NewGuid().ToString() });
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000084C4 File Offset: 0x000066C4
		internal static string CreateMultipartMixedContentType(string boundary)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}; {1}={2}", new object[] { "multipart/mixed", "boundary", boundary });
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000084EF File Offset: 0x000066EF
		internal static void WriteStartBoundary(TextWriter writer, string boundary, bool firstBoundary)
		{
			if (!firstBoundary)
			{
				writer.WriteLine();
			}
			writer.WriteLine("--{0}", boundary);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008506 File Offset: 0x00006706
		internal static void WriteEndBoundary(TextWriter writer, string boundary, bool missingStartBoundary)
		{
			if (!missingStartBoundary)
			{
				writer.WriteLine();
			}
			writer.Write("--{0}--", boundary);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000851D File Offset: 0x0000671D
		internal static void WriteRequestPreamble(TextWriter writer, string httpMethod, Uri uri, Uri baseUri, bool inChangeSetBound, string contentId, BatchPayloadUriOption payloadUriOption)
		{
			ODataBatchWriterUtils.WriteHeaders(writer, inChangeSetBound, contentId);
			writer.WriteLine();
			ODataBatchWriterUtils.WriteRequestUri(writer, httpMethod, uri, baseUri, payloadUriOption);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000853A File Offset: 0x0000673A
		internal static void WriteResponsePreamble(TextWriter writer, bool inChangeSetBound, string contentId)
		{
			ODataBatchWriterUtils.WriteHeaders(writer, inChangeSetBound, contentId);
			writer.WriteLine();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000854C File Offset: 0x0000674C
		internal static void WriteChangeSetPreamble(TextWriter writer, string changeSetBoundary)
		{
			string text = ODataBatchWriterUtils.CreateMultipartMixedContentType(changeSetBoundary);
			writer.WriteLine("{0}: {1}", "Content-Type", text);
			writer.WriteLine();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008578 File Offset: 0x00006778
		private static void WriteHeaders(TextWriter writer, bool inChangeSetBound, string contentId)
		{
			writer.WriteLine("{0}: {1}", "Content-Type", "application/http");
			writer.WriteLine("{0}: {1}", "Content-Transfer-Encoding", "binary");
			if (inChangeSetBound && contentId != null)
			{
				writer.WriteLine("{0}: {1}", "Content-ID", contentId);
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000085C8 File Offset: 0x000067C8
		private static void WriteRequestUri(TextWriter writer, string httpMethod, Uri uri, Uri baseUri, BatchPayloadUriOption payloadUriOption)
		{
			if (!uri.IsAbsoluteUri)
			{
				writer.WriteLine("{0} {1} {2}", httpMethod, UriUtils.UriToString(uri), "HTTP/1.1");
				return;
			}
			string absoluteUri = uri.AbsoluteUri;
			switch (payloadUriOption)
			{
			case BatchPayloadUriOption.AbsoluteUri:
				writer.WriteLine("{0} {1} {2}", httpMethod, UriUtils.UriToString(uri), "HTTP/1.1");
				return;
			case BatchPayloadUriOption.AbsoluteUriUsingHostHeader:
			{
				string text = absoluteUri.Substring(absoluteUri.IndexOf('/', absoluteUri.IndexOf("//", 4) + 2));
				writer.WriteLine("{0} {1} {2}", httpMethod, text, "HTTP/1.1");
				writer.WriteLine("Host: {0}:{1}", uri.Host, uri.Port);
				return;
			}
			case BatchPayloadUriOption.RelativeUri:
			{
				string text2 = UriUtils.UriToString(baseUri);
				string text3 = absoluteUri.Substring(text2.Length);
				writer.WriteLine("{0} {1} {2}", httpMethod, text3, "HTTP/1.1");
				return;
			}
			default:
				return;
			}
		}
	}
}
