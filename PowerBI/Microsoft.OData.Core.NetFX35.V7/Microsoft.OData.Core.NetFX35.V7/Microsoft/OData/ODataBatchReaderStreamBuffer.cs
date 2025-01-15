using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x02000038 RID: 56
	internal sealed class ODataBatchReaderStreamBuffer
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001AE RID: 430 RVA: 0x000072C1 File Offset: 0x000054C1
		internal byte[] Bytes
		{
			get
			{
				return this.bytes;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001AF RID: 431 RVA: 0x000072C9 File Offset: 0x000054C9
		internal int CurrentReadPosition
		{
			get
			{
				return this.currentReadPosition;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000072D1 File Offset: 0x000054D1
		internal int NumberOfBytesInBuffer
		{
			get
			{
				return this.numberOfBytesInBuffer;
			}
		}

		// Token: 0x1700005C RID: 92
		internal byte this[int index]
		{
			get
			{
				return this.bytes[index];
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000072E4 File Offset: 0x000054E4
		internal void SkipTo(int newPosition)
		{
			int num = newPosition - this.currentReadPosition;
			this.currentReadPosition = newPosition;
			this.numberOfBytesInBuffer -= num;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007310 File Offset: 0x00005510
		internal bool RefillFrom(Stream stream, int preserveFrom)
		{
			this.ShiftToBeginning(preserveFrom);
			int num = 8000 - this.numberOfBytesInBuffer;
			int num2 = stream.Read(this.bytes, this.numberOfBytesInBuffer, num);
			this.numberOfBytesInBuffer += num2;
			return num2 == 0;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00007358 File Offset: 0x00005558
		internal ODataBatchReaderStreamScanResult ScanForLineEnd(out int lineEndStartPosition, out int lineEndEndPosition)
		{
			bool flag;
			return this.ScanForLineEnd(this.currentReadPosition, 8000, false, out lineEndStartPosition, out lineEndEndPosition, out flag);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000737C File Offset: 0x0000557C
		internal ODataBatchReaderStreamScanResult ScanForBoundary(IEnumerable<string> boundaries, int maxDataBytesToScan, out int boundaryStartPosition, out int boundaryEndPosition, out bool isEndBoundary, out bool isParentBoundary)
		{
			boundaryStartPosition = -1;
			boundaryEndPosition = -1;
			isEndBoundary = false;
			isParentBoundary = false;
			int num = this.currentReadPosition;
			int num2;
			int num3;
			for (;;)
			{
				switch (this.ScanForBoundaryStart(num, maxDataBytesToScan, out num2, out num3))
				{
				case ODataBatchReaderStreamScanResult.NoMatch:
					return ODataBatchReaderStreamScanResult.NoMatch;
				case ODataBatchReaderStreamScanResult.PartialMatch:
					goto IL_003C;
				case ODataBatchReaderStreamScanResult.Match:
					isParentBoundary = false;
					foreach (string text in boundaries)
					{
						switch (this.MatchBoundary(num2, num3, text, out boundaryStartPosition, out boundaryEndPosition, out isEndBoundary))
						{
						case ODataBatchReaderStreamScanResult.NoMatch:
							boundaryStartPosition = -1;
							boundaryEndPosition = -1;
							isEndBoundary = false;
							isParentBoundary = true;
							break;
						case ODataBatchReaderStreamScanResult.PartialMatch:
							boundaryEndPosition = -1;
							isEndBoundary = false;
							return ODataBatchReaderStreamScanResult.PartialMatch;
						case ODataBatchReaderStreamScanResult.Match:
							return ODataBatchReaderStreamScanResult.Match;
						default:
							throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStreamBuffer_ScanForBoundary));
						}
					}
					num = ((num == num3) ? (num3 + 1) : num3);
					continue;
				}
				break;
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStreamBuffer_ScanForBoundary));
			IL_003C:
			boundaryStartPosition = ((num2 < 0) ? num3 : num2);
			return ODataBatchReaderStreamScanResult.PartialMatch;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007490 File Offset: 0x00005690
		private ODataBatchReaderStreamScanResult ScanForBoundaryStart(int scanStartIx, int maxDataBytesToScan, out int lineEndStartPosition, out int boundaryDelimiterStartPosition)
		{
			int num = this.currentReadPosition + Math.Min(maxDataBytesToScan, this.numberOfBytesInBuffer) - 1;
			int i = scanStartIx;
			while (i <= num)
			{
				char c = (char)this.bytes[i];
				if (c == '\r' || c == '\n')
				{
					lineEndStartPosition = i;
					if (c == '\r' && i == num && maxDataBytesToScan >= this.numberOfBytesInBuffer)
					{
						boundaryDelimiterStartPosition = i;
						return ODataBatchReaderStreamScanResult.PartialMatch;
					}
					boundaryDelimiterStartPosition = ((c == '\r' && this.bytes[i + 1] == 10) ? (i + 2) : (i + 1));
					return ODataBatchReaderStreamScanResult.Match;
				}
				else
				{
					if (c == '-')
					{
						lineEndStartPosition = -1;
						if (i == num && maxDataBytesToScan >= this.numberOfBytesInBuffer)
						{
							boundaryDelimiterStartPosition = i;
							return ODataBatchReaderStreamScanResult.PartialMatch;
						}
						if (this.bytes[i + 1] == 45)
						{
							boundaryDelimiterStartPosition = i;
							return ODataBatchReaderStreamScanResult.Match;
						}
					}
					i++;
				}
			}
			lineEndStartPosition = -1;
			boundaryDelimiterStartPosition = -1;
			return ODataBatchReaderStreamScanResult.NoMatch;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000754C File Offset: 0x0000574C
		private ODataBatchReaderStreamScanResult ScanForLineEnd(int scanStartIx, int maxDataBytesToScan, bool allowLeadingWhitespaceOnly, out int lineEndStartPosition, out int lineEndEndPosition, out bool endOfBufferReached)
		{
			endOfBufferReached = false;
			int num = this.currentReadPosition + Math.Min(maxDataBytesToScan, this.numberOfBytesInBuffer) - 1;
			int i = scanStartIx;
			while (i <= num)
			{
				char c = (char)this.bytes[i];
				if (c == '\r' || c == '\n')
				{
					lineEndStartPosition = i;
					if (c == '\r' && i == num && maxDataBytesToScan >= this.numberOfBytesInBuffer)
					{
						lineEndEndPosition = -1;
						return ODataBatchReaderStreamScanResult.PartialMatch;
					}
					lineEndEndPosition = lineEndStartPosition;
					if (c == '\r' && this.bytes[i + 1] == 10)
					{
						lineEndEndPosition++;
					}
					return ODataBatchReaderStreamScanResult.Match;
				}
				else
				{
					if (allowLeadingWhitespaceOnly && !char.IsWhiteSpace(c))
					{
						lineEndStartPosition = -1;
						lineEndEndPosition = -1;
						return ODataBatchReaderStreamScanResult.NoMatch;
					}
					i++;
				}
			}
			endOfBufferReached = true;
			lineEndStartPosition = -1;
			lineEndEndPosition = -1;
			return ODataBatchReaderStreamScanResult.NoMatch;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000075F4 File Offset: 0x000057F4
		private ODataBatchReaderStreamScanResult MatchBoundary(int lineEndStartPosition, int boundaryDelimiterStartPosition, string boundary, out int boundaryStartPosition, out int boundaryEndPosition, out bool isEndBoundary)
		{
			boundaryStartPosition = -1;
			boundaryEndPosition = -1;
			int num = this.currentReadPosition + this.numberOfBytesInBuffer - 1;
			int num2 = boundaryDelimiterStartPosition + 2 + boundary.Length + 2 - 1;
			bool flag;
			int num3;
			if (num < num2 + 2)
			{
				flag = true;
				num3 = Math.Min(num, num2) - boundaryDelimiterStartPosition + 1;
			}
			else
			{
				flag = false;
				num3 = num2 - boundaryDelimiterStartPosition + 1;
			}
			if (this.MatchBoundary(boundary, boundaryDelimiterStartPosition, num3, out isEndBoundary))
			{
				boundaryStartPosition = ((lineEndStartPosition < 0) ? boundaryDelimiterStartPosition : lineEndStartPosition);
				if (flag)
				{
					isEndBoundary = false;
					return ODataBatchReaderStreamScanResult.PartialMatch;
				}
				boundaryEndPosition = boundaryDelimiterStartPosition + 2 + boundary.Length - 1;
				if (isEndBoundary)
				{
					boundaryEndPosition += 2;
				}
				int num4;
				int num5;
				bool flag2;
				switch (this.ScanForLineEnd(boundaryEndPosition + 1, 2147483647, true, out num4, out num5, out flag2))
				{
				case ODataBatchReaderStreamScanResult.NoMatch:
					if (flag2)
					{
						if (boundaryStartPosition == 0)
						{
							throw new ODataException(Strings.ODataBatchReaderStreamBuffer_BoundaryLineSecurityLimitReached(8000));
						}
						isEndBoundary = false;
						return ODataBatchReaderStreamScanResult.PartialMatch;
					}
					break;
				case ODataBatchReaderStreamScanResult.PartialMatch:
					if (boundaryStartPosition == 0)
					{
						throw new ODataException(Strings.ODataBatchReaderStreamBuffer_BoundaryLineSecurityLimitReached(8000));
					}
					isEndBoundary = false;
					return ODataBatchReaderStreamScanResult.PartialMatch;
				case ODataBatchReaderStreamScanResult.Match:
					boundaryEndPosition = num5;
					return ODataBatchReaderStreamScanResult.Match;
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReaderStreamBuffer_ScanForBoundary));
				}
			}
			return ODataBatchReaderStreamScanResult.NoMatch;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007714 File Offset: 0x00005914
		private bool MatchBoundary(string boundary, int startIx, int matchLength, out bool isEndBoundary)
		{
			isEndBoundary = false;
			if (matchLength == 0)
			{
				return true;
			}
			int num = 0;
			int num2 = startIx;
			for (int i = -2; i < matchLength - 2; i++)
			{
				if (i < 0)
				{
					if (this.bytes[num2] != 45)
					{
						return false;
					}
				}
				else if (i < boundary.Length)
				{
					if ((char)this.bytes[num2] != boundary.get_Chars(i))
					{
						return false;
					}
				}
				else
				{
					if (this.bytes[num2] != 45)
					{
						return true;
					}
					num++;
				}
				num2++;
			}
			isEndBoundary = num == 2;
			return true;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000778C File Offset: 0x0000598C
		private void ShiftToBeginning(int startIndex)
		{
			int num = this.currentReadPosition + this.numberOfBytesInBuffer - startIndex;
			this.currentReadPosition = 0;
			if (num <= 0)
			{
				this.numberOfBytesInBuffer = 0;
				return;
			}
			this.numberOfBytesInBuffer = num;
			Buffer.BlockCopy(this.bytes, startIndex, this.bytes, 0, num);
		}

		// Token: 0x040000FD RID: 253
		internal const int BufferLength = 8000;

		// Token: 0x040000FE RID: 254
		private const int MaxLineFeedLength = 2;

		// Token: 0x040000FF RID: 255
		private const int TwoDashesLength = 2;

		// Token: 0x04000100 RID: 256
		private readonly byte[] bytes = new byte[8000];

		// Token: 0x04000101 RID: 257
		private int currentReadPosition;

		// Token: 0x04000102 RID: 258
		private int numberOfBytesInBuffer;
	}
}
