using System;
using System.Runtime.CompilerServices;

namespace System.Text.Json
{
	// Token: 0x02000038 RID: 56
	internal struct BitStack
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00006438 File Offset: 0x00004638
		public int CurrentDepth
		{
			get
			{
				return this._currentDepth;
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00006440 File Offset: 0x00004640
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PushTrue()
		{
			if (this._currentDepth < 64)
			{
				this._allocationFreeContainer = (this._allocationFreeContainer << 1) | 1UL;
			}
			else
			{
				this.PushToArray(true);
			}
			this._currentDepth++;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00006474 File Offset: 0x00004674
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PushFalse()
		{
			if (this._currentDepth < 64)
			{
				this._allocationFreeContainer <<= 1;
			}
			else
			{
				this.PushToArray(false);
			}
			this._currentDepth++;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000064A8 File Offset: 0x000046A8
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void PushToArray(bool value)
		{
			if (this._array == null)
			{
				this._array = new int[2];
			}
			int num = this._currentDepth - 64;
			int num3;
			int num2 = BitStack.Div32Rem(num, out num3);
			if (num2 >= this._array.Length)
			{
				this.DoubleArray(num2);
			}
			int num4 = this._array[num2];
			if (value)
			{
				num4 |= 1 << num3;
			}
			else
			{
				num4 &= ~(1 << num3);
			}
			this._array[num2] = num4;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00006518 File Offset: 0x00004718
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Pop()
		{
			this._currentDepth--;
			bool flag;
			if (this._currentDepth < 64)
			{
				this._allocationFreeContainer >>= 1;
				flag = (this._allocationFreeContainer & 1UL) > 0UL;
			}
			else if (this._currentDepth == 64)
			{
				flag = (this._allocationFreeContainer & 1UL) > 0UL;
			}
			else
			{
				flag = this.PopFromArray();
			}
			return flag;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00006580 File Offset: 0x00004780
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool PopFromArray()
		{
			int num = this._currentDepth - 64 - 1;
			int num3;
			int num2 = BitStack.Div32Rem(num, out num3);
			return (this._array[num2] & (1 << num3)) != 0;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000065B4 File Offset: 0x000047B4
		private void DoubleArray(int minSize)
		{
			int num = Math.Max(minSize + 1, this._array.Length * 2);
			Array.Resize<int>(ref this._array, num);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000065E0 File Offset: 0x000047E0
		public void SetFirstBit()
		{
			this._currentDepth++;
			this._allocationFreeContainer = 1UL;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000065F8 File Offset: 0x000047F8
		public void ResetFirstBit()
		{
			this._currentDepth++;
			this._allocationFreeContainer = 0UL;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00006610 File Offset: 0x00004810
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int Div32Rem(int number, out int remainder)
		{
			uint num = (uint)(number / 32);
			remainder = number & 31;
			return (int)num;
		}

		// Token: 0x04000118 RID: 280
		private const int AllocationFreeMaxDepth = 64;

		// Token: 0x04000119 RID: 281
		private const int DefaultInitialArraySize = 2;

		// Token: 0x0400011A RID: 282
		private int[] _array;

		// Token: 0x0400011B RID: 283
		private ulong _allocationFreeContainer;

		// Token: 0x0400011C RID: 284
		private int _currentDepth;
	}
}
