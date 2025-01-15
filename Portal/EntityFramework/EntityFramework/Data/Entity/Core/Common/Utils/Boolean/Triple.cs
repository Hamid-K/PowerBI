using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200061F RID: 1567
	internal struct Triple<T1, T2, T3> : IEquatable<Triple<T1, T2, T3>> where T1 : IEquatable<T1> where T2 : IEquatable<T2> where T3 : IEquatable<T3>
	{
		// Token: 0x06004BCD RID: 19405 RVA: 0x0010B54F File Offset: 0x0010974F
		internal Triple(T1 value1, T2 value2, T3 value3)
		{
			this._value1 = value1;
			this._value2 = value2;
			this._value3 = value3;
		}

		// Token: 0x06004BCE RID: 19406 RVA: 0x0010B568 File Offset: 0x00109768
		public bool Equals(Triple<T1, T2, T3> other)
		{
			T1 value = this._value1;
			if (value.Equals(other._value1))
			{
				T2 value2 = this._value2;
				if (value2.Equals(other._value2))
				{
					T3 value3 = this._value3;
					return value3.Equals(other._value3);
				}
			}
			return false;
		}

		// Token: 0x06004BCF RID: 19407 RVA: 0x0010B5C9 File Offset: 0x001097C9
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06004BD0 RID: 19408 RVA: 0x0010B5DC File Offset: 0x001097DC
		public override int GetHashCode()
		{
			T1 value = this._value1;
			int hashCode = value.GetHashCode();
			T2 value2 = this._value2;
			int num = hashCode ^ value2.GetHashCode();
			T3 value3 = this._value3;
			return num ^ value3.GetHashCode();
		}

		// Token: 0x04001A7C RID: 6780
		private readonly T1 _value1;

		// Token: 0x04001A7D RID: 6781
		private readonly T2 _value2;

		// Token: 0x04001A7E RID: 6782
		private readonly T3 _value3;
	}
}
