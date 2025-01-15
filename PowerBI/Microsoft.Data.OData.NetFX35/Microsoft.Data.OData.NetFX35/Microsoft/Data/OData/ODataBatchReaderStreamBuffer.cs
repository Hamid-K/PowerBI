using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x020001DA RID: 474
	internal sealed class ODataBatchReaderStreamBuffer
	{
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00030DA8 File Offset: 0x0002EFA8
		internal byte[] Bytes
		{
			get
			{
				return this.bytes;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00030DB0 File Offset: 0x0002EFB0
		internal int CurrentReadPosition
		{
			get
			{
				return this.currentReadPosition;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00030DB8 File Offset: 0x0002EFB8
		internal int NumberOfBytesInBuffer
		{
			get
			{
				return this.numberOfBytesInBuffer;
			}
		}

		// Token: 0x1700031A RID: 794
		internal byte this[int index]
		{
			get
			{
				return this.bytes[index];
			}
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00030DCC File Offset: 0x0002EFCC
		internal void SkipTo(int newPosition)
		{
			int num = newPosition - this.currentReadPosition;
			this.currentReadPosition = newPosition;
			this.numberOfBytesInBuffer -= num;
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x00030DF8 File Offset: 0x0002EFF8
		internal bool RefillFrom(Stream stream, int preserveFrom)
		{
			this.ShiftToBeginning(preserveFrom);
			int num = 8000 - this.numberOfBytesInBuffer;
			int num2 = stream.Read(this.bytes, this.numberOfBytesInBuffer, num);
			this.numberOfBytesInBuffer += num2;
			return num2 == 0;
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00030E40 File Offset: 0x0002F040
		internal ODataBatchReaderStreamScanResult ScanForLineEnd(out int lineEndStartPosition, out int lineEndEndPosition)
		{
			bool flag;
			return this.ScanForLineEnd(this.currentReadPosition, 8000, false, out lineEndStartPosition, out lineEndEndPosition, out flag);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00030E64 File Offset: 0x0002F064
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
					goto IL_0040;
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
			IL_0040:
			boundaryStartPosition = ((num2 < 0) ? num3 : num2);
			return ODataBatchReaderStreamScanResult.PartialMatch;
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00030F80 File Offset: 0x0002F180
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

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0003103C File Offset: 0x0002F23C
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

		// Token: 0x06000DE1 RID: 3553 RVA: 0x000310E4 File Offset: 0x0002F2E4
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

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00031208 File Offset: 0x0002F408
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

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00031280 File Offset: 0x0002F480
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

		// Token: 0x04000507 RID: 1287
		internal const int BufferLength = 8000;

		// Token: 0x04000508 RID: 1288
		private const int MaxLineFeedLength = 2;

		// Token: 0x04000509 RID: 1289
		private const int TwoDashesLength = 2;

		// Token: 0x0400050A RID: 1290
		private readonly byte[] bytes = new byte[8000];

		// Token: 0x0400050B RID: 1291
		private int currentReadPosition;

		// Token: 0x0400050C RID: 1292
		private int numberOfBytesInBuffer;
	}
}
