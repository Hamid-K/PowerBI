using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Unicode;

namespace System.Text.Encodings.Web
{
	// Token: 0x02000023 RID: 35
	internal struct AllowedBmpCodePointsBitmap
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00004A70 File Offset: 0x00002C70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void AllowChar(char value)
		{
			UIntPtr uintPtr;
			int num;
			AllowedBmpCodePointsBitmap._GetIndexAndOffset((uint)value, out uintPtr, out num);
			*((ref this.Bitmap.FixedElementField) + (UIntPtr)((ulong)uintPtr * 4UL)) |= 1U << num;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004AA4 File Offset: 0x00002CA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void ForbidChar(char value)
		{
			UIntPtr uintPtr;
			int num;
			AllowedBmpCodePointsBitmap._GetIndexAndOffset((uint)value, out uintPtr, out num);
			*((ref this.Bitmap.FixedElementField) + (UIntPtr)((ulong)uintPtr * 4UL)) &= ~(1U << num);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004AD8 File Offset: 0x00002CD8
		public void ForbidHtmlCharacters()
		{
			this.ForbidChar('<');
			this.ForbidChar('>');
			this.ForbidChar('&');
			this.ForbidChar('\'');
			this.ForbidChar('"');
			this.ForbidChar('+');
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004B0C File Offset: 0x00002D0C
		public unsafe void ForbidUndefinedCharacters()
		{
			fixed (uint* ptr = &this.Bitmap.FixedElementField)
			{
				uint* ptr2 = ptr;
				ReadOnlySpan<byte> definedBmpCodePointsBitmapLittleEndian = UnicodeHelpers.GetDefinedBmpCodePointsBitmapLittleEndian();
				Span<uint> span = new Span<uint>((void*)ptr2, 2048);
				for (int i = 0; i < span.Length; i++)
				{
					*span[i] &= BinaryPrimitives.ReadUInt32LittleEndian(definedBmpCodePointsBitmapLittleEndian.Slice(i * 4));
				}
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004B74 File Offset: 0x00002D74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe readonly bool IsCharAllowed(char value)
		{
			UIntPtr uintPtr;
			int num;
			AllowedBmpCodePointsBitmap._GetIndexAndOffset((uint)value, out uintPtr, out num);
			return (*((ref this.Bitmap.FixedElementField) + (UIntPtr)((ulong)uintPtr * 4UL)) & (1U << num)) != 0U;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004BAC File Offset: 0x00002DAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe readonly bool IsCodePointAllowed(uint value)
		{
			if (!UnicodeUtility.IsBmpCodePoint(value))
			{
				return false;
			}
			UIntPtr uintPtr;
			int num;
			AllowedBmpCodePointsBitmap._GetIndexAndOffset(value, out uintPtr, out num);
			return (*((ref this.Bitmap.FixedElementField) + (UIntPtr)((ulong)uintPtr * 4UL)) & (1U << num)) != 0U;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004BEC File Offset: 0x00002DEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void _GetIndexAndOffset(uint value, [NativeInteger] out UIntPtr index, out int offset)
		{
			index = (UIntPtr)(value >> 5);
			offset = (int)(value & 31U);
		}

		// Token: 0x040000C8 RID: 200
		private const int BitmapLengthInDWords = 2048;

		// Token: 0x040000C9 RID: 201
		[FixedBuffer(typeof(uint), 2048)]
		private AllowedBmpCodePointsBitmap.<Bitmap>e__FixedBuffer Bitmap;

		// Token: 0x02000035 RID: 53
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 8192)]
		public struct <Bitmap>e__FixedBuffer
		{
			// Token: 0x040000E8 RID: 232
			public uint FixedElementField;
		}
	}
}
