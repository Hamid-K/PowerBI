using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000014 RID: 20
	internal class CompositeKey<T1, T2> : IEquatable<CompositeKey<T1, T2>>
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00003688 File Offset: 0x00001888
		public CompositeKey(T1 first, T2 second)
		{
			this.first = first;
			this.second = second;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000369E File Offset: 0x0000189E
		public static bool operator ==(CompositeKey<T1, T2> left, CompositeKey<T1, T2> right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000036A7 File Offset: 0x000018A7
		public static bool operator !=(CompositeKey<T1, T2> left, CompositeKey<T1, T2> right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000036B4 File Offset: 0x000018B4
		public bool Equals(CompositeKey<T1, T2> other)
		{
			return !object.ReferenceEquals(null, other) && (object.ReferenceEquals(this, other) || (object.Equals(other.first, this.first) && object.Equals(other.second, this.second)));
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003711 File Offset: 0x00001911
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CompositeKey<T1, T2>);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003720 File Offset: 0x00001920
		public override int GetHashCode()
		{
			T1 t = this.first;
			int num = t.GetHashCode() * 397;
			T2 t2 = this.second;
			return num ^ t2.GetHashCode();
		}

		// Token: 0x04000017 RID: 23
		private readonly T1 first;

		// Token: 0x04000018 RID: 24
		private readonly T2 second;
	}
}
