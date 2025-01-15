using System;
using System.Globalization;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x020001D8 RID: 472
	internal static class ODataBatchUtils
	{
		// Token: 0x06000DD3 RID: 3539 RVA: 0x00030CA4 File Offset: 0x0002EEA4
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
					string text = (UriUtilsCommon.UriToString(uri).StartsWith("$", 4) ? Strings.ODataBatchUtils_RelativeUriStartingWithDollarUsedWithoutBaseUriSpecified(UriUtilsCommon.UriToString(uri)) : Strings.ODataBatchUtils_RelativeUriUsedWithoutBaseUriSpecified(UriUtilsCommon.UriToString(uri)));
					throw new ODataException(text);
				}
				uri2 = UriUtils.UriToAbsoluteUri(baseUri, uri);
			}
			return uri2;
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00030D1C File Offset: 0x0002EF1C
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

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00030D64 File Offset: 0x0002EF64
		internal static ODataBatchOperationWriteStream CreateBatchOperationWriteStream(Stream outputStream, IODataBatchOperationListener operationListener)
		{
			return new ODataBatchOperationWriteStream(outputStream, operationListener);
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00030D70 File Offset: 0x0002EF70
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
