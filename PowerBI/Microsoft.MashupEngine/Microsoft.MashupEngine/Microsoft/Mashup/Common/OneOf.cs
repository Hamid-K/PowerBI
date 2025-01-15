using System;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C06 RID: 7174
	public struct OneOf<T1, T2>
	{
		// Token: 0x0600B312 RID: 45842 RVA: 0x00247026 File Offset: 0x00245226
		public OneOf(T1 value)
		{
			this.value = value;
		}

		// Token: 0x0600B313 RID: 45843 RVA: 0x00247034 File Offset: 0x00245234
		public OneOf(T2 value)
		{
			this.value = value;
		}

		// Token: 0x0600B314 RID: 45844 RVA: 0x00247042 File Offset: 0x00245242
		public static implicit operator OneOf<T1, T2>(T1 t1)
		{
			return new OneOf<T1, T2>(t1);
		}

		// Token: 0x0600B315 RID: 45845 RVA: 0x0024704A File Offset: 0x0024524A
		public static implicit operator OneOf<T1, T2>(T2 t2)
		{
			return new OneOf<T1, T2>(t2);
		}

		// Token: 0x0600B316 RID: 45846 RVA: 0x00247054 File Offset: 0x00245254
		public static implicit operator T1(OneOf<T1, T2> oneOf)
		{
			if (!(oneOf.value is T1))
			{
				return default(T1);
			}
			return (T1)((object)oneOf.value);
		}

		// Token: 0x0600B317 RID: 45847 RVA: 0x00247084 File Offset: 0x00245284
		public static implicit operator T2(OneOf<T1, T2> oneOf)
		{
			if (!(oneOf.value is T2))
			{
				return default(T2);
			}
			return (T2)((object)oneOf.value);
		}

		// Token: 0x0600B318 RID: 45848 RVA: 0x002470B3 File Offset: 0x002452B3
		public bool Is<T>()
		{
			return this.value is T;
		}

		// Token: 0x0600B319 RID: 45849 RVA: 0x002470C3 File Offset: 0x002452C3
		public T As<T>() where T : class
		{
			return this.value as T;
		}

		// Token: 0x04005B64 RID: 23396
		private readonly object value;
	}
}
