using System;
using System.Globalization;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x0200003B RID: 59
	internal static class ODataBatchUtils
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x000078EC File Offset: 0x00005AEC
		internal static Uri CreateOperationRequestUri(Uri uri, Uri baseUri, IODataPayloadUriConverter payloadUriConverter)
		{
			Uri uri2;
			if (payloadUriConverter != null)
			{
				uri2 = payloadUriConverter.ConvertPayloadUri(baseUri, uri);
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

		// Token: 0x060001C3 RID: 451 RVA: 0x00007964 File Offset: 0x00005B64
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

		// Token: 0x060001C4 RID: 452 RVA: 0x000079AC File Offset: 0x00005BAC
		internal static ODataBatchOperationWriteStream CreateBatchOperationWriteStream(Stream outputStream, IODataBatchOperationListener operationListener)
		{
			return new ODataBatchOperationWriteStream(outputStream, operationListener);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000079B8 File Offset: 0x00005BB8
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
