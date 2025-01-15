using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200000C RID: 12
	internal class CompositeKey<T1, T2> : IEquatable<CompositeKey<T1, T2>>
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00003046 File Offset: 0x00001246
		public CompositeKey(T1 first, T2 second)
		{
			this.first = first;
			this.second = second;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000305C File Offset: 0x0000125C
		public static bool operator ==(CompositeKey<T1, T2> left, CompositeKey<T1, T2> right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003065 File Offset: 0x00001265
		public static bool operator !=(CompositeKey<T1, T2> left, CompositeKey<T1, T2> right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003074 File Offset: 0x00001274
		public bool Equals(CompositeKey<T1, T2> other)
		{
			return other != null && (this == other || (object.Equals(other.first, this.first) && object.Equals(other.second, this.second)));
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000030C6 File Offset: 0x000012C6
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CompositeKey<T1, T2>);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000030D4 File Offset: 0x000012D4
		public override int GetHashCode()
		{
			T1 t = this.first;
			int num = t.GetHashCode() * 397;
			T2 t2 = this.second;
			return num ^ t2.GetHashCode();
		}

		// Token: 0x04000016 RID: 22
		private readonly T1 first;

		// Token: 0x04000017 RID: 23
		private readonly T2 second;
	}
}
