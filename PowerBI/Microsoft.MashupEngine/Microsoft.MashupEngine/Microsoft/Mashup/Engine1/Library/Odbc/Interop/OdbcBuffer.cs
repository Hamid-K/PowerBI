using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Mashup.Engine1.Library.Odbc.Interop
{
	// Token: 0x020006DC RID: 1756
	internal class OdbcBuffer : SafeHandle
	{
		// Token: 0x06003499 RID: 13465 RVA: 0x000A9157 File Offset: 0x000A7357
		public OdbcBuffer(int initialSize)
			: this(initialSize, true)
		{
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x000A9164 File Offset: 0x000A7364
		private OdbcBuffer(int initialSize, bool zeroBuffer)
			: base(IntPtr.Zero, true)
		{
			if (initialSize > 0)
			{
				int num = (zeroBuffer ? 64 : 0);
				this.bufferLength = initialSize;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					this.handle = NativeMethods.LocalAlloc(num, (IntPtr)initialSize);
				}
				if (IntPtr.Zero == this.handle)
				{
					throw new OutOfMemoryException("OutofMemory", new Win32Exception());
				}
			}
		}

		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x0600349B RID: 13467 RVA: 0x00002105 File Offset: 0x00000305
		private int BaseOffset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x0600349C RID: 13468 RVA: 0x000A91E0 File Offset: 0x000A73E0
		public short ShortLength
		{
			get
			{
				return checked((short)this.Length);
			}
		}

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x0600349D RID: 13469 RVA: 0x000A91E9 File Offset: 0x000A73E9
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x0600349E RID: 13470 RVA: 0x000A91FB File Offset: 0x000A73FB
		public int Length
		{
			get
			{
				return this.bufferLength;
			}
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x000A9204 File Offset: 0x000A7404
		public HandleRef PtrOffset(int offset, int length)
		{
			this.Validate(offset, length);
			IntPtr intPtr = OdbcUtils.IntPtrOffset(base.DangerousGetHandle(), offset);
			return new HandleRef(this, intPtr);
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x000A9230 File Offset: 0x000A7430
		public string PtrToStringUni(int offset)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2);
			string text = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = OdbcUtils.IntPtrOffset(base.DangerousGetHandle(), offset);
				int num = NativeMethods.lstrlenW(intPtr);
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

		// Token: 0x060034A1 RID: 13473 RVA: 0x000A92A4 File Offset: 0x000A74A4
		public string PtrToStringUni(int offset, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			string text = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				text = Marshal.PtrToStringUni(OdbcUtils.IntPtrOffset(base.DangerousGetHandle(), offset), length);
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

		// Token: 0x060034A2 RID: 13474 RVA: 0x000A9308 File Offset: 0x000A7508
		public byte ReadByte(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			byte b;
			try
			{
				base.DangerousAddRef(ref flag);
				b = Marshal.ReadByte(base.DangerousGetHandle(), offset);
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

		// Token: 0x060034A3 RID: 13475 RVA: 0x000A9358 File Offset: 0x000A7558
		public byte[] ReadBytes(int offset, int length)
		{
			byte[] array = new byte[length];
			return this.ReadBytes(offset, array, 0, length);
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000A9378 File Offset: 0x000A7578
		public byte[] ReadBytes(int offset, byte[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.Copy(OdbcUtils.IntPtrOffset(base.DangerousGetHandle(), offset), destination, startIndex, length);
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

		// Token: 0x060034A5 RID: 13477 RVA: 0x000A93DC File Offset: 0x000A75DC
		public char ReadChar(int offset)
		{
			return (char)this.ReadInt16(offset);
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000A93E8 File Offset: 0x000A75E8
		public char[] ReadChars(int offset, char[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.Copy(OdbcUtils.IntPtrOffset(base.DangerousGetHandle(), offset), destination, startIndex, length);
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

		// Token: 0x060034A7 RID: 13479 RVA: 0x000A944C File Offset: 0x000A764C
		public double ReadDouble(int offset)
		{
			return BitConverter.Int64BitsToDouble(this.ReadInt64(offset));
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x000A945C File Offset: 0x000A765C
		public short ReadInt16(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			short num;
			try
			{
				base.DangerousAddRef(ref flag);
				num = Marshal.ReadInt16(base.DangerousGetHandle(), offset);
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

		// Token: 0x060034A9 RID: 13481 RVA: 0x000A94AC File Offset: 0x000A76AC
		public void ReadInt16Array(int offset, short[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.Copy(OdbcUtils.IntPtrOffset(base.DangerousGetHandle(), offset), destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x000A9510 File Offset: 0x000A7710
		public int ReadInt32(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			int num;
			try
			{
				base.DangerousAddRef(ref flag);
				num = Marshal.ReadInt32(base.DangerousGetHandle(), offset);
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

		// Token: 0x060034AB RID: 13483 RVA: 0x000A9560 File Offset: 0x000A7760
		public void ReadInt32Array(int offset, int[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 4 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.Copy(OdbcUtils.IntPtrOffset(base.DangerousGetHandle(), offset), destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x000A95C4 File Offset: 0x000A77C4
		public long ReadInt64(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			long num;
			try
			{
				base.DangerousAddRef(ref flag);
				num = Marshal.ReadInt64(base.DangerousGetHandle(), offset);
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

		// Token: 0x060034AD RID: 13485 RVA: 0x000A9614 File Offset: 0x000A7814
		public IntPtr ReadIntPtr(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			IntPtr intPtr;
			try
			{
				base.DangerousAddRef(ref flag);
				intPtr = Marshal.ReadIntPtr(base.DangerousGetHandle(), offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return intPtr;
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000A9664 File Offset: 0x000A7864
		public unsafe float ReadSingle(int offset)
		{
			int num = this.ReadInt32(offset);
			return *(float*)(&num);
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000A9680 File Offset: 0x000A7880
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (IntPtr.Zero != handle)
			{
				NativeMethods.LocalFree(handle);
			}
			return true;
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x000A96B4 File Offset: 0x000A78B4
		private void StructureToPtr(int offset, object structure)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = OdbcUtils.IntPtrOffset(base.DangerousGetHandle(), offset);
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

		// Token: 0x060034B1 RID: 13489 RVA: 0x000A970C File Offset: 0x000A790C
		public void WriteByte(int offset, byte value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.WriteByte(base.DangerousGetHandle(), offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x000A975C File Offset: 0x000A795C
		public void WriteBytes(int offset, byte[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = OdbcUtils.IntPtrOffset(base.DangerousGetHandle(), offset);
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

		// Token: 0x060034B3 RID: 13491 RVA: 0x000A97C0 File Offset: 0x000A79C0
		public void WriteCharArray(int offset, char[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = OdbcUtils.IntPtrOffset(base.DangerousGetHandle(), offset);
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

		// Token: 0x060034B4 RID: 13492 RVA: 0x000A9824 File Offset: 0x000A7A24
		public void WriteDouble(int offset, double value)
		{
			this.WriteInt64(offset, BitConverter.DoubleToInt64Bits(value));
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x000A9834 File Offset: 0x000A7A34
		public void WriteInt16(int offset, short value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.WriteInt16(base.DangerousGetHandle(), offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x000A9884 File Offset: 0x000A7A84
		public void WriteInt16Array(int offset, short[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = OdbcUtils.IntPtrOffset(base.DangerousGetHandle(), offset);
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

		// Token: 0x060034B7 RID: 13495 RVA: 0x000A98E8 File Offset: 0x000A7AE8
		public void WriteInt32(int offset, int value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.WriteInt32(base.DangerousGetHandle(), offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x000A9938 File Offset: 0x000A7B38
		public void WriteInt32Array(int offset, int[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 4 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr intPtr = OdbcUtils.IntPtrOffset(base.DangerousGetHandle(), offset);
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

		// Token: 0x060034B9 RID: 13497 RVA: 0x000A999C File Offset: 0x000A7B9C
		public void WriteInt64(int offset, long value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.WriteInt64(base.DangerousGetHandle(), offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x000A99EC File Offset: 0x000A7BEC
		public void WriteIntPtr(int offset, IntPtr value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.WriteIntPtr(base.DangerousGetHandle(), offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x000A9A3C File Offset: 0x000A7C3C
		public unsafe void WriteSingle(int offset, float value)
		{
			this.WriteInt32(offset, *(int*)(&value));
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x000A9A4C File Offset: 0x000A7C4C
		public void ZeroMemory()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				NativeMethods.ZeroMemory(base.DangerousGetHandle(), (IntPtr)this.Length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x060034BD RID: 13501 RVA: 0x000A9A9C File Offset: 0x000A7C9C
		public Guid ReadGuid(int offset)
		{
			byte[] array = new byte[16];
			this.ReadBytes(offset, array, 0, 16);
			return new Guid(array);
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x000A9AC3 File Offset: 0x000A7CC3
		public void WriteGuid(int offset, Guid value)
		{
			this.StructureToPtr(offset, value);
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x000A9AD4 File Offset: 0x000A7CD4
		public DateTime ReadDate(int offset)
		{
			short[] array = new short[3];
			this.ReadInt16Array(offset, array, 0, 3);
			return new DateTime((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]));
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x000A9B04 File Offset: 0x000A7D04
		public void WriteDate(int offset, DateTime value)
		{
			short[] array = new short[]
			{
				(short)value.Year,
				(short)value.Month,
				(short)value.Day
			};
			this.WriteInt16Array(offset, array, 0, 3);
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x000A9B44 File Offset: 0x000A7D44
		public TimeSpan ReadTime(int offset)
		{
			short[] array = new short[3];
			this.ReadInt16Array(offset, array, 0, 3);
			return new TimeSpan((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]));
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000A9B74 File Offset: 0x000A7D74
		public void WriteTime(int offset, TimeSpan value)
		{
			short[] array = new short[]
			{
				(short)value.Hours,
				(short)value.Minutes,
				(short)value.Seconds
			};
			this.WriteInt16Array(offset, array, 0, 3);
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x000A9BB4 File Offset: 0x000A7DB4
		public DateTime ReadDateTime(int offset)
		{
			short[] array = new short[6];
			this.ReadInt16Array(offset, array, 0, 6);
			int num = this.ReadInt32(offset + 12);
			DateTime dateTime = new DateTime((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]), (int)((ushort)array[3]), (int)((ushort)array[4]), (int)((ushort)array[5]));
			return dateTime.AddTicks((long)(num / 100));
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x000A9C08 File Offset: 0x000A7E08
		public void WriteDateTime(int offset, DateTime value)
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

		// Token: 0x060034C5 RID: 13509 RVA: 0x000A9C88 File Offset: 0x000A7E88
		public decimal ReadNumeric(int offset)
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
				throw new InvalidOperationException("Overflow");
			}
			return new decimal(array2);
		}

		// Token: 0x060034C6 RID: 13510 RVA: 0x000A9D08 File Offset: 0x000A7F08
		public void WriteNumeric(int offset, decimal value, byte precision)
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

		// Token: 0x060034C7 RID: 13511 RVA: 0x000A9D68 File Offset: 0x000A7F68
		public object MarshalToManaged(int offset, Odbc32.SQL_C sqlctype, int cb)
		{
			if (sqlctype <= Odbc32.SQL_C.SSHORT)
			{
				if (sqlctype <= Odbc32.SQL_C.SBIGINT)
				{
					if (sqlctype == Odbc32.SQL_C.UTINYINT)
					{
						return this.ReadByte(offset);
					}
					if (sqlctype == Odbc32.SQL_C.SBIGINT)
					{
						return this.ReadInt64(offset);
					}
				}
				else
				{
					if (sqlctype == Odbc32.SQL_C.SLONG)
					{
						return this.ReadInt32(offset);
					}
					if (sqlctype == Odbc32.SQL_C.SSHORT)
					{
						return this.ReadInt16(offset);
					}
				}
			}
			else if (sqlctype <= Odbc32.SQL_C.NUMERIC)
			{
				switch (sqlctype)
				{
				case Odbc32.SQL_C.GUID:
					return this.ReadGuid(offset);
				case (Odbc32.SQL_C)(-10):
				case (Odbc32.SQL_C)(-9):
					break;
				case Odbc32.SQL_C.WCHAR:
					if (cb == -3)
					{
						return this.PtrToStringUni(offset);
					}
					cb = Math.Min(cb / 2, (this.Length - 2) / 2);
					return this.PtrToStringUni(offset, cb);
				case Odbc32.SQL_C.BIT:
					return this.ReadByte(offset) > 0;
				default:
					switch (sqlctype)
					{
					case Odbc32.SQL_C.BINARY:
					case Odbc32.SQL_C.CHAR:
						cb = Math.Min(cb, this.Length);
						return this.ReadBytes(offset, cb);
					case Odbc32.SQL_C.NUMERIC:
						return this.ReadNumeric(offset);
					}
					break;
				}
			}
			else
			{
				if (sqlctype == Odbc32.SQL_C.FLOAT)
				{
					return this.ReadSingle(offset);
				}
				if (sqlctype == Odbc32.SQL_C.DOUBLE)
				{
					return this.ReadDouble(offset);
				}
				switch (sqlctype)
				{
				case Odbc32.SQL_C.TYPE_DATE:
					return this.ReadDate(offset);
				case Odbc32.SQL_C.TYPE_TIME:
					return this.ReadTime(offset);
				case Odbc32.SQL_C.TYPE_TIMESTAMP:
					return this.ReadDateTime(offset);
				}
			}
			return null;
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x000A9EF8 File Offset: 0x000A80F8
		public void MarshalToNative(int offset, object value, Odbc32.SQL_C sqlctype, int sizeorprecision, int valueOffset)
		{
			if (sqlctype <= Odbc32.SQL_C.SSHORT)
			{
				if (sqlctype <= Odbc32.SQL_C.SBIGINT)
				{
					if (sqlctype == Odbc32.SQL_C.UTINYINT)
					{
						this.WriteByte(offset, (byte)value);
						return;
					}
					if (sqlctype != Odbc32.SQL_C.SBIGINT)
					{
						return;
					}
					this.WriteInt64(offset, (long)value);
					return;
				}
				else
				{
					if (sqlctype == Odbc32.SQL_C.SLONG)
					{
						this.WriteInt32(offset, (int)value);
						return;
					}
					if (sqlctype != Odbc32.SQL_C.SSHORT)
					{
						return;
					}
					this.WriteInt16(offset, (short)value);
					return;
				}
			}
			else
			{
				if (sqlctype <= Odbc32.SQL_C.NUMERIC)
				{
					switch (sqlctype)
					{
					case Odbc32.SQL_C.GUID:
						this.WriteGuid(offset, (Guid)value);
						return;
					case (Odbc32.SQL_C)(-10):
					case (Odbc32.SQL_C)(-9):
						break;
					case Odbc32.SQL_C.WCHAR:
					{
						int num;
						char[] array;
						if (value is string)
						{
							num = Math.Max(0, ((string)value).Length - valueOffset);
							if (sizeorprecision > 0 && sizeorprecision < num)
							{
								num = sizeorprecision;
							}
							array = ((string)value).ToCharArray(valueOffset, num);
							this.WriteCharArray(offset, array, 0, array.Length);
							this.WriteInt16(offset + array.Length * 2, 0);
							return;
						}
						num = Math.Max(0, ((char[])value).Length - valueOffset);
						if (sizeorprecision > 0 && sizeorprecision < num)
						{
							num = sizeorprecision;
						}
						array = (char[])value;
						this.WriteCharArray(offset, array, valueOffset, num);
						this.WriteInt16(offset + array.Length * 2, 0);
						return;
					}
					case Odbc32.SQL_C.BIT:
						this.WriteByte(offset, ((bool)value > false) ? 1 : 0);
						return;
					default:
						switch (sqlctype)
						{
						case Odbc32.SQL_C.BINARY:
						case Odbc32.SQL_C.CHAR:
						{
							byte[] array2 = (byte[])value;
							int num2 = array2.Length;
							num2 -= valueOffset;
							if (sizeorprecision > 0 && sizeorprecision < num2)
							{
								num2 = sizeorprecision;
							}
							this.WriteBytes(offset, array2, valueOffset, num2);
							return;
						}
						case (Odbc32.SQL_C)(-1):
						case (Odbc32.SQL_C)0:
							break;
						case Odbc32.SQL_C.NUMERIC:
							this.WriteNumeric(offset, (decimal)value, checked((byte)sizeorprecision));
							break;
						default:
							return;
						}
						break;
					}
					return;
				}
				if (sqlctype == Odbc32.SQL_C.FLOAT)
				{
					this.WriteSingle(offset, (float)value);
					return;
				}
				if (sqlctype == Odbc32.SQL_C.DOUBLE)
				{
					this.WriteDouble(offset, (double)value);
					return;
				}
				switch (sqlctype)
				{
				case Odbc32.SQL_C.TYPE_DATE:
					this.WriteDate(offset, (DateTime)value);
					return;
				case Odbc32.SQL_C.TYPE_TIME:
					this.WriteTime(offset, (TimeSpan)value);
					return;
				case Odbc32.SQL_C.TYPE_TIMESTAMP:
					this.WriteODBCDateTime(offset, (DateTime)value);
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000AA100 File Offset: 0x000A8300
		private void WriteODBCDateTime(int offset, DateTime value)
		{
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
			this.WriteInt32(offset + 12, value.Millisecond * 1000000);
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x000AA177 File Offset: 0x000A8377
		[Conditional("DEBUG")]
		protected void ValidateCheck(int offset, int count)
		{
			this.Validate(offset, count);
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x000AA181 File Offset: 0x000A8381
		protected void Validate(int offset, int count)
		{
			if (offset < 0 || count < 0 || this.Length < checked(offset + count))
			{
				throw new InvalidOperationException("Invalid buffer.");
			}
		}

		// Token: 0x04001B5D RID: 7005
		private const int LMEM_FIXED = 0;

		// Token: 0x04001B5E RID: 7006
		private const int LMEM_MOVEABLE = 2;

		// Token: 0x04001B5F RID: 7007
		private const int LMEM_ZEROINIT = 64;

		// Token: 0x04001B60 RID: 7008
		private readonly int bufferLength;
	}
}
