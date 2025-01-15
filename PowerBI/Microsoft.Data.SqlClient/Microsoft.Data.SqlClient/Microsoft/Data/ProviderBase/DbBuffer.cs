using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Data.Common;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x02000168 RID: 360
	internal abstract class DbBuffer : SafeHandle
	{
		// Token: 0x06001AAE RID: 6830 RVA: 0x0006D3EC File Offset: 0x0006B5EC
		private DbBuffer(int initialSize, bool zeroBuffer)
			: base(IntPtr.Zero, true)
		{
			if (0 < initialSize)
			{
				int num = (zeroBuffer ? 64 : 0);
				this._bufferLength = initialSize;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					this.handle = SafeNativeMethods.LocalAlloc(num, (IntPtr)initialSize);
				}
				if (IntPtr.Zero == this.handle)
				{
					throw new OutOfMemoryException();
				}
			}
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x0006D45C File Offset: 0x0006B65C
		protected DbBuffer(int initialSize)
			: this(initialSize, true)
		{
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x0006D466 File Offset: 0x0006B666
		protected DbBuffer(IntPtr invalidHandleValue, bool ownsHandle)
			: base(invalidHandleValue, ownsHandle)
		{
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x0001996E File Offset: 0x00017B6E
		private int BaseOffset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x0002743C File Offset: 0x0002563C
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x0006D470 File Offset: 0x0006B670
		internal int Length
		{
			get
			{
				return this._bufferLength;
			}
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x0006D478 File Offset: 0x0006B678
		internal string PtrToStringUni(int offset)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2);
			string text = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				int num = UnsafeNativeMethods.lstrlenW(intPtr);
				this.Validate(offset, 2 * (num + 1));
				text = Marshal.PtrToStringUni(intPtr, num);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return text;
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x0006D4F0 File Offset: 0x0006B6F0
		internal string PtrToStringUni(int offset, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			string text = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				text = Marshal.PtrToStringUni(intPtr, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return text;
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x0006D554 File Offset: 0x0006B754
		internal byte ReadByte(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			byte b;
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = base.DangerousGetHandle();
				b = Marshal.ReadByte(intPtr, offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return b;
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0006D5A8 File Offset: 0x0006B7A8
		internal byte[] ReadBytes(int offset, int length)
		{
			byte[] array = new byte[length];
			return this.ReadBytes(offset, array, 0, length);
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x0006D5C8 File Offset: 0x0006B7C8
		internal byte[] ReadBytes(int offset, byte[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(intPtr, destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return destination;
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x0006D62C File Offset: 0x0006B82C
		internal char ReadChar(int offset)
		{
			short num = this.ReadInt16(offset);
			return (char)num;
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x0006D644 File Offset: 0x0006B844
		internal char[] ReadChars(int offset, char[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(intPtr, destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return destination;
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x0006D6AC File Offset: 0x0006B8AC
		internal double ReadDouble(int offset)
		{
			long num = this.ReadInt64(offset);
			return BitConverter.Int64BitsToDouble(num);
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x0006D6C8 File Offset: 0x0006B8C8
		internal short ReadInt16(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			short num;
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = base.DangerousGetHandle();
				num = Marshal.ReadInt16(intPtr, offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x0006D71C File Offset: 0x0006B91C
		internal void ReadInt16Array(int offset, short[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(intPtr, destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x0006D780 File Offset: 0x0006B980
		internal int ReadInt32(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			int num;
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = base.DangerousGetHandle();
				num = Marshal.ReadInt32(intPtr, offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x0006D7D4 File Offset: 0x0006B9D4
		internal void ReadInt32Array(int offset, int[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 4 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(intPtr, destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x0006D838 File Offset: 0x0006BA38
		internal long ReadInt64(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			long num;
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = base.DangerousGetHandle();
				num = Marshal.ReadInt64(intPtr, offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0006D88C File Offset: 0x0006BA8C
		internal IntPtr ReadIntPtr(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			IntPtr intPtr2;
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = base.DangerousGetHandle();
				intPtr2 = Marshal.ReadIntPtr(intPtr, offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return intPtr2;
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0006D8E0 File Offset: 0x0006BAE0
		internal unsafe float ReadSingle(int offset)
		{
			int num = this.ReadInt32(offset);
			return *(float*)(&num);
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x0006D8FC File Offset: 0x0006BAFC
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (IntPtr.Zero != handle)
			{
				SafeNativeMethods.LocalFree(handle);
			}
			return true;
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x0006D930 File Offset: 0x0006BB30
		private void StructureToPtr(int offset, object structure)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.StructureToPtr(structure, intPtr, false);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x0006D988 File Offset: 0x0006BB88
		internal void WriteByte(int offset, byte value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = base.DangerousGetHandle();
				Marshal.WriteByte(intPtr, offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x0006D9DC File Offset: 0x0006BBDC
		internal void WriteBytes(int offset, byte[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, startIndex, intPtr, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x0006DA40 File Offset: 0x0006BC40
		internal void WriteCharArray(int offset, char[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, startIndex, intPtr, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x0006DAA4 File Offset: 0x0006BCA4
		internal void WriteDouble(int offset, double value)
		{
			this.WriteInt64(offset, BitConverter.DoubleToInt64Bits(value));
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x0006DAB4 File Offset: 0x0006BCB4
		internal void WriteInt16(int offset, short value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = base.DangerousGetHandle();
				Marshal.WriteInt16(intPtr, offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x0006DB08 File Offset: 0x0006BD08
		internal void WriteInt16Array(int offset, short[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, startIndex, intPtr, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0006DB6C File Offset: 0x0006BD6C
		internal void WriteInt32(int offset, int value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = base.DangerousGetHandle();
				Marshal.WriteInt32(intPtr, offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0006DBC0 File Offset: 0x0006BDC0
		internal void WriteInt32Array(int offset, int[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 4 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, startIndex, intPtr, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0006DC24 File Offset: 0x0006BE24
		internal void WriteInt64(int offset, long value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = base.DangerousGetHandle();
				Marshal.WriteInt64(intPtr, offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x0006DC78 File Offset: 0x0006BE78
		internal void WriteIntPtr(int offset, IntPtr value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = base.DangerousGetHandle();
				Marshal.WriteIntPtr(intPtr, offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x0006DCCC File Offset: 0x0006BECC
		internal unsafe void WriteSingle(int offset, float value)
		{
			this.WriteInt32(offset, *(int*)(&value));
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0006DCDC File Offset: 0x0006BEDC
		internal void ZeroMemory()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = base.DangerousGetHandle();
				SafeNativeMethods.ZeroMemory(intPtr, (IntPtr)this.Length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x0006DD2C File Offset: 0x0006BF2C
		internal Guid ReadGuid(int offset)
		{
			byte[] array = new byte[16];
			this.ReadBytes(offset, array, 0, 16);
			return new Guid(array);
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0006DD53 File Offset: 0x0006BF53
		internal void WriteGuid(int offset, Guid value)
		{
			this.StructureToPtr(offset, value);
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x0006DD64 File Offset: 0x0006BF64
		internal DateTime ReadDate(int offset)
		{
			short[] array = new short[3];
			this.ReadInt16Array(offset, array, 0, 3);
			return new DateTime((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]));
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0006DD94 File Offset: 0x0006BF94
		internal void WriteDate(int offset, DateTime value)
		{
			short[] array = new short[]
			{
				(short)value.Year,
				(short)value.Month,
				(short)value.Day
			};
			this.WriteInt16Array(offset, array, 0, 3);
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0006DDD4 File Offset: 0x0006BFD4
		internal TimeSpan ReadTime(int offset)
		{
			short[] array = new short[3];
			this.ReadInt16Array(offset, array, 0, 3);
			return new TimeSpan((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]));
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0006DE04 File Offset: 0x0006C004
		internal void WriteTime(int offset, TimeSpan value)
		{
			short[] array = new short[]
			{
				(short)value.Hours,
				(short)value.Minutes,
				(short)value.Seconds
			};
			this.WriteInt16Array(offset, array, 0, 3);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0006DE44 File Offset: 0x0006C044
		internal DateTime ReadDateTime(int offset)
		{
			short[] array = new short[6];
			this.ReadInt16Array(offset, array, 0, 6);
			int num = this.ReadInt32(offset + 12);
			DateTime dateTime = new DateTime((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]), (int)((ushort)array[3]), (int)((ushort)array[4]), (int)((ushort)array[5]));
			return dateTime.AddTicks((long)(num / 100));
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0006DE98 File Offset: 0x0006C098
		internal void WriteDateTime(int offset, DateTime value)
		{
			int num = (int)(value.Ticks % 10000000L) * 100;
			short[] array = new short[]
			{
				(short)value.Year,
				(short)value.Month,
				(short)value.Day,
				(short)value.Hour,
				(short)value.Minute,
				(short)value.Second
			};
			this.WriteInt16Array(offset, array, 0, 6);
			this.WriteInt32(offset + 12, num);
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0006DF18 File Offset: 0x0006C118
		internal decimal ReadNumeric(int offset)
		{
			byte[] array = new byte[20];
			this.ReadBytes(offset, array, 1, 19);
			int[] array2 = new int[]
			{
				0,
				0,
				0,
				(int)array[2] << 16
			};
			if (array[3] == 0)
			{
				array2[3] |= int.MinValue;
			}
			array2[0] = BitConverter.ToInt32(array, 4);
			array2[1] = BitConverter.ToInt32(array, 8);
			array2[2] = BitConverter.ToInt32(array, 12);
			if (BitConverter.ToInt32(array, 16) != 0)
			{
				throw ADP.NumericToDecimalOverflow();
			}
			return new decimal(array2);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0006DF94 File Offset: 0x0006C194
		internal void WriteNumeric(int offset, decimal value, byte precision)
		{
			int[] bits = decimal.GetBits(value);
			byte[] array = new byte[20];
			array[1] = precision;
			Buffer.BlockCopy(bits, 14, array, 2, 2);
			array[3] = ((array[3] == 0) ? 1 : 0);
			Buffer.BlockCopy(bits, 0, array, 4, 12);
			array[16] = 0;
			array[17] = 0;
			array[18] = 0;
			array[19] = 0;
			this.WriteBytes(offset, array, 1, 19);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x0006DFF3 File Offset: 0x0006C1F3
		[Conditional("DEBUG")]
		protected void ValidateCheck(int offset, int count)
		{
			this.Validate(offset, count);
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x0006DFFD File Offset: 0x0006C1FD
		protected void Validate(int offset, int count)
		{
			if (offset < 0 || count < 0 || this.Length < checked(offset + count))
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidBuffer);
			}
		}

		// Token: 0x04000AE9 RID: 2793
		internal const int LMEM_FIXED = 0;

		// Token: 0x04000AEA RID: 2794
		internal const int LMEM_MOVEABLE = 2;

		// Token: 0x04000AEB RID: 2795
		internal const int LMEM_ZEROINIT = 64;

		// Token: 0x04000AEC RID: 2796
		private readonly int _bufferLength;
	}
}
