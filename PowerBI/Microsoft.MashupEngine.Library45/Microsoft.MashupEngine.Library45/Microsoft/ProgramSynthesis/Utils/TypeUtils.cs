using System;
using System.Reflection;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000519 RID: 1305
	public static class TypeUtils
	{
		// Token: 0x06001CFE RID: 7422 RVA: 0x0005675A File Offset: 0x0005495A
		public static ConstructorInfo GetConstructor(this Type type, BindingFlags bindingAttr, Type[] parameterTypes)
		{
			return type.GetConstructor(bindingAttr, null, parameterTypes, null);
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x00056766 File Offset: 0x00054966
		public static MethodInfo GetMethod(this Type type, string name, BindingFlags bindingAttr, Type[] parameterTypes)
		{
			return type.GetMethod(name, bindingAttr, null, parameterTypes, null);
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x00056773 File Offset: 0x00054973
		public static object GetDefaultValue(Type t)
		{
			if (!t.GetTypeInfo().IsValueType)
			{
				return null;
			}
			return Activator.CreateInstance(t);
		}
	}
}
