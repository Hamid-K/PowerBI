using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200000C RID: 12
	internal class CompositeKey<T1, T2> : IEquatable<CompositeKey<T1, T2>>
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00002C5E File Offset: 0x00000E5E
		public CompositeKey(T1 first, T2 second)
		{
			this.first = first;
			this.second = second;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002C74 File Offset: 0x00000E74
		public static bool operator ==(CompositeKey<T1, T2> left, CompositeKey<T1, T2> right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002C7D File Offset: 0x00000E7D
		public static bool operator !=(CompositeKey<T1, T2> left, CompositeKey<T1, T2> right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002C8C File Offset: 0x00000E8C
		public bool Equals(CompositeKey<T1, T2> other)
		{
			return other != null && (this == other || (object.Equals(other.first, this.first) && object.Equals(other.second, this.second)));
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002CDE File Offset: 0x00000EDE
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CompositeKey<T1, T2>);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002CEC File Offset: 0x00000EEC
		public override int GetHashCode()
		{
			T1 t = this.first;
			int num = t.GetHashCode() * 397;
			T2 t2 = this.second;
			return num ^ t2.GetHashCode();
		}

		// Token: 0x04000015 RID: 21
		private readonly T1 first;

		// Token: 0x04000016 RID: 22
		private readonly T2 second;
	}
}
