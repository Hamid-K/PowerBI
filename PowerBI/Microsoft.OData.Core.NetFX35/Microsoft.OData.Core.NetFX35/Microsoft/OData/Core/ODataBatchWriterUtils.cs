using System;
using System.Globalization;
using System.IO;

namespace Microsoft.OData.Core
{
	// Token: 0x02000150 RID: 336
	internal static class ODataBatchWriterUtils
	{
		// Token: 0x06000CCA RID: 3274 RVA: 0x000302F4 File Offset: 0x0002E4F4
		internal static string CreateBatchBoundary(bool isResponse)
		{
			string text = (isResponse ? "batchresponse_{0}" : "batch_{0}");
			return string.Format(CultureInfo.InvariantCulture, text, new object[] { Guid.NewGuid().ToString() });
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0003033C File Offset: 0x0002E53C
		internal static string CreateChangeSetBoundary(bool isResponse)
		{
			string text = (isResponse ? "changesetresponse_{0}" : "changeset_{0}");
			return string.Format(CultureInfo.InvariantCulture, text, new object[] { Guid.NewGuid().ToString() });
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00030384 File Offset: 0x0002E584
		internal static string CreateMultipartMixedContentType(string boundary)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}; {1}={2}", new object[] { "multipart/mixed", "boundary", boundary });
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x000303BC File Offset: 0x0002E5BC
		internal static void WriteStartBoundary(TextWriter writer, string boundary, bool firstBoundary)
		{
			if (!firstBoundary)
			{
				writer.WriteLine();
			}
			writer.WriteLine("--{0}", boundary);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x000303D3 File Offset: 0x0002E5D3
		internal static void WriteEndBoundary(TextWriter writer, string boundary, bool missingStartBoundary)
		{
			if (!missingStartBoundary)
			{
				writer.WriteLine();
			}
			writer.Write("--{0}--", boundary);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x000303EC File Offset: 0x0002E5EC
		internal static void WriteRequestPreamble(TextWriter writer, string httpMethod, Uri uri, bool inChangeSetBound, string contentId)
		{
			writer.WriteLine("{0}: {1}", "Content-Type", "application/http");
			writer.WriteLine("{0}: {1}", "Content-Transfer-Encoding", "binary");
			if (inChangeSetBound && contentId != null)
			{
				writer.WriteLine("{0}: {1}", "Content-ID", contentId);
			}
			writer.WriteLine();
			writer.WriteLine("{0} {1} {2}", httpMethod, UriUtils.UriToString(uri), "HTTP/1.1");
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0003045C File Offset: 0x0002E65C
		internal static void WriteResponsePreamble(TextWriter writer, bool inChangeSetBound, string contentId)
		{
			writer.WriteLine("{0}: {1}", "Content-Type", "application/http");
			writer.WriteLine("{0}: {1}", "Content-Transfer-Encoding", "binary");
			if (inChangeSetBound && contentId != null)
			{
				writer.WriteLine("{0}: {1}", "Content-ID", contentId);
			}
			writer.WriteLine();
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x000304B0 File Offset: 0x0002E6B0
		internal static void WriteChangeSetPreamble(TextWriter writer, string changeSetBoundary)
		{
			string text = ODataBatchWriterUtils.CreateMultipartMixedContentType(changeSetBoundary);
			writer.WriteLine("{0}: {1}", "Content-Type", text);
			writer.WriteLine();
		}
	}
}
