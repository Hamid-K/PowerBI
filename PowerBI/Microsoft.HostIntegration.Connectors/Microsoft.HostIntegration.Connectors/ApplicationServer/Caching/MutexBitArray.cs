using System;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000248 RID: 584
	internal class MutexBitArray
	{
		// Token: 0x060013A2 RID: 5026 RVA: 0x0003D974 File Offset: 0x0003BB74
		public MutexBitArray(int mutexCount)
		{
			int num = mutexCount / 32;
			if (mutexCount % 32 != 0)
			{
				num++;
			}
			this._mutexBitArray = new int[num];
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0003D9A2 File Offset: 0x0003BBA2
		public bool TryEnter(int index)
		{
			return this.takeMutex(index);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0003D9AC File Offset: 0x0003BBAC
		private bool takeMutex(int index)
		{
			int index2 = MutexBitArray.GetIndex(index);
			int offset = MutexBitArray.GetOffset(index);
			int num = this._mutexBitArray[index2];
			int num2 = 1 << offset;
			while ((num & num2) == 0)
			{
				if (Interlocked.CompareExchange(ref this._mutexBitArray[index2], num | num2, num) == num)
				{
					return true;
				}
				num = this._mutexBitArray[index2];
			}
			return false;
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0003DA02 File Offset: 0x0003BC02
		private static int GetIndex(int index)
		{
			return index / 32;
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0003DA08 File Offset: 0x0003BC08
		private static int GetOffset(int index)
		{
			return index % 32;
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0003DA10 File Offset: 0x0003BC10
		public void Exit(int index)
		{
			int index2 = MutexBitArray.GetIndex(index);
			int offset = MutexBitArray.GetOffset(index);
			int num = ~(1 << offset);
			int num2;
			do
			{
				num2 = this._mutexBitArray[index2];
			}
			while (Interlocked.CompareExchange(ref this._mutexBitArray[index2], num2 & num, num2) != num2);
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0003DA54 File Offset: 0x0003BC54
		internal bool IsLatched(int index)
		{
			int index2 = MutexBitArray.GetIndex(index);
			int offset = MutexBitArray.GetOffset(index);
			int num = this._mutexBitArray[index2];
			int num2 = 1 << offset;
			return (num & num2) != 0;
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0003DA88 File Offset: 0x0003BC88
		internal void Clean()
		{
			for (int i = 0; i < this._mutexBitArray.Length; i++)
			{
				this._mutexBitArray[i] = 0;
			}
		}

		// Token: 0x04000BCA RID: 3018
		private const int SizeOfInt = 32;

		// Token: 0x04000BCB RID: 3019
		private int[] _mutexBitArray;
	}
}
