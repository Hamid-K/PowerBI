using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Properties;
using System.Text;
using System.Web.Http;

namespace System.Net.Http.Formatting.Parsers
{
	// Token: 0x02000058 RID: 88
	internal class MimeMultipartParser
	{
		// Token: 0x06000342 RID: 834 RVA: 0x0000BCB8 File Offset: 0x00009EB8
		public MimeMultipartParser(string boundary, long maxMessageSize)
		{
			if (maxMessageSize < 10L)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxMessageSize", maxMessageSize, 10);
			}
			if (string.IsNullOrWhiteSpace(boundary))
			{
				throw Error.ArgumentNull("boundary");
			}
			if (boundary.Length > 246)
			{
				throw Error.ArgumentMustBeLessThanOrEqualTo("boundary", boundary.Length, 246);
			}
			if (boundary.EndsWith(" ", StringComparison.Ordinal))
			{
				throw Error.Argument("boundary", Resources.MimeMultipartParserBadBoundary, new object[0]);
			}
			this._maxMessageSize = maxMessageSize;
			this._boundary = boundary;
			this._currentBoundary = new MimeMultipartParser.CurrentBodyPartStore(this._boundary);
			this._bodyPartState = MimeMultipartParser.BodyPartState.AfterFirstLineFeed;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000BD73 File Offset: 0x00009F73
		public bool IsWaitingForEndOfMessage
		{
			get
			{
				return this._bodyPartState == MimeMultipartParser.BodyPartState.AfterBoundary && this._currentBoundary != null && this._currentBoundary.IsFinal;
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000BD93 File Offset: 0x00009F93
		public bool CanParseMore(int bytesRead, int bytesConsumed)
		{
			return bytesConsumed < bytesRead || (bytesRead == 0 && this.IsWaitingForEndOfMessage);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000BDAC File Offset: 0x00009FAC
		public MimeMultipartParser.State ParseBuffer(byte[] buffer, int bytesReady, ref int bytesConsumed, out ArraySegment<byte> remainingBodyPart, out ArraySegment<byte> bodyPart, out bool isFinalBodyPart)
		{
			if (buffer == null)
			{
				throw Error.ArgumentNull("buffer");
			}
			MimeMultipartParser.State state = MimeMultipartParser.State.NeedMoreData;
			remainingBodyPart = MimeMultipartParser._emptyBodyPart;
			bodyPart = MimeMultipartParser._emptyBodyPart;
			isFinalBodyPart = false;
			try
			{
				state = MimeMultipartParser.ParseBodyPart(buffer, bytesReady, ref bytesConsumed, ref this._bodyPartState, this._maxMessageSize, ref this._totalBytesConsumed, this._currentBoundary);
			}
			catch (Exception)
			{
				state = MimeMultipartParser.State.Invalid;
			}
			remainingBodyPart = this._currentBoundary.GetDiscardedBoundary();
			bodyPart = this._currentBoundary.BodyPart;
			if (state == MimeMultipartParser.State.BodyPartCompleted)
			{
				isFinalBodyPart = this._currentBoundary.IsFinal;
				this._currentBoundary.ClearAll();
			}
			else
			{
				this._currentBoundary.ClearBodyPart();
			}
			return state;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000BE6C File Offset: 0x0000A06C
		private static MimeMultipartParser.State ParseBodyPart(byte[] buffer, int bytesReady, ref int bytesConsumed, ref MimeMultipartParser.BodyPartState bodyPartState, long maximumMessageLength, ref long totalBytesConsumed, MimeMultipartParser.CurrentBodyPartStore currentBodyPart)
		{
			int num = bytesConsumed;
			if (bytesReady == 0 && bodyPartState == MimeMultipartParser.BodyPartState.AfterBoundary && currentBodyPart.IsFinal)
			{
				return MimeMultipartParser.State.BodyPartCompleted;
			}
			MimeMultipartParser.State state = MimeMultipartParser.State.DataTooBig;
			long num2 = ((maximumMessageLength <= 0L) ? long.MaxValue : (maximumMessageLength - totalBytesConsumed + (long)bytesConsumed));
			if (num2 == 0L)
			{
				return MimeMultipartParser.State.DataTooBig;
			}
			if ((long)bytesReady <= num2)
			{
				state = MimeMultipartParser.State.NeedMoreData;
				num2 = (long)bytesReady;
			}
			currentBodyPart.ResetBoundaryOffset();
			switch (bodyPartState)
			{
			case MimeMultipartParser.BodyPartState.BodyPart:
				break;
			case MimeMultipartParser.BodyPartState.AfterFirstCarriageReturn:
				goto IL_00B4;
			case MimeMultipartParser.BodyPartState.AfterFirstLineFeed:
				goto IL_00E7;
			case MimeMultipartParser.BodyPartState.AfterFirstDash:
				goto IL_014D;
			case MimeMultipartParser.BodyPartState.Boundary:
				goto IL_0183;
			case MimeMultipartParser.BodyPartState.AfterBoundary:
				goto IL_01F5;
			case MimeMultipartParser.BodyPartState.AfterSecondDash:
				goto IL_02C3;
			case MimeMultipartParser.BodyPartState.AfterSecondCarriageReturn:
				goto IL_030B;
			default:
				goto IL_0351;
			}
			IL_008D:
			int num3;
			while (buffer[bytesConsumed] != 13)
			{
				num3 = bytesConsumed + 1;
				bytesConsumed = num3;
				if ((long)num3 == num2)
				{
					goto IL_0351;
				}
			}
			currentBodyPart.AppendBoundary(13);
			bodyPartState = MimeMultipartParser.BodyPartState.AfterFirstCarriageReturn;
			num3 = bytesConsumed + 1;
			bytesConsumed = num3;
			if ((long)num3 == num2)
			{
				goto IL_0351;
			}
			IL_00B4:
			if (buffer[bytesConsumed] != 10)
			{
				currentBodyPart.ResetBoundary();
				bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
				goto IL_008D;
			}
			currentBodyPart.AppendBoundary(10);
			bodyPartState = MimeMultipartParser.BodyPartState.AfterFirstLineFeed;
			num3 = bytesConsumed + 1;
			bytesConsumed = num3;
			if ((long)num3 == num2)
			{
				goto IL_0351;
			}
			IL_00E7:
			if (buffer[bytesConsumed] == 13)
			{
				currentBodyPart.ResetBoundary();
				currentBodyPart.AppendBoundary(13);
				bodyPartState = MimeMultipartParser.BodyPartState.AfterFirstCarriageReturn;
				num3 = bytesConsumed + 1;
				bytesConsumed = num3;
				if ((long)num3 == num2)
				{
					goto IL_0351;
				}
				goto IL_00B4;
			}
			else
			{
				if (buffer[bytesConsumed] != 45)
				{
					currentBodyPart.ResetBoundary();
					bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
					goto IL_008D;
				}
				currentBodyPart.AppendBoundary(45);
				bodyPartState = MimeMultipartParser.BodyPartState.AfterFirstDash;
				num3 = bytesConsumed + 1;
				bytesConsumed = num3;
				if ((long)num3 == num2)
				{
					goto IL_0351;
				}
			}
			IL_014D:
			if (buffer[bytesConsumed] != 45)
			{
				currentBodyPart.ResetBoundary();
				bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
				goto IL_008D;
			}
			currentBodyPart.AppendBoundary(45);
			bodyPartState = MimeMultipartParser.BodyPartState.Boundary;
			num3 = bytesConsumed + 1;
			bytesConsumed = num3;
			if ((long)num3 == num2)
			{
				goto IL_0351;
			}
			IL_0183:
			int num4 = bytesConsumed;
			while (buffer[bytesConsumed] != 13)
			{
				num3 = bytesConsumed + 1;
				bytesConsumed = num3;
				if ((long)num3 == num2)
				{
					if (!currentBodyPart.AppendBoundary(buffer, num4, bytesConsumed - num4))
					{
						currentBodyPart.ResetBoundary();
						bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
						goto IL_0351;
					}
					if (currentBodyPart.IsBoundaryComplete())
					{
						bodyPartState = MimeMultipartParser.BodyPartState.AfterBoundary;
						goto IL_0351;
					}
					goto IL_0351;
				}
			}
			if (bytesConsumed > num4 && !currentBodyPart.AppendBoundary(buffer, num4, bytesConsumed - num4))
			{
				currentBodyPart.ResetBoundary();
				bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
				goto IL_008D;
			}
			IL_01F5:
			if (buffer[bytesConsumed] == 45 && !currentBodyPart.IsFinal)
			{
				currentBodyPart.AppendBoundary(45);
				num3 = bytesConsumed + 1;
				bytesConsumed = num3;
				if ((long)num3 == num2)
				{
					bodyPartState = MimeMultipartParser.BodyPartState.AfterSecondDash;
					goto IL_0351;
				}
			}
			else
			{
				num4 = bytesConsumed;
				while (buffer[bytesConsumed] != 13)
				{
					num3 = bytesConsumed + 1;
					bytesConsumed = num3;
					if ((long)num3 == num2)
					{
						if (!currentBodyPart.AppendBoundary(buffer, num4, bytesConsumed - num4))
						{
							currentBodyPart.ResetBoundary();
							bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
							goto IL_0351;
						}
						goto IL_0351;
					}
				}
				if (bytesConsumed > num4 && !currentBodyPart.AppendBoundary(buffer, num4, bytesConsumed - num4))
				{
					currentBodyPart.ResetBoundary();
					bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
					goto IL_008D;
				}
				if (buffer[bytesConsumed] != 13)
				{
					currentBodyPart.ResetBoundary();
					bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
					goto IL_008D;
				}
				currentBodyPart.AppendBoundary(13);
				num3 = bytesConsumed + 1;
				bytesConsumed = num3;
				if ((long)num3 == num2)
				{
					bodyPartState = MimeMultipartParser.BodyPartState.AfterSecondCarriageReturn;
					goto IL_0351;
				}
				goto IL_030B;
			}
			IL_02C3:
			if (buffer[bytesConsumed] != 45)
			{
				currentBodyPart.ResetBoundary();
				bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
				goto IL_008D;
			}
			currentBodyPart.AppendBoundary(45);
			bytesConsumed++;
			if (currentBodyPart.IsBoundaryComplete())
			{
				bodyPartState = MimeMultipartParser.BodyPartState.AfterBoundary;
				state = MimeMultipartParser.State.NeedMoreData;
				goto IL_0351;
			}
			currentBodyPart.ResetBoundary();
			if ((long)bytesConsumed == num2)
			{
				goto IL_0351;
			}
			goto IL_008D;
			IL_030B:
			if (buffer[bytesConsumed] != 10)
			{
				currentBodyPart.ResetBoundary();
				bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
				goto IL_008D;
			}
			currentBodyPart.AppendBoundary(10);
			bytesConsumed++;
			bodyPartState = MimeMultipartParser.BodyPartState.BodyPart;
			if (currentBodyPart.IsBoundaryComplete())
			{
				state = MimeMultipartParser.State.BodyPartCompleted;
			}
			else
			{
				currentBodyPart.ResetBoundary();
				if ((long)bytesConsumed != num2)
				{
					goto IL_008D;
				}
			}
			IL_0351:
			if (num < bytesConsumed)
			{
				int boundaryDelta = currentBodyPart.BoundaryDelta;
				if (boundaryDelta > 0 && state != MimeMultipartParser.State.BodyPartCompleted)
				{
					currentBodyPart.HasPotentialBoundaryLeftOver = true;
				}
				int num5 = bytesConsumed - num - boundaryDelta;
				currentBodyPart.BodyPart = new ArraySegment<byte>(buffer, num, num5);
			}
			totalBytesConsumed += (long)(bytesConsumed - num);
			return state;
		}

		// Token: 0x04000120 RID: 288
		internal const int MinMessageSize = 10;

		// Token: 0x04000121 RID: 289
		private const int MaxBoundarySize = 256;

		// Token: 0x04000122 RID: 290
		private const byte HTAB = 9;

		// Token: 0x04000123 RID: 291
		private const byte SP = 32;

		// Token: 0x04000124 RID: 292
		private const byte CR = 13;

		// Token: 0x04000125 RID: 293
		private const byte LF = 10;

		// Token: 0x04000126 RID: 294
		private const byte Dash = 45;

		// Token: 0x04000127 RID: 295
		private static readonly ArraySegment<byte> _emptyBodyPart = new ArraySegment<byte>(new byte[0]);

		// Token: 0x04000128 RID: 296
		private long _totalBytesConsumed;

		// Token: 0x04000129 RID: 297
		private long _maxMessageSize;

		// Token: 0x0400012A RID: 298
		private MimeMultipartParser.BodyPartState _bodyPartState;

		// Token: 0x0400012B RID: 299
		private string _boundary;

		// Token: 0x0400012C RID: 300
		private MimeMultipartParser.CurrentBodyPartStore _currentBoundary;

		// Token: 0x0200008A RID: 138
		private enum BodyPartState
		{
			// Token: 0x040001F9 RID: 505
			BodyPart,
			// Token: 0x040001FA RID: 506
			AfterFirstCarriageReturn,
			// Token: 0x040001FB RID: 507
			AfterFirstLineFeed,
			// Token: 0x040001FC RID: 508
			AfterFirstDash,
			// Token: 0x040001FD RID: 509
			Boundary,
			// Token: 0x040001FE RID: 510
			AfterBoundary,
			// Token: 0x040001FF RID: 511
			AfterSecondDash,
			// Token: 0x04000200 RID: 512
			AfterSecondCarriageReturn
		}

		// Token: 0x0200008B RID: 139
		private enum MessageState
		{
			// Token: 0x04000202 RID: 514
			Boundary,
			// Token: 0x04000203 RID: 515
			BodyPart,
			// Token: 0x04000204 RID: 516
			CloseDelimiter
		}

		// Token: 0x0200008C RID: 140
		public enum State
		{
			// Token: 0x04000206 RID: 518
			NeedMoreData,
			// Token: 0x04000207 RID: 519
			BodyPartCompleted,
			// Token: 0x04000208 RID: 520
			Invalid,
			// Token: 0x04000209 RID: 521
			DataTooBig
		}

		// Token: 0x0200008D RID: 141
		[DebuggerDisplay("{DebuggerToString()}")]
		private class CurrentBodyPartStore
		{
			// Token: 0x060003FC RID: 1020 RVA: 0x0000EDB0 File Offset: 0x0000CFB0
			public CurrentBodyPartStore(string referenceBoundary)
			{
				this._referenceBoundary[0] = 13;
				this._referenceBoundary[1] = 10;
				this._referenceBoundary[2] = 45;
				this._referenceBoundary[3] = 45;
				this._referenceBoundaryLength = 4 + Encoding.UTF8.GetBytes(referenceBoundary, 0, referenceBoundary.Length, this._referenceBoundary, 4);
				this._boundary[0] = 13;
				this._boundary[1] = 10;
				this._boundaryLength = 2;
			}

			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000EE69 File Offset: 0x0000D069
			// (set) Token: 0x060003FE RID: 1022 RVA: 0x0000EE71 File Offset: 0x0000D071
			public bool HasPotentialBoundaryLeftOver { get; set; }

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000EE7A File Offset: 0x0000D07A
			public int BoundaryDelta
			{
				get
				{
					if (this._boundaryLength - this._boundaryOffset <= 0)
					{
						return this._boundaryLength;
					}
					return this._boundaryLength - this._boundaryOffset;
				}
			}

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000EEA0 File Offset: 0x0000D0A0
			// (set) Token: 0x06000401 RID: 1025 RVA: 0x0000EEA8 File Offset: 0x0000D0A8
			public ArraySegment<byte> BodyPart
			{
				get
				{
					return this._bodyPart;
				}
				set
				{
					this._bodyPart = value;
				}
			}

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000EEB1 File Offset: 0x0000D0B1
			public bool IsFinal
			{
				get
				{
					return this._isFinal;
				}
			}

			// Token: 0x06000403 RID: 1027 RVA: 0x0000EEB9 File Offset: 0x0000D0B9
			public void ResetBoundaryOffset()
			{
				this._boundaryOffset = this._boundaryLength;
			}

			// Token: 0x06000404 RID: 1028 RVA: 0x0000EEC8 File Offset: 0x0000D0C8
			public void ResetBoundary()
			{
				if (this.HasPotentialBoundaryLeftOver)
				{
					Buffer.BlockCopy(this._boundary, 0, this._boundaryStore, 0, this._boundaryOffset);
					this._boundaryStoreLength = this._boundaryOffset;
					this.HasPotentialBoundaryLeftOver = false;
					this._releaseDiscardedBoundary = true;
				}
				this._boundaryLength = 0;
				this._boundaryOffset = 0;
			}

			// Token: 0x06000405 RID: 1029 RVA: 0x0000EF20 File Offset: 0x0000D120
			public void AppendBoundary(byte data)
			{
				byte[] boundary = this._boundary;
				int boundaryLength = this._boundaryLength;
				this._boundaryLength = boundaryLength + 1;
				boundary[boundaryLength] = data;
			}

			// Token: 0x06000406 RID: 1030 RVA: 0x0000EF48 File Offset: 0x0000D148
			public bool AppendBoundary(byte[] data, int offset, int count)
			{
				if (this._boundaryLength + count > this._referenceBoundaryLength + 6)
				{
					return false;
				}
				int i = this._boundaryLength;
				Buffer.BlockCopy(data, offset, this._boundary, this._boundaryLength, count);
				this._boundaryLength += count;
				int num = Math.Min(this._boundaryLength, this._referenceBoundaryLength);
				while (i < num)
				{
					if (this._boundary[i] != this._referenceBoundary[i])
					{
						return false;
					}
					i++;
				}
				return true;
			}

			// Token: 0x06000407 RID: 1031 RVA: 0x0000EFC3 File Offset: 0x0000D1C3
			public ArraySegment<byte> GetDiscardedBoundary()
			{
				if (this._boundaryStoreLength > 0 && this._releaseDiscardedBoundary)
				{
					ArraySegment<byte> arraySegment = new ArraySegment<byte>(this._boundaryStore, 0, this._boundaryStoreLength);
					this._boundaryStoreLength = 0;
					return arraySegment;
				}
				return MimeMultipartParser._emptyBodyPart;
			}

			// Token: 0x06000408 RID: 1032 RVA: 0x0000EFF8 File Offset: 0x0000D1F8
			public bool IsBoundaryValid()
			{
				int num = 0;
				if (this._isFirst)
				{
					num = 2;
				}
				int i;
				for (i = num; i < this._referenceBoundaryLength; i++)
				{
					if (this._boundary[i] != this._referenceBoundary[i])
					{
						return false;
					}
				}
				bool flag = false;
				if (this._boundary[i] == 45 && this._boundary[i + 1] == 45)
				{
					flag = true;
					i += 2;
				}
				while (i < this._boundaryLength - 2)
				{
					if (this._boundary[i] != 32 && this._boundary[i] != 9)
					{
						return false;
					}
					i++;
				}
				this._isFinal = flag;
				this._isFirst = false;
				return true;
			}

			// Token: 0x06000409 RID: 1033 RVA: 0x0000F092 File Offset: 0x0000D292
			public bool IsBoundaryComplete()
			{
				return this.IsBoundaryValid() && this._boundaryLength >= this._referenceBoundaryLength && (this._boundaryLength != this._referenceBoundaryLength + 1 || this._boundary[this._referenceBoundaryLength] != 45);
			}

			// Token: 0x0600040A RID: 1034 RVA: 0x0000F0D2 File Offset: 0x0000D2D2
			public void ClearBodyPart()
			{
				this.BodyPart = MimeMultipartParser._emptyBodyPart;
			}

			// Token: 0x0600040B RID: 1035 RVA: 0x0000F0DF File Offset: 0x0000D2DF
			public void ClearAll()
			{
				this._releaseDiscardedBoundary = false;
				this.HasPotentialBoundaryLeftOver = false;
				this._boundaryLength = 0;
				this._boundaryOffset = 0;
				this._boundaryStoreLength = 0;
				this._isFinal = false;
				this.ClearBodyPart();
			}

			// Token: 0x0600040C RID: 1036 RVA: 0x0000F114 File Offset: 0x0000D314
			private string DebuggerToString()
			{
				string @string = Encoding.UTF8.GetString(this._referenceBoundary, 0, this._referenceBoundaryLength);
				string string2 = Encoding.UTF8.GetString(this._boundary, 0, this._boundaryLength);
				return string.Format(CultureInfo.InvariantCulture, "Expected: {0} *** Current: {1}", new object[] { @string, string2 });
			}

			// Token: 0x0400020A RID: 522
			private const int InitialOffset = 2;

			// Token: 0x0400020B RID: 523
			private byte[] _boundaryStore = new byte[256];

			// Token: 0x0400020C RID: 524
			private int _boundaryStoreLength;

			// Token: 0x0400020D RID: 525
			private byte[] _referenceBoundary = new byte[256];

			// Token: 0x0400020E RID: 526
			private int _referenceBoundaryLength;

			// Token: 0x0400020F RID: 527
			private byte[] _boundary = new byte[256];

			// Token: 0x04000210 RID: 528
			private int _boundaryLength;

			// Token: 0x04000211 RID: 529
			private ArraySegment<byte> _bodyPart = MimeMultipartParser._emptyBodyPart;

			// Token: 0x04000212 RID: 530
			private bool _isFinal;

			// Token: 0x04000213 RID: 531
			private bool _isFirst = true;

			// Token: 0x04000214 RID: 532
			private bool _releaseDiscardedBoundary;

			// Token: 0x04000215 RID: 533
			private int _boundaryOffset;
		}
	}
}
