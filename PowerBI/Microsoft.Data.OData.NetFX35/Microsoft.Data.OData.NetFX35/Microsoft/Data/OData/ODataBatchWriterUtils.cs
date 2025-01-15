using System;
using System.Globalization;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x02000263 RID: 611
	internal static class ODataBatchWriterUtils
	{
		// Token: 0x06001318 RID: 4888 RVA: 0x00047984 File Offset: 0x00045B84
		internal static string CreateBatchBoundary(bool isResponse)
		{
			string text = (isResponse ? "batchresponse_{0}" : "batch_{0}");
			return string.Format(CultureInfo.InvariantCulture, text, new object[] { Guid.NewGuid().ToString() });
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x000479CC File Offset: 0x00045BCC
		internal static string CreateChangeSetBoundary(bool isResponse)
		{
			string text = (isResponse ? "changesetresponse_{0}" : "changeset_{0}");
			return string.Format(CultureInfo.InvariantCulture, text, new object[] { Guid.NewGuid().ToString() });
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00047A14 File Offset: 0x00045C14
		internal static string CreateMultipartMixedContentType(string boundary)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}; {1}={2}", new object[] { "multipart/mixed", "boundary", boundary });
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00047A4C File Offset: 0x00045C4C
		internal static void WriteStartBoundary(TextWriter writer, string boundary, bool firstBoundary)
		{
			if (!firstBoundary)
			{
				writer.WriteLine();
			}
			writer.WriteLine("--{0}", boundary);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00047A63 File Offset: 0x00045C63
		internal static void WriteEndBoundary(TextWriter writer, string boundary, bool missingStartBoundary)
		{
			if (!missingStartBoundary)
			{
				writer.WriteLine();
			}
			writer.Write("--{0}--", boundary);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00047A7C File Offset: 0x00045C7C
		internal static void WriteRequestPreamble(TextWriter writer, string httpMethod, Uri uri)
		{
			writer.WriteLine("{0}: {1}", "Content-Type", "application/http");
			writer.WriteLine("{0}: {1}", "Content-Transfer-Encoding", "binary");
			writer.WriteLine();
			writer.WriteLine("{0} {1} {2}", httpMethod, UriUtilsCommon.UriToString(uri), "HTTP/1.1");
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00047AD0 File Offset: 0x00045CD0
		internal static void WriteResponsePreamble(TextWriter writer)
		{
			writer.WriteLine("{0}: {1}", "Content-Type", "application/http");
			writer.WriteLine("{0}: {1}", "Content-Transfer-Encoding", "binary");
			writer.WriteLine();
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00047B04 File Offset: 0x00045D04
		internal static void WriteChangeSetPreamble(TextWriter writer, string changeSetBoundary)
		{
			string text = ODataBatchWriterUtils.CreateMultipartMixedContentType(changeSetBoundary);
			writer.WriteLine("{0}: {1}", "Content-Type", text);
			writer.WriteLine();
		}
	}
}
