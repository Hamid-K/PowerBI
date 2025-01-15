using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x0200035F RID: 863
	internal static class Auxiliary
	{
		// Token: 0x06001980 RID: 6528 RVA: 0x0005E7C8 File Offset: 0x0005C9C8
		[CanBeNull]
		public static T GetCustomAttribute<T>(MemberInfo target) where T : Attribute
		{
			return Auxiliary.GetCustomAttribute(target, typeof(T)) as T;
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0005E7E4 File Offset: 0x0005C9E4
		[CanBeNull]
		public static object GetCustomAttribute(MemberInfo target, Type attrType)
		{
			CustomAttributeData customAttributeData = CustomAttributeData.GetCustomAttributes(target).FirstOrDefault((CustomAttributeData a) => a.Constructor.DeclaringType.FullName.Equals(attrType.FullName, StringComparison.Ordinal));
			if (customAttributeData == null)
			{
				return null;
			}
			ConstructorInfo constructorInfo = null;
			foreach (ConstructorInfo constructorInfo2 in attrType.GetConstructors())
			{
				ParameterInfo[] parameters = constructorInfo2.GetParameters();
				if (parameters.Length == customAttributeData.ConstructorArguments.Count)
				{
					bool flag = true;
					for (int j = 0; j < parameters.Length; j++)
					{
						if (!parameters[j].ParameterType.FullName.Equals(customAttributeData.ConstructorArguments[j].ArgumentType.FullName, StringComparison.Ordinal))
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						constructorInfo = constructorInfo2;
						break;
					}
				}
			}
			object obj = constructorInfo.Invoke(customAttributeData.ConstructorArguments.Select((CustomAttributeTypedArgument a) => a.Value).ToArray<object>());
			foreach (CustomAttributeNamedArgument customAttributeNamedArgument in customAttributeData.NamedArguments)
			{
				attrType.GetProperty(customAttributeNamedArgument.MemberInfo.Name).GetSetMethod().Invoke(obj, new object[] { customAttributeNamedArgument.TypedValue.Value });
			}
			return obj;
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0005E96C File Offset: 0x0005CB6C
		public static T[] GetCustomAttributes<T>(MemberInfo target) where T : Attribute
		{
			T[] array;
			try
			{
				IEnumerable<CustomAttributeData> enumerable = from a in CustomAttributeData.GetCustomAttributes(target)
					where a.Constructor.DeclaringType.FullName == typeof(T).FullName
					select a;
				List<T> list = new List<T>();
				foreach (CustomAttributeData customAttributeData in enumerable)
				{
					ConstructorInfo constructorInfo = null;
					foreach (ConstructorInfo constructorInfo2 in typeof(T).GetConstructors())
					{
						ParameterInfo[] parameters = constructorInfo2.GetParameters();
						if (parameters.Length == customAttributeData.ConstructorArguments.Count)
						{
							bool flag = true;
							for (int j = 0; j < parameters.Length; j++)
							{
								if (!parameters[j].ParameterType.FullName.Equals(customAttributeData.ConstructorArguments[j].ArgumentType.FullName, StringComparison.Ordinal))
								{
									flag = false;
									break;
								}
							}
							if (flag)
							{
								constructorInfo = constructorInfo2;
								break;
							}
						}
					}
					object obj = constructorInfo.Invoke(customAttributeData.ConstructorArguments.Select((CustomAttributeTypedArgument a) => a.Value).ToArray<object>());
					foreach (CustomAttributeNamedArgument customAttributeNamedArgument in customAttributeData.NamedArguments)
					{
						typeof(T).GetProperty(customAttributeNamedArgument.MemberInfo.Name).GetSetMethod().Invoke(obj, new object[] { customAttributeNamedArgument.TypedValue.Value });
					}
					list.Add(obj as T);
				}
				array = list.ToArray();
			}
			catch
			{
				throw;
			}
			return array;
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0005EB88 File Offset: 0x0005CD88
		public static string CreateIdentifierFromString([NotNull] string str)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(str, "str");
			string text = new string((from c in str.ToCharArray()
				where char.IsLetterOrDigit(c) || c == '_'
				select c).SkipWhile((char c) => char.IsDigit(c)).ToArray<char>());
			if (text.Length != str.Length)
			{
				text = "{0}{1}".FormatWithInvariantCulture(new object[]
				{
					text,
					Obfuscation.Obfuscate(str, false)
				});
			}
			return text;
		}
	}
}
