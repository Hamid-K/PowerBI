using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004CF RID: 1231
	public static class ObjectUtils
	{
		// Token: 0x06001B6A RID: 7018 RVA: 0x000525C4 File Offset: 0x000507C4
		public static void Swap<T>(ref T obj1, ref T obj2)
		{
			T t = obj1;
			obj1 = obj2;
			obj2 = t;
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x0000CC37 File Offset: 0x0000AE37
		public static void Dump<T>(this T obj)
		{
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x000525EC File Offset: 0x000507EC
		public static int GetInheritanceDepth(this Type type)
		{
			int num = 0;
			Type type2 = type;
			while (type2 != null)
			{
				num++;
				type2 = type2.GetTypeInfo().BaseType;
			}
			return num;
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x00052619 File Offset: 0x00050819
		public static bool Is<T>(this Type type)
		{
			return type == typeof(T);
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x0005262C File Offset: 0x0005082C
		static ObjectUtils()
		{
			ObjectUtils.ValueInitializers.Add((Type t) => t.GetTypeInfo().IsValueType.Then(() => Activator.CreateInstance(t)));
			ObjectUtils.ValueInitializers.Add((Type t) => t.IsArray.Then(() => Array.CreateInstance(t.GetElementType(), 0)));
			ObjectUtils.ValueInitializers.Add((Type t) => t.Is<string>().Then(() => Bottom.Value.ToString()));
			ObjectUtils.ValueInitializers.Add(delegate(Type t)
			{
				ConstructorInfo[] constructors = t.GetConstructors();
				for (int i = 0; i < constructors.Length; i++)
				{
					ConstructorInfo cons = constructors[i];
					Optional<object> optional = from ps in (from p in cons.GetParameters()
							select ObjectUtils.ValueInitializers.FirstValue((Func<Type, Optional<object>> init) => init(p.ParameterType))).WholeSequenceOfValues<object>()
						select cons.Invoke(ps.ToArray<object>());
					if (optional.HasValue)
					{
						return optional;
					}
				}
				return Optional<object>.Nothing;
			});
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x000526AC File Offset: 0x000508AC
		public static object CreateValidValue(this Type type)
		{
			return ObjectUtils.ValueInitializers.FirstValue((Func<Type, Optional<object>> init) => init(type)).OrElseDefault<object>();
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x000526E1 File Offset: 0x000508E1
		public static bool IsTrue(this object obj)
		{
			return obj is bool && (bool)obj;
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x000526F3 File Offset: 0x000508F3
		public static int Indicator(this bool flag)
		{
			return (flag > false) ? 1 : 0;
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x000526F9 File Offset: 0x000508F9
		public static object NullToBottom(this object value)
		{
			return value ?? Bottom.Value;
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x00052705 File Offset: 0x00050905
		public static object BottomToNull(this object value)
		{
			if (!(value is Bottom))
			{
				return value;
			}
			return null;
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x00052714 File Offset: 0x00050914
		public static string InternedFormat(this object obj, Dictionary<object, int> identityCache, ObjectFormatting formatting = ObjectFormatting.ToString)
		{
			if (obj == null)
			{
				return "null";
			}
			if (identityCache == null)
			{
				return formatting.Format(obj);
			}
			int num;
			if (identityCache.TryGetValue(obj, out num))
			{
				return "##" + num.ToString();
			}
			identityCache[obj] = identityCache.Count + 1;
			return formatting.Format(obj);
		}

		// Token: 0x04000D76 RID: 3446
		private static readonly List<Func<Type, Optional<object>>> ValueInitializers = new List<Func<Type, Optional<object>>>();
	}
}
