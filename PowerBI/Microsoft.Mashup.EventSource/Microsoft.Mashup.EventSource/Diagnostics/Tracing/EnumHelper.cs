using System;
using System.Reflection;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200002D RID: 45
	internal static class EnumHelper<UnderlyingType>
	{
		// Token: 0x06000172 RID: 370 RVA: 0x0000B4B8 File Offset: 0x000096B8
		public static UnderlyingType Cast<ValueType>(ValueType value)
		{
			return EnumHelper<UnderlyingType>.Caster<ValueType>.Instance(value);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000B4C5 File Offset: 0x000096C5
		internal static UnderlyingType Identity(UnderlyingType value)
		{
			return value;
		}

		// Token: 0x040000BF RID: 191
		private static readonly MethodInfo IdentityInfo = Statics.GetDeclaredStaticMethod(typeof(EnumHelper<UnderlyingType>), "Identity");

		// Token: 0x0200008B RID: 139
		// (Invoke) Token: 0x06000312 RID: 786
		private delegate UnderlyingType Transformer<ValueType>(ValueType value);

		// Token: 0x0200008C RID: 140
		private static class Caster<ValueType>
		{
			// Token: 0x040001B8 RID: 440
			public static readonly EnumHelper<UnderlyingType>.Transformer<ValueType> Instance = (EnumHelper<UnderlyingType>.Transformer<ValueType>)Statics.CreateDelegate(typeof(EnumHelper<UnderlyingType>.Transformer<ValueType>), EnumHelper<UnderlyingType>.IdentityInfo);
		}
	}
}
