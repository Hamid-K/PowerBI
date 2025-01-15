using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text.Encodings.Web
{
	// Token: 0x02000022 RID: 34
	internal struct AsciiByteMap
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00004A1D File Offset: 0x00002C1D
		internal unsafe void InsertAsciiChar(char key, byte value)
		{
			if (key < '\u0080')
			{
				*((ref this.Buffer.FixedElementField) + key) = value;
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004A38 File Offset: 0x00002C38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe readonly bool TryLookup(Rune key, out byte value)
		{
			if (key.IsAscii)
			{
				byte b = *((ref this.Buffer.FixedElementField) + (UIntPtr)key.Value);
				if (b != 0)
				{
					value = b;
					return true;
				}
			}
			value = 0;
			return false;
		}

		// Token: 0x040000C6 RID: 198
		private const int BufferSize = 128;

		// Token: 0x040000C7 RID: 199
		[FixedBuffer(typeof(byte), 128)]
		private AsciiByteMap.<Buffer>e__FixedBuffer Buffer;

		// Token: 0x02000034 RID: 52
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 128)]
		public struct <Buffer>e__FixedBuffer
		{
			// Token: 0x040000E7 RID: 231
			public byte FixedElementField;
		}
	}
}
