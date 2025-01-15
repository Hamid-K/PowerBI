using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000278 RID: 632
	public static class ExtendedReflection
	{
		// Token: 0x060010D3 RID: 4307 RVA: 0x0003A288 File Offset: 0x00038488
		public static string GetFullyQualifiedMemberName(MemberInfo member)
		{
			Type declaringType = member.DeclaringType;
			if (!(declaringType == null))
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}!{1}.{2}", new object[]
				{
					declaringType.Assembly.ManifestModule.Name,
					declaringType.FullName,
					member.Name
				});
			}
			return member.Name;
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0003A2E8 File Offset: 0x000384E8
		public static CustomAttributeData GetCustomAttribute(IList<CustomAttributeData> customAttributes, string attribute)
		{
			return customAttributes.FirstOrDefault((CustomAttributeData customAttribute) => ExtendedReflection.CheckAttribute(customAttribute, attribute));
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0003A314 File Offset: 0x00038514
		[CanBeNull]
		public static object GetParameterValueFromCustomAttribute(CustomAttributeData attributeData, string paramName)
		{
			IEnumerable<CustomAttributeNamedArgument> namedArguments = attributeData.NamedArguments;
			Func<CustomAttributeNamedArgument, bool> <>9__0;
			Func<CustomAttributeNamedArgument, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (CustomAttributeNamedArgument arg) => arg.MemberInfo.Name.Equals(paramName, StringComparison.OrdinalIgnoreCase));
			}
			using (IEnumerator<CustomAttributeNamedArgument> enumerator = namedArguments.Where(func).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					CustomAttributeNamedArgument customAttributeNamedArgument = enumerator.Current;
					return customAttributeNamedArgument.TypedValue.Value;
				}
			}
			ParameterInfo[] parameters = attributeData.Constructor.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				if (parameters[i].Name.Equals(paramName, StringComparison.OrdinalIgnoreCase))
				{
					return attributeData.ConstructorArguments[i].Value.ToString();
				}
			}
			return null;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0003A3F8 File Offset: 0x000385F8
		public static object GetParameterValueFromCustomAttribute(MemberInfo method, CustomAttributeData attribute, string paramName)
		{
			return ExtendedReflection.GetParameterValueFromCustomAttribute(method, attribute.Constructor.ReflectedType.Name, paramName);
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0003A414 File Offset: 0x00038614
		public static object GetParameterValueFromCustomAttribute(MemberInfo method, string attribute, string paramName)
		{
			object obj = null;
			IEnumerable<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(method);
			Func<CustomAttributeData, bool> <>9__0;
			Func<CustomAttributeData, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (CustomAttributeData customAttribute) => ExtendedReflection.CheckAttribute(customAttribute, attribute));
			}
			using (IEnumerator<CustomAttributeData> enumerator = customAttributes.Where(func).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return ExtendedReflection.GetParameterValueFromCustomAttribute(enumerator.Current, paramName);
				}
			}
			return obj;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0003A49C File Offset: 0x0003869C
		[CanBeNull]
		public static CustomAttributeData GetBaseCustomAttribute(IList<CustomAttributeData> customAttributes, string attribute)
		{
			foreach (CustomAttributeData customAttributeData in customAttributes)
			{
				Type type = customAttributeData.Constructor.DeclaringType;
				while (type != null)
				{
					if (type.FullName.Equals(attribute, StringComparison.Ordinal))
					{
						return customAttributeData;
					}
					type = type.BaseType;
				}
			}
			return null;
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0003A514 File Offset: 0x00038714
		public static bool IsCustomAttributePresent(MemberInfo method, string attribute)
		{
			return ExtendedReflection.IsCustomAttributePresent(CustomAttributeData.GetCustomAttributes(method), attribute);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0003A522 File Offset: 0x00038722
		public static bool IsCustomAttributePresent(ParameterInfo parameter, string attribute)
		{
			return ExtendedReflection.IsCustomAttributePresent(CustomAttributeData.GetCustomAttributes(parameter), attribute);
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0003A530 File Offset: 0x00038730
		public static bool IsCustomAttributePresent(IEnumerable<CustomAttributeData> customAttributes, string attribute)
		{
			return customAttributes.Any((CustomAttributeData customAttribute) => ExtendedReflection.CheckAttribute(customAttribute, attribute));
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0003A55C File Offset: 0x0003875C
		public static IEnumerable<T> GetMethodInfoAttributes<T>(MethodInfo methodInfo) where T : Attribute
		{
			return methodInfo.GetCustomAttributes(typeof(T), true).Cast<T>();
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0003A574 File Offset: 0x00038774
		private static bool CheckAttribute(CustomAttributeData customAttribute, string attribute)
		{
			MemberInfo constructor = customAttribute.Constructor;
			return constructor != null && constructor.ReflectedType.FullName.Equals(attribute, StringComparison.Ordinal);
		}
	}
}
