using System;
using System.Collections.Generic;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000C3 RID: 195
	internal static class TypeSystem
	{
		// Token: 0x0600024A RID: 586 RVA: 0x0001B084 File Offset: 0x0001B084
		internal static Type GetElementType(Type seqType)
		{
			Type type = TypeSystem.FindIEnumerable(seqType);
			if (type == null)
			{
				return seqType;
			}
			return type.GetGenericArguments()[0];
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0001B0AC File Offset: 0x0001B0AC
		private static Type FindIEnumerable(Type seqType)
		{
			if (seqType == null || seqType == typeof(string))
			{
				return null;
			}
			if (seqType.IsArray)
			{
				return typeof(IEnumerable<>).MakeGenericType(new Type[] { seqType.GetElementType() });
			}
			if (seqType.IsGenericType)
			{
				foreach (Type type in seqType.GetGenericArguments())
				{
					Type type2 = typeof(IEnumerable<>).MakeGenericType(new Type[] { type });
					if (type2.IsAssignableFrom(seqType))
					{
						return type2;
					}
				}
			}
			Type[] interfaces = seqType.GetInterfaces();
			if (interfaces != null && interfaces.Length != 0)
			{
				Type[] array = interfaces;
				for (int i = 0; i < array.Length; i++)
				{
					Type type3 = TypeSystem.FindIEnumerable(array[i]);
					if (type3 != null)
					{
						return type3;
					}
				}
			}
			if (seqType.BaseType != null && seqType.BaseType != typeof(object))
			{
				return TypeSystem.FindIEnumerable(seqType.BaseType);
			}
			return null;
		}
	}
}
