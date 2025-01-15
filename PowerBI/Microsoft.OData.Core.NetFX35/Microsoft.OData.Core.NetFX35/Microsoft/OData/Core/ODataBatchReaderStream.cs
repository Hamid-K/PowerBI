using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData.Core
{
	// Token: 0x02000149 RID: 329
	internal sealed class ODataBatchReaderStream
	{
		// Token: 0x06000C73 RID: 3187 RVA: 0x0002E538 File Offset: 0x0002C738
		internal ODataBatchReaderStream(ODataRawInputContext inputContext, string batchBoundary, Encoding batchEncoding)
		{
			this.inputContext = inputContext;
			this.batchBoundary = batchBoundary;
			this.batchEncoding = batchEncoding;
			this.batchBuffer = new ODataBatchReaderStreamBuffer();
			this.lineBuffer = new byte[2000];
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0002E570 File Offset: 0x0002C770
		internal string BatchBoundary
		{
			get
			{
				return this.batchBoundary;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0002E578 File Offset: 0x0002C778
		internal string ChangeSetBoundary
		{
			get
			{
				return this.changesetBoundary;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0002E688 File Offset: 0x0002C888
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

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0002E6A5 File Offset: 0x0002C8A5
		private Encoding CurrentEncoding
		{
			get
			{
				return this.changesetEncoding ?? this.batchEncoding;
			}
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0002E6B7 File Offset: 0x0002C8B7
		internal void ResetChangeSetBoundary()
		{
			this.changesetBoundary = null;
			this.changesetEncoding = null;
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0002E6C8 File Offset: 0x0002C8C8
		internal bool SkipToBoundary(out bool isEndBoundary, out bool isParentBoundary)
		{
			this.EnsureBatchEncoding();
			ODataBatchReaderStreamScanResult odataBatchReaderStreamScanResult = ODataBatchReaderStreamScanResult.NoMatch;
			while (odataBatchReaderStreamScanResult != ODataBatchReaderStreamScanResult.Match)
			{
				int num;
				int num2;
				odataBatchReaderStreamScanResult = this.batchBuffer.ScanForBoundary(this.CurrentBoundaries, int.MaxValue, out num, out num2, out isEndBoundary, out isParentBoundary);
				switch (odataBatchReaderStreamScanResult)
				{
				case ODataBatchReaderStreamScanResult.NoMatch:
					if (this.underlyingStreamExhausted)
					{
						this.batchBuffer.SkipTo(this.batchBuffer.CurrentReadPosition + this.batchBuffer.NumberOfBytesInBuffer);
						return false;
					}
					this.underlyingStreamExhausted = this.batchBuffer.RefillFrom(this.inputContext.Stream, 8000);
					break;
				case ODataBatchReaderStreamScanResult.PartialMatch:
					if (this.underlyingStreamExhausted)
					{
						this.batchBuffer.SkipTo(this.batchBuffer.CurrentReadPosition + this.batchBuffer.NumberOfBytesInBuffer);
						return false;
					}
					this.underlyingStreamExhausted = this.batchBuffer.RefillFrom(this.inputContext.Stream, num);
					break;
				case ODataBatchReaderStreamScanResult.Match:
					this.batchBuffer.SkipTo(isParentBoundary ? num : (num2 + 1));
					return true;
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStream_SkipToBoundary));
				}
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStream_SkipToBoundary));
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0002E7F4 File Offset: 0x0002C9F4
		internal int ReadWithDelimiter(byte[] userBuffer, int userBufferOffset, int count)
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
				odataBatchReaderStreamScanResult = this.batchBuffer.ScanForBoundary(this.CurrentBoundaries, num, out num2, out num3, out flag, out flag2);
				switch (odataBatchReaderStreamScanResult)
				{
				case ODataBatchReaderStreamScanResult.NoMatch:
				{
					if (this.batchBuffer.NumberOfBytesInBuffer >= num)
					{
						Buffer.BlockCopy(this.batchBuffer.Bytes, this.batchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, num);
						this.batchBuffer.SkipTo(this.batchBuffer.CurrentReadPosition + num);
						return count;
					}
					int numberOfBytesInBuffer = this.batchBuffer.NumberOfBytesInBuffer;
					Buffer.BlockCopy(this.batchBuffer.Bytes, this.batchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, numberOfBytesInBuffer);
					num -= numberOfBytesInBuffer;
					userBufferOffset += numberOfBytesInBuffer;
					if (this.underlyingStreamExhausted)
					{
						this.batchBuffer.SkipTo(this.batchBuffer.CurrentReadPosition + numberOfBytesInBuffer);
						return count - num;
					}
					this.underlyingStreamExhausted = this.batchBuffer.RefillFrom(this.inputContext.Stream, 8000);
					break;
				}
				case ODataBatchReaderStreamScanResult.PartialMatch:
				{
					if (this.underlyingStreamExhausted)
					{
						int num4 = Math.Min(this.batchBuffer.NumberOfBytesInBuffer, num);
						Buffer.BlockCopy(this.batchBuffer.Bytes, this.batchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, num4);
						this.batchBuffer.SkipTo(this.batchBuffer.CurrentReadPosition + num4);
						num -= num4;
						return count - num;
					}
					int num5 = num2 - this.batchBuffer.CurrentReadPosition;
					Buffer.BlockCopy(this.batchBuffer.Bytes, this.batchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, num5);
					num -= num5;
					userBufferOffset += num5;
					this.underlyingStreamExhausted = this.batchBuffer.RefillFrom(this.inputContext.Stream, num2);
					break;
				}
				case ODataBatchReaderStreamScanResult.Match:
				{
					int num5 = num2 - this.batchBuffer.CurrentReadPosition;
					Buffer.BlockCopy(this.batchBuffer.Bytes, this.batchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, num5);
					num -= num5;
					userBufferOffset += num5;
					this.batchBuffer.SkipTo(num2);
					return count - num;
				}
				}
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStream_ReadWithDelimiter));
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0002EA28 File Offset: 0x0002CC28
		internal int ReadWithLength(byte[] userBuffer, int userBufferOffset, int count)
		{
			int i = count;
			while (i > 0)
			{
				if (this.batchBuffer.NumberOfBytesInBuffer >= i)
				{
					Buffer.BlockCopy(this.batchBuffer.Bytes, this.batchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, i);
					this.batchBuffer.SkipTo(this.batchBuffer.CurrentReadPosition + i);
					i = 0;
				}
				else
				{
					int numberOfBytesInBuffer = this.batchBuffer.NumberOfBytesInBuffer;
					Buffer.BlockCopy(this.batchBuffer.Bytes, this.batchBuffer.CurrentReadPosition, userBuffer, userBufferOffset, numberOfBytesInBuffer);
					i -= numberOfBytesInBuffer;
					userBufferOffset += numberOfBytesInBuffer;
					if (this.underlyingStreamExhausted)
					{
						throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStreamBuffer_ReadWithLength));
					}
					this.underlyingStreamExhausted = this.batchBuffer.RefillFrom(this.inputContext.Stream, 8000);
				}
			}
			return count - i;
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0002EAFC File Offset: 0x0002CCFC
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
					this.changesetEncoding = this.DetectEncoding();
				}
				ReaderValidationUtils.ValidateEncodingSupportedInBatch(this.changesetEncoding);
			}
			else if (this.ChangeSetBoundary != null)
			{
				odataBatchOperationHeaders.TryGetValue("Content-ID", out contentId);
			}
			return flag;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0002EB60 File Offset: 0x0002CD60
		internal ODataBatchOperationHeaders ReadHeaders()
		{
			ODataBatchOperationHeaders odataBatchOperationHeaders = new ODataBatchOperationHeaders();
			string text = this.ReadLine();
			while (text != null && text.Length > 0)
			{
				string text2;
				string text3;
				ODataBatchReaderStream.ValidateHeaderLine(text, out text2, out text3);
				if (odataBatchOperationHeaders.ContainsKeyOrdinal(text2))
				{
					throw new ODataException(Strings.ODataBatchReaderStream_DuplicateHeaderFound(text2));
				}
				odataBatchOperationHeaders.Add(text2, text3);
				text = this.ReadLine();
			}
			return odataBatchOperationHeaders;
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0002EBB8 File Offset: 0x0002CDB8
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

		// Token: 0x06000C7F RID: 3199 RVA: 0x0002EBE4 File Offset: 0x0002CDE4
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

		// Token: 0x06000C80 RID: 3200 RVA: 0x0002EC2C File Offset: 0x0002CE2C
		private string ReadLine()
		{
			int num = 0;
			byte[] array = this.lineBuffer;
			ODataBatchReaderStreamScanResult odataBatchReaderStreamScanResult = ODataBatchReaderStreamScanResult.NoMatch;
			while (odataBatchReaderStreamScanResult != ODataBatchReaderStreamScanResult.Match)
			{
				int num2;
				int num3;
				odataBatchReaderStreamScanResult = this.batchBuffer.ScanForLineEnd(out num2, out num3);
				switch (odataBatchReaderStreamScanResult)
				{
				case ODataBatchReaderStreamScanResult.NoMatch:
				{
					int num4 = this.batchBuffer.NumberOfBytesInBuffer;
					if (num4 > 0)
					{
						ODataBatchUtils.EnsureArraySize(ref array, num, num4);
						Buffer.BlockCopy(this.batchBuffer.Bytes, this.batchBuffer.CurrentReadPosition, array, num, num4);
						num += num4;
					}
					if (this.underlyingStreamExhausted)
					{
						if (num == 0)
						{
							return null;
						}
						odataBatchReaderStreamScanResult = ODataBatchReaderStreamScanResult.Match;
						this.batchBuffer.SkipTo(this.batchBuffer.CurrentReadPosition + num4);
					}
					else
					{
						this.underlyingStreamExhausted = this.batchBuffer.RefillFrom(this.inputContext.Stream, 8000);
					}
					break;
				}
				case ODataBatchReaderStreamScanResult.PartialMatch:
				{
					int num4 = num2 - this.batchBuffer.CurrentReadPosition;
					if (num4 > 0)
					{
						ODataBatchUtils.EnsureArraySize(ref array, num, num4);
						Buffer.BlockCopy(this.batchBuffer.Bytes, this.batchBuffer.CurrentReadPosition, array, num, num4);
						num += num4;
					}
					if (this.underlyingStreamExhausted)
					{
						odataBatchReaderStreamScanResult = ODataBatchReaderStreamScanResult.Match;
						this.batchBuffer.SkipTo(num2 + 1);
					}
					else
					{
						this.underlyingStreamExhausted = this.batchBuffer.RefillFrom(this.inputContext.Stream, num2);
					}
					break;
				}
				case ODataBatchReaderStreamScanResult.Match:
				{
					int num4 = num2 - this.batchBuffer.CurrentReadPosition;
					if (num4 > 0)
					{
						ODataBatchUtils.EnsureArraySize(ref array, num, num4);
						Buffer.BlockCopy(this.batchBuffer.Bytes, this.batchBuffer.CurrentReadPosition, array, num, num4);
						num += num4;
					}
					this.batchBuffer.SkipTo(num3 + 1);
					break;
				}
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStream_ReadLine));
				}
			}
			return this.CurrentEncoding.GetString(array, 0, num);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0002EDF3 File Offset: 0x0002CFF3
		private void EnsureBatchEncoding()
		{
			if (this.batchEncoding == null)
			{
				this.batchEncoding = this.DetectEncoding();
			}
			ReaderValidationUtils.ValidateEncodingSupportedInBatch(this.batchEncoding);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0002EE14 File Offset: 0x0002D014
		private Encoding DetectEncoding()
		{
			while (!this.underlyingStreamExhausted && this.batchBuffer.NumberOfBytesInBuffer < 4)
			{
				this.underlyingStreamExhausted = this.batchBuffer.RefillFrom(this.inputContext.Stream, this.batchBuffer.CurrentReadPosition);
			}
			int numberOfBytesInBuffer = this.batchBuffer.NumberOfBytesInBuffer;
			if (numberOfBytesInBuffer < 2)
			{
				return Encoding.ASCII;
			}
			if (this.batchBuffer[this.batchBuffer.CurrentReadPosition] == 254 && this.batchBuffer[this.batchBuffer.CurrentReadPosition + 1] == 255)
			{
				return new UnicodeEncoding(true, true);
			}
			if (this.batchBuffer[this.batchBuffer.CurrentReadPosition] == 255 && this.batchBuffer[this.batchBuffer.CurrentReadPosition + 1] == 254)
			{
				if (numberOfBytesInBuffer >= 4 && this.batchBuffer[this.batchBuffer.CurrentReadPosition + 2] == 0 && this.batchBuffer[this.batchBuffer.CurrentReadPosition + 3] == 0)
				{
					return new UTF32Encoding(false, true);
				}
				return new UnicodeEncoding(false, true);
			}
			else
			{
				if (numberOfBytesInBuffer >= 3 && this.batchBuffer[this.batchBuffer.CurrentReadPosition] == 239 && this.batchBuffer[this.batchBuffer.CurrentReadPosition + 1] == 187 && this.batchBuffer[this.batchBuffer.CurrentReadPosition + 2] == 191)
				{
					return Encoding.UTF8;
				}
				if (numberOfBytesInBuffer >= 4 && this.batchBuffer[this.batchBuffer.CurrentReadPosition] == 0 && this.batchBuffer[this.batchBuffer.CurrentReadPosition + 1] == 0 && this.batchBuffer[this.batchBuffer.CurrentReadPosition + 2] == 254 && this.batchBuffer[this.batchBuffer.CurrentReadPosition + 3] == 255)
				{
					return new UTF32Encoding(true, true);
				}
				return Encoding.ASCII;
			}
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0002F024 File Offset: 0x0002D224
		private ODataBatchOperationHeaders ReadPartHeaders(out bool isChangeSetPart)
		{
			ODataBatchOperationHeaders odataBatchOperationHeaders = this.ReadHeaders();
			return this.ValidatePartHeaders(odataBatchOperationHeaders, out isChangeSetPart);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0002F040 File Offset: 0x0002D240
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
				if (!headers.TryGetValue("Content-Transfer-Encoding", out text2) || string.Compare(text2, "binary", 5) != 0)
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

		// Token: 0x06000C85 RID: 3205 RVA: 0x0002F0E8 File Offset: 0x0002D2E8
		private void DetermineChangesetBoundaryAndEncoding(string contentType)
		{
			ODataMediaType odataMediaType;
			ODataPayloadKind odataPayloadKind;
			MediaTypeUtils.GetFormatFromContentType(contentType, new ODataPayloadKind[] { ODataPayloadKind.Batch }, ODataMediaTypeResolver.DefaultMediaTypeResolver, out odataMediaType, out this.changesetEncoding, out odataPayloadKind, out this.changesetBoundary);
		}

		// Token: 0x0400052B RID: 1323
		private const int LineBufferLength = 2000;

		// Token: 0x0400052C RID: 1324
		private readonly byte[] lineBuffer;

		// Token: 0x0400052D RID: 1325
		private readonly ODataRawInputContext inputContext;

		// Token: 0x0400052E RID: 1326
		private readonly string batchBoundary;

		// Token: 0x0400052F RID: 1327
		private readonly ODataBatchReaderStreamBuffer batchBuffer;

		// Token: 0x04000530 RID: 1328
		private Encoding batchEncoding;

		// Token: 0x04000531 RID: 1329
		private string changesetBoundary;

		// Token: 0x04000532 RID: 1330
		private Encoding changesetEncoding;

		// Token: 0x04000533 RID: 1331
		private bool underlyingStreamExhausted;
	}
}
