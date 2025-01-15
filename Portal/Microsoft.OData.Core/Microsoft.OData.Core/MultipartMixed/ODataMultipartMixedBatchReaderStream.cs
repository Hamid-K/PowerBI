using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.OData.MultipartMixed
{
	// Token: 0x02000207 RID: 519
	internal sealed class ODataMultipartMixedBatchReaderStream : ODataBatchReaderStream
	{
		// Token: 0x060016D3 RID: 5843 RVA: 0x00040038 File Offset: 0x0003E238
		internal ODataMultipartMixedBatchReaderStream(ODataMultipartMixedBatchInputContext inputContext, string batchBoundary, Encoding batchEncoding)
		{
			this.batchEncoding = batchEncoding;
			this.multipartMixedBatchInputContext = inputContext;
			this.batchBoundary = batchBoundary;
			this.lineBuffer = new byte[2000];
			this.mediaTypeResolver = ODataMediaTypeResolver.GetMediaTypeResolver(inputContext.Container);
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x00040076 File Offset: 0x0003E276
		internal string BatchBoundary
		{
			get
			{
				return this.batchBoundary;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x0004007E File Offset: 0x0003E27E
		internal string ChangeSetBoundary
		{
			get
			{
				return this.changesetBoundary;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x00040086 File Offset: 0x0003E286
		private Encoding CurrentEncoding
		{
			get
			{
				return this.changesetEncoding ?? this.batchEncoding;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x00040098 File Offset: 0x0003E298
		private IEnumerable<string> CurrentBoundaries
		{
			get
			{
				if (this.changesetBoundary != null)
				{
					yield return this.changesetBoundary;
				}
				yield return this.batchBoundary;
				yield break;
			}
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x000400B5 File Offset: 0x0003E2B5
		internal void ResetChangeSetBoundary()
		{
			this.changesetBoundary = null;
			this.changesetEncoding = null;
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x000400C8 File Offset: 0x0003E2C8
		internal bool SkipToBoundary(out bool isEndBoundary, out bool isParentBoundary)
		{
			this.EnsureBatchEncoding(this.multipartMixedBatchInputContext.Stream);
			ODataBatchReaderStreamScanResult odataBatchReaderStreamScanResult = ODataBatchReaderStreamScanResult.NoMatch;
			while (odataBatchReaderStreamScanResult != ODataBatchReaderStreamScanResult.Match)
			{
				int num;
				int num2;
				odataBatchReaderStreamScanResult = this.BatchBuffer.ScanForBoundary(this.CurrentBoundaries, int.MaxValue, out num, out num2, out isEndBoundary, out isParentBoundary);
				switch (odataBatchReaderStreamScanResult)
				{
				case ODataBatchReaderStreamScanResult.NoMatch:
					if (this.underlyingStreamExhausted)
					{
						this.BatchBuffer.SkipTo(this.BatchBuffer.CurrentReadPosition + this.BatchBuffer.NumberOfBytesInBuffer);
						return false;
					}
					this.underlyingStreamExhausted = this.BatchBuffer.RefillFrom(this.multipartMixedBatchInputContext.Stream, 8000);
					break;
				case ODataBatchReaderStreamScanResult.PartialMatch:
					if (this.underlyingStreamExhausted)
					{
						this.BatchBuffer.SkipTo(this.BatchBuffer.CurrentReadPosition + this.BatchBuffer.NumberOfBytesInBuffer);
						return false;
					}
					this.underlyingStreamExhausted = this.BatchBuffer.RefillFrom(this.multipartMixedBatchInputContext.Stream, num);
					break;
				case ODataBatchReaderStreamScanResult.Match:
					this.BatchBuffer.SkipTo(isParentBoundary ? num : (num2 + 1));
					return true;
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStream_SkipToBoundary));
				}
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStream_SkipToBoundary));
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x000401FC File Offset: 0x0003E3FC
		internal override int ReadWithDelimiter(byte[] userBuffer, int userBufferOffset, int count)
		{
			if (count == 0)
			{
				return 0;
			}
			int num = count;
			ODataBatchReaderStreamScanResult odataBatchReaderStreamScanResult = ODataBatchReaderStreamScanResult.NoMatch;
			while (num > 0 && odataBatchReaderStreamScanResult != ODataBatchReaderStreamScanResult.Match)
			{
				int num2;
				int num3;
				bool flag;
				bool flag2;
				odataBatchReaderStreamScanResult = this.BatchBuffer.ScanForBoundary(this.CurrentBoundaries, num, out num2, out num3, out flag, out flag2);
				switch (odataBatchReaderStreamScanResult)
				{
				case ODataBatchReaderStreamScanResult.NoMatch:
				{
					if (this.BatchBuffer.NumberOfBytesInBuffer >= num)
					{
						Buffer.BlockCopy(this.BatchBuffer.Bytes, this.BatchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, num);
						this.BatchBuffer.SkipTo(this.BatchBuffer.CurrentReadPosition + num);
						return count;
					}
					int numberOfBytesInBuffer = this.BatchBuffer.NumberOfBytesInBuffer;
					Buffer.BlockCopy(this.BatchBuffer.Bytes, this.BatchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, numberOfBytesInBuffer);
					num -= numberOfBytesInBuffer;
					userBufferOffset += numberOfBytesInBuffer;
					if (this.underlyingStreamExhausted)
					{
						this.BatchBuffer.SkipTo(this.BatchBuffer.CurrentReadPosition + numberOfBytesInBuffer);
						return count - num;
					}
					this.underlyingStreamExhausted = this.BatchBuffer.RefillFrom(this.multipartMixedBatchInputContext.Stream, 8000);
					break;
				}
				case ODataBatchReaderStreamScanResult.PartialMatch:
				{
					if (this.underlyingStreamExhausted)
					{
						int num4 = Math.Min(this.BatchBuffer.NumberOfBytesInBuffer, num);
						Buffer.BlockCopy(this.BatchBuffer.Bytes, this.BatchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, num4);
						this.BatchBuffer.SkipTo(this.BatchBuffer.CurrentReadPosition + num4);
						num -= num4;
						return count - num;
					}
					int num5 = num2 - this.BatchBuffer.CurrentReadPosition;
					Buffer.BlockCopy(this.BatchBuffer.Bytes, this.BatchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, num5);
					num -= num5;
					userBufferOffset += num5;
					this.underlyingStreamExhausted = this.BatchBuffer.RefillFrom(this.multipartMixedBatchInputContext.Stream, num2);
					break;
				}
				case ODataBatchReaderStreamScanResult.Match:
				{
					int num5 = num2 - this.BatchBuffer.CurrentReadPosition;
					Buffer.BlockCopy(this.BatchBuffer.Bytes, this.BatchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, num5);
					num -= num5;
					userBufferOffset += num5;
					this.BatchBuffer.SkipTo(num2);
					return count - num;
				}
				}
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStream_ReadWithDelimiter));
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x0004042C File Offset: 0x0003E62C
		internal override int ReadWithLength(byte[] userBuffer, int userBufferOffset, int count)
		{
			int i = count;
			while (i > 0)
			{
				if (this.BatchBuffer.NumberOfBytesInBuffer >= i)
				{
					Buffer.BlockCopy(this.BatchBuffer.Bytes, this.BatchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, i);
					this.BatchBuffer.SkipTo(this.BatchBuffer.CurrentReadPosition + i);
					i = 0;
				}
				else
				{
					int numberOfBytesInBuffer = this.BatchBuffer.NumberOfBytesInBuffer;
					Buffer.BlockCopy(this.BatchBuffer.Bytes, this.BatchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, numberOfBytesInBuffer);
					i -= numberOfBytesInBuffer;
					userBufferOffset += numberOfBytesInBuffer;
					if (this.underlyingStreamExhausted)
					{
						throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStreamBuffer_ReadWithLength));
					}
					this.underlyingStreamExhausted = this.BatchBuffer.RefillFrom(this.multipartMixedBatchInputContext.Stream, 8000);
				}
			}
			return count - i;
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x00040500 File Offset: 0x0003E700
		internal bool ProcessPartHeader(out string contentId)
		{
			bool flag;
			ODataBatchOperationHeaders odataBatchOperationHeaders = this.ReadPartHeaders(out flag);
			contentId = null;
			if (flag)
			{
				this.DetermineChangesetBoundaryAndEncoding(odataBatchOperationHeaders["Content-Type"]);
				if (this.changesetEncoding == null)
				{
					this.changesetEncoding = this.DetectEncoding(this.multipartMixedBatchInputContext.Stream);
				}
				ReaderValidationUtils.ValidateEncodingSupportedInBatch(this.changesetEncoding);
			}
			else
			{
				odataBatchOperationHeaders.TryGetValue("Content-ID", out contentId);
			}
			return flag;
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x00040568 File Offset: 0x0003E768
		internal ODataBatchOperationHeaders ReadHeaders()
		{
			ODataBatchOperationHeaders odataBatchOperationHeaders = new ODataBatchOperationHeaders();
			string text = this.ReadLine();
			while (text != null && text.Length > 0)
			{
				string text2;
				string text3;
				ODataMultipartMixedBatchReaderStream.ValidateHeaderLine(text, out text2, out text3);
				if (odataBatchOperationHeaders.ContainsKeyOrdinal(text2))
				{
					throw new ODataException(Strings.ODataBatchReaderStream_DuplicateHeaderFound(text2));
				}
				odataBatchOperationHeaders.Add(text2, text3);
				text = this.ReadLine();
			}
			return odataBatchOperationHeaders;
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x000405C0 File Offset: 0x0003E7C0
		internal string ReadFirstNonEmptyLine()
		{
			for (;;)
			{
				string text = this.ReadLine();
				if (text == null)
				{
					break;
				}
				if (text.Length != 0)
				{
					return text;
				}
			}
			throw new ODataException(Strings.ODataBatchReaderStream_UnexpectedEndOfInput);
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x000405EC File Offset: 0x0003E7EC
		private static void ValidateHeaderLine(string headerLine, out string headerName, out string headerValue)
		{
			int num = headerLine.IndexOf(':');
			if (num <= 0)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidHeaderSpecified(headerLine));
			}
			headerName = headerLine.Substring(0, num).Trim();
			headerValue = headerLine.Substring(num + 1).Trim();
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x00040634 File Offset: 0x0003E834
		private string ReadLine()
		{
			int num = 0;
			byte[] array = this.lineBuffer;
			ODataBatchReaderStreamScanResult odataBatchReaderStreamScanResult = ODataBatchReaderStreamScanResult.NoMatch;
			while (odataBatchReaderStreamScanResult != ODataBatchReaderStreamScanResult.Match)
			{
				int num2;
				int num3;
				odataBatchReaderStreamScanResult = this.BatchBuffer.ScanForLineEnd(out num2, out num3);
				switch (odataBatchReaderStreamScanResult)
				{
				case ODataBatchReaderStreamScanResult.NoMatch:
				{
					int num4 = this.BatchBuffer.NumberOfBytesInBuffer;
					if (num4 > 0)
					{
						ODataBatchUtils.EnsureArraySize(ref array, num, num4);
						Buffer.BlockCopy(this.BatchBuffer.Bytes, this.BatchBuffer.CurrentReadPosition, array, num, num4);
						num += num4;
					}
					if (this.underlyingStreamExhausted)
					{
						if (num == 0)
						{
							return null;
						}
						odataBatchReaderStreamScanResult = ODataBatchReaderStreamScanResult.Match;
						this.BatchBuffer.SkipTo(this.BatchBuffer.CurrentReadPosition + num4);
					}
					else
					{
						this.underlyingStreamExhausted = this.BatchBuffer.RefillFrom(this.multipartMixedBatchInputContext.Stream, 8000);
					}
					break;
				}
				case ODataBatchReaderStreamScanResult.PartialMatch:
				{
					int num4 = num2 - this.BatchBuffer.CurrentReadPosition;
					if (num4 > 0)
					{
						ODataBatchUtils.EnsureArraySize(ref array, num, num4);
						Buffer.BlockCopy(this.BatchBuffer.Bytes, this.BatchBuffer.CurrentReadPosition, array, num, num4);
						num += num4;
					}
					if (this.underlyingStreamExhausted)
					{
						odataBatchReaderStreamScanResult = ODataBatchReaderStreamScanResult.Match;
						this.BatchBuffer.SkipTo(num2 + 1);
					}
					else
					{
						this.underlyingStreamExhausted = this.BatchBuffer.RefillFrom(this.multipartMixedBatchInputContext.Stream, num2);
					}
					break;
				}
				case ODataBatchReaderStreamScanResult.Match:
				{
					int num4 = num2 - this.BatchBuffer.CurrentReadPosition;
					if (num4 > 0)
					{
						ODataBatchUtils.EnsureArraySize(ref array, num, num4);
						Buffer.BlockCopy(this.BatchBuffer.Bytes, this.BatchBuffer.CurrentReadPosition, array, num, num4);
						num += num4;
					}
					this.BatchBuffer.SkipTo(num3 + 1);
					break;
				}
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStream_ReadLine));
				}
			}
			return this.CurrentEncoding.GetString(array, 0, num);
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x000407F8 File Offset: 0x0003E9F8
		private ODataBatchOperationHeaders ReadPartHeaders(out bool isChangeSetPart)
		{
			ODataBatchOperationHeaders odataBatchOperationHeaders = this.ReadHeaders();
			return this.ValidatePartHeaders(odataBatchOperationHeaders, out isChangeSetPart);
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00040814 File Offset: 0x0003EA14
		private ODataBatchOperationHeaders ValidatePartHeaders(ODataBatchOperationHeaders headers, out bool isChangeSetPart)
		{
			string text;
			if (!headers.TryGetValue("Content-Type", out text))
			{
				throw new ODataException(Strings.ODataBatchReaderStream_MissingContentTypeHeader);
			}
			if (MediaTypeUtils.MediaTypeAndSubtypeAreEqual(text, "application/http"))
			{
				isChangeSetPart = false;
				string text2;
				if (!headers.TryGetValue("Content-Transfer-Encoding", out text2) || string.Compare(text2, "binary", StringComparison.OrdinalIgnoreCase) != 0)
				{
					throw new ODataException(Strings.ODataBatchReaderStream_MissingOrInvalidContentEncodingHeader("Content-Transfer-Encoding", "binary"));
				}
			}
			else
			{
				if (!MediaTypeUtils.MediaTypeStartsWithTypeAndSubtype(text, "multipart/mixed"))
				{
					throw new ODataException(Strings.ODataBatchReaderStream_InvalidContentTypeSpecified("Content-Type", text, "multipart/mixed", "application/http"));
				}
				isChangeSetPart = true;
				if (this.changesetBoundary != null)
				{
					throw new ODataException(Strings.ODataBatchReaderStream_NestedChangesetsAreNotSupported);
				}
			}
			return headers;
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x000408BC File Offset: 0x0003EABC
		private void DetermineChangesetBoundaryAndEncoding(string contentType)
		{
			ODataMediaType odataMediaType;
			ODataPayloadKind odataPayloadKind;
			MediaTypeUtils.GetFormatFromContentType(contentType, new ODataPayloadKind[] { ODataPayloadKind.Batch }, this.mediaTypeResolver, out odataMediaType, out this.changesetEncoding, out odataPayloadKind);
			this.changesetBoundary = ODataMultipartMixedBatchWriterUtils.GetBatchBoundaryFromMediaType(odataMediaType);
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x000408F7 File Offset: 0x0003EAF7
		private void EnsureBatchEncoding(Stream stream)
		{
			if (this.batchEncoding == null)
			{
				this.batchEncoding = this.DetectEncoding(stream);
			}
			ReaderValidationUtils.ValidateEncodingSupportedInBatch(this.batchEncoding);
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0004091C File Offset: 0x0003EB1C
		private Encoding DetectEncoding(Stream stream)
		{
			while (!this.underlyingStreamExhausted && this.BatchBuffer.NumberOfBytesInBuffer < 4)
			{
				this.underlyingStreamExhausted = this.BatchBuffer.RefillFrom(stream, this.BatchBuffer.CurrentReadPosition);
			}
			int numberOfBytesInBuffer = this.BatchBuffer.NumberOfBytesInBuffer;
			if (numberOfBytesInBuffer < 2)
			{
				return MediaTypeUtils.FallbackEncoding;
			}
			if (this.BatchBuffer[this.BatchBuffer.CurrentReadPosition] == 254 && this.BatchBuffer[this.BatchBuffer.CurrentReadPosition + 1] == 255)
			{
				return new UnicodeEncoding(true, true);
			}
			if (this.BatchBuffer[this.BatchBuffer.CurrentReadPosition] == 255 && this.BatchBuffer[this.BatchBuffer.CurrentReadPosition + 1] == 254)
			{
				if (numberOfBytesInBuffer >= 4 && this.BatchBuffer[this.BatchBuffer.CurrentReadPosition + 2] == 0 && this.BatchBuffer[this.BatchBuffer.CurrentReadPosition + 3] == 0)
				{
					throw Error.NotSupported();
				}
				return new UnicodeEncoding(false, true);
			}
			else
			{
				if (numberOfBytesInBuffer >= 3 && this.BatchBuffer[this.BatchBuffer.CurrentReadPosition] == 239 && this.BatchBuffer[this.BatchBuffer.CurrentReadPosition + 1] == 187 && this.BatchBuffer[this.BatchBuffer.CurrentReadPosition + 2] == 191)
				{
					return Encoding.UTF8;
				}
				if (numberOfBytesInBuffer >= 4 && this.BatchBuffer[this.BatchBuffer.CurrentReadPosition] == 0 && this.BatchBuffer[this.BatchBuffer.CurrentReadPosition + 1] == 0 && this.BatchBuffer[this.BatchBuffer.CurrentReadPosition + 2] == 254 && this.BatchBuffer[this.BatchBuffer.CurrentReadPosition + 3] == 255)
				{
					throw Error.NotSupported();
				}
				return MediaTypeUtils.FallbackEncoding;
			}
		}

		// Token: 0x04000A50 RID: 2640
		private const int LineBufferLength = 2000;

		// Token: 0x04000A51 RID: 2641
		private readonly byte[] lineBuffer;

		// Token: 0x04000A52 RID: 2642
		private readonly string batchBoundary;

		// Token: 0x04000A53 RID: 2643
		private readonly ODataMediaTypeResolver mediaTypeResolver;

		// Token: 0x04000A54 RID: 2644
		private Encoding batchEncoding;

		// Token: 0x04000A55 RID: 2645
		private Encoding changesetEncoding;

		// Token: 0x04000A56 RID: 2646
		private string changesetBoundary;

		// Token: 0x04000A57 RID: 2647
		private ODataMultipartMixedBatchInputContext multipartMixedBatchInputContext;
	}
}
