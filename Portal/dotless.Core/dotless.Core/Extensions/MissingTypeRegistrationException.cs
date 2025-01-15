using System;
using System.Linq;
using System.Reflection;

namespace dotless.Core.Extensions
{
	// Token: 0x020000BC RID: 188
	public class MissingTypeRegistrationException : InvalidOperationException
	{
		// Token: 0x06000569 RID: 1385 RVA: 0x00017D30 File Offset: 0x00015F30
		public MissingTypeRegistrationException(Type serviceType)
			: base(string.Format("Could not find any registered services for type '{0}'.", MissingTypeRegistrationException.GetFriendlyName(serviceType)))
		{
			this.ServiceType = serviceType;
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00017D4F File Offset: 0x00015F4F
		public Type ServiceType { get; }

		// Token: 0x0600056B RID: 1387 RVA: 0x00017D58 File Offset: 0x00015F58
		private static string GetFriendlyName(Type type)
		{
			if (type == typeof(int))
			{
				return "int";
			}
			if (type == typeof(short))
			{
				return "short";
			}
			if (type == typeof(byte))
			{
				return "byte";
			}
			if (type == typeof(bool))
			{
				return "bool";
			}
			if (type == typeof(char))
			{
				return "char";
			}
			if (type == typeof(long))
			{
				return "long";
			}
			if (type == typeof(float))
			{
				return "float";
			}
			if (type == typeof(double))
			{
				return "double";
			}
			if (type == typeof(decimal))
			{
				return "decimal";
			}
			if (type == typeof(string))
			{
				return "string";
			}
			if (type == typeof(object))
			{
				return "object";
			}
			TypeInfo typeInfo = type.GetTypeInfo();
			if (typeInfo.IsGenericType)
			{
				return MissingTypeRegistrationException.GetGenericFriendlyName(typeInfo);
			}
			return type.Name;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00017E8C File Offset: 0x0001608C
		private static string GetGenericFriendlyName(TypeInfo typeInfo)
		{
			string[] array = typeInfo.GenericTypeArguments.Select(new Func<Type, string>(MissingTypeRegistrationException.GetFriendlyName)).ToArray<string>();
			string text = typeInfo.Name.Split(new char[] { '`' }).First<string>();
			return string.Format("{0}<{1}>", text, string.Join(", ", array));
		}
	}
}
