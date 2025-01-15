using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200003E RID: 62
	public struct FixedLenByteArray : IEquatable<FixedLenByteArray>
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x000064E0 File Offset: 0x000046E0
		public FixedLenByteArray(IntPtr pointer)
		{
			this.Pointer = pointer;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000064EC File Offset: 0x000046EC
		public bool Equals(FixedLenByteArray other)
		{
			return this.Pointer == other.Pointer;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006500 File Offset: 0x00004700
		[NullableContext(1)]
		public override bool Equals(object obj)
		{
			if (obj is FixedLenByteArray)
			{
				FixedLenByteArray fixedLenByteArray = (FixedLenByteArray)obj;
				return this.Equals(fixedLenByteArray);
			}
			return false;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000652C File Offset: 0x0000472C
		public override int GetHashCode()
		{
			return this.Pointer.GetHashCode();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000654C File Offset: 0x0000474C
		[NullableContext(1)]
		public override string ToString()
		{
			return string.Format("Pointer: {0:X16}", this.Pointer.ToInt64());
		}

		// Token: 0x04000074 RID: 116
		public readonly IntPtr Pointer;
	}
}
