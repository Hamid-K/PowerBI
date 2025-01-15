using System;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x0200006E RID: 110
	internal readonly struct StructMultiKey<T1, T2> : IEquatable<StructMultiKey<T1, T2>>
	{
		// Token: 0x060005EF RID: 1519 RVA: 0x000192ED File Offset: 0x000174ED
		public StructMultiKey(T1 v1, T2 v2)
		{
			this.Value1 = v1;
			this.Value2 = v2;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00019300 File Offset: 0x00017500
		public override int GetHashCode()
		{
			T1 value = this.Value1;
			int num = ((value != null) ? value.GetHashCode() : 0);
			T2 value2 = this.Value2;
			return num ^ ((value2 != null) ? value2.GetHashCode() : 0);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00019358 File Offset: 0x00017558
		public override bool Equals(object obj)
		{
			if (obj is StructMultiKey<T1, T2>)
			{
				StructMultiKey<T1, T2> structMultiKey = (StructMultiKey<T1, T2>)obj;
				return this.Equals(structMultiKey);
			}
			return false;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001937F File Offset: 0x0001757F
		public bool Equals(StructMultiKey<T1, T2> other)
		{
			return object.Equals(this.Value1, other.Value1) && object.Equals(this.Value2, other.Value2);
		}

		// Token: 0x0400020C RID: 524
		public readonly T1 Value1;

		// Token: 0x0400020D RID: 525
		public readonly T2 Value2;
	}
}
