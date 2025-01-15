using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common.Graph
{
	// Token: 0x02000085 RID: 133
	[ImmutableObject(false)]
	internal sealed class VertexHeap
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x0000C9FA File Offset: 0x0000ABFA
		internal VertexHeap(int capacity)
		{
			this._nextPos = 1;
			this._heap = new VertexHeap.HeapElement[capacity + 1];
			this._heapPos = new int[capacity + 1];
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000CA28 File Offset: 0x0000AC28
		internal bool Insert(int key, double value)
		{
			int num = this._heapPos[key];
			if (num == 0)
			{
				this._heap[this._nextPos] = new VertexHeap.HeapElement(key, value);
				this._heapPos[key] = this._nextPos;
				this.Up(this._nextPos);
				this._nextPos++;
				return true;
			}
			if (this._heap[num].Value < value)
			{
				return false;
			}
			this._heap[num] = new VertexHeap.HeapElement(key, value);
			this.Up(num);
			return true;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		internal void FixKey(int key, double value)
		{
			int num = this._heapPos[key];
			if (num == 0)
			{
				this._heap[this._nextPos] = new VertexHeap.HeapElement(key, value);
				this._heapPos[key] = this._nextPos;
				this.Up(this._nextPos);
				this._nextPos++;
				return;
			}
			double value2 = this._heap[num].Value;
			this._heap[num] = new VertexHeap.HeapElement(key, value);
			if (value2 < value)
			{
				this.Down(num);
				return;
			}
			this.Up(num);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000CB44 File Offset: 0x0000AD44
		internal void Heapify()
		{
			for (int i = (this._nextPos - 1) / 2; i >= 1; i--)
			{
				this.Down(i);
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000CB70 File Offset: 0x0000AD70
		internal bool PushBack(int key, double value)
		{
			if (this.Contains(key))
			{
				return false;
			}
			this._heap[this._nextPos] = new VertexHeap.HeapElement(key, value);
			this._heapPos[key] = this._nextPos;
			this._nextPos++;
			return true;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000CBC0 File Offset: 0x0000ADC0
		internal void RemoveFirst(out int key, out double value)
		{
			key = this._heap[1].Key;
			value = this._heap[1].Value;
			this._heap[1] = this._heap[this._nextPos - 1];
			this._heapPos[this._heap[1].Key] = 1;
			this._heapPos[key] = 0;
			this._nextPos--;
			this.Down(1);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000CC4C File Offset: 0x0000AE4C
		internal void RemoveElement(int key)
		{
			int num = this._heapPos[key];
			if (num != 0)
			{
				this._heapPos[key] = 0;
				this._nextPos--;
				if (this._nextPos != 1 && this._nextPos != num)
				{
					this._heap[num] = this._heap[this._nextPos];
					this._heapPos[this._heap[num].Key] = num;
					if (num > 1 && this._heap[num / 2].Value >= this._heap[num].Value)
					{
						this.Up(num);
						return;
					}
					this.Down(num);
				}
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000CD00 File Offset: 0x0000AF00
		internal double GetElementValue(int key)
		{
			return this.GetValue(this._heapPos[key]);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000CD10 File Offset: 0x0000AF10
		internal bool Contains(int key)
		{
			return this._heapPos[key] != 0;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000CD1D File Offset: 0x0000AF1D
		internal bool IsEmpty()
		{
			return this._nextPos == 1;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000CD28 File Offset: 0x0000AF28
		internal int GetSize()
		{
			return this._nextPos - 1;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000CD32 File Offset: 0x0000AF32
		private double GetValue(int pos)
		{
			return this._heap[pos].Value;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000CD45 File Offset: 0x0000AF45
		private int GetKey(int pos)
		{
			return this._heap[pos].Key;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000CD58 File Offset: 0x0000AF58
		private void Swap(int a, int b)
		{
			VertexHeap.HeapElement heapElement = this._heap[a];
			this._heap[a] = this._heap[b];
			this._heap[b] = heapElement;
			this._heapPos[this._heap[a].Key] = a;
			this._heapPos[this._heap[b].Key] = b;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000CDC9 File Offset: 0x0000AFC9
		private void Up(int n)
		{
			while (n > 1 && this._heap[n].Value < this._heap[n / 2].Value)
			{
				this.Swap(n, n / 2);
				n /= 2;
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000CE08 File Offset: 0x0000B008
		private void Down(int n)
		{
			int num = 2 * n;
			if (num < this._nextPos)
			{
				VertexHeap.HeapElement heapElement = this._heap[n];
				do
				{
					bool flag = true;
					double num2 = this._heap[num].Value;
					if (num + 1 < this._nextPos && this._heap[num + 1].Value < num2)
					{
						num2 = this._heap[num + 1].Value;
						flag = false;
					}
					if (num2 >= heapElement.Value)
					{
						break;
					}
					if (flag)
					{
						this._heap[n] = this._heap[num];
						this._heapPos[this._heap[n].Key] = n;
						n = num;
					}
					else
					{
						this._heap[n] = this._heap[num + 1];
						this._heapPos[this._heap[n].Key] = n;
						n = num + 1;
					}
					num = 2 * n;
				}
				while (num < this._nextPos);
				this._heap[n] = heapElement;
				this._heapPos[this._heap[n].Key] = n;
			}
		}

		// Token: 0x0400011E RID: 286
		private VertexHeap.HeapElement[] _heap;

		// Token: 0x0400011F RID: 287
		private int _nextPos;

		// Token: 0x04000120 RID: 288
		private int[] _heapPos;

		// Token: 0x020000CD RID: 205
		private struct HeapElement
		{
			// Token: 0x0600060A RID: 1546 RVA: 0x0000FB76 File Offset: 0x0000DD76
			internal HeapElement(int key, double value)
			{
				this.Key = key;
				this.Value = value;
			}

			// Token: 0x0600060B RID: 1547 RVA: 0x0000FB86 File Offset: 0x0000DD86
			public override string ToString()
			{
				return StringUtil.FormatInvariant("Key:{0} Value:{1}", this.Key, this.Value);
			}

			// Token: 0x04000218 RID: 536
			internal readonly int Key;

			// Token: 0x04000219 RID: 537
			internal readonly double Value;
		}
	}
}
