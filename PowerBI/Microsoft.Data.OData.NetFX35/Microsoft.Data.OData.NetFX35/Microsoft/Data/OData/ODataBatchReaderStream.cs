using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Data.OData
{
	// Token: 0x020001DB RID: 475
	internal sealed class ODataBatchReaderStream
	{
		// Token: 0x06000DE5 RID: 3557 RVA: 0x000312E3 File Offset: 0x0002F4E3
		internal ODataBatchReaderStream(ODataRawInputContext inputContext, string batchBoundary, Encoding batchEncoding)
		{
			this.inputContext = inputContext;
			this.batchBoundary = batchBoundary;
			this.batchEncoding = batchEncoding;
			this.batchBuffer = new ODataBatchReaderStreamBuffer();
			this.lineBuffer = new byte[2000];
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x0003131B File Offset: 0x0002F51B
		internal string BatchBoundary
		{
			get
			{
				return this.batchBoundary;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x00031323 File Offset: 0x0002F523
		internal string ChangeSetBoundary
		{
			get
			{
				return this.changesetBoundary;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00031434 File Offset: 0x0002F634
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

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00031451 File Offset: 0x0002F651
		private Encoding CurrentEncoding
		{
			get
			{
				return this.changesetEncoding ?? this.batchEncoding;
			}
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00031463 File Offset: 0x0002F663
		internal void ResetChangeSetBoundary()
		{
			this.changesetBoundary = null;
			this.changesetEncoding = null;
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00031474 File Offset: 0x0002F674
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

		// Token: 0x06000DEC RID: 3564 RVA: 0x000315A0 File Offset: 0x0002F7A0
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

		// Token: 0x06000DED RID: 3565 RVA: 0x000317D4 File Offset: 0x0002F9D4
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

		// Token: 0x06000DEE RID: 3566 RVA: 0x000318A8 File Offset: 0x0002FAA8
		internal bool ProcessPartHeader()
		{
			bool flag;
			ODataBatchOperationHeaders odataBatchOperationHeaders = this.ReadPartHeaders(out flag);
			if (flag)
			{
				this.DetermineChangesetBoundaryAndEncoding(odataBatchOperationHeaders["Content-Type"]);
				if (this.changesetEncoding == null)
				{
					this.changesetEncoding = this.DetectEncoding();
				}
				ReaderValidationUtils.ValidateEncodingSupportedInBatch(this.changesetEncoding);
			}
			return flag;
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x000318F4 File Offset: 0x0002FAF4
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

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0003194C File Offset: 0x0002FB4C
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

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00031978 File Offset: 0x0002FB78
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

		// Token: 0x06000DF2 RID: 3570 RVA: 0x000319C0 File Offset: 0x0002FBC0
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

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00031B87 File Offset: 0x0002FD87
		private void EnsureBatchEncoding()
		{
			if (this.batchEncoding == null)
			{
				this.batchEncoding = this.DetectEncoding();
			}
			ReaderValidationUtils.ValidateEncodingSupportedInBatch(this.batchEncoding);
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00031BA8 File Offset: 0x0002FDA8
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

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00031DB8 File Offset: 0x0002FFB8
		private ODataBatchOperationHeaders ReadPartHeaders(out bool isChangeSetPart)
		{
			ODataBatchOperationHeaders odataBatchOperationHeaders = this.ReadHeaders();
			return this.ValidatePartHeaders(odataBatchOperationHeaders, out isChangeSetPart);
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00031DD4 File Offset: 0x0002FFD4
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

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00031E7C File Offset: 0x0003007C
		private void DetermineChangesetBoundaryAndEncoding(string contentType)
		{
			MediaType mediaType;
			ODataPayloadKind odataPayloadKind;
			MediaTypeUtils.GetFormatFromContentType(contentType, new ODataPayloadKind[] { ODataPayloadKind.Batch }, MediaTypeResolver.DefaultMediaTypeResolver, out mediaType, out this.changesetEncoding, out odataPayloadKind, out this.changesetBoundary);
		}

		// Token: 0x0400050D RID: 1293
		private const int LineBufferLength = 2000;

		// Token: 0x0400050E RID: 1294
		private readonly byte[] lineBuffer;

		// Token: 0x0400050F RID: 1295
		private readonly ODataRawInputContext inputContext;

		// Token: 0x04000510 RID: 1296
		private readonly string batchBoundary;

		// Token: 0x04000511 RID: 1297
		private readonly ODataBatchReaderStreamBuffer batchBuffer;

		// Token: 0x04000512 RID: 1298
		private Encoding batchEncoding;

		// Token: 0x04000513 RID: 1299
		private string changesetBoundary;

		// Token: 0x04000514 RID: 1300
		private Encoding changesetEncoding;

		// Token: 0x04000515 RID: 1301
		private bool underlyingStreamExhausted;
	}
}
