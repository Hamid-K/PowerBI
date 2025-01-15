using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData.Core
{
	// Token: 0x0200014A RID: 330
	internal sealed class ODataBatchReaderStreamBuffer
	{
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0002F11E File Offset: 0x0002D31E
		internal byte[] Bytes
		{
			get
			{
				return this.bytes;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x0002F126 File Offset: 0x0002D326
		internal int CurrentReadPosition
		{
			get
			{
				return this.currentReadPosition;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0002F12E File Offset: 0x0002D32E
		internal int NumberOfBytesInBuffer
		{
			get
			{
				return this.numberOfBytesInBuffer;
			}
		}

		// Token: 0x1700027F RID: 639
		internal byte this[int index]
		{
			get
			{
				return this.bytes[index];
			}
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0002F140 File Offset: 0x0002D340
		internal void SkipTo(int newPosition)
		{
			int num = newPosition - this.currentReadPosition;
			this.currentReadPosition = newPosition;
			this.numberOfBytesInBuffer -= num;
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0002F16C File Offset: 0x0002D36C
		internal bool RefillFrom(Stream stream, int preserveFrom)
		{
			this.ShiftToBeginning(preserveFrom);
			int num = 8000 - this.numberOfBytesInBuffer;
			int num2 = stream.Read(this.bytes, this.numberOfBytesInBuffer, num);
			this.numberOfBytesInBuffer += num2;
			return num2 == 0;
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0002F1B4 File Offset: 0x0002D3B4
		internal ODataBatchReaderStreamScanResult ScanForLineEnd(out int lineEndStartPosition, out int lineEndEndPosition)
		{
			bool flag;
			return this.ScanForLineEnd(this.currentReadPosition, 8000, false, out lineEndStartPosition, out lineEndEndPosition, out flag);
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0002F1D8 File Offset: 0x0002D3D8
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

		// Token: 0x06000C8E RID: 3214 RVA: 0x0002F2F4 File Offset: 0x0002D4F4
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

		// Token: 0x06000C8F RID: 3215 RVA: 0x0002F3B0 File Offset: 0x0002D5B0
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

		// Token: 0x06000C90 RID: 3216 RVA: 0x0002F458 File Offset: 0x0002D658
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

		// Token: 0x06000C91 RID: 3217 RVA: 0x0002F57C File Offset: 0x0002D77C
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

		// Token: 0x06000C92 RID: 3218 RVA: 0x0002F5F4 File Offset: 0x0002D7F4
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

		// Token: 0x04000534 RID: 1332
		internal const int BufferLength = 8000;

		// Token: 0x04000535 RID: 1333
		private const int MaxLineFeedLength = 2;

		// Token: 0x04000536 RID: 1334
		private const int TwoDashesLength = 2;

		// Token: 0x04000537 RID: 1335
		private readonly byte[] bytes = new byte[8000];

		// Token: 0x04000538 RID: 1336
		private int currentReadPosition;

		// Token: 0x04000539 RID: 1337
		private int numberOfBytesInBuffer;
	}
}
