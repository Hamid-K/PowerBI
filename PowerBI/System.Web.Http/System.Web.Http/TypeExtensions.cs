using System;
using System.ComponentModel;

namespace System
{
	// Token: 0x02000006 RID: 6
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static class TypeExtensions
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002530 File Offset: 0x00000730
		public static bool IsNullable(this Type type)
		{
			return !type.IsValueType || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
		}
	}
}
