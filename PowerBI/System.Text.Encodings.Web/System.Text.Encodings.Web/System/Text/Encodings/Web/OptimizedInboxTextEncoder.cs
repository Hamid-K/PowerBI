using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text.Encodings.Web
{
	// Token: 0x02000024 RID: 36
	internal sealed class OptimizedInboxTextEncoder
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00004BFC File Offset: 0x00002DFC
		internal unsafe OptimizedInboxTextEncoder(ScalarEscaperBase scalarEscaper, in AllowedBmpCodePointsBitmap allowedCodePointsBmp, bool forbidHtmlSensitiveCharacters = true, ReadOnlySpan<char> extraCharactersToEscape = default(ReadOnlySpan<char>))
		{
			this._scalarEscaper = scalarEscaper;
			this._allowedBmpCodePoints = allowedCodePointsBmp;
			this._allowedBmpCodePoints.ForbidUndefinedCharacters();
			if (forbidHtmlSensitiveCharacters)
			{
				this._allowedBmpCodePoints.ForbidHtmlCharacters();
			}
			ReadOnlySpan<char> readOnlySpan = extraCharactersToEscape;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				char c = (char)(*readOnlySpan[i]);
				this._allowedBmpCodePoints.ForbidChar(c);
			}
			this._asciiPreescapedData.PopulatePreescapedData(in this._allowedBmpCodePoints, scalarEscaper);
			this._allowedAsciiCodePoints.PopulateAllowedCodePoints(in this._allowedBmpCodePoints);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004C89 File Offset: 0x00002E89
		[Obsolete("FindFirstCharacterToEncode has been deprecated. It should only be used by the TextEncoder adapter.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe int FindFirstCharacterToEncode(char* text, int textLength)
		{
			return this.GetIndexOfFirstCharToEncode(new ReadOnlySpan<char>((void*)text, textLength));
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004C98 File Offset: 0x00002E98
		[Obsolete("TryEncodeUnicodeScalar has been deprecated. It should only be used by the TextEncoder adapter.")]
		public unsafe bool TryEncodeUnicodeScalar(int unicodeScalar, char* buffer, int bufferLength, out int numberOfCharactersWritten)
		{
			Span<char> span = new Span<char>((void*)buffer, bufferLength);
			if (this._allowedBmpCodePoints.IsCodePointAllowed((uint)unicodeScalar))
			{
				if (!span.IsEmpty)
				{
					*span[0] = (char)unicodeScalar;
					numberOfCharactersWritten = 1;
					return true;
				}
			}
			else
			{
				int num = this._scalarEscaper.EncodeUtf16(new Rune(unicodeScalar), span);
				if (num >= 0)
				{
					numberOfCharactersWritten = num;
					return true;
				}
			}
			numberOfCharactersWritten = 0;
			return false;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004CF8 File Offset: 0x00002EF8
		public unsafe OperationStatus Encode(ReadOnlySpan<char> source, Span<char> destination, out int charsConsumed, out int charsWritten, bool isFinalBlock)
		{
			this._AssertThisNotNull();
			int num = 0;
			int num2 = 0;
			IL_000A:
			OperationStatus operationStatus;
			while (SpanUtility.IsValidIndex<char>(source, num))
			{
				char c = (char)(*source[num]);
				ulong num3;
				if (this._asciiPreescapedData.TryGetPreescapedData((uint)c, out num3))
				{
					if (!SpanUtility.IsValidIndex<char>(destination, num2))
					{
						goto IL_0148;
					}
					*destination[num2] = (char)((byte)num3);
					if (((uint)num3 & 65280U) == 0U)
					{
						num2++;
						num++;
						continue;
					}
					num3 >>= 8;
					int num4 = num2 + 1;
					while (SpanUtility.IsValidIndex<char>(destination, num4))
					{
						*destination[num4++] = (char)((byte)num3);
						if ((byte)(num3 >>= 8) == 0)
						{
							num2 = num4;
							num++;
							goto IL_000A;
						}
					}
					goto IL_0148;
				}
				else
				{
					Rune replacementChar;
					if (!Rune.TryCreate(c, out replacementChar))
					{
						int num5 = num + 1;
						if (SpanUtility.IsValidIndex<char>(source, num5))
						{
							if (Rune.TryCreate(c, (char)(*source[num5]), out replacementChar))
							{
								goto IL_00E1;
							}
						}
						else if (!isFinalBlock && char.IsHighSurrogate(c))
						{
							operationStatus = OperationStatus.NeedMoreData;
							goto IL_013F;
						}
						replacementChar = Rune.ReplacementChar;
						goto IL_010D;
					}
					IL_00E1:
					if (this.IsScalarValueAllowed(replacementChar))
					{
						int num6;
						if (replacementChar.TryEncodeToUtf16(destination.Slice(num2), out num6))
						{
							num2 += num6;
							num += num6;
							continue;
						}
						goto IL_0148;
					}
					IL_010D:
					int num7 = this._scalarEscaper.EncodeUtf16(replacementChar, destination.Slice(num2));
					if (num7 >= 0)
					{
						num2 += num7;
						num += replacementChar.Utf16SequenceLength;
						continue;
					}
					goto IL_0148;
				}
				IL_013F:
				charsConsumed = num;
				charsWritten = num2;
				return operationStatus;
				IL_0148:
				operationStatus = OperationStatus.DestinationTooSmall;
				goto IL_013F;
			}
			operationStatus = OperationStatus.Done;
			goto IL_013F;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004E54 File Offset: 0x00003054
		public unsafe OperationStatus EncodeUtf8(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesConsumed, out int bytesWritten, bool isFinalBlock)
		{
			this._AssertThisNotNull();
			int num = 0;
			int num2 = 0;
			IL_000A:
			OperationStatus operationStatus2;
			while (SpanUtility.IsValidIndex<byte>(source, num))
			{
				uint num3 = (uint)(*source[num]);
				ulong num4;
				if (this._asciiPreescapedData.TryGetPreescapedData(num3, out num4))
				{
					if (SpanUtility.TryWriteUInt64LittleEndian(destination, num2, num4))
					{
						num2 += (int)(num4 >> 56);
						num++;
						continue;
					}
					int num5 = num2;
					while (SpanUtility.IsValidIndex<byte>(destination, num5))
					{
						*destination[num5++] = (byte)num4;
						if ((byte)(num4 >>= 8) == 0)
						{
							num2 = num5;
							num++;
							goto IL_000A;
						}
					}
					goto IL_0103;
				}
				else
				{
					Rune rune;
					int num6;
					OperationStatus operationStatus = Rune.DecodeFromUtf8(source.Slice(num), out rune, out num6);
					if (operationStatus != OperationStatus.Done)
					{
						if (!isFinalBlock && operationStatus == OperationStatus.NeedMoreData)
						{
							operationStatus2 = OperationStatus.NeedMoreData;
							goto IL_00FA;
						}
					}
					else if (this.IsScalarValueAllowed(rune))
					{
						int num7;
						if (rune.TryEncodeToUtf8(destination.Slice(num2), out num7))
						{
							num2 += num7;
							num += num7;
							continue;
						}
						goto IL_0103;
					}
					int num8 = this._scalarEscaper.EncodeUtf8(rune, destination.Slice(num2));
					if (num8 >= 0)
					{
						num2 += num8;
						num += num6;
						continue;
					}
					goto IL_0103;
				}
				IL_00FA:
				bytesConsumed = num;
				bytesWritten = num2;
				return operationStatus2;
				IL_0103:
				operationStatus2 = OperationStatus.DestinationTooSmall;
				goto IL_00FA;
			}
			operationStatus2 = OperationStatus.Done;
			goto IL_00FA;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004F6C File Offset: 0x0000316C
		public int GetIndexOfFirstByteToEncode(ReadOnlySpan<byte> data)
		{
			int length = data.Length;
			Rune rune;
			int num;
			while (!data.IsEmpty && Rune.DecodeFromUtf8(data, out rune, out num) == OperationStatus.Done && num < 4 && this._allowedBmpCodePoints.IsCharAllowed((char)rune.Value))
			{
				data = data.Slice(num);
			}
			if (!data.IsEmpty)
			{
				return length - data.Length;
			}
			return -1;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004FD4 File Offset: 0x000031D4
		public unsafe int GetIndexOfFirstCharToEncode(ReadOnlySpan<char> data)
		{
			fixed (char* pinnableReference = data.GetPinnableReference())
			{
				char* ptr = pinnableReference;
				UIntPtr uintPtr = (UIntPtr)data.Length;
				UIntPtr uintPtr2 = (UIntPtr)((IntPtr)0);
				if (uintPtr2 < uintPtr)
				{
					this._AssertThisNotNull();
					IntPtr intPtr = (IntPtr)0;
					while (uintPtr - uintPtr2 >= (UIntPtr)((IntPtr)8))
					{
						intPtr = (IntPtr)(-1);
						if (!this._allowedBmpCodePoints.IsCharAllowed(ptr[(UIntPtr)((ulong)(uintPtr2 + (UIntPtr)(++intPtr)) * 2UL)]) || !this._allowedBmpCodePoints.IsCharAllowed(ptr[(UIntPtr)((ulong)(uintPtr2 + (UIntPtr)(++intPtr)) * 2UL)]) || !this._allowedBmpCodePoints.IsCharAllowed(ptr[(UIntPtr)((ulong)(uintPtr2 + (UIntPtr)(++intPtr)) * 2UL)]) || !this._allowedBmpCodePoints.IsCharAllowed(ptr[(UIntPtr)((ulong)(uintPtr2 + (UIntPtr)(++intPtr)) * 2UL)]) || !this._allowedBmpCodePoints.IsCharAllowed(ptr[(UIntPtr)((ulong)(uintPtr2 + (UIntPtr)(++intPtr)) * 2UL)]) || !this._allowedBmpCodePoints.IsCharAllowed(ptr[(UIntPtr)((ulong)(uintPtr2 + (UIntPtr)(++intPtr)) * 2UL)]) || !this._allowedBmpCodePoints.IsCharAllowed(ptr[(UIntPtr)((ulong)(uintPtr2 + (UIntPtr)(++intPtr)) * 2UL)]) || !this._allowedBmpCodePoints.IsCharAllowed(ptr[(UIntPtr)((ulong)(uintPtr2 + (UIntPtr)(++intPtr)) * 2UL)]))
						{
							uintPtr2 += (UIntPtr)intPtr;
							goto IL_0166;
						}
						uintPtr2 += (UIntPtr)((IntPtr)8);
					}
					while (uintPtr2 < uintPtr)
					{
						if (!this._allowedBmpCodePoints.IsCharAllowed(ptr[(ulong)uintPtr2 * 2UL / 2UL]))
						{
							break;
						}
						uintPtr2++;
					}
				}
				IL_0166:
				int num = (int)uintPtr2;
				if (num == (int)uintPtr)
				{
					num = -1;
				}
				return num;
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005156 File Offset: 0x00003356
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsScalarValueAllowed(Rune value)
		{
			return this._allowedBmpCodePoints.IsCodePointAllowed((uint)value.Value);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000516A File Offset: 0x0000336A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void _AssertThisNotNull()
		{
			base.GetType() == typeof(OptimizedInboxTextEncoder);
		}

		// Token: 0x040000CA RID: 202
		private readonly OptimizedInboxTextEncoder.AllowedAsciiCodePoints _allowedAsciiCodePoints;

		// Token: 0x040000CB RID: 203
		private readonly OptimizedInboxTextEncoder.AsciiPreescapedData _asciiPreescapedData;

		// Token: 0x040000CC RID: 204
		private readonly AllowedBmpCodePointsBitmap _allowedBmpCodePoints;

		// Token: 0x040000CD RID: 205
		private readonly ScalarEscaperBase _scalarEscaper;

		// Token: 0x02000036 RID: 54
		[StructLayout(LayoutKind.Explicit)]
		private struct AllowedAsciiCodePoints
		{
			// Token: 0x060001B2 RID: 434 RVA: 0x000063FC File Offset: 0x000045FC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal unsafe readonly bool IsAllowedAsciiCodePoint(uint codePoint)
			{
				if (codePoint > 127U)
				{
					return false;
				}
				uint num = (uint)(*((ref this.AsBytes.FixedElementField) + (UIntPtr)(codePoint & 15U)));
				return (num & (1U << (int)(codePoint >> 4))) != 0U;
			}

			// Token: 0x060001B3 RID: 435 RVA: 0x00006434 File Offset: 0x00004634
			internal void PopulateAllowedCodePoints(in AllowedBmpCodePointsBitmap allowedBmpCodePoints)
			{
				this = default(OptimizedInboxTextEncoder.AllowedAsciiCodePoints);
				for (int i = 32; i < 127; i++)
				{
					if (allowedBmpCodePoints.IsCharAllowed((char)i))
					{
						ref byte ptr = (ref this.AsBytes.FixedElementField) + (i & 15);
						ptr |= (byte)(1 << (i >> 4));
					}
				}
			}

			// Token: 0x040000E9 RID: 233
			[FixedBuffer(typeof(byte), 16)]
			[FieldOffset(0)]
			private OptimizedInboxTextEncoder.AllowedAsciiCodePoints.<AsBytes>e__FixedBuffer AsBytes;

			// Token: 0x0200003F RID: 63
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 16)]
			public struct <AsBytes>e__FixedBuffer
			{
				// Token: 0x040000F5 RID: 245
				public byte FixedElementField;
			}
		}

		// Token: 0x02000037 RID: 55
		private struct AsciiPreescapedData
		{
			// Token: 0x060001B4 RID: 436 RVA: 0x00006480 File Offset: 0x00004680
			internal unsafe void PopulatePreescapedData(in AllowedBmpCodePointsBitmap allowedCodePointsBmp, ScalarEscaperBase innerEncoder)
			{
				this = default(OptimizedInboxTextEncoder.AsciiPreescapedData);
				IntPtr intPtr = stackalloc byte[(UIntPtr)16];
				initblk(intPtr, 0, 16);
				Span<char> span = new Span<char>(intPtr, 8);
				Span<char> span2 = span;
				for (int i = 0; i < 128; i++)
				{
					Rune rune = new Rune(i);
					ulong num;
					int num2;
					if (!Rune.IsControl(rune) && allowedCodePointsBmp.IsCharAllowed((char)i))
					{
						num = (ulong)i;
						num2 = 1;
					}
					else
					{
						num2 = innerEncoder.EncodeUtf16(rune, span2.Slice(0, 6));
						num = 0UL;
						span2.Slice(num2).Clear();
						for (int j = num2 - 1; j >= 0; j--)
						{
							uint num3 = (uint)(*span2[j]);
							num = (num << 8) | (ulong)num3;
						}
					}
					*((ref this.Data.FixedElementField) + (IntPtr)i * 8) = num | ((ulong)num2 << 56);
				}
			}

			// Token: 0x060001B5 RID: 437 RVA: 0x00006548 File Offset: 0x00004748
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal unsafe readonly bool TryGetPreescapedData(uint codePoint, out ulong preescapedData)
			{
				if (codePoint <= 127U)
				{
					preescapedData = *((ref this.Data.FixedElementField) + (IntPtr)((ulong)codePoint * 8UL));
					return true;
				}
				preescapedData = 0UL;
				return false;
			}

			// Token: 0x040000EA RID: 234
			[FixedBuffer(typeof(ulong), 128)]
			private OptimizedInboxTextEncoder.AsciiPreescapedData.<Data>e__FixedBuffer Data;

			// Token: 0x02000040 RID: 64
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 1024)]
			public struct <Data>e__FixedBuffer
			{
				// Token: 0x040000F6 RID: 246
				public ulong FixedElementField;
			}
		}
	}
}
