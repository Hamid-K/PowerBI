using System;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C0E RID: 7182
	public struct Pair<T1, T2> : IEquatable<Pair<T1, T2>> where T1 : IEquatable<T1> where T2 : IEquatable<T2>
	{
		// Token: 0x0600B339 RID: 45881 RVA: 0x002474FA File Offset: 0x002456FA
		public Pair(T1 value1, T2 value2)
		{
			this.value1 = value1;
			this.value2 = value2;
		}

		// Token: 0x0600B33A RID: 45882 RVA: 0x0024750C File Offset: 0x0024570C
		public override int GetHashCode()
		{
			T1 t = this.value1;
			int num = ((t != null) ? t.GetHashCode() : 0) * 17;
			T2 t2 = this.value2;
			return num + ((t2 != null) ? t2.GetHashCode() : 0);
		}

		// Token: 0x0600B33B RID: 45883 RVA: 0x00247568 File Offset: 0x00245768
		public override bool Equals(object obj)
		{
			Pair<T1, T2>? pair = obj as Pair<T1, T2>?;
			return pair != null && this.Equals(pair.Value);
		}

		// Token: 0x0600B33C RID: 45884 RVA: 0x00247599 File Offset: 0x00245799
		public bool Equals(Pair<T1, T2> other)
		{
			return Pair<T1, T2>.AreEqualSafe<T1>(this.value1, other.value1) && Pair<T1, T2>.AreEqualSafe<T2>(this.value2, other.value2);
		}

		// Token: 0x0600B33D RID: 45885 RVA: 0x002475C1 File Offset: 0x002457C1
		private static bool AreEqualSafe<T>(T x, T y) where T : IEquatable<T>
		{
			if (x == null)
			{
				return y == null;
			}
			return y != null && x.Equals(y);
		}

		// Token: 0x04005B70 RID: 23408
		private readonly T1 value1;

		// Token: 0x04005B71 RID: 23409
		private readonly T2 value2;
	}
}
