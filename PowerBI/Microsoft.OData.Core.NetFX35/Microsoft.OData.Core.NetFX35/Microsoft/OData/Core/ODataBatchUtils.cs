using System;
using System.Globalization;
using System.IO;

namespace Microsoft.OData.Core
{
	// Token: 0x0200014D RID: 333
	internal static class ODataBatchUtils
	{
		// Token: 0x06000C9A RID: 3226 RVA: 0x0002F754 File Offset: 0x0002D954
		internal static Uri CreateOperationRequestUri(Uri uri, Uri baseUri, IODataUrlResolver urlResolver)
		{
			Uri uri2;
			if (urlResolver != null)
			{
				uri2 = urlResolver.ResolveUrl(baseUri, uri);
				if (uri2 != null)
				{
					return uri2;
				}
			}
			if (uri.IsAbsoluteUri)
			{
				uri2 = uri;
			}
			else
			{
				if (baseUri == null)
				{
					string text = (UriUtils.UriToString(uri).StartsWith("$", 4) ? Strings.ODataBatchUtils_RelativeUriStartingWithDollarUsedWithoutBaseUriSpecified(UriUtils.UriToString(uri)) : Strings.ODataBatchUtils_RelativeUriUsedWithoutBaseUriSpecified(UriUtils.UriToString(uri)));
					throw new ODataException(text);
				}
				uri2 = UriUtils.UriToAbsoluteUri(baseUri, uri);
			}
			return uri2;
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0002F7CC File Offset: 0x0002D9CC
		internal static ODataBatchOperationReadStream CreateBatchOperationReadStream(ODataBatchReaderStream batchReaderStream, ODataBatchOperationHeaders headers, IODataBatchOperationListener operationListener)
		{
			string text;
			if (!headers.TryGetValue("Content-Length", out text))
			{
				return ODataBatchOperationReadStream.Create(batchReaderStream, operationListener);
			}
			int num = int.Parse(text, CultureInfo.InvariantCulture);
			if (num < 0)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidContentLengthSpecified(text));
			}
			return ODataBatchOperationReadStream.Create(batchReaderStream, operationListener, num);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0002F814 File Offset: 0x0002DA14
		internal static ODataBatchOperationWriteStream CreateBatchOperationWriteStream(Stream outputStream, IODataBatchOperationListener operationListener)
		{
			return new ODataBatchOperationWriteStream(outputStream, operationListener);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0002F820 File Offset: 0x0002DA20
		internal static void EnsureArraySize(ref byte[] buffer, int numberOfBytesInBuffer, int requiredByteCount)
		{
			int num = buffer.Length - numberOfBytesInBuffer;
			if (requiredByteCount <= num)
			{
				return;
			}
			int num2 = requiredByteCount - num;
			byte[] array = buffer;
			buffer = new byte[buffer.Length + num2];
			Buffer.BlockCopy(array, 0, buffer, 0, numberOfBytesInBuffer);
		}
	}
}
