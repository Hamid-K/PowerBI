using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000D1 RID: 209
	internal static class SpanHelpers
	{
		// Token: 0x0600074D RID: 1869 RVA: 0x0001CF9C File Offset: 0x0001B19C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T, TComparable>(this ReadOnlySpan<T> span, TComparable comparable) where TComparable : object, IComparable<T>
		{
			if (comparable == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.comparable);
			}
			return SpanHelpers.BinarySearch<T, TComparable>(MemoryMarshal.GetReference<T>(span), span.Length, comparable);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001CFC4 File Offset: 0x0001B1C4
		public unsafe static int BinarySearch<T, TComparable>(ref T spanStart, int length, TComparable comparable) where TComparable : object, IComparable<T>
		{
			int i = 0;
			int num = length - 1;
			while (i <= num)
			{
				int num2 = (int)((uint)(num + i) >> 1);
				int num3 = comparable.CompareTo(*Unsafe.Add<T>(ref spanStart, num2));
				if (num3 == 0)
				{
					return num2;
				}
				if (num3 > 0)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return ~i;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001D024 File Offset: 0x0001B224
		public static int IndexOf(ref byte searchSpace, int searchSpaceLength, ref byte value, int valueLength)
		{
			if (valueLength == 0)
			{
				return 0;
			}
			byte b = value;
			ref byte ptr = ref Unsafe.Add<byte>(ref value, 1);
			int num = valueLength - 1;
			int num2 = 0;
			for (;;)
			{
				int num3 = searchSpaceLength - num2 - num;
				if (num3 <= 0)
				{
					return -1;
				}
				int num4 = SpanHelpers.IndexOf(Unsafe.Add<byte>(ref searchSpace, num2), b, num3);
				if (num4 == -1)
				{
					return -1;
				}
				num2 += num4;
				if (SpanHelpers.SequenceEqual<byte>(Unsafe.Add<byte>(ref searchSpace, num2 + 1), ref ptr, num))
				{
					break;
				}
				num2++;
			}
			return num2;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001D09C File Offset: 0x0001B29C
		public unsafe static int IndexOfAny(ref byte searchSpace, int searchSpaceLength, ref byte value, int valueLength)
		{
			if (valueLength == 0)
			{
				return 0;
			}
			int num = -1;
			for (int i = 0; i < valueLength; i++)
			{
				int num2 = SpanHelpers.IndexOf(ref searchSpace, *Unsafe.Add<byte>(ref value, i), searchSpaceLength);
				if (num2 < num)
				{
					num = num2;
					searchSpaceLength = num2;
					if (num == 0)
					{
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001D0EC File Offset: 0x0001B2EC
		public unsafe static int LastIndexOfAny(ref byte searchSpace, int searchSpaceLength, ref byte value, int valueLength)
		{
			if (valueLength == 0)
			{
				return 0;
			}
			int num = -1;
			for (int i = 0; i < valueLength; i++)
			{
				int num2 = SpanHelpers.LastIndexOf(ref searchSpace, *Unsafe.Add<byte>(ref value, i), searchSpaceLength);
				if (num2 > num)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001D130 File Offset: 0x0001B330
		public unsafe static int IndexOf(ref byte searchSpace, byte value, int length)
		{
			IntPtr intPtr = (IntPtr)0;
			IntPtr intPtr2 = (IntPtr)length;
			if (Vector.IsHardwareAccelerated && length >= Vector<byte>.Count * 2)
			{
				int num = Unsafe.AsPointer<byte>(ref searchSpace) & (Vector<byte>.Count - 1);
				intPtr2 = (IntPtr)((Vector<byte>.Count - num) & (Vector<byte>.Count - 1));
			}
			Vector<byte> vector2;
			for (;;)
			{
				if ((void*)intPtr2 < 8)
				{
					if ((void*)intPtr2 >= 4)
					{
						intPtr2 -= 4;
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
						{
							goto IL_0254;
						}
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1))
						{
							goto IL_025C;
						}
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2))
						{
							goto IL_026A;
						}
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3))
						{
							goto IL_0278;
						}
						intPtr += 4;
					}
					while ((void*)intPtr2 != null)
					{
						intPtr2 -= 1;
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
						{
							goto IL_0254;
						}
						intPtr += 1;
					}
					if (!Vector.IsHardwareAccelerated || (void*)intPtr >= length)
					{
						return -1;
					}
					intPtr2 = (IntPtr)((length - (void*)intPtr) & ~(Vector<byte>.Count - 1));
					Vector<byte> vector = SpanHelpers.GetVector(value);
					while ((void*)intPtr2 != (void*)intPtr)
					{
						vector2 = Vector.Equals<byte>(vector, Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr)));
						if (!Vector<byte>.Zero.Equals(vector2))
						{
							goto IL_0213;
						}
						intPtr += Vector<byte>.Count;
					}
					if ((void*)intPtr >= length)
					{
						return -1;
					}
					intPtr2 = (IntPtr)(length - (void*)intPtr);
				}
				else
				{
					intPtr2 -= 8;
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
					{
						goto IL_0254;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1))
					{
						goto IL_025C;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2))
					{
						goto IL_026A;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3))
					{
						goto IL_0278;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 4))
					{
						goto IL_0286;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 5))
					{
						goto IL_0294;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 6))
					{
						goto IL_02A2;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 7))
					{
						goto IL_02B0;
					}
					intPtr += 8;
				}
			}
			IL_0213:
			return (void*)intPtr + SpanHelpers.LocateFirstFoundByte(vector2);
			IL_0254:
			return (void*)intPtr;
			IL_025C:
			return (void*)(intPtr + 1);
			IL_026A:
			return (void*)(intPtr + 2);
			IL_0278:
			return (void*)(intPtr + 3);
			IL_0286:
			return (void*)(intPtr + 4);
			IL_0294:
			return (void*)(intPtr + 5);
			IL_02A2:
			return (void*)(intPtr + 6);
			IL_02B0:
			return (void*)(intPtr + 7);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001D400 File Offset: 0x0001B600
		public static int LastIndexOf(ref byte searchSpace, int searchSpaceLength, ref byte value, int valueLength)
		{
			if (valueLength == 0)
			{
				return 0;
			}
			byte b = value;
			ref byte ptr = ref Unsafe.Add<byte>(ref value, 1);
			int num = valueLength - 1;
			int num2 = 0;
			int num4;
			for (;;)
			{
				int num3 = searchSpaceLength - num2 - num;
				if (num3 <= 0)
				{
					return -1;
				}
				num4 = SpanHelpers.LastIndexOf(ref searchSpace, b, num3);
				if (num4 == -1)
				{
					return -1;
				}
				if (SpanHelpers.SequenceEqual<byte>(Unsafe.Add<byte>(ref searchSpace, num4 + 1), ref ptr, num))
				{
					break;
				}
				num2 += num3 - num4;
			}
			return num4;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001D470 File Offset: 0x0001B670
		public unsafe static int LastIndexOf(ref byte searchSpace, byte value, int length)
		{
			IntPtr intPtr = (IntPtr)length;
			IntPtr intPtr2 = (IntPtr)length;
			if (Vector.IsHardwareAccelerated && length >= Vector<byte>.Count * 2)
			{
				int num = Unsafe.AsPointer<byte>(ref searchSpace) & (Vector<byte>.Count - 1);
				intPtr2 = (IntPtr)(((length & (Vector<byte>.Count - 1)) + num) & (Vector<byte>.Count - 1));
			}
			Vector<byte> vector2;
			for (;;)
			{
				if ((void*)intPtr2 < 8)
				{
					if ((void*)intPtr2 >= 4)
					{
						intPtr2 -= 4;
						intPtr -= 4;
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3))
						{
							goto IL_028A;
						}
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2))
						{
							goto IL_027C;
						}
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1))
						{
							goto IL_026E;
						}
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
						{
							break;
						}
					}
					while ((void*)intPtr2 != null)
					{
						intPtr2 -= 1;
						intPtr -= 1;
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
						{
							goto IL_0266;
						}
					}
					if (!Vector.IsHardwareAccelerated || (void*)intPtr == null)
					{
						return -1;
					}
					intPtr2 = (IntPtr)((void*)intPtr & ~(Vector<byte>.Count - 1));
					Vector<byte> vector = SpanHelpers.GetVector(value);
					while ((void*)intPtr2 != Vector<byte>.Count - 1)
					{
						vector2 = Vector.Equals<byte>(vector, Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr - Vector<byte>.Count)));
						if (!Vector<byte>.Zero.Equals(vector2))
						{
							goto IL_022B;
						}
						intPtr -= Vector<byte>.Count;
						intPtr2 -= Vector<byte>.Count;
					}
					if ((void*)intPtr == null)
					{
						return -1;
					}
					intPtr2 = intPtr;
				}
				else
				{
					intPtr2 -= 8;
					intPtr -= 8;
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 7))
					{
						goto IL_02C2;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 6))
					{
						goto IL_02B4;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 5))
					{
						goto IL_02A6;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 4))
					{
						goto IL_0298;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3))
					{
						goto IL_028A;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2))
					{
						goto IL_027C;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1))
					{
						goto IL_026E;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
					{
						break;
					}
				}
			}
			goto IL_0266;
			IL_022B:
			return (int)intPtr - Vector<byte>.Count + SpanHelpers.LocateLastFoundByte(vector2);
			IL_0266:
			return (void*)intPtr;
			IL_026E:
			return (void*)(intPtr + 1);
			IL_027C:
			return (void*)(intPtr + 2);
			IL_028A:
			return (void*)(intPtr + 3);
			IL_0298:
			return (void*)(intPtr + 4);
			IL_02A6:
			return (void*)(intPtr + 5);
			IL_02B4:
			return (void*)(intPtr + 6);
			IL_02C2:
			return (void*)(intPtr + 7);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001D750 File Offset: 0x0001B950
		public unsafe static int IndexOfAny(ref byte searchSpace, byte value0, byte value1, int length)
		{
			IntPtr intPtr = (IntPtr)0;
			IntPtr intPtr2 = (IntPtr)length;
			if (Vector.IsHardwareAccelerated && length >= Vector<byte>.Count * 2)
			{
				int num = Unsafe.AsPointer<byte>(ref searchSpace) & (Vector<byte>.Count - 1);
				intPtr2 = (IntPtr)((Vector<byte>.Count - num) & (Vector<byte>.Count - 1));
			}
			Vector<byte> vector4;
			for (;;)
			{
				if ((void*)intPtr2 < 8)
				{
					if ((void*)intPtr2 >= 4)
					{
						intPtr2 -= 4;
						uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
						if ((uint)value0 == num2 || (uint)value1 == num2)
						{
							goto IL_030E;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
						if ((uint)value0 == num2 || (uint)value1 == num2)
						{
							goto IL_0316;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
						if ((uint)value0 == num2 || (uint)value1 == num2)
						{
							goto IL_0324;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
						if ((uint)value0 == num2 || (uint)value1 == num2)
						{
							goto IL_0332;
						}
						intPtr += 4;
					}
					while ((void*)intPtr2 != null)
					{
						intPtr2 -= 1;
						uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
						if ((uint)value0 == num2 || (uint)value1 == num2)
						{
							goto IL_030E;
						}
						intPtr += 1;
					}
					if (!Vector.IsHardwareAccelerated || (void*)intPtr >= length)
					{
						return -1;
					}
					intPtr2 = (IntPtr)((length - (void*)intPtr) & ~(Vector<byte>.Count - 1));
					Vector<byte> vector = SpanHelpers.GetVector(value0);
					Vector<byte> vector2 = SpanHelpers.GetVector(value1);
					while ((void*)intPtr2 != (void*)intPtr)
					{
						Vector<byte> vector3 = Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
						vector4 = Vector.BitwiseOr<byte>(Vector.Equals<byte>(vector3, vector), Vector.Equals<byte>(vector3, vector2));
						if (!Vector<byte>.Zero.Equals(vector4))
						{
							goto IL_02CD;
						}
						intPtr += Vector<byte>.Count;
					}
					if ((void*)intPtr >= length)
					{
						return -1;
					}
					intPtr2 = (IntPtr)(length - (void*)intPtr);
				}
				else
				{
					intPtr2 -= 8;
					uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_030E;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_0316;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_0324;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_0332;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 4));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_0340;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 5));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_034E;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 6));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_035C;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 7));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_036A;
					}
					intPtr += 8;
				}
			}
			IL_02CD:
			return (void*)intPtr + SpanHelpers.LocateFirstFoundByte(vector4);
			IL_030E:
			return (void*)intPtr;
			IL_0316:
			return (void*)(intPtr + 1);
			IL_0324:
			return (void*)(intPtr + 2);
			IL_0332:
			return (void*)(intPtr + 3);
			IL_0340:
			return (void*)(intPtr + 4);
			IL_034E:
			return (void*)(intPtr + 5);
			IL_035C:
			return (void*)(intPtr + 6);
			IL_036A:
			return (void*)(intPtr + 7);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001DAD8 File Offset: 0x0001BCD8
		public unsafe static int IndexOfAny(ref byte searchSpace, byte value0, byte value1, byte value2, int length)
		{
			IntPtr intPtr = (IntPtr)0;
			IntPtr intPtr2 = (IntPtr)length;
			if (Vector.IsHardwareAccelerated && length >= Vector<byte>.Count * 2)
			{
				int num = Unsafe.AsPointer<byte>(ref searchSpace) & (Vector<byte>.Count - 1);
				intPtr2 = (IntPtr)((Vector<byte>.Count - num) & (Vector<byte>.Count - 1));
			}
			Vector<byte> vector5;
			for (;;)
			{
				if ((void*)intPtr2 < 8)
				{
					if ((void*)intPtr2 >= 4)
					{
						intPtr2 -= 4;
						uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
						if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
						{
							goto IL_03A2;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
						if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
						{
							goto IL_03AA;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
						if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
						{
							goto IL_03B8;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
						if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
						{
							goto IL_03C6;
						}
						intPtr += 4;
					}
					while ((void*)intPtr2 != null)
					{
						intPtr2 -= 1;
						uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
						if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
						{
							goto IL_03A2;
						}
						intPtr += 1;
					}
					if (!Vector.IsHardwareAccelerated || (void*)intPtr >= length)
					{
						return -1;
					}
					intPtr2 = (IntPtr)((length - (void*)intPtr) & ~(Vector<byte>.Count - 1));
					Vector<byte> vector = SpanHelpers.GetVector(value0);
					Vector<byte> vector2 = SpanHelpers.GetVector(value1);
					Vector<byte> vector3 = SpanHelpers.GetVector(value2);
					while ((void*)intPtr2 != (void*)intPtr)
					{
						Vector<byte> vector4 = Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
						vector5 = Vector.BitwiseOr<byte>(Vector.BitwiseOr<byte>(Vector.Equals<byte>(vector4, vector), Vector.Equals<byte>(vector4, vector2)), Vector.Equals<byte>(vector4, vector3));
						if (!Vector<byte>.Zero.Equals(vector5))
						{
							goto IL_035D;
						}
						intPtr += Vector<byte>.Count;
					}
					if ((void*)intPtr >= length)
					{
						return -1;
					}
					intPtr2 = (IntPtr)(length - (void*)intPtr);
				}
				else
				{
					intPtr2 -= 8;
					uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_03A2;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_03AA;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_03B8;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_03C6;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 4));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_03D4;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 5));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_03E2;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 6));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_03F0;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 7));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_03FE;
					}
					intPtr += 8;
				}
			}
			IL_035D:
			return (void*)intPtr + SpanHelpers.LocateFirstFoundByte(vector5);
			IL_03A2:
			return (void*)intPtr;
			IL_03AA:
			return (void*)(intPtr + 1);
			IL_03B8:
			return (void*)(intPtr + 2);
			IL_03C6:
			return (void*)(intPtr + 3);
			IL_03D4:
			return (void*)(intPtr + 4);
			IL_03E2:
			return (void*)(intPtr + 5);
			IL_03F0:
			return (void*)(intPtr + 6);
			IL_03FE:
			return (void*)(intPtr + 7);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001DEF4 File Offset: 0x0001C0F4
		public unsafe static int LastIndexOfAny(ref byte searchSpace, byte value0, byte value1, int length)
		{
			IntPtr intPtr = (IntPtr)length;
			IntPtr intPtr2 = (IntPtr)length;
			if (Vector.IsHardwareAccelerated && length >= Vector<byte>.Count * 2)
			{
				int num = Unsafe.AsPointer<byte>(ref searchSpace) & (Vector<byte>.Count - 1);
				intPtr2 = (IntPtr)(((length & (Vector<byte>.Count - 1)) + num) & (Vector<byte>.Count - 1));
			}
			Vector<byte> vector4;
			for (;;)
			{
				if ((void*)intPtr2 < 8)
				{
					if ((void*)intPtr2 >= 4)
					{
						intPtr2 -= 4;
						intPtr -= 4;
						uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
						if ((uint)value0 == num2 || (uint)value1 == num2)
						{
							goto IL_0347;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
						if ((uint)value0 == num2 || (uint)value1 == num2)
						{
							goto IL_0339;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
						if ((uint)value0 == num2 || (uint)value1 == num2)
						{
							goto IL_032B;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
						if ((uint)value0 == num2)
						{
							break;
						}
						if ((uint)value1 == num2)
						{
							break;
						}
					}
					while ((void*)intPtr2 != null)
					{
						intPtr2 -= 1;
						intPtr -= 1;
						uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
						if ((uint)value0 == num2 || (uint)value1 == num2)
						{
							goto IL_0323;
						}
					}
					if (!Vector.IsHardwareAccelerated || (void*)intPtr == null)
					{
						return -1;
					}
					intPtr2 = (IntPtr)((void*)intPtr & ~(Vector<byte>.Count - 1));
					Vector<byte> vector = SpanHelpers.GetVector(value0);
					Vector<byte> vector2 = SpanHelpers.GetVector(value1);
					while ((void*)intPtr2 != Vector<byte>.Count - 1)
					{
						Vector<byte> vector3 = Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr - Vector<byte>.Count));
						vector4 = Vector.BitwiseOr<byte>(Vector.Equals<byte>(vector3, vector), Vector.Equals<byte>(vector3, vector2));
						if (!Vector<byte>.Zero.Equals(vector4))
						{
							goto IL_02E5;
						}
						intPtr -= Vector<byte>.Count;
						intPtr2 -= Vector<byte>.Count;
					}
					if ((void*)intPtr == null)
					{
						return -1;
					}
					intPtr2 = intPtr;
				}
				else
				{
					intPtr2 -= 8;
					intPtr -= 8;
					uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 7));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_037F;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 6));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_0371;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 5));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_0363;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 4));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_0355;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_0347;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_0339;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						goto IL_032B;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
					if ((uint)value0 == num2 || (uint)value1 == num2)
					{
						break;
					}
				}
			}
			goto IL_0323;
			IL_02E5:
			return (int)intPtr - Vector<byte>.Count + SpanHelpers.LocateLastFoundByte(vector4);
			IL_0323:
			return (void*)intPtr;
			IL_032B:
			return (void*)(intPtr + 1);
			IL_0339:
			return (void*)(intPtr + 2);
			IL_0347:
			return (void*)(intPtr + 3);
			IL_0355:
			return (void*)(intPtr + 4);
			IL_0363:
			return (void*)(intPtr + 5);
			IL_0371:
			return (void*)(intPtr + 6);
			IL_037F:
			return (void*)(intPtr + 7);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001E294 File Offset: 0x0001C494
		public unsafe static int LastIndexOfAny(ref byte searchSpace, byte value0, byte value1, byte value2, int length)
		{
			IntPtr intPtr = (IntPtr)length;
			IntPtr intPtr2 = (IntPtr)length;
			if (Vector.IsHardwareAccelerated && length >= Vector<byte>.Count * 2)
			{
				int num = Unsafe.AsPointer<byte>(ref searchSpace) & (Vector<byte>.Count - 1);
				intPtr2 = (IntPtr)(((length & (Vector<byte>.Count - 1)) + num) & (Vector<byte>.Count - 1));
			}
			Vector<byte> vector5;
			for (;;)
			{
				if ((void*)intPtr2 < 8)
				{
					if ((void*)intPtr2 >= 4)
					{
						intPtr2 -= 4;
						intPtr -= 4;
						uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
						if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
						{
							goto IL_03DB;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
						if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
						{
							goto IL_03CD;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
						if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
						{
							goto IL_03BF;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
						if ((uint)value0 == num2 || (uint)value1 == num2)
						{
							break;
						}
						if ((uint)value2 == num2)
						{
							break;
						}
					}
					while ((void*)intPtr2 != null)
					{
						intPtr2 -= 1;
						intPtr -= 1;
						uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
						if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
						{
							goto IL_03B7;
						}
					}
					if (!Vector.IsHardwareAccelerated || (void*)intPtr == null)
					{
						return -1;
					}
					intPtr2 = (IntPtr)((void*)intPtr & ~(Vector<byte>.Count - 1));
					Vector<byte> vector = SpanHelpers.GetVector(value0);
					Vector<byte> vector2 = SpanHelpers.GetVector(value1);
					Vector<byte> vector3 = SpanHelpers.GetVector(value2);
					while ((void*)intPtr2 != Vector<byte>.Count - 1)
					{
						Vector<byte> vector4 = Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr - Vector<byte>.Count));
						vector5 = Vector.BitwiseOr<byte>(Vector.BitwiseOr<byte>(Vector.Equals<byte>(vector4, vector), Vector.Equals<byte>(vector4, vector2)), Vector.Equals<byte>(vector4, vector3));
						if (!Vector<byte>.Zero.Equals(vector5))
						{
							goto IL_0377;
						}
						intPtr -= Vector<byte>.Count;
						intPtr2 -= Vector<byte>.Count;
					}
					if ((void*)intPtr == null)
					{
						return -1;
					}
					intPtr2 = intPtr;
				}
				else
				{
					intPtr2 -= 8;
					intPtr -= 8;
					uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 7));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_0413;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 6));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_0405;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 5));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_03F7;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 4));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_03E9;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_03DB;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_03CD;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						goto IL_03BF;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
					if ((uint)value0 == num2 || (uint)value1 == num2 || (uint)value2 == num2)
					{
						break;
					}
				}
			}
			goto IL_03B7;
			IL_0377:
			return (int)intPtr - Vector<byte>.Count + SpanHelpers.LocateLastFoundByte(vector5);
			IL_03B7:
			return (void*)intPtr;
			IL_03BF:
			return (void*)(intPtr + 1);
			IL_03CD:
			return (void*)(intPtr + 2);
			IL_03DB:
			return (void*)(intPtr + 3);
			IL_03E9:
			return (void*)(intPtr + 4);
			IL_03F7:
			return (void*)(intPtr + 5);
			IL_0405:
			return (void*)(intPtr + 6);
			IL_0413:
			return (void*)(intPtr + 7);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001E6C8 File Offset: 0x0001C8C8
		public unsafe static bool SequenceEqual(ref byte first, ref byte second, NUInt length)
		{
			if (!Unsafe.AreSame<byte>(ref first, ref second))
			{
				IntPtr intPtr = (IntPtr)0;
				IntPtr intPtr2 = (IntPtr)((void*)length);
				if (Vector.IsHardwareAccelerated && (void*)intPtr2 >= Vector<byte>.Count)
				{
					intPtr2 -= Vector<byte>.Count;
					while ((void*)intPtr2 != (void*)intPtr)
					{
						if (Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref first, intPtr)) != Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref second, intPtr)))
						{
							return false;
						}
						intPtr += Vector<byte>.Count;
					}
					return Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref first, intPtr2)) == Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref second, intPtr2));
				}
				if ((void*)intPtr2 >= sizeof(UIntPtr))
				{
					intPtr2 -= sizeof(UIntPtr);
					while ((void*)intPtr2 != (void*)intPtr)
					{
						if (Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref first, intPtr)) != Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref second, intPtr)))
						{
							return false;
						}
						intPtr += sizeof(UIntPtr);
					}
					return Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref first, intPtr2)) == Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref second, intPtr2));
				}
				while ((void*)intPtr2 != (void*)intPtr)
				{
					if (*Unsafe.AddByteOffset<byte>(ref first, intPtr) != *Unsafe.AddByteOffset<byte>(ref second, intPtr))
					{
						return false;
					}
					intPtr += 1;
				}
				return true;
			}
			return true;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001E82C File Offset: 0x0001CA2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateFirstFoundByte(Vector<byte> match)
		{
			Vector<ulong> vector = Vector.AsVectorUInt64<byte>(match);
			ulong num = 0UL;
			int i;
			for (i = 0; i < Vector<ulong>.Count; i++)
			{
				num = vector[i];
				if (num != 0UL)
				{
					break;
				}
			}
			return i * 8 + SpanHelpers.LocateFirstFoundByte(num);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001E874 File Offset: 0x0001CA74
		public unsafe static int SequenceCompareTo(ref byte first, int firstLength, ref byte second, int secondLength)
		{
			if (!Unsafe.AreSame<byte>(ref first, ref second))
			{
				IntPtr intPtr = (IntPtr)((firstLength < secondLength) ? firstLength : secondLength);
				IntPtr intPtr2 = (IntPtr)0;
				IntPtr intPtr3 = (IntPtr)((void*)intPtr);
				if (Vector.IsHardwareAccelerated && (void*)intPtr3 != Vector<byte>.Count)
				{
					intPtr3 -= Vector<byte>.Count;
					while ((void*)intPtr3 != (void*)intPtr2)
					{
						if (Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref first, intPtr2)) != Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref second, intPtr2)))
						{
							break;
						}
						intPtr2 += Vector<byte>.Count;
					}
				}
				else if ((void*)intPtr3 != sizeof(UIntPtr))
				{
					intPtr3 -= sizeof(UIntPtr);
					while ((void*)intPtr3 != (void*)intPtr2)
					{
						if (Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref first, intPtr2)) != Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref second, intPtr2)))
						{
							break;
						}
						intPtr2 += sizeof(UIntPtr);
					}
				}
				while ((void*)intPtr != (void*)intPtr2)
				{
					int num = Unsafe.AddByteOffset<byte>(ref first, intPtr2).CompareTo(*Unsafe.AddByteOffset<byte>(ref second, intPtr2));
					if (num != 0)
					{
						return num;
					}
					intPtr2 += 1;
				}
			}
			return firstLength - secondLength;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001E9C4 File Offset: 0x0001CBC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateLastFoundByte(Vector<byte> match)
		{
			Vector<ulong> vector = Vector.AsVectorUInt64<byte>(match);
			ulong num = 0UL;
			int i;
			for (i = Vector<ulong>.Count - 1; i >= 0; i--)
			{
				num = vector[i];
				if (num != 0UL)
				{
					break;
				}
			}
			return i * 8 + SpanHelpers.LocateLastFoundByte(num);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001EA10 File Offset: 0x0001CC10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateFirstFoundByte(ulong match)
		{
			ulong num = match ^ (match - 1UL);
			return (int)(num * 283686952306184UL >> 57);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001EA38 File Offset: 0x0001CC38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateLastFoundByte(ulong match)
		{
			int num = 7;
			while (match > 0UL)
			{
				match <<= 8;
				num--;
			}
			return num;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001EA60 File Offset: 0x0001CC60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector<byte> GetVector(byte vectorByte)
		{
			return Vector.AsVectorByte<uint>(new Vector<uint>((uint)vectorByte * 16843009U));
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001EA74 File Offset: 0x0001CC74
		public unsafe static int SequenceCompareTo(ref char first, int firstLength, ref char second, int secondLength)
		{
			int num = firstLength - secondLength;
			if (!Unsafe.AreSame<char>(ref first, ref second))
			{
				IntPtr intPtr = (IntPtr)((firstLength < secondLength) ? firstLength : secondLength);
				IntPtr intPtr2 = (IntPtr)0;
				if ((void*)intPtr >= sizeof(UIntPtr) / 2)
				{
					if (Vector.IsHardwareAccelerated && (void*)intPtr >= Vector<ushort>.Count)
					{
						IntPtr intPtr3 = intPtr - Vector<ushort>.Count;
						while (!(Unsafe.ReadUnaligned<Vector<ushort>>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref first, intPtr2))) != Unsafe.ReadUnaligned<Vector<ushort>>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref second, intPtr2)))))
						{
							intPtr2 += Vector<ushort>.Count;
							if ((void*)intPtr3 < (void*)intPtr2)
							{
								break;
							}
						}
					}
					while ((void*)intPtr >= (void*)(intPtr2 + sizeof(UIntPtr) / 2) && !(Unsafe.ReadUnaligned<UIntPtr>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref first, intPtr2))) != Unsafe.ReadUnaligned<UIntPtr>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref second, intPtr2)))))
					{
						intPtr2 += sizeof(UIntPtr) / 2;
					}
				}
				if (sizeof(UIntPtr) > 4 && (void*)intPtr >= (void*)(intPtr2 + 2) && Unsafe.ReadUnaligned<int>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref first, intPtr2))) == Unsafe.ReadUnaligned<int>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref second, intPtr2))))
				{
					intPtr2 += 2;
				}
				while ((void*)intPtr2 < (void*)intPtr)
				{
					int num2 = Unsafe.Add<char>(ref first, intPtr2).CompareTo(*Unsafe.Add<char>(ref second, intPtr2));
					if (num2 != 0)
					{
						return num2;
					}
					intPtr2 += 1;
				}
			}
			return num;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001EC1C File Offset: 0x0001CE1C
		public unsafe static int IndexOf(ref char searchSpace, char value, int length)
		{
			fixed (char* ptr = &searchSpace)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2;
				char* ptr4 = ptr3 + length;
				if (Vector.IsHardwareAccelerated && length >= Vector<ushort>.Count * 2)
				{
					int num = (ptr3 & (Unsafe.SizeOf<Vector<ushort>>() - 1)) / 2;
					length = (Vector<ushort>.Count - num) & (Vector<ushort>.Count - 1);
				}
				Vector<ushort> vector2;
				for (;;)
				{
					if (length < 4)
					{
						while (length > 0)
						{
							length--;
							if (*ptr3 == value)
							{
								goto IL_0145;
							}
							ptr3++;
						}
						if (!Vector.IsHardwareAccelerated || ptr3 >= ptr4)
						{
							return -1;
						}
						length = (int)((long)(ptr4 - ptr3) & (long)(~(long)(Vector<ushort>.Count - 1)));
						Vector<ushort> vector = new Vector<ushort>((ushort)value);
						while (length > 0)
						{
							vector2 = Vector.Equals<ushort>(vector, Unsafe.Read<Vector<ushort>>((void*)ptr3));
							if (!Vector<ushort>.Zero.Equals(vector2))
							{
								goto IL_010E;
							}
							ptr3 += Vector<ushort>.Count;
							length -= Vector<ushort>.Count;
						}
						if (ptr3 >= ptr4)
						{
							return -1;
						}
						length = (int)((long)(ptr4 - ptr3));
					}
					else
					{
						length -= 4;
						if (*ptr3 == value)
						{
							goto IL_0145;
						}
						if (ptr3[1] == value)
						{
							goto IL_0141;
						}
						if (ptr3[2] == value)
						{
							goto IL_013D;
						}
						if (ptr3[3] == value)
						{
							goto IL_0139;
						}
						ptr3 += 4;
					}
				}
				IL_010E:
				return (int)((long)(ptr3 - ptr2)) + SpanHelpers.LocateFirstFoundChar(vector2);
				IL_0139:
				ptr3++;
				IL_013D:
				ptr3++;
				IL_0141:
				ptr3++;
				IL_0145:
				return (int)((long)(ptr3 - ptr2));
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001ED7C File Offset: 0x0001CF7C
		public unsafe static int LastIndexOf(ref char searchSpace, char value, int length)
		{
			fixed (char* ptr = &searchSpace)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + length;
				char* ptr4 = ptr2;
				if (Vector.IsHardwareAccelerated && length >= Vector<ushort>.Count * 2)
				{
					length = (ptr3 & (Unsafe.SizeOf<Vector<ushort>>() - 1)) / 2;
				}
				char* ptr5;
				Vector<ushort> vector2;
				for (;;)
				{
					if (length < 4)
					{
						while (length > 0)
						{
							length--;
							ptr3--;
							if (*ptr3 == value)
							{
								goto IL_0135;
							}
						}
						if (!Vector.IsHardwareAccelerated || ptr3 == ptr4)
						{
							return -1;
						}
						length = (int)((long)(ptr3 - ptr4) & (long)(~(long)(Vector<ushort>.Count - 1)));
						Vector<ushort> vector = new Vector<ushort>((ushort)value);
						while (length > 0)
						{
							ptr5 = ptr3 - Vector<ushort>.Count;
							vector2 = Vector.Equals<ushort>(vector, Unsafe.Read<Vector<ushort>>((void*)ptr5));
							if (!Vector<ushort>.Zero.Equals(vector2))
							{
								goto IL_0109;
							}
							ptr3 -= Vector<ushort>.Count;
							length -= Vector<ushort>.Count;
						}
						if (ptr3 == ptr4)
						{
							return -1;
						}
						length = (int)((long)(ptr3 - ptr4));
					}
					else
					{
						length -= 4;
						ptr3 -= 4;
						if (ptr3[3] == value)
						{
							goto IL_0151;
						}
						if (ptr3[2] == value)
						{
							goto IL_0147;
						}
						if (ptr3[1] == value)
						{
							goto IL_013D;
						}
						if (*ptr3 == value)
						{
							goto IL_0135;
						}
					}
				}
				IL_0109:
				return (int)((long)(ptr5 - ptr4)) + SpanHelpers.LocateLastFoundChar(vector2);
				IL_0135:
				return (int)((long)(ptr3 - ptr4));
				IL_013D:
				return (int)((long)(ptr3 - ptr4)) + 1;
				IL_0147:
				return (int)((long)(ptr3 - ptr4)) + 2;
				IL_0151:
				return (int)((long)(ptr3 - ptr4)) + 3;
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001EEE8 File Offset: 0x0001D0E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateFirstFoundChar(Vector<ushort> match)
		{
			Vector<ulong> vector = Vector.AsVectorUInt64<ushort>(match);
			ulong num = 0UL;
			int i;
			for (i = 0; i < Vector<ulong>.Count; i++)
			{
				num = vector[i];
				if (num != 0UL)
				{
					break;
				}
			}
			return i * 4 + SpanHelpers.LocateFirstFoundChar(num);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001EF30 File Offset: 0x0001D130
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateFirstFoundChar(ulong match)
		{
			ulong num = match ^ (match - 1UL);
			return (int)(num * 4295098372UL >> 49);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001EF58 File Offset: 0x0001D158
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateLastFoundChar(Vector<ushort> match)
		{
			Vector<ulong> vector = Vector.AsVectorUInt64<ushort>(match);
			ulong num = 0UL;
			int i;
			for (i = Vector<ulong>.Count - 1; i >= 0; i--)
			{
				num = vector[i];
				if (num != 0UL)
				{
					break;
				}
			}
			return i * 4 + SpanHelpers.LocateLastFoundChar(num);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001EFA4 File Offset: 0x0001D1A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateLastFoundChar(ulong match)
		{
			int num = 3;
			while (match > 0UL)
			{
				match <<= 16;
				num--;
			}
			return num;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001EFCC File Offset: 0x0001D1CC
		public static int IndexOf<T>(ref T searchSpace, int searchSpaceLength, ref T value, int valueLength) where T : object, IEquatable<T>
		{
			if (valueLength == 0)
			{
				return 0;
			}
			T t = value;
			ref T ptr = ref Unsafe.Add<T>(ref value, 1);
			int num = valueLength - 1;
			int num2 = 0;
			for (;;)
			{
				int num3 = searchSpaceLength - num2 - num;
				if (num3 <= 0)
				{
					return -1;
				}
				int num4 = SpanHelpers.IndexOf<T>(Unsafe.Add<T>(ref searchSpace, num2), t, num3);
				if (num4 == -1)
				{
					return -1;
				}
				num2 += num4;
				if (SpanHelpers.SequenceEqual<T>(Unsafe.Add<T>(ref searchSpace, num2 + 1), ref ptr, num))
				{
					break;
				}
				num2++;
			}
			return num2;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001F048 File Offset: 0x0001D248
		public unsafe static int IndexOf<T>(ref T searchSpace, T value, int length) where T : object, IEquatable<T>
		{
			IntPtr intPtr = (IntPtr)0;
			while (length >= 8)
			{
				length -= 8;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr)))
				{
					IL_020E:
					return (void*)intPtr;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 1)))
				{
					IL_0216:
					return (void*)(intPtr + 1);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 2)))
				{
					IL_0224:
					return (void*)(intPtr + 2);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 3)))
				{
					IL_0232:
					return (void*)(intPtr + 3);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 4)))
				{
					return (void*)(intPtr + 4);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 5)))
				{
					return (void*)(intPtr + 5);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 6)))
				{
					return (void*)(intPtr + 6);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 7)))
				{
					return (void*)(intPtr + 7);
				}
				intPtr += 8;
			}
			if (length >= 4)
			{
				length -= 4;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr)))
				{
					goto IL_020E;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 1)))
				{
					goto IL_0216;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 2)))
				{
					goto IL_0224;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 3)))
				{
					goto IL_0232;
				}
				intPtr += 4;
			}
			while (length > 0)
			{
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr)))
				{
					goto IL_020E;
				}
				intPtr += 1;
				length--;
			}
			return -1;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001F2D0 File Offset: 0x0001D4D0
		public unsafe static int IndexOfAny<T>(ref T searchSpace, T value0, T value1, int length) where T : object, IEquatable<T>
		{
			int i = 0;
			while (length - i >= 8)
			{
				T t = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return i;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 1);
				if (value0.Equals(t) || value1.Equals(t))
				{
					IL_02DD:
					return i + 1;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 2);
				if (value0.Equals(t) || value1.Equals(t))
				{
					IL_02E1:
					return i + 2;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 3);
				if (value0.Equals(t) || value1.Equals(t))
				{
					IL_02E5:
					return i + 3;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 4);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return i + 4;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 5);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return i + 5;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 6);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return i + 6;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 7);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return i + 7;
				}
				i += 8;
			}
			if (length - i >= 4)
			{
				T t = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return i;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 1);
				if (value0.Equals(t) || value1.Equals(t))
				{
					goto IL_02DD;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 2);
				if (value0.Equals(t) || value1.Equals(t))
				{
					goto IL_02E1;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 3);
				if (value0.Equals(t) || value1.Equals(t))
				{
					goto IL_02E5;
				}
				i += 4;
			}
			while (i < length)
			{
				T t = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001F5DC File Offset: 0x0001D7DC
		public unsafe static int IndexOfAny<T>(ref T searchSpace, T value0, T value1, T value2, int length) where T : object, IEquatable<T>
		{
			int i = 0;
			while (length - i >= 8)
			{
				T t = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return i;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 1);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					IL_03D7:
					return i + 1;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 2);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					IL_03DB:
					return i + 2;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 3);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					IL_03DF:
					return i + 3;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 4);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return i + 4;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 5);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return i + 5;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 6);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return i + 6;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 7);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return i + 7;
				}
				i += 8;
			}
			if (length - i >= 4)
			{
				T t = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return i;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 1);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					goto IL_03D7;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 2);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					goto IL_03DB;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 3);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					goto IL_03DF;
				}
				i += 4;
			}
			while (i < length)
			{
				T t = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001F9E0 File Offset: 0x0001DBE0
		public unsafe static int IndexOfAny<T>(ref T searchSpace, int searchSpaceLength, ref T value, int valueLength) where T : object, IEquatable<T>
		{
			if (valueLength == 0)
			{
				return 0;
			}
			int num = -1;
			for (int i = 0; i < valueLength; i++)
			{
				int num2 = SpanHelpers.IndexOf<T>(ref searchSpace, *Unsafe.Add<T>(ref value, i), searchSpaceLength);
				if (num2 < num)
				{
					num = num2;
					searchSpaceLength = num2;
					if (num == 0)
					{
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001FA34 File Offset: 0x0001DC34
		public static int LastIndexOf<T>(ref T searchSpace, int searchSpaceLength, ref T value, int valueLength) where T : object, IEquatable<T>
		{
			if (valueLength == 0)
			{
				return 0;
			}
			T t = value;
			ref T ptr = ref Unsafe.Add<T>(ref value, 1);
			int num = valueLength - 1;
			int num2 = 0;
			int num4;
			for (;;)
			{
				int num3 = searchSpaceLength - num2 - num;
				if (num3 <= 0)
				{
					return -1;
				}
				num4 = SpanHelpers.LastIndexOf<T>(ref searchSpace, t, num3);
				if (num4 == -1)
				{
					return -1;
				}
				if (SpanHelpers.SequenceEqual<T>(Unsafe.Add<T>(ref searchSpace, num4 + 1), ref ptr, num))
				{
					break;
				}
				num2 += num3 - num4;
			}
			return num4;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001FAA8 File Offset: 0x0001DCA8
		public unsafe static int LastIndexOf<T>(ref T searchSpace, T value, int length) where T : object, IEquatable<T>
		{
			while (length >= 8)
			{
				length -= 8;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 7)))
				{
					return length + 7;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 6)))
				{
					return length + 6;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 5)))
				{
					return length + 5;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 4)))
				{
					return length + 4;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 3)))
				{
					IL_01D1:
					return length + 3;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 2)))
				{
					IL_01CD:
					return length + 2;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 1)))
				{
					IL_01C9:
					return length + 1;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length)))
				{
					return length;
				}
			}
			if (length >= 4)
			{
				length -= 4;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 3)))
				{
					goto IL_01D1;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 2)))
				{
					goto IL_01CD;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 1)))
				{
					goto IL_01C9;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length)))
				{
					return length;
				}
			}
			while (length > 0)
			{
				length--;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length)))
				{
					return length;
				}
			}
			return -1;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001FCA0 File Offset: 0x0001DEA0
		public unsafe static int LastIndexOfAny<T>(ref T searchSpace, T value0, T value1, int length) where T : object, IEquatable<T>
		{
			while (length >= 8)
			{
				length -= 8;
				T t = *Unsafe.Add<T>(ref searchSpace, length + 7);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return length + 7;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 6);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return length + 6;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 5);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return length + 5;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 4);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return length + 4;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 3);
				if (value0.Equals(t) || value1.Equals(t))
				{
					IL_02E2:
					return length + 3;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 2);
				if (value0.Equals(t) || value1.Equals(t))
				{
					IL_02DE:
					return length + 2;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 1);
				if (value0.Equals(t) || value1.Equals(t))
				{
					IL_02DA:
					return length + 1;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return length;
				}
			}
			if (length >= 4)
			{
				length -= 4;
				T t = *Unsafe.Add<T>(ref searchSpace, length + 3);
				if (value0.Equals(t) || value1.Equals(t))
				{
					goto IL_02E2;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 2);
				if (value0.Equals(t) || value1.Equals(t))
				{
					goto IL_02DE;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 1);
				if (value0.Equals(t) || value1.Equals(t))
				{
					goto IL_02DA;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(t))
				{
					return length;
				}
				if (value1.Equals(t))
				{
					return length;
				}
			}
			while (length > 0)
			{
				length--;
				T t = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return length;
				}
			}
			return -1;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001FFA8 File Offset: 0x0001E1A8
		public unsafe static int LastIndexOfAny<T>(ref T searchSpace, T value0, T value1, T value2, int length) where T : object, IEquatable<T>
		{
			while (length >= 8)
			{
				length -= 8;
				T t = *Unsafe.Add<T>(ref searchSpace, length + 7);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return length + 7;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 6);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return length + 6;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 5);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return length + 5;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 4);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return length + 4;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 3);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					IL_03EF:
					return length + 3;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 2);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					IL_03EA:
					return length + 2;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 1);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					IL_03E5:
					return length + 1;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return length;
				}
			}
			if (length >= 4)
			{
				length -= 4;
				T t = *Unsafe.Add<T>(ref searchSpace, length + 3);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					goto IL_03EF;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 2);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					goto IL_03EA;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 1);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					goto IL_03E5;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return length;
				}
				if (value2.Equals(t))
				{
					return length;
				}
			}
			while (length > 0)
			{
				length--;
				T t = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return length;
				}
			}
			return -1;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000203C0 File Offset: 0x0001E5C0
		public unsafe static int LastIndexOfAny<T>(ref T searchSpace, int searchSpaceLength, ref T value, int valueLength) where T : object, IEquatable<T>
		{
			if (valueLength == 0)
			{
				return 0;
			}
			int num = -1;
			for (int i = 0; i < valueLength; i++)
			{
				int num2 = SpanHelpers.LastIndexOf<T>(ref searchSpace, *Unsafe.Add<T>(ref value, i), searchSpaceLength);
				if (num2 > num)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00020408 File Offset: 0x0001E608
		public unsafe static bool SequenceEqual<T>(ref T first, ref T second, int length) where T : object, IEquatable<T>
		{
			if (!Unsafe.AreSame<T>(ref first, ref second))
			{
				IntPtr intPtr = (IntPtr)0;
				while (length >= 8)
				{
					length -= 8;
					if (!Unsafe.Add<T>(ref first, intPtr).Equals(*Unsafe.Add<T>(ref second, intPtr)) || !Unsafe.Add<T>(ref first, intPtr + 1).Equals(*Unsafe.Add<T>(ref second, intPtr + 1)) || !Unsafe.Add<T>(ref first, intPtr + 2).Equals(*Unsafe.Add<T>(ref second, intPtr + 2)) || !Unsafe.Add<T>(ref first, intPtr + 3).Equals(*Unsafe.Add<T>(ref second, intPtr + 3)) || !Unsafe.Add<T>(ref first, intPtr + 4).Equals(*Unsafe.Add<T>(ref second, intPtr + 4)) || !Unsafe.Add<T>(ref first, intPtr + 5).Equals(*Unsafe.Add<T>(ref second, intPtr + 5)) || !Unsafe.Add<T>(ref first, intPtr + 6).Equals(*Unsafe.Add<T>(ref second, intPtr + 6)) || !Unsafe.Add<T>(ref first, intPtr + 7).Equals(*Unsafe.Add<T>(ref second, intPtr + 7)))
					{
						return false;
					}
					intPtr += 8;
				}
				if (length >= 4)
				{
					length -= 4;
					if (!Unsafe.Add<T>(ref first, intPtr).Equals(*Unsafe.Add<T>(ref second, intPtr)) || !Unsafe.Add<T>(ref first, intPtr + 1).Equals(*Unsafe.Add<T>(ref second, intPtr + 1)) || !Unsafe.Add<T>(ref first, intPtr + 2).Equals(*Unsafe.Add<T>(ref second, intPtr + 2)) || !Unsafe.Add<T>(ref first, intPtr + 3).Equals(*Unsafe.Add<T>(ref second, intPtr + 3)))
					{
						return false;
					}
					intPtr += 4;
				}
				while (length > 0)
				{
					if (!Unsafe.Add<T>(ref first, intPtr).Equals(*Unsafe.Add<T>(ref second, intPtr)))
					{
						return false;
					}
					intPtr += 1;
					length--;
				}
			}
			return true;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x000206B4 File Offset: 0x0001E8B4
		public unsafe static int SequenceCompareTo<T>(ref T first, int firstLength, ref T second, int secondLength) where T : object, IComparable<T>
		{
			int num = firstLength;
			if (num > secondLength)
			{
				num = secondLength;
			}
			for (int i = 0; i < num; i++)
			{
				int num2 = Unsafe.Add<T>(ref first, i).CompareTo(*Unsafe.Add<T>(ref second, i));
				if (num2 != 0)
				{
					return num2;
				}
			}
			return firstLength.CompareTo(secondLength);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00020710 File Offset: 0x0001E910
		public unsafe static void CopyTo<T>(ref T dst, int dstLength, ref T src, int srcLength)
		{
			IntPtr intPtr = Unsafe.ByteOffset<T>(ref src, Unsafe.Add<T>(ref src, srcLength));
			IntPtr intPtr2 = Unsafe.ByteOffset<T>(ref dst, Unsafe.Add<T>(ref dst, dstLength));
			IntPtr intPtr3 = Unsafe.ByteOffset<T>(ref src, ref dst);
			if (!((sizeof(IntPtr) == 4) ? ((int)intPtr3 < (int)intPtr || (int)intPtr3 > -(int)intPtr2) : ((long)intPtr3 < (long)intPtr || (long)intPtr3 > -(long)intPtr2)) && !SpanHelpers.IsReferenceOrContainsReferences<T>())
			{
				ref byte ptr = ref Unsafe.As<T, byte>(ref dst);
				ref byte ptr2 = ref Unsafe.As<T, byte>(ref src);
				ulong num = (ulong)(long)intPtr;
				uint num3;
				for (ulong num2 = 0UL; num2 < num; num2 += (ulong)num3)
				{
					num3 = ((num - num2 > (ulong)(-1)) ? uint.MaxValue : ((uint)(num - num2)));
					Unsafe.CopyBlock(Unsafe.Add<byte>(ref ptr, (IntPtr)((long)num2)), Unsafe.Add<byte>(ref ptr2, (IntPtr)((long)num2)), num3);
				}
				return;
			}
			bool flag = ((sizeof(IntPtr) == 4) ? ((int)intPtr3 > -(int)intPtr2) : ((long)intPtr3 > -(long)intPtr2));
			int num4 = (flag ? 1 : (-1));
			int num5 = (flag ? 0 : (srcLength - 1));
			int i;
			for (i = 0; i < (srcLength & -8); i += 8)
			{
				*Unsafe.Add<T>(ref dst, num5) = *Unsafe.Add<T>(ref src, num5);
				*Unsafe.Add<T>(ref dst, num5 + num4) = *Unsafe.Add<T>(ref src, num5 + num4);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 2) = *Unsafe.Add<T>(ref src, num5 + num4 * 2);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 3) = *Unsafe.Add<T>(ref src, num5 + num4 * 3);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 4) = *Unsafe.Add<T>(ref src, num5 + num4 * 4);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 5) = *Unsafe.Add<T>(ref src, num5 + num4 * 5);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 6) = *Unsafe.Add<T>(ref src, num5 + num4 * 6);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 7) = *Unsafe.Add<T>(ref src, num5 + num4 * 7);
				num5 += num4 * 8;
			}
			if (i < (srcLength & -4))
			{
				*Unsafe.Add<T>(ref dst, num5) = *Unsafe.Add<T>(ref src, num5);
				*Unsafe.Add<T>(ref dst, num5 + num4) = *Unsafe.Add<T>(ref src, num5 + num4);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 2) = *Unsafe.Add<T>(ref src, num5 + num4 * 2);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 3) = *Unsafe.Add<T>(ref src, num5 + num4 * 3);
				num5 += num4 * 4;
				i += 4;
			}
			while (i < srcLength)
			{
				*Unsafe.Add<T>(ref dst, num5) = *Unsafe.Add<T>(ref src, num5);
				num5 += num4;
				i++;
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00020A74 File Offset: 0x0001EC74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static IntPtr Add<T>(this IntPtr start, int index)
		{
			if (sizeof(IntPtr) == 4)
			{
				uint num = (uint)(index * Unsafe.SizeOf<T>());
				return (IntPtr)((void*)((byte*)(void*)start + num));
			}
			ulong num2 = (ulong)((long)index * (long)Unsafe.SizeOf<T>());
			return (IntPtr)((void*)((byte*)(void*)start + num2));
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00020AC0 File Offset: 0x0001ECC0
		public static bool IsReferenceOrContainsReferences<T>()
		{
			return SpanHelpers.PerTypeValues<T>.IsReferenceOrContainsReferences;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00020AC8 File Offset: 0x0001ECC8
		private static bool IsReferenceOrContainsReferencesCore(Type type)
		{
			if (type.GetTypeInfo().IsPrimitive)
			{
				return false;
			}
			if (!type.GetTypeInfo().IsValueType)
			{
				return true;
			}
			Type underlyingType = Nullable.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
			}
			if (type.GetTypeInfo().IsEnum)
			{
				return false;
			}
			foreach (FieldInfo fieldInfo in type.GetTypeInfo().DeclaredFields)
			{
				if (!fieldInfo.IsStatic && SpanHelpers.IsReferenceOrContainsReferencesCore(fieldInfo.FieldType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00020B90 File Offset: 0x0001ED90
		public unsafe static void ClearLessThanPointerSized(byte* ptr, UIntPtr byteLength)
		{
			if (sizeof(UIntPtr) == 4)
			{
				Unsafe.InitBlockUnaligned((void*)ptr, 0, (uint)byteLength);
				return;
			}
			ulong num = (ulong)byteLength;
			uint num2 = (uint)(num & (ulong)(-1));
			Unsafe.InitBlockUnaligned((void*)ptr, 0, num2);
			num -= (ulong)num2;
			ptr += num2;
			while (num > 0UL)
			{
				num2 = ((num >= (ulong)(-1)) ? uint.MaxValue : ((uint)num));
				Unsafe.InitBlockUnaligned((void*)ptr, 0, num2);
				ptr += num2;
				num -= (ulong)num2;
			}
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00020C0C File Offset: 0x0001EE0C
		public static void ClearLessThanPointerSized(ref byte b, UIntPtr byteLength)
		{
			if (sizeof(UIntPtr) == 4)
			{
				Unsafe.InitBlockUnaligned(ref b, 0, (uint)byteLength);
				return;
			}
			ulong num = (ulong)byteLength;
			uint num2 = (uint)(num & (ulong)(-1));
			Unsafe.InitBlockUnaligned(ref b, 0, num2);
			num -= (ulong)num2;
			long num3 = (long)((ulong)num2);
			while (num > 0UL)
			{
				num2 = ((num >= (ulong)(-1)) ? uint.MaxValue : ((uint)num));
				ref byte ptr = ref Unsafe.Add<byte>(ref b, (IntPtr)num3);
				Unsafe.InitBlockUnaligned(ref ptr, 0, num2);
				num3 += (long)((ulong)num2);
				num -= (ulong)num2;
			}
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00020C90 File Offset: 0x0001EE90
		public unsafe static void ClearPointerSizedWithoutReferences(ref byte b, UIntPtr byteLength)
		{
			IntPtr intPtr = IntPtr.Zero;
			while (intPtr.LessThanEqual(byteLength - sizeof(SpanHelpers.Reg64)))
			{
				*Unsafe.As<byte, SpanHelpers.Reg64>(Unsafe.Add<byte>(ref b, intPtr)) = default(SpanHelpers.Reg64);
				intPtr += sizeof(SpanHelpers.Reg64);
			}
			if (intPtr.LessThanEqual(byteLength - sizeof(SpanHelpers.Reg32)))
			{
				*Unsafe.As<byte, SpanHelpers.Reg32>(Unsafe.Add<byte>(ref b, intPtr)) = default(SpanHelpers.Reg32);
				intPtr += sizeof(SpanHelpers.Reg32);
			}
			if (intPtr.LessThanEqual(byteLength - sizeof(SpanHelpers.Reg16)))
			{
				*Unsafe.As<byte, SpanHelpers.Reg16>(Unsafe.Add<byte>(ref b, intPtr)) = default(SpanHelpers.Reg16);
				intPtr += sizeof(SpanHelpers.Reg16);
			}
			if (intPtr.LessThanEqual(byteLength - 8))
			{
				*Unsafe.As<byte, long>(Unsafe.Add<byte>(ref b, intPtr)) = 0L;
				intPtr += 8;
			}
			if (sizeof(IntPtr) == 4 && intPtr.LessThanEqual(byteLength - 4))
			{
				*Unsafe.As<byte, int>(Unsafe.Add<byte>(ref b, intPtr)) = 0;
				intPtr += 4;
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00020DA8 File Offset: 0x0001EFA8
		public unsafe static void ClearPointerSizedWithReferences(ref IntPtr ip, UIntPtr pointerSizeLength)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			while ((intPtr2 = intPtr + 8).LessThanEqual(pointerSizeLength))
			{
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 0) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 1) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 2) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 3) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 4) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 5) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 6) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 7) = 0;
				intPtr = intPtr2;
			}
			if ((intPtr2 = intPtr + 4).LessThanEqual(pointerSizeLength))
			{
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 0) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 1) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 2) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 3) = 0;
				intPtr = intPtr2;
			}
			if ((intPtr2 = intPtr + 2).LessThanEqual(pointerSizeLength))
			{
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 0) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 1) = 0;
				intPtr = intPtr2;
			}
			if ((intPtr + 1).LessThanEqual(pointerSizeLength))
			{
				*Unsafe.Add<IntPtr>(ref ip, intPtr) = 0;
			}
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00020F38 File Offset: 0x0001F138
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool LessThanEqual(this IntPtr index, UIntPtr length)
		{
			if (sizeof(UIntPtr) != 4)
			{
				return (long)index <= (long)(ulong)length;
			}
			return (int)index <= (int)(uint)length;
		}

		// Token: 0x04000237 RID: 567
		private const ulong XorPowerOfTwoToHighByte = 283686952306184UL;

		// Token: 0x04000238 RID: 568
		private const ulong XorPowerOfTwoToHighChar = 4295098372UL;

		// Token: 0x02000146 RID: 326
		internal struct ComparerComparable<T, TComparer> : IComparable<T> where TComparer : object, IComparer<T>
		{
			// Token: 0x06000A17 RID: 2583 RVA: 0x0002C490 File Offset: 0x0002A690
			public ComparerComparable(T value, TComparer comparer)
			{
				this._value = value;
				this._comparer = comparer;
			}

			// Token: 0x06000A18 RID: 2584 RVA: 0x0002C4A0 File Offset: 0x0002A6A0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public int CompareTo(T other)
			{
				TComparer comparer = this._comparer;
				return comparer.Compare(this._value, other);
			}

			// Token: 0x04000325 RID: 805
			private readonly T _value;

			// Token: 0x04000326 RID: 806
			private readonly TComparer _comparer;
		}

		// Token: 0x02000147 RID: 327
		private struct Reg64
		{
		}

		// Token: 0x02000148 RID: 328
		private struct Reg32
		{
		}

		// Token: 0x02000149 RID: 329
		private struct Reg16
		{
		}

		// Token: 0x0200014A RID: 330
		public static class PerTypeValues<T>
		{
			// Token: 0x06000A19 RID: 2585 RVA: 0x0002C4CC File Offset: 0x0002A6CC
			private static IntPtr MeasureArrayAdjustment()
			{
				T[] array = new T[1];
				return Unsafe.ByteOffset<T>(ref Unsafe.As<Pinnable<T>>(array).Data, ref array[0]);
			}

			// Token: 0x04000327 RID: 807
			public static readonly bool IsReferenceOrContainsReferences = SpanHelpers.IsReferenceOrContainsReferencesCore(typeof(T));

			// Token: 0x04000328 RID: 808
			public static readonly T[] EmptyArray = new T[0];

			// Token: 0x04000329 RID: 809
			public static readonly IntPtr ArrayAdjustment = SpanHelpers.PerTypeValues<T>.MeasureArrayAdjustment();
		}
	}
}
