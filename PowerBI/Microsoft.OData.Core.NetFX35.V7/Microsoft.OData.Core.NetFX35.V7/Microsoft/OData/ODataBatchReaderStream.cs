using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData
{
	// Token: 0x02000037 RID: 55
	internal sealed class ODataBatchReaderStream
	{
		// Token: 0x0600019B RID: 411 RVA: 0x000067D0 File Offset: 0x000049D0
		internal ODataBatchReaderStream(ODataRawInputContext inputContext, string batchBoundary, Encoding batchEncoding)
		{
			this.inputContext = inputContext;
			this.batchBoundary = batchBoundary;
			this.batchEncoding = batchEncoding;
			this.batchBuffer = new ODataBatchReaderStreamBuffer();
			this.lineBuffer = new byte[2000];
			this.mediaTypeResolver = ODataMediaTypeResolver.GetMediaTypeResolver(inputContext.Container);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00006824 File Offset: 0x00004A24
		internal string BatchBoundary
		{
			get
			{
				return this.batchBoundary;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000682C File Offset: 0x00004A2C
		internal string ChangeSetBoundary
		{
			get
			{
				return this.changesetBoundary;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00006834 File Offset: 0x00004A34
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

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00006851 File Offset: 0x00004A51
		private Encoding CurrentEncoding
		{
			get
			{
				return this.changesetEncoding ?? this.batchEncoding;
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006863 File Offset: 0x00004A63
		internal void ResetChangeSetBoundary()
		{
			this.changesetBoundary = null;
			this.changesetEncoding = null;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006874 File Offset: 0x00004A74
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

		// Token: 0x060001A2 RID: 418 RVA: 0x000069A0 File Offset: 0x00004BA0
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

		// Token: 0x060001A3 RID: 419 RVA: 0x00006BD0 File Offset: 0x00004DD0
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

		// Token: 0x060001A4 RID: 420 RVA: 0x00006CA4 File Offset: 0x00004EA4
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

		// Token: 0x060001A5 RID: 421 RVA: 0x00006D08 File Offset: 0x00004F08
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

		// Token: 0x060001A6 RID: 422 RVA: 0x00006D60 File Offset: 0x00004F60
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

		// Token: 0x060001A7 RID: 423 RVA: 0x00006D8C File Offset: 0x00004F8C
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

		// Token: 0x060001A8 RID: 424 RVA: 0x00006DD4 File Offset: 0x00004FD4
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

		// Token: 0x060001A9 RID: 425 RVA: 0x00006F97 File Offset: 0x00005197
		private void EnsureBatchEncoding()
		{
			if (this.batchEncoding == null)
			{
				this.batchEncoding = this.DetectEncoding();
			}
			ReaderValidationUtils.ValidateEncodingSupportedInBatch(this.batchEncoding);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00006FB8 File Offset: 0x000051B8
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

		// Token: 0x060001AB RID: 427 RVA: 0x000071C8 File Offset: 0x000053C8
		private ODataBatchOperationHeaders ReadPartHeaders(out bool isChangeSetPart)
		{
			ODataBatchOperationHeaders odataBatchOperationHeaders = this.ReadHeaders();
			return this.ValidatePartHeaders(odataBatchOperationHeaders, out isChangeSetPart);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000071E4 File Offset: 0x000053E4
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

		// Token: 0x060001AD RID: 429 RVA: 0x0000728C File Offset: 0x0000548C
		private void DetermineChangesetBoundaryAndEncoding(string contentType)
		{
			ODataMediaType odataMediaType;
			ODataPayloadKind odataPayloadKind;
			MediaTypeUtils.GetFormatFromContentType(contentType, new ODataPayloadKind[] { ODataPayloadKind.Batch }, this.mediaTypeResolver, out odataMediaType, out this.changesetEncoding, out odataPayloadKind, out this.changesetBoundary);
		}

		// Token: 0x040000F3 RID: 243
		private const int LineBufferLength = 2000;

		// Token: 0x040000F4 RID: 244
		private readonly byte[] lineBuffer;

		// Token: 0x040000F5 RID: 245
		private readonly ODataRawInputContext inputContext;

		// Token: 0x040000F6 RID: 246
		private readonly string batchBoundary;

		// Token: 0x040000F7 RID: 247
		private readonly ODataBatchReaderStreamBuffer batchBuffer;

		// Token: 0x040000F8 RID: 248
		private readonly ODataMediaTypeResolver mediaTypeResolver;

		// Token: 0x040000F9 RID: 249
		private Encoding batchEncoding;

		// Token: 0x040000FA RID: 250
		private string changesetBoundary;

		// Token: 0x040000FB RID: 251
		private Encoding changesetEncoding;

		// Token: 0x040000FC RID: 252
		private bool underlyingStreamExhausted;
	}
}
