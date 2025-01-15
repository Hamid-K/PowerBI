using System;

namespace antlr
{
	// Token: 0x0200000D RID: 13
	internal sealed class CircularBuffer
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000392E File Offset: 0x00001B2E
		public int Count
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000004 RID: 4
		public char this[int index]
		{
			get
			{
				return this.array[(this.offset + index) % this.array.Length];
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003950 File Offset: 0x00001B50
		public void Add(char c)
		{
			this.buffer[0] = c;
			this.Add(this.buffer, 1);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003968 File Offset: 0x00001B68
		public void Add(char[] sourceArray, int lengthToCopy)
		{
			if (lengthToCopy + this.length > this.array.Length)
			{
				char[] array = new char[Math.Max(this.array.Length * 2, this.array.Length + lengthToCopy)];
				this.CopyTo(array, 0);
				this.array = array;
				this.offset = 0;
			}
			int num;
			int num2;
			this.GetTailAndHeadLengths(this.length, lengthToCopy, out num, out num2);
			Array.Copy(sourceArray, 0, this.array, this.offset + this.length, num);
			Array.Copy(sourceArray, num, this.array, 0, num2);
			this.length += lengthToCopy;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003A08 File Offset: 0x00001C08
		public void CopyTo(char[] targetArray, int targetIndex)
		{
			int num;
			int num2;
			this.GetTailAndHeadLengths(0, this.length, out num, out num2);
			Array.Copy(this.array, this.offset, targetArray, targetIndex, num);
			Array.Copy(this.array, 0, targetArray, targetIndex + num, num2);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003A4B File Offset: 0x00001C4B
		public void RemoveRange(int offset, int length)
		{
			if (offset != 0 || length > this.length || length < 0)
			{
				throw new NotSupportedException();
			}
			this.offset = (this.offset + length) % this.array.Length;
			this.length -= length;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003A88 File Offset: 0x00001C88
		public void Clear()
		{
			this.offset = 0;
			this.length = 0;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003A98 File Offset: 0x00001C98
		private void GetTailAndHeadLengths(int offset, int length, out int tailLength, out int headLength)
		{
			int num = this.offset + offset;
			tailLength = Math.Min(num + length, this.array.Length) - num;
			headLength = length - tailLength;
		}

		// Token: 0x04000035 RID: 53
		private char[] buffer = new char[1];

		// Token: 0x04000036 RID: 54
		private char[] array = new char[16];

		// Token: 0x04000037 RID: 55
		private int offset;

		// Token: 0x04000038 RID: 56
		private int length;
	}
}
