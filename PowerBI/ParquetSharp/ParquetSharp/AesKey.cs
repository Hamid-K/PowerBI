using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x0200000C RID: 12
	[StructLayout(LayoutKind.Sequential, Size = 16)]
	internal struct AesKey
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000217C File Offset: 0x0000037C
		[NullableContext(1)]
		public unsafe AesKey(byte[] key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (key.Length != 0 && key.Length != 16 && key.Length != 24 && key.Length != 32)
			{
				throw new ArgumentException("AES key can only be 128, 192, or 256-bit in length", "key");
			}
			if (key.Length != 0)
			{
				fixed (byte[] array = key)
				{
					byte* ptr;
					if (key == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					ulong* ptr2 = (ulong*)ptr;
					this._key.FixedElementField = *ptr2;
					*((ref this._key.FixedElementField) + 8) = ptr2[1];
					*((ref this._key.FixedElementField) + (IntPtr)2 * 8) = ((key.Length > 16) ? ptr2[2] : 0UL);
					*((ref this._key.FixedElementField) + (IntPtr)3 * 8) = ((key.Length > 24) ? ptr2[3] : 0UL);
				}
			}
			this._size = (uint)key.Length;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000227C File Offset: 0x0000047C
		[NullableContext(1)]
		public unsafe byte[] ToBytes()
		{
			if (this._size != 0U && this._size != 16U && this._size != 24U && this._size != 32U)
			{
				throw new ArgumentException("AES key can only be 128, 192, or 256-bit in length", "_size");
			}
			byte[] array = new byte[this._size];
			if (this._size != 0U)
			{
				byte[] array2;
				byte* ptr;
				if ((array2 = array) == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				ulong* ptr2 = (ulong*)ptr;
				*ptr2 = this._key.FixedElementField;
				ptr2[1] = *((ref this._key.FixedElementField) + 8);
				if (this._size > 16U)
				{
					ptr2[2] = *((ref this._key.FixedElementField) + (IntPtr)2 * 8);
				}
				if (this._size > 24U)
				{
					ptr2[3] = *((ref this._key.FixedElementField) + (IntPtr)3 * 8);
				}
				array2 = null;
			}
			return array;
		}

		// Token: 0x04000009 RID: 9
		[FixedBuffer(typeof(ulong), 4)]
		private AesKey.<_key>e__FixedBuffer _key;

		// Token: 0x0400000A RID: 10
		private readonly uint _size;

		// Token: 0x020000F8 RID: 248
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 32)]
		public struct <_key>e__FixedBuffer
		{
			// Token: 0x040002B6 RID: 694
			public ulong FixedElementField;
		}
	}
}
