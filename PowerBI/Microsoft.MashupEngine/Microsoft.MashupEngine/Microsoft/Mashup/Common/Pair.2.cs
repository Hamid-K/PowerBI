using System;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C0F RID: 7183
	public static class Pair
	{
		// Token: 0x0600B33E RID: 45886 RVA: 0x002475ED File Offset: 0x002457ED
		public static Pair<T1, T2> New<T1, T2>(T1 value1, T2 value2) where T1 : IEquatable<T1> where T2 : IEquatable<T2>
		{
			return new Pair<T1, T2>(value1, value2);
		}
	}
}
