using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Microsoft.OData.MultipartMixed
{
	// Token: 0x02000205 RID: 517
	internal static class ODataMultipartMixedBatchWriterUtils
	{
		// Token: 0x060016BC RID: 5820 RVA: 0x0003F7F8 File Offset: 0x0003D9F8
		internal static string CreateBatchBoundary(bool isResponse)
		{
			string text = (isResponse ? "batchresponse_{0}" : "batch_{0}");
			return string.Format(CultureInfo.InvariantCulture, text, new object[] { Guid.NewGuid() });
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0003F834 File Offset: 0x0003DA34
		internal static string CreateChangeSetBoundary(bool isResponse, string changesetId)
		{
			string text = (isResponse ? "changesetresponse_{0}" : "changeset_{0}");
			return string.Format(CultureInfo.InvariantCulture, text, new object[] { changesetId });
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x0003F866 File Offset: 0x0003DA66
		internal static string CreateMultipartMixedContentType(string boundary)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}; {1}={2}", new object[] { "multipart/mixed", "boundary", boundary });
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x0003F894 File Offset: 0x0003DA94
		internal static string GetBatchBoundaryFromMediaType(ODataMediaType mediaType)
		{
			KeyValuePair<string, string> keyValuePair = default(KeyValuePair<string, string>);
			IEnumerable<KeyValuePair<string, string>> parameters = mediaType.Parameters;
			if (parameters != null)
			{
				bool flag = false;
				foreach (KeyValuePair<string, string> keyValuePair2 in parameters.Where((KeyValuePair<string, string> p) => HttpUtils.CompareMediaTypeParameterNames("boundary", p.Key)))
				{
					if (flag)
					{
						throw new ODataException(Strings.MediaTypeUtils_BoundaryMustBeSpecifiedForBatchPayloads(mediaType.ToText(), "boundary"));
					}
					keyValuePair = keyValuePair2;
					flag = true;
				}
			}
			if (keyValuePair.Key == null)
			{
				throw new ODataException(Strings.MediaTypeUtils_BoundaryMustBeSpecifiedForBatchPayloads(mediaType.ToText(), "boundary"));
			}
			string value = keyValuePair.Value;
			ValidationUtils.ValidateBoundaryString(value);
			return value;
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0003F960 File Offset: 0x0003DB60
		internal static void WriteStartBoundary(TextWriter writer, string boundary, bool firstBoundary)
		{
			if (!firstBoundary)
			{
				writer.WriteLine();
			}
			writer.WriteLine("--{0}", new object[] { boundary });
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0003F980 File Offset: 0x0003DB80
		internal static void WriteEndBoundary(TextWriter writer, string boundary, bool missingStartBoundary)
		{
			if (!missingStartBoundary)
			{
				writer.WriteLine();
			}
			writer.Write("--{0}--", new object[] { boundary });
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0003F9A0 File Offset: 0x0003DBA0
		internal static void WriteRequestPreamble(TextWriter writer, string httpMethod, Uri uri, Uri baseUri, bool inChangeSetBound, string contentId, BatchPayloadUriOption payloadUriOption)
		{
			ODataMultipartMixedBatchWriterUtils.WriteHeaders(writer, inChangeSetBound, contentId);
			writer.WriteLine();
			ODataMultipartMixedBatchWriterUtils.WriteRequestUri(writer, httpMethod, uri, baseUri, payloadUriOption);
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0003F9BD File Offset: 0x0003DBBD
		internal static void WriteResponsePreamble(TextWriter writer, bool inChangeSetBound, string contentId)
		{
			ODataMultipartMixedBatchWriterUtils.WriteHeaders(writer, inChangeSetBound, contentId);
			writer.WriteLine();
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0003F9D0 File Offset: 0x0003DBD0
		internal static void WriteChangeSetPreamble(TextWriter writer, string changeSetBoundary)
		{
			string text = ODataMultipartMixedBatchWriterUtils.CreateMultipartMixedContentType(changeSetBoundary);
			writer.WriteLine("{0}: {1}", new object[] { "Content-Type", text });
			writer.WriteLine();
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x0003FA08 File Offset: 0x0003DC08
		private static void WriteHeaders(TextWriter writer, bool inChangeSetBound, string contentId)
		{
			writer.WriteLine("{0}: {1}", new object[] { "Content-Type", "application/http" });
			writer.WriteLine("{0}: {1}", new object[] { "Content-Transfer-Encoding", "binary" });
			if (inChangeSetBound && contentId != null)
			{
				writer.WriteLine("{0}: {1}", new object[] { "Content-ID", contentId });
			}
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x0003FA7C File Offset: 0x0003DC7C
		private static void WriteRequestUri(TextWriter writer, string httpMethod, Uri uri, Uri baseUri, BatchPayloadUriOption payloadUriOption)
		{
			if (!uri.IsAbsoluteUri)
			{
				writer.WriteLine("{0} {1} {2}", new object[]
				{
					httpMethod,
					UriUtils.UriToString(uri),
					"HTTP/1.1"
				});
				return;
			}
			string absoluteUri = uri.AbsoluteUri;
			switch (payloadUriOption)
			{
			case BatchPayloadUriOption.AbsoluteUri:
				writer.WriteLine("{0} {1} {2}", new object[]
				{
					httpMethod,
					UriUtils.UriToString(uri),
					"HTTP/1.1"
				});
				return;
			case BatchPayloadUriOption.AbsoluteUriUsingHostHeader:
			{
				string text = absoluteUri.Substring(absoluteUri.IndexOf('/', absoluteUri.IndexOf("//", StringComparison.Ordinal) + 2));
				writer.WriteLine("{0} {1} {2}", new object[] { httpMethod, text, "HTTP/1.1" });
				writer.WriteLine("Host: {0}:{1}", new object[] { uri.Host, uri.Port });
				return;
			}
			case BatchPayloadUriOption.RelativeUri:
			{
				string text2 = UriUtils.UriToString(baseUri);
				string text3 = absoluteUri.Substring(text2.Length);
				writer.WriteLine("{0} {1} {2}", new object[] { httpMethod, text3, "HTTP/1.1" });
				return;
			}
			default:
				return;
			}
		}
	}
}
