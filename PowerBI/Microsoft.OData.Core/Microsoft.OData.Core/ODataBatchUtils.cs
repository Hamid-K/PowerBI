using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Microsoft.OData
{
	// Token: 0x0200005F RID: 95
	internal static class ODataBatchUtils
	{
		// Token: 0x0600031F RID: 799 RVA: 0x00009608 File Offset: 0x00007808
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
					string text = (UriUtils.UriToString(uri).StartsWith("$", StringComparison.Ordinal) ? Strings.ODataBatchUtils_RelativeUriStartingWithDollarUsedWithoutBaseUriSpecified(UriUtils.UriToString(uri)) : Strings.ODataBatchUtils_RelativeUriUsedWithoutBaseUriSpecified(UriUtils.UriToString(uri)));
					throw new ODataException(text);
				}
				uri2 = UriUtils.UriToAbsoluteUri(baseUri, uri);
			}
			return uri2;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00009680 File Offset: 0x00007880
		internal static ODataReadStream CreateBatchOperationReadStream(ODataBatchReaderStream batchReaderStream, ODataBatchOperationHeaders headers, IODataStreamListener operationListener)
		{
			string text;
			if (!headers.TryGetValue("Content-Length", out text))
			{
				return ODataReadStream.Create(batchReaderStream, operationListener);
			}
			int num = int.Parse(text, CultureInfo.InvariantCulture);
			if (num < 0)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidContentLengthSpecified(text));
			}
			return ODataReadStream.Create(batchReaderStream, operationListener, num);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000096C8 File Offset: 0x000078C8
		internal static ODataWriteStream CreateBatchOperationWriteStream(Stream outputStream, IODataStreamListener operationListener)
		{
			return new ODataWriteStream(outputStream, operationListener);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000096D4 File Offset: 0x000078D4
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

		// Token: 0x06000323 RID: 803 RVA: 0x0000970C File Offset: 0x0000790C
		internal static void ValidateReferenceUri(Uri uri, IEnumerable<string> dependsOnRequestIds, Uri baseUri)
		{
			if (UriUtils.UriToString(uri).IndexOf('$') == -1)
			{
				return;
			}
			string text2;
			if (uri.IsAbsoluteUri)
			{
				if (baseUri == null)
				{
					return;
				}
				string text = UriUtils.UriToString(baseUri);
				if (!uri.AbsoluteUri.StartsWith(text, StringComparison.Ordinal))
				{
					return;
				}
				text2 = uri.AbsoluteUri.Substring(text.Length);
			}
			else
			{
				text2 = UriUtils.UriToString(uri);
			}
			while (text2.StartsWith("/", StringComparison.Ordinal))
			{
				text2 = text2.Substring(1);
			}
			if (text2.Length > 0 && text2[0] == '$')
			{
				int num = text2.IndexOf('/', 1);
				string text3 = ((num > 0) ? text2.Substring(1, num - 1) : text2.Substring(1));
				if (dependsOnRequestIds == null || !dependsOnRequestIds.Contains(text3))
				{
					throw new ODataException(Strings.ODataBatchReader_ReferenceIdNotIncludedInDependsOn(text3, UriUtils.UriToString(uri), (dependsOnRequestIds != null) ? string.Join(",", dependsOnRequestIds.ToArray<string>()) : "null"));
				}
			}
		}
	}
}
