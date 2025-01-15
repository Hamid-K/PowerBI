using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200000F RID: 15
	public readonly struct ByteArray : IEquatable<ByteArray>
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002850 File Offset: 0x00000A50
		public ByteArray(IntPtr pointer, int length)
		{
			this.Length = length;
			this.Pointer = pointer;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002860 File Offset: 0x00000A60
		public bool Equals(ByteArray other)
		{
			return this.Length == other.Length && this.Pointer == other.Pointer;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002888 File Offset: 0x00000A88
		[NullableContext(1)]
		public override bool Equals(object obj)
		{
			if (obj is ByteArray)
			{
				ByteArray byteArray = (ByteArray)obj;
				return this.Equals(byteArray);
			}
			return false;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000028B4 File Offset: 0x00000AB4
		public override int GetHashCode()
		{
			return (this.Length * 397) ^ this.Pointer.GetHashCode();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000028E0 File Offset: 0x00000AE0
		[NullableContext(1)]
		public override string ToString()
		{
			return string.Format("Pointer: {0:X16}, Length: {1}", this.Pointer.ToInt64(), this.Length);
		}

		// Token: 0x0400001F RID: 31
		public readonly int Length;

		// Token: 0x04000020 RID: 32
		public readonly IntPtr Pointer;
	}
}
