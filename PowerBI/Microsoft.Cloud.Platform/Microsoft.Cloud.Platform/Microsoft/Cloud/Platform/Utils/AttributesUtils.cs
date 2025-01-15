using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200020C RID: 524
	public static class AttributesUtils
	{
		// Token: 0x06000DD1 RID: 3537 RVA: 0x00030868 File Offset: 0x0002EA68
		public static IEnumerable<FieldAttributePair<T>> GetFieldsWithAttribute<T>(object target) where T : Attribute
		{
			BindingFlags bindingFlags = AttributesUtils.s_defaultBindingFlags | BindingFlags.GetField;
			return AttributesUtils.GetMembersInfoWithAttribute<FieldAttributePair<T>, FieldInfo, T>(target.GetType(), (Type type) => type.GetFields(bindingFlags));
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x000308A4 File Offset: 0x0002EAA4
		public static IEnumerable<MethodAttributePair<T>> GetMethodsWithAttribute<T>(object target) where T : Attribute
		{
			BindingFlags bindingFlags = AttributesUtils.s_defaultBindingFlags;
			return AttributesUtils.GetMembersInfoWithAttribute<MethodAttributePair<T>, MethodInfo, T>(target.GetType(), (Type type) => type.GetMethods(bindingFlags));
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x000308DC File Offset: 0x0002EADC
		public static IEnumerable<MemberAttributePair<T>> GetMembersWithAttribute<T>(object target) where T : Attribute
		{
			BindingFlags bindingFlags = AttributesUtils.s_defaultBindingFlags | BindingFlags.GetField;
			return AttributesUtils.GetMembersInfoWithAttribute<MemberAttributePair<T>, MemberInfo, T>(target.GetType(), (Type type) => type.GetMembers(bindingFlags));
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00030918 File Offset: 0x0002EB18
		public static IEnumerable<MemberAttributePair<T>> GetMembersWithAttribute<T>(Type targetType) where T : Attribute
		{
			BindingFlags bindingFlags = AttributesUtils.s_defaultBindingFlags | BindingFlags.GetField;
			return AttributesUtils.GetMembersInfoWithAttribute<MemberAttributePair<T>, MemberInfo, T>(targetType, (Type type) => type.GetMembers(bindingFlags));
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0003094E File Offset: 0x0002EB4E
		private static IEnumerable<TMemberAttributePair> GetMembersInfoWithAttribute<TMemberAttributePair, TMemberInfo, TAttribute>(Type type, AttributesUtils.GetMembersDelegate getMembers) where TMemberAttributePair : MemberInfoAttributePair<TMemberInfo, TAttribute>, new() where TMemberInfo : MemberInfo where TAttribute : Attribute
		{
			Type attributeType = typeof(TAttribute);
			while (type != typeof(object))
			{
				MemberInfo[] array = getMembers(type);
				if (array != null)
				{
					MemberInfo[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						TMemberInfo tmemberInfo = (TMemberInfo)((object)array2[i]);
						object[] customAttributes = tmemberInfo.GetCustomAttributes(attributeType, true);
						if (customAttributes != null && customAttributes.Length != 0)
						{
							TAttribute tattribute = (TAttribute)((object)customAttributes[0]);
							TMemberAttributePair tmemberAttributePair = new TMemberAttributePair();
							tmemberAttributePair.Member = tmemberInfo;
							tmemberAttributePair.Attribute = tattribute;
							yield return tmemberAttributePair;
						}
					}
					array2 = null;
				}
				type = type.BaseType;
			}
			yield break;
		}

		// Token: 0x0400056E RID: 1390
		private static BindingFlags s_defaultBindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x020006AD RID: 1709
		// (Invoke) Token: 0x06002E39 RID: 11833
		private delegate MemberInfo[] GetMembersDelegate(Type type);
	}
}
